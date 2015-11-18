using MvcEmployee.Models;

namespace MvcEmployee.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcEmployee.Models.EmployeeDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcEmployee.Models.EmployeeDBContext context)
        {
            context.Employees.Add(new Employee(){FirstName = "Rizwan",LastName = "Rumi",Email = "rumi@gmail.com",Gender = "male",ImagePath = ""});
            context.SaveChanges();
        }
    }
}
