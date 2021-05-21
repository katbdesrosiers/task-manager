using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
	public class ProjectTask
	{
		public ProjectTask()
		{
			Developers = new HashSet<ApplicationUser>();
		}

		public int ID { get; set; }
		public int ProjectID { get; set; }
		public int CompletionPercentage { get; set; }
		public string Name { get; set; }

		public virtual Project Project { get; set; }
		public virtual ICollection<ApplicationUser> Developers { get; set; }
	}
}