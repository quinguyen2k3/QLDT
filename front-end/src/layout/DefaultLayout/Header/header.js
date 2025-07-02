import React from "react";
import { Link } from "react-router-dom";

function Header() {
  return (
    <nav className="main-header navbar navbar-expand navbar-success navbar-dark">
      {/* Left navbar links */}
      <ul className="navbar-nav">
        <li className="nav-item">
          <a className="nav-link" data-widget="pushmenu" href="#" role="button">
            <i className="fas fa-bars" />
          </a>
        </li>
        <li className="nav-item d-none d-sm-inline-block">
          <Link to="/" className="nav-link">
            Trang chủ
          </Link>
        </li>
      </ul>
      {/* Right navbar links */}
      <ul className="navbar-nav ml-auto">
        <li className="nav-item">
          <a
            className="nav-link"
            data-widget="fullscreen"
            href="#"
            role="button"
          >
            <i className="fas fa-expand-arrows-alt" />
          </a>
        </li>
        <li className="nav-item">
          <a
            className="nav-link"
            href="/logout"
            role="button"
          >
            <i className="fas fa-sign-out-alt" />
            <span className="ml-1">Đăng Xuất</span>
          </a>
        </li>
      </ul>
    </nav>
  );
}

export default Header;
