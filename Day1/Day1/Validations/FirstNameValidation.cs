using System.ComponentModel.DataAnnotations;

namespace Day1.Validations
{
    public class FirstNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) // Checking for empty value
            {
                return new ValidationResult("Please provide first name");
            }
            else
            {
                if (value.ToString().Contains("@"))
                {
                    return new ValidationResult("First name should not contain @");
                }
            }
            return ValidationResult.Success;
        }
    }
}