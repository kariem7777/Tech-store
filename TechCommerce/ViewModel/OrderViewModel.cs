using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TechCommerce.Controllers;
using TechCommerce.Models;

namespace TechCommerce.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 30000, ErrorMessage = "Price must be between 0 and 30000")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Price must be a number")]
        public int? Price { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        public String CustomerId { get; set; }
       

    }
}
