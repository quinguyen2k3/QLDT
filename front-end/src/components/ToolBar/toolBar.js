import React from "react";

function ToolBar({ title, buttons = [] }) {
  return (
    <div className="d-flex align-items-start mb-3">
      {/* Icon bên trái */}
      <div className="mr-2 text-center">
        <div className="btn btn-warning rounded-circle shadow-sm">
          <i className="fas fa-tools text-white"></i>
        </div>
        <div className="border-left mx-auto" style={{ height: "40px" }}></div>
      </div>

      {/* Nội dung chính */}
      <div className="card flex-grow-1">
        <div className="card-header bg-success py-2">
          <h3 className="card-title text-white m-0">{title}</h3>
        </div>
        <div className="card-body pt-3 pb-2">
          {buttons.map((btn, index) => (
            <button
              key={index}
              className={`btn ${btn.className} mr-2`}
              onClick={btn.onClick}
            >
              {btn.label}
            </button>
          ))}
        </div>
      </div>
    </div>
  );
}

export default ToolBar;
