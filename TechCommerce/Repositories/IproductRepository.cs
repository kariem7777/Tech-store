using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public interface IproductRepository : IGenericRepository<Product>
    {
        public Product GetWithRates(int id); 
    }
}
