namespace TechCommerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal totalPrice { get; set; }

        public ICollection<CartProducts> CaPr { get; set; } = new HashSet<CartProducts>();
        public Customer Customer { get; set; }
    }
}
