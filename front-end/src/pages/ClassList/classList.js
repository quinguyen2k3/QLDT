import { useEffect, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { classApi } from '@/service/apis';
import { toast } from 'react-toastify';

function ClassList() {
    //Khởi tạo đối tượng chuyển
    const navigate = useNavigate();
    const location = useLocation();

    const isLong = location.pathname.includes('longterm');
    const isShort = location.pathname.includes('shortterm');

    //Chuyển hướng sang trang Danh sách bộ phận
    const handleListClick = () => {
        if (isLong) {
            navigate('/class/list/longterm');
        } else if (isShort) {
            navigate('/class/list/shortterm');
        }
    };

    //Chuyển hướng sang trang Tạo bộ phận
    const handleAddClick = () => {
        navigate('/class/create');
    };

    const [loading, setLoading] = useState(true);
    const [classes, setClasses] = useState([]);

    useEffect(() => {
        const fetchFormats = async () => {
            try {
                let response;

                if (isLong) {
                    response = await classApi.getAll(1);
                } else if (isShort) {
                    response = await classApi.getAll(2);
                }
                const classData = response.data.data.map((item) => ({
                    ...item,
                    classNgayQDML: item.classNgayQDML ? new Date(item.classNgayQDML).toLocaleDateString('vi-VN') : '',
                    classNgayKT: item.classNgayKT ? new Date(item.classNgayKT).toLocaleDateString('vi-VN') : '',
                    classNgayBD: item.classNgayBD ? new Date(item.classNgayBD).toLocaleDateString('vi-VN') : '',
                }));
                setClasses(classData);
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

    const labelMap = {
        name: 'Tên Lớp Học',
        classNgayBD: 'Ngày Bắt Đầu',
        classNgayKT: 'Ngày Kết Thúc',
        classSoTiet: 'Số Tiết',
        unitName: 'Đơn Vị ĐT',
        levelName: 'Trình Độ ĐT',
        content: 'Nội Dung',
        classKinhPhi: 'Kinh Phí',
        classSoQDML: 'Số QĐ Mở Lớp',
        classNgayQDML: 'Ngày QĐ Mở Lớp',
    };

    const columnHidden = [
        'classSoCVTS',
        'classNgayCVTS',
        'classSoQDDH',
        'classNgayQDDH',
        'formatId',
        'formatName',
        'levelId',
        'courseId',
        'unitId',
        'soTinhChi',
        'content',
        'attachments',
        'employeeIds',
        'isActive'
    ];

    return (
        <section className="content">
            <PageHeader title="Danh Sách Lớp Học" />
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
                title="Danh sách lớp học"
                data={classes}
                columnMap={labelMap}
                columnHidden={columnHidden}
                updateLinkPrefix="/class/update"
            />
            <BackButton />
        </section>
    );
}

export default ClassList;
