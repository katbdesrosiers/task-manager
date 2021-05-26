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
		public ActionResult Index() //change method to check role of user logged in
		{
			var user = CurrentUser();
			if(Membership.UserInRole(user.Id, "manager"))
            {
				return RedirectToAction("Index", "Projects");
            }
			else if(Membership.UserInRole(user.Id, "developer"))
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
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}