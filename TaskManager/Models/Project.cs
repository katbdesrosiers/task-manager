using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
	public class Project
	{
		public Project()
		{
			Tasks = new HashSet<ProjectTask>();
			Users = new HashSet<ApplicationUser>();
		}

		public int ID { get; set; }
		public string Name { get; set; }

		public virtual ICollection<ProjectTask> Tasks { get; set; }
		public virtual ICollection<ApplicationUser> Users { get; set; }
		//public DateTime Deadline { get; set; }
	}
}