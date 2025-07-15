using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class TrainingFormatReq
    {
        [Required]
        public string Name { get; set; }
        public string Note { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
