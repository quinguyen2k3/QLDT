// Models/EducationLevel.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("EducationLevels")]
    public class EducationLevel : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Note { get; set; }

        [InverseProperty(nameof(Employee.Level))]
        public ICollection<Employee> Employees { get; set; }
    }
}