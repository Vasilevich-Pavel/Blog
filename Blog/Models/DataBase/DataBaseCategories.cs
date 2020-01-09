using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	public class DataBaseCategories
	{
		private OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["conn"].ConnectionString, MySqlDialect.Provider);

		public void CreateTable()
		{
			using (var db = dbFactory.Open())
			{
				db.CreateTable<Category>();
			}
		}

		public int Insert(string name)
		{
			using (var db = dbFactory.Open())
			{
				Category category = new Category
				{
					Name = name
				};

				long id = db.Insert(category, selectIdentity: true);
				return Convert.ToInt32(id);
			}
		}

		public void Insert()
		{
			using (var db = dbFactory.Open())
			{
				var single = db.Single<Category>(x => x.Name == "Без категории");

				if (single == null)
				{

					Category category = new Category
					{
						Name = "Без категории"
					};

					db.Insert(category);
				}
			}
		}

		public bool Insert(Category category)
		{
			using (var db = dbFactory.Open())
			{
				var single = db.Single<Category>(x => x.Name == category.Name);
				if (single == null)
				{
					db.Insert(category);
					return true;
				}
				return false;
			}
		}

		public List<Category> Select()
		{
			using (var db = dbFactory.Open())
			{
				return db.Select<Category>();
			}
		}

		public List<string> SelectName()
		{
			using (var db = dbFactory.Open())
			{
				var q = db.From<Category>().Select(x => x.Name);
				return db.Column<string>(q);
			}
		}

		public int Select(string name)
		{
			using (var db = dbFactory.Open())
			{
				var single = db.Single<Category>(x => x.Name == name);

				if (single == null)
				{
					return 0;
				}

				return single.Category_Id;
			}
		}

		public Category Select(int id)
		{
			using (var db = dbFactory.Open())
			{
				return db.Single<Category>(x => x.Category_Id == id);
			}
		}

		public Category Select(int? id)
		{
			using (var db = dbFactory.Open())
			{
				return db.Single<Category>(x => x.Category_Id == id);
			}
		}

		public int SelectWithoutCategory()
		{
			using (var db = dbFactory.Open())
			{
				return db.Single<Category>(x => x.Name == "Без категории").Category_Id;
			}
		}

		public void Delete(int? id)
		{
			using (var db = dbFactory.Open())
			{
				db.Delete<Category>(x => x.Category_Id == id);
			}
		}

		public void Update(Category category)
		{
			using (var db = dbFactory.Open())
			{
				db.UpdateOnly(() => new Category
				{
					Name = category.Name,
				},
					where: x => x.Category_Id == category.Category_Id);
			}
		}
	}
}