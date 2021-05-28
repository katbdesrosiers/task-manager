using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class Notification
    {
        public int ScheduledItemID { get; set; }
        public string ApplicationUserID { get; set; }
        public bool Read { get; set; }
        public string Content { get; set; }

        public virtual ScheduledItem ScheduledItem { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}