using System.ComponentModel.DataAnnotations;

namespace Stationery.Areas.Admin.Models
{
    public class AdminRegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; } // Use Username for registration

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } // Use Email for registration

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        
    }
}