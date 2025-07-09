import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { authApi } from '@/service/apis';
import { setTokens } from '@/service/authService';
import { useAuth } from '@/contexts';

function Login() {
    const navigate = useNavigate();

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const { login } = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');

        try {
            const response = await authApi.login({
                username,
                password,
            });

            const { accessToken, refreshToken } = response.data;
            setTokens(accessToken, refreshToken);

            login();

            navigate("/home");
        } catch (err) {
            console.error(err);
            setError('Tên đăng nhập hoặc mật khẩu không đúng.');
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
                                className="form-control"
                                placeholder="Tên đăng nhập"
                                value={username}
                                onChange={(e) => setUsername(e.target.value)}
                                required
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
                                className="form-control"
                                placeholder="Mật khẩu"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
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
