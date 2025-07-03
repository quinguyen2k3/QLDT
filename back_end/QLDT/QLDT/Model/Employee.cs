namespace QLDT.Model
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string EmMaCBVC { get; set; }
        public string EmGioiTinh { get; set; }
        public DateTime? EmNgaySinh { get; set; }
        public string EmChucDanh { get; set; }
        public string EmChucVu { get; set; }
        public string EmSDT { get; set; }

        public long? LevelId { get; set; }
        public EducationLevel Level { get; set; }

        public long? DepId { get; set; }
        public Department Department { get; set; }

        public ICollection<Detail> Details { get; set; }
    }
}
