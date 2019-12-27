using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	[Alias("Users")]
	public class User
	{
		[PrimaryKey]
		[AutoIncrement]
		public int User_Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Login { get; set; }

		[Required]
		[StringLength(30)]
		public string Password { get; set; }

		[Required]
		[StringLength(50)]
		public string Email { get; set; }
	}
}