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
                project.Tasks.Add(task);
                db.SaveChanges();
            }
            else
            {
                TempData["Error"] = "Your task is missing something";
            }


            return RedirectToAction("Details", "Projects", new { id = task.ProjectID });
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var task = db.Tasks.Find(id);

            if (task == null)
                return HttpNotFound();

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


    }
}