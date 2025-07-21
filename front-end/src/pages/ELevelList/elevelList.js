import React, { useEffect, useState } from 'react';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate } from 'react-router-dom';
import { levelApi } from '@/service/apis';
import { toast } from 'react-toastify';

function ELevelList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang T
    const handleAddClick = () => {
        navigate('/elevel/create');
    };

    const [loading, setLoading] = useState(true);
    const [levels, setLevels] = useState([]);

    useEffect(() => {
        const fetchFormats = async () => {
            try {
                const response = await levelApi.getAll();

                const levels = response.data.data.map((item) => ({
                    ...item,
                    createdDate: item.createdDate ? new Date(item.createdDate).toLocaleDateString('vi-VN') : '',
                }));
                setLevels(levels);
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
        name: 'Trình Độ Đào Tạo',
        note: 'Ghi Chú',
        createdDate: 'Ngày Tạo',
    };

    const columnHidden = ['isActive'];

    return (
        <section className="content">
            <PageHeader title="Danh Sách Trình Độ Đào Tạo" />
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
                title="Danh sách trình độ đào tạo"
                data={levels}
                columnMap={labelMap}
                columnHidden={columnHidden}
                updateLinkPrefix="/elevel/update"
            />
            <BackButton />
        </section>
    );
}

export default ELevelList;
