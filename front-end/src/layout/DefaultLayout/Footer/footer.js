import React from "react";

function Footer() {
  return (
    <div>
      <footer className="main-footer">
        <strong>
          © 2025 <a href="https://benhvienlevanthinh.vn">Bệnh viện Lê Văn Thịnh</a>.
        </strong>{" "}
        Mọi quyền được bảo lưu.
        <div className="float-right d-none d-sm-inline-block">
          <b>Phiên bản</b> 1.0.0
        </div>
      </footer>
    </div>
  );
}

export default Footer;
