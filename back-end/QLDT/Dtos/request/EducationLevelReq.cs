// Dtos/request/EducationLevelReq.cs
namespace QLDT.Dtos.request
{
    public class EducationLevelReq
    {
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}