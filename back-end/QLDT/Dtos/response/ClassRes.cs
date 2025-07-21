namespace QLDT.Dtos.response
{
    public class ClassRes
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime ClassNgayBD { get; set; }
        public DateTime ClassNgayKT { get; set; }
        public string Content { get; set; } = string.Empty;
        public int ClassSoTiet { get; set; }
        public int ClassKinhPhi { get; set; }
        public string ClassSoCVTS { get; set; } = string.Empty;
        public DateTime ClassNgayCVTS { get; set; }
        public string ClassSoQDML { get; set; } = string.Empty;
        public DateTime ClassNgayQDML { get; set; }
        public string ClassSoQDDH { get; set; } = string.Empty ;
        public DateTime ClassNgayQDDH { get; set; }
        public long CourseId { get; set; }
        public long LevelId { get; set; }
        public long UnitId { get; set; }
        public long FormatId { get; set; }
        public int SoTinhChi { get; set; }
        public List<FileDto> Attachments { get; set; }
        public List<long> EmployeeIds { get; set; } = new();
        public bool isActive { get; set; }
    }
}
