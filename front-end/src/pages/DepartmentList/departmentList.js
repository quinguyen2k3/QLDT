import React, { useState, useEffect } from 'react';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate } from 'react-router-dom';
import { departmentApi } from '@/service/apis';
import { toast } from 'react-toastify';

function DepartmentList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang Danh sách tổng hợp khoa phòng
    const handleListClick = () => {
        navigate('/departments/list');
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
                const response = await departmentApi.getAll();

                const departmentData = response.data.data.map((item) => ({
                    ...item,
                    createdDate: item.createdDate ? new Date(item.createdDate).toLocaleDateString('vi-VN') : '',
                }));
                setDepartments(departmentData);
            } catch (error) {
                toast.error('Lỗi tải dữ liệu');
                console.error('Error fetching formats:', error);
            } finally {
                setLoading(false);
            }
        };
        fetchFormats();
    }, []);

    //Map label từ api sang tên khác
    const labelMap = {
        partName: 'Bộ Phận',
        name: 'Tên Khoa Phòng',
        note: 'Ghi Chú',
        createdDate: 'Ngày Tạo',
    };

    const columnHidden = ['partId']

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
