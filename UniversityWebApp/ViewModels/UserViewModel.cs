using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityWebApp.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "Email")]
        [Required (AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [DataType (DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be 6 characters minimum")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required (AllowEmptyStrings = false, ErrorMessage = "You need to confirm your password")]
        [DataType (DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}