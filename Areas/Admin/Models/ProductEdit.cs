using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stationery.Areas.Admin.Models
{
    public class ProductEdit
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
