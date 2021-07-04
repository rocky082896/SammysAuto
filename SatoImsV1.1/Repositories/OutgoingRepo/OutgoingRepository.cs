using SatoImsV1._1.Data;
using SatoImsV1._1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SatoImsV1._1.Repositories.OutgoingRepo
{
    public class OutgoingRepository : IOutgoing
    {
        internal IMSContext _context;
        public OutgoingRepository()
        {
            _context = new IMSContext();
        }
        public void InsertOutgoing<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
        }

        //public void getPoNumber()
        //{
        //    return  _context.Receiving.Select(x => new {x.officePoNumber, x.status }).Where(y => y.status == 1).Distinct(); 
        //}

        public IQueryable getItemNo(string PoNumber)
        {
           return _context.Receiving.Select(y => new { y.item_no, y.officePoNumber }).Where(z=> z.officePoNumber == PoNumber);
        }

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }

        public async Task<bool> TransactItemsAsync(Outgoing og, ObservableCollection<OutgoingDetails> items)
        {
            bool flag;
            _context.Outgoings.Add(og);
            try
            {
                flag = await _context.SaveChangesAsync() > 0;

            }
            catch (Exception e)
            {
                flag = false;
                MessageBox.Show(e.Message);
            }

            if (flag)
            {
                _context.OutgoingDetails.AddRange(items);
                await _context.SaveChangesAsync();
            }

            return flag;
        }
    }
}
