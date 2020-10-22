using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marrwie.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "Açıklama")]
        [AllowHtml]
        public string Desciption { get; set; }

        [Display(Name = "Oluşturan")]
        public string CreatedUserId { get; set; }

        [Display(Name = "Oluşturan")]
        public string CreatedUserName { get; set; }

        [Display(Name = "Tarih")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Guncelleme")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [Display(Name = "Kategoriler")]
        public string ArticleCategoriesString { get; set; }
        public bool IsApproved { get; set; }
        public bool IsAuthorizedForEdit { get; set; }
        public bool IsAuthorizedForDelete { get; set; }
        public bool IsAuthorizedForDetails { get; set; }
        public bool IsAuthorizedForApprove { get; set; }

        [Display(Name = "Kategoriler")]
        public List<ArticleCategoriesViewModel> ArticleCategories { get; set; }

        public ArticleViewModel()
        {
            ArticleCategories = new List<ArticleCategoriesViewModel>();
        }
    }

    public class ArticleCategoriesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasGot { get; set; }
    }
}