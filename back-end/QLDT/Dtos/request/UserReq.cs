namespace QLDT.Dtos.request
{
    public class UserReq
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public long? DepId { get; set; }
        public long? RoleId { get; set; }
    }
}
