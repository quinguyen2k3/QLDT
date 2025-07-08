import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import {getAccessToken, getRefreshToken,  clearTokens} from '@/service/authService';
import { authApi } from '@/service/apis';

function Header() {
    const navigate = useNavigate();

    const handleLogout = async (e) => {
        e.preventDefault();

        try {
            const accessToken = getAccessToken()
            const refreshToken = getRefreshToken()

            if (accessToken && refreshToken) {
                await authApi.logout({
                    accessToken,
                    refreshToken,
                });
            }

            clearTokens()
            document.body.className = "";
            navigate("/login");
        } catch (error) {
            console.error('Logout failed', error);
                 
            clearTokens()
            document.body.className = "";
            navigate("/login");
        }
    };

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
                    <Link to="/home" className="nav-link">
                        Trang chủ
                    </Link>
                </li>
            </ul>
            {/* Right navbar links */}
            <ul className="navbar-nav ml-auto">
                <li className="nav-item">
                    <a className="nav-link" data-widget="fullscreen" href="#" role="button">
                        <i className="fas fa-expand-arrows-alt" />
                    </a>
                </li>
                <li className="nav-item">
                    <button
                        type="button"
                        className="nav-link btn btn-link text-white"
                        onClick={handleLogout}
                        style={{ textDecoration: 'none' }} 
                    >
                        <i className="fas fa-sign-out-alt" />
                        <span className="ml-1">Đăng Xuất</span>
                    </button>
                </li>
            </ul>
        </nav>
    );
}

export default Header;
