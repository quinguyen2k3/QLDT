using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("Certificates")]
    public class Certificate : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string CertificateNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        [Required]
        public long? UnitId { get; set; }
        [ForeignKey(nameof(UnitId))]
        public TrainingUnit Unit { get; set; }

        [Required]
        public long? EmpId { get; set; }

        [ForeignKey(nameof(EmpId))]
        public Employee Employee { get; set; }

        [Required]
        public long ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; }

        [InverseProperty(nameof(FileCertificate.Certificate))]
        public ICollection<FileCertificate> FileCertificates { get; set; }
    }
}
