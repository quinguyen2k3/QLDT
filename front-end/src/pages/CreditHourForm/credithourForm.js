import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input } from '@/components/Form/FormGroup';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';
import { hourApi } from '@/service/apis';
import { toast } from 'react-toastify';
import Switch from 'react-switch';

function HourForm() {
    const { id } = useParams();
    const isEditMode = !!id;

    const [formData, setFormData] = useState({
        name: '',
        hour: 0,
        note: '',
        isActive: false,
    });

    const { pageTitle } = useFormMode('/elevel/update', {
        add: 'Thêm Mới Thông Tin Giờ Tín Chỉ',
        edit: 'Thay Đổi Thông Tin Giờ Tín Chỉ',
    });

    useEffect(() => {
        const fetchFormat = async () => {
            if (isEditMode) {
                try {
                    const res = await hourApi.getById(id);
                    setFormData({
                        name: res.data.data.name || '',
                        hour: res.data.data.hour || 0,
                        note: res.data.data.note || '',
                        createdDate: res.data.data.createdDate?.slice(0, 10) || '',
                        isActive: res.data.data.isActive,
                    });
                } catch (error) {
                    if (error.response?.status !== 403) {
                        console.error('Lỗi tải dữ liệu:', error);
                        toast.error('Lỗi tải dữ liệu');
                    }
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
            hour: 0,
            note: '',
            isActive: false,
        });
    };

    const validateForm = () => {
        const errors = [];
        if (!formData.hour || formData.hour <= 0) {
            errors.push('Số giờ là bắt buộc và phải lớn hơn 0.');
        }
        return errors;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const errors = validateForm();
        if (errors.length > 0) {
            errors.forEach((err) => toast.warning(err));
            return;
        }
        try {
            if (isEditMode) {
                await hourApi.update(id, formData);
                toast.success('Cập nhật thông tin thành công!');
            } else {
                await hourApi.create(formData);
                toast.success('Thêm thông tin thành công!');
                resetForm();
            }
        } catch (error) {
            console.error('Lỗi submit:', error);
            toast.error(isEditMode ? 'Cập nhật thông tin thất bại!' : 'Tạo mới thông tin thất bại!');
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
                            <div class="col-md-6">
                                <Input
                                    type="number"
                                    name="hour"
                                    id="hour"
                                    label="Số giờ tín chỉ"
                                    value={formData.hour}
                                    onChange={handleChange}
                                />
                            </div>
                            <div class="col-md-6">
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
                            <div className="col-md-2 d-flex align-items-center">
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

export default HourForm;
