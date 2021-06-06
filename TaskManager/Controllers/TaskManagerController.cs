﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TaskManagerController : Controller
    {
        public ApplicationDbContext db { get; set; }
        public TaskHelper taskHelper { get; set; }
        public ProjectHelper projectHelper { get; set; }
        public NotificationHelper notificationHelper { get; set; }
        private UserManager<ApplicationUser> userManager { get; set; }
        public FormsHelper formsHelper { get; set; }

        public TaskManagerController()
        {
            db = new ApplicationDbContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            taskHelper = Helper.Task(db);
            projectHelper = Helper.Project(db);
            notificationHelper = Helper.Notification(db);
            formsHelper = Helper.Forms(db);
        }

        public ApplicationUser CurrentUser()
        {
            return userManager.FindById(User.Identity.GetUserId());
        }

        protected void DefaultViewBag(ApplicationUser user)
        {
            ViewBag.NotificationCount = notificationHelper.UnreadCount(user);
        }
    }
}