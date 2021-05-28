using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class ProjectTask : ScheduledItem
    {
        public ProjectTask()
        {
            Comments = new HashSet<Comment>();
        }

        [Required]
        public int ProjectID { get; set; }

        [Required]
        public string DeveloperID { get; set; }
        public bool DeadlineNotificationSent { get; set; }
        public int CompletionPercentage { get; set; }
        public virtual Project Project { get; set; }
        public virtual ApplicationUser Developer { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}