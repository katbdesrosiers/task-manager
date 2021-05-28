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
            var user = CurrentUser();
            //NotificationHelper.MarkRead(user, db);
            return View(user.Notifications);
        }
    }
}