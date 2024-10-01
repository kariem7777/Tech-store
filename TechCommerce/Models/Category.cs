using System.ComponentModel.DataAnnotations;

namespace TechCommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name cannot be shorter than 2 characters or longer than 100 characters")]
        public required string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
