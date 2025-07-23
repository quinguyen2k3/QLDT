import React, { useEffect, useState } from 'react';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate } from 'react-router-dom';
import { majorApi } from '@/service/apis';
import { toast } from 'react-toastify';

function MajorList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang T
    const handleAddClick = () => {
        navigate('/major/create');
    };

    const [loading, setLoading] = useState(true);
    const [majors, setMajors] = useState([]);

    useEffect(() => {
        const fetchFormats = async () => {
            try {
                const response = await majorApi.getAll();

                const formattedData = response.data.data.map((item) => ({
                    ...item,
                    createdDate: item.createdDate ? new Date(item.createdDate).toLocaleDateString('vi-VN') : '',
                }));
                setMajors(formattedData);
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
        name: 'Chuyên Ngành Đào Tạo',
        note: 'Ghi Chú',
        createdDate: 'Ngày Tạo',
    };

    const columnHidden = ['isActive'];

    return (
        <section className="content">
            <PageHeader title="Chuyên Ngành Đào Tạo" />
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
                title="Danh sách chuyên ngành"
                data={majors}
                columnMap={labelMap}
                columnHidden={columnHidden}
                updateLinkPrefix="/major/update"
            />
            <BackButton />
        </section>
    );
}

export default MajorList;