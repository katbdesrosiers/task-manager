using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class ProjectHelper : Helper
    {
        public void Add(Project project, ApplicationUser user)
        {
            user.Projects.Add(project);
            db.SaveChanges();
        }
        
        public void Remove(Project project)
        {
            db.Projects.Remove(project);
            db.SaveChanges();
        }

        public ICollection<ProjectTask> Tasks(Project project, string filter, string sort)
        {
            IEnumerable<ProjectTask> tasks = project.Tasks.OrderByDescending(t => t.CompletionPercentage);

            if (filter == "hide")
            {
                tasks = project.Tasks.Where(t => t.CompletionPercentage != 100);
            }
            else if (sort == "highPriority")
            {
                tasks = project.Tasks.OrderByDescending(t => t.Priority).ThenByDescending(t => t.CompletionPercentage);
            }

            return tasks.ToList();
        }

        public ICollection<Project> OverBudget()
        {
            return db.Projects.Where(p => p.DateCompleted != null && p.Budget < p.TotalCost).ToList();
        }

        public void CalcTotalCost(ApplicationUser user)
        {
            var completeProjects = user.Projects.Where(p => p.DateCompleted != null).ToList();

            foreach (var project in completeProjects)
            {
                var daysToComplete = (project.DateCompleted.Value.Date - project.DateCreated.Date).Days;
                double managerCost = daysToComplete * project.Manager.Salary;
                double devCost = project.Tasks.Sum(t => t.Developer.Salary * daysToComplete);

                project.TotalCost = managerCost + devCost;
            }

            db.SaveChanges();
        }

        public void CheckProjectsCompletion(ApplicationUser user)
        {
            var projects = user.Projects.ToList();

            foreach (var project in projects)
            {
                if (project.Tasks.Count() > 0 && project.Tasks.All(t => t.DateCompleted != null))
                {
                    var latestCompletion = project.Tasks.OrderByDescending(t => t.DateCompleted).FirstOrDefault();
                    project.DateCompleted = latestCompletion.DateCompleted;
                }
                else
                {
                    project.DateCompleted = null;
                    project.TotalCost = 0;
                }
            }

            db.SaveChanges();
        }
    }
}