using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.response
{
    public class UserRes
    {   
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public long? DepId { get; set; }
        public long EmpId { get; set; }
        public long? RoleId { get; set; }        
        public string RoleName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
