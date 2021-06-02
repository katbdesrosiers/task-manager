using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class Notification
    {
        public Notification()
        {
            Read = false;
            DateCreated = DateTime.Now;
        }

        public int NotificationID { get; set; }
        public int? ProjectID { get; set; }
        public int? TaskID { get; set; }
        public string ApplicationUserID { get; set; }
        public bool Read { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Project Project { get; set; }
        public virtual ProjectTask Task { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}