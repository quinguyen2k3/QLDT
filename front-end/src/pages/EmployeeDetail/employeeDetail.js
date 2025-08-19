import React, { useEffect, useState } from 'react';
import { useParams, useLocation } from 'react-router-dom';
import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';
import { employeeApi, classApi, certificateApi } from '@/service/apis';
import { toast } from 'react-toastify';

function EmployeeDetail() {
    const { id } = useParams();
    const location = useLocation();
    const [loading, setLoading] = useState(true);
    const [employee, setEmployee] = useState({});
    const [classes, setClasses] = useState([]);
    const [certificates, setCertificates] =useState([]);

    useEffect(() => {
        const fetchEmployeeDetail = async () => {
            try {
               let empResponse, classResponse, cerResponse;
                if (location.pathname.includes('/learning-process')) {
                    empResponse = await employeeApi.getMyEmployeeInfo();
                    classResponse = await classApi.getAllUserStudied();
                    cerResponse = await certificateApi.getAllByMe();
                } else {
                    empResponse = await employeeApi.getById(id);
                    classResponse = await classApi.getAllByEmployee(id);
                    cerResponse = await certificateApi.getAllByEmployee(id);
                }
                const empData = empResponse.data.data;
                const classData = classResponse.data.data;
                const cerData = cerResponse.data.data;
                setEmployee(empData);
                setClasses(classData);
                setCertificates(cerData);

            } catch (error) {
                console.error('Lỗi tải dữ liệu:', error);
                toast.error('Lỗi tải dữ liệu');
            } finally {
                setLoading(false);
            }
        };
        fetchEmployeeDetail();
    }, [id, location.pathname]);

    const classLabelMap = {
        name: 'Tên Lớp Học',
        content: 'Nội Dung Lớp Học',
        classSoTiet: 'Số Tiết',
        hour: 'Số Tính Chỉ',
    };

    const classColumnHidden = [
        'classSoCVTS',
        'classNgayCVTS',
        'classSoQDDH',
        'classNgayQDDH',
        'classSoQDML',
        'classNgayQDML',
        'formatId',
        'formatName',
        'levelId',
        'courseId',
        'unitId',
        'hourId',
        'attachments',
        'employeeIds',
        'isActive',
    ];

    const certificateLabelMap = {
        certificateNumber: 'Số Hiệu',
        issueDate: 'Ngày Cấp',
        unitName: 'Đơn Vị Cấp',
        className: 'Tên Lớp Học',
    };

    const certificateColumnHidden = [
        'unitId',
        'classId',
        'attachments'
    ];

    const totalSoTiet = classes.reduce((sum, item) => sum + (item.classSoTiet || 0), 0);
    const totalSoTinhChi = classes.reduce((sum, item) => sum + (item.hour || 0), 0);

    return (
        <section className="content">
            <PageHeader title="Chi Tiết Thông Tin Nhân Viên" />
            <section className="content">
                <div className="container-fluid">
                    <div className="row">
                        <div className="col-md-3">
                            <div className="card card-infor mb-3">
                                <div
                                    className="card-header bg-white text-center border-bottom"
                                    style={{ borderTop: '4px solid #28a745' }}
                                >
                                    <h5 className="mb-1 font-weight-bold">{employee.name}</h5>
                                    <div className="text-muted h6 mb-0">{employee.emMaCBVC}</div>
                                </div>

                                <div className="card-body">
                                    <div className="d-flex justify-content-between border-bottom py-2">
                                        <strong>Chức vụ:</strong>
                                        <span className="text-primary">{employee.emChucVu}</span>
                                    </div>

                                    <div className="d-flex justify-content-between border-bottom py-2">
                                        <strong>Chức danh:</strong>
                                        <span className="text-primary">{employee.emChucDanh}</span>
                                    </div>

                                    <div className="d-flex justify-content-between border-bottom py-2">
                                        <strong>Ngày Sinh:</strong>
                                        <span className="text-primary">
                                            {employee.emNgaySinh
                                                ? new Date(employee.emNgaySinh).toLocaleDateString('vi-VN')
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
                                        <strong>Tổng số Giờ Tính Chỉ: </strong>
                                        <span className="text-danger font-weight-bold">{totalSoTinhChi}</span> Giờ.
                                    </p>
                                </div>
                            </div>
                        </div>

                        <div className="col-md-9">
                            <DataTable
                                title="Chi tiết khóa học"
                                tableId="classData"
                                data={classes}
                                columnMap={classLabelMap}
                                columnHidden={classColumnHidden}
                                showActions={false}
                            />
                            <DataTable
                                title="Danh sách chứng chỉ"
                                tableId="certificateData"
                                data={certificates}
                                columnMap={certificateLabelMap}
                                columnHidden={certificateColumnHidden}
                                showActions={false}
                            />
                        </div>
                    </div>
                </div>
            </section>
            <BackButton />
        </section>
    );
}

export default EmployeeDetail;
