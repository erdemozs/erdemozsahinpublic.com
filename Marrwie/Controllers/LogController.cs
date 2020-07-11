using Marrwie.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marrwie.Controllers
{
    public class LogController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var path = Server.MapPath("~\\Store");
            var fullPath = FileHelper.CheckLogFile(path);
            ViewBag.Logs = System.IO.File.ReadAllText(fullPath);
            return View();
        }
    }
}