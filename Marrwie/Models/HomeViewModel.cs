using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marrwie.Models
{
    public class HomeViewModel
    {
        public List<ArticleViewModel> Articles { get; set; }
        public int TotalPageNumber { get; set; }
    }
}