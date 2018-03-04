using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShope.Models;
using Microsoft.AspNet.Identity;

namespace WebShope.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Carts
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var uId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Include("MyCart").Include("MyCart.Product").SingleOrDefault(u => u.Id == uId);
            
            return View(user.MyCart);
        }

        public JsonResult AddToCart(Product product)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var uId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Include("MyCart").SingleOrDefault(u => u.Id == uId);

            bool notFound = true;

            foreach (var item in user.MyCart)
            {
                if (item.Product_Id == product.Id)
                {
                    item.Amount++;
                    notFound = false;
                    break;
                }
            }
            if (notFound)
            {
                Cart cart = new Cart();
                cart.UserAppId = uId;
                cart.Product_Id = product.Id;
                cart.Amount = 1;
                db.Carts.Add(cart);
            }
            db.SaveChanges();
            return Json("okay", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddToCartOrRemove (int id , string more)
        {
            var uId = User.Identity.GetUserId();
            ApplicationUser AppUser = db.Users.Include("MyCart").SingleOrDefault(u => u.Id == uId);

            foreach (var item in AppUser.MyCart)
            {
                if (item.Id == id)
                {
                    if (more == "+")
                    {
                        item.Amount++;
                    }
                    else if (item.Amount > 1)
                    {
                        item.Amount--;
                    }
                    else
                    {
                        AppUser.MyCart.Remove(item);
                    }
                    break;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
