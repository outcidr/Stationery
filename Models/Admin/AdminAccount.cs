using System.ComponentModel.DataAnnotations;

namespace Stationery.Models.Admin
{
    public class AdminAccount
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }


}
