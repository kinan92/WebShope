using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShope.Models;

namespace WebShope.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public JsonResult ProductList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return Json(db.Products.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateProduct(Product product)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Products.Add(product);
            db.SaveChanges();
            return Json(product, JsonRequestBehavior.AllowGet); //add 
        }
        public JsonResult GetAuthStatus ()
        {
            if (User.Identity.IsAuthenticated is true)
            {
                return Json(true, JsonRequestBehavior.AllowGet); //add 
            }
            else 
            {
                return Json(false, JsonRequestBehavior.AllowGet); //add 
            }
        }
    }
}