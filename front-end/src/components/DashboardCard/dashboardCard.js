import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '@/contexts';
import { toast } from 'react-toastify';
import { FaArrowRight, FaUser, FaDesktop, FaIdCard, FaPaperPlane } from 'react-icons/fa';

function DashboardCard({ title, subtitle, icon: Icon, bgColor, link, requiredPermissions }) {
    const { user } = useAuth();

    const hasPermission = user?.permissions?.length > 0 && requiredPermissions?.length > 0
        ? requiredPermissions.every(perm => user.permissions.includes(perm))
        : false;

    const handleClick = () => {
        console.log(`Card clicked: ${title}`);
        if (!hasPermission) {
            toast.error('Bạn không có quyền truy cập trang này!', {
                preventDuplicate: true,
                autoClose: 2000,
            });
        }
    };

    const CardWrapper = hasPermission ? Link : 'div';

    return (
        <div className="col-lg-3 col-md-6 col-12 mb-3">
            <CardWrapper
                {...(hasPermission ? { to: link } : {})}
                className={`text-white text-decoration-none ${!hasPermission ? 'disabled-link' : ''}`}
                onClick={handleClick}
                style={{ cursor: hasPermission ? 'pointer' : 'not-allowed' }}
            >
                <div className={`small-box ${bgColor} text-white ${!hasPermission ? 'disabled' : ''}`}>
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
            </CardWrapper>
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
        requiredPermissions: ['Employee.Manage'],
    },
    {
        title: 'QL Khoá Học',
        subtitle: 'Thông Tin Khoá Học',
        icon: FaDesktop,
        bgColor: 'bg-success',
        link: '/courses/list',
        requiredPermissions: ['Course.Manage'],
    },
    {
        title: 'Lớp Học Dài Hạn',
        subtitle: 'Thông Tin Đào Tạo Dài Hạn',
        icon: FaIdCard,
        bgColor: 'bg-warning',
        link: '/class/list/longterm',
        requiredPermissions: ['Class.Manage'],
    },
    {
        title: 'Lớp Học Ngắn Hạn',
        subtitle: 'Thông Tin Đào Tạo Ngắn Hạn',
        icon: FaPaperPlane,
        bgColor: 'bg-danger',
        link: '/class/list/shortterm',
        requiredPermissions: ['Class.Manage'],
    },
];

export { DashboardCard, dashboardCards };
