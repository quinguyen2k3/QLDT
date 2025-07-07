using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class EducationLevelReq
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Note { get; set; }
    }
}