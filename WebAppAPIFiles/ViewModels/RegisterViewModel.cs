using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppAPIFiles.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Enter your Email")]
        [StringLength(50, ErrorMessage = "the field should be no more than 50 characters")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "The field must be filled")]
        public string Email { get; set; }

        [Display(Name = "Enter your phone number")]
        [StringLength(30, MinimumLength = 7, ErrorMessage = "Line length must be between 7 and 30 characters")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "The field must be filled")]

        public string Phone { get; set; }

        [Display(Name = "Enter your GivenName")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Line length must be between 2 and 50 characters")]
        [Required(ErrorMessage = "The field must be filled")]
        public string GivenName { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Line length must be between 6 and 100 characters")]
        [Required(ErrorMessage = "The field must be filled")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password entered incorrectly")]
        public string ConfirmPassword { get; set; }






    }
}
