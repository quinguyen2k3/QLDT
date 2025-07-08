namespace QLDT.Dtos.response
{
    public class UserRes
    {   
        public long id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public long? DepId { get; set; }
        public long? RoleId { get; set; }
    }
}
