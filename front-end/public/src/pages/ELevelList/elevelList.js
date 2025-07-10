import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton'
import { useNavigate } from 'react-router-dom';

function ELevelList() {
    const navigate = useNavigate();

    //Chuyển hướng sang trang T
    const handleAddClick = () => {
        navigate('/elevel/create');
    };

    //Dữ liệu giả lập
    const dataFromApi = [
        {
            id: 1,
            education_level: 'Thạc Sĩ',
            note: '',
            created_at: '07/02/2024',
        },
        {
            id: 2,
            education_level: 'Tiến Sĩ',
            note: '',
            created_at: '07/02/2024',
        },
        {
            id: 3,
            education_level: 'Chuyên Khoa II',
            note: '',
            created_at: '07/02/2024',
        },
    ];

    //Map label từ api sang tên khác
    const labelMap = {
        education_level: 'Trình Độ Đào Tạo',
        note: 'Ghi Chú',
        created_at: 'Ngày Tạo',
    };

    return (
        <section className="content">
            <PageHeader title="Danh Sách Trình Độ Đào Tạo" />
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
            <DataTable title="Danh sách trình độ đào tạo" data={dataFromApi} columnMap={labelMap} updateLinkPrefix="/elevel/update"/>
            <BackButton />
        </section>
    );
}

export default ELevelList;
