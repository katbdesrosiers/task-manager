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

            taskHelper.CheckTaskDeadline(user, notificationHelper);
            projectHelper.CalcTotalCost();

            ViewBag.NotificationCount = notificationHelper.UnreadCount(user);
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
                taskHelper.Add(task, project);
            else
                TempData["Error"] = "Your task is missing something";

            var user = CurrentUser();
            ViewBag.NotificationCount = notificationHelper.UnreadCount(user);
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

            var user = CurrentUser();
            ViewBag.NotificationCount = notificationHelper.UnreadCount(user);

            ViewBag.Developers = formsHelper.TaskDevelopers(task);

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

            taskHelper.Remove(task);

            return RedirectToAction("Details", "Projects", new { id = task.ProjectID });
        }

        [Authorize(Roles = "manager")]
        public ActionResult TasksNotFinishedOnTime()
        {
            var user = CurrentUser();
            ViewBag.NotificationCount = notificationHelper.UnreadCount(user);
            return View(taskHelper.OverdueTasks());
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

            taskHelper.ChangeCompletion(task, CompletionPercentage);

            ViewBag.Developers = formsHelper.TaskDevelopers(task);

            var user = CurrentUser();
            ViewBag.NotificationCount = notificationHelper.UnreadCount(user);
            return View("Details", task);
        }

        [HttpPost]
        [Authorize(Roles = "developer")]
        public ActionResult Comment([Bind(Include = "Content,TaskID,DeveloperID,Urgent")] Comment comment)
        {
            var task = db.Tasks.Find(comment.TaskID);

            if (task == null)
                return HttpNotFound();

            if (ModelState.IsValid)
                taskHelper.AddComment(task, comment, notificationHelper);
            else
                TempData["Error"] = "Your comment is missing something";

            return RedirectToAction("Details", "Tasks", new { id = comment.TaskID });
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult ChangeDeveloper(int? id, string DeveloperID)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var task = db.Tasks.Find(id);

            if (task == null)
                return HttpNotFound();

            var developer = db.Users.Find(DeveloperID);

            taskHelper.ChangeDeveloper(task, developer);

            return RedirectToAction("Details", "Tasks", new { id = task.ID });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Read()
        {
            var user = CurrentUser();

            user.Notifications
                .Where(n => !n.Read)
                .ToList()
                .ForEach(n => n.Read = true);

            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}