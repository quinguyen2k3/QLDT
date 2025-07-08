using Microsoft.EntityFrameworkCore;
using QLDT.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security;

namespace QLDT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<InvalidToken> InvalidTokens { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<RolePermission> RolePermissions { get; set; } = null!;
        public DbSet<Part> Parts { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<EducationLevel> EducationLevels { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<FileCourse> FileCourses { get; set; } = null!;
        public DbSet<Class> Classes { get; set; } = null!;
        public DbSet<FileClass> FileClasses { get; set; } = null!;
        public DbSet<CreditHourse> CreditHourses { get; set; } = null!;
        public DbSet<Detail> Details { get; set; } = null!;
        public DbSet<TrainingUnit> TrainingUnits { get; set; } = null!;
        public DbSet<TrainingFormat> TrainingFormats { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key cho RolePermission
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            // Quan hệ RolePermission ↔ Role, Permission
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            // User ↔ Department, Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepId);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Department ↔ Part
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Part)
                .WithMany(p => p.Departments)
                .HasForeignKey(d => d.PartId);

            // Employee ↔ EducationLevel, Department
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Level)
                .WithMany(l => l.Employees)
                .HasForeignKey(e => e.LevelId);
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepId);

            // Detail ↔ Class, Employee
            modelBuilder.Entity<Detail>()
                .HasOne(d => d.Class)
                .WithMany(c => c.Details)
                .HasForeignKey(d => d.ClassId);
            modelBuilder.Entity<Detail>()
                .HasOne(d => d.Employee)
                .WithMany(e => e.Details)
                .HasForeignKey(d => d.EmpId);

            // CreditHour ↔ Class
            modelBuilder.Entity<CreditHourse>()
                .HasOne(ch => ch.Class)
                .WithMany(c => c.CreditHours)
                .HasForeignKey(ch => ch.ClassId);

            // FileCourse ↔ Course
            modelBuilder.Entity<FileCourse>()
                .HasOne(fc => fc.Course)
                .WithMany(c => c.FileCourses)
                .HasForeignKey(fc => fc.CourseId);

            // Class ↔ Course, TrainingUnit, TrainingFormat
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Course)
                .WithMany(co => co.Classes)
                .HasForeignKey(c => c.CourseId);
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Unit)
                .WithMany(u => u.Classes)
                .HasForeignKey(c => c.UnitId);
            modelBuilder.Entity<Class>()
                .HasOne(c => c.Format)
                .WithMany(f => f.Classes)
                .HasForeignKey(c => c.FormatId);

            // FileClass ↔ Class
            modelBuilder.Entity<FileClass>()
                .HasOne(fc => fc.Class)
                .WithMany(c => c.FileClasses)
                .HasForeignKey(fc => fc.ClassId);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId);
        }
    }
}