using SatoImsV1._1.Model;
using System.Threading.Tasks;

namespace SatoImsV1._1.Repositories.ReceivingRepo
{
    interface IReceivingRepo 
    {
        void AddReceiving(Receiving receiving);
        void UpdateOfficeItem(string poNumber);
        void UpdateOffice(string poNumber);
        Task<int> TransactItems();
    }
}
