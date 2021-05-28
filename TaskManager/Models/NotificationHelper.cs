using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Controllers;

namespace TaskManager.Models
{
    public class NotificationHelper : Helper
    {
        public void MarkRead(ApplicationUser user)
        {
            foreach (var notif in user.Notifications)
                notif.Read = true;

            db.SaveChanges();
        }

        public void CreateCommentNotification(ProjectTask task)
        {
            Notification n = new Notification
            {
                Task = task,
                User = task.Project.Manager,
                Content = $"Task '{task.Name}' has an urgent comment!"
            };

            db.Notifications.Add(n);
            db.SaveChanges();
        }
    }
}