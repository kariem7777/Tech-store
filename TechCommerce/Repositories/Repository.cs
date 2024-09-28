using TechCommerce.Data;

namespace TechCommerce.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext context;

        public Repository(ApplicationDbContext _context) // INJECT
        {
            context = _context;
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
            }
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
