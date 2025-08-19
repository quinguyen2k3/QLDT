using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class UserReq
    {   
        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        [Required]
        public long DepId { get; set; }

        [Required]
        public long RoleId { get; set; }

        [Required]
        public long EmpId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
