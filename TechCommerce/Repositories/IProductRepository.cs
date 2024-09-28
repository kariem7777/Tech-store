using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public interface IProductRepository
    {
        public void Add(Product product);
        public void Update(Product product);
        public void Delete(int id);
        public Product? GetbyId(int id);
        public List<Product> GetAll();
        public void Save();
    }
}
