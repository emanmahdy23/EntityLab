using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace EntityDay5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            context context = new context();

            Console.WriteLine("----------------> Eager Loading");
            var departmentWithEmployees = context.Departments
               .Include(d => d.Employees)
               .FirstOrDefault();

            Console.WriteLine("Department Name: " + departmentWithEmployees.Name);


            Console.WriteLine("----------------------> explicit Loading");
            var department = context.Departments.FirstOrDefault(d => d.ID == 1);
            if (department != null)
            {
                context.Entry(department).Collection(d => d.Employees).Load();
                Console.WriteLine("Department Name: " + department.Name);
                foreach (var employee in department.Employees)
                {
                    Console.WriteLine("Employee Name: " + employee.Name);
                }
            }
            else
            {
                Console.WriteLine("Department not found.");
            }

            Console.WriteLine("----------------------> Lazy Loading");
            var department1 = context.Departments.First();
            var employees = department1.Employees; 
            Console.WriteLine("Department Name: " + department1.Name);


            Console.WriteLine("----------------------> Without Tracking");
            var departments = context.Departments
                .AsNoTracking()
                .ToList();

            foreach (var dep in departments)
            {
                Console.WriteLine("Department ID: " + dep.ID + ", Name: " + dep.Name);
            }
            Console.WriteLine("----------------------> NoTrackingWithIdentityResolution");

            var departmentsWithIdentityResolution = context.Departments
                .AsNoTrackingWithIdentityResolution()
                .ToList();
            foreach (var dep in departmentsWithIdentityResolution)
            {
                Console.WriteLine("Department ID: " + dep.ID + ", Name: " + dep.Name);
            }

            Console.WriteLine("----------------------> debugview");
            var debugView = context.Model.ToDebugString(MetadataDebugStringOptions.LongDefault);
            Console.WriteLine(debugView);

        }
    }
}
