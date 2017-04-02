using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            /*
             *  [Route("Employee/List/{id:int}")]
                We can have following constraints

                {x:alpha} – string validation
                {x:bool} – Boolean validation
                {x:datetime} – Date Time validation
                {x:decimal} – Decimal validation
                {x:double} – 64 bit float point value validation
                {x:float} – 32 bit float point value validation
                {x:guid} – GUID validation
                {x:length(6)} –length validation
                {x:length(1,20)} – Min and Max length validation
                {x:long} – 64 int validation
                {x:max(10)} – Max integer number validation
                {x:maxlength(10)} – Max length validation
                {x:min(10)} – Min Integer number validation
                {x:minlength(10)} – Min length validation
                {x:range(10,50)} – Integer range validation
                {x:regex(SomeRegularExpression)} – Regular Expression validation
             */
        }
    }
}
