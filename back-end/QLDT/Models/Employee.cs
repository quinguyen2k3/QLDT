// Models/Employee.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDT.Models
{
    [Table("Employees")]
    public class Employee : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string EmMaCBVC { get; set; }

        public string EmGioiTinh { get; set; }
        public DateTime EmNgaySinh { get; set; }
        public string EmChucDanh { get; set; }
        public string EmChucVu { get; set; }
        public string EmSDT { get; set; }

        public bool IsActive { get; set; }

        public long? LevelId { get; set; }
        [ForeignKey(nameof(LevelId))]
        public EducationLevel Level { get; set; }

        public long? DepId { get; set; }
        [ForeignKey(nameof(DepId))]
        public Department Department { get; set; }

        [InverseProperty(nameof(Detail.Employee))]
        public ICollection<Detail> Details { get; set; }
    }
}