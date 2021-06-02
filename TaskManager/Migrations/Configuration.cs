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
                Project project3 = new Project // Complete
                {
                    Name = "Test Project 3",
                    Deadline = DateTime.Now.AddDays(25),
                    Priority = Priority.High,
                    Manager = db.Users.First(u => u.Email == "manager@gmail.com"),
                    Budget = 15000,
                    DateCompleted = DateTime.Now.AddDays(5),
                };
                Project project4 = new Project // Past deadline
                {
                    Name = "Test Project 4",
                    Deadline = DateTime.Now.AddDays(-15),
                    Priority = Priority.Low,
                    Manager = db.Users.First(u => u.Email == "manager@gmail.com"),
                    Budget = 500,
                };
                Project project5 = new Project // Over budget
                {
                    Name = "Test Project 5",
                    Deadline = DateTime.Now.AddDays(45),
                    Priority = Priority.Low,
                    Manager = db.Users.First(u => u.Email == "manager@gmail.com"),
                    Budget = 3000,
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
                    Deadline = DateTime.Now.AddDays(1),
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
                    DeveloperID = db.Users.First(u => u.Email == "elizabeth@gmail.com").Id,
                    CompletionPercentage = 100,
                    DateCompleted = DateTime.Now,
                };
                ProjectTask projectTask6 = new ProjectTask
                {
                    Name = "Test Task 6",
                    Deadline = DateTime.Now.AddDays(1),
                    Priority = Priority.High,
                    Project = project4,
                    DeveloperID = db.Users.First(u => u.Email == "braden@gmail.com").Id,
                    CompletionPercentage = 100,
                    DateCompleted = DateTime.Now
                };
                ProjectTask projectTask7 = new ProjectTask
                {
                    Name = "Test Task 7",
                    Deadline = DateTime.Now.AddDays(6),
                    Priority = Priority.Low,
                    Project = project3,
                    DeveloperID = db.Users.First(u => u.Email == "elizabeth@gmail.com").Id,
                    CompletionPercentage = 100,
                    DateCompleted = DateTime.Now
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
                ProjectTask projectTask9 = new ProjectTask
                {
                    Name = "Test Task 9",
                    Deadline = DateTime.Now.AddDays(45),
                    Priority = Priority.High,
                    Project = project5,
                    DeveloperID = db.Users.First(u => u.Email == "elizabeth@gmail.com").Id,
                    CompletionPercentage = 15
                };
                ProjectTask projectTask10 = new ProjectTask
                {
                    Name = "Test Task 10",
                    Deadline = DateTime.Now.AddDays(45),
                    Priority = Priority.High,
                    Project = project5,
                    DeveloperID = db.Users.First(u => u.Email == "braden@gmail.com").Id,
                };

                db.Projects.AddRange(new List<Project>
                {
                    project1,
                    project2,
                    project3,
                    project4,
                    project5
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
                    projectTask8,
                    projectTask9,
                    projectTask10
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
                    Urgent = true,
                    Content = "Test comment 2",
                };
                Comment comment3 = new Comment
                {
                    Task = projectTask6,
                    Developer = db.Users.First(u => u.Email == "braden@gmail.com"),
                    Urgent = false,
                    Content = "Test comment 3",
                };
                Comment comment4 = new Comment
                {
                    Task = projectTask9,
                    Developer = db.Users.First(u => u.Email == "elizabeth@gmail.com"),
                    Urgent = false,
                    Content = "Test comment 4",
                };

                db.Comments.AddRange(new List<Comment>
                {
                    comment1,
                    comment2,
                    comment3,
                    comment4
                });
            }
        }
    }
}