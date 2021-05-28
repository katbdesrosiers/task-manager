using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Authorize]
    public class TasksController : TaskManagerController
    {
        // GET: Tasks

        public ActionResult Index()
        {
            var user = CurrentUser();

            TaskHelper.CheckTaskDeadline(user, db);
            ProjectHelper.CalcTotalCost();

            ViewBag.NotificationCount = user.Notifications.Count();
            return View(user.Tasks);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,ProjectID,Deadline,Priority,DeveloperID")] ProjectTask task)
        {
            var project = db.Projects.Find(task.ProjectID);

            if (project == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                TaskHelper.Add(project, task, db);
            }
            else
            {
                TempData["Error"] = "Your task is missing something";
            }

            ViewBag.NotificationCount = CurrentUser().Notifications.Count();
            return RedirectToAction("Details", "Projects", new { id = task.ProjectID });
        }

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

        public ActionResult TasksNotFinishedOnTime()
        {
            var tasks = db.Tasks.Where(t => t.DateCompleted == null && t.Deadline < DateTime.Now).ToList();

            ViewBag.NotificationCount = CurrentUser().Notifications.Count();
            return View(tasks);
        }

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
    }
}