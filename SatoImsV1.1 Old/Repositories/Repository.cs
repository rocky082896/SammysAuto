using SatoImsV1._1.Data;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SatoImsV1._1.Repositories
{
    public class Repository : IRepository
    {
        internal IMSContext _context;

        public Repository()
        {
            _context = new IMSContext();
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return _context.Set<T>();
        }
        public IQueryable<T> All<T>(Func<T, object> include) where T : class
        {
            return _context.Set<T>();
        }


        public IQueryable<T> AllIncluding<T>(params Expression<Func<T, object>>[] include) where T : class
        {
            IQueryable<T> retval = _context.Set<T>();
            foreach (var item in include)
            {
                retval = retval.Include(item);
            }

            return retval;

        }


        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public async Task<int> Save<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }

    }
}
