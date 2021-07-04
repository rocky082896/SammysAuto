using SatoImsV1._1.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SatoImsV1._1.Repositories.PORepo
{
    public interface IPORepo : IDisposable
    {
        void AddPO(OfficePurchaseOrder po);
        void AddPOItems(ObservableCollection<OfficePurchaseOrderItems> items);
        Task<bool> TransactItemsAsync(OfficePurchaseOrder po,
            ObservableCollection<OfficePurchaseOrderItems> items);
    }
}
