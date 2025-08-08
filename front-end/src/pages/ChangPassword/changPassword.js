import { useState } from 'react';
import PageHeader from '@/components/PageHeader';
import BackButton from '@/components/BackButton';
import { authApi } from '@/service/apis';
import { toast } from 'react-toastify';

function ChangePassword() {
    const [form, setForm] = useState({
        newPassword: '',
        confirmPassword: '',
    });

    const handleChange = (e) => {
        const { id, value } = e.target;
        setForm((prev) => ({ ...prev, [id]: value }));
    };

    const handleReset = () => {
        setForm({
            newPassword: '',
            confirmPassword: '',
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!form.newPassword || !form.confirmPassword) {
            toast.warning('Vui lòng nhập đầy đủ thông tin');
            return;
        }
        if (form.newPassword !== form.confirmPassword) {
            toast.warning('Mật khẩu không khớp');
            return;
        }
        try {
            await authApi.changePassword({ password: form.newPassword });
            toast.success('Đổi mật khẩu thành công');
            handleReset(); // Reset form sau khi đổi mật khẩu thành công
        } catch (error) {
            toast.error('Đổi mật khẩu thất bại');
            console.error(error);
        }
    };

    return (
        <section className="content">
            <PageHeader title="Đổi Mật Khẩu Tài Khoản" />
            <div className="card card-info">
                <div className="card-header bg-white" style={{ borderTop: '4px solid #28a745', borderBottom: 'none' }}>
                    <h3 className="card-title">Bảng Thông Tin</h3>
                </div>
                <form className="form-horizontal" onSubmit={handleSubmit}>
                    <div className="card-body">
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
                                    value={form.newPassword}
                                    onChange={handleChange}
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
                                    value={form.confirmPassword}
                                    onChange={handleChange}
                                />
                            </div>
                        </div>
                    </div>
                    <div className="card-footer">
                        <button type="submit" className="btn btn-success">
                            Đổi mật khẩu
                        </button>
                        <button type="button" className="btn btn-secondary float-right" onClick={handleReset}>
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