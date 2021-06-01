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
            var notifHelper = Notification(db);

            foreach (var task in user.Tasks)
            {
                if (!task.DeadlineNotificationSent && DateTime.Now.Day == task.Deadline.AddDays(-1).Day)
                {
                    task.DeadlineNotificationSent = true;
                    notifHelper.CreateDeadlineNotification(task);
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