using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	[Alias("Categories")]
	public class Category
	{
		[PrimaryKey]
		[AutoIncrement]
		public int Category_Id { get; set; }

		[Required]
		[StringLength(30)]
		public string Name { get; set; }
	}
}