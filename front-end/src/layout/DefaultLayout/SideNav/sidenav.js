import React from "react";
import {
  FaUser,
  FaBuilding,
  FaKey,
  FaThList,
} from "react-icons/fa";
import { Link } from "react-router-dom";

function SideNav() {
  return (
    <aside className="main-sidebar sidebar-dark-primary elevation-4">
      {/* Brand Logo */}
      <Link to="/" className="brand-link">
        <img
          src="/dist/img/logoLeVanThinhcircle.png"
          alt="Hospital Logo"
          className="brand-image img-circle elevation-3"
          style={{ opacity: ".8" }}
        />
        <span className="brand-text fw-bold fs-4">QL Đào Tạo</span>
      </Link>

      {/* Sidebar */}
      <div className="sidebar">
        {/* Sidebar user panel */}
        <div className="user-panel mt-3 pb-3 mb-3 d-flex">
          <div className="image">
            <img
              src="/dist/img/avatar6.png"
              className="img-circle elevation-2"
              alt="User Image"
            />
          </div>
          <div className="info">
            <a href="#" className="d-block">
              Administrator
            </a>
          </div>
        </div>

        {/* Sidebar Menu */}
        <nav className="mt-2">
          <ul
            className="nav nav-pills nav-sidebar flex-column"
            data-widget="treeview"
            role="menu"
            data-accordion="false"
          >
            <li className="nav-item">
              <Link to="/change-password" className="nav-link">
                <FaKey className="nav-icon" />
                <p>Đổi Mật Khẩu</p>
              </Link>
            </li>

            <li className="nav-item has-treeview">
              <Link to="/users/list" className="nav-link">
                <FaUser className="nav-icon" />
                <p>
                  QL Tài Khoản
                  <i className="right fas fa-angle-left" />
                </p>
              </Link>
              {/* Sub-menu nếu có thể thêm vào đây */}
            </li>

            <li className="nav-item has-treeview">
              <a href="" className="nav-link">
                <FaBuilding className="nav-icon" />
                <p>
                  QL Khoa Phòng
                  <i className="right fas fa-angle-left" />
                </p>
              </a>
              <ul className="nav nav-treeview">
                <li className="nav-item">
                  <Link to="/devisions/list" className="nav-link">
                    <i className="far fa-circle nav-icon" />
                    <p>Quản Lý Bộ Phận</p>
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to="/deparments/list" className="nav-link">
                    <i className="far fa-circle nav-icon" />
                    <p>Quản Lý Khoa Phòng</p>
                  </Link>
                </li>
              </ul>
            </li>

            <li className="nav-item has-treeview">
              <a className="nav-link">
                <FaThList className="nav-icon" />
                <p>
                  QL Danh Mục
                  <i className="right fas fa-angle-left" />
                </p>
              </a>
              <ul className="nav nav-treeview">
                <li className="nav-item">
                  <Link to="/eunits/list" className="nav-link">
                    <i className="far fa-circle nav-icon" />
                    <p>Đơn Vị Đào Tạo</p>
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to="/training-types/list" className="nav-link">
                    <i className="far fa-circle nav-icon" />
                    <p>Hình Thức Đào Tạo</p>
                  </Link>
                </li>
              </ul>
            </li>
          </ul>
        </nav>
        {/* /.sidebar-menu */}
      </div>
      {/* /.sidebar */}
    </aside>
  );
}

export default SideNav;
