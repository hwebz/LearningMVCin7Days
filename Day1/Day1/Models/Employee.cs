using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Day1.Validations;

namespace Day1.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        //[Required(ErrorMessage = "Enter first name")]
        [FirstNameValidation]
        public string FirstName { get; set; }

        [StringLength(15, ErrorMessage = "Last name length should not be greater than 15")]
        public string LastName { get; set; }
        public int? Salary { get; set; } // int? for Nullable
    }
}