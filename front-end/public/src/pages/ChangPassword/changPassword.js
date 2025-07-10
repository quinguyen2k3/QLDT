import PageHeader from '@/components/PageHeader';
import BackButton from '@/components/BackButton';

function ChangePassword() {
    return (
        <section className="content">
            <PageHeader title="Đổi Mật Khẩu Tài Khoản" />

            <div className="card card-info">
                <div className="card-header bg-white" style={{ borderTop: '4px solid #28a745', borderBottom: 'none' }}>
                    <h3 className="card-title">Bảng Thông Tin</h3>
                </div>

                <form className="form-horizontal">
                    <div className="card-body">
                        <div className="form-group row">
                            <label htmlFor="currentPassword" className="col-sm-2 col-form-label">
                                Mật khẩu hiện tại
                            </label>
                            <div className="col-sm-6">
                                <input
                                    type="password"
                                    className="form-control"
                                    id="currentPassword"
                                    placeholder="Nhập mật khẩu hiện tại"
                                />
                            </div>
                        </div>

                        <div className="form-group row">
                            <label htmlFor="newPassword" className="col-sm-2 col-form-label">
                                Mật khẩu mới
                            </label>
                            <div className="col-sm-6">
                                <input
                                    type="password"
                                    className="form-control"
                                    id="newPassword"
                                    placeholder="Nhập mật khẩu mới"
                                />
                            </div>
                        </div>

                        <div className="form-group row">
                            <label htmlFor="confirmPassword" className="col-sm-2 col-form-label">
                                Nhập lại mật khẩu
                            </label>
                            <div className="col-sm-6">
                                <input
                                    type="password"
                                    className="form-control"
                                    id="confirmPassword"
                                    placeholder="Nhập lại mật khẩu mới"
                                />
                            </div>
                        </div>
                    </div>

                    <div className="card-footer">
                        <button type="submit" className="btn btn-success">
                            Đổi mật khẩu
                        </button>
                        <button type="reset" className="btn btn-secondary float-right">
                            Hủy
                        </button>
                    </div>
                </form>
            </div>
        <BackButton />
        </section>
    );
}

export default ChangePassword;
