using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Authorize]
    public class HomeController : TaskManagerController
    {
        public ActionResult Index()
        {
            if (User.IsInRole("manager"))
            {
                return RedirectToAction("Index", "Projects");
            }
            else if (User.IsInRole("developer"))
            {
                return RedirectToAction("Index", "Tasks");
            }
            else
            {
                return RedirectToAction("Users", "Manage");
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}