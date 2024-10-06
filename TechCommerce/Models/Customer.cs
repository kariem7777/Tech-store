using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TechCommerce.Models
{
    public class Customer : IdentityUser
    {

        [Required]
        public required int CartId { get; set; }
       
        [Required]
        public  String Firstname { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public Cart Cart { get; set; }
    }
}
