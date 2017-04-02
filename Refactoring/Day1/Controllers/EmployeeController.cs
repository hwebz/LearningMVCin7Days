using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessEntities;
using BusinessLayer;
using Day1.Filters;
using ViewModel;

namespace Day1.Controllers
{
    public class Customer
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return "Name: " + this.CustomerName + ", Address: " + this.Address;
        }
    }
    [Authorize]
    public class EmployeeController : Controller
    {
        public string GetString()
        {
            return "Hello World is old now. It's time for wassup bro ;)";
        }

        public Customer GetCustomer()
        {
            var c = new Customer();
            c.CustomerName = "John Doe";
            c.Address = "1832 Washington DC Street";
            return c;
        }

        [NonAction]
        public string SimpleMethod()
        {
            return "Hi, I am not a action method";
        }

        [HeaderFooterFilter]
        [Route("Employee/Single")]
        public ActionResult Index()
        {
            var e = new Employee
            {
                FirstName = "Micheal",
                LastName = "Jackson",
                Salary = 1200
            };
            var evm = new EmployeeViewModel
            {
                EmployeeName = e.FirstName + " " + e.LastName,
                SalaryColor = "",
                UserName = User.Identity.Name
            };
            if (e.Salary.HasValue)
            {
                evm.Salary = e.Salary.Value.ToString("C");
                evm.SalaryColor = e.Salary > 1000 ? "yellow" : "green";
            }
            return View("MyView", evm);
        }
        public ActionResult GetEmployees()
        {
            var elvm = new EmployeeListViewModel();
            var ebl = new EmployeeBusinessLayer();
            var employees = ebl.GetEmployees();
            var levm = new List<EmployeeViewModel>();

            foreach (var e in employees)
            {
                var evm = new EmployeeViewModel
                {
                    EmployeeName = e.FirstName + " " + e.LastName,
                    Salary = "No data found",
                    SalaryColor = ""
                };
                if (e.Salary.HasValue)
                {
                    evm.Salary = e.Salary.Value.ToString("C");
                    evm.SalaryColor = e.Salary > 1000 ? "yellow" : "green";
                }
                levm.Add(evm);
            }
            elvm.Employees = levm;
            //elvm.UserName = "Administrator";
            elvm.FooterData = new FooterViewModel();
            elvm.FooterData.CompanyName = "ExampleCompanyName";
            elvm.FooterData.Year = DateTime.Now.Year.ToString();

            return View("MyEmployeeList", elvm);
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {
            return View("CreateEmployee", new CreateEmployeeViewModel());
        }
        
        [AdminFilter]
        [ValidateAntiForgeryToken]
        [HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee e, string btnSubmit)
        {
            switch (btnSubmit)
            {
                case "Create":
                    if (ModelState.IsValid)
                    {
                        var ebl = new EmployeeBusinessLayer();
                        ebl.SaveEmployee(e);
                    }
                    else
                    {
                        var cevm = new CreateEmployeeViewModel();
                        cevm.FirstName = e.FirstName;
                        cevm.LastName = e.LastName;
                        cevm.Salary = e.Salary.HasValue ? e.Salary.ToString() : ModelState["Salary"].Value.AttemptedValue;
                        return View("CreateEmployee", cevm);
                    }
                    return RedirectToAction("GetEmployees");
                case "Cancel":
                    return RedirectToAction("GetEmployees");
            }
            return new EmptyResult();
        }

        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("_AddNewLink");
            }
            return new EmptyResult();
        }
    }
}