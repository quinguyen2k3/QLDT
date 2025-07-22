using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class ChangePasswordReq
    {
        [Required]
        public string Password { get; set; }
    }
}
