import React, { useState, useEffect } from 'react';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate, useLocation } from 'react-router-dom';
import { courseApi } from '@/service/apis';
import { toast } from 'react-toastify';

function CourseList() {
    //Khởi tạo đối tượng chuyển
    const navigate = useNavigate();
    const location = useLocation();

    const isAll = location.pathname.includes('all');

    //Chuyển hướng sang trang Danh sách bộ phận
     const handleListClick = () => {
        const targetPath = '/courses/list/all';
        if (location.pathname !== targetPath) {
            navigate(targetPath);
        }
    };

    //Chuyển hướng sang trang Tạo bộ phận
    const handleAddClick = () => {
        navigate('/course/create');
    };

    const [loading, setLoading] = useState(true);
    const [courses, setCourses] = useState([]);

    useEffect(() => {
        const fetchFormats = async () => {
            try {
                let response
                if(isAll){
                    response = await courseApi.getAll();
                }else{
                    response = await courseApi.getAllByMe();
                }
                const courseData = response.data.data.map((item) => ({
                    ...item,
                    createdDate: item.createdDate ? new Date(item.createdDate).toLocaleDateString('vi-VN') : '',
                    courseNgayKg: item.courseNgayKg ? new Date(item.courseNgayKg).toLocaleDateString('vi-VN') : '',
                }));
                setCourses(courseData);
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

    const labelMap = {
        name: 'Tên Khóa Học',
        note: 'Ghi Chú',
        content: 'Nội Dung Khóa Học',
        createdDate: 'Ngày Tạo',
        courseNgayKg: 'Ngày Khai Giảng',
    };

    const columnHidden = ['attachments', 'depId', 'isActive'];

    return (
        <section className="content">
            <PageHeader title="Danh Sách Khóa Học" />
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
                title="Danh sách nhân sự"
                data={courses}
                columnMap={labelMap}
                columnHidden={columnHidden}
                updateLinkPrefix="/course/update"
            />
            <BackButton />
        </section>
    );
}

export default CourseList;