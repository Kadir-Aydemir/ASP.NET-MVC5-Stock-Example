using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC5_Stock_Example.Models.Entity;

namespace ASP.NET_MVC5_Stock_Example.Controllers
{
    public class ProductController : Controller
    {
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(string s)
        {
            // var prd = db.PRODUCT.Where(x=>x.REMOVE==false).ToList();
            var prd = db.PRODUCT.Where(x => x.REMOVE == false);
            if (!string.IsNullOrEmpty(s))
            {
                prd = prd.Where(x => x.NAME.Contains(s) && x.REMOVE==false);
            }
            return View(prd.ToList());
        }

        [Authorize]
        [HttpGet]
        public ActionResult newProduct()
        {
            List<SelectListItem> ctg = (from x in db.CATEGORY.ToList()
                                        select new SelectListItem
                                        {
                                            Text=x.NAME,
                                            Value=x.ID.ToString(),
                                        }).ToList();
            ViewBag.ctglist = ctg;
            return View();
        }

        [HttpPost]
        public ActionResult newProduct(PRODUCT p)
        {   //to find category
            var ctg = db.CATEGORY.Where(x => x.ID == p.CATEGORY1.ID).FirstOrDefault();
            p.CATEGORY1 = ctg;

            p.REMOVE = false;
            db.PRODUCT.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult updatePage(int id)
        {
            List<SelectListItem> ctg = (from x in db.CATEGORY.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.NAME,
                                            Value = x.ID.ToString(),
                                        }).ToList();
            ViewBag.ctglist = ctg; //to category list

            var prd = db.PRODUCT.Find(id);
            return View("updatePage", prd);
        }

        public ActionResult updateProduct(PRODUCT p)
        {
            var ctg = db.CATEGORY.Where(x => x.ID == p.CATEGORY1.ID).FirstOrDefault();

            var prd = db.PRODUCT.Find(p.ID);
            prd.NAME = p.NAME;
            prd.BRAND = p.BRAND;
            prd.STOCK = p.STOCK;
            prd.PURCHASE = p.PURCHASE;
            prd.SALE=p.SALE;
            prd.CATEGORY = ctg.ID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(PRODUCT p)
        {
            var prd = db.PRODUCT.Find(p.ID);
            prd.REMOVE = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}