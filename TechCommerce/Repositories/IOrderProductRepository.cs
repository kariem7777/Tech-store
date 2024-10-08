using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public interface IOrderProductRepository : IGenericRepository<OrderProduct>
    {
        public List<OrderDetailsViewModel> GetOrderDetails(int orderId);
    }
}
