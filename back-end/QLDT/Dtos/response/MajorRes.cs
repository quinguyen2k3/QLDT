namespace QLDT.Dtos.response
{
    public class MajorRes
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
