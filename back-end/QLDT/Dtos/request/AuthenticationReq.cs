using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class AuthenticationReq
    {
        [Required]
        public string username {  get; set; }

        [Required]
        public string password { get; set; }
    }
}
