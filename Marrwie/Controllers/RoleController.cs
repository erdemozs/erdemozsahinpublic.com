using Marrwie.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Marrwie.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private RoleManager<ApplicationRole> _roleManager;

        public RoleController()
        {
            _roleManager = new Microsoft.AspNet.Identity.RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new ApplicationDbContext()));
        }

        public ActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        [Authorize(Roles ="admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new ApplicationRole() { Name = model.Name, Description = model.Description });
                return RedirectToAction("Index");
            }
            
            return View(model);
        }


    }
}