namespace TechCommerce.Models
{
    public class CartProducts
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }

    }
}
