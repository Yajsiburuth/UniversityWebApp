using System.ComponentModel.DataAnnotations;

namespace DAL.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "Email")]
        [Required (AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please a valid email format")]
        [DataType (DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be 6 characters minimum")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Required (AllowEmptyStrings = false, ErrorMessage = "You need to confirm your password")]
        [DataType (DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}