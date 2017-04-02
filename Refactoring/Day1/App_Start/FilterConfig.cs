using System.Web.Mvc;
using Day1.Filters;

namespace Day1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new EmployeeExceptionFilter());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
