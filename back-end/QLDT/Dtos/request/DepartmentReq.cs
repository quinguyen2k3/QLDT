using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class DepartmentReq
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        public string? Note { get; set; }

        [Required]
        public long PartId { get; set; }
    }
}