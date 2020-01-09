using Blog.Models;
using System;
using System.Web.Mvc;

namespace Blog.Controllers
{
	public class NewArticleController : Controller
    {
        [HttpGet]
        public ViewResult Create()
        {
			DataBaseCategories dataBaseCategory = new DataBaseCategories();
			NewArticle newArticle = new NewArticle
			{
				Categories = new SelectList(dataBaseCategory.Select(), "Category_Id", "Name")
			};
			return View(newArticle);
        }

		[HttpPost]
		public ActionResult Create(NewArticle newArticle)
		{
			DataBaseTegs dataBaseTeg = new DataBaseTegs();
			DataBaseArticles dataBaseArticles = new DataBaseArticles();
			DataBaseArticles_Tegs dataBaseArticles_Tegs = new DataBaseArticles_Tegs(); 

			newArticle.Article.Category_Id = newArticle.CategoryId;
			newArticle.Article.DateTime = DateTime.Now;
			int id_article = dataBaseArticles.Insert(newArticle.Article);

			string[] tegs = newArticle.Tegs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string teg in tegs)
			{
				int id_teg = dataBaseTeg.Insert("#" + teg);
				dataBaseArticles_Tegs.Insert(id_article, id_teg);
			}

			DataBaseCategories dataBaseCategory = new DataBaseCategories();
			NewArticle newArticle1 = new NewArticle
			{
				Categories = new SelectList(dataBaseCategory.Select(), "Category_Id", "Name")
			};

			return RedirectToAction("Index", "Home");
		}
    }
}