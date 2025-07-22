import React, { useState, useEffect } from 'react';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate, useLocation } from 'react-router-dom';
import { departmentApi } from '@/service/apis';
import { toast } from 'react-toastify';

function DepartmentList() {
    const navigate = useNavigate();
    const location = useLocation();

    const isAll = location.pathname.includes('all');

    //Chuyển hướng sang trang Danh sách tổng hợp khoa phòng
    const handleListClick = () => {
        const targetPath = '/departments/list/all';
        if (location.pathname !== targetPath) {
            navigate(targetPath);
        }
    };

    //Chuyển hướng sang trang Khoa
    const handleAddClick = () => {
        navigate('/department/create');
    };

    const [loading, setLoading] = useState(true);
    const [departments, setDepartments] = useState([]);

    useEffect(() => {
        const fetchFormats = async () => {
            try {
                let response

                if(isAll){
                    response = await departmentApi.getAll();
                }else{
                    response = await departmentApi.getAllByMe();
                }

                const departmentData = response.data.data.map((item) => ({
                    ...item,
                    createdDate: item.createdDate ? new Date(item.createdDate).toLocaleDateString('vi-VN') : '',
                }));
                setDepartments(departmentData);
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
    }, [location.pathname]);

    //Map label từ api sang tên khác
    const labelMap = {
        partName: 'Bộ Phận',
        name: 'Tên Khoa Phòng',
        note: 'Ghi Chú',
        createdDate: 'Ngày Tạo',
    };

    const columnHidden = ['partId', 'isActive']

    return (
        <section className="content">
            <PageHeader title="Danh Sách Khoa Phòng" />
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
            <DataTable
                title="Danh sách khoa phòng"
                data={departments}
                columnHidden = {columnHidden}
                columnMap={labelMap}
                updateLinkPrefix="/department/update"
            />
            <BackButton />
        </section>
    );
}

export default DepartmentList;
