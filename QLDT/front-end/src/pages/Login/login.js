import React from 'react';

function Login() {
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
                    <form>
                        <div className="input-group mb-3">
                            <input type="email" className="form-control" placeholder="Email" />
                            <div className="input-group-append">
                                <div className="input-group-text">
                                    <span className="fas fa-user" />
                                </div>
                            </div>
                        </div>
                        <div className="input-group mb-3">
                            <input type="password" className="form-control" placeholder="Password" />
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