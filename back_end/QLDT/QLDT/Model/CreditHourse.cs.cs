namespace QLDT.Model
{
    public class CreditHourse
    {
        public long Id { get; set; }
        public long ClassId { get; set; }
        public Class Class { get; set; }
        public int Hour { get; set; }
    }
}
