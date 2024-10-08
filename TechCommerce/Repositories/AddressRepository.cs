﻿using Stripe;
using TechCommerce.Data;

namespace TechCommerce.Repositories
{
    public class AddressRepository : GenericRepository<Address>
    {
        private readonly ApplicationDbContext context;

        public AddressRepository(ApplicationDbContext _context) : base(_context)
        {
            context = _context;
        }
        //public List<Address> GetaddressOfCustomer(String id)
        //{
        //    return context.Addresses?.Where(A => A.CustomerId == id).ToList() ?? new List<Address>();
        //}
    }
}
