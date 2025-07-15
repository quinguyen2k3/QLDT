namespace QLDT.Dtos.response
{
    public class TrainingFormatRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedDate { get; set; }

        public bool IsActive { get; set; }

    }
}
