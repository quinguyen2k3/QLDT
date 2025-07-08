// Models/Part.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("Parts")]
    public class Part : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Note { get; set; }

        [InverseProperty(nameof(Department.Part))]
        public ICollection<Department> Departments { get; set; }
    }
}