using System.ComponentModel.DataAnnotations;

namespace Stationery.Areas.Admin.Models
{
    public class CategoryEditViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
    }
}
