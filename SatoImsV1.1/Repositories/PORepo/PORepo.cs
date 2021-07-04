using SatoImsV1._1.Data;
using SatoImsV1._1.Model;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace SatoImsV1._1.Repositories.PORepo
{
    public class PORepo : IPORepo
    {
        internal IMSContext _context;
        public PORepo()
        {
            _context = new IMSContext();
        }
        public void AddPO(OfficePurchaseOrder po)
        {
            //bool flag;
            //_context.OfficePurchaseOrders.Add(po);
            //try
            //{
            //    flag = await _context.SaveChangesAsync() > 0;

            //}
            //catch (DbUpdateException ex) when (ex.InnerException?.InnerException is SqlException
            //    sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            //{
            //    flag = false;
            //    MessageBox.Show("Duplicate entry");
            //}

            //return flag;
        }

        public void AddPOItems(ObservableCollection<OfficePurchaseOrderItems> items)
        {
            _context.OfficePurchaseOrdersItems.AddRange(items);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public async Task<bool> TransactItemsAsync(OfficePurchaseOrder po,
            ObservableCollection<OfficePurchaseOrderItems> items)
        {
            bool flag;
            _context.OfficePurchaseOrders.Add(po);
            try
            {
                flag = await _context.SaveChangesAsync() > 0;

            }
            catch (DbUpdateException ex) when (ex.InnerException?.InnerException is SqlException
                sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                flag = false;
                MessageBox.Show("Duplicate entry");
            }

            if (flag)
            {
                _context.OfficePurchaseOrdersItems.AddRange(items);
                await _context.SaveChangesAsync();
            }

            //using (var dbTrans = _context.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        result = await _context.SaveChangesAsync();
            //        dbTrans.Commit();
            //    }
            //    catch (DbUpdateException ex) when (ex.InnerException?.InnerException is SqlException
            //    sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            //    {
            //        dbTrans.Rollback();
            //        result = 0;
            //        MessageBox.Show("Duplicate entry");
            //    }
            //}

            //return result;

            return flag;
        }
    }
}
