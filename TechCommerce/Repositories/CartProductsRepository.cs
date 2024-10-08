using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechCommerce.Data;
using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public class CartProductsRepository : GenericRepository<CartProducts>, ICartProductsRepository
    {
        private readonly ApplicationDbContext _context;

        public CartProductsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CartProductViewModel>> GetCartProductsAsync(int cartId)
        {
            return await _context.CartProducts
                .Include(cp => cp.Product)
                .Where(cp => cp.CartId == cartId)
                .Select(cp => new CartProductViewModel
                {
                    ProductId = cp.ProductId,
                    Name = cp.Product.Name ?? "Unknown",
                    Units = cp.Product.Units,
                    Price = cp.Product.Price,
                    Description = cp.Product.Description ?? "No description",
                    ImageUrl = cp.Product.ImageUrl ?? string.Empty,
                    CategoryId = cp.Product.CategoryId,
                    Quantity = cp.Quantity
                })
                .ToListAsync();
        }

        public List<CartProducts> GetCartProducts(int cartId)
        {
            return _context.CartProducts
                .Include(cp => cp.Product).Where(cp => cp.CartId == cartId).ToList();
        }

        public void RemoveProduct(CartProducts cartProduct)
        {
            var existingProduct = _context.CartProducts.Find(cartProduct.ProductId, cartProduct.CartId);
            if (existingProduct != null)
            {
                _context.Set<CartProducts>().Remove(existingProduct);
            }
        }
    }
}
