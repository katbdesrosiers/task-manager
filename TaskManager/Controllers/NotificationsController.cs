using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.NotificationCount = notificationHelper.UnreadCount(user);
            return View(user.Notifications.OrderByDescending(n=>n.DateCreated));
        }
    }
}