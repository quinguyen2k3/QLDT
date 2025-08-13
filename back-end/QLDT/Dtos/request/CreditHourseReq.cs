using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class CreditHourseReq
    {
        [Required]
        public double Hour { get; set; }
        public string? Note { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
