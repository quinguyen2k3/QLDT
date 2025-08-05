// Models/CreditHour.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("CreditHourses")]
    public class CreditHourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [InverseProperty(nameof(Class.Hour))]
        public ICollection<Class> Classes { get; set; }

        public int Hour { get; set; }
    }
}
