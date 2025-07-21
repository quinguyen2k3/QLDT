import React, { useEffect, useState } from 'react';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate } from 'react-router-dom';
import { formatApi } from '@/service/apis';
import { toast } from 'react-toastify';

function TrainingTypeList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang T
    const handleAddClick = () => {
        navigate('/format/create');
    };

    const [loading, setLoading] = useState(true);
    const [formats, setFormats] = useState([]);

    useEffect(() => {
        const fetchFormats = async () => {
            try {
                const response = await formatApi.getAll();

                const formattedData = response.data.data.map((item) => ({
                    ...item,
                    createdDate: item.createdDate ? new Date(item.createdDate).toLocaleDateString('vi-VN') : '',
                }));
                setFormats(formattedData);
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
        name: 'Hình Thức Đào Tạo',
        note: 'Ghi Chú',
        createdDate: 'Ngày Tạo',
    };

    const columnHidden = ['isActive'];

    return (
        <section className="content">
            <PageHeader title="Hình Thức Đào Tạo" />
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
                title="Danh sách hình thức đào tạo"
                data={formats}
                columnMap={labelMap}
                columnHidden={columnHidden}
                updateLinkPrefix="/format/update"
            />
            <BackButton />
        </section>
    );
}

export default TrainingTypeList;
