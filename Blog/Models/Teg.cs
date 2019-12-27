using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	[Alias("Tegs")]
	public class Teg
	{
		[PrimaryKey]
		[AutoIncrement]
		public int Teg_Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }
	}
}