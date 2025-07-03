namespace QLDT.Model
{
    public class FileCourse
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }

        public long CourseId { get; set; }
        public Course Course { get; set; }
    }
}
