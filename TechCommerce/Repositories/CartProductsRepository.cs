using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechCommerce.Data;
using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public class CartProductsRepository : Repository<CartProducts>
    {
        private readonly ApplicationDbContext _context;

        public CartProductsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public List<CartProducts> GetbyIDWithProducts(int id)
        {
            return _context.CartProducts.Include(cp => cp.Product).Where(cp => cp.CartId == id).ToList();
        }
    }
}
