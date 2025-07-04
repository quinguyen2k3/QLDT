using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("Permissions")]
    public class Permission : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}