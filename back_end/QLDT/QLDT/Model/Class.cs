namespace QLDT.Model
{
    public class Class : BaseEntity
    {
        public string Name { get; set; }
        public DateTime? ClassNgayBD { get; set; }
        public DateTime? ClassNgayKT { get; set; }
        public string Content { get; set; }
        public int? ClassSoTiet { get; set; }
        public int? ClassKinhPhi { get; set; }
        public string ClassDoiTuong { get; set; }
        public DateTime? ClassNgayCVTS { get; set; }
        public DateTime? ClassNgayQDDH { get; set; }
        public DateTime? ClassNgayQDML { get; set; }

        public long? UnitId { get; set; }
        public TrainingUnit Unit { get; set; }

        public long? FormatId { get; set; }
        public TrainingFormat Format { get; set; }

        public long? CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<FileClass> FileClasses { get; set; }
        public ICollection<CreditHourse> CreditHourse { get; set; }
        public ICollection<Detail> Details { get; set; }
    }
}
