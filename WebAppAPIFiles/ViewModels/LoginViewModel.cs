using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAppAPIFiles.Attribute;

namespace WebAppAPIFiles.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The Email or Phone number must be filled")]
        [EmailOrPhoneNumber]
        public string EmailOrPhoneNumber { get; set; }

        [Required(ErrorMessage = "The Password must be filled")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
