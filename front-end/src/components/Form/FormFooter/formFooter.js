import React from "react";

function FormFooter({ isEdit = false}) {
  return (
    <div className="card-footer">
      <button type="submit" className="btn btn-success">
        {isEdit ? "Lưu" : "Thêm mới"}
      </button>
    </div>
  );
}

export default FormFooter;