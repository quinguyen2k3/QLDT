import { Link } from 'react-router-dom';

function Page404() {
    return (
        <>
            <div>
                <section className="content-header">
                    <div className="container-fluid">
                        <div className="row mb-2">
                            <div className="col-sm-6">
                                <h1>Không tìm thấy trang - 404</h1>
                            </div>
                        </div>
                    </div>
                    {/* /.container-fluid */}
                </section>
                {/* Main conten */}
                <section className="content">
                    <div className="error-page">
                        <h2 className="headline text-warning">404</h2>
                        <div className="error-content">
                            <h3>
                                <i className="fas fa-exclamation-triangle text-warning" /> Ôi không! Không tìm thấy
                                trang.
                            </h3>
                            <p>
                                Trang bạn yêu cầu không tồn tại hoặc đã bị di chuyển. Bạn có thể{' '}
                                <Link to="/home">quay lại trang chính</Link> để tiếp tục.
                            </p>
                        </div>
                        {/* /.error-content */}
                    </div>
                    {/* /.error-page */}
                </section>
            </div>
        </>
    );
}

export default Page404;
