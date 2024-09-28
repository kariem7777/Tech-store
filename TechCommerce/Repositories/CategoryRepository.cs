using TechCommerce.Data;
using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(ApplicationDbContext _context) : base(_context)
        {
        }
    }
}

