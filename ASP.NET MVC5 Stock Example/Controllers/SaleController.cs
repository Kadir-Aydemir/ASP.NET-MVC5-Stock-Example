using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC5_Stock_Example.Models.Entity;

namespace ASP.NET_MVC5_Stock_Example.Controllers
{
    public class SaleController : Controller
    {
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var sls = db.SALES.Where(x => x.REMOVE == false).ToList();
            return View(sls);
        }

        [Authorize]
        [HttpGet]
        public ActionResult newSale()
        {
            List<SelectListItem> prd = (from x in db.PRODUCT.Where(x => x.REMOVE == false).ToList()
                                    select new SelectListItem
                                    {
                                        Text = x.NAME,
                                        Value = x.ID.ToString()
                                    }).ToList();
            ViewBag.prdct=prd;

            List<SelectListItem> emp = (from x in db.EMPLOYES.Where(x => x.REMOVE == false).ToList()
                                        select new SelectListItem
                                        {
                                            Text=x.NAME + " " + x.SURNAME,
                                            Value =x.ID.ToString()
                                        }).ToList();
            ViewBag.emply = emp;

            List<SelectListItem> cus = (from x in db.CUSTOMERS.Where(x => x.REMOVE == false).ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.NAME+" "+x.SURNAME,
                                            Value = x.ID.ToString()
                                        }).ToList();
            ViewBag.cstmr = cus;

            return View();
        }

        [HttpPost]
        public ActionResult newSale(SALES s)
        {
            var prd = db.PRODUCT.Where(x => x.ID == s.PRODUCT1.ID).FirstOrDefault();
            s.PRODUCT1=prd;
            var emp = db.EMPLOYES.Where(x => x.ID == s.EMPLOYES.ID).FirstOrDefault();
            s.EMPLOYES = emp;
            var cst = db.CUSTOMERS.Where(x => x.ID == s.CUSTOMERS.ID).FirstOrDefault();
            s.CUSTOMERS = cst;
            s.DATE =DateTime.Parse(DateTime.Now.ToShortDateString());
            s.REMOVE = false;
            db.SALES.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}