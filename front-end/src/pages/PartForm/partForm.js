import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input } from '@/components/Form/FormGroup';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';
import { partApi } from '@/service/apis';

import { toast } from 'react-toastify';

function PartForm() {
    const { id } = useParams();
    const isEditMode = !!id;

    const [formData, setFormData] = useState({
        name: '',
        note: '',
        createdDate: '',
    });

    const { pageTitle } = useFormMode('/part/update', {
        add: 'Thêm Mới Thông Tin Bộ Phận',
        edit: 'Thay Đổi Thông Tin Bộ Phận',
    });

    useEffect(() => {
        const fetchFormat = async () => {
            if (isEditMode) {
                try {
                    const res = await partApi.getById(id);
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
                await partApi.update(id, formData);
                toast.success('Cập nhật thông tin thành công!');
            } else {
                await partApi.create(formData);
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
                            <div className="col-md-4">
                                <Input
                                    name="name"
                                    id="part-name"
                                    label="Tên Bộ Phận"
                                    value={formData.name}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-4">
                                <Input
                                    name="note"
                                    id="note"
                                    label="Ghi Chú"
                                    value={formData.note}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-4">
                                <Input
                                    name="createdDate"
                                    type="date"
                                    id="created_date"
                                    label="Ngày Tạo"
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

export default PartForm;
