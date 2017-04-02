using System;
using System.Web.Mvc;
using BusinessEntities;

namespace Day1.ModelBinder
{
    public class EmployeeModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            var e = new Employee();
            e.FirstName = controllerContext.RequestContext.HttpContext.Request.Form["FName"];
            e.LastName = controllerContext.RequestContext.HttpContext.Request.Form["LName"];
            e.Salary = int.Parse(controllerContext.RequestContext.HttpContext.Request.Form["Salary"]);

            return e;
        }
    }
}