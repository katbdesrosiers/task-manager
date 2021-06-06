using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public enum Priority
    {
        Low,
        High
    }

    public abstract class ScheduledItem
    {
        public ScheduledItem()
        {
            DateCreated = DateTime.Now;
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public DateTime? DateCompleted { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [NotMapped]
        public string PriorityClassName
        {
            get
            {
                switch (Priority)
                {
                    case Priority.High:
                        return "warning";
                    default:
                        return "default";
                }
            }
        }
    }
}