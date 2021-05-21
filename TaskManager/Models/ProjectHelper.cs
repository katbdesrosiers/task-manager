using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models
{
    [Authorize(Roles = "manager")]
    public class ProjectHelper
    {
        public static void AddProject()
        {

        }
    }
}