using System.ComponentModel.DataAnnotations;

namespace DAL.ViewModels
{
    public class LoginUserViewModel
    {
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please enter valid email format")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}