using System.Linq;
using Teleperformance.Data;

namespace Teleperformance.Repository.Generics
{
    public class Repos : IRepo
    {
        internal TeleContext context;

        public Repos()
        {
            context = new TeleContext();
        }

        public IQueryable<T> All<T>() where T : class
        {
            return context.Set<T>();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
