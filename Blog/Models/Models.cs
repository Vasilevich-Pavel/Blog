﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	public class LoginModel
	{
		public string Login { get; set; }
		public string Password { get; set; }
	}

	public class RegisterModel
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
	}
}