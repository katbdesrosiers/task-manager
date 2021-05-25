using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public enum Priority
    {
        Low,
        High
    }

    public class Project
    {
        public Project()
        {
            Tasks = new HashSet<ProjectTask>();
            DateCreated = DateTime.Now;
        }

        public int ID { get; set; }
        public string ManagerID { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? DateCompleted { get; set; }
        public Priority Priority { get; set; }

        public virtual ICollection<ProjectTask> Tasks { get; set; }
        public virtual ApplicationUser Manager { get; set; }

        public string PanelClassName
        {
            get
            {
                switch (Priority)
                {
                    case Priority.High:
                        return "panel-warning";
                    default:
                        return "panel-info";                  
                }
            }
        }
    }
}