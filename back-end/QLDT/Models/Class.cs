using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("Classes")]
    public class Class : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        public DateTime ClassNgayBD { get; set; }
        public DateTime ClassNgayKT { get; set; }
        public string? Content { get; set; }
        public int ClassSoTiet { get; set; }
        public int? ClassKinhPhi { get; set; }
        public string ClassSoCVTS{ get; set; }
        public DateTime ClassNgayCVTS { get; set; }
        public string ClassSoQDDH { get; set; }
        public DateTime ClassNgayQDDH { get; set; }
        public string ClassSoQDML { get; set; }
        public DateTime? ClassNgayQDML { get; set; }
        public long? UnitId { get; set; }
        [ForeignKey(nameof(UnitId))]
        public TrainingUnit Unit { get; set; }

        public long? FormatId { get; set; }
        [ForeignKey(nameof(FormatId))]
        public TrainingFormat Format { get; set; }

        public long? CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        public long? LevelId { get; set; }
        [ForeignKey(nameof(LevelId))]
        public EducationLevel Level { get; set; }

        [InverseProperty(nameof(FileClass.Class))]
        public ICollection<FileClass> FileClasses { get; set; }

        [InverseProperty(nameof(CreditHourse.Class))]
        public ICollection<CreditHourse> CreditHours { get; set; }

        [InverseProperty(nameof(Detail.Class))]
        public ICollection<Detail> Details { get; set; }
    }
}