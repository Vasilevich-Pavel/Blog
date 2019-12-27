using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models.DataBase
{
	public class DataBaseArticles_Tegs
	{
		private OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory("server=localhost;user id=root;password=mysql;database=blog;", MySqlDialect.Provider);

		public void CreateTable()
		{
			using (var db = dbFactory.Open())
			{
				db.CreateTable<Articles_Tegs>();
			}
		}
	}
}