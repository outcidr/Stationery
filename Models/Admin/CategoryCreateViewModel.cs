using System.ComponentModel.DataAnnotations;

namespace Stationery.Models.Admin
{
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(50)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
    }
}
