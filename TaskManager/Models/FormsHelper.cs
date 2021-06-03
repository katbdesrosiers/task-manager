using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class FormsHelper : Helper
    {
        public SelectList Priorities()
        {
            var Priorities = new SelectList(Enum.GetValues(typeof(Priority)));
            return Priorities;
        }
        public SelectList ProjectDevelopers(IEnumerable<ApplicationUser> developers)
        {
            var Developers = new SelectList(developers, "Id", "UserName");
            return Developers;
        }

        public SelectList TaskDevelopers(ProjectTask task)
        {
            var Developers = new SelectList(
                db.Users.ToList()
                .Where(u => Membership.UserInRole(u.Id, "developer"))
                .OrderBy(u => u.UserName),
                "Id",
                "UserName",
                task.Developer.Id);

            return Developers;
        }
    }
}