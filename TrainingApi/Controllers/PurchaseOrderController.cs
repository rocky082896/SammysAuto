using IMSDataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TrainingApi.Controllers
{
    /// <summary>
    /// Controllers for Getting the Purchase Orders Information
    /// </summary>

    public class PurchaseOrderController : ApiController
    {
        /// <summary>
        /// Getting the list of active Purchase Orders
        /// </summary>
        /// <returns></returns>
        public List<tbl_office_po> Get()
        {
            List<tbl_office_po> output = new List<tbl_office_po>();
            using (sato_imsdbEntities entities = new sato_imsdbEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                foreach (tbl_office_po po in
                    entities.tbl_office_po.Where(x => x.status == 1))
                {
                    output.Add(po);
                }
                return output;
            }
        }
    }
}
