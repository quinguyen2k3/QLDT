import React, { useState, useEffect } from 'react';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate, useLocation } from 'react-router-dom';
import { partApi } from '@/service/apis';
import { toast } from 'react-toastify';


function PartList() {
    //Khởi tạo đối tượng chuyển
    const navigate = useNavigate();
    const location = useLocation();

    const isAll = location.pathname.includes('all');

    //Chuyển hướng sang trang Danh sách bộ phận
    const handleListClick = () => {
        const targetPath = '/parts/list/all';
        if (location.pathname !== targetPath) {
            navigate(targetPath);
        }
    };

    //Chuyển hướng sang trang Tạo bộ phận
    const handleAddClick = () => {
        navigate('/part/create');
    };

    const [loading, setLoading] = useState(true);
    const [parts, setParts] = useState([]);
    
    useEffect(() => {
        const fetchFormats = async () => {
            try {
                let response

                if(isAll){
                    response = await partApi.getAll();
                }else{
                    response = await partApi.getAllByMe();
                }

                const partData = response.data.data.map((item) => ({
                    ...item,
                    createdDate: item.createdDate ? new Date(item.createdDate).toLocaleDateString('vi-VN') : '',
                }));
                setParts(partData);
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

    //Map label từ api sang tên khácnpm
    const labelMap = {
        name: 'Tên Bộ Phận',
        note: 'Ghi Chú',
        createdDate: 'Ngày Tạo',
    };
    
    const columnHidden = ['isActive']

    return (
        <section className="content">
            <PageHeader title="Danh Sách Bộ Phận" />
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
                title="Danh sách bộ phận"
                data={parts}
                columnMap={labelMap}
                columnHidden={columnHidden}
                updateLinkPrefix="/part/update"
            />
            <BackButton />
        </section>
    );
}

export default PartList;
