namespace QLDT.Model
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Note { get; set; }

        public long? PartId { get; set; }
        public Part Part { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
