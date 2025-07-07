using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class PartReq
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Note { get; set; }
    }
}