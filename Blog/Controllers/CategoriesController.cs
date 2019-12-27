using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing;

namespace Blog.Controllers
{
    public class CategoriesController : Controller
    {
        [HttpGet]
        public ActionResult Categories()
        {
			DataBaseCategory dataBaseCategory = new DataBaseCategory();
			var categories = dataBaseCategory.Select();

            return View(categories);
        }

		[HttpGet]
		public ActionResult Category(int? id_category, int? id_page)
		{
			DataBaseArticle dataBaseArticle = new DataBaseArticle();
			DataBaseCategory dataBaseCategory = new DataBaseCategory();
			var articles = dataBaseArticle.SelectByCategory(id_category, id_page);
			var category = dataBaseCategory.Select(id_category);

			List<OneArticle> fullArticles = new List<OneArticle>();

			for (int i = (Convert.ToInt32(id_page) - 1) * 10; i < id_page * 10 && i < articles.Count; i++)
			{
				OneArticle oneArticle = new OneArticle
				{
					Article = articles[articles.Count - i - 1],
					Category = category
				};

				fullArticles.Add(oneArticle);
			}
			ViewBag.index = ++id_page;

			return View(fullArticles);
		}

    }
}