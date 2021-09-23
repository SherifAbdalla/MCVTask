using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCVTask.Models
{
    public class MCVContext : DbContext
    {
        public MCVContext(DbContextOptions options) : base(options)
        {
            
            Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(new List<Department> { new Department { Id = 1, Name = "HR" },
            new Department { Id = 2, Name = "IT" },
            new Department { Id = 3, Name = "Accounting" },
            new Department{ Id = 4, Name = "Finance" },
            new Department{ Id = 5, Name = "Marketing" },
            new Department{ Id = 6, Name = "Sale" } });

            modelBuilder.Entity<Employee>().Property(x => x.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}
