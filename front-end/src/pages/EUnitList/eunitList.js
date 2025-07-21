import React, { useEffect, useState } from 'react';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate } from 'react-router-dom';
import { unitApi } from '@/service/apis';
import { toast } from 'react-toastify';

function EUnitList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang T
    const handleAddClick = () => {
        navigate('/eunit/create');
    };

    const [loading, setLoading] = useState(true);
    const [units, setUnits] = useState([]);

    useEffect(() => {
        const fetchFormats = async () => {
            try {
                const response = await unitApi.getAll();

                const unitsData = response.data.data.map((item) => ({
                    ...item,
                    createdDate: item.createdDate ? new Date(item.createdDate).toLocaleDateString('vi-VN') : '',
                }));
                setUnits(unitsData);
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
        name: 'Đơn Vị Đào Tạo',
        note: 'Ghi Chú',
        createdDate: 'Ngày Tạo',
    };

    const columnHidden = ['isActive']

    return (
        <section className="content">
            <PageHeader title="Danh Sách Đơn Vị Đào Tạo" />
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
                title="Danh sách đơn vị đào tạo"
                data={units}
                columnMap={labelMap}
                columnHidden={columnHidden}
                updateLinkPrefix="/eunit/update"
            />
            <BackButton />
        </section>
    );
}

export default EUnitList;
