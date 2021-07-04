using System;
using System.Linq;

namespace Teleperformance.Repository.Generics
{
    public interface IRepo : IDisposable
    {
        IQueryable<T> All<T>() where T : class;
    }
}
