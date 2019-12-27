using Blog.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Blog.Controllers
{
	public class AccountController : Controller
    {
        
		//Login

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				User user = null;
				DataBaseUser db = new DataBaseUser();
				user = db.GetUser(model);

				if (user != null)
				{
					FormsAuthentication.SetAuthCookie(model.Login, true);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
				}
			}

			return View(model);
		}

		//Register

        public ActionResult Register()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterModel model)
		{
			if(ModelState.IsValid)
			{
				User user = null;
				DataBaseUser db = new DataBaseUser();
				user = db.GetUser(model.Login);

				if(user == null)
				{
					db.AddUser(model);
					user = db.GetUser(model);

					if (user != null)
					{
						FormsAuthentication.SetAuthCookie(model.Login, true);
						return RedirectToAction("Index", "Home");
					}

				}
				else
				{
					ModelState.AddModelError("", "Пользователь с таким логином уже существует");
				}
			}

			return View(model);
		}

		//Admin

		[Authorize]
		public ActionResult Admin()
		{
			string userName = HttpContext.User.Identity.Name;
			DataBaseUser dataBaseUser = new DataBaseUser();
			var user = dataBaseUser.GetUser(userName);

			return View(user);
		}

		[Authorize]
		[HttpPost]
		public ActionResult Admin(User user, string action)
		{
			if (ModelState.IsValid)
			{
				switch (action)
				{
					case "saveAdmin":

						return View(SaveAdmin(user));

					case "exitAdmin":

						ExitAdmin(user);
						return RedirectToAction("Index", "Home");

					case "deleteAdmin":

						DeleteAdmin(user);
						return RedirectToAction("Index", "Home");

					case "registerAdmin":

						return RedirectToAction("Register", "Account");
				}
			}

			return View();
		}

		private User SaveAdmin(User user)
		{
			DataBaseUser dataBaseUser = new DataBaseUser();
			dataBaseUser.UpdateUser(user);

			ModelState.AddModelError("", "Пользователь изменен");

			Response.Redirect(Request.Path);
			return dataBaseUser.GetUser(user.User_Id);
		}

		private void ExitAdmin(User user)
		{
			if (Request.Cookies["auth"] != null)
			{
				var cookie = new HttpCookie("auth")
				{
					Expires = DateTime.Now.AddDays(-1d)
				};
				Response.Cookies.Add(cookie);
			}
		}

		private void DeleteAdmin(User user)
		{
			DataBaseUser dataBaseUser = new DataBaseUser();
			dataBaseUser.DeleteUser(user.User_Id);
		}

		public ActionResult RestorePassword()
		{
			return View();
		}

		[HttpPost]
		public ActionResult RestorePassword(string Email)
		{
			string blogEmail = "blog.myblog@yandex.by";
			MailAddress blog = new MailAddress(blogEmail, "Blog.com");
			MailAddress user = new MailAddress(Email);
			MailMessage message = new MailMessage(blog, user);

			string newPassword = PasswordGeneration();

			message.Subject = "Восстановление пароля";
			message.Body = "Ваш новый пароль : " + newPassword + "\nС уважением, Blog.com";

			DataBaseUser dataBaseUser = new DataBaseUser();
			dataBaseUser.UpdatePassword(Email, newPassword);

			SmtpClient smtp = new SmtpClient("smtp.yandex.by", 587)
			{
				Credentials = new NetworkCredential(blogEmail, "blog_myblog"),
				EnableSsl = true
			};
			smtp.Send(message);

			ModelState.AddModelError("", "Новый пароль отправлен на почту");
			return View();
		}

		private string PasswordGeneration()
		{
			Random rnd = new Random();
			string password = "";

			for (int i = 0; i < 10; i++) //Пароль в 10 символов
			{
				int index = 0;
				switch (rnd.Next(1,3))
				{
					case 1: 

						index = rnd.Next(48, 57);
						break;

					case 2:

						index = rnd.Next(65, 90);
						break;

					case 3:

						index = rnd.Next(97, 122);
						break;
				}
				password += (char)index;
			}

			return password;
		}

	}
}