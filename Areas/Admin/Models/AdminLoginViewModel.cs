using System.ComponentModel.DataAnnotations;

namespace Stationery.Areas.Admin.Models
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "Username/Email is required")]
        public string UsernameOrEmail { get; set; } // We will use UsernameOrEmail for login

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}