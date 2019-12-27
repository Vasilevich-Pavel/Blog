using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	[Alias("Articles_Tegs")]
	public class Articles_Tegs
	{
		[PrimaryKey]
		[AutoIncrement]
		public int Articles_Tegs_Id { get; set; }

		[ForeignKey(typeof(Article))]
		public int Articles_Id { get; set; }

		[ForeignKey(typeof(Teg))]
		public int Tegs_Id { get; set; }
	}
}