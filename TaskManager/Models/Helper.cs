using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public abstract class Helper
    {
        public ApplicationDbContext db { get; set; }

        public static TaskHelper Task (ApplicationDbContext context)
        {
            var helper = new TaskHelper();
            
            helper.db = context;

            return helper;
        }

        public static ProjectHelper Project(ApplicationDbContext context)
        {
            var helper = new ProjectHelper();

            helper.db = context;

            return helper;
        }

        public static NotificationHelper Notification(ApplicationDbContext context)
        {
            var helper = new NotificationHelper();

            helper.db = context;

            return helper;
        }

        public static FormsHelper Forms(ApplicationDbContext context)
        {
            var helper = new FormsHelper();

            helper.db = context;

            return helper;
        }
    }
}