using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAppAPIFiles.Attribute
{
    public class EmailOrPhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string valueAsString = value.ToString();

            const string emailRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            bool isValidEmail = Regex.IsMatch(valueAsString, emailRegex);

            if (isValidEmail)
            {
                return ValidationResult.Success;
            }

            const string usaPhoneNumbersRegex = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
            bool isValidPhone = Regex.IsMatch(valueAsString, usaPhoneNumbersRegex);

            if (isValidPhone)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid email or phone number.");
        }
    }
}
