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

        public SelectList DeveloperSelectList()
        {
            return DeveloperSelectList(null);
        }

        public SelectList DeveloperSelectList(ApplicationUser selected)
        {
            var selectedID = selected != null ? selected.Id : null;

            var developers = new SelectList(
                db.Users.ToList()
                .Where(u => Membership.UserInRole(u.Id, "developer"))
                .OrderBy(u => u.UserName),
                "Id",
                "UserName", selectedID);

            return developers;
        }
    }
}