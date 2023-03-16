using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC5_Stock_Example.Models.Entity;
using System.Web.Security;

namespace ASP.NET_MVC5_Stock_Example.Controllers
{
    public class LoginController : Controller
    {
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult LoginPanel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPanel(ADMIN a)
        {
            var info = db.ADMIN.FirstOrDefault(x => x.USER == a.USER && x.PASSWORD == a.PASSWORD);
            if (info != null)
            {
                FormsAuthentication.SetAuthCookie(info.USER, false);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View();
            }

        }
    }
}