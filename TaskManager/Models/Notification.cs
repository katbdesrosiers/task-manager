using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class Notification
    {
        public int ScheduledItemID { get; set; }
        public int UserID { get; set; }
        public bool Read { get; set; }
        public string Content { get; set; }
    }
}