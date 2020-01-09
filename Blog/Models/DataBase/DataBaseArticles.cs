using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Blog.Models
{
	public class DataBaseArticles
	{
		private OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["conn"].ConnectionString, MySqlDialect.Provider);

		public void CreateTable()
		{
			using (var db = dbFactory.Open())
			{
				db.CreateTable<Article>();
			}
		}

		public int Insert(Article article)
		{
			using (var db = dbFactory.Open())
			{
				long id = db.Insert(article, selectIdentity: true);
				return Convert.ToInt32(id);
			}
		}

		public List<Article> SelectTop10(int id)
		{
			using (var db = dbFactory.Open())
			{
				bool check = false;
				var ids = db.Select<Article>("SELECT Article_Id FROM Articles");
				var articles = db.Select<Article>("SELECT * FROM Articles ORDER BY Article_Id DESC LIMIT " + id * 10);
				if(articles.Count == ids.Count)
				{
					check = true;
				}

				if (id != 1)
				{
					try
					{
						articles.RemoveRange(0, (id - 1) * 10 - 1);
					}
					catch (Exception)
					{
						return null;
					}
				}

				if (check)
				{
					articles.Add(null);
				}

				return articles;

			}
		}

		public List<Article> SelectTop10Category(int id, int category_id)
		{
			using (var db = dbFactory.Open())
			{
				bool check = false;
				var ids = db.Select<Article>("SELECT Article_Id FROM Articles WHERE Category_Id = " + category_id);
				var articles = db.Select<Article>("SELECT * FROM Articles WHERE Category_Id = " + category_id + " ORDER BY Article_Id DESC LIMIT " + id * 10);
				if (articles.Count == ids.Count)
				{
					check = true;
				}

				if (id != 1)
				{
					try
					{
						articles.RemoveRange(0, (id - 1) * 10 - 1);
					}
					catch (Exception)
					{
						return null;
					}
				}

				if (check)
				{
					articles.Add(null);
				}

				return articles;
			}
		}

		public Article Select(int? id)
		{
			using (var db = dbFactory.Open())
			{
				return db.Single<Article>(x => x.Article_Id == id);
			}
		}

		public void UpdateCategory(int id_withoutCategory, int? id_category)
		{
			using (var db = dbFactory.Open())
			{
				var articles = db.Select<Article>(x => x.Category_Id == id_category);

				foreach(var a in articles)
				{
					db.UpdateOnly(() => new Article
					{
						Category_Id = id_withoutCategory
					}, where: x => x.Article_Id == a.Article_Id);
				}

				db.Delete<Category>(x => x.Category_Id == id_category);
			}
		}

		public List<Article> Select(SearchModel model)
		{
			using (var db = dbFactory.Open())
			{
				var articles = new List<Article>();

				if (model.To != DateTime.MinValue && model.From != DateTime.MinValue)
				{
					articles = db.Select<Article>(x => x.Category_Id == model.CategoryId && x.DateTime >= model.From && x.DateTime <= model.To.AddDays(1));
				}
				else if (model.To == DateTime.MinValue && model.From != DateTime.MinValue) {
					articles = db.Select<Article>(x => x.Category_Id == model.CategoryId && x.DateTime >= model.From);
				}
				else if(model.To != DateTime.MinValue && model.From == DateTime.MinValue)
				{
					articles = db.Select<Article>(x => x.Category_Id == model.CategoryId && x.DateTime <= model.To.AddDays(1));
				}
				else if (model.To == DateTime.MinValue && model.From == DateTime.MinValue)
				{
					articles = db.Select<Article>(x => x.Category_Id == model.CategoryId);
				}

				return articles;
			}
		}

		public void Update(Article article)
		{
			using (var db = dbFactory.Open())
			{
				db.UpdateOnly(() => new Article
				{
					Name = article.Name,
					ShortDescription = article.ShortDescription,
					DateTime = article.DateTime,
					Description = article.Description,
					Category_Id = article.Category_Id
				}, where: x => x.Article_Id == article.Article_Id);
			}
		}

		public void Delete(int? id)
		{
			using (var db = dbFactory.Open())
			{
				db.Delete<Article>(x => x.Article_Id == id);
			}
		}
	}
}