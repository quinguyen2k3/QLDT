import { useState, useEffect } from 'react';
import ToolBar from '@/components/ToolBar';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { useNavigate, useLocation } from 'react-router-dom';
import { employeeApi } from '@/service/apis';
import { toast } from 'react-toastify';
import { useAuth } from '@/contexts';

function EmployeeList() {
    const navigate = useNavigate();
    const location = useLocation();
    const { user } = useAuth();

    const isAll = location.pathname.includes('all');

    //Chuyển hướng sang trang Trang danh sách nhân sự
    const handleListClick = () => {
        const targetPath = '/employees/list/all';
        if (location.pathname !== targetPath) {
            navigate(targetPath);
        }
    };
   
    //Chuyển hướng sang trang Tạo mới nhân sự
    const handleAddClick = () => {
        navigate('/employee/create');
    };

    const [loading, setLoading] = useState(true);
    const [employees, setEmployees] = useState([]);

    useEffect(() => {
        const fetchFormats = async () => {
            try {
                let response

                if(isAll){
                    response = await employeeApi.getAll();
                }else{
                    response = await employeeApi.getAllByDepartmentMe();
                }
                
                const employeeData = response.data.data.map((item) => ({
                    ...item,
                    emNgaySinh: item.emNgaySinh ? new Date(item.emNgaySinh).toLocaleDateString('vi-VN') : '',
                }));
                setEmployees(employeeData);
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
    }, [location.pathname]);

    const columnHidden = ['emMaCBVC','depId', 'levelId', 'isActive'];

    //Map label từ api sang tên khác
    const labelMap = {
        name: 'Tên Nhân Viên',
        emGioiTinh: 'Giới Tính',
        emNgaySinh: 'Ngày Sinh',
        emChucDanh: 'Chức Danh',
        emChucVu: 'Chức Vụ',
        emSDT: 'Số Điện Thoại',
        depName: 'Khoa Phòng',
        levelName: 'Trình Độ',
    };

    return (
        <section className="content">
            <PageHeader title="Danh Sách Nhân Sự" />
            <ToolBar
                title="Thanh Công Cụ - Chức Năng Hệ Thống"
                buttons={[
                    ...(user?.role === 'ADMIN'
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
                ]}
            />
            <DataTable
                title="Danh sách nhân sự"
                data={employees}
                columnMap={labelMap}
                columnHidden={columnHidden}
                detailLinkPrefix="/employee/detail"
                updateLinkPrefix="/employee/update"
            />
            <BackButton />
        </section>
    );
}

export default EmployeeList;
