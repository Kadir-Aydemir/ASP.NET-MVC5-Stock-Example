using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC5_Stock_Example.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace ASP.NET_MVC5_Stock_Example.Controllers
{
    public class CustomerController : Controller
    {
       DbMvcStokEntities db=new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(int page=1)
        {
          //  var cus = db.CUSTOMERS.Where(x => x.REMOVE == false).ToList();
            var cus = db.CUSTOMERS.Where(x => x.REMOVE == false).ToList().ToPagedList(page,3);
            return View(cus);
        }

        [Authorize]
        [HttpGet]
        public ActionResult newCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult newCustomer(CUSTOMERS c)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            c.REMOVE = false;
            db.CUSTOMERS.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(CUSTOMERS c)
        {
            var cus = db.CUSTOMERS.Find(c.ID);
            cus.REMOVE = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult updatePage(int id)
        {
            var cus = db.CUSTOMERS.Find(id);
            return View("updatePage",cus);
        }

        public ActionResult updateCustomer(CUSTOMERS c)
        {
            var cus = db.CUSTOMERS.Find(c.ID);
            cus.NAME = c.NAME;
            cus.SURNAME=c.SURNAME;
            cus.CITY = c.CITY;
            cus.BALANCE = c.BALANCE;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}