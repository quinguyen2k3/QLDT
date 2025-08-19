import React, { useEffect, useRef } from "react";
import $ from "jquery";
import "select2";
import "select2/dist/css/select2.min.css";

function Selector(props) {
    const {
        id,
        label,
        options = [],
        placeholderText = "-- Chọn --",
        name = "",
        value = "",
        onChange,
        disabled = false,
        labelField = "name",
        labelFormatter = null
    } = props;
    const selectRef = useRef();

    useEffect(() => {
        const $select = $(selectRef.current);
        $select.select2({
            theme: "bootstrap4",
            width: "100%",
            placeholder: placeholderText,
            allowClear: true
        });

        $select.on("change", (e) => {
            if (onChange) {
                onChange({
                    target: {
                        name,
                        value: e.target.value
                    }
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

    // Hàm lấy label cho option
    const getOptionLabel = (opt) => {
        if (labelFormatter) {
            return labelFormatter(opt) || "N/A";
        }
        return opt[labelField] || opt.name || opt.hour || "N/A";
    };

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
                {options.map((opt) => (
                    <option key={opt.id} value={opt.id}>
                        {getOptionLabel(opt)}
                    </option>
                ))}
            </select>
        </div>
    );
}

export default Selector;