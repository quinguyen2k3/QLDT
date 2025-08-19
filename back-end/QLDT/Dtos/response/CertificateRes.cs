using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.response
{
    public class CertificateRes
    {
        public long Id { get; set; }
        public string CertificateNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public long? UnitId { get; set; }
        public string UnitName { get; set; }
        public long? ClassId { get; set; }
        public string ClassName { get; set; }
        public List<FileDto> Attachments { get; set; }
    }
}
