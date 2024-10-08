using Microsoft.EntityFrameworkCore;
using TechCommerce.Data;
using TechCommerce.Models;

namespace TechCommerce.Repositories
{
    // The following GenericRepository class Implement the IGenericRepository Interface
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> table;

        public GenericRepository(ApplicationDbContext _context) // INJECT
        {
            context = _context;
            table = _context.Set<T>();
        }

        public void Add(T entity)
        {
            table.Add(entity);
        }

        public void Update(T entity)
        {
            table.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                table.Remove(entity);
            }
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }

        public T GetById(string? id)
        {
            return table.Find(id);
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
