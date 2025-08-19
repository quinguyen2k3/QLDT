import React, { useEffect, useState, useRef, useCallback } from 'react';
import { useParams, useLocation } from 'react-router-dom';
import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input, Selector, Radio, FileInput } from '@/components/Form/FormGroup';
import DataTable from '@/components/DataTable';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';
import { classApi, levelApi, unitApi, formatApi, courseApi, employeeApi, majorApi, hourApi } from '@/service/apis';
import { toast } from 'react-toastify';
import Switch from 'react-switch';
import { useAuth } from '@/contexts';

function ClassForm() {
    const { id } = useParams();
    const { user } = useAuth();
    const location = useLocation();
    const isEditMode = location.pathname.includes('update');
    const isDetailMode = location.pathname.includes('detail');

    const { pageTitle } = useFormMode({
        update: '/class/update',
        detail: '/class/detail',
        title: {
            add: 'Thêm Mới Thông Tin Lớp Học',
            edit: 'Thay Đổi Thông Tin Lớp Học',
            detail: 'Thông Tin Chi Tiết Lớp Học',
        },
    });

    const [formData, setFormData] = useState({
        name: '',
        classNgayBD: '',
        classNgayKT: '',
        classSoTiet: '',
        unitId: '',
        levelId: '',
        majorId: '',
        hourId: '',
        courseId: null,
        content: '',
        classSoQDDH: '',
        classNgayQDDH: '',
        classNgayCVTS: '',
        classSoCVTS: '',
        classSoQDML: '',
        classNgayQDML: '',
        classKinhPhi: '',
        isActive: false,
    });
    const fileInputRef = useRef();
    const [units, setUnits] = useState([]);
    const [courses, setCourses] = useState([]);
    const [levels, setLevels] = useState([]);
    const [formats, setFormats] = useState([]);
    const [employees, setEmployees] = useState([]);
    const [majors, setMajors] = useState([]);
    const [hours, setHours] = useState([]);
    const [initialFiles, setInitialFiles] = useState([]);
    const [selectedEmployeeIds, setSelectedEmployeeIds] = useState([]);
    const [isChooseCourse, setIsChooseCourse] = useState(false);

    const validateForm = () => {
        if (isDetailMode) return [];
        const errors = [];
        if (!formData.name.trim()) errors.push('Tên lớp là bắt buộc.');
        if (!formData.classNgayBD) errors.push('Ngày bắt đầu là bắt buộc.');
        if (!formData.classNgayKT) errors.push('Ngày kết thúc là bắt buộc.');
        if (!formData.classSoTiet || Number(formData.classSoTiet) <= 0) errors.push('Số tiết phải lớn hơn 0.');
        if (!formData.unitId) errors.push('Vui lòng chọn Đơn vị đào tạo.');
        if (!formData.levelId) errors.push('Vui lòng chọn Trình độ đào tạo.');
        if (!formData.majorId) errors.push('Vui lòng chọn Chuyên ngành.');
        if (!formData.hourId) errors.push('Vui lòng chọn Giờ tín chỉ.');
        if (isChooseCourse && !formData.courseId) {
            errors.push('Vui lòng chọn Khóa học.');
        }
        if (formData.classNgayBD && formData.classNgayKT) {
            const start = new Date(formData.classNgayBD);
            const end = new Date(formData.classNgayKT);
            if (start > end) errors.push('Ngày bắt đầu không được sau ngày kết thúc.');
        }
        if (formData.classKinhPhi === '' || formData.classKinhPhi === null || formData.classKinhPhi === undefined) {
            errors.push('Kinh phí là bắt buộc.');
        } else if (Number(formData.classKinhPhi) < 0) {
            errors.push('Kinh phí không được là số âm.');
        }
        return errors;
    };

    useEffect(() => {
        if (!user || !user.role) return;
        const fetchFormat = async () => {
            const resUnit = await unitApi.getAllActive();
            setUnits(resUnit.data.data);
            const resLevel = await levelApi.getAllActive();
            setLevels(resLevel.data.data);
            const resFormat = await formatApi.getBasic();
            setFormats(resFormat.data.data);
            const resCourse = await courseApi.getAllActive();
            setCourses(resCourse.data.data);
            const resMajor = await majorApi.getAllActive();
            setMajors(resMajor.data.data);
            const resHour = await hourApi.getAllActive();
            setHours(resHour.data.data);
            let employee;
            if (user.role === 'ADMIN') {
                employee = await employeeApi.getAll();
            } else {
                employee = await employeeApi.getAllByDepartmentMe();
            }
            const formattedEmployees = (employee.data.data || []).map((item) => ({
                ...item,
                emNgaySinh: item.emNgaySinh ? new Date(item.emNgaySinh).toLocaleDateString('vi-VN') : '',
            }));
            setEmployees(formattedEmployees);
            if (isEditMode || isDetailMode) {
                try {
                    const res = await classApi.getById(id);
                    setFormData({
                        name: res.data.data.name || '',
                        classNgayBD: res.data.data.classNgayBD?.slice(0, 10) || '',
                        classNgayKT: res.data.data.classNgayKT?.slice(0, 10) || '',
                        classSoTiet:
                            res.data.data.classSoTiet !== null && res.data.data.classSoTiet !== undefined
                                ? String(res.data.data.classSoTiet)
                                : '',
                        classNgayQDDH: res.data.data.classNgayQDDH?.slice(0, 10) || '',
                        classSoQDDH: res.data.data.classSoQDDH || '',
                        classNgayCVTS: res.data.data.classNgayCVTS?.slice(0, 10) || '',
                        classSoCVTS: res.data.data.classSoCVTS || '',
                        classSoQDML: res.data.data.classSoQDML || '',
                        classNgayQDML: res.data.data.classNgayQDML?.slice(0, 10) || '',
                        classKinhPhi:
                            res.data.data.classKinhPhi !== null && res.data.data.classKinhPhi !== undefined
                                ? String(res.data.data.classKinhPhi)
                                : '',
                        hourId: res.data.data.hourId || '',
                        unitId: res.data.data.unitId || '',
                        majorId: res.data.data.majorId || '',
                        levelId: res.data.data.levelId || '',
                        formatId: res.data.data.formatId || '',
                        courseId: res.data.data.courseId || '',
                        content: res.data.data.content || '',
                        isActive: res.data.data.isActive || false,
                    });
                    setSelectedEmployeeIds(res.data.data.employeeIds || []);
                    setInitialFiles(res.data.data.attachments || []);
                    setIsChooseCourse(!!res.data.data.courseId);
                } catch (error) {
                    if (error.response?.status !== 403) {
                        console.error('Lỗi tải dữ liệu:', error);
                        toast.error('Lỗi tải dữ liệu');
                    }
                }
            }
        };
        fetchFormat();
    }, [id, user, isEditMode, isDetailMode]);

    const handleChange = (e) => {
        if (isDetailMode) return;
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const handleSelectedChange = useCallback(
        (ids) => {
            if (isDetailMode) return;
            setSelectedEmployeeIds(ids);
        },
        [isDetailMode],
    );

    const resetForm = () => {
        if (isDetailMode) return;
        setFormData({
            name: '',
            classNgayBD: '',
            classNgayKT: '',
            classSoTiet: '',
            classSoQDDH: '',
            classNgayQDDH: '',
            classNgayCVTS: '',
            classSoCVTS: '',
            classSoQDML: '',
            classNgayQDML: '',
            classKinhPhi: '',
            hourId: '',
            unitId: '',
            levelId: '',
            courseId: '',
            majorId: '',
            content: '',
            isActive: false,
        });
        fileInputRef.current?.reset();
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (isDetailMode) return;
        const errors = validateForm();
        if (errors.length > 0) {
            errors.forEach((err) => toast.warning(err));
            return;
        }
        if (!isEditMode && user?.role !== 'ADMIN') {
            formData.isActive = true;
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
            selectedEmployeeIds.forEach((id) => {
                data.append('employeeIds', id);
            });
            if (isEditMode) {
                const oldFileIds = fileInputRef.current?.uploadedFiles.map((f) => f.id) || [];
                oldFileIds.forEach((id) => {
                    data.append('oldFileIds', id);
                });
            }
            if (isEditMode) {
                await classApi.update(id, data);
                toast.success('Cập nhật thông tin thành công!');
            } else {
                await classApi.create(data);
                toast.success('Thêm thông tin thành công!');
                resetForm();
            }
        } catch (error) {
            console.error('Lỗi submit:', error);
            toast.error(isEditMode ? 'Cập nhật thông tin thất bại!' : 'Tạo mới thông tin thất bại!');
        }
    };

    const columnHidden = ['emMaCBVC', 'depId', 'levelId', 'isActive'];
    const labelMap = {
        name: 'Tên Nhân Viên',
        emGioiTinh: 'Giới Tính',
        emNgaySinh: 'Ngày Sinh',
        emChucDanh: 'Chức Danh',
        emChucVu: 'Chức Vụ',
        emSDT: 'Số Điện Thoại',
        depName: 'Khoa Phòng',
        levelName: 'Trình Độ',
    };

    const filteredEmployees = isDetailMode
        ? employees.filter((employee) => selectedEmployeeIds.includes(employee.id))
        : employees;

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
                                    disabled={isDetailMode}
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
                                    disabled={isDetailMode}
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
                                    disabled={isDetailMode}
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
                                    disabled={isDetailMode}
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
                                    options={units}
                                    onChange={handleChange}
                                    placeholderText="--Chọn Đơn Vị--"
                                    disabled={isDetailMode}
                                />
                            </div>
                            <div className="col-md-3">
                                <Selector
                                    name="levelId"
                                    id="level-select"
                                    label="Trình Độ Đào Tạo"
                                    value={formData.levelId}
                                    options={levels}
                                    onChange={handleChange}
                                    placeholderText="--Chọn Trình Độ--"
                                    disabled={isDetailMode}
                                />
                            </div>
                            <div className="col-md-3">
                                <Selector
                                    name="majorId"
                                    id="major-select"
                                    label="Chuyên Ngành Đào Tạo"
                                    value={formData.majorId}
                                    options={majors}
                                    onChange={handleChange}
                                    placeholderText="--Chọn Chuyên Ngành--"
                                    disabled={isDetailMode}
                                />
                            </div>
                            <div className="col-md-3">
                                <Selector
                                    type="number"
                                    name="hourId"
                                    id="credit-hour-select"
                                    label="Số Giờ Tín Chỉ"
                                    value={formData.hourId}
                                    options={hours}
                                    onChange={handleChange}
                                    placeholderText="--Chọn Giờ Tính Chỉ--"
                                    disabled={isDetailMode}
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
                                    disabled={isDetailMode}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    type="date"
                                    name="classNgayCVTS"
                                    id="classNgayCVTS"
                                    label="Ngày Công Văn Tuyển Sinh"
                                    value={formData.classNgayCVTS}
                                    onChange={handleChange}
                                    disabled={isDetailMode}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    name="classSoQDML"
                                    id="classSoQDML"
                                    label="Số Quyết Định Mở Lớp"
                                    value={formData.classSoQDML}
                                    onChange={handleChange}
                                    disabled={isDetailMode}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    type="date"
                                    name="classNgayQDML"
                                    id="classNgayQDML"
                                    label="Ngày Quyết Định Mở Lớp"
                                    value={formData.classNgayQDML}
                                    onChange={handleChange}
                                    disabled={isDetailMode}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-3">
                                <Input
                                    name="classSoQDDH"
                                    id="classSoQDDH"
                                    label="Số Quyết Định Đi Học"
                                    value={formData.classSoQDDH}
                                    onChange={handleChange}
                                    disabled={isDetailMode}
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
                                    disabled={isDetailMode}
                                />
                            </div>
                            <div className="col-md-3">
                                <Input
                                    type="number"
                                    name="classKinhPhi"
                                    id="classKinhPhi"
                                    label="Kinh Phí"
                                    value={formData.classKinhPhi}
                                    onChange={handleChange}
                                    disabled={isDetailMode}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-6">
                                <Input
                                    name="content"
                                    id="content"
                                    label="Nội Dung Lớp Học"
                                    value={formData.content}
                                    onChange={handleChange}
                                    disabled={isDetailMode}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-3">
                                <Radio
                                    label="Hình Thức Đào Tạo"
                                    name="formatId"
                                    options={formats}
                                    value={formData.formatId}
                                    onChange={(value) => {
                                        if (isDetailMode) return;
                                        setFormData((prev) => ({
                                            ...prev,
                                            formatId: value,
                                        }));
                                    }}
                                    disabled={isDetailMode}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-3">
                                <div className="form-check">
                                    <input
                                        className="form-check-input"
                                        type="checkbox"
                                        id="choose-course-checkbox"
                                        checked={isChooseCourse}
                                        onChange={(e) => {
                                            if (isDetailMode) return;
                                            setIsChooseCourse(e.target.checked);
                                            if (!e.target.checked) {
                                                setFormData((prev) => ({
                                                    ...prev,
                                                    courseId: null,
                                                }));
                                            }
                                        }}
                                        disabled={isDetailMode}
                                    />
                                    <label className="form-check-label" htmlFor="choose-course-checkbox">
                                        Chọn Khóa Học
                                    </label>
                                </div>
                            </div>
                        </div>
                        {isChooseCourse && (
                            <div className="row">
                                <div className="col-md-3">
                                    <Selector
                                        name="courseId"
                                        id="course-select"
                                        label="Khóa Học"
                                        value={formData.courseId}
                                        options={courses}
                                        onChange={handleChange}
                                        placeholderText="--Chọn Khóa Học--"
                                        disabled={isDetailMode}
                                    />
                                </div>
                            </div>
                        )}
                        <div className="row">
                            <div className="col-md-3">
                                <FileInput ref={fileInputRef} initialFiles={initialFiles} disabled={isDetailMode} />
                            </div>
                        </div>
                        {(user?.role === 'ADMIN' || (user?.role === 'USER' && isDetailMode)) && (
                            <div className="row justify-content-end">
                                <div className="col-md-2 d-flex align-items-center">
                                    <label className="form-label mb-0 mr-2">Trạng thái:</label>
                                    <Switch
                                        checked={formData.isActive}
                                        onChange={(value) => {
                                            if (isDetailMode) return;
                                            setFormData((prev) => ({
                                                ...prev,
                                                isActive: value,
                                            }));
                                        }}
                                        onColor="#28a745"
                                        offColor="#ccc"
                                        disabled={isDetailMode}
                                    />
                                </div>
                            </div>
                        )}
                    </div>
                </div>
                <DataTable
                    title="Chọn nhân sự tham gia lớp học"
                    data={
                        isDetailMode
                            ? employees.filter((employee) => selectedEmployeeIds.includes(employee.id))
                            : employees
                    }
                    columnMap={labelMap}
                    columnHidden={columnHidden}
                    enableMultiSelect={!isDetailMode}
                    onSelectedChange={handleSelectedChange}
                    initialSelectedIds={selectedEmployeeIds}
                    showActions={false}
                    updateLinkPrefix="/employee/update"
                />
                {!isDetailMode && <FormFooter isEdit={isEditMode} />}
            </form>
            <BackButton />
        </section>
    );
}

export default ClassForm;
