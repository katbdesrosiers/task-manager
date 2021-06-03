using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class FormsHelper : Helper
    {
        public SelectList PrioritySelectList()
        {
            var names = Enum.GetNames(typeof(Priority));

            var priorities = new SelectList(names, Priority.Low.ToString());
            return priorities;
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