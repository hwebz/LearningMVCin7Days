using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessEntities;
using BusinessLayer;
using Day1.Filters;
using ViewModel;
using ViewModel.SPA;
using CreateEmployeeViewModel = ViewModel.SPA.CreateEmployeeViewModel;
using EmployeeListViewModel = ViewModel.SPA.EmployeeListViewModel;
using EmployeeViewModel = ViewModel.SPA.EmployeeViewModel;

namespace Day1.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        // GET: SPA/Main
        public ActionResult Index()
        {
            var mvm = new MainViewModel()
            {
                UserName = User.Identity.Name,
                FooterData = new FooterViewModel()
                {
                    CompanyName = "CompanyNameWillPutInHereForSPA",
                    Year = DateTime.Now.Year.ToString()
                }
            };
            return View("Index", mvm);
        }

        public ActionResult EmployeeList()
        {
            var employeeListViewModel = new EmployeeListViewModel();
            var ebl = new EmployeeBusinessLayer();
            var employees = ebl.GetEmployees();
            var evml = new List<EmployeeViewModel>();

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
                evml.Add(evm);
            }
            employeeListViewModel.Employees = evml;
            return View("EmployeeList", employeeListViewModel);
        }

        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("_AddNewLink");
            }
            return new EmptyResult();
        }

        [AdminFilter]
        public ActionResult AddNew()
        {
            var v = new CreateEmployeeViewModel();
            return View("CreateEmployee", v);
        }

        [HttpPost]
        [AdminFilter]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmployee(Employee e)
        {
            var ebl = new EmployeeBusinessLayer();
            ebl.SaveEmployee(e);

            var evm = new EmployeeViewModel()
            {
                EmployeeName = e.FirstName + " " + e.LastName,
                Salary = "No Data Found",
                SalaryColor = ""
            };
            if (e.Salary.HasValue)
            {
                evm.Salary = e.Salary.Value.ToString("C");
                evm.SalaryColor = e.Salary > 1000 ? "yellow" : "green";
            }
            return Json(evm);
        }
    }
}