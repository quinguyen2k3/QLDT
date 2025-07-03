namespace QLDT.Model
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
