using System.Security.Claims;

namespace QLDT.Model
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public DateTime? CourseNgayKG { get; set; }
        public string Note { get; set; }

        public long? DepId { get; set; }
        public Department Department { get; set; }

        public ICollection<FileCourse> FileCourses { get; set; }
        public ICollection<Class> Classes { get; set; }
    }
}
