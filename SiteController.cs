using PizzaHut.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace PizzaHut.Controllers
{
    public class SiteController : Controller
    {
        // GET: SIte
        // chi tiet bai viet
        private BookstoreDbContext db = new BookstoreDbContext();

        public ActionResult Index(String slug = "")
        {
            int page = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["page"]))
            {
                page = int.Parse(Request.QueryString["page"].ToString());
            }

            if (slug == "")
            {               
                return this.home();
            }
            else
            {
                var rowlink = db.Link.Where(m => m.slug == slug);
                if (rowlink.Count() > 0)
                {
                    var link = rowlink.First();
                    if (link.type == "ProductDetail" && link.tableId == 1)
                    {
                        return this.ProductDetail(slug);
                    }
                    else if (link.type == "category" && link.tableId == 2)
                    {

                        return this.productOfCategory(slug, page);
                    }
                   
                }
                else
                {
                    //slug k co tring ban link
                    return this.page404();
                }
                return this.page404();
            }
        }
        public ActionResult home()
        {

            return View("home");
        }

        public ActionResult page404()
        {
            return View("page404");
        }
        public ActionResult ProductDealOfDay()
        {
            var ProductDealOfDay = db.Products.Where(m => m.status == 1 && m.pricesale != 0).OrderByDescending(m => m.pricesale).Take(8).ToList();
            ViewBag.category = db.Categorys.Where(m => m.status == 1).ToList();
            var kmCaonhat = db.Products.Where(m => m.status == 1 && m.pricesale != 0).OrderByDescending(m => m.pricesale).First();
            ViewBag.discout = kmCaonhat.pricesale;
            return View("_product_dealOfDay", ProductDealOfDay);
        }

        public ActionResult ProductDetail(string slug)
        {
            var SingleProduct = db.Products.Where(m => m.status == 1 && m.slug == slug).First();
            ViewBag.category = db.Categorys.Find(SingleProduct.catid);
            return View("productDetail", SingleProduct);
        }

        public ActionResult reladProduct(int catId)
        {
            var list = db.Products.Where(m => m.status == 1 && m.catid == catId).Take(5).ToList();
            return View("reladProduct", list);
        }
        public ActionResult buyWithProduct(int catId, int productId)
        {
            var list = db.Products.Where(m => m.status == 1 && m.catid == catId)
                .Where(m => m.ID != productId)
                .OrderBy(m => Guid.NewGuid())
                .Take(2)
                .ToList();
            return View("buyWithProduct", list);
        }
        private ActionResult productOfCategory(string slug, int? page) 
        { 
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            Mcategory single = db.Categorys.Where(m => m.status == 1 && m.slug == slug).First();
            ViewBag.url = single.slug + "";
            ViewBag.blace = single.name;
            ViewBag.category = db.Categorys.Where(m => m.status == 1).ToList();
            var list = db.Products
                .Where(m => m.status == 1 && m.catid == single.ID)
                 .ToList();
            return View("allProduct", list.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult allProduct(int? page)
        {
            ViewBag.url = "san-pham";
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ViewBag.category = db.Categorys.Where(m => m.status == 1);
            var list = db.Products.Where(m => m.status == 1).ToList().ToPagedList(pageNumber, pageSize);
            ViewBag.allItem = db.Products.Where(m => m.status == 1).Count();
            return View("allProduct", list);
        }
        public ActionResult ProductNew()
        {
            var list = db.Products.Where(m => m.status == 1).OrderByDescending(m => m.ID).Take(8)
                .ToList();
            return View("reladProduct", list);
        }
        public ActionResult Bestseller()
        {
            var list = db.Products.Where(m => m.status == 1).OrderBy(m => m.ID).Take(8)
                .ToList();
            return View("reladProduct", list);
        }
        public ActionResult FlashSale()
        {
            ViewBag.category = db.Categorys.Where(m => m.status == 1).ToList();
            var list = db.Products.Where(m => m.status == 1).OrderByDescending(m => m.pricesale).Take(5)
                .ToList();
            return View("_flashSale", list);
        }
        public ActionResult SearchProduct(string keyw, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ViewBag.url = "tim-kiem/?keyw=" + keyw + "";
            @ViewBag.blace = keyw;
            ViewBag.category = db.Categorys.Where(m => m.status == 1).ToList();
            var list = db.Products.Where(m => m.status == 1 && m.name.Contains(keyw)).OrderBy(m => m.ID);
            return View("allProduct", list.ToList().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult homeCategory()
        {
            var list = db.Categorys.Where(m => m.status == 1).OrderBy(m => m.ID).Take(1)
                .ToList();
            return View("_homeBookOfCategory", list);
        }
        public ActionResult homeBookOfCategory(int catId)
        {
            var list = db.Products.Where(m => m.status == 1 && m.catid == catId).OrderByDescending(m => m.pricesale).Take(5)
                .ToList();
            return View("reladProduct", list);
        }
        //posst of home
       

    
    }
}