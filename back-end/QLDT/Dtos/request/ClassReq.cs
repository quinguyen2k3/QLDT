using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class ClassReq
    {
        [Required]
        public string Name {  get; set; } = string.Empty;

        [Required]
        public DateTime ClassNgayBD { get; set; }

        [Required]
        public DateTime ClassNgayKT { get; set; }
        public string Content { get; set; } = string.Empty ;

        [Required]
        public int ClassSoTiet { get; set; }

        [Required]
        public int ClassKinhPhi { get; set; }

        [Required]
        public string ClassSoCVTS { get; set; } = string.Empty;

        [Required]
        public  DateTime  ClassNgayCVST { get; set; }

        [Required]
        public string ClassSoQDDH { get; set; } = String.Empty;

        [Required]
        public DateTime  ClassNgayQDDH { get; set; }

        [Required]
        public string ClassSoQDML { get; set; } = string.Empty;

        [Required]
        public DateTime ClassNgayQDML  { get; set; }
        public long? CourseId { get; set; }

        [Required]
        public long LevelId { get; set; }

        [Required]
        public long UnitId { get; set; }

        [Required]
        public long FormatId { get; set; }

        [Required]
        public long MajorId { get; set; }

        [Required]
        public double SoTinhChi { get; set; }
        public List<IFormFile> Attachments { get; set; } = new();
        public List<string>? OldFileIds { get; set; }
        public List<long> EmployeeIds { get; set; } = new();
        public bool isActive { get; set; } = false;

    }
}
