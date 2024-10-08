using Microsoft.EntityFrameworkCore;
using TechCommerce.Data;
using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public class OrderProductRepository : GenericRepository<OrderProduct>, IOrderProductRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<OrderDetailsViewModel> GetOrderDetails(int orderId)
        {
            return _context.OrderProduct
                .Include(O => O.Product)
                .Where(O => O.OrderId == orderId)
                .Select(O => new OrderDetailsViewModel
                {
                    Quantity = O.Quantity,
                    Price = O.Price,
                    Name = O.Product.Name
                }).ToList();
        }
    }
}
