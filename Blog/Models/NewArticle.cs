using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Models
{
	public class NewArticle
	{
		public Article Article { get; set; }
		public Category Category { get; set; }
		public string Tegs { get; set; }
		public SelectList Categories { get; set; }
		public int CategoryId { get; set; }
	}
}