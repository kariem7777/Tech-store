namespace TechCommerce.Models
{
    public class ProductRate
    {
        public int Id { get; set; }

        public String UserId { get; set; }
        public string?  CustomerName { get; set; }   
        public string? CustomerComment   { get; set; }
        public int? Rate { get; set; }

        public int ProductId { get; set; }
        public Product? product { get; set; }
    }
}
