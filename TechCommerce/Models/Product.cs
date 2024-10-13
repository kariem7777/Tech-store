using System.ComponentModel.DataAnnotations;

namespace TechCommerce.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public int Units { get; set; }
        [Required]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public String? Description { get; set; }

        public int CategoryId { get; set; }
        public Category category { get; set; }

        public ICollection<CartProducts> CaPr { get; set; } = new HashSet<CartProducts>();
        public ICollection<OrderProduct> OrPr { get; set; } = new HashSet<OrderProduct>();
        public ICollection<ProductRate> Prate { get; set; } = new HashSet<ProductRate>();


    }
}
