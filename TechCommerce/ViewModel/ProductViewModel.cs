using System.ComponentModel.DataAnnotations;
using TechCommerce.Models;

namespace TechCommerce.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name cannot be shorter than 2 characters or longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Units is required")]
        [Range(0, 1000, ErrorMessage = "Units must be between 0 and 1000")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Units must be a number")]
        public int? Units { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 30000, ErrorMessage = "Price must be between 0 and 30000")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Description cannot be shorter than 5 characters or longer than 300 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? ImageUrl { get; set; }


        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
