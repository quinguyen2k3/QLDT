import React, { useState, useEffect, useRef } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import useFormMode from '@/hooks/FormMode';
import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input, Selector } from '@/components/Form/FormGroup';
import FileInput from '@/components/Form/FormGroup/file';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import { unitApi, classApi, certificateApi } from '@/service/apis';
import { toast } from 'react-toastify';
import { useAuth } from '@/contexts';

function CertificateForm() {
    const { id } = useParams();
    const navigate = useNavigate();
    const { user } = useAuth();
    const isEditMode = !!id;
    const [formData, setFormData] = useState({
        certificateNumber: '',
        issueDate: '',
        unitId: '',
        classId: '',
    });
    const [units, setUnits] = useState([]);
    const [classes, setClasses] = useState([]);
    const [initialFiles, setInitialFiles] = useState([]);
    const fileInputRef = useRef();
    const { pageTitle } = useFormMode({
        update: '/certificate/update',
        title: { add: 'Thêm Mới Chứng Chỉ', edit: 'Cập Nhật Thông Tin Chứng Chỉ' },
    });

    useEffect(() => {
        if (!user) {
            navigate('/login');
            return;
        }

        const fetchData = async () => {
            try {
                const resUnits = await unitApi.getAllActive();
                setUnits(resUnits.data.data);
                const resClasses = await classApi.getAllUserStudied(user.id);
                setClasses(resClasses.data.data);
                if (isEditMode) {
                    const resCertificate = await certificateApi.getById(id);
                    const data = resCertificate.data.data;
                    setFormData({
                        certificateNumber: data.certificateNumber || '',
                        issueDate: data.issueDate?.slice(0, 10) || '',
                        unitId: data.unitId || '',
                        classId: data.classId || '',
                    });
                    setInitialFiles(data.attachments || []);
                }
            } catch (error) {
                if (error.response?.status !== 403) {
                    console.error('Lỗi tải dữ liệu:', error);
                    toast.error('Lỗi tải dữ liệu');
                }
            }
        };
        fetchData();
    }, [id, isEditMode, user, navigate]);

    const validateForm = () => {
        const errors = [];
        if (!formData.certificateNumber.trim()) {
            errors.push('Số hiệu chứng chỉ là bắt buộc.');
        }
        if (!formData.issueDate) {
            errors.push('Ngày cấp là bắt buộc.');
        }
        if (!formData.unitId) {
            errors.push('Vui lòng chọn đơn vị đào tạo.');
        }
        if (!formData.classId) {
            errors.push('Vui lòng chọn lớp học.');
        }
        return errors;
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const resetForm = () => {
        setFormData({
            certificateNumber: '',
            issueDate: '',
            unitId: '',
            classId: '',
        });
        fileInputRef.current?.reset();
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const errors = validateForm();
        if (errors.length > 0) {
            errors.forEach((error) => toast.warning(error));
            return;
        }

        try {
            const data = new FormData();
            Object.entries(formData).forEach(([key, value]) => {
                if (value) {
                    data.append(key, value);
                }
            });
            if (fileInputRef.current?.newFiles) {
                fileInputRef.current.newFiles.forEach((file) => {
                    data.append('attachments', file);
                });
            }
            if (isEditMode) {
                const oldFileIds = fileInputRef.current?.uploadedFiles?.map((f) => f.id) || [];
                data.append('oldFileIds', oldFileIds.length > 0 ? oldFileIds.join(',') : '');
                await certificateApi.update(id, data);
                toast.success('Cập nhật thông tin chứng chỉ thành công!');
            } else {
                await certificateApi.create(data);
                toast.success('Thêm mới chứng chỉ thành công!');
                resetForm();
            }
        } catch (error) {
            toast.error(isEditMode ? 'Cập nhật thất bại!' : 'Tạo mới thất bại!');
        }
    };

    return (
        <section className="content">
            <PageHeader title={pageTitle} />
            <form onSubmit={handleSubmit} encType="multipart/form-data">
                <div className="card card-default">
                    <FormHeader title="Bảng thông tin chứng chỉ" />
                    <div className="card-body">
                        <div className="row">
                            <div className="col-md-6">
                                <Input
                                    name="certificateNumber"
                                    id="certificate-number"
                                    label="Số Hiệu Chứng Chỉ"
                                    value={formData.certificateNumber}
                                    onChange={handleChange}
                                    required
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="issueDate"
                                    type="date"
                                    id="issue-date"
                                    label="Ngày Cấp"
                                    value={formData.issueDate}
                                    onChange={handleChange}
                                    required
                                />
                            </div>
                            <div className="col-md-3">
                                <Selector
                                    name="unitId"
                                    id="unit-select"
                                    label="Đơn Vị Đào Tạo"
                                    value={formData.unitId}
                                    onChange={handleChange}
                                    options={units}
                                    placeholderText="--Chọn Đơn Vị--"
                                    required
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-6">
                                <Selector
                                    name="classId"
                                    id="class-select"
                                    label="Lớp Học"
                                    value={formData.classId}
                                    onChange={handleChange}
                                    options={classes}
                                    placeholderText="--Chọn Lớp Học--"
                                    required
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-3">
                                <FileInput ref={fileInputRef} initialFiles={initialFiles} />
                            </div>
                        </div>
                    </div>
                    <FormFooter isEdit={isEditMode} />
                </div>
            </form>
            <BackButton />
        </section>
    );
}

export default CertificateForm;
