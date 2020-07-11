using Marrwie.Entities;
using Marrwie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marrwie.Controllers
{
    public class HomeController : Controller
    {
        private MarrwieDbContext db = new MarrwieDbContext();

        public ActionResult Index(int pageNumber=1)
        {
            var model = new HomeViewModel();
            model.Articles = new List<ArticleViewModel>();
            
            foreach(var art in db.Articles.OrderByDescending(p => p.Id).Skip((pageNumber - 1) * 10).Take(10).ToList())
            {
                var articleModel = new ArticleViewModel();
                articleModel.Id = art.Id;
                articleModel.CreatedDate = art.CreatedDate;
                articleModel.CreatedUserId = art.CreatedUserId;
                articleModel.CreatedUserName = art.CreatedUserName;
                articleModel.Desciption = art.Desciption;
                articleModel.Title = art.Title;
                articleModel.UpdatedDate = art.UpdatedDate;
                articleModel.ArticleCategoriesString = string.Join(",", art.Categories.Select(p => p.Name).ToList());
                model.Articles.Add(articleModel);
            }
            model.TotalPageNumber = db.Articles.Count() != 0 ? (int)Math.Ceiling(db.Articles.Count() / 10d):0;
            model.CurrentPageNumber = pageNumber;
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}