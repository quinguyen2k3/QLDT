using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("FileCertificates")]
    public class FileCertificate
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string FileName { get; set; }
        public string Path { get; set; }

        public long CertificateId { get; set; }
        [ForeignKey(nameof(CertificateId))]
        public Certificate Certificate { get; set; }
    }
}
