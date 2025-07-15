namespace QLDT.Dtos.response
{
    public class DepartmentRes
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public string partName { get; set; } = null!;
        public string partId { get; set; }

        public bool IsActive { get; set; }
    }
}