using Blog.Models.DataBase;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	public class DataBaseSchema
	{
		public static void CreateTables()
		{
			DataBaseUser user = new DataBaseUser();
			user.CreateTables();

			DataBaseCategory category = new DataBaseCategory();
			category.CreateTable();

			DataBaseTeg teg = new DataBaseTeg();
			teg.CreateTable();

			DataBaseArticle article = new DataBaseArticle();
			article.CreateTable();

			DataBaseArticles_Tegs articles_Tegs = new DataBaseArticles_Tegs();
			articles_Tegs.CreateTable();
		}

		public static void FillTables()
		{
			DataBaseCategory category = new DataBaseCategory();
			category.Insert();
		}
	}
}