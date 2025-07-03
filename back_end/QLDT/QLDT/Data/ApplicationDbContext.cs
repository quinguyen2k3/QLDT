using Microsoft.EntityFrameworkCore;
using QLDT.Model;   
namespace QLDT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<FileCourse> FileCourses { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<FileClass> FileClasses { get; set; }
        public DbSet<CreditHourse> CreditHourses { get; set; }   
        public DbSet<Detail> Details { get; set; }
        public DbSet<TrainingUnit> TrainingUnits { get; set; }
        public DbSet<TrainingFormat> TrainingFormats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Role–Permission many-to-many
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            // Detail ↔ Class & Employee
            modelBuilder.Entity<Detail>()
                .HasOne(d => d.Class)
                .WithMany(c => c.Details)
                .HasForeignKey(d => d.ClassId);
            modelBuilder.Entity<Detail>()
                .HasOne(d => d.Employee)
                .WithMany(e => e.Details)
                .HasForeignKey(d => d.EmpId);

            // CreditHourse ↔ Class
            modelBuilder.Entity<CreditHourse>()
                .HasOne(ch => ch.Class)
                .WithMany(c => c.CreditHourse)    
                .HasForeignKey(ch => ch.ClassId);
        }
    }
}
