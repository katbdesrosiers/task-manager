using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Controllers;

namespace TaskManager.Models
{
    public class NotificationHelper : Helper
    {
        public void CreateCommentNotification(ProjectTask task)
        {
            var taskID = task.ID;

            Notification n = new Notification
            {
                ItemID = taskID,
                User = task.Project.Manager,
                Content = $"Task '{task.Name}' has an urgent comment!"
            };

            db.Notifications.Add(n);
            db.SaveChanges();
        }

        public void CreatePastDeadlineNotification()
        {
            var projects = db.Projects.ToList();

            foreach (var project in projects)
            {
                if (DateTime.Now.Date > project.Deadline.Date)
                {
                    if (project.Tasks.Any(t => t.DateCompleted == null))
                    {
                        var projectID = project.ID;

                        Notification n = new Notification
                        {
                            ItemID = projectID,
                            User = project.Manager,
                            Content = $"Project '{project.Name}' has passed its deadline with incomplete tasks!",
                            IsProject = true,
                        };

                        if (!db.Notifications.ToList().Any(notif => notif.Content == n.Content))
                            db.Notifications.Add(n);
                    }
                }
            }
            db.SaveChanges();
        }

        public void CheckProjectsComplete()
        {
            var projects = db.Projects.ToList();

            foreach (var project in projects)
            {
                if (project.DateCompleted != null)
                {
                    var projectID = project.ID;

                    Notification n = new Notification
                    {
                        ItemID = projectID,
                        User = project.Manager,
                        Content = $"Project '{project.Name}' has been completed!",
                        IsProject = true,
                    };

                    if (!db.Notifications.ToList().Any(notif => notif.Content == n.Content))
                        db.Notifications.Add(n);
                }
            }

            db.SaveChanges();
        }

        public void CheckTasksComplete()
        {
            var tasks = db.Tasks.ToList();

            foreach (var task in tasks)
            {
                if (task.CompletionPercentage == 100)
                {
                    var taskID = task.ID;

                    Notification n = new Notification
                    {
                        ItemID = taskID,
                        User = task.Project.Manager,
                        Content = $"Task '{task.Name}' has been completed!",
                    };

                    if (!db.Notifications.ToList().Any(notif => notif.Content == n.Content))
                        db.Notifications.Add(n);
                }
            }

            db.SaveChanges();
        }

        public void CreateDeadlineNotification(ProjectTask task)
        {
            var taskID = task.ID;

            Notification n = new Notification
            {
                ItemID = taskID,
                User = task.Developer,
                Content = $"Task '{task.Name}' has 1 day until the deadline!"
            };

            db.Notifications.Add(n);
            db.SaveChanges();
        }

        public int UnreadCount(ApplicationUser user)
        {
            return user.Notifications.Where(n => !n.Read).Count();
        }

        public void RemoveDeletedNotifications()
        {
            foreach (var notif in db.Notifications.ToList())
            {
                if (!db.Projects.Any(p => p.ID == notif.ItemID) &&
                    !db.Tasks.Any(t => t.ID == notif.ItemID))
                    db.Notifications.Remove(notif);
            }

            db.SaveChanges();
        }
    }
}