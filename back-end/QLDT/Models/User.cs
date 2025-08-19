using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(20)]
        public string Username { get; set; } = null!;

        [Required, MaxLength(200)]
        public string Password { get; set; } = null!;

        [MaxLength(200), EmailAddress]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        public long? DepId { get; set; }
        public Department? Department { get; set; }

        public long? RoleId { get; set; }
        public bool IsActive { get; set; } = true;
        public long? EmpId { get; set; }
        [ForeignKey(nameof(EmpId))]
        public Employee Employee { get; set; }
        public Role? Role { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
