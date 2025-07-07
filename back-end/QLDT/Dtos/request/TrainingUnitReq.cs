using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class TrainingUnitReq
    {
        [Required]
        public string Name { get; set; }

        public string Note { get; set; }
    }
}