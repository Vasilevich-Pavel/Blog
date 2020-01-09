using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class ArticlesController : Controller
    {
        [HttpGet]
        public ActionResult Article(int? id)
        {
			if (id == null) return RedirectToAction("Index", "Home");

			DataBaseArticles dataBaseArticle = new DataBaseArticles();
			DataBaseCategories dataBaseCategory = new DataBaseCategories();
			DataBaseArticles_Tegs dataBaseArticles_Tegs = new DataBaseArticles_Tegs();
			DataBaseTegs dataBaseTeg = new DataBaseTegs();

			OneArticle oneArticle = new OneArticle();
			oneArticle.Article = dataBaseArticle.Select(id);
			oneArticle.Category = dataBaseCategory.Select(oneArticle.Article.Category_Id);

			var id_tegs = dataBaseArticles_Tegs.SelectTegs(oneArticle.Article.Article_Id);

			foreach(var id_teg in id_tegs)
			{
				var teg = dataBaseTeg.Select(id_teg.Tegs_Id);
				oneArticle.Tegs.Add(teg);
			}

			return View(oneArticle);

        }

		[HttpGet]
		public ActionResult Delete(int? id)
		{
			if (id != null) {

				DataBaseArticles_Tegs dataBaseArticles_Tegs = new DataBaseArticles_Tegs();
				DataBaseArticles dataBaseArticle = new DataBaseArticles();

				dataBaseArticles_Tegs.DeleteArticle(id);
				dataBaseArticle.Delete(id);

				ModelState.AddModelError("", "Статья успешно удалена");
			}
			return RedirectToAction("Index", "Home");

		}

		[HttpGet]
		public ActionResult Edit(int? id)
		{
			if (id == null) return RedirectToAction("Index", "Home");

			DataBaseArticles dataBaseArticle = new DataBaseArticles();
			DataBaseCategories dataBaseCategory = new DataBaseCategories();
			DataBaseArticles_Tegs dataBaseArticles_Tegs = new DataBaseArticles_Tegs();
			DataBaseTegs dataBaseTeg = new DataBaseTegs();

			OneArticle oneArticle = new OneArticle();
			oneArticle.Article = dataBaseArticle.Select(id);
			oneArticle.Categories = new SelectList(dataBaseCategory.Select(), "Category_Id", "Name");
			oneArticle.Category = dataBaseCategory.Select(oneArticle.Article.Category_Id);

			var id_tegs = dataBaseArticles_Tegs.SelectTegs(oneArticle.Article.Article_Id);

			foreach (var id_teg in id_tegs)
			{
				var teg = dataBaseTeg.Select(id_teg.Tegs_Id);
				oneArticle.Teg += teg.Name;
			}

			return View(oneArticle);
		}

		[HttpPost]
		public ActionResult Edit(OneArticle oneArticle)
		{
			oneArticle.Article.DateTime = DateTime.Now;

			DataBaseArticles dataBaseArticles = new DataBaseArticles();
			dataBaseArticles.Update(oneArticle.Article);

			DataBaseTegs dataBaseTeg = new DataBaseTegs();
			DataBaseArticles_Tegs dataBaseArticles_Tegs = new DataBaseArticles_Tegs();

			string[] tegs = oneArticle.Teg.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
			dataBaseArticles_Tegs.DeleteArticle(oneArticle.Article.Article_Id);

			foreach (string teg in tegs)
			{
				int id_teg = dataBaseTeg.Insert("#" + teg);
				dataBaseArticles_Tegs.Insert(oneArticle.Article.Article_Id, id_teg);
			}

			ModelState.AddModelError("", "Статья успешно изменена");

			return Edit(oneArticle.Article.Article_Id);
		}

		[HttpGet]
		public ActionResult Tegs(int? id_teg)
		{
			if(id_teg == null) return RedirectToAction("Index", "Home");

			DataBaseTegs dataBaseTeg = new DataBaseTegs();
			DataBaseArticles_Tegs dataBaseArticles_Tegs = new DataBaseArticles_Tegs();
			DataBaseArticles dataBaseArticle = new DataBaseArticles();
			DataBaseCategories dataBaseCategory = new DataBaseCategories();

			var id_articles = dataBaseArticles_Tegs.SelectArticles(id_teg);

			List<OneArticle> fullArticles = new List<OneArticle>();

			foreach(var id_article in id_articles)
			{
				OneArticle oneArticle = new OneArticle();

				var id_tegs = dataBaseArticles_Tegs.SelectTegs(id_article.Articles_Id);

				foreach(var teg in id_tegs)
				{
					var tegs = dataBaseTeg.Select(teg.Tegs_Id);
					oneArticle.Tegs.Add(tegs);
				}

				oneArticle.Article = dataBaseArticle.Select(id_article.Articles_Id);
				oneArticle.Category = dataBaseCategory.Select(oneArticle.Article.Category_Id);
				fullArticles.Add(oneArticle);
			}

			return View(fullArticles);

		}
	}
}