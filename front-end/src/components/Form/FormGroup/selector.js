import React, { useEffect, useRef } from "react";
import $ from "jquery";
import "select2";
import "select2/dist/css/select2.min.css";

function Selector(props) {
    const { id, label, options = [], placeholderText = "-- Chọn --", name = "", value = "", onChange } = props;
    const selectRef = useRef();

    useEffect(() => {
        const $select = $(`#${id}`);
        $select.select2({
            theme: "bootstrap4",
            width: "100%",
        });

        if (onChange) {
            $select.on("change", (e) => {
                onChange({
                    target: {
                        name,
                        value: e.target.value
                    }
                });
            });
        }

        return () => {
            $select.select2("destroy");
        };
    }, [id]);

    useEffect(() => {
        $(`#${id}`).val(value).trigger('change.select2');
    }, [value, id]);

    return (
        <div className="form-group">
            <label htmlFor={id}>{label}</label>
            <select
                ref={selectRef}
                id={id}
                name={name}
                className="form-control select2"
                style={{ width: "100%" }}
            >
                <option value="">{placeholderText}</option>
                {options.map((opt) => (
                    <option key={opt.id} value={opt.id}>
                        {opt.name}
                    </option>
                ))}
            </select>
        </div>
    );
}

export default Selector;
