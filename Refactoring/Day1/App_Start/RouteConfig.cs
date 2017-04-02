using System.Web.Mvc;
using System.Web.Routing;

namespace Day1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes(); // allow to use attribute above each action. Eg: [Route("Employee/List")]

            routes.MapRoute(
                name: "Upload",
                url: "Employee/Upload",
                defaults: new { controller = "BulkUpload", action = "Index" }    
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
