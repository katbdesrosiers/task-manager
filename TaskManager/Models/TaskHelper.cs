using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class TaskHelper : Helper
    {
        public void CheckTaskDeadline(ApplicationUser user, NotificationHelper notificationHelper)
        {
            foreach (var task in user.Tasks)
            {
                var taskDate = task.Deadline.Date.AddDays(-1);
                var currentDate = DateTime.Now.Date;

                if (!task.DeadlineNotificationSent && taskDate == currentDate && task.CompletionPercentage != 100)
                {
                    task.DeadlineNotificationSent = true;
                    notificationHelper.CreateDeadlineNotification(task);
                }
            }

            db.SaveChanges();
        }

        public void Add(ProjectTask task, Project project)
        {
            project.Tasks.Add(task);
            db.SaveChanges();
        }

        public void Remove(ProjectTask task)
        {
            db.Tasks.Remove(task);
            db.SaveChanges();
        }

        public List<ProjectTask> OverdueTasks()
        {
            return db.Tasks
                .Where(t => t.DateCompleted == null && t.Deadline < DateTime.Now)
                .ToList();
        }

        public void ChangeCompletion(ProjectTask task, int percentage)
        {
            task.CompletionPercentage = percentage;

            if (percentage == 100)
                task.DateCompleted = DateTime.Now;
            else
                task.DateCompleted = null;

            db.SaveChanges();
        }

        public void AddComment(ProjectTask task, Comment comment, NotificationHelper notificationHelper)
        {
            task.Comments.Add(comment);

            if (comment.Urgent)
                notificationHelper.CreateCommentNotification(task);

            db.SaveChanges();
        }

        public void ChangeDeveloper(ProjectTask task, ApplicationUser developer)
        {
            developer.Tasks.Add(task);
            db.SaveChanges();
        }
    }
}