using HR_management_project.Model;
using Microsoft.EntityFrameworkCore;

namespace HR_management_project.Data.Stores.SqlLiteStore
{
    public class ApplicationDbContext : DbContext
    {
        // make it match your project model

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HR.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().OwnsOne(e => e.Salary, sa =>
            {
                sa.Property(s => s.BaseSalary).HasColumnName("BaseSalary");
                sa.Property(s => s.Deduction).HasColumnName("Deduction");
                sa.Property(s => s.Bonus).HasColumnName("Bonus");
            });
        }
    }
}
