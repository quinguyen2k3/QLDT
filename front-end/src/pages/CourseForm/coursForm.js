import React, { useState, useEffect, useRef } from 'react';
import { useParams } from 'react-router-dom';
import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input, Selector } from '@/components/Form/FormGroup';
import FileInput from '@/components/Form/FormGroup/file';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';
import { departmentApi, courseApi } from '@/service/apis';
import { toast } from 'react-toastify';
import Switch from 'react-switch';

function CourseForm() {
    const { id } = useParams();
    const isEditMode = !!id;

    const [formData, setFormData] = useState({
        name: '',
        note: '',
        courseNgayKg: '',
        depId: '',
        content: '',
        isActive: false,
    });

    const [deps, setDeps] = useState([]);

    const [initialFiles, setInitialFiles] = useState([]);

    const fileInputRef = useRef();

    const { pageTitle } = useFormMode('/course/update', {
        add: 'Thêm Mới Khóa Học',
        edit: 'Cập Nhật Thông Tin Khóa Học',
    });

    const validateForm = () => {
        const errors = [];

        if (!formData.name.trim()) {
            errors.push('Tên khóa học là bắt buộc.');
        }

        if (!formData.courseNgayKg) {
            errors.push('Ngày khai giảng là bắt buộc.');
        }

        if (!formData.depId) {
            errors.push('Vui lòng chọn khoa phòng.');
        }

        return errors;
    };

    useEffect(() => {
        const fetchData = async () => {
            const resDep = await departmentApi.getAllActive();
            setDeps(resDep.data.data);

            if (isEditMode) {
                try {
                    const resCourse = await courseApi.getById(id);
                    const data = resCourse.data.data;
                    setFormData({
                        name: data.name || '',
                        note: data.note || '',
                        courseNgayKg: data.courseNgayKg?.slice(0, 10) || '',
                        depId: data.depId || '',
                        content: data.content || '',
                        isActive: data.isActive || false,
                    });
                    setInitialFiles(data.attachments || []);
                } catch (error) {
                    if (error.response?.status !== 403) {
                        console.error('Lỗi tải dữ liệu:', error);
                        toast.error('Lỗi tải dữ liệu');
                    }
                }
            }
        };
        fetchData();
    }, [id, isEditMode]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const resetForm = () => {
        setFormData({
            name: '',
            note: '',
            courseNgayKg: '',
            depId: '',
            content: '',
            isActive: false,
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
                if (typeof value === 'boolean') {
                    data.append(key, value ? 'true' : 'false');
                } else if (value !== null && value !== undefined && value !== '') {
                    data.append(key, value);
                }
            });

            fileInputRef.current?.newFiles.forEach((file) => {
                data.append('attachments', file);
            });

            if (isEditMode) {
                const oldFileIds = fileInputRef.current?.uploadedFiles.map((f) => f.id) || [];
                if (oldFileIds.length > 0) {
                    oldFileIds.forEach((id) => {
                        data.append('oldFileIds', id);
                    });
                } else {
                    data.append('oldFileIds', '');
                }
            }

            if (isEditMode) {
                await courseApi.update(id, data);
                toast.success('Cập nhật thông tin khóa học thành công!');
            } else {
                await courseApi.create(data);
                toast.success('Thêm mới khóa học thành công!');
                resetForm();
            }
        } catch (error) {
            console.error('Lỗi submit:', error.response?.data || error);
            toast.error(isEditMode ? 'Cập nhật thất bại!' : 'Tạo mới thất bại!');
        }
    };

    return (
        <section className="content">
            <PageHeader title={pageTitle} />
            <form onSubmit={handleSubmit} encType="multipart/form-data">
                <div className="card card-default">
                    <FormHeader title="Bảng thông tin" />
                    <div className="card-body">
                        <div className="row">
                            <div className="col-md-6">
                                <Input
                                    name="name"
                                    id="course-name"
                                    label="Tên Khóa Học"
                                    value={formData.name}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="courseNgayKg"
                                    type="date"
                                    id="opening-day"
                                    label="Ngày Khai Giảng"
                                    value={formData.courseNgayKg}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Selector
                                    name="depId"
                                    id="department-select"
                                    label="Chọn Khoa Phòng"
                                    value={formData.depId}
                                    onChange={handleChange}
                                    options={deps}
                                    placeholderText="--Chọn Khoa - Phòng--"
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-6">
                                <Input
                                    name="content"
                                    id="training-content"
                                    label="Nội Dung Đào Tạo"
                                    value={formData.content}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-6">
                                <Input
                                    name="note"
                                    id="note"
                                    label="Ghi Chú"
                                    value={formData.note}
                                    onChange={handleChange}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-3">
                                <FileInput ref={fileInputRef} initialFiles={initialFiles} />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-3 d-flex align-items-center">
                                <label className="form-label mb-0 mr-2">Trạng thái:</label>
                                <Switch
                                    checked={formData.isActive}
                                    onChange={(value) =>
                                        setFormData((prev) => ({
                                            ...prev,
                                            isActive: value,
                                        }))
                                    }
                                    onColor="#28a745"
                                    offColor="#ccc"
                                />
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

export default CourseForm;
