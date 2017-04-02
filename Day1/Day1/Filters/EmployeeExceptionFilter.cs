using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Day1.Logger;

namespace Day1.Filters
{
    public class EmployeeExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var logger = new FileLogger();
            logger.LogException(filterContext.Exception);

            // To be returned with default error page
            //base.OnException(filterContext);

            // TO be returned with custom error page
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ContentResult()
            {
                  Content = "Sorry for the Error"  
            };
        }
    }
}