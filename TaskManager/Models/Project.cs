using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class Project : ScheduledItem
    {
        public Project()
        {
            Tasks = new HashSet<ProjectTask>();
        }

        public string ManagerID { get; set; }
        public double Budget { get; set; }
        public double TotalCost { get; set; }

        public virtual ICollection<ProjectTask> Tasks { get; set; }
        public virtual ApplicationUser Manager { get; set; }
    }
}