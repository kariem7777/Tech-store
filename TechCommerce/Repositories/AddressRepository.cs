
using TechCommerce.Data;
using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly ApplicationDbContext context;

        public AddressRepository(ApplicationDbContext _context) : base(_context)
        {
            context = _context;
        }

        public List<Address> GetAllById(string id)
        {
            return context.Addresses.Where(A=>A.CustomerId == id).ToList();
        }
        
    }
}
