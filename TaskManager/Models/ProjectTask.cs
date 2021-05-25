using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
	public class ProjectTask
	{
		public ProjectTask()
		{
			Developers = new HashSet<ApplicationUser>();
			DateCreated = DateTime.Now;
		}

		public int ID { get; set; }
		public int ProjectID { get; set; }
		public int CompletionPercentage { get; set; }
		[Required]
		public string Name { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime Deadline { get; set; }
		public DateTime? DateCompleted { get; set; }
		public Priority Priority { get; set; }

		public virtual Project Project { get; set; }
		public virtual ICollection<ApplicationUser> Developers { get; set; }

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