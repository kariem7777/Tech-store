namespace TechCommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        public required String State {get;set; }

        public int CustomerId { get; set; }
        public required Customer Customer { get; set; }

        public ICollection<OrderProduct> OrPr { get; set; } = new HashSet<OrderProduct>();
    }
}
