// Dtos/request/DepartmentReq.cs
using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class DepartmentReq
    {   
        [Required]
        public string Name { get; set; } = null!;
        public string? Note { get; set; }

        [Required]
        public long PartId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
