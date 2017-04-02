using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Day1.Filters;
using Day1.Models;
using Day1.ViewModel;

namespace Day1.Controllers
{
    public class BulkUploadController : AsyncController
    {
        // GET: BulkUpload
        [HeaderFooterFilter]
        [AdminFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }

        // [AdminFilter]
        //public ActionResult Upload(FileUploadViewModel model)
        //{
        //    var employees = GetEmployees(model);
        //    var ebl = new EmployeeBusinessLayer();
        //    ebl.UploadEmployees(employees);
        //    return RedirectToAction("Index", "Employee");
        //}

        [AdminFilter]
        [HandleError]
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            int t1 = Thread.CurrentThread.ManagedThreadId;
            var employees = await Task.Factory.StartNew<List<Employee>>(() => GetEmployees(model));
            int t2 = Thread.CurrentThread.ManagedThreadId;
            var bal = new EmployeeBusinessLayer();
            bal.UploadEmployees(employees);
            return RedirectToAction("GetEmployees", "Employee");
        }

        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
            var employees = new List<Employee>();
            var csvReader = new StreamReader(model.fileUpload.InputStream);
            csvReader.ReadLine(); // Assume first line is header
            while (!csvReader.EndOfStream)
            {
                var line = csvReader.ReadLine();
                var values = line.Split(',');
                var employee = new Employee()
                {
                    FirstName = values[0],
                    LastName = values[1],
                    Salary = int.Parse(values[2])
                };
                employees.Add(employee);
            }
            return employees;
        }
    }
}