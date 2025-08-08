import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input, Selector } from '@/components/Form/FormGroup';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { userApi, roleApi, departmentApi } from '@/service/apis';
import { toast } from 'react-toastify';
import Switch from 'react-switch';

function UserForm() {
    const { id } = useParams();
    const isEditMode = !!id;

    const [formData, setFormData] = useState({
        name: '',
        username: '',
        password: '',
        email: '',
        phone: '',
        depId: '',
        roleId: '',
        isActive: false,
    });

    const { pageTitle } = useFormMode('/user/update', {
        add: 'Thêm Mới Thông Tin Tài Khoản Người Dùng',
        edit: 'Thay Đổi Thông Tin Tài Khoản Người Dùng',
    });

    const [roles, setRoles] = useState([]);
    const [departments, setDepartments] = useState([]);

    useEffect(() => {
        const fetchFormat = async () => {
            const roles = await roleApi.getAll();
            setRoles(roles.data.data);

            const departments = await departmentApi.getAll();
            setDepartments(departments.data.data);

            if (isEditMode) {
                try {
                    const res = await userApi.getById(id);
                    setFormData({
                        name: res.data.data.name || '',
                        username: res.data.data.username || '',
                        password: '',
                        email: res.data.data.email || '',
                        phone: res.data.data.phone || '',
                        depId: res.data.data.depId || '',
                        roleId: res.data.data.roleId || '',
                        isActive: res.data.data.isActive || false,
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
            username: '',
            password: '',
            email: '',
            phone: '',
            depId: '',
            roleId: '',
            isActive: false,
        });
    };

    const validateForm = () => {
        const errors = [];

        if (!formData.username.trim()) {
            errors.push('Tài khoản là bắt buộc.');
        } else {
            const noWhitespaceRegex = /^\S+$/; 
            const noSpecialNoDiacriticRegex = /^[a-zA-Z0-9]+$/;
            if (!noWhitespaceRegex.test(formData.username)) {
                errors.push('Tài khoản không được chứa khoảng trắng.');
            }
            if (!noSpecialNoDiacriticRegex.test(formData.username)) {
                errors.push('Tài khoản chỉ được chứa ký tự không dấu và không ký tự đặc biệt.');
            }
        }

        if (!formData.name.trim()) {
            errors.push('Họ tên là bắt buộc.');
        }

        if (!isEditMode && !formData.password.trim()) {
            errors.push('Mật khẩu là bắt buộc khi tạo mới.');
        }

        if (!formData.depId) {
            errors.push('Khoa - Phòng là bắt buộc.');
        }

        if (!formData.roleId) {
            errors.push('Nhóm quyền là bắt buộc.');
        }
        if (formData.phone.trim()) {
            const phoneRegex = /^(03|05|07|08|09)\d{8}$/;
            if (!phoneRegex.test(formData.phone)) {
                errors.push('Số điện thoại không đúng định dạng.');
            }
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
                await userApi.update(id, formData);
                toast.success('Cập nhật thông tin thành công!');
            } else {
                await userApi.create(formData);
                toast.success('Thêm thông tin thành công!');
                resetForm();
            }
        } catch (error) {
            if (error.response?.status === 409) {
                toast.error('Tài khoản đã tồn tại');
            } else {
                toast.error(isEditMode ? 'Cập nhật thông tin thất bại!' : 'Tạo mới thông tin thất bại!');
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
                            <div className="col-md-4">
                                <Input
                                    name="username"
                                    id="account"
                                    label="Tài khoản"
                                    placeholder="Nhập tên tài khoản"
                                    value={formData.username}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-4">
                                <Input
                                    name="name"
                                    id="fullname"
                                    label="Tên đầy đủ"
                                    placeholder="Nhập họ tên"
                                    value={formData.name}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-4">
                                <Selector
                                    name="depId"
                                    id="state-select"
                                    label="Thuộc khoa phòng"
                                    options={departments}
                                    placeholderText="--Chọn Khoa - Phòng--"
                                    value={formData.depId}
                                    onChange={handleChange}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-3">
                                <Input
                                    name="password"
                                    id="password"
                                    label="Mật khẩu"
                                    placeholder="Nhập mật khẩu"
                                    type="password"
                                    value={formData.password}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Selector
                                    name="roleId"
                                    id="role-select"
                                    label="Phân quyền"
                                    options={roles}
                                    placeholderText="--Chọn Nhóm Quyền--"
                                    value={formData.roleId}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="email"
                                    id="email"
                                    label="Thư điện tử"
                                    placeholder="Nhập email"
                                    type="email"
                                    value={formData.email}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="phone"
                                    id="phone"
                                    label="Số điện thoại"
                                    placeholder="Nhập số điện thoại"
                                    type="phone"
                                    value={formData.phone}
                                    onChange={handleChange}
                                />
                            </div>
                        </div>
                        <div>
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

export default UserForm;
