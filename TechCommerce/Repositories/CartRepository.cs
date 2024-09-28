using TechCommerce.Data;
using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public class CartRepository : Repository<Cart>
    {
        public CartRepository(ApplicationDbContext _context) : base(_context)
        {
        }
    }
}
