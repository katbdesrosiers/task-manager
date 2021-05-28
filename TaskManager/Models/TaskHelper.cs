using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class TaskHelper : Helper
    {
        public void CheckTaskDeadline(ApplicationUser user)
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

        public void Add(ProjectTask task, Project project)
        {
            project.Tasks.Add(task);
            db.SaveChanges();
        }
    }
}