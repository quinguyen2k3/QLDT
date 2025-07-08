using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("TrainingUnits")]
    public class TrainingUnit : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Note { get; set; }

        [InverseProperty(nameof(Class.Unit))]
        public ICollection<Class> Classes { get; set; }
    }
}