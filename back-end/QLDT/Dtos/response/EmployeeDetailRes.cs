namespace QLDT.Dtos.response
{
    public class EmployeeDetailRes
    {
        public string EmployeeName { get; set; }
        public string EmployeeMaCBVC { get; set; }
        public string EmployeeChucVu { get; set; }
        public string EmployeeChucDanh { get; set; }
        public DateTime EmployeeNgaySinh { get; set; }
        public List<ClassDetailRes> Classes { get; set; }
    }
}
