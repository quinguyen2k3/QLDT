import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { certificateApi } from '@/service/apis';
import { toast } from 'react-toastify';
import { useAuth } from '@/contexts';

function CertificateList() {
    const navigate = useNavigate();
    const { user } = useAuth();
    const pageTitle = 'Danh Sách Chứng Chỉ Của Tôi';
    const linkPrefix = { updateLinkPrefix: '/certificate/update' };

    const handleAddClick = () => {
        navigate('/certificate/create');
    };

    const handleViewLearningProcess = () => {
        navigate('/employee/learning-process');
    };

    const [loading, setLoading] = useState(true);
    const [certificates, setCertificates] = useState([]);

    useEffect(() => {
        const fetchCertificates = async () => {
            try {
                const response = await certificateApi.getAllByMe();
                const certificateData = response.data.data.map((item) => ({
                    ...item,
                    issueDate: item.issueDate ? new Date(item.issueDate).toLocaleDateString('vi-VN') : '',
                }));
                setCertificates(certificateData);
            } catch (error) {
                if (error.response?.status !== 403) {
                    console.error('Lỗi tải dữ liệu:', error);
                    toast.error('Lỗi tải dữ liệu');
                }
            } finally {
                setLoading(false);
            }
        };
        fetchCertificates();
    }, []);

    const labelMap = {
        certificateNumber: 'Số Hiệu',
        issueDate: 'Ngày Cấp',
        unitName: 'Đơn Vị Cấp',
        className: 'Tên Lớp Học',
    };

    const columnHidden = [
        'unitId',
        'classId',
        'attachments'
    ];

    return (
        <section className="content">
            <PageHeader title={pageTitle} />
            <ToolBar
                title="Thanh Công Cụ - Chức Năng Hệ Thống"
                buttons={[
                    ...(user?.permissions.includes('Certificate.Manage')
                        ? [
                              {
                                  label: 'Thêm Mới',
                                  className: 'btn-success',
                                  onClick: handleAddClick,
                              }
                          ]
                        : []),
                    ...(user?.permissions.includes('Report.ViewProcess')
                        ? [
                              {
                                  label: 'Xem Quá Trình Học',
                                  className: 'btn-info',
                                  onClick: handleViewLearningProcess,
                              }
                          ]
                        : []),
                ]}
            />
            {!loading && (
                <DataTable
                    title="Danh sách chứng chỉ"
                    data={certificates}
                    columnMap={labelMap}
                    columnHidden={columnHidden}
                    {...linkPrefix}
                />
            )}
            <BackButton />
        </section>
    );
}

export default CertificateList;