using PizzaHut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaHut.Controllers
{
    public class ModulesController : Controller
    {
        private BookstoreDbContext db = new BookstoreDbContext();
        public ActionResult Header()
        {
            ViewBag.username = null;
           Muser sessionUser = (Muser)Session[Common.CommonConstants.CUSTOMER_SESSION];
            if(sessionUser != null)
            {
                ViewBag.username = sessionUser.username;
            }
           
            return View("_header");
        }
        public ActionResult Category()
        {
            var listCate = db.Categorys.Where(m => m.status == 1).OrderBy(m => m.orders).ToList();
            return View("_category", listCate);
        }
        
             public ActionResult Mobile_category()
        {
            var listCate = db.Categorys.Where(m => m.status == 1).OrderBy(m => m.orders).ToList();
            return View("_mobie_category", listCate);
        }
        public ActionResult Mainmenu()
        {
            var listParentCate = db.Menus.Where(m => m.status == 1 && m.parentid ==0).OrderBy(m => m.orders).ToList();
            return View("_mainmenu", listParentCate);
        }
        public ActionResult mobileSubMainmenu()
        {
            var listParentCate = db.Menus.Where(m => m.status == 1 && m.parentid == 0).OrderBy(m => m.orders).ToList();
            return View("_mobile-mainmenu", listParentCate);
        }
        public ActionResult submainmenu(int id)
        {
            ViewBag.mainmenuitem = db.Menus.Find(id);
            var list = db.Menus.Where(m => m.status == 1).Where(m => m.parentid == id)
                .OrderBy(m => m.orders);         
            if (list.Count() != 0)
            {
                return View("_submainmenu1", list);
            }
            else
            {
                return View("_submainmenu2");
            }

        }
        public ActionResult _mobile_submainmenu(int id)
        {
            ViewBag.mainmenuitem = db.Menus.Find(id);
            var list = db.Menus.Where(m => m.status == 1).Where(m => m.parentid == id)
                .OrderBy(m => m.orders);
            if (list.Count() != 0)
            {
                return View("_moblie_SubmainMenu1", list);
            }
            else
            {
                return View("_submainmenu2");
            }

        }

        public ActionResult MobileMainmenu()
        {
            return View("_mobile-mainmenu");
        }
        public ActionResult Slider()
        {
            var list = db.Sliders.Where(m => m.status == 1 && m.position== "SliderShow").OrderBy(m => m.orders).ToList();
            return View("_slider", list);
        }
        public ActionResult footer()
        {
            return View("_footer");
        }
    }
}