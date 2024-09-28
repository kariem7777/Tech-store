using TechCommerce.Data;
using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(ApplicationDbContext _context) : base(_context)
        {
        }

    }
}
