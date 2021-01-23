using Newtonsoft.Json.Linq;
using PizzaHut.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PizzaHut.Controllers
{
    public class CheckoutController : Controller
    {
        private const string SessionCart = "SessionCart";
        BookstoreDbContext db = new BookstoreDbContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
            }
            else
            {
                ViewBag.error = "Chưa có sản phẩm nào trong giỏ";
                return View(list);
            }
            ViewBag.error = "";
          
            return View(list);

        }
        [HttpPost]
        public ActionResult formCheckOut(FormCollection f)
        {
            //Muser user = (Muser)Session[Common.CommonConstants.CUSTOMER_SESSION];
            var tenKH = f["deliveryname"];
            var SDT = f["deliveryphone"];
            var email = f["deliveryemail"];
            var diaChi = f["deliveryaddress"];
            var TongTien = f["sumOrder"];
            Morder order = new Morder();
            order.deliveryname = tenKH;
            order.deliveryphone = SDT;
            order.deliveryemail = email;
            order.deliveryaddress = diaChi;
            order.updated_at = DateTime.Today;
            db.Orders.Add(order);
            db.SaveChanges();
            var cart = Session[SessionCart];
            var list1 = new List<Cart_item>();
            if (cart != null)
            {
                list1 = (List<Cart_item>)cart;
            }
            var orderid = db.Orders.OrderByDescending(m => m.ID).ToList()[0].ID;
            foreach(var item in list1)
            {
                Mordersdetail detail = new Mordersdetail();
                detail.orderid = orderid;
                detail.productid = item.product.ID;
                detail.price = item.product.price;
                detail.quantity = item.quantity;
                detail.priceSale = item.product.pricesale;
                detail.amount = (detail.price * (100 - detail.priceSale) / 100)*detail.quantity;
                db.Ordersdetails.Add(detail);
                db.SaveChanges();
            }
            return View("OrderComplete");
        }

    }
}