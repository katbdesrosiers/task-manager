using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class TaskHelper
    {
        public static void CheckTaskDeadline(ApplicationUser user, ApplicationDbContext db)
        {
            foreach (var task in user.Tasks)
            {
                if (!task.DeadlineNotificationSent && DateTime.Now > task.Deadline.AddDays(-1))
                {
                    task.DeadlineNotificationSent = true;
                    //create a notification for task developer
                }
            }
            db.SaveChanges();
        }

        public static void Add(Project project, ProjectTask task, ApplicationDbContext db)
        {
            project.Tasks.Add(task);
            db.SaveChanges();
        }
    }
}