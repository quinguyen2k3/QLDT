import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton'
import { useNavigate } from 'react-router-dom';

function EUnitList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang T
    const handleAddClick = () => {
        navigate('/eunit/create');
    };

    //Dữ liệu giả lập
    const dataFromApi = [
        {
            id: 1,
            training_unit: 'Bệnh viện Lê Văn Thịnh',
            note: '',
            created_at: '07/02/2024',
        },
        {
            id: 2,
            training_unit: 'Sở Nội Vụ',
            note: '',
            created_at: '07/02/2024',
        },
        {
            id: 3,
            training_unit: 'Sở Y Tế',
            note: '',
            created_at: '07/02/2024',
        },
    ];

    //Map label từ api sang tên khác
    const labelMap = {
        training_unit: 'Đơn Vị Đào Tạo',
        note: 'Ghi Chú',
        created_at: 'Ngày Tạo',
    };

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
            <DataTable title="Danh sách đơn vị đào tạo" data={dataFromApi} columnMap={labelMap} updateLinkPrefix = "/eunit/update"/>
            <BackButton />
        </section>
    );
}

export default EUnitList;
