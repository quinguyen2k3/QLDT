namespace QLDT.Model
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public long? DepId { get; set; }
        public Department Department { get; set; }

        public long? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
