namespace QLDT.Dtos.response
{
    public class CourseRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Content { get; set; }
        public DateTime CourseNgayKg {  get; set; }
        public DateTime CreatedDate { get; set; }
        public List<FileDto> Attachments { get; set; }
        public long DepId { get; set; }
    }
}
