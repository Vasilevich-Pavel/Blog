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

			DataBaseArticle dataBaseArticle = new DataBaseArticle();
			DataBaseCategory dataBaseCategory = new DataBaseCategory();

			OneArticle oneArticle = new OneArticle();
			oneArticle.Article = dataBaseArticle.Select(id);

			return View(oneArticle);

        }

		[HttpGet]
		public ActionResult Delete(int? id)
		{
			if (id == null) return RedirectToAction("Index", "Home");

			DataBaseArticle dataBaseArticle = new DataBaseArticle();
			DataBaseCategory dataBaseCategory = new DataBaseCategory();

			OneArticle oneArticle = new OneArticle();
			oneArticle.Article = dataBaseArticle.Select(id);

			return View(oneArticle);

		}

		[HttpGet]
		public ActionResult Edit(int? id)
		{
			if (id == null) return RedirectToAction("Index", "Home");

			DataBaseArticle dataBaseArticle = new DataBaseArticle();
			DataBaseCategory dataBaseCategory = new DataBaseCategory();

			OneArticle oneArticle = new OneArticle();
			oneArticle.Article = dataBaseArticle.Select(id);

			return View(oneArticle);

		}
	}
}