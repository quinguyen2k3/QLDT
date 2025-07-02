import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton'
import { useNavigate } from 'react-router-dom';

function DepartmentList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang Danh sách tổng hợp khoa phòng
    const handleListClick = () => {
        navigate('/users/list');
    };

    //Chuyển hướng sang trang Khoa
    const handleAddClick = () => {
        navigate('/users/create');
    };

    //Dữ liệu giả lập
    const dataFromApi = [
        {
            id: 1,
            department: 'Khối Hành Chính',
            room_name: 'Khoa Huyết học truyền máu',
            note: '',
            created_at: '01/02/2024',
        },
        {
            id: 2,
            department: 'Khối Hành Chính',
            room_name: 'Khoa Nội hô hấp',
            note: '',
            created_at: '01/02/2024',
        },
        {
            id: 3,
            department: 'Khối Hành Chính',
            room_name: 'Phòng khám đa khoa Thảo Điền',
            note: '',
            created_at: '01/02/2024',
        },
        {
            id: 4,
            department: 'Khối Hành Chính',
            room_name: 'Khoa Tiết Niệu',
            note: '',
            created_at: '01/02/2024',
        },
        {
            id: 5,
            department: 'Khối Hành Chính',
            room_name: 'Khoa Nội Soi',
            note: '',
            created_at: '01/02/2024',
        },
    ];

    //Map label từ api sang tên khác
    const labelMap = {
        department: 'Bộ Phận',
        room_name: 'Tên Khoa Phòng',
        note: 'Ghi Chú',
        created_at: 'Ngày Tạo',
    };

    return (
        <section className="content">
            <PageHeader title="Danh Sách Khoa Phòng" />
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
            <DataTable title="Danh sách người dùng" data={dataFromApi} columnMap={labelMap} />
            <BackButton />
        </section>
    );
}

export default DepartmentList;
