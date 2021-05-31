﻿using System;
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

        public void CreatePastDeadlineNotification()
        {
            var projects = db.Projects.ToList();

            foreach (var project in projects)
            {
                if (DateTime.Now >= project.Deadline)
                {
                    if (project.Tasks.Any(t => t.DateCompleted == null))
                    {
                        Notification n = new Notification
                        {
                            Project = project,
                            User = project.Manager,
                            Content = $"Project {project.Name} has passed its deadline with incomplete tasks!",
                        };

                        db.Notifications.Add(n);
                    }
                }
            }
            db.SaveChanges();
        }

        public void CreateDeadlineNotification(ProjectTask task)
        {
            Notification n = new Notification
            {
                Task = task,
                User = task.Developer,
                Content = $"Task '{task.Name}' has 1 day until the deadline!"
            };

            db.Notifications.Add(n);
            db.SaveChanges();
        }
    }
}