import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input, Selector } from '@/components/Form/FormGroup';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';

// Dữ liệu test
const states = [
    { id: 1, name: 'Phòng Công nghệ thông tin' },
    { id: 2, name: 'Khoa dược' },
    { id: 3, name: 'Khoa hô hấp' },
];

function UserForm() {
    const { pageTitle } = useFormMode('/course/update', {
        add: 'Thêm Mới Thông Tin Nhân Sự',
        edit: 'Thay Đổi Thông Tin Nhân Sự',
    });

    return (
        <section className="content">
            <PageHeader title={pageTitle} />
            <div className="card card-default">
                <FormHeader title="Bảng thông tin" />
                <div className="card-body">
                    <div className="row">
                        <div class="col-md-6">
                            <Selector
                                id="department-select"
                                label="Thuộc khoa phòng"
                                options={states}
                                placeholderText="--Chọn Khoa - Phòng--"
                            />
                        </div>
                        <div class="col-md-3">
                            <Input id="employee-code" label="Mã Cán Bộ Viên Chức" />
                        </div>
                        <div class="col-md-3">
                            <Input id="position-code" label="Mã Chức Danh" />
                        </div>
                    </div>
                    <div className="row">
                        <div class="col-md-3">
                            <Input id="fullname" label="Tên Nhân Viên" />
                        </div>
                        <div class="col-md-3">
                            <Input id="ethnicity" label="Dân Tộc" />
                        </div>
                        <div class="col-md-3">
                            <Input id="qualification" label="Trình Độ" />
                        </div>
                        <div class="col-md-3">
                            <Input id="party-day" label="Ngày Vào Đảng" type="date" />
                        </div>
                    </div>
                    <div className="row">
                        <div class="col-md-3">
                            <Input id="gender" label="Giới Tính" />
                        </div>
                        <div class="col-md-3">
                            <Input id="birthday" label="Ngày Sinh" type="date" />
                        </div>
                        <div class="col-md-3">
                            <Input id="contract-info" label="Thông Tin Hợp Đồng" />
                        </div>
                        <div class="col-md-3">
                            <Input id="note" label="Ghi Chú" />
                        </div>
                    </div>
                </div>
                <FormFooter />
            </div>
            <BackButton />
        </section>
    );
}

export default UserForm;
