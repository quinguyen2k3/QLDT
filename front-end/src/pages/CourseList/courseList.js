import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate } from 'react-router-dom';

function CourseList() {
    //Dữ liệu giả lập
    const dataFromApi = [
        {
            id: 1,
            className: 'Tí Vi',
            department: 'Phòng Công Nghệ Thông Tin',
            educationUnit: 'Bệnh viện Lê Văn Thịnh',
            trainingType: 'Nội Bộ',
            trainingContent: 'Nội dung chi tiết',
            openingDate: '22/02/2024',
            note: 'Note',
            createdAt: '19/02/2024',
        },
        {
            id: 2,
            className: 'Lớp Máy Tính',
            department: 'Phòng Hành Chính Quản Trị',
            educationUnit: 'Bệnh viện Lê Văn Thịnh',
            trainingType: 'Nội Bộ',
            trainingContent: 'Nội dung chi tiết',
            openingDate: '21/02/2024',
            note: 'Note',
            createdAt: '19/02/2024',
        },
        {
            id: 3,
            className: 'Lớp Điện Thoại',
            department: 'Khoa Kiểm Soát Nhiễm Khuẩn',
            educationUnit: 'Bệnh viện Lê Văn Thịnh',
            trainingType: 'Nội Bộ',
            trainingContent: 'Nội dung chi tiết',
            openingDate: '23/02/2024',
            note: 'Note',
            createdAt: '19/02/2024',
        },
        {
            id: 4,
            className: 'Phòng cháy chữa cháy',
            department: 'Phòng Hành Chính Quản Trị',
            educationUnit: 'Bệnh viện Lê Văn Thịnh',
            trainingType: 'Nội Bộ',
            trainingContent: 'Nội dung chi tiết',
            openingDate: '06/03/2024',
            note: 'Note',
            createdAt: '01/02/2024',
        },
    ];

    //Map label từ api sang tên khác
    const labelMap = {
        className: 'Lớp Đào Tạo',
        department: 'Khoa Phòng',
        educationUnit: 'Đơn Vị Đào Tạo',
        trainingType: 'Hình Thức Đào Tạo',
        trainingContent: 'Nội Dung Đào Tạo',
        openingDate: 'Ngày Khai Giảng',
        note: 'Ghi Chú',
        createdAt: 'Ngày Tạo',
    };
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
            data={dataFromApi} 
            columnMap={labelMap}             
            updateLinkPrefix="/course/update"
            />
            <BackButton />
            <BackButton />
        </section>
    );
}

export default CourseList;
