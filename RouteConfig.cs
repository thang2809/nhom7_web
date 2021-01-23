using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PizzaHut
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
           name: "tim kiem sp",
           url: "tim-kiem",
           defaults: new { controller = "Site", action = "SearchProduct", id = UrlParameter.Optional },
           new[] { "PizzaHut.Controllers" }
           );
            routes.MapRoute(
          name: "theo doi don hang",
          url: "theo-doi-don-hang",
          defaults: new { controller = "TrackOrder", action = "index", id = UrlParameter.Optional },
          new[] { "PizzaHut.Controllers" }
          );
            routes.MapRoute(
               name: "detail Order",
               url: "thong-tin-don-hang/{id}",
               defaults: new { controller = "TrackOrder", action = "DetailOrder", id = UrlParameter.Optional },
               new[] { "PizzaHut.Controllers" }
               );
            routes.MapRoute(
             name: "thongtin kh",
             url: "thong-tin-kh",
             defaults: new { controller = "Custommer", action = "index", id = UrlParameter.Optional },
             new[] { "PizzaHut.Controllers" }
             );
            routes.MapRoute(
            name: "thanh toan",
            url: "thanh-toan",
            defaults: new { controller = "Checkout", action = "Index", id = UrlParameter.Optional },
            new[] { "PizzaHut.Controllers" }
            );
            routes.MapRoute(
            name: "thanh toan thanh cong",
            url: "thanh-toan-thanh-cong",
            defaults: new { controller = "Checkout", action = "OrderComplete", id = UrlParameter.Optional },
            new[] { "PizzaHut.Controllers" }
            );
            //can doi


            routes.MapRoute(
        name: "them vao gio hang",
        url: "them-sp-gio-hang",
        defaults: new { controller = "Cart", action = "Additem", id = UrlParameter.Optional },
        new[] { "PizzaHut.Controllers" }
        );
            routes.MapRoute(
        name: "Gio hang",
        url: "gio-hang",
        defaults: new { controller = "Cart", action = "index", id = UrlParameter.Optional },
        new[] { "PizzaHut.Controllers" }
        );

            routes.MapRoute(
        name: "productOfCategory",
        url: "loai-sach/{slug}",
        defaults: new { controller = "Site", action = "productOfCategory", id = UrlParameter.Optional },
        new[] { "PizzaHut.Controllers" }
        );

            routes.MapRoute(
        name: "all-product",
        url: "san-pham",
        defaults: new { controller = "Site", action = "allProduct", id = UrlParameter.Optional },
        new[] { "PizzaHut.Controllers" }
        );
            routes.MapRoute(
        name: "productDetail",
        url: "san-pham/{slug}",
        defaults: new { controller = "Site", action = "ProductDetail", id = UrlParameter.Optional },
        new[] { "PizzaHut.Controllers" }
        );

            routes.MapRoute(
        name: "slug",
        url: "{slug}",
        defaults: new { controller = "Site", action = "index", id = UrlParameter.Optional },
        new[] { "PizzaHut.Controllers" }
        );

            routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
        );
        }
    }
}
