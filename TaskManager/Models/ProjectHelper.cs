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

        public void CalcTotalCost()
        {
            var completeProjects = db.Projects.Where(p => p.DateCompleted != null).ToList();

            foreach (var project in completeProjects)
            {
                var daysToComplete = (project.DateCompleted.Value.Date - project.DateCreated.Date).Days;
                double managerCost = daysToComplete * project.Manager.Salary;
                double devCost = project.Tasks.Sum(t => t.Developer.Salary * daysToComplete);

                project.TotalCost = managerCost + devCost;
            }

            db.SaveChanges();
        }
    }
}