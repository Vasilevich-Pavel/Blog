using System.Collections.Generic;
using System.Web.Mvc;

namespace Blog.Models
{
	public class OneArticle
	{
		public Category Category { get; set; }
		public Article Article { get; set; }
		public List<Teg> Tegs { get; set; } = new List<Teg>();
		public string Teg { get; set; }
		public SelectList Categories { get; set; }
		public SearchModel SearchModel { get; set; }
	}
}