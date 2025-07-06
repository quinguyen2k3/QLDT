import React, { useEffect } from "react";
import $ from "jquery";
import "select2";
import "select2/dist/css/select2.min.css";

function Selector(props) {
  const { id, label, options = [], placeholderText = "-- Chọn --" } = props;

  useEffect(() => {
    $(`#${id}`).select2({
      theme: "bootstrap4",
      width: "100%",
    });

    return () => {
      $(`#${id}`).select2("destroy");
    };
  }, [id, options]);

  return (
    <div className="form-group">
      <label htmlFor={id}>{label}</label>
      <select
        id={id}
        className="form-control select2"
        style={{ width: "100%" }}
        defaultValue=""
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
