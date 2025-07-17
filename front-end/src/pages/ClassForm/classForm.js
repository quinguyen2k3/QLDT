import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input, Selector, Radio } from '@/components/Form/FormGroup';
import DataTable from '@/components/DataTable';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';
import { levelApi } from '@/service/apis';
import { toast } from 'react-toastify';
import Switch from 'react-switch';

function ClassForm() {
    const { id } = useParams();
    const isEditMode = !!id;

    const [formData, setFormData] = useState({
        name: '',
        classNgayBD: '',
        classNgayKT: '',
        classSoTiet: '',
        unitId: '',
        levelId: '',
        content: '',
        createdDate: '',
        classSoQDDH: '',
        classNgayCVTS: '',
        classSoCVTS: '',
        clasNgayCVTS: '',
        isActive: false,
    });

    const [formatId, setFormatId] = useState('');

    const { pageTitle } = useFormMode('/elevel/update', {
        add: 'Thêm Mới Thông Tin Lớp Học',
        edit: 'Thay Đổi Thông Tin Lớp Học',
    });

    useEffect(() => {
        const fetchFormat = async () => {
            if (isEditMode) {
                try {
                    const res = await levelApi.getById(id);
                    setFormData({
                        name: res.data.data.name || '',
                        note: res.data.data.note || '',
                        createdDate: res.data.data.createdDate?.slice(0, 10) || '',
                        isActive: res.data.data.isActive,
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
            classNgayBD: '',
            classNgayKT: '',
            classSoTiet: '',
            createdDate: '',
            classSoQDDH: '',
            classNgayCVTS: '',
            classSoCVTS: '',
            clasNgayCVTS: '',
            unitId: '',
            levelId: '',
            content: '',
            isActive: false,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const submitData = {
                ...formData,
                formatId: formatId || null,
            };

            if (isEditMode) {
                await levelApi.update(id, submitData);
                toast.success('Cập nhật thông tin thành công!');
            } else {
                await levelApi.create(submitData);
                toast.success('Thêm thông tin thành công!');
                resetForm();
            }
        } catch (error) {
            console.error('Lỗi submit:', error);
            toast.error(isEditMode ? 'Cập nhật thông tin thất bại!' : 'Tạo mới thông tin thất bại!');
        }
    };

    const employees = [
        {
            id: 1,
            code: 'EMP001',
            name: 'Nguyễn Văn A',
            gender: 'Nam',
            dob: '1990-01-01',
            position: 'Nhân viên Kế toán',
            department: 'Phòng Kế toán',
        },
        {
            id: 2,
            code: 'EMP002',
            name: 'Trần Thị B',
            gender: 'Nữ',
            dob: '1992-06-12',
            position: 'Trưởng phòng Nhân sự',
            department: 'Phòng Nhân sự',
        },
        {
            id: 3,
            code: 'EMP003',
            name: 'Lê Văn C',
            gender: 'Nam',
            dob: '1988-03-25',
            position: 'Kỹ sư IT',
            department: 'Phòng Công nghệ',
        },
    ];

    const labelMap = {
        code: 'Mã Nhân Viên',
        name: 'Họ Tên',
        gender: 'Giới Tính',
        dob: 'Ngày Sinh',
        position: 'Chức Vụ',
        department: 'Phòng Ban',
    };

    return (
        <section className="content">
            <PageHeader title={pageTitle} />
            <form onSubmit={handleSubmit}>
                <div className="card card-default">
                    <FormHeader title="Bảng thông tin" />
                    <div className="card-body">
                        <div className="row">
                            <div className="col-md-3">
                                <Input
                                    name="name"
                                    id="className"
                                    label="Tên Lớp"
                                    value={formData.name}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="classNgayBD"
                                    type="date"
                                    id="classNgayBD"
                                    label="Ngày Bắt Đầu"
                                    value={formData.classNgayBD}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="classNgayKT"
                                    type="date"
                                    id="classNgayKT"
                                    label="Ngày Kết Thúc"
                                    value={formData.classNgayKT}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="classSoTiet"
                                    type="number"
                                    id="classSoTiet"
                                    label="Số Tiết"
                                    value={formData.classSoTiet}
                                    onChange={handleChange}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-3">
                                <Selector
                                    name="unitId"
                                    id="unit-select"
                                    label="Đơn Vị Đào Tạo"
                                    value={formData.unitId}
                                    onChange={handleChange}
                                    placeholderText="--Chọn Đơn Vị--"
                                />
                            </div>
                            <div className="col-md-3">
                                <Selector
                                    name="levelId"
                                    id="level-select"
                                    label="Trình Độ Đào Tạo"
                                    value={formData.levelId}
                                    onChange={handleChange}
                                    placeholderText="--Chọn Trình Độ--"
                                />
                            </div>
                            <div className="col-md-6">
                                <Input
                                    name="content"
                                    id="content"
                                    label="Nội Dung Lớp Học"
                                    value={formData.content}
                                    onChange={handleChange}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-3">
                                <Input
                                    name="classSoCVTS"
                                    id="classSoCVTS"
                                    label="Số Công Văn Tuyển Sinh"
                                    value={formData.classSoCVTS}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    type="date"
                                    name="classNgayCVTS"
                                    id="classNgayCVTS"
                                    label="Ngày Công Văn Tuyển Sinh"
                                    value={formData.classNgayQDDH}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="classSoQDDH"
                                    id="classSoQDDH"
                                    label="Số Quyết Định Đi Học"
                                    value={formData.classSoQDDH}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    type="date"
                                    name="classNgayQDDH"
                                    id="classNgayQDDH"
                                    label="Ngày Quyết Định Đi Học"
                                    value={formData.classNgayQDDH}
                                    onChange={handleChange}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-3">
                                <Radio
                                    label="Hình Thức Đào Tạo"
                                    name="formatId"
                                    options={[
                                        { id: 1, name: 'Ngắn Hạn' },
                                        { id: 2, name: 'Dài Hạn' },
                                    ]}
                                    value={formatId}
                                    onChange={(value) => setFormatId(value)}
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
            <DataTable
                title="Danh sách nhân sự"
                data={employees}
                columnMap={labelMap}
                enableMultiSelect={true}
                showActions={false}
                updateLinkPrefix="/employee/update"
            />
            <BackButton />
        </section>
    );
}

export default ClassForm;