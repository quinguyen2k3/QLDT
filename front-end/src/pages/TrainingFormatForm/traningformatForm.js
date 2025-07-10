import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

// Components
import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input } from '@/components/Form/FormGroup';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
// Hooks
import useFormMode from '@/hooks/FormMode';
// API
import { formatApi } from '@/service/apis';
//Toast
import { toast } from 'react-toastify';

function FormatForm() {
    const { id } = useParams();
    const isEditMode = !!id;

    const [formData, setFormData] = useState({
        name: '',
        note: '',
        createdDate: '',
    });

    const { pageTitle } = useFormMode('/format/update', {
        add: 'Thêm Mới Hình Thức Đào Tạo',
        edit: 'Thay Đổi Thông Tin Hình Thức Đào Tạo',
    });

    useEffect(() => {
        const fetchFormat = async () => {
            if (isEditMode) {
                try {
                    const res = await formatApi.getById(id);
                    setFormData({
                        name: res.data.data.name || '',
                        note: res.data.data.note || '',
                        createdDate: res.data.data.createdDate?.slice(0, 10) || '',
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
            createdDate: '',
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (isEditMode) {
                await formatApi.update(id, formData);
                toast.success('Cập nhật thông tin thành công!');
            } else {
                await formatApi.create(formData);
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

            <div className="card card-default">
                <FormHeader title="Bảng thông tin" />
                <form onSubmit={handleSubmit}>
                    <div className="card-body">
                        <div className="row">
                            <div className="col-md-3">
                                <Input
                                    id="format-name"
                                    label="Tên Hình Thức Đào Tạo"
                                    name="name"
                                    value={formData.name}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-6">
                                <Input
                                    id="format-note"
                                    label="Ghi Chú"
                                    name="note"
                                    value={formData.note}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    id="format-created-date"
                                    label="Ngày Tạo"
                                    name="createdDate"
                                    type="date"
                                    value={formData.createdDate}
                                    onChange={handleChange}
                                />
                            </div>
                        </div>
                    </div>
                    <FormFooter isEdit={isEditMode} />
                </form>
            </div>
            <BackButton />
        </section>
    );
}

export default FormatForm;
