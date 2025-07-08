import { useState } from 'react';
import { FaUser, FaBuilding, FaKey, FaThList } from 'react-icons/fa';
import { Link, useLocation } from 'react-router-dom';

function SideNav() {
    const [openMenu, setOpenMenu] = useState({
        khoaPhong: false,
        danhMuc: false,
    });

    const location = useLocation();
    const { pathname } = location;

    const toggleMenu = (e, menu) => {
        e.preventDefault();
        setOpenMenu((prev) => ({
            ...prev,
            [menu]: !prev[menu],
        }));
    };

    // Xác định active cho menu cha
    const isKhoaPhongActive = pathname.startsWith('/devisions') || pathname.startsWith('/deparments');
    const isDanhMucActive = pathname.startsWith('/eunits') || pathname.startsWith('/training-types');

    return (
        <aside className="main-sidebar sidebar-dark-primary elevation-4">
            {/* Brand Logo */}
            <Link to="/home" className="brand-link">
                <img
                    src="/dist/img/logoLeVanThinhcircle.png"
                    alt="Hospital Logo"
                    className="brand-image img-circle elevation-3"
                    style={{ opacity: '.8' }}
                />
                <span className="brand-text fw-bold fs-4">QL Đào Tạo</span>
            </Link>

            {/* Sidebar */}
            <div className="sidebar">
                {/* Sidebar user panel */}
                <div className="user-panel mt-3 pb-3 mb-3 d-flex">
                    <div className="image">
                        <img src="/dist/img/avatar6.png" className="img-circle elevation-2" alt="User" />
                    </div>
                    <div className="info">
                        <span className="d-block text-white">Administrator</span>
                    </div>
                </div>

                {/* Sidebar Menu */}
                <nav className="mt-2">
                    <ul
                        className="nav nav-pills nav-sidebar flex-column"
                        role="menu"
                        data-accordion="false"
                    >
                        {/* Đổi mật khẩu */}
                        <li className="nav-item">
                            <Link to="/change-password" className={`nav-link ${pathname === '/change-password' ? 'active' : ''}`}>
                                <FaKey className="nav-icon" />
                                <p>Đổi Mật Khẩu</p>
                            </Link>
                        </li>

                        {/* QL Tài Khoản */}
                        <li className="nav-item">
                            <Link to="/users/list" className={`nav-link ${pathname === '/users/list' ? 'active' : ''}`}>
                                <FaUser className="nav-icon" />
                                <p>QL Tài Khoản</p>
                            </Link>
                        </li>

                        {/* QL Khoa Phòng */}
                        <li className={`nav-item has-treeview ${openMenu.khoaPhong || isKhoaPhongActive ? 'menu-open' : ''}`}>
                            <a
                                href="#"
                                className={`nav-link ${isKhoaPhongActive ? 'active' : ''}`}
                                onClick={(e) => toggleMenu(e, 'khoaPhong')}
                            >
                                <FaBuilding className="nav-icon" />
                                <p>
                                    QL Khoa Phòng
                                    <i className="right fas fa-angle-left"></i>
                                </p>
                            </a>
                            <ul className={`nav nav-treeview ${openMenu.khoaPhong || isKhoaPhongActive ? 'd-block' : 'd-none'}`}>
                                <li className="nav-item">
                                    <Link
                                        to="/devisions/list"
                                        className={`nav-link ${pathname === '/devisions/list' ? 'active' : ''}`}
                                    >
                                        <i className="far fa-circle nav-icon"></i>
                                        <p>Quản Lý Bộ Phận</p>
                                    </Link>
                                </li>
                                <li className="nav-item">
                                    <Link
                                        to="/deparments/list"
                                        className={`nav-link ${pathname === '/deparments/list' ? 'active' : ''}`}
                                    >
                                        <i className="far fa-circle nav-icon"></i>
                                        <p>Quản Lý Khoa Phòng</p>
                                    </Link>
                                </li>
                            </ul>
                        </li>

                        {/* QL Danh Mục */}
                        <li className={`nav-item has-treeview ${openMenu.danhMuc || isDanhMucActive ? 'menu-open' : ''}`}>
                            <a
                                href="#"
                                className={`nav-link ${isDanhMucActive ? 'active' : ''}`}
                                onClick={(e) => toggleMenu(e, 'danhMuc')}
                            >
                                <FaThList className="nav-icon" />
                                <p>
                                    QL Danh Mục
                                    <i className="right fas fa-angle-left"></i>
                                </p>
                            </a>
                            <ul className={`nav nav-treeview ${openMenu.danhMuc || isDanhMucActive ? 'd-block' : 'd-none'}`}>
                                <li className="nav-item">
                                    <Link
                                        to="/eunits/list"
                                        className={`nav-link ${pathname === '/eunits/list' ? 'active' : ''}`}
                                    >
                                        <i className="far fa-circle nav-icon"></i>
                                        <p>Đơn Vị Đào Tạo</p>
                                    </Link>
                                </li>
                                <li className="nav-item">
                                    <Link
                                        to="/training-types/list"
                                        className={`nav-link ${pathname === '/training-types/list' ? 'active' : ''}`}
                                    >
                                        <i className="far fa-circle nav-icon"></i>
                                        <p>Hình Thức Đào Tạo</p>
                                    </Link>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </nav>
            </div>
        </aside>
    );
}

export default SideNav;
