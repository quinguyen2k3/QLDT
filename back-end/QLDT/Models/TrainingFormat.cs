// Models/TrainingFormat.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("TrainingFormats")]
    public class TrainingFormat : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Note { get; set; }

        [InverseProperty(nameof(Class.Format))]
        public ICollection<Class> Classes { get; set; }
    }
}
