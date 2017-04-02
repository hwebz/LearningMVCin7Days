using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using BusinessEntities;
using DataAccessLayer;

namespace BusinessLayer
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
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