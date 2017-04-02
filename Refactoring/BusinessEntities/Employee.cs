using System.ComponentModel.DataAnnotations;
using BusinessEntities.Validations;

namespace BusinessEntities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [FirstNameValidation]
        public string FirstName { get; set; }

        [StringLength(15, ErrorMessage = "Last name length should not be greater than 15")]
        public string LastName { get; set; }
        public int? Salary { get; set; }
    }
}