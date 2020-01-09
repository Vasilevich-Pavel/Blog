using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Models
{
	public class SearchModel
	{

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime From { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime To { get; set; }

		public string Tegs { get; set; }
		public SelectList Categories { get; set; }
		public int CategoryId { get; set; }

		public List<OneArticle> OneArticles { get; set; } = new List<OneArticle>();
	}
}