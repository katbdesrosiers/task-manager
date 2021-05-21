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
            return View(user.Projects);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Project project)
        {
            var user = CurrentUser();

            if (ModelState.IsValid)
            {
                user.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index"); //change this to redirect to project details
            }

            return View(project);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var project = db.Projects.Find(id);

            if (project == null)
                return HttpNotFound();

            return View(project);
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