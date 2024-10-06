using System.ComponentModel.DataAnnotations;

namespace TechCommerce.Models
{
    public class Address
    {
        [Required]
        public int Id { get; set; }
        public String? Name { get; set; }
        [Required]
        public String MainSt { get; set; }
        public String? City { get; set; }

        public String? Country { get; set; }
        public String CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
