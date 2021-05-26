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

            // Roles

            if (db.Roles.Count() == 0)
            {
                Membership.AddRole("developer");
                Membership.AddRole("manager");
            }

            // Users

            if (db.Users.Count() == 0)
            {
                var people = new Dictionary<string, string> {
                {"manager@gmail.com", "manager" },
                {"manager2@gmail.com", "manager" },
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
                        EmailConfirmed = true,
                        Salary = 400
                    };

                    Membership.CreateUser(user, "P3nguin!");

                    Membership.AddUserToRole(user.Id, person.Value);
                }
            }

            // Projects

            if (db.Projects.Count() == 0 && db.Tasks.Count() == 0)
            {
                Project project1 = new Project
                {
                    Name = "Test Project 1",
                    Deadline = DateTime.Now.AddDays(20),
                    Priority = Priority.High,
                    Manager = db.Users.First(u => u.Email == "manager@gmail.com"),
                    Budget = 5000
                };
                Project project2 = new Project
                {
                    Name = "Test Project 2",
                    Deadline = DateTime.Now.AddDays(10),
                    Priority = Priority.Low,
                    Manager = db.Users.First(u => u.Email == "manager@gmail.com"),
                    Budget = 2200
                };
                Project project3 = new Project
                {
                    Name = "Test Project 3",
                    Deadline = DateTime.Now.AddDays(60),
                    Priority = Priority.High,
                    Manager = db.Users.First(u => u.Email == "manager@gmail.com"),
                    Budget = 15000
                };
                Project project4 = new Project
                {
                    Name = "Test Project 4",
                    Deadline = DateTime.Now.AddDays(45),
                    Priority = Priority.Low,
                    Manager = db.Users.First(u => u.Email == "manager@gmail.com"),
                    Budget = 11000
                };

                // Project Tasks

                ProjectTask projectTask1 = new ProjectTask
                {
                    Name = "Test Task 1",
                    Deadline = DateTime.Now.AddDays(10),
                    Priority = Priority.Low,
                    Project = project1,
                    DeveloperID = db.Users.First(u => u.Email == "chows@gmail.com").Id
                };
                ProjectTask projectTask2 = new ProjectTask
                {
                    Name = "Test Task 2",
                    Deadline = DateTime.Now.AddDays(15),
                    Priority = Priority.High,
                    Project = project1,
                    DeveloperID = db.Users.First(u => u.Email == "katherine@gmail.com").Id
                };
                ProjectTask projectTask3 = new ProjectTask
                {
                    Name = "Test Task 3",
                    Deadline = DateTime.Now.AddDays(5),
                    Priority = Priority.Low,
                    Project = project2,
                    DeveloperID = db.Users.First(u => u.Email == "chows@gmail.com").Id
                };
                ProjectTask projectTask4 = new ProjectTask
                {
                    Name = "Test Task 4",
                    Deadline = DateTime.Now.AddDays(3),
                    Priority = Priority.High,
                    Project = project2,
                    DeveloperID = db.Users.First(u => u.Email == "katherine@gmail.com").Id
                };
                ProjectTask projectTask5 = new ProjectTask
                {
                    Name = "Test Task 5",
                    Deadline = DateTime.Now.AddDays(25),
                    Priority = Priority.Low,
                    Project = project3,
                    DeveloperID = db.Users.First(u => u.Email == "elizabeth@gmail.com").Id
                };
                ProjectTask projectTask6 = new ProjectTask
                {
                    Name = "Test Task 6",
                    Deadline = DateTime.Now.AddDays(30),
                    Priority = Priority.High,
                    Project = project4,
                    DeveloperID = db.Users.First(u => u.Email == "braden@gmail.com").Id
                };

                db.Projects.AddRange(new List<Project>
                {
                    project1,
                    project2,
                    project3,
                    project4
                });

                db.Tasks.AddRange(new List<ProjectTask>
                {
                    projectTask1,
                    projectTask2,
                    projectTask3,
                    projectTask4,
                    projectTask5,
                    projectTask6
                });
            }
        }
    }
}