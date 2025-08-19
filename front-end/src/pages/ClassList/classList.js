import { useEffect, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { classApi } from '@/service/apis';
import { toast } from 'react-toastify';
import { useAuth } from '@/contexts';

function ClassList() {
    const navigate = useNavigate();
    const location = useLocation();
    const { user } = useAuth();
    const isLong = location.pathname.includes('longterm');
    const isShort = location.pathname.includes('shortterm');
    const isAll = location.pathname.includes('all');
    const isManage = !isLong && !isShort && !isAll;

    const pageTitle = isManage
        ? 'Danh Sách Lớp Học'
        : isLong
        ? 'Danh Sách Lớp Học Dài Hạn'
        : isShort
        ? 'Danh Sách Lớp Học Ngắn Hạn'
        : 'Danh Sách Tổng Hợp';

    const linkPrefix =
        isAll || isManage ? { updateLinkPrefix: '/class/update' } : { detailLinkPrefix: '/class/detail' };

    const handleListClick = () => {
        const targetPath = '/classes/list/all';
        if (location.pathname !== targetPath) {
            navigate(targetPath);
        }
    };

    const handleManageClick = () => {
        const targetPath = '/classes/list';
        if (location.pathname !== targetPath) {
            navigate(targetPath);
        }
    };

    const handleAddClick = () => {
        navigate('/class/create');
    };

    const [loading, setLoading] = useState(true);
    const [classes, setClasses] = useState([]);

    useEffect(() => {
        const fetchFormats = async () => {
            try {
                let response;
                if (isAll) {
                    response = await classApi.getAll();
                } else if (isManage) {
                    response = await classApi.getAllByMe();
                } else if (user?.role === 'POWER') {
                    const formatId = isLong ? 1 : 2;
                    response = await classApi.getAllByUserAndFormat(formatId);
                } else {
                    const formatId = isLong ? 1 : 2;
                    response = await classApi.getAllByFormat(formatId);
                }
                const classData = response.data.data.map((item) => ({
                    ...item,
                    classNgayQDML: item.classNgayQDML ? new Date(item.classNgayQDML).toLocaleDateString('vi-VN') : '',
                    classNgayKT: item.classNgayKT ? new Date(item.classNgayKT).toLocaleDateString('vi-VN') : '',
                    classNgayBD: item.classNgayBD ? new Date(item.classNgayBD).toLocaleDateString('vi-VN') : '',
                    classKinhPhi:
                        item.classKinhPhi === 0
                            ? 'Miễn phí'
                            : item.classKinhPhi
                            ? `${item.classKinhPhi.toLocaleString('vi-VN')} Đ`
                            : '',
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
    }, [location.pathname, isLong, isShort, isManage]);

    const labelMap = {
        name: 'Tên Lớp Học',
        classNgayBD: 'Ngày Bắt Đầu',
        classNgayKT: 'Ngày Kết Thúc',
        classSoTiet: 'Số Tiết',
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
        'hourId',
        'hour',
        'attachments',
        'employeeIds',
        'isActive',
    ];

    return (
        <section className="content">
            <PageHeader title={pageTitle} />
            <ToolBar
                title="Thanh Công Cụ - Chức Năng Hệ Thống"
                buttons={[
                    ...(isAll || isManage
                        ? [
                              ...(user?.permissions.includes('Report.ViewSummaryList')
                                  ? [
                                        {
                                            label: 'Danh Sách Tổng Hợp',
                                            className: 'btn-info',
                                            onClick: handleListClick,
                                        },
                                    ]
                                  : []),
                              {
                                  label: 'Thêm Mới',
                                  className: 'btn-success',
                                  onClick: handleAddClick,
                              },
                          ]
                        : []),
                    ...(user?.permissions.includes('Class.Manage') && (isLong || isShort)
                        ? [
                              {
                                  label: 'QL Lớp Học',
                                  className: 'btn-primary',
                                  onClick: handleManageClick,
                              },
                          ]
                        : []),
                ]}
            />
            {!loading && (
                <DataTable
                    title="Danh sách lớp học"
                    data={classes}
                    columnMap={labelMap}
                    columnHidden={columnHidden}
                    {...linkPrefix}
                />
            )}
            <BackButton />
        </section>
    );
}

export default ClassList;
