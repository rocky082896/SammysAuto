using SatoImsV1._1.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SatoImsV1._1.Repositories.OutgoingRepo
{
    public interface IOutgoing: IDisposable
    {
        void InsertOutgoing<T>(T entity) where T : class;
        //void getPoNumber();
        IQueryable getItemNo(string PoNumber);
        
        Task<bool> TransactItemsAsync(Outgoing og,
            ObservableCollection<OutgoingDetails> items);
    }
}
