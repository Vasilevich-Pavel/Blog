using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	public class ViewCategory
	{
		public List<Category> Categories { get; set; } = new List<Category>();
		public Category NewCategory { get; set; }
	}
}