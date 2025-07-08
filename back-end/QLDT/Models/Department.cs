using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("Departments")]
    public class Department : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        public string? Note { get; set; }

        public long? PartId { get; set; }
        public Part? Part { get; set; }

        [Required]
        public int CreatedById { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        
        public ICollection<User>? Users { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
