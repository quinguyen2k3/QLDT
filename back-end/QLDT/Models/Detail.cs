// Models/Detail.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("Details")]
    public class Detail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public double SoTinhChi { get; set; }
        public long ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; }
        public long EmpId { get; set; }
        [ForeignKey(nameof(EmpId))]
        public Employee Employee { get; set; }
    }
}