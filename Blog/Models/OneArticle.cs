using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	public class OneArticle
	{
		public Category Category { get; set; }
		public Article Article { get; set; }
		public List<Teg> Tegs { get; set; }
	}
}