import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import {Input, Selector} from '@/components/Form/FormGroup';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';

// Dữ liệu test
const states = [
    { id: 1, name: 'Phòng Công nghệ thông tin' },
    { id: 2, name: 'Khoa dược' },
    { id: 3, name: 'Khoa hô hấp' },
];

function DepartmentForm() {
    
    const { pageTitle } = useFormMode(
        '/department/update',
        {
        add: 'Thêm Mới Thông Tin Khoa Phòng',
        edit: 'Thay Đổi Thông Tin Khoa Phòng',
        }
    );

    return (
        <section className="content">
            <PageHeader title={pageTitle} />
            <div className="card card-default">
                <FormHeader title="Bảng thông tin" />
                <div className="card-body">
                    <div className="row">
                        <div class="col-md-6">
                            <Input name="Name" id="department-name" label="Tên Khoa Phòng" />
                        </div>
                        <div class="col-md-6">
                            <Selector
                                id="part-select"
                                name="PartId"
                                label="Chọn Khoa Phòng"
                                options={states}
                                placeholderText="--Chọn Bộ Phận--"
                            />
                        </div>
                    </div>
                    <div className="row">
                        <div class="col-md-6">
                            <Input name="Note" id="note" label="Nội Dung Đào Tạo" />
                        </div>
                        <div class="col-md-6">
                            <Input name="CreatedDate" type="date" id="created_date" label="Ngày Tạo"/>
                        </div>                     
                  </div>
                </div>
                <FormFooter />
            </div>
            <BackButton />
        </section>
    );
}

export default DepartmentForm;
