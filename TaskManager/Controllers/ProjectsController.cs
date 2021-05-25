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
            return View(user.Projects.OrderByDescending(p => p.Priority).ThenBy(p => p.Deadline));
        }

        public ActionResult Create()
        {
            ViewBag.Priorities = new SelectList(Enum.GetValues(typeof(Priority)));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Deadline,Priority")] Project project)
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
            ViewBag.Priorities = new SelectList(Enum.GetValues(typeof(Priority)));
            var developers = db.Users.ToList().Where(u => Membership.UserInRole(u.Id, "developer"));
            ViewBag.Developers = new SelectList(developers, "Id", "Email");
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