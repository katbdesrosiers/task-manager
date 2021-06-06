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

            notificationHelper.CreatePassedDeadlineNotification(user);
            
            DefaultViewBag(user);

            return View(user.Projects.OrderByDescending(p => p.Priority).ThenBy(p => p.Deadline));
        }

        public ActionResult Create()
        {
            var user = CurrentUser();
            
            DefaultViewBag(user);
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

            DefaultViewBag(user);
            ViewBag.Priorities = formsHelper.PrioritySelectList();

            return View(project);
        }

        public ActionResult Details(int? id, string filter, string sort)
        {
            Project project;

            var user = CurrentUser();

            var result = ProtectProject(id, user);

            if (result is HttpStatusCodeResult)
                return (HttpStatusCodeResult)result;
            else
                project = (Project)result;

            ViewBag.Filter = String.IsNullOrEmpty(filter) ? "hide" : "";
            ViewBag.Sort = sort == "highPriority" ? "normal" : "highPriority";

            project.Tasks = projectHelper.Tasks(project, filter, sort);


            DefaultViewBag(user);
            ViewBag.Priorities = formsHelper.PrioritySelectList();
            ViewBag.Developers = formsHelper.DeveloperSelectList();

            return View(project);
        }

        public ActionResult OverBudget()
        {
            var user = CurrentUser();

            DefaultViewBag(user);

            return View(projectHelper.OverBudget(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            Project project;

            var user = CurrentUser();

            var result = ProtectProject(id, user);

            if (result is HttpStatusCodeResult)
                return (HttpStatusCodeResult)result;
            else
                project = (Project)result;

            projectHelper.Remove(project);

            return RedirectToAction("Index");
        }
    }
}