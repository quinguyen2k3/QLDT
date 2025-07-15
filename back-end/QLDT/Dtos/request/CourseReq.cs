using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class CourseReq
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string CourseNgayKg { get; set; }

        public string? Note { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedDate {  get; set; }

        public List<IFormFile> Attachments { get; set; } = new();

        public List<string> OldFileIds { get; set; }

        public long DepId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
