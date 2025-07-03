namespace QLDT.Model
{
    public class Detail
    {
        public long Id { get; set; }
        public long ClassId { get; set; }
        public Class Class { get; set; }

        public long EmpId { get; set; }
        public Employee Employee { get; set; }
    }
}
