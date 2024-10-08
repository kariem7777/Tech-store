using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public interface ICartProductsRepository : IGenericRepository<CartProducts>
    {
        public Task<List<CartProductViewModel>> GetCartProductsAsync(int cartId);
        public List<CartProducts> GetCartProducts(int cartId);
        public void RemoveProduct(CartProducts cartProduct);
    }
}
