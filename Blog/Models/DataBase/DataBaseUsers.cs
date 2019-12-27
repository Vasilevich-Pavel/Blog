using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	public class DataBaseUser
	{
		private OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory("server=localhost;user id=root;password=mysql;database=blog;", MySqlDialect.Provider);

		public void CreateTables()
		{
			using (var db = dbFactory.Open())
			{
				db.CreateTable<User>();
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
				return db.Single<User>(x => x.Login == model.Login && x.Password == model.Password);
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
				db.Update(new User { Login = user.Login,
					Password = user.Password,
					Email = user.Email,
					User_Id = user.User_Id },
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