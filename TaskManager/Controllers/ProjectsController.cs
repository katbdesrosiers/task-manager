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
            ProjectHelper.CalcTotalCost();

            ViewBag.NotificationCount = user.Notifications.Count();
            return View(user.Projects.OrderByDescending(p => p.Priority).ThenBy(p => p.Deadline));
        }
        public ActionResult Create()
        {
            ViewBag.Priorities = new SelectList(Enum.GetValues(typeof(Priority)));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Budget,Deadline,Priority")] Project project)
        {
            var user = CurrentUser();

            if (ModelState.IsValid)
            {
                user.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index"); //change this to redirect to project details
            }
            ViewBag.Priorities = new SelectList(Enum.GetValues(typeof(Priority)));
            return View(project);
        }

        public ActionResult Details(int? id, string filter, string sort)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var project = db.Projects.Find(id);

            if (project == null)
                return HttpNotFound();

            var displayProject = project;

            ViewBag.Filter = String.IsNullOrEmpty(filter) ? "hide" : "";
            ViewBag.Sort = sort == "highPriority" ? "normal" : "highPriority";

            if (filter == "hide")
            {
                project.Tasks = project.Tasks.Where(t => t.CompletionPercentage != 100).ToList();
            }
            else if (sort == "highPriority")
            {
                project.Tasks = project.Tasks.OrderByDescending(t => t.Priority).ThenByDescending(t => t.CompletionPercentage).ToList();
            }
            else if (String.IsNullOrEmpty(filter) && String.IsNullOrEmpty(sort))
            {
                project.Tasks = project.Tasks.OrderByDescending(t => t.CompletionPercentage).ToList();
            }

            ViewBag.Priorities = new SelectList(Enum.GetValues(typeof(Priority)));
            var developers = db.Users.ToList().Where(u => Membership.UserInRole(u.Id, "developer"));
            ViewBag.Developers = new SelectList(developers, "Id", "Email");

            return View(project);
        }

        public ActionResult OverBudget()
        {
            var overBudgetProjects = db.Projects.Where(p => p.DateCompleted != null && p.Budget < p.TotalCost);

            return View(overBudgetProjects.ToList());
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var project = db.Projects.Find(id);

            if (project == null)
                return HttpNotFound();

            db.Projects.Remove(project);
            db.SaveChanges();

            return RedirectToAction("Index");//change this to go to dashboard
        }
    }
}