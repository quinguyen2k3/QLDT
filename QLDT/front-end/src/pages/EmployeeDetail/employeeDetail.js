import PageHeader from '@/components/PageHeader';
import DataTable from '@/components/DataTable';
import BackButton from '@/components/BackButton';

function EmployeeDetail() {
    //Dữ liệu giả lập
    const dataFromApi = [
        {
            id: 1,
            employeeName: 'Lê Hoàng Nam Việt',
            courseTitle: 'Học tập tư tưởng Hồ Chí Minh',
            courseContent: 'Học tập tư tưởng Hồ Chí Minh',
            hours: 6.0,
        },
        {
            id: 2,
            employeeName: 'Lê Hoàng Nam Việt',
            courseTitle: 'Lớp Điện Thoại',
            courseContent: 'Tập huấn sử dụng máy tính',
            hours: 3.0,
        },
    ];

    //Map label từ api sang tên khác
    const labelMap = {
        employeeName: 'Tên Nhân Viên',
        courseTitle: 'Tên Khóa Học',
        courseContent: 'Nội Dung Khóa Học',
        hours: 'Số Tiết',
    };

    return (
        <section className="content">
            <PageHeader title="Chi Tiết Thông Tin Nhân Viên" />
            <section className="content">
                <div className="container-fluid">
                    <div className="row">
                        <div className="col-md-3">
                            {/* Phần thông tin cơ bản của nhân viên */}
                            <div className="card card-infor mb-3">
                                <div
                                    className="card-header bg-white text-center border-bottom"
                                    style={{ borderTop: '4px solid #28a745' }}
                                >
                                    <h5 className="mb-1 font-weight-bold">Lê Hoàng Nam Việt</h5>
                                    <div className="text-muted h6 mb-0">19529705</div>
                                </div>

                                <div className="card-body">
                                    <div className="d-flex justify-content-between border-bottom py-2">
                                        <strong>Mã chức danh:</strong>
                                        <span className="text-primary">V.05.02.08</span>
                                    </div>

                                    <div className="d-flex justify-content-between border-bottom py-2">
                                        <strong>Ngày-Tháng-Năm-Sinh:</strong>
                                        <span className="text-primary">29/12/1988</span>
                                    </div>

                                    <div className="d-flex justify-content-between py-2">
                                        <strong>Khoa-Phòng:</strong>
                                        <span className="text-primary">Phòng Công Nghệ Thông Tin</span>
                                    </div>
                                </div>
                            </div>
                            
                            {/* Số tiết tích lũy được của nhân viên */}
                            <div className="card mb-3">
                                <div className="card-header bg-success text-white font-weight-bold">
                                    Chi Tiết Quá Trình
                                </div>
                                <div className="card-body d-flex align-items-center">
                                    <i className="bi bi-journal-bookmark-fill mr-2"></i>
                                    <p className="mb-0">
                                        <strong>Tổng số Tích Lũy: </strong>
                                        <span className="text-danger font-weight-bold">9</span> Tiết học.
                                    </p>
                                </div>
                            </div>
                        </div>

                        {/* Phần danh sách các lớp học mà nhân viên đã tham gia */}
                        <div class="col-md-9">
                            <div className="card card-infor mb-3">
                                <div
                                    className="card-header bg-white border-bottom"
                                    style={{ borderTop: '4px solid #28a745' }}
                                >
                                    <button className="btn btn-primary font-weight-bold">
                                        Tổng Hợp Danh Sách Khoá Học
                                    </button>
                                </div>
                                <div className="card-body">
                                    <DataTable title="Chi tiết khóa học" 
                                    data={dataFromApi} 
                                    columnMap={labelMap}
                                    showActions = {false}  />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <BackButton />
        </section>
    );
}
export default EmployeeDetail;
