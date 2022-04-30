using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();   // For creating Attribute routing


            routes.MapRoute(
                "CustomersController3",
                "Customer/New",
                new { Controller = "Customers", action = "New"}
            );
            routes.MapRoute(
                "CustomersController2",
                "Customer/Index",
                new { Controller = "Customers", action = "Index"}
            );
            routes.MapRoute(
                "CustomersController",
                "Customer/Detail/{Id}",
                new { Controller = "Customers", action = "Detail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "MoviesController3",
                url: "movies/New",
                defaults: new { Controller = "Movies", action = "New"}
            );
            routes.MapRoute(
                name:"MoviesController2",
                url:"movies/Details/{Id}",
                defaults: new { Controller = "Movies", action = "Detail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "MoviesController1",
                url: "movies/Moview",
                defaults: new { Controller = "Movies", action = "Moview" }
             );

            //routes.MapRoute(
            //    "MoviesByReleaseDate",
            //    "movies/released/{year}/{month}",
            //    new { Controller = "Movies", action = "ByReleaseDate" },
            //    new { year = @"\d{4}", month = @"\d{2}" });
            //new { year = @"2015|2016", month = @"\d{2}" });
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

        }
    }
}
