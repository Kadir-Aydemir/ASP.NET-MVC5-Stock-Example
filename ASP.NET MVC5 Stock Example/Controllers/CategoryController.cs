using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC5_Stock_Example.Models.Entity;

namespace ASP.NET_MVC5_Stock_Example.Controllers
{
    public class CategoryController : Controller
    {
        DbMvcStokEntities db=new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var ctg = db.CATEGORY.Where(x=>x.REMOVE==false).ToList();
            return View(ctg);
        }

        [Authorize]
        [HttpGet]
        public ActionResult newCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult newCategory(CATEGORY c)
        {
            c.REMOVE = false;
            db.CATEGORY.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(CATEGORY c)
        {
            var ctg = db.CATEGORY.Find(c.ID);
            ctg.REMOVE = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult updatePage(int id)
        {
            var ctg = db.CATEGORY.Find(id);
            return View("updatePage",ctg);
        }

        public ActionResult updateCategory(CATEGORY c)
        {
            var ctg = db.CATEGORY.Find(c.ID);
            ctg.NAME = c.NAME;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}