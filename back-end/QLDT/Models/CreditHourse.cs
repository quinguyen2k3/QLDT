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

        public long ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]

        public Class Class { get; set; }

        public int Hour { get; set; }
    }
}
