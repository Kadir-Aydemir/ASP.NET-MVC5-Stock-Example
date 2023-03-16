using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC5_Stock_Example.Models.Entity;

namespace ASP.NET_MVC5_Stock_Example.Controllers
{
    public class EmployeController : Controller
    {
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var emp = db.EMPLOYES.Where(x => x.REMOVE == false).ToList();
            return View(emp);
        }

        [Authorize]
        [HttpGet]
        public ActionResult newEmploye()
        {
            return View();
        }

        [HttpPost]
        public ActionResult newEmploye(EMPLOYES e)
        {
            e.REMOVE = false;
            db.EMPLOYES.Add(e);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(EMPLOYES e)
        {
            var emp = db.EMPLOYES.Find(e.ID);
            emp.REMOVE = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult updatePage(int id)
        {
            var emp = db.EMPLOYES.Find(id);
            return View("updatePage",emp);
        }

        public ActionResult updateEmploye(EMPLOYES e)
        {
            var emp = db.EMPLOYES.Find(e.ID);
            emp.NAME = e.NAME;
            emp.SURNAME = e.SURNAME;
            emp.DEPARTMENT = e.DEPARTMENT;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}