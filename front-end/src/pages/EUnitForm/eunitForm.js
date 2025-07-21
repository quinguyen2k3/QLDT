import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input } from '@/components/Form/FormGroup';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';
import { unitApi } from '@/service/apis';
import { toast } from 'react-toastify';
import Switch from 'react-switch';

function EUnitForm() {
    const { id } = useParams();
    const isEditMode = !!id;

    const [formData, setFormData] = useState({
        name: '',
        note: '',
        isActive: false
    });

    const { pageTitle } = useFormMode('/eunit/update', {
        add: 'Thêm Mới Thông Tin Đơn Vị Đào Tạo',
        edit: 'Thay Đổi Thông Tin Bộ Phận',
    });

    useEffect(() => {
        const fetchFormat = async () => {
            if (isEditMode) {
                try {
                    const res = await unitApi.getById(id);
                    setFormData({
                        name: res.data.data.name || '',
                        note: res.data.data.note || '',
                        isActive: res.data.data.isActive || false
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
            note: '',
            isActive: false
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (isEditMode) {
                await unitApi.update(id, formData);
                toast.success('Cập nhật thông tin thành công!');
            } else {
                await unitApi.create(formData);
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
                                    name="name"
                                    id="unit-name"
                                    label="Tên Đơn Vị Đào Tạo"
                                    value={formData.name}
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

export default EUnitForm;
