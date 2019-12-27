using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class NewArticleController : Controller
    {
        [HttpGet]
        public ViewResult Create()
        {
			DataBaseCategory category = new DataBaseCategory();
			NewArticle newArticle = new NewArticle();
			newArticle.Categories = new SelectList(category.Select(), "Category_Id", "Name");
			return View(newArticle);
        }

		[HttpPost]
		public ViewResult Create(NewArticle newArticle)
		{
			//DataBaseCategory category = new DataBaseCategory();
			DataBaseArticle article = new DataBaseArticle();

			newArticle.Article.Category_Id = newArticle.CategoryId;
			newArticle.Article.DateTime = DateTime.Now;
			article.Insert(newArticle.Article);

			DataBaseCategory category = new DataBaseCategory();
			NewArticle newArticle1 = new NewArticle();
			newArticle1.Categories = new SelectList(category.Select(), "Category_Id", "Name");
			return View(newArticle1);
		}
    }
}