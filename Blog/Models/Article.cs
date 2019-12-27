using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	[Alias("Articles")]
	public class Article
	{
		[PrimaryKey]
		[AutoIncrement]
		public int Article_Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[StringLength(300)]
		public string ShortDescription { get; set; }

		[Required]
		[StringLength(StringLengthAttribute.MaxText)]
		public string Description { get; set; }

		[ForeignKey(typeof(Category))]
		public int Category_Id { get; set; }

		[Required]
		public DateTime DateTime { get; set; }

	}
}