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

            DefaultViewBag(user);

            var tasks = user.Tasks.GroupBy(t => t.Project);
            return View(tasks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult Create([Bind(Include = "Name,ProjectID,Deadline,Priority,DeveloperID")] ProjectTask task)
        {
            Project project;

            var user = CurrentUser();

            var result = ProtectProject(task.ProjectID, user);

            if (result is HttpStatusCodeResult)
                return (HttpStatusCodeResult)result;
            else
                project = (Project)result;

            if (ModelState.IsValid)
                taskHelper.Add(task, project);
            else
                TempData["Error"] = "Your task is missing something";

            DefaultViewBag(user);

            return RedirectToAction("Details", "Projects", new { id = task.ProjectID });
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            ProjectTask task;

            var user = CurrentUser();

            var result = ProtectTask(id, user, null);

            if (result is HttpStatusCodeResult)
                return (HttpStatusCodeResult)result;
            else
                task = (ProjectTask)result;

            DefaultViewBag(user);
            ViewBag.Developers = formsHelper.DeveloperSelectList(task.Developer);

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            ProjectTask task;

            var user = CurrentUser();

            var result = ProtectTask(id, user, "manager");

            if (result is HttpStatusCodeResult)
                return (HttpStatusCodeResult)result;
            else
                task = (ProjectTask)result;

            var project = task.Project;
            taskHelper.Remove(task);
            projectHelper.CheckProjectCompletion(project, notificationHelper);

            return RedirectToAction("Details", "Projects", new { id = task.ProjectID });
        }

        [Authorize(Roles = "manager")]
        public ActionResult TasksNotFinishedOnTime()
        {
            var user = CurrentUser();

            DefaultViewBag(user);

            return View(taskHelper.OverdueTasks(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "developer")]
        public ActionResult UpdatePercent(int? id, int CompletionPercentage)
        {
            ProjectTask task;

            var user = CurrentUser();

            var result = ProtectTask(id, user, "developer");

            if (result is HttpStatusCodeResult)
                return (HttpStatusCodeResult)result;
            else
                task = (ProjectTask)result;

            taskHelper.ChangeCompletion(task, CompletionPercentage, notificationHelper, projectHelper);

            DefaultViewBag(user);
            ViewBag.Developers = formsHelper.DeveloperSelectList(task.Developer);

            return View("Details", task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "developer")]
        public ActionResult Comment([Bind(Include = "Content,TaskID,DeveloperID,Urgent")] Comment comment)
        {
            ProjectTask task;

            var user = CurrentUser();

            var result = ProtectTask(comment.TaskID, user, "developer");

            if (result is HttpStatusCodeResult)
                return (HttpStatusCodeResult)result;
            else
                task = (ProjectTask)result;

            if (ModelState.IsValid)
                taskHelper.AddComment(task, comment, notificationHelper);
            else
                TempData["Error"] = "Your comment is missing something";

            return RedirectToAction("Details", "Tasks", new { id = comment.TaskID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public ActionResult ChangeDeveloper(int? id, string DeveloperID)
        {
            ProjectTask task;

            var user = CurrentUser();

            var result = ProtectTask(id, user, "manager");

            if (result is HttpStatusCodeResult)
                return (HttpStatusCodeResult)result;
            else
                task = (ProjectTask)result;

            var developer = db.Users.Find(DeveloperID);

            taskHelper.ChangeDeveloper(task, developer);

            return RedirectToAction("Details", "Tasks", new { id = task.ID });
        }
    }
}