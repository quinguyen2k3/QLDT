using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLDT.Models
{
    [Table("RolePermissions")]
    public class RolePermission
    {
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Role))]

        public long RoleId { get; set; }

        public Role Role { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Permission))]

        public long PermissionId { get; set; }

        public Permission Permission { get; set; }
    }
}