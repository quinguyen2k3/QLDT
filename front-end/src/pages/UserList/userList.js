import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate } from 'react-router-dom';
import {useEffect, useState} from "react";
import {userApi} from "@/service/apis";
import {toast} from "react-toastify";

function UserList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang Danh sách người dùng
    const handleListClick = () => {
        navigate('/users/list');
    };

    //Chuyển hướng sang trang Tạo người dùng
    const handleAddClick = () => {
        navigate('/user/create');
    };

    const [loading, setLoading] = useState(true);
    const [users, setUsers] = useState([]);

    useEffect(() => {
        const fetchFormats = async () => {
            try {
                const response = await userApi.getAll();

                const userData = response.data.data.map((item) => ({
                    ...item,
                    createdDate: item.createdDate
                        ? new Date(item.createdDate).toLocaleDateString('vi-VN')
                        : '',
                    isActive: item.isActive ? 'Hoạt Động' : 'Vô Hiệu',
                }));
                setUsers(userData);
            } catch (error) {
                if (error.response?.status !== 403) {
                    console.error('Lỗi tải dữ liệu:', error);
                    toast.error('Lỗi tải dữ liệu');
                }
            } finally {
                setLoading(false);
            }
        };
        fetchFormats();
    }, []);

    //Map label từ api sang tên khác
    const labelMap = {
        name: 'Họ Tên',
        username: 'Tài Khoản',
        email: 'Thư Điện Tử',
        phone: 'Số Điện Thoại',
        isActive: 'Trạng Thái',
        roleName: 'Vai Trò',
        createdDate: 'Ngày Tạo',
    };

    const columnHidden = ['depId','roleId','password']

    return (
        <section className="content">
            <PageHeader title="Danh Sách Tài Khoản Người Dùng" />
            <ToolBar
                title="Thanh Công Cụ - Chức Năng Hệ Thống"
                buttons={[
                    {
                        label: 'Danh Sách Tổng Hợp',
                        className: 'btn-info',
                        onClick: handleListClick,
                    },
                    {
                        label: 'Thêm Mới',
                        className: 'btn-success',
                        onClick: handleAddClick,
                    },
                ]}
            />
            <DataTable title="Danh sách người dùng" 
            data={users}
            columnMap={labelMap}
            columnHidden={columnHidden}
            updateLinkPrefix="/user/update" />
            <BackButton />
        </section>
    );
}

export default UserList;
