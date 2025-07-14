import React, { useState, useEffect } from 'react';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate } from 'react-router-dom';
import { courseApi } from '@/service/apis';
import { toast } from 'react-toastify';

function CourseList() {

    //Khởi tạo đối tượng chuyển
    const navigate = useNavigate();

    //Chuyển hướng sang trang Danh sách bộ phận
    const handleListClick = () => {
        navigate('/courses/list');
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
                const response = await courseApi.getAll();

                const courseData = response.data.data.map((item) => ({
                    ...item,
                    createdDate: item.createdDate ? new Date(item.createdDate).toLocaleDateString('vi-VN') : '',
                    courseNgayKg: item.courseNgayKg ? new Date(item.courseNgayKg).toLocaleDateString('vi-VN') : '',
                }));
                setCourses(courseData);
            } catch (error) {
                toast.error('Lỗi tải dữ liệu');
                console.error('Error fetching formats:', error);
            } finally {
                setLoading(false);
            }
        };
        fetchFormats();
    }, []);

    const labelMap = {
        name: 'Tên Khóa Học',
        note: 'Ghi Chú',
        content: 'Nội Dung Khóa Học',
        createdDate: 'Ngày Tạo',
        courseNgayKg: 'Ngày Khai Giảng'
    };

    const columnHidden = ['attachments', 'depId']

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
            <DataTable title="Danh sách nhân sự" 
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
