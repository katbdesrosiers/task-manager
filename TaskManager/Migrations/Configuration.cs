namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TaskManager.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            if (db.Roles.Count() == 0)
            {
                Membership.AddRole("developer");
                Membership.AddRole("manager");
                Membership.AddRole("admin");
            }

            if (db.Users.Count() == 0)
            {
                var people = new Dictionary<string, string> {
                {"admin@gmail.com", "admin" },
                {"manager@gmail.com", "manager" },
                {"chows@gmail.com", "developer" },
                {"elizabeth@gmail.com", "developer" },
                {"katherine@gmail.com", "developer" },
                {"braden@gmail.com", "developer" },
            };

                foreach (var person in people)
                {
                    var user = new ApplicationUser
                    {
                        Email = person.Key,
                        UserName = person.Key,
                        EmailConfirmed = true
                    };

                    Membership.CreateUser(user, "P3nguin!");

                    Membership.AddUserToRole(user.Id, person.Value);
                }
            }
        }
    }
}
