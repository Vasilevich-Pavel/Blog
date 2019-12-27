using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	public class DataBaseArticle
	{
		private OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory("server=localhost;user id=root;password=mysql;database=blog;", MySqlDialect.Provider);

		public void CreateTable()
		{
			using (var db = dbFactory.Open())
			{
				db.CreateTable<Article>();
			}
		}

		public void Insert(Article article)
		{
			using (var db = dbFactory.Open())
			{
				db.Insert(article);
			}
		}

		public List<Article> SelectTop10(int id)
		{
			using (var db = dbFactory.Open())
			{
				var single = db.Single<Article>("SELECT * FROM Articles ORDER BY Article_Id DESC LIMIT 1");

				int upLimit = single.Article_Id - 10 * (id - 1);
				int dowmLimit = upLimit - 9;

				return db.Select<Article>("SELECT * FROM Articles WHERE Article_Id BETWEEN " + dowmLimit + " AND " + upLimit + " ORDER BY Article_Id DESC");
			}
		}

		public Article Select(int? id)
		{
			using (var db = dbFactory.Open())
			{
				return db.Single<Article>(x => x.Article_Id == id);
			}
		}

		public List<Article> SelectByCategory(int? id_category, int? id_page)
		{
			using (var db = dbFactory.Open())
			{
				//var single = db.Single<Article>("SELECT * FROM Articles WHERE Category_Id=" + id_category + " ORDER BY Article_Id DESC LIMIT 1");

				//int upLimit = single.Article_Id - 10 * (Convert.ToInt32(id_page) - 1);
				//int dowmLimit = upLimit - 9;

				return db.Select<Article>(x => x.Category_Id == id_category);
			}
		}
	}
}