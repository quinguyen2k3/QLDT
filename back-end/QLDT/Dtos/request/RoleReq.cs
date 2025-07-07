using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.request
{
    public class RoleReq
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
    }
}