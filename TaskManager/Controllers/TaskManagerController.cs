using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        protected object ProtectProject(int? id, ApplicationUser user)
        {
            HttpStatusCode status = HttpStatusCode.OK;
            Project project = null;
            object result;

            if (id == null)
            {
                status = HttpStatusCode.BadRequest;
            }
            else if (!userManager.IsInRole(user.Id, "manager"))
            {
                status = HttpStatusCode.Forbidden;
            }
            else
            {
                project = db.Projects.Find(id);

                if (project == null)
                    status = HttpStatusCode.NotFound;
                else if (project.ManagerID != user.Id)
                    status = HttpStatusCode.Forbidden;
            }

            if (status != HttpStatusCode.OK)
                result = new HttpStatusCodeResult(status);
            else
                result = project;

            return result;
        }

        protected object ProtectTask(int? id, ApplicationUser user, string role)
        {
            HttpStatusCode status = HttpStatusCode.OK;
            ProjectTask task = null;
            object result;

            if (id == null)
            {
                status = HttpStatusCode.BadRequest;
            }
            else if (role != null && !userManager.IsInRole(user.Id, role))
            {
                status = HttpStatusCode.Forbidden;
            }
            else
            {
                task = db.Tasks.Find(id);

                if (task == null)
                    status = HttpStatusCode.NotFound;
                else if (Membership.UserInRole(user.Id, "manager") && task.Project.ManagerID != user.Id)
                    status = HttpStatusCode.Forbidden;
                else if (Membership.UserInRole(user.Id, "developer") && task.DeveloperID != user.Id)
                    status = HttpStatusCode.Forbidden;
            }

            if (status != HttpStatusCode.OK)
                result = new HttpStatusCodeResult(status);
            else
                result = task;

            return result;
        }
    }
}