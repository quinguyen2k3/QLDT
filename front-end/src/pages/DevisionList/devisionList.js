import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton'
import { useNavigate } from 'react-router-dom';

function DevisionList() {
    //Dữ liệu giả lập
    const dataFromApi = [
        {
            id: 1,
            name: 'Khối Cận Lâm Sàng',
            note: '',
            createdAt: '01/02/2024',
        },
        {
            id: 2,
            name: 'Khối Nội',
            note: '',
            createdAt: '01/02/2024',
        },
        {
            id: 3,
            name: 'Khối Ngoại',
            note: '',
            createdAt: '01/02/2024',
        },
        {
            id: 4,
            name: 'Khối Hành Chính',
            note: 'Hành Chính',
            createdAt: '01/02/2024',
        },
    ];

    //Map label từ api sang tên khác
    const labelMap = {
        name: 'Tên bộ phận',
        note: 'Ghi chú',
        createdAt: 'Ngày Tạo',
    };

    //Khởi tạo đối tượng chuyển
    const navigate = useNavigate();

    //Chuyển hướng sang trang Danh sách bộ phận
    const handleListClick = () => {
        navigate('/devisions/list');
    };

    //Chuyển hướng sang trang Tạo bộ phận
    const handleAddClick = () => {
        navigate('/users/create');
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
            <DataTable title="Danh sách bộ phận" data={dataFromApi} columnMap={labelMap} />
            <BackButton />
        </section>
    );
}

export default DevisionList;
