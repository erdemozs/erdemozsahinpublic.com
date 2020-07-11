using Marrwie.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marrwie.Controllers
{
    [Authorize(Roles ="admin")]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        public UserController()
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        public ActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
    }
}