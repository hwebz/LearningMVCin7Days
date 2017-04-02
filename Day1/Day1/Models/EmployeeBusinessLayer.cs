using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Day1.DataAccessLayer;

namespace Day1.Models
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            //var employees = new List<Employee>();
            //var e = new Employee
            //{
            //    FirstName = "Johnson",
            //    LastName = "Fernandes",
            //    Salary = 900
            //};
            //employees.Add(e);

            //e = new Employee
            //{
            //    FirstName = "Micheal",
            //    LastName = "Jackson",
            //    Salary = 1200
            //};
            //employees.Add(e);

            //e = new Employee
            //{
            //    FirstName = "Robert",
            //    LastName = "Pattingson",
            //    Salary = 600
            //};
            //employees.Add(e);

            //return employees;

            // Using DB
            var salesDal = new SalesERPDAL();
            return salesDal.Employees.ToList();
        }

        public Employee SaveEmployee(Employee e)
        {
            var db = new SalesERPDAL();
            db.Employees.Add(e);
            db.SaveChanges();
            return e;
        }

        /*public bool IsValidUser(UserDetails u)
        {
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return true;
            } else
            {
                return false;
            }
        }*/

        public UserStatus GetUserValidity(UserDetails u)
        {
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            if (u.UserName == "m4nh4" && u.Password == "m4nh4")
            {
                return UserStatus.AuthenticatedUser;
            }
            return UserStatus.NonAuthenticatedUser;
        }

        public void UploadEmployees(List<Employee> employees)
        {
            try
            {
                var salesDal = new SalesERPDAL();
                salesDal.Employees.AddRange(employees);
                salesDal.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                // Make change in CSV file to generate exception
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }
    }
}