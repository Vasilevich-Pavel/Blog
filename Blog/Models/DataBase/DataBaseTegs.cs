using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	public class DataBaseTeg
	{
		private OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory("server=localhost;user id=root;password=mysql;database=blog;", MySqlDialect.Provider);

		public void CreateTable()
		{
			using (var db = dbFactory.Open())
			{
				db.CreateTable<Teg>();
			}
		}

		public int Insert(Teg teg)
		{
			using (var db = dbFactory.Open())
			{
				long id = db.Insert(teg, selectIdentity: true);
				return Convert.ToInt32(id);
			}
		}
	}
}