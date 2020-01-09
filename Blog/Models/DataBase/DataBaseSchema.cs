namespace Blog.Models
{
	public class DataBaseSchema
	{
		public static void CreateTables()
		{
			DataBaseUsers user = new DataBaseUsers();
			user.CreateTables();

			DataBaseCategories category = new DataBaseCategories();
			category.CreateTable();

			DataBaseTegs teg = new DataBaseTegs();
			teg.CreateTable();

			DataBaseArticles article = new DataBaseArticles();
			article.CreateTable();

			DataBaseArticles_Tegs articles_Tegs = new DataBaseArticles_Tegs();
			articles_Tegs.CreateTable();
		}

		public static void FillTables()
		{
			DataBaseCategories category = new DataBaseCategories();
			category.Insert();

			DataBaseUsers dataBaseUsers = new DataBaseUsers();
			dataBaseUsers.Insert();
		}
	}
}