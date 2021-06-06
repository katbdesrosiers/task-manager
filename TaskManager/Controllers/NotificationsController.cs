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
    public class NotificationsController : TaskManagerController
    {
        // GET: Notifications
        public ActionResult Index()
        {
            notificationHelper.RemoveDeletedNotifications();

            var user = CurrentUser();

            DefaultViewBag(user);

            return View(user.Notifications.OrderByDescending(n => n.DateCreated));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Read()
        {
            var user = CurrentUser();

            user.Notifications
                .Where(n => !n.Read)
                .ToList()
                .ForEach(n => n.Read = true);

            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}