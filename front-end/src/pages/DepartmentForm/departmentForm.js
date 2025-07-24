import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input, Selector } from '@/components/Form/FormGroup';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';
import { departmentApi, partApi } from '@/service/apis';
import { toast } from 'react-toastify';
import Switch from 'react-switch';

function DepartmentForm() {
    const { id } = useParams();
    const isEditMode = !!id;

    const [formData, setFormData] = useState({
        name: '',
        note: '',
        partId: '',
        isActive: false,
    });

    const { pageTitle } = useFormMode('/department/update', {
        add: 'Thêm Mới Thông Tin Khoa Phòng',
        edit: 'Thay Đổi Thông Tin Khoa Phòng',
    });

    const [parts, setParts] = useState([]);

    useEffect(() => {
        const fetchFormat = async () => {
            const parts = await partApi.getAllActive();
            setParts(parts.data.data);
            if (isEditMode) {
                try {
                    const dep = await departmentApi.getById(id);
                    setFormData({
                        name: dep.data.data.name || '',
                        note: dep.data.data.note || '',
                        partId: dep.data.data.partId || '',
                        isActive: dep.data.data.isActive || false,
                    });
                } catch (error) {
                    console.error('Lỗi tải dữ liệu:', error);
                    toast.error('Lỗi tải dữ liệu');
                }
            }
        };
        fetchFormat();
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
            partId: '',
            isActive: false,
        });
    };

    const validateForm = () => {
        const errors = [];

        if (!formData.name.trim()) {
            errors.push('Tên khoa phòng là bắt buộc.');
        }

        if (!formData.partId) {
            errors.push('Vui lòng chọn bộ phận.');
        }

        return errors;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        
        const errors = validateForm();

        if (errors.length > 0) {
            errors.forEach((err) => toast.error(err));
            return;
        }
        try {
            if (isEditMode) {
                await departmentApi.update(id, formData);
                toast.success('Cập nhật thông tin thành công!');
            } else {
                await departmentApi.create(formData);
                toast.success('Thêm thông tin thành công!');
                resetForm();
            }
        } catch (error) {
            if (error.response?.status !== 403) {
                console.error('Lỗi tải dữ liệu:', error);
                toast.error('Lỗi tải dữ liệu');
            }
        }
    };

    return (
        <section className="content">
            <PageHeader title={pageTitle} />
            <form onSubmit={handleSubmit}>
                <div className="card card-default">
                    <FormHeader title="Bảng thông tin" />
                    <div className="card-body">
                        <div className="row">
                            <div className="col-md-6">
                                <Input
                                    name="name"
                                    id="department-name"
                                    label="Tên Khoa Phòng"
                                    value={formData.name}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-6">
                                <Selector
                                    id="part-select"
                                    name="partId"
                                    label="Chọn Bộ Phận"
                                    options={parts}
                                    placeholderText="--Chọn Bộ Phận--"
                                    value={formData.partId}
                                    onChange={handleChange}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-6">
                                <Input
                                    name="note"
                                    id="note"
                                    label="Ghi Chú"
                                    value={formData.note}
                                    onChange={handleChange}
                                />
                            </div>
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

export default DepartmentForm;
