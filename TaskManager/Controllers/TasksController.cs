using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TasksController : TaskManagerController
    {
        // GET: Tasks
        [Authorize(Roles = "developer")]
        public ActionResult Index()
        {
            var user = CurrentUser();
            taskHelper.CheckTaskDeadline(user);
            projectHelper.CalcTotalCost();

            ViewBag.NotificationCount = user.Notifications.Count();

            return View(user.Tasks);
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult Create([Bind(Include = "Name,ProjectID,Deadline,Priority,DeveloperID")] ProjectTask task)
        {
            var project = db.Projects.Find(task.ProjectID);

            if (project == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                taskHelper.Add(task, project);
            }
            else
            {
                TempData["Error"] = "Your task is missing something";
            }

            ViewBag.NotificationCount = CurrentUser().Notifications.Count();
            return RedirectToAction("Details", "Projects", new { id = task.ProjectID });
        }
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var task = db.Tasks.Find(id);

            if (task == null)
                return HttpNotFound();

            ViewBag.NotificationCount = CurrentUser().Notifications.Count();
            return View(task);
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var task = db.Tasks.Find(id);

            if (task == null)
                return HttpNotFound();

            db.Tasks.Remove(task);
            db.SaveChanges();

            return RedirectToAction("Details");
        }

        [Authorize(Roles = "manager")]
        public ActionResult TasksNotFinishedOnTime()
        {
            var tasks = db.Tasks.Where(t => t.DateCompleted == null && t.Deadline < DateTime.Now).ToList();

            ViewBag.NotificationCount = CurrentUser().Notifications.Count();
            return View(tasks);
        }

        [HttpPost]
        [Authorize(Roles = "developer")]
        public ActionResult UpdatePercent(int? id, int CompletionPercentage)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var task = db.Tasks.Find(id);

            if (task == null)
                return HttpNotFound();

            task.CompletionPercentage = CompletionPercentage;

            if (CompletionPercentage == 100)
            {
                task.DateCompleted = DateTime.Now;
            }
            else
            {
                task.DateCompleted = null;
            }
            db.SaveChanges();

            ViewBag.NotificationCount = CurrentUser().Notifications.Count();
            return View("Details", task);
        }

        [HttpPost]
        [Authorize(Roles = "developer")]
        public ActionResult Comment([Bind(Include ="Content,TaskID,DeveloperID,Urgent")] Comment comment)
        {
            var task = db.Tasks.Find(comment.TaskID);

            if (task == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                task.Comments.Add(comment);

                if (comment.Urgent)
                {
                    notificationHelper.CreateCommentNotification(task);
                }

                db.SaveChanges();
            }
            else
            {
                TempData["Error"] = "Your comment is missing something";
            }

            return RedirectToAction("Details", "Tasks", new { id = comment.TaskID });
        }
    }
}