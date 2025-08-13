// Models/CreditHour.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("CreditHourses")]
    public class CreditHourse : BaseEntity
    {
        [InverseProperty(nameof(Class.Hour))]
        public ICollection<Class> Classes { get; set; }

        public double Hour { get; set; }

        public string Note { get; set; } = string.Empty;
    }
}
