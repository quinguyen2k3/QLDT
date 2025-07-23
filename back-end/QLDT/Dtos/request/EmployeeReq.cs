using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class EmployeeReq
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string EmMaCBVC { get; set; } = string.Empty;

        [Required]
        public string EmGioiTinh { get; set; } = string.Empty;

        [Required]
        public DateTime EmNgaySinh { get; set; }

        [Required]
        public string EmChucDanh { get; set; } = string.Empty;

        [Required]
        public string EmChucVu { get; set; } = string.Empty;

        [Required]
        public string EmSDT {  get; set; } = string.Empty;
 
        [Required]
        public long DepId { get; set; }

        [Required]
        public long LevelId { get; set; }

        [Required]
        public long MajorId { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}
