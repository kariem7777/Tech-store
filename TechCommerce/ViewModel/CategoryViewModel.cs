using System.ComponentModel.DataAnnotations;
using TechCommerce.Models;

namespace TechCommerce.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name cannot be shorter than 2 characters or longer than 100 characters")]
        public string Name { get; set; }

        public List<Product>? Products { get; set; }
    }
}
