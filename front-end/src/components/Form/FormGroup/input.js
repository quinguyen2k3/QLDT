import React from "react";

function FormInput({
  id,
  label,
  value,
  name = "",
  onChange,
  placeholder = "",
  defaultValue = "",
  type = "text",
}) {
  const inputProps = {
    type,
    id,
    name,
    className: "form-control",
    placeholder,
  };

  if (onChange) {
    inputProps.value = value ?? "";
    inputProps.onChange = onChange;
  } else {
    inputProps.defaultValue = defaultValue;
  }

  return (
    <div className="form-group">
      <label htmlFor={id}>{label}</label>
      <input {...inputProps} />
    </div>
  );
}

export default FormInput;
