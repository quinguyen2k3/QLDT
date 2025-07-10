// Dtos/response/TrainingUnitRes.cs
namespace QLDT.Dtos.response
{
    public class TrainingUnitRes
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;
    }
}