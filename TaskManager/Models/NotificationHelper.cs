using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Controllers;

namespace TaskManager.Models
{
    public class NotificationHelper : Helper
    {
        public void MarkRead(ApplicationUser user)
        {
            foreach (var notif in user.Notifications)
                notif.Read = true;

            db.SaveChanges();
        }
    }
}