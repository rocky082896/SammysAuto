using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SatoImsV1._1.Repositories
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> All<T>() where T : class;
        IQueryable<T> AllIncluding<T>(params Expression<Func<T, object>>[] include) where T : class;
        Task<int> Save<T>(T entity) where T : class;
    }
}
