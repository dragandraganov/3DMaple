using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using _3DMapleSystem.Web.Infrastructure.Helpers;

namespace _3DMapleSystem.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "Categories",
                url: "Categories/{name}",
                defaults: new { controller = "Categories", action = "Index" },
                namespaces: new[] { "_3DMapleSystem.Web.Controllers" }
                );

            routes.MapRoute(
                name: "Rating",
                url: "Ratings/{action}",
                defaults: new { controller = "Ratings", action = "Vote" },
                namespaces: new[] { "_3DMapleSystem.Web.Controllers" }
                );

            routes.MapRoute(
                name: "SubCategories",
                url: "Categories/{categoryName}/{subCategoryName}",
                defaults: new { controller = "SubCategories", action = "Index" },
                namespaces: new[] { "_3DMapleSystem.Web.Controllers" }
                );

            routes.MapRoute(
               name: "PolyModelDetails",
               url: "3DModels/{id}/{title}",
               defaults: new { controller = "PolyModels", action = "Details" },
               namespaces: new[] { "_3DMapleSystem.Web.Controllers" }
               );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "home", action = "index", id = UrlParameter.Optional },
               namespaces: new[] { "_3DMapleSystem.Web.Controllers" }
           );
        }
    }
}
