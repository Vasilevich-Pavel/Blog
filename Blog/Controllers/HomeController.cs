using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
	public class HomeController : Controller
	{

		[HttpGet]
		public ActionResult Index(int id = 1)
		{
			DataBaseArticle dataBaseArticle = new DataBaseArticle();
			DataBaseCategory dataBaseCategory = new DataBaseCategory();

			var articles = dataBaseArticle.SelectTop10(id);
			List<OneArticle> fullArticles = new List<OneArticle>();

			foreach (var a in articles)
			{
				OneArticle oneArticle = new OneArticle();
				oneArticle.Article = a;
				oneArticle.Category = dataBaseCategory.Select(a.Category_Id);

				fullArticles.Add(oneArticle);
			}
			ViewBag.index = ++id;
				
			return View(fullArticles);
		}


		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}