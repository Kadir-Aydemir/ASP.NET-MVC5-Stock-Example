using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC5_Stock_Example.Models.Entity;

namespace ASP.NET_MVC5_Stock_Example.Controllers
{
    public class AdminController : Controller
    {
        DbMvcStokEntities db=new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {            
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult newAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult newAdmin(ADMIN a)
        {
            db.ADMIN.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}