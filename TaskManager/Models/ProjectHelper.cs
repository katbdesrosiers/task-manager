using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models
{
    [Authorize(Roles = "manager")]
    public class ProjectHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static void AddProject()
        {

        }

        public static void CalcTotalCost()
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