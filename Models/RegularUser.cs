namespace Stationery.Models
{
    public class RegularUser : User
    {
        public DateTime DateOfBirth { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
