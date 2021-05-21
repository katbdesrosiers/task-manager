using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
	public class Project
	{
		public Project()
		{
			Tasks = new HashSet<ProjectTask>();
		}

		public int ID { get; set; }
		public string ManagerID { get; set; }
		[Required]
		public string Name { get; set; }

		public virtual ICollection<ProjectTask> Tasks { get; set; }
		public virtual ApplicationUser Manager { get; set; }
		//public DateTime Deadline { get; set; }
	}
}