import PageHeader from '@/components/PageHeader';
import FormHeader from '@/components/Form/FormHeader';
import { Input } from '@/components/Form/FormGroup';
import FormFooter from '@/components/Form/FormFooter';
import BackButton from '@/components/BackButton';
import useFormMode from '@/hooks/FormMode';

function ELevelForm() {
    const { pageTitle } = useFormMode('/elevel/update', {
        add: 'Thêm Mới Thông Tin Trình Độ Đào Tạo',
        edit: 'Thay Đổi Thông Tin Trình Độ Đào Tạo',
    });

    return (
        <section className="content">
            <PageHeader title={pageTitle} />
            <div className="card card-default">
                <FormHeader title="Bảng thông tin" />
                <div className="card-body">
                    <div className="row">
                        <div class="col-md-4">
                            <Input name="Name" id="elevel-name" label="Tên Trình Độ Đào Tạo" />
                        </div>
                        <div class="col-md-4">
                            <Input name="Note" id="note" label="Ghi Chú" />
                        </div>
                        <div class="col-md-4">
                            <Input name="CreatedDate" type="date" id="created_date" label="Ngày Tạo" />
                        </div>
                    </div>
                </div>
                <FormFooter />
            </div>
            <BackButton />
        </section>
    );
}

export default ELevelForm;
