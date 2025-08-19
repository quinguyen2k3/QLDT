using QLDT.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Dtos.request
{
    public class CertificateReq
    {
        [Required]
        public string CertificateNumber { get; set; } = string.Empty;

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public long? UnitId { get; set; }

        [Required]
        public long? ClassId { get; set; }
        public List<IFormFile> Attachments { get; set; } = new();
        public List<string>? OldFileIds { get; set; }
    }
}
