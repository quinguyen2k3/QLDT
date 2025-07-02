import React from "react";

function FormFooter({ isEdit = false, onSubmit = () => {} }) {
  return (
    <div className="card-footer">
      <button type="submit" className="btn btn-success" onClick={onSubmit}>
        {isEdit ? "Lưu" : "Thêm mới"}
      </button>
    </div>
  );
}

export default FormFooter;