namespace QLDT.Model
{
    public class ClassDetail
    {
        public long ClassID { get; set; }
        public Class Class { get; set; }

        public long EmpID { get; set; }
        public Employee Employee { get; set; }
    }
}
