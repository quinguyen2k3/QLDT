import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate } from 'react-router-dom';

function UserList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang Danh sách người dùng
    const handleListClick = () => {
        navigate('/users/list');
    };

    //Chuyển hướng sang trang Tạo người dùng
    const handleAddClick = () => {
        navigate('/users/create');
    };

    //Dữ liệu giả lập
    const dataFromApi = [
        {
            id: 1,
            name: 'Nguyễn Văn A',
            username: 'nguyenvana',
            email: 'a.nguyen@example.com',
            phone: '0912345678',
            address: 'Hà Nội',
            gender: 'Nam',
            age: 25,
            role: 'Admin',
            status: 'Active',
            created_at: '2024-05-12',
        },
        {
            id: 2,
            name: 'Trần Thị B',
            username: 'tranthib',
            email: 'b.tran@example.com',
            phone: '0987654321',
            address: 'TP.HCM',
            gender: 'Nữ',
            age: 30,
            role: 'User',
            status: 'Inactive',
            created_at: '2024-05-15',
        },
        {
            id: 3,
            name: 'Lê Văn C',
            username: 'levanc',
            email: 'c.le@example.com',
            phone: '0909090909',
            address: 'Đà Nẵng',
            gender: 'Nam',
            age: 28,
            role: 'Editor',
            status: 'Active',
            created_at: '2024-05-20',
        },
        {
            id: 4,
            name: 'Phạm Thị D',
            username: 'phamthid',
            email: 'd.pham@example.com',
            phone: '0933221144',
            address: 'Cần Thơ',
            gender: 'Nữ',
            age: 32,
            role: 'Moderator',
            status: 'Active',
            created_at: '2024-05-25',
        },
        {
            id: 5,
            name: 'Đỗ Văn E',
            username: 'dovane',
            email: 'e.do@example.com',
            phone: '0977889900',
            address: 'Hải Phòng',
            gender: 'Nam',
            age: 35,
            role: 'Admin',
            status: 'Suspended',
            created_at: '2024-06-01',
        },
    ];
    
    //Map label từ api sang tên khác
    const labelMap = {
        name: 'Họ và Tên',
        username: 'Tên Đăng Nhập',
        email: 'Email',
        phone: 'Số Điện Thoại',
        address: 'Địa Chỉ',
        gender: 'Giới Tính',
        age: 'Tuổi',
        role: 'Vai Trò',
        status: 'Trạng Thái',
        created_at: 'Ngày Tạo',
    };

    return (
        <section className="content">
            <PageHeader title="Danh Sách Tài Khoản Người Dùng" />
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
            <DataTable title="Danh sách người dùng" 
            data={dataFromApi} 
            columnMap={labelMap}
            updateLinkPrefix="/user/update" />
            <BackButton />
        </section>
    );
}

export default UserList;
