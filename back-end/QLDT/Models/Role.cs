// Models/Role.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("Roles")]
    public class Role
    {
     
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [InverseProperty(nameof(User.Role))]
        public ICollection<User> Users { get; set; }

        [InverseProperty(nameof(RolePermission.Role))]
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
