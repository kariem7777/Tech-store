using Microsoft.EntityFrameworkCore;
using Stripe.Climate;
using TechCommerce.Data;
using TechCommerce.Models;
using Product = TechCommerce.Models.Product;

namespace TechCommerce.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IproductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Product GetWithRates(int id)
        {
            return _context.Products
                .Include(O => O.Prate).FirstOrDefault(p=> p.Id== id);
        }
    }
}
