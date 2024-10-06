namespace TechCommerce.ViewModel
{
    public class CartProductViewModel
    {
        public int ProductId { get; set; } 
        public string Name { get; set; } 
        public int Units { get; set; }
        public decimal Price { get; set; } 
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; } 
        public int Quantity { get; set; } 
    }
}
