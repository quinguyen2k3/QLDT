using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.response
{
    public class EmployeeRes
    {   
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmMaCBVC { get; set; } = string.Empty;
        public string EmGioiTinh { get; set; } = string.Empty;
        public DateTime EmNgaySinh { get; set; }
        public string EmChucDanh { get; set; } = string.Empty;
        public string EmChucVu { get; set; } = string.Empty;
        public string EmSDT { get; set; } = string.Empty;
        public long DepId { get; set; }
        public string DepName { get; set; } = string.Empty ;
        public long LevelId { get; set; }
        public string LevelName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
