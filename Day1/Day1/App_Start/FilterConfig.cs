using System;
using System.Web;
using System.Web.Mvc;
using Day1.Filters;

namespace Day1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //[HandleError(View="DivideError", ExceptionType=typeof(DivideByZeroException))]
            //[HandleError(View="NotFiniteError", Exception=typeof(NotFiniteNumberException))]
            //[HandleError]
            //filters.Add(new HandleErrorAttribute()
            //{
            //    ExceptionType = typeof(DivideByZeroException),
            //    View = "DivideError"
            //});
            //filters.Add(new HandleErrorAttribute()
            //{
            //    ExceptionType = typeof(NotFiniteNumberException),
            //    View = "NotFiniteError"
            //});
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new EmployeeExceptionFilter());
            filters.Add(new AuthorizeAttribute());
            // filters.Add(new AuthorizeAttribute()); // Register Authorize for all application
        }
    }
}
