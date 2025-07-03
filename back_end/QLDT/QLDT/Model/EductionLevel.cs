namespace QLDT.Model
{
    public class EducationLevel : BaseEntity
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
