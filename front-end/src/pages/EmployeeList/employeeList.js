import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate } from 'react-router-dom';

function EmployeeList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang Trang danh sách nhân sự
    const handleListClick = () => {
        navigate('/employees/list');
    };

    //Chuyển hướng sang trang Tạo mới nhân sự
    const handleAddClick = () => {
        navigate('/employee/create');
    };

    //Dữ liệu giả lập
    const dataFromApi = [
        {
            department: 'Khoa Nội Tổng Hợp',
            full_name: 'Nguyễn Văn A',
            ethnicity: 'Kinh',
            qualification: 'Đại học',
            gender: 'Nam',
            birth_date: '1990-05-12',
            contract_info: 'Biên chế',
            note: 'Trưởng khoa',
            party_join_date: '2012-06-01',
            status: true,
        },
        {
            department: 'Khoa Ngoại Chấn Thương',
            full_name: 'Trần Thị B',
            ethnicity: 'Kinh',
            qualification: 'Thạc sĩ',
            gender: 'Nữ',
            birth_date: '1988-09-20',
            contract_info: 'Hợp đồng 3 năm',
            note: '',
            party_join_date: '2015-04-20',
            status: false,
        },
    ];

    //Map label từ api sang tên khác
    const labelMap = {
        department: 'Khoa Phòng',
        full_name: 'Họ và Tên',
        ethnicity: 'Dân tộc',
        qualification: 'Trình Độ',
        birth_date: 'Ngày Sinh',
        contract_info: 'Thông Tin Hợp Đồng',
        note: 'Ghi Chú',
        party_join_date: 'Ngày Vào Đảng',
        status: 'Trạng Thái'
    };

    return (
        <section className="content">
            <PageHeader title="Danh Sách Nhân Sự" />
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
            detailLinkPrefix="/employee/detail"
            updateLinkPrefix="/employee/update"
            />
            <BackButton />
        </section>
    );
}

export default EmployeeList;
