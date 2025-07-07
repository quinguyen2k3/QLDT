import React from 'react';

import { Link } from 'react-router-dom';
import { FaArrowRight, FaUser, FaDesktop, FaIdCard, FaPaperPlane } from 'react-icons/fa';

function DashboardCard({ title, subtitle, icon: Icon, bgColor, link }) {
    return (
        <div className="col-lg-3 col-md-6 col-12 mb-3">
            <Link to={link} className="text-white text-decoration-none">
                <div className={`small-box ${bgColor} text-white`}>
                    <div className="inner">
                        <h5 className="font-weight-bold mb-1">{title}</h5>
                        <p>{subtitle}</p>
                    </div>
                    <div className="icon">
                        <Icon size={50} />
                    </div>
                    <div className="small-box-footer d-flex justify-content-between align-items-center px-2">
                        <span>Thông Tin Chi Tiết</span>
                        <FaArrowRight />
                    </div>
                </div>
            </Link>
        </div>
    );
}

const dashboardCards = [
    {
        title: 'QL Nhân Viên',
        subtitle: 'Thông Tin Nhân Viên',
        icon: FaUser,
        bgColor: 'bg-info',
        link: '/employees/list',
    },
    {
        title: 'QL Khoá Học',
        subtitle: 'Thông Tin Khoá Học',
        icon: FaDesktop,
        bgColor: 'bg-primary',
        link: '/courses/list',
    },
    {
        title: 'Nội Bộ',
        subtitle: 'Thông Tin Đào Tạo Nội Bộ',
        icon: FaIdCard,
        bgColor: 'bg-success',
        link: '/employees/list',
    },
    {
        title: 'Nâng Cao',
        subtitle: 'Thông Tin Đào Tạo Nâng Cao',
        icon: FaPaperPlane,
        bgColor: 'bg-success',
        link: '/employees/list',
    },
];

export { DashboardCard, dashboardCards };
