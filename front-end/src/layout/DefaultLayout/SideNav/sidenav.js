import { useState, useEffect } from 'react';
import { FaUser, FaBuilding, FaKey, FaThList } from 'react-icons/fa';
import { Link, useLocation } from 'react-router-dom';
import { useAuth } from '@/contexts';

function SideNav() {
    const [openMenu, setOpenMenu] = useState({
        khoaPhong: false,
        danhMuc: false,
    });
    const location = useLocation();
    const { pathname } = location;
    const { user, authenticated } = useAuth();

    useEffect(() => {
        setOpenMenu({
            khoaPhong: false,
            danhMuc: false,
        });
    }, [pathname]);

    const toggleMenu = (e, menu) => {
        e.preventDefault();
        setOpenMenu((prev) => ({
            khoaPhong: menu === 'khoaPhong' ? !prev.khoaPhong : false,
            danhMuc: menu === 'danhMuc' ? !prev.danhMuc : false,
        }));
    };

    const isPartsActive = pathname.includes('part') && !pathname.includes('department');
    const isDepartmentsActive = pathname.includes('department');
    const isKhoaPhongActive = isPartsActive || isDepartmentsActive;
    const isDanhMucActive =
        pathname.includes('eunit') ||
        pathname.includes('format') ||
        pathname.includes('elevel') ||
        pathname.includes('major') ||
        pathname.includes('hour');
    const isUserManageActive =
        pathname === '/users/list' || pathname === '/user/create' || pathname.startsWith('/user/update');

    const showKhoaPhongMenu =
        user?.permissions.includes('Part.Manage') || user?.permissions.includes('Department.Manage');
    const showParts = user?.permissions.includes('Part.Manage');
    const showDepartments = user?.permissions.includes('Department.Manage') || showKhoaPhongMenu;
    const showDanhMucMenu =
        user?.permissions.includes('Report.ViewSummaryList') ||
        user?.permissions.includes('EducationLevel.Manage') ||
        user?.permissions.includes('Major.Manage') ||
        user?.permissions.includes('CreditHourse.Manage');
    const showEUnits = user?.permissions.includes('Report.ViewSummaryList');
    const showFormats = user?.permissions.includes('Report.ViewSummaryList');
    const showELevels = user?.permissions.includes('EducationLevel.Manage');
    const showMajors = user?.permissions.includes('Major.Manage');
    const showCreditHours = user?.permissions.includes('CreditHourse.Manage');

    return (
        <aside className="main-sidebar sidebar-dark-primary elevation-4">
            <Link to="/home" className="brand-link">
                <img
                    src="/dist/img/logoLeVanThinhcircle.png"
                    alt="Hospital Logo"
                    className="brand-image img-circle elevation-3"
                    style={{ opacity: '.8' }}
                />
                <span className="brand-text fw-bold fs-4">QL Đào Tạo</span>
            </Link>
            <div className="sidebar">
                <div className="user-panel mt-3 pb-3 mb-3 d-flex">
                    <div className="image">
                        <img src="/dist/img/avatar6.png" className="img-circle elevation-2" alt="User" />
                    </div>
                    <div className="info">
                        <span className="d-block text-white">{user?.name || 'Chưa đăng nhập'}</span>
                    </div>
                </div>
                <nav className="mt-2">
                    {authenticated && (
                        <ul className="nav nav-pills nav-sidebar flex-column" role="menu" data-accordion="false">
                            <li className="nav-item">
                                <Link
                                    to="/change-password"
                                    className={`nav-link ${pathname === '/change-password' ? 'active' : ''}`}
                                >
                                    <FaKey className="nav-icon" />
                                    <p>Đổi Mật Khẩu</p>
                                </Link>
                            </li>
                            {user?.permissions.includes('User.ManageAccounts') && (
                                <li className="nav-item">
                                    <Link to="/users/list" className={`nav-link ${isUserManageActive ? 'active' : ''}`}>
                                        <FaUser className="nav-icon" />
                                        <p>QL Tài Khoản</p>
                                    </Link>
                                </li>
                            )}
                            {showKhoaPhongMenu && (
                                <li
                                    className={`nav-item has-treeview ${
                                        openMenu.khoaPhong || isKhoaPhongActive ? 'menu-open' : ''
                                    }`}
                                >
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
                                    <ul
                                        className={`nav nav-treeview ${
                                            openMenu.khoaPhong || isKhoaPhongActive ? 'd-block' : 'd-none'
                                        }`}
                                    >
                                        {showParts && (
                                            <li className="nav-item">
                                                <Link
                                                    to="/parts/list"
                                                    className={`nav-link ${isPartsActive ? 'active' : ''}`}
                                                >
                                                    <i className="far fa-circle nav-icon"></i>
                                                    <p>Quản Lý Bộ Phận</p>
                                                </Link>
                                            </li>
                                        )}
                                        {showDepartments && (
                                            <li className="nav-item">
                                                <Link
                                                    to="/departments/list"
                                                    className={`nav-link ${isDepartmentsActive ? 'active' : ''}`}
                                                >
                                                    <i className="far fa-circle nav-icon"></i>
                                                    <p>Quản Lý Khoa Phòng</p>
                                                </Link>
                                            </li>
                                        )}
                                    </ul>
                                </li>
                            )}
                            {showDanhMucMenu && (
                                <li
                                    className={`nav-item has-treeview ${
                                        openMenu.danhMuc || isDanhMucActive ? 'menu-open' : ''
                                    }`}
                                >
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
                                    <ul
                                        className={`nav nav-treeview ${
                                            openMenu.danhMuc || isDanhMucActive ? 'd-block' : 'd-none'
                                        }`}
                                    >
                                        {showEUnits && (
                                            <li className="nav-item">
                                                <Link
                                                    to="/eunits/list"
                                                    className={`nav-link ${pathname.includes('eunit') ? 'active' : ''}`}
                                                >
                                                    <i className="far fa-circle nav-icon"></i>
                                                    <p>Đơn Vị Đào Tạo</p>
                                                </Link>
                                            </li>
                                        )}
                                        {showFormats && (
                                            <li className="nav-item">
                                                <Link
                                                    to="/formats/list"
                                                    className={`nav-link ${
                                                        pathname.includes('format') ? 'active' : ''
                                                    }`}
                                                >
                                                    <i className="far fa-circle nav-icon"></i>
                                                    <p>Hình Thức Đào Tạo</p>
                                                </Link>
                                            </li>
                                        )}
                                        {showELevels && (
                                            <li className="nav-item">
                                                <Link
                                                    to="/elevels/list"
                                                    className={`nav-link ${
                                                        pathname.includes('elevel') ? 'active' : ''
                                                    }`}
                                                >
                                                    <i className="far fa-circle nav-icon"></i>
                                                    <p>Trình Độ Đào Tạo</p>
                                                </Link>
                                            </li>
                                        )}
                                        {showMajors && (
                                            <li className="nav-item">
                                                <Link
                                                    to="/majors/list"
                                                    className={`nav-link ${pathname.includes('major') ? 'active' : ''}`}
                                                >
                                                    <i className="far fa-circle nav-icon"></i>
                                                    <p>Chuyên Ngành Đào Tạo</p>
                                                </Link>
                                            </li>
                                        )}
                                        {showCreditHours && (
                                            <li className="nav-item">
                                                <Link
                                                    to="/hours/list"
                                                    className={`nav-link ${pathname.includes('hour') ? 'active' : ''}`}
                                                >
                                                    <i className="far fa-circle nav-icon"></i>
                                                    <p>Số Giờ Tín Chỉ</p>
                                                </Link>
                                            </li>
                                        )}
                                    </ul>
                                </li>
                            )}
                        </ul>
                    )}
                </nav>
            </div>
        </aside>
    );
}

export default SideNav;
