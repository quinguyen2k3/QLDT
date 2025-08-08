import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { employeeApi } from '@/service/apis';
import { toast } from 'react-toastify';

function EmployeeDetail() {
    const { id } = useParams();
    const [loading, setLoading] = useState(true);
    const [employee, setEmployee] = useState({});
    const [classes, setClasses] = useState([]);

    useEffect(() => {
        const fetchEmployeeDetail = async () => {
            try {
                const response = await employeeApi.getDetail(id);
                const data = response.data.data;
                setEmployee(data);
                setClasses(
                    data.classes.map((cls) => ({
                        employeeName: data.employeeName,
                        className: cls.className,
                        classContent: cls.classContent,
                        soTiet: cls.classSoTiet,
                        soTinhChi: cls.classSoTinhChi 
                    }))
                );
            } catch (error) {
                console.error('Lỗi tải dữ liệu:', error);
                toast.error('Lỗi tải dữ liệu');
            } finally {
                setLoading(false);
            }
        };
        fetchEmployeeDetail();
    }, [id]);

    const labelMap = {
        employeeName: 'Tên Nhân Viên',
        className: 'Tên Lớp Học',
        classContent: 'Nội Dung Lớp Học',
        soTiet: 'Số Tiết',
        soTinhChi: 'Số Tính Chỉ'
    };

    const totalSoTiet = classes.reduce((sum, item) => sum + (item.soTiet || 0), 0);
    const totalSoTinhChi = classes.reduce((sum, item) => sum + (item.soTinhChi || 0), 0);

    return (
        <section className="content">
            <PageHeader title="Chi Tiết Thông Tin Nhân Viên" />
            <section className="content">
                <div className="container-fluid">
                    <div className="row">
                        <div className="col-md-3">
                            <div className="card card-infor mb-3">
                                <div className="card-header bg-white text-center border-bottom" style={{ borderTop: '4px solid #28a745' }}>
                                    <h5 className="mb-1 font-weight-bold">{employee.employeeName}</h5>
                                    <div className="text-muted h6 mb-0">{employee.employeeMaCBVC}</div>
                                </div>

                                <div className="card-body">
                                    <div className="d-flex justify-content-between border-bottom py-2">
                                        <strong>Chức vụ:</strong>
                                        <span className="text-primary">{employee.employeeChucVu}</span>
                                    </div>

                                    <div className="d-flex justify-content-between border-bottom py-2">
                                        <strong>Chức danh:</strong>
                                        <span className="text-primary">{employee.employeeChucDanh}</span>
                                    </div>

                                    <div className="d-flex justify-content-between border-bottom py-2">
                                        <strong>Ngày Sinh:</strong>
                                        <span className="text-primary">
                                            {employee.employeeNgaySinh
                                                ? new Date(employee.employeeNgaySinh).toLocaleDateString('vi-VN')
                                                : ''}
                                        </span>
                                    </div>
                                </div>
                            </div>

                            <div className="card mb-3">
                                <div className="card-header bg-success text-white font-weight-bold">
                                    Chi Tiết Quá Trình
                                </div>
                                <div className="card-body align-items-center">
                                    <p className="mb-0">
                                        <strong>Tổng số Tích Lũy: </strong>
                                        <span className="text-danger font-weight-bold">{totalSoTiet}</span> Tiết học.
                                    </p>
                                    <p className="mb-0">
                                        <strong>Tổng số Tính Chỉ: </strong>
                                        <span className="text-danger font-weight-bold">{totalSoTinhChi}</span> Tính chỉ.
                                    </p>
                                </div>
                            </div>
                        </div>

                        <div className="col-md-9">
                            <DataTable title="Chi tiết khóa học" data={classes} columnMap={labelMap} showActions={false} />
                        </div>
                    </div>
                </div>
            </section>
            <BackButton />
        </section>
    );
}

export default EmployeeDetail;
