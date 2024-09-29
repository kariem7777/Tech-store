using Microsoft.AspNetCore.Mvc;
using TechCommerce.Models;

namespace TechCommerce.ViewModel
{
    public class OrdersPagertViewModel
    {
        public Pager? Pager { get; set; }


        public List<Order> orders = [];

        public int CustomerId { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();

    }
}
