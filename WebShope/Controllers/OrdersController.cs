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
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {

            var userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Include("MyCart.Product").Include("MyOrders.OrderItems").SingleOrDefault(u => u.Id == userId);

            if (user.MyOrders.Count < 1)
            {
                return RedirectToAction("Index","Home");
            }
            return View(user.MyOrders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id=0)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include("OrderItems").Include("OrderItems.Product").FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult PlaceOrder()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Include("MyCart.Product").Include("MyOrders.OrderItems").SingleOrDefault(u => u.Id == userId);

            if (user.MyCart.Count < 1)
            {
                return RedirectToAction("Index","Carts");
            }

            Order order = new Order();
            order.OrderDate = DateTime.Now;

            foreach (var item in user.MyCart)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.Price = item.Product.Price;
                orderItem.Product = item.Product;
                orderItem.Amount = item.Amount;
                order.OrderItems.Add(orderItem);
            }

            user.MyOrders.Add(order);
            user.MyCart.Clear();
            db.SaveChanges();

            return View(order);
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
