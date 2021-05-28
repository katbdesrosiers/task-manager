using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [Required]
        public string DeveloperID { get; set; }

        [Required]
        public int TaskID { get; set; }

        [Required]
        public string Content { get; set; }

        public bool Urgent { get; set; }

        public virtual ApplicationUser Developer { get; set; }
        public virtual ProjectTask Task { get; set; }
    }
}