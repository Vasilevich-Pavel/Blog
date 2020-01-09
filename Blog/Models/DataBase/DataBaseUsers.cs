using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	public class DataBaseUsers
	{
		private OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["conn"].ConnectionString, MySqlDialect.Provider);

		public void CreateTables()
		{
			using (var db = dbFactory.Open())
			{
				db.CreateTable<User>();
			}
		}

		public void Insert()
		{
			using (var db = dbFactory.Open())
			{
				var single = db.Single<User>(x => x.Login == "Admin" && x.Password == "Admin");

				if (single == null)
				{

					User user = new User
					{
						Login = "Admin",
						Password = "Admin",
						Email = "admin.admin@inbox.ru"
					};

					db.Insert(user);
				}
			}
		}

		public User GetUser(string login)
		{
			using (var db = dbFactory.Open())
			{ 
				return db.Single<User>(x => x.Login == login);
			}
		}

		public User GetUser(int user_id)
		{
			using (var db = dbFactory.Open())
			{
				return db.Single<User>(x => x.User_Id == user_id);
			}
		}

		public User GetUser(RegisterModel model)
		{
			using (var db = dbFactory.Open())
			{
				return db.Single<User>(x => x.Login == model.Login && x.Password == model.Password && x.Email == model.Email);
			}
		}

		public User GetUser(LoginModel model)
		{
			using (var db = dbFactory.Open())
			{
				return db.Single<User>(x => x.Login == model.Login && x.Password == model.Password);
			}
		}

		public void AddUser(RegisterModel model)
		{
			using (var db = dbFactory.Open())
			{
				User user = new User
				{
					Login = model.Login,
					Password = model.Password,
					Email = model.Email
				};

				db.Insert(user);
			}
		}

		public void AddUser(User user)
		{
			using (var db = dbFactory.Open())
			{
				db.Insert(user);
			}
		}

		public void UpdateUser(User user)
		{
			using (var db = dbFactory.Open())
			{
				db.UpdateOnly(() => new User { Login = user.Login,
					Password = user.Password,
					Email = user.Email },
					where: x => x.User_Id == user.User_Id);
			}
		}

		public void UpdatePassword(string email, string password)
		{
			using (var db = dbFactory.Open())
			{
				db.UpdateOnly(() => new User
				{
					Password = password,
				},
					where: x => x.Email == email);
			}
		}

		public void DeleteUser(int user_id)
		{
			using (var db = dbFactory.Open())
			{
				db.Delete<User>(x => x.User_Id == user_id);
			}
		}
	}
}