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
                        UserName = person.Key.Substring(0, person.Key.IndexOf('@')),
                        LockoutEnabled = true,
                        Salary = 400,
                    };

                    Membership.CreateUser(user, "P3nguin!");

                    Membership.AddUserToRole(user.Id, person.Value);
                }
            }

            // Projects

            if (db.Projects.Count() == 0)
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
                    Budget = 11000,
                    DateCompleted = DateTime.Now.AddDays(15),
                };

                // Project Tasks

                ProjectTask projectTask1 = new ProjectTask
                {
                    Name = "Test Task 1",
                    Deadline = DateTime.Now.AddDays(10),
                    Priority = Priority.Low,
                    Project = project1,
                    CompletionPercentage = 23,
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
                    CompletionPercentage = 66,
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
                    DeveloperID = db.Users.First(u => u.Email == "braden@gmail.com").Id,
                    CompletionPercentage = 100
                };
                ProjectTask projectTask7 = new ProjectTask
                {
                    Name = "Test Task 7",
                    Deadline = DateTime.Now.AddDays(-5),
                    Priority = Priority.Low,
                    Project = project3,
                    DeveloperID = db.Users.First(u => u.Email == "elizabeth@gmail.com").Id
                };
                ProjectTask projectTask8 = new ProjectTask
                {
                    Name = "Test Task 8",
                    Deadline = DateTime.Now.AddDays(-3),
                    Priority = Priority.High,
                    Project = project4,
                    DeveloperID = db.Users.First(u => u.Email == "braden@gmail.com").Id,
                    CompletionPercentage = 80
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
                    projectTask6,
                    projectTask7,
                    projectTask8
                });

                // Notifications

                Notification notif1 = new Notification
                {
                    Project = project1,
                    User = db.Users.First(u => u.Email == "manager@gmail.com"),
                    Content = $"Project '{project1.Name}' has been completed!",
                };
                Notification notif2 = new Notification
                {
                    Task = projectTask3,
                    User = db.Users.First(u => u.Email == "chows@gmail.com"),
                    Content = $"Task '{projectTask3.Name}' has one day before the deadline!",
                };
                Notification notif3 = new Notification
                {
                    Project = project3,
                    User = db.Users.First(u => u.Email == "manager@gmail.com"),
                    Content = $"Project '{project3.Name}' has an urgent note!",
                };
                Notification notif4 = new Notification
                {
                    Project = project2,
                    User = db.Users.First(u => u.Email == "manager@gmail.com"),
                    Content = $"Project '{project2.Name}' has passed its deadline with unfinished tasks!",
                };

                db.Notifications.AddRange(new List<Notification>
                {
                    notif1,
                    notif2,
                    notif3,
                    notif4
                });

                // Comments

                Comment comment1 = new Comment
                {
                    Task = projectTask1,
                    Developer = db.Users.First(u => u.Email == "chows@gmail.com"),
                    Urgent = true,
                    Content = "Test comment 1",
                };
                Comment comment2 = new Comment
                {
                    Task = projectTask2,
                    Developer = db.Users.First(u => u.Email == "braden@gmail.com"),
                    Urgent = false,
                    Content = "Test comment 2",
                };
                Comment comment3 = new Comment
                {
                    Task = projectTask7,
                    Developer = db.Users.First(u => u.Email == "elizabeth@gmail.com"),
                    Urgent = false,
                    Content = "Test comment 3",
                };
            }
        }
    }
}