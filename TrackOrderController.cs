using PizzaHut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaHut.Controllers
{
    public class TrackOrderController : Controller
    {
        private BookstoreDbContext db = new BookstoreDbContext();
        // GET: TrackOrder
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string phoneNumber = fc["phone"].ToString();
            var listOrder = db.Orders.Where(m => m.deliveryphone.Equals(phoneNumber)).OrderByDescending(m=>m.ID).ToList();
            return View("listOrders",listOrder);
        }
        public ActionResult DetailOrder(int id)
        {
         
            var listOrder = db.Orders.Find(id);
            return View("DetailOrder", listOrder);
        }
        
    }
}