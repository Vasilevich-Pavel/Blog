using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	public class DataBaseCategory
	{
		private OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory("server=localhost;user id=root;password=mysql;database=blog;", MySqlDialect.Provider);

		public void CreateTable()
		{
			using (var db = dbFactory.Open())
			{
				db.CreateTable<Category>();
			}
		}

		public void Insert()
		{
			using (var db = dbFactory.Open())
			{
				List<Category> categories = new List<Category>();

				Category category1 = new Category
				{
					Name = "Здоровье и спорт"
				};
				categories.Add(category1);

				Category category2 = new Category
				{
					Name = "Семья"
				};
				categories.Add(category2);

				Category category3 = new Category
				{
					Name = "Музыка"
				};
				categories.Add(category3);

				Category category4 = new Category
				{
					Name = "Фильмы"
				};
				categories.Add(category4);

				Category category5 = new Category
				{
					Name = "Книги"
				};
				categories.Add(category5);

				Category category6 = new Category
				{
					Name = "Путешествия"
				};
				categories.Add(category6);

				Category category7= new Category
				{
					Name = "Без категории"
				};
				categories.Add(category7);

				db.InsertAll(categories);
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
	}
}