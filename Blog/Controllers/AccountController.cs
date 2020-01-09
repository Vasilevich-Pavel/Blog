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
				DataBaseUsers db = new DataBaseUsers();
				user = db.GetUser(model);

				if (user != null)
				{
					FormsAuthentication.SetAuthCookie(model.Login, true);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Неверный логин или пароль");
				}
			}

			return View(model);
		}

		//Register

		[Authorize]
		public ActionResult Register()
        {
            return View();
        }

		[HttpPost]
		[Authorize]
		public ActionResult Register(RegisterModel model)
		{
			if(ModelState.IsValid)
			{
				User user = null;
				DataBaseUsers db = new DataBaseUsers();
				user = db.GetUser(model);

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
					ModelState.AddModelError("", "Пользователь с таким логином или email существует");
				}
			}

			return View(model);
		}

		//Admin

		[Authorize]
		public ActionResult Admin()
		{
			string userName = HttpContext.User.Identity.Name;
			DataBaseUsers dataBaseUser = new DataBaseUsers();
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

					default:
						return RedirectToAction("Index", "Home");
				}
			}

			return View();
		}

		private User SaveAdmin(User user)
		{
			ExitAdmin(user);

			DataBaseUsers dataBaseUser = new DataBaseUsers();
			dataBaseUser.UpdateUser(user);

			var newUser = dataBaseUser.GetUser(user.User_Id);
			FormsAuthentication.SetAuthCookie(newUser.Login, true);

			ModelState.AddModelError("", "Пользователь изменен");
			Response.Redirect(Request.Path);
			return newUser;
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
			ExitAdmin(user);

			DataBaseUsers dataBaseUser = new DataBaseUsers();
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

			DataBaseUsers dataBaseUser = new DataBaseUsers();
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