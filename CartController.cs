using PizzaHut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PizzaHut.Controllers
{
    public class CartController : Controller
    {
        // khởi tạo session:
        private const string SessionCart = "SessionCart";
        // GET: Cart
        BookstoreDbContext db = new BookstoreDbContext();
        public ActionResult Index()
        {
            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
            }
            else if (cart == null)
            {
                ViewBag.statusCart = "Giỏ hàng trống";

            }
            return View(list);
        }
        public ActionResult cart_header()
        {
            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            float priceTotol = 0;
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
                foreach (var item1 in list)
                {
                    if (item1.product.pricesale > 0)
                    {
                        int temp = (((int)item1.product.price) - ((int)item1.product.price / 100 * (int)item1.product.pricesale)) * ((int)item1.quantity);

                        priceTotol += temp;
                    }
                    else
                    {
                        int temp = (int)item1.product.price * (int)item1.quantity;
                        priceTotol += temp;
                    }
                }
            }
            ViewBag.CartTotal = priceTotol;
            return View("_cartHeader",list);
        }
        public RedirectToRouteResult updateitem(long P_SanPhamID, int P_quantity)
        {
            var cart = Session[SessionCart];
            var list = (List<Cart_item>)cart;
            Cart_item itemSua = list.FirstOrDefault(m => m.product.ID == P_SanPhamID);
            if (itemSua != null)
            {
                itemSua.quantity = P_quantity;
            }
            return RedirectToAction("Index");
        }
        public JsonResult deleteitem(long productID)
        {
            var item = new Cart_item();
            var cart = Session[SessionCart];
            var list = (List<Cart_item>)cart;
            Cart_item itemXoa = list.FirstOrDefault(m => m.product.ID == productID);
            if (itemXoa != null)
            {
                list.Remove(itemXoa);
                if (list.Count == 0)
                {
                    Session["SessionCart"] = null;
                }
            }
            item.countCart = list.Count();     
            int priceTotol = 0;
            foreach (var item1 in list)
            {
                if (item1.product.pricesale > 0)
                {
                    int temp = (((int)item1.product.price) - ((int)item1.product.price / 100 * (int)item1.product.pricesale)) * ((int)item1.quantity);
                    priceTotol += temp;
                }
                else
                {
                    int temp = (int)item1.product.price * (int)item1.quantity;
                    priceTotol += temp;
                }
            }
            item.priceTotal = priceTotol;
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Additem(long productID, int quantity)
        {
            var item = new Cart_item();
            Product product = db.Products.Find(productID);
            var cart = Session[SessionCart];
            if (cart != null)
            {
                var list = (List<Cart_item>)cart;
                if (list.Exists(m => m.product.ID == productID))
                {
                    int quantity1 = 0;
                    foreach (var item1 in list)
                    {
                        if (item1.product.ID == productID)
                        {
              
                                item1.quantity += quantity;
                                quantity1 = item1.quantity;
                                                    
                        }
                    }
                    int priceTotol = 0;                    
                    foreach (var item1 in list)
                    {
                        if (item1.product.pricesale > 0)
                        {
                            int temp = (((int)item1.product.price) - ((int)item1.product.price / 100 * (int)item1.product.pricesale)) * ((int)item1.quantity);
                           
                            priceTotol += temp;
                        }
                        else {
                            int temp = (int)item1.product.price * (int)item1.quantity;
                            priceTotol += temp;
                        }
                     
                    }
                    return Json(new
                    {  
                        ProductPrice = ((int)product.price) - (((int)product.price / 100) * ((int)product.pricesale)),
                        bad = false,
                        price = product.price,
                        priceSale = product.pricesale,
                        quantity = quantity1,
                        priceTotol = priceTotol,
                        productID = productID,
                        meThod = "updateQuantity"
                    }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    item.meThod = "cartExist";
                    item.f = false;

                        item.quantity = quantity;
                        list.Add(item);
                        item.product = product;
                        item.countCart = list.Count();
                        item.meThod = "cartExist";
                        int priceTotol = 0;
                        foreach (var item1 in list)
                        {
                            if (item1.product.pricesale > 0)
                            {
                                int temp = (((int)item1.product.price) - ((int)item1.product.price / 100 * (int)item1.product.pricesale)) * ((int)item1.quantity);
                                priceTotol += temp;
                            }
                            else
                            {
                                int temp = (int)item1.product.price * (int)item1.quantity;
                                priceTotol += temp;
                            }
                        
                        item.priceTotal = priceTotol;
                        item.priceSaleee = (int)product.price - (int)product.price / 100 * (int)product.pricesale;
                    }
                        return Json(item, JsonRequestBehavior.AllowGet);                    
                }
            }
            else
            {
                item.product = product;
                item.meThod = "cartEmpty";
                item.countCart = 1;
                item.f = false;
                    item.priceSaleee = (int)product.price - (int)product.price / 100 * (int)product.pricesale;
                    item.quantity = quantity;
                    var list = new List<Cart_item>();
                    list.Add(item);
                    Session[SessionCart] = list;
                    if (item.product.pricesale > 0)
                    {
                        item.priceTotal = (((int)item.product.price) - ((int)item.product.price / 100 * (int)item.product.pricesale)) * ((int)item.quantity);
                    }
                    else
                    {
                        item.priceTotal = (int)product.price;
                    }
                    item.priceTotal = (((int)item.product.price) - ((int)item.product.price / 100 * (int)item.product.pricesale)) * ((int)item.quantity);
                  
            }
            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}