using Microsoft.AspNetCore.Mvc;
using TechCommerce.Data;
using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public class CartProductsRepository : Repository<CartProducts>
    {
        public CartProductsRepository(ApplicationDbContext _context) : base(_context)
        {
        }
    }
}
