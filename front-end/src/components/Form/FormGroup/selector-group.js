import { useEffect, useRef } from "react";
import $ from "jquery";
import "select2";
import "select2/dist/css/select2.min.css";

function SelectorGroup(props) {
    const {
        id,
        label,
        options = [],
        placeholderText = "-- Chọn --",
        name = "",
        value = "",
        onChange,
        disabled = false,
        groupByField = null, 
        groupLabelMap = {}, 
        labelField = "name",
    } = props;
    
    const selectRef = useRef();

    useEffect(() => {
        const $select = $(selectRef.current);
        $select.select2({
            theme: "bootstrap4",
            width: "100%",
            placeholder: placeholderText,
            allowClear: true,
        });

        $select.on("change", (e) => {
            if (onChange) {
                onChange({
                    target: {
                        name,
                        value: e.target.value,
                    },
                });
            }
        });

        return () => {
            $select.select2("destroy");
        };
    }, [name, onChange, placeholderText]);

    useEffect(() => {
        const $select = $(selectRef.current);
        $select.val(value || "").trigger("change.select2");
    }, [value]);

    const getGroupLabel = (groupKey) => {
        return groupLabelMap[groupKey] || groupKey || "Không xác định";
    };

    const groupedOptions = groupByField
        ? options.reduce((acc, opt) => {
              const groupKey = opt[groupByField] || "unknown";
              if (!acc[groupKey]) {
                  acc[groupKey] = { name: getGroupLabel(groupKey), options: [] };
              }
              acc[groupKey].options.push(opt);
              return acc;
          }, {})
        : null;

    const sortedGroupedOptions = groupByField
        ? Object.entries(groupedOptions).sort((a, b) => a[1].name.localeCompare(b[1].name))
        : null;

    return (
        <div className="form-group">
            <label htmlFor={id}>{label}</label>
            <select
                ref={selectRef}
                id={id}
                name={name}
                className="form-control select2"
                style={{ width: "100%" }}
                disabled={disabled}
            >
                <option value="">{placeholderText}</option>
                {groupByField ? (
                    sortedGroupedOptions.map(([groupKey, group]) => (
                        <optgroup key={groupKey} label={group.name}>
                            {group.options.map(opt => (
                                <option key={opt.id} value={opt.id}>
                                    {opt[labelField] || opt.name || "N/A"}
                                </option>
                            ))}
                        </optgroup>
                    ))
                ) : (
                    options.map(opt => (
                        <option key={opt.id} value={opt.id}>
                            {opt[labelField] || opt.name || "N/A"}
                        </option>
                    ))
                )}
            </select>
        </div>
    );
}

export default SelectorGroup;