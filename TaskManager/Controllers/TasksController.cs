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

            IncludeNotificationCount();
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

            IncludeNotificationCount();
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

            IncludeNotificationCount();

            ViewBag.Developers = new SelectList(
                db.Users.ToList()
                .Where(u => Membership.UserInRole(u.Id, "developer"))
                .OrderBy(u => u.UserName),
                "Id",
                "UserName",
                task.Developer.Id);

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

            return RedirectToAction("Details");
        }

        public void IncludeNotificationCount()
        {
            ViewBag.NotificationCount = CurrentUser().Notifications.Count();
        }

        [Authorize(Roles = "manager")]
        public ActionResult TasksNotFinishedOnTime()
        {
            IncludeNotificationCount();
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

            ViewBag.Developers = new SelectList(
                db.Users.ToList()
                .Where(u => Membership.UserInRole(u.Id, "developer"))
                .OrderBy(u => u.UserName),
                "Id",
                "UserName",
                task.Developer.Id);

            IncludeNotificationCount();
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
                taskHelper.AddComment(task, comment);
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
    }
}