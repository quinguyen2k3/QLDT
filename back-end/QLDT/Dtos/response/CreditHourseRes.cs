namespace QLDT.Dtos.response
{
    public class CreditHourseRes
    {
        public long Id { get; set; }
        public double Hour { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
