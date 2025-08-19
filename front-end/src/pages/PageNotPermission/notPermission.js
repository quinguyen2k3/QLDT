import { Link } from 'react-router-dom';

function PageNotPermission() {
    return (
        <>
            <div>
                <section className="content-header">
                    <div className="container-fluid">
                        <div className="row mb-2">
                            <div className="col-sm-6">
                                <h1>Không Có Quyền Truy Cập</h1>
                            </div>
                        </div>
                    </div>
                </section>
                <section className="content">
                    <div className="error-page">
                        <h2 className="headline text-danger">403</h2>
                        <div className="error-content">
                            <h3>
                                <i className="fas fa-exclamation-circle text-danger" /> Ôi không! Bạn không có quyền truy cập trang này.
                            </h3>
                            <p>
                                Bạn không có đủ quyền để xem nội dung này. Vui lòng liên hệ quản trị viên hoặc{' '}
                                <Link to="/home">quay lại trang chính</Link> để tiếp tục.
                            </p>
                        </div>
                    </div>
                </section>
            </div>
        </>
    );
}

export default PageNotPermission;