using Blog.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Blog.Controllers
{
	public class CategoriesController : Controller
    {
        [HttpGet]
        public ActionResult Categories()
        {
			DataBaseCategories dataBaseCategory = new DataBaseCategories();
			ViewCategory viewCategory = new ViewCategory();
			viewCategory.Categories = dataBaseCategory.Select();

			return View(viewCategory);
        }

		[HttpPost]
		[Authorize]
		public ActionResult Categories(ViewCategory category)
		{
			DataBaseCategories dataBaseCategory = new DataBaseCategories();
			bool check = dataBaseCategory.Insert(category.NewCategory);

			ViewCategory viewCategory = new ViewCategory();
			viewCategory.Categories = dataBaseCategory.Select();

			Response.Redirect(Request.Path);
			if (check)
			{
				ModelState.AddModelError("", "Категория успешно добавлена");
			}
			else ModelState.AddModelError("", "Данная категория уже существует");
			return View(viewCategory);
		}

		[HttpGet]
		public ActionResult Category(int? id_category, int? id_page)
		{
			DataBaseArticles dataBaseArticle = new DataBaseArticles();
			DataBaseCategories dataBaseCategory = new DataBaseCategories();

			var articles = dataBaseArticle.SelectTop10Category(Convert.ToInt32(id_page), Convert.ToInt32(id_category));
			var category = dataBaseCategory.Select(id_category);

			List<OneArticle> fullArticles = new List<OneArticle>();

			bool check = false;
			try
			{
				if (articles[articles.Count - 1] == null)
				{
					articles.RemoveAt(articles.Count - 1);
					check = true;
				}
			}
			catch (NullReferenceException)
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

				fullArticles.Add(oneArticle);
			}

			ViewBag.index = ++id_page;
			ViewBag.end = 0;
			if (check)
			{
				ViewBag.end = 1;
			}

			return View(fullArticles);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Delete(int? id_category)
		{
			if(id_category == null)
				return RedirectToAction("Index", "Home");

			DataBaseCategories dataBaseCategory = new DataBaseCategories();
			int id_withoutCategory = dataBaseCategory.SelectWithoutCategory();

			DataBaseArticles dataBaseArticle = new DataBaseArticles();
			dataBaseArticle.UpdateCategory(id_withoutCategory, id_category);
			
			dataBaseCategory.Delete(id_category);

			return RedirectToAction("Categories");
		}

		[HttpGet]
		[Authorize]
		public ActionResult Edit(int? id_category)
		{

			DataBaseCategories dataBaseCategory = new DataBaseCategories();
			var category = dataBaseCategory.Select(Convert.ToInt32(id_category));

			return View(category);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Edit(Category category)
		{
			DataBaseCategories dataBaseCategory = new DataBaseCategories();
			dataBaseCategory.Update(category);

			ModelState.AddModelError("", "Категория изменена успешно");

			return RedirectToAction("Categories");
		}


	}
}