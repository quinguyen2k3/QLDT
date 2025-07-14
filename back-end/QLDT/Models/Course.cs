// Models/Course.cs
using QLDT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("Courses")]
    public class Course : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        public DateTime? CourseNgayKG { get; set; }
        public string? Note { get; set; }

        public string? Content { get; set; }

        public long DepId { get; set; }
        [ForeignKey(nameof(DepId))]
        public Department Department { get; set; }

        [InverseProperty(nameof(FileCourse.Course))]
        public ICollection<FileCourse> FileCourses { get; set; }

        [InverseProperty(nameof(Class.Course))]
        public ICollection<Class> Classes { get; set; }
    }
}