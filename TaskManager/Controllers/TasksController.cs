﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TasksController : TaskManagerController
    {
        // GET: Tasks
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,ProjectID")] ProjectTask task)
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
                TempData["Error"] = "Task name cannot be blank";
            }


            return RedirectToAction("Details", "Projects", new { id = task.ProjectID });
        }
    }
}