using System.Security.Claims;

namespace QLDT.Model
{
    public class FileClass
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }

        public long ClassId { get; set; }
        public Class Class { get; set; }
    }
}
