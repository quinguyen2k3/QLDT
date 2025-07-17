import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { authApi } from '@/service/apis';
import { setTokens } from '@/service/authService';
import { useAuth } from '@/contexts';
//Toast
import { toast } from 'react-toastify';

function Login() {
    const navigate = useNavigate();

    const [formData, setFormData] = useState({ username: '', password: '' });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    const { login } = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();

        const { username, password } = formData;

        if (username === '' || password === '') {
            toast.warning('Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu');
            return;
        }

        try {
            const response = await authApi.login({
                username,
                password,
            });

            const { accessToken, refreshToken } = response.data;
            setTokens(accessToken, refreshToken);

            login();

            navigate('/home');
        } catch (error) {
            console.log('Error object:', error); // Log the entire error
            console.log('Error response:', error.response); // Log the response
            console.log('Error response data:', error.response?.data); // Log the data
            console.log('Error status:', error.response?.status); // Log the status
            console.log('Error message:', error.response?.data?.message); // Log the message

            if (error.response?.data?.message === 'Unauthenticated' && error.response?.status === 401) {
                toast.error('Sai tên đăng nhập hoặc mật khẩu');
            } else {
                toast.error('Lỗi quá trình đăng nhập');
                console.error(error);
            }
        }
    };

    return (
        <>
            <div className="card card-outline card-success">
                <div className="card-header text-center">
                    <img
                        src="/dist/img/logoLeVanThinhcircle.png"
                        alt="Logo bệnh viện"
                        style={{ height: '100px', marginRight: '20px' }}
                    />
                    <h2 className="mb-0">
                        <span className="fw-bold">QLĐT</span> Lê Văn Thịnh
                    </h2>
                </div>
                <div className="card-body">
                    <p className="login-box-msg">Đăng nhập hệ thống Quản lý Đào Tạo</p>
                    <form onSubmit={handleSubmit}>
                        <div className="input-group mb-3">
                            <input
                                type="text"
                                name="username"
                                className="form-control"
                                placeholder="Tên đăng nhập"
                                value={formData.username}
                                onChange={handleChange}
                            />
                            <div className="input-group-append">
                                <div className="input-group-text">
                                    <span className="fas fa-user" />
                                </div>
                            </div>
                        </div>
                        <div className="input-group mb-3">
                            <input
                                type="password"
                                name="password"
                                className="form-control"
                                placeholder="Mật khẩu"
                                value={formData.password}
                                onChange={handleChange}
                            />
                            <div className="input-group-append">
                                <div className="input-group-text">
                                    <span className="fas fa-lock" />
                                </div>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-8">
                                <div className="icheck-success">
                                    <input type="checkbox" id="remember" />
                                    <label htmlFor="remember">Ghi nhớ mật khẩu</label>
                                </div>
                            </div>
                            <div className="col-5">
                                <button type="submit" className="btn btn-success btn-block">
                                    Đăng nhập
                                </button>
                            </div>
                        </div>
                    </form>
                    <p className="mb-1">
                        <a href="#">Tôi đã quên mật khẩu</a>
                    </p>
                </div>
            </div>
        </>
    );
}

export default Login;
