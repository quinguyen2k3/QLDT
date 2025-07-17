using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class PartReq
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Note { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}