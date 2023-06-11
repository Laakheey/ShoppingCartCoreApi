using Jwt.Model;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext dbContext = null;
        private DbSet<T> table = null;
        public GenericRepository()
        {
            dbContext = new ApplicationDbContext();
            table = dbContext.Set<T>();
        }

        public GenericRepository(ApplicationDbContext _context)
        {
            dbContext = _context;
            table = dbContext.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
 
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            dbContext.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }


    }
}
