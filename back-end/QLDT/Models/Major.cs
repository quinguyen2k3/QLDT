using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("Major")]
    public class Major : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Note { get; set; }

        [InverseProperty(nameof(Employee.Major))]
        public ICollection<Employee> Employees { get; set; }

        [InverseProperty(nameof(Class.Major))]
        public ICollection<Class> Classes { get; set; }
    }
}
