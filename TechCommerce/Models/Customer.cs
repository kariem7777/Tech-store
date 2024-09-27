using System.ComponentModel.DataAnnotations;

namespace TechCommerce.Models
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public required int CartId { get; set; }
        public String? Address { get; set; }

        [EmailAddress]
        public required String Mail { get; set; }

        public int? Phone { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public Cart Cart { get; set; }
    }
}
