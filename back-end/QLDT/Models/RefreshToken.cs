using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public string Token { get; set; }
        public string JwtId { get; set; }

        public bool IsUsed { get; set; } = false;
        public bool IsRevoked { get; set; } = false;

        public DateTime IssuedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
