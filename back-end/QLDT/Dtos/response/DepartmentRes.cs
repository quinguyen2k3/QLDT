using System;

namespace QLDT.Dtos.response
{
    public class DepartmentRes
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Note { get; set; }

        public long PartId { get; set; }

        public int CreatedById { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}