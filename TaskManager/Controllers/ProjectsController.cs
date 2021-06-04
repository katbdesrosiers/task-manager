using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Authorize(Roles = "manager")]
    public class ProjectsController : TaskManagerController
    {
        // GET: Projects
        public ActionResult Index()
        {
            var user = CurrentUser();
            projectHelper.CalcTotalCost();
            projectHelper.CheckProjectsCompletion();

            notificationHelper.CreatePassedDeadlineNotification();
            notificationHelper.CheckProjectsComplete();
            notificationHelper.CheckTasksComplete();

            ViewBag.NotificationCount = notificationHelper.UnreadCount(user);
            return View(user.Projects.OrderByDescending(p => p.Priority).ThenBy(p => p.Deadline));
        }

        public ActionResult Create()
        {
            var user = CurrentUser();
            ViewBag.NotificationCount = notificationHelper.UnreadCount(user);
            ViewBag.Priorities = formsHelper.PrioritySelectList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Budget,Deadline,Priority")] Project project)
        {
            var user = CurrentUser();

            if (ModelState.IsValid)
            {
                projectHelper.Add(project, user);
                return RedirectToAction("Details", "Projects", new { id = project.ID });
            }
            ViewBag.NotificationCount = notificationHelper.UnreadCount(user);
            ViewBag.Priorities = formsHelper.PrioritySelectList();

            return View(project);
        }

        public ActionResult Details(int? id, string filter, string sort)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var project = db.Projects.Find(id);

            if (project == null)
                return HttpNotFound();

            ViewBag.Filter = String.IsNullOrEmpty(filter) ? "hide" : "";
            ViewBag.Sort = sort == "highPriority" ? "normal" : "highPriority";

            project.Tasks = projectHelper.Tasks(project, filter, sort);

            ViewBag.Priorities = formsHelper.PrioritySelectList();
            ViewBag.Developers = formsHelper.DeveloperSelectList();

            var user = CurrentUser();
            ViewBag.NotificationCount = notificationHelper.UnreadCount(user);
            return View(project);
        }

        public ActionResult OverBudget()
        {
            var user = CurrentUser();
            ViewBag.NotificationCount = notificationHelper.UnreadCount(user);
            return View(projectHelper.OverBudget());
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var project = db.Projects.Find(id);

            if (project == null)
                return HttpNotFound();

            projectHelper.Remove(project);

            return RedirectToAction("Index");
        }
    }
}