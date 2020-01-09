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
			DataBaseArticles dataBaseArticles = new DataBaseArticles();
			DataBaseCategories dataBaseCategory = new DataBaseCategories();

			var articles = dataBaseArticles.SelectTop10(id);
			SearchModel searchModel = new SearchModel();

			bool check = false;
			try
			{
				if (articles.Last() == null)
				{
					articles.RemoveAt(articles.Count - 1);
					check = true;
				}
			} 
			catch (ArgumentNullException)
			{
				return RedirectToAction("Index", "Home");
			}

			foreach (var a in articles)
			{
				OneArticle oneArticle = new OneArticle
				{
					Article = a,
					Category = dataBaseCategory.Select(a.Category_Id)
				};

				searchModel.OneArticles.Add(oneArticle);
			}

			searchModel.Categories = new SelectList(dataBaseCategory.Select(), "Category_Id", "Name");

			ViewBag.index = ++id;
			ViewBag.end = 0;
			if (check)
			{
				ViewBag.end = 1;
			}

			return View(searchModel);
		}

		[HttpPost]
		public ActionResult Search(SearchModel model)
		{
			DataBaseArticles dataBaseArticle = new DataBaseArticles();

			var articles = dataBaseArticle.Select(model);
			var finalArticles = new List<Article>();

			if(model.Tegs == null)
			{
				return View(GetArticles(articles));
			}
			else
			{
				return View(GetArticles(articles, model.Tegs));
			}
		}

		private List<OneArticle> GetArticles(List<Article> articles, string tegsModel)
		{
			DataBaseArticles_Tegs dataBaseArticles_Tegs = new DataBaseArticles_Tegs();
			DataBaseTegs dataBaseTegs = new DataBaseTegs();
			DataBaseCategories dataBaseCategories = new DataBaseCategories();

			string[] tegs = tegsModel.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

			List<OneArticle> oneArticles = new List<OneArticle>();
			foreach (var a in articles)
			{
				bool check = false;
				OneArticle oneArticle = new OneArticle();
				var id_teg = dataBaseArticles_Tegs.SelectTegs(a.Article_Id);

				List<Teg> listTegs = new List<Teg>();

				foreach (var teg in id_teg)
				{
					var getTegs = dataBaseTegs.Select(teg.Tegs_Id);
					listTegs.Add(getTegs);

					if (tegs.Contains(getTegs.Name.Substring(1)))
					{
						check = true;
					}
				}

				if (check)
				{
					oneArticle.Article = a;
					oneArticle.Category = dataBaseCategories.Select(a.Category_Id);
					oneArticle.Tegs = listTegs;
				}

				oneArticles.Add(oneArticle);
			}

			try
			{
				oneArticles.Reverse();	
			}
			catch (Exception)
			{}

			return oneArticles;
		}

		private List<OneArticle> GetArticles(List<Article> articles)
		{
			DataBaseArticles_Tegs dataBaseArticles_Tegs = new DataBaseArticles_Tegs();
			DataBaseTegs dataBaseTegs = new DataBaseTegs();
			DataBaseCategories dataBaseCategories = new DataBaseCategories();

			List<OneArticle> oneArticles = new List<OneArticle>();
			foreach (var a in articles)
			{
				OneArticle oneArticle = new OneArticle();
				oneArticle.Article = a;
				oneArticle.Category = dataBaseCategories.Select(a.Category_Id);

				var id_teg = dataBaseArticles_Tegs.SelectTegs(a.Article_Id);

				foreach (var teg in id_teg)
				{
					oneArticle.Tegs.Add(dataBaseTegs.Select(teg.Tegs_Id));
				}

				oneArticles.Add(oneArticle);
			}

			return oneArticles;
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Персональные данные";

			return View();
		}
	}
}