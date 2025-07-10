// Dtos/request/DepartmentReq.cs
namespace QLDT.Dtos.request
{
    public class DepartmentReq
    {
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? PartId { get; set; }
    }
}
