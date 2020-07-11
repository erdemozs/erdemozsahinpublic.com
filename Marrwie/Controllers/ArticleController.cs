using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Marrwie.Entities;
using Microsoft.AspNet.Identity;
using Marrwie.Models;

namespace Marrwie.Controllers
{
    [Authorize(Roles = "admin")]
    public class ArticleController : Controller
    {
        private MarrwieDbContext db = new MarrwieDbContext();

        public ActionResult Index()
        {
            var model = db.Articles.ToList().Select(p => new ArticleViewModel{ Id = p.Id, ArticleCategoriesString = string.Join(",", p.Categories.Select(t => t.Name).ToList()), CreatedDate = p.CreatedDate, CreatedUserId = p.CreatedUserId, CreatedUserName = p.CreatedUserName, Desciption = p.Desciption, Title = p.Title, UpdatedDate = p.UpdatedDate }).ToList();
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult GetArticlesForCategory(int id, int pageNumber=1)
        {
            var model = new HomeViewModel();
            model.Articles = new List<ArticleViewModel>();

            foreach (var art in db.Articles.Where(p => p.Categories.Select(t => t.Id).Contains(id)).ToList().OrderByDescending(p => p.Id).Skip((pageNumber - 1) * 10).Take(10).ToList())
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
            model.TotalPageNumber = db.Articles.Where(p => p.Categories.Select(t => t.Id).Contains(id)).ToList().Count() != 0 ? (int)Math.Ceiling(db.Articles.Where(p => p.Categories.Select(t => t.Id).Contains(id)).ToList().Count() / 10d) : 0;
            model.CurrentPageNumber = pageNumber;
            model.CategoryId = id;
            return View(model);
        }

        public ActionResult GetAllArticles()
        {
            var model = db.Articles.ToList().Select(p => new ArticleViewModel { Id = p.Id, ArticleCategoriesString = string.Join(",", p.Categories.Select(t => t.Name).ToList()), CreatedDate = p.CreatedDate, CreatedUserId = p.CreatedUserId, CreatedUserName = p.CreatedUserName, Desciption = p.Desciption, Title = p.Title, UpdatedDate = p.UpdatedDate }).ToList();
            return View(model);
        }

        public ActionResult GetArticlesOfUser(string id)
        {
            var model = db.Articles.Where(p => p.CreatedUserId == id).ToList().Select(p => new ArticleViewModel { Id = p.Id, ArticleCategoriesString = string.Join(",", p.Categories.Select(t => t.Name).ToList()), CreatedDate = p.CreatedDate, CreatedUserId = p.CreatedUserId, CreatedUserName = p.CreatedUserName, Desciption = p.Desciption, Title = p.Title, UpdatedDate = p.UpdatedDate }).ToList();
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult GetArticle(int id)
        {
            var article = db.Articles.Find(id);
            var model = new ArticleViewModel { ArticleCategoriesString = string.Join(",", article.Categories.ToList()), CreatedDate = article.CreatedDate, Id = article.Id, CreatedUserId = article.CreatedUserId, CreatedUserName = article.CreatedUserName, Desciption = article.Desciption, Title = article.Title, UpdatedDate = article.UpdatedDate };
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        public ActionResult Create()
        {
            var model = new ArticleViewModel();
            model.ArticleCategories = new List<ArticleCategoriesViewModel>();

            foreach (var cat in db.Categories)
                model.ArticleCategories.Add(new ArticleCategoriesViewModel() { Id = cat.Id, HasGot = false, Name = cat.Name });
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = new Article();
                article.CreatedUserId = User.Identity.GetUserId();
                article.CreatedUserName = User.Identity.GetUserName();
                article.CreatedDate = DateTime.Now;
                article.UpdatedDate = DateTime.Now;
                article.Desciption = model.Desciption;
                article.Title = model.Title;

                var addedCategoryIds = model.ArticleCategories.Where(p => p.HasGot).Select(p => p.Id).ToList().Except(article.Categories.Select(p => p.Id).ToList()).ToList();

                foreach (var addedCategoryId in addedCategoryIds)
                {
                    article.Categories.Add(db.Categories.Find(addedCategoryId));
                }

                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);

            var model = new ArticleViewModel();
            model.CreatedDate = article.CreatedDate;
            model.CreatedUserId = article.CreatedUserId;
            model.CreatedUserName = article.CreatedUserName;
            model.Desciption = article.Desciption;
            model.Id = article.Id;
            model.Title = article.Title;
            model.UpdatedDate = article.UpdatedDate;

            model.ArticleCategories = new List<ArticleCategoriesViewModel>();

            foreach (var cat in db.Categories.ToList())
            {
                if (article.Categories.Select(p => p.Id).Contains(cat.Id))
                    model.ArticleCategories.Add(new ArticleCategoriesViewModel() { Id = cat.Id, HasGot = true, Name = cat.Name });
                else
                    model.ArticleCategories.Add(new ArticleCategoriesViewModel() { Id = cat.Id, HasGot = false, Name = cat.Name });
            }
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = db.Articles.Find(model.Id);
                article.UpdatedDate = DateTime.Now;
                article.Desciption = model.Desciption;
                article.Title = model.Title;

                var addedCategoryIds = model.ArticleCategories.Where(p => p.HasGot).Select(p => p.Id).ToList().Except(article.Categories.Select(p => p.Id).ToList()).ToList();
                var deletedCategoryIds = article.Categories.Select(p => p.Id).ToList().Except(model.ArticleCategories.Where(p => p.HasGot).Select(p => p.Id).ToList()).ToList();

                foreach (var addedCategoryId in addedCategoryIds)
                {
                    article.Categories.Add(db.Categories.Find(addedCategoryId));
                }

                foreach (var deletedCategoryId in deletedCategoryIds)
                {
                    article.Categories.Remove(db.Categories.Find(deletedCategoryId));
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
