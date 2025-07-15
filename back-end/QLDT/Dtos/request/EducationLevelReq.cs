// Dtos/request/EducationLevelReq.cs
using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class EducationLevelReq
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Note { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}