import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton'
import { useNavigate } from 'react-router-dom';

function TrainingTypeList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang T
    const handleAddClick = () => {
        navigate('/users/create');
    };

    //Dữ liệu giả lập
    const dataFromApi = [
        {
            training_type_name: 'Nâng Cao',
            note: '',
            created_date: '01/02/2024',
        },
        {
            training_type_name: 'Nội Bộ',
            note: '',
            created_date: '01/02/2024',
        },
    ];

    //Map label từ api sang tên khác
    const labelMap = {
        training_type_name: 'Hình Thức Đào Tạo',
        note: 'Ghi Chú',
        created_date: 'Ngày Tạo',
    };

    return (
        <section className = "content">
            <PageHeader title="Hình Thức Đào Tạo" />
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
            <DataTable title="Danh sách hình thức đào tạo" data={dataFromApi} columnMap={labelMap} />
            <BackButton />
        </section>
    );
}

export default TrainingTypeList;
