import React, { useEffect, useState } from 'react';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate } from 'react-router-dom';
import { hourApi } from '@/service/apis';
import { toast } from 'react-toastify';

function HourList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang T
    const handleAddClick = () => {
        navigate('/hour/create');
    };

    const [loading, setLoading] = useState(true);
    const [hours, setHours] = useState([]);

    useEffect(() => {
        const fetchFormats = async () => {
            try {
                const response = await hourApi.getAll();

                const formattedData = response.data.data.map((item) => ({
                    ...item,
                    createdDate: item.createdDate ? new Date(item.createdDate).toLocaleDateString('vi-VN') : '',
                }));
                setHours(formattedData);
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

    //Map label từ api sang tên khácnpm
    const labelMap = {
        hour: 'Số Giờ Tín Chỉ',
        note: 'Ghi Chú',
        createdDate: 'Ngày Tạo',
    };

    const columnHidden = ['isActive'];

    return (
        <section className="content">
            <PageHeader title="Số Giờ Tín Chỉ" />
            <ToolBar
                title="Thanh Công Cụ - Chức Năng Hệ Thống"
                buttons={[
                    {
                        label: 'Thêm Mới',
                        className: 'btn-success',
                        onClick: handleAddClick,
                    },
                ]}
            />
            <DataTable
                title="Danh sách số giờ tín chỉ"
                data={hours}
                columnMap={labelMap}
                columnHidden={columnHidden}
                updateLinkPrefix="/hour/update"
            />
            <BackButton />
        </section>
    );
}

export default HourList;