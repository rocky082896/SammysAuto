using SatoImsV1._1.Data;
using SatoImsV1._1.Model;
using System.Linq;
using System.Threading.Tasks;

namespace SatoImsV1._1.Repositories.ReceivingRepo
{
    class ReceivingRepository : IReceivingRepo
    {
        internal IMSContext _context;
        internal Receiving _receiving;
        public ReceivingRepository()
        {
            _context = new IMSContext();
        }

        public void AddReceiving(Receiving receiving)
        {
            _receiving = receiving;
            _context.Receiving.Add(receiving);
        }

        public async Task<int> TransactItems()
        {
            int result = 0;
            using (var dbTrans = _context.Database.BeginTransaction())
            {
                result = await _context.SaveChangesAsync();
                dbTrans.Commit();
            }

            return result;
        }

        public void UpdateOffice(string poNumber)
        {
            var poResult = _context.OfficePurchaseOrders.SingleOrDefault
                            (op => op.officePoNumber == poNumber);
            if (poResult != null)
            {
                poResult.status = 2;
            }
        }

        public void UpdateOfficeItem(string poNumber)
        {
            var result = _context.OfficePurchaseOrdersItems.SingleOrDefault
                            (op => op.item_no == _receiving.item_no &&
                            op.officePoNumber == poNumber);
            if (result != null)
            {
                result.status = 2;
            }
        }
    }
}
