import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input, Selector } from '@/components/Form/FormGroup';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';
import { employeeApi, departmentApi, levelApi, majorApi } from '@/service/apis';
import { toast } from 'react-toastify';
import Switch from 'react-switch';

const gender = [
    { id: 'Nam', name: 'Nam' },
    { id: 'Nữ', name: 'Nữ' },
];

function EmployeeForm() {
    const { id } = useParams();
    const isEditMode = !!id;

    const [formData, setFormData] = useState({
        name: '',
        emMaCBVC: '',
        emGioiTinh: '',
        emNgaySinh: '',
        emChucDanh: '',
        emChucVu: '',
        emSDT: '',
        depId: '',
        levelId: '',
        majorId: '',
        isActive: false,
    });

    const { pageTitle } = useFormMode('/employee/update', {
        add: 'Thêm Mới Thông Tin Nhân Sự',
        edit: 'Thay Đổi Thông Tin Nhân Sự',
    });

    const [deps, setDeps] = useState([]);
    const [levels, setLevels] = useState([]);
    const [majors, setMajors] = useState([]);

    useEffect(() => {
        const fetchFormat = async () => {
            const resDep = await departmentApi.getAllActive();
            setDeps(resDep.data.data);

            const resLevel = await levelApi.getAllActive();
            setLevels(resLevel.data.data);

            const resMajor = await majorApi.getAllActive();
            setMajors(resMajor.data.data);

            if (isEditMode) {
                try {
                    const res = await employeeApi.getById(id);
                    setFormData({
                        name: res.data.data.name || '',
                        emMaCBVC: res.data.data.emMaCBVC || '',
                        emGioiTinh: res.data.data.emGioiTinh || '',
                        emNgaySinh: res.data.data.emNgaySinh?.slice(0, 10) || '',
                        emChucDanh: res.data.data.emChucDanh || '',
                        emChucVu: res.data.data.emChucVu || '',
                        emSDT: res.data.data.emSDT || '',
                        depId: res.data.data.depId || '',
                        levelId: res.data.data.levelId || '',
                        majorId: res.data.data.majorId || '',
                        isActive: res.data.data.isActive ?? false,
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
            emMaCBVC: '',
            emGioiTinh: '',
            emNgaySinh: '',
            emChucDanh: '',
            emChucVu: '',
            emSDT: '',
            depId: '',
            levelId: '',
            majorId: '',
            isActive: false,
        });
    };

    const validateForm = () => {
        const errors = [];

        if (!formData.name.trim()) {
            errors.push('Tên nhân viên là bắt buộc.');
        }
        if (!formData.emMaCBVC.trim()) {
            errors.push('Mã CBVC là bắt buộc.');
        }
        if (!formData.emGioiTinh) {
            errors.push('Vui lòng chọn giới tính.');
        }
        if (!formData.emNgaySinh) {
            errors.push('Ngày sinh là bắt buộc.');
        }
        if (!formData.depId) {
            errors.push('Vui lòng chọn khoa phòng.');
        }
        if (!formData.levelId) {
            errors.push('Vui lòng chọn trình độ.');
        }
        if (!formData.majorId) {
            errors.push('Vui lòng chọn chuyên ngành.');
        }
        if (!formData.emSDT.trim()) {
            errors.push('Số điện thoại là bắt buộc.');
        } else if (!/^\d{10,11}$/.test(formData.emSDT)) {
            errors.push('Số điện thoại không hợp lệ.');
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
                await employeeApi.update(id, formData);
                toast.success('Cập nhật thông tin thành công!');
            } else {
                await employeeApi.create(formData);
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
                            <div className="col-md-6">
                                <Selector
                                    id="department-select"
                                    name="depId"
                                    label="Thuộc khoa phòng"
                                    value={formData.depId}
                                    onChange={handleChange}
                                    options={deps}
                                    placeholderText="--Chọn Khoa - Phòng--"
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="emChucDanh"
                                    id="professional"
                                    label="Chức Danh"
                                    value={formData.emChucDanh}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="emChucVu"
                                    id="position"
                                    label="Chức Vụ"
                                    value={formData.emChucVu}
                                    onChange={handleChange}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-3">
                                <Input
                                    name="name"
                                    id="fullname"
                                    label="Tên Nhân Viên"
                                    value={formData.name}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="emMaCBVC"
                                    id="employee-code"
                                    label="Mã Cán Bộ Viên Chức"
                                    value={formData.emMaCBVC}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Selector
                                    id="gender-select"
                                    name="levelId"
                                    label="Trình Độ"
                                    options={levels}
                                    value={formData.levelId}
                                    onChange={handleChange}
                                    placeholderText="--Chọn Trình Độ--"
                                />
                            </div>
                            <div className="col-md-3">
                                <Selector
                                    id="major-select"
                                    name="majorId"
                                    label="Chuyên Ngành"
                                    options={majors}
                                    value={formData.majorId}
                                    onChange={handleChange}
                                    placeholderText="--Chọn Chuyên Ngành--"
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-3">
                                <Input
                                    name="emNgaySinh"
                                    id="birthday"
                                    label="Ngày Sinh"
                                    type="date"
                                    value={formData.emNgaySinh}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Selector
                                    id="level-select"
                                    name="emGioiTinh"
                                    label="Giới Tính"
                                    options={gender}
                                    value={formData.emGioiTinh}
                                    onChange={handleChange}
                                    placeholderText="--Chọn Giới Tính--"
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="emSDT"
                                    id="sdt"
                                    label="Số Điện Thoại"
                                    value={formData.emSDT}
                                    onChange={handleChange}
                                />
                            </div>
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

export default EmployeeForm;
