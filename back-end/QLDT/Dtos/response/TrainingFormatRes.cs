namespace QLDT.Dtos.response
{
    public class TrainingFormatRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
