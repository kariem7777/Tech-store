using TechCommerce.Data;
using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(ApplicationDbContext _context) : base(_context)
        {
        }

    }
}
