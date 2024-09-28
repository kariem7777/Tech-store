using TechCommerce.Data;
using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext context;
        public ProductRepository(ApplicationDbContext _context) // INJECT
        {
            context = _context;
        }

        //CRUD
        public void Add(Product product)
        {
            context.Add(product);
        }
        public void Update(Product product)
        {
            context.Update(product);
        }
        public void Delete(int id)
        {
            Product? product = GetbyId(id);
            if (product != null)
            {
                context.Remove(product);
            }
        }
        public Product? GetbyId(int id)
        {
            return context.Products.FirstOrDefault(P => P.Id == id);
        }
        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
