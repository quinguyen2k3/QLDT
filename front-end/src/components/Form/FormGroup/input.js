import React from "react";

function FormInput({
  id,
  label,
  placeholder = "",
  defaultValue = "",
  type = "text",
}) {
  return (
    <div className="form-group">
      <label htmlFor={id}>{label}</label>
      <input
        type={type}
        id={id}
        className="form-control"
        placeholder={placeholder}
        defaultValue={defaultValue}
      />
    </div>
  );
}

export default FormInput;
