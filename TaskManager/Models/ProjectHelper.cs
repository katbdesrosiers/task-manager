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
                tasks = tasks.Where(t => t.CompletionPercentage != 100);
            }
            else if (sort == "highPriority")
            {
                tasks = tasks.OrderByDescending(t => t.Priority).ThenByDescending(t => t.CompletionPercentage);
            }

            return tasks.ToList();
        }

        public ICollection<Project> OverBudget(ApplicationUser user)
        {
            return user.Projects.Where(p => p.DateCompleted != null && p.Budget < p.TotalCost).ToList();
        }

        public void CalcTotalCost(Project project)
        {
            var daysToComplete = (project.DateCompleted.Value.Date - project.DateCreated.Date).Days;

            if (daysToComplete == 0)
                daysToComplete = 1;

            double managerCost = daysToComplete * project.Manager.Salary;
            double devCost = project.Tasks.Sum(t => t.Developer.Salary * daysToComplete);

            project.TotalCost = managerCost + devCost;

            db.SaveChanges();
        }

        public void CheckProjectCompletion(Project project, NotificationHelper notificationHelper)
        {
            if (project.Tasks.Count() > 0 && project.Tasks.All(t => t.DateCompleted != null))
            {
                var latestCompletion = project.Tasks.OrderByDescending(t => t.DateCompleted).FirstOrDefault();
                project.DateCompleted = latestCompletion.DateCompleted;
                notificationHelper.CreateProjectCompleteNotification(project);
                CalcTotalCost(project);
            }
            else
            {
                project.DateCompleted = null;
                project.TotalCost = 0;
            }

            db.SaveChanges();
        }
    }
}