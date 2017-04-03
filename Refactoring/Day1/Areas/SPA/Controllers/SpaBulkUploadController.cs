using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessEntities;
using BusinessLayer;
using Day1.Filters;
using ViewModel.SPA;

namespace Day1.Areas.SPA.Controllers
{
    public class SpaBulkUploadController : AsyncController
    {
        [AdminFilter]
        public ActionResult Index()
        {
            return PartialView("Index");
        }

        [AdminFilter]
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            int t1 = Thread.CurrentThread.ManagedThreadId;
            var employees = await Task.Factory.StartNew<List<Employee>>(() => GetEmployees(model));
            int t2 = Thread.CurrentThread.ManagedThreadId;
            var ebl = new EmployeeBusinessLayer();
            ebl.UploadEmployees(employees);

            var elvm = new EmployeeListViewModel();
            elvm.Employees = new List<EmployeeViewModel>();

            foreach (var emp in employees)
            {
                var evm = new EmployeeViewModel()
                {
                    EmployeeName = emp.FirstName + " " + emp.LastName,
                    Salary = "Salary not found",
                    SalaryColor = ""
                };
                if (emp.Salary.HasValue)
                {
                    evm.Salary = emp.Salary.Value.ToString("C");
                    evm.SalaryColor = emp.Salary > 1000 ? "yellow" : "green";
                }
                elvm.Employees.Add(evm);
            }
            return Json(elvm);
        }

        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
            var employees = new List<Employee>();
            var csvReader = new StreamReader(model.fileUpload.InputStream);
            csvReader.ReadLine();
            while (!csvReader.EndOfStream)
            {
                var line = csvReader.ReadLine();
                var values = line.Split(',');
                
                var e = new Employee()
                {
                    FirstName = values[0],
                    LastName = values[1],
                    Salary = Int32.Parse(values[2])
                };

                employees.Add(e);
            }
            return employees;
        }
    }
}