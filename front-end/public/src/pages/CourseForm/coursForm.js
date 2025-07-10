import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import {Input, Selector} from '@/components/Form/FormGroup';
import FileInput from '@/components/Form/FormGroup/file'
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';

// Dữ liệu test
const states = [
    { id: 1, name: 'Phòng Công nghệ thông tin' },
    { id: 2, name: 'Khoa dược' },
    { id: 3, name: 'Khoa hô hấp' },
];

function CourseForm() {
    
    const { pageTitle } = useFormMode(
        '/course/update',
        {
        add: 'Thêm Mới Thông Tin Khóa Học',
        edit: 'Thay Đổi Thông Tin Khóa Học',
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
                            <Input id="course-name" label="Tên Đề " />
                        </div>
                        <div class="col-md-3">
                            <Input id="opening-day" label="Ngày Khai Giảng" />
                        </div>
                        <div class="col-md-3">
                            <Selector
                                id="deparment-select"
                                label="Chọn Khoa Phòng"
                                options={states}
                                placeholderText="--Chọn Khoa - Phòng--"
                            />
                        </div>
                    </div>
                    <div className="row">
                        <div class="col-md-6">
                            <Input id="training-content" label="Nội Dung Đào Tạo" />
                        </div>
                        <div class="col-md-3">
                            <Selector
                                id="training-type"
                                label="Chọn Hình Thức Đào"
                                options={states}
                                placeholderText="--Chọn Hình Thức Đào Tạo--"
                            />
                        </div>
                        <div class="col-md-3">
                            <Selector
                                id="education-unit"
                                label="Chọn Đơn Vị Đào Tạo"
                                options={states}
                                placeholderText="--Chọn Đơn Vị Đào Tạo--"
                            />
                        </div>
                    </div>
                    <div className="row">
                        <div class="col-md-6">
                            <Input id="note" label="Ghi Chú" />
                        </div>
                        <div class="col-md-3">
                            <FileInput id="documentUpload" label="Tệp Đính Kèm" multiple={true} />
                        </div>
                    </div>
                </div>
                <FormFooter />
            </div>
            <BackButton />
        </section>
    );
}

export default CourseForm;
