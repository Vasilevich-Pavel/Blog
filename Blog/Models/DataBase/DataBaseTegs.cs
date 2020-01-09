using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	public class DataBaseTegs
	{
		private OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["conn"].ConnectionString, MySqlDialect.Provider);

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

		public int Insert(string teg)
		{
			using (var db = dbFactory.Open())
			{
				Teg t = new Teg
				{
					Name = teg
				};

				var single = db.Single<Teg>(x => x.Name == teg);
				if (single == null)
				{
					long id = db.Insert<Teg>(t, selectIdentity: true);
					return Convert.ToInt32(id);
				}
				else
				{
					return single.Teg_Id;
				}
			}
		}

		public Teg Select(int id_teg)
		{
			using (var db = dbFactory.Open())
			{
				return db.Single<Teg>(x => x.Teg_Id == id_teg);
			}
		}
	}
}