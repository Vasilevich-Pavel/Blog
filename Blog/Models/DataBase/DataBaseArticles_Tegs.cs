using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Configuration;

namespace Blog.Models
{
	public class DataBaseArticles_Tegs
	{
		private OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["conn"].ConnectionString, MySqlDialect.Provider);

		public void CreateTable()
		{
			using (var db = dbFactory.Open())
			{
				db.CreateTable<Articles_Tegs>();
			}
		}

		public void Insert(int id_article, int id_teg)
		{
			using (var db = dbFactory.Open())
			{
				Articles_Tegs articles_Tegs = new Articles_Tegs();
				articles_Tegs.Articles_Id = id_article;
				articles_Tegs.Tegs_Id = id_teg;

				db.Insert(articles_Tegs);
			}
		}

		public List<Articles_Tegs> SelectTegs(int id_article)
		{
			using (var db = dbFactory.Open())
			{
				return db.Select<Articles_Tegs>(x => x.Articles_Id == id_article);
			}
		}

		public List<Articles_Tegs> SelectArticles(int? id_teg)
		{
			using (var db = dbFactory.Open())
			{
				return db.Select<Articles_Tegs>(x => x.Tegs_Id == id_teg);
			}
		}

		public void DeleteArticle(int? id_Article)
		{
			using (var db = dbFactory.Open())
			{
				db.Delete<Articles_Tegs>(x => x.Articles_Id == id_Article);
			}
		}

	}
}