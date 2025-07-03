namespace QLDT.Model
{
    public class Part : BaseEntity
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
