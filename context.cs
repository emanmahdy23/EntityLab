using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDay5
{
    public class context: DbContext

    {



        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Employee & Department
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)                
                .WithMany(d => d.Employees)               
                .HasForeignKey(e => e.DepartmentID);      

            // Shadow properties
            modelBuilder.Entity<Department>().Property<DateTime>("CreatedDate").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Department>().Property<string>("CreatedBy").HasDefaultValue("eman");


            //seeding data
            // Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { ID = 1, Name = "IT Department" },
                new Department { ID= 2, Name = "HR Department" }
            );

            // Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { ID = 1, Name = "Ahmed Hassan", DepartmentID = 1 },
                new Employee {ID = 2, Name = "Sara Mostafa", DepartmentID = 1 },
                new Employee { ID = 3, Name = "Fatma Adel", DepartmentID = 2 }
            );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-KJV18OK;Database=EntityDay5;Trusted_Connection=True;TrustServerCertificate=True;")
                .LogTo(log => Debug.WriteLine(log), Microsoft.Extensions.Logging.LogLevel.Information)
               .EnableSensitiveDataLogging(); ;
        }
        
    }
}
