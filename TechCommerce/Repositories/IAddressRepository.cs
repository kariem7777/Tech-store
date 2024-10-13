

using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        public List<Address> GetAllById(string id);
    }
}
