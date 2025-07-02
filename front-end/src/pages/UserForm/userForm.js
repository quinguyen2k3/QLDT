import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import {Input, Selector} from '@/components/Form/FormGroup';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode'

// Dữ liệu test
const states = [
    { id: 1, name: 'Phòng Công nghệ thông tin' },
    { id: 2, name: 'Khoa dược' },
    { id: 3, name: 'Khoa hô hấp' },
];

const roles = [
    { id: 1, name: 'Quyền Admin' },
    { id: 2, name: 'Quyền User' },
];

function UserForm() {
     const { pageTitle } = useFormMode('/user/update', {
        add: 'Thêm Mới Thông Tin Tài Khoản Người Dùng',
        edit: 'Thay Đổi Thông Tin Tài Khoản Người Dùng',
    });

    return (
        <section className="content">
            <PageHeader title={pageTitle}  />
            <div className="card card-default">
                <FormHeader title="Bảng thông tin" />
                <div className="card-body">
                    <div className="row">
                        <div class="col-md-4">
                            <Input id="account" label="Tài khoản" placeholder="Nhập tên tài khoản" />
                        </div>
                        <div class="col-md-4">
                            <Input id="fullname" label="Tên đầy đủ" placeholder="Nhập họ tên" />
                        </div>
                        <div class="col-md-4">
                            <Selector
                                id="state-select"
                                label="Thuộc khoa phòng"
                                options={states}
                                placeholderText="--Chọn Khoa - Phòng--"
                            />
                        </div>
                    </div>
                    <div className="row">
                        <div class="col-md-3">
                            <Input id="password" label="Mật khẩu" placeholder="Nhập mật khẩu" type="password" />
                        </div>
                        <div class="col-md-3">
                            <Selector
                                id="role-select"
                                label="Phân quyền"
                                options={roles}
                                placeholderText="--Chọn Nhóm Quyền--"
                            />
                        </div>
                        <div class="col-md-3">
                            <Input id="email" label="Thư điện tử" placeholder="Nhập email" type="email" />
                        </div>
                        <div class="col-md-3">
                            <Input id="phone" label="Số điện thoại" placeholder="Nhập số điện thoại" type="phone" />
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
