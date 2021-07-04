using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiClassModel.Entities;
using WebApiClassModel.Model.Dto;

namespace WebApi.Controllers
{
    public class ReceivingController : ApiController
    {
        // GET: api/Receiving
        [Route("api/Receiving")]
        public async Task<List<PurchaseOrderDto>> GetPurchaseOrders()
        {
            List<PurchaseOrderDto> purchaseOrderDtos = new List<PurchaseOrderDto>();
            PurchaseOrderDto purchaseOrderDto;
            using (sato_imsdb_testEntities entities = new sato_imsdb_testEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                var output = entities.tbl_office_po.Where(x => x.status == 1)
                    .Join(entities.tbl_supplier, o => o.supp_id, s => s.supp_id,
                    (o, s) => new { o, s }).ToList();
                foreach (var item in output)
                {
                    purchaseOrderDto = new PurchaseOrderDto()
                    {
                        officePoId = item.o.officeCreditTerms,
                        officePoNumber = item.o.officePoNumber,
                        supp_id = item.o.supp_id,
                        supp_name = item.s.supplier_name,
                        officeremarks = item.o.officeRemarks,
                        officetotalamount = item.o.officeTotalAmount,
                        deliverydate = item.o.officeDeliveryDate.ToString("yyyy-MM-dd")
                    };
                    purchaseOrderDtos.Add(purchaseOrderDto);
                }
                return await Task.Run(() => purchaseOrderDtos);
            }
        }
        [Route("api/Receiving/GetItems")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]SelectPurchaseOrderDto items)
        {
            using (sato_imsdb_testEntities entities = new sato_imsdb_testEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;

                var output = entities.tbl_office_po.Where(x => x.officePoNumber == items.officePoNumber)
                    .Join(entities.tbl_office_po_items, x => x.officePoNumber, poi => poi.officePoNumber,
                    (x, poi) => new { x, poi })
                    .Join(entities.tbl_supplier, combined3 => combined3.x.supp_id,
                    s => s.supp_id, (combined3, s) => new { combined3, s })
                    .AsEnumerable()
                    .Select(x => new PurchaseOrderDto()
                    {
                        officePoNumber = x.combined3.x.officePoNumber,
                        officePoId = x.combined3.x.officePoId,
                        officeremarks = x.combined3.x.officeRemarks,
                        officetotalamount = x.combined3.x.officeTotalAmount,
                        deliverydate = x.combined3.x.officeDeliveryDate.ToString("yyyy-MM-dd"),
                        supp_id = x.combined3.x.supp_id,
                        supp_name = x.s.supplier_name,
                        poItems = entities.tbl_office_po_items
                        .Where(p => p.officePoNumber == x.combined3.x.officePoNumber
                        && p.status == 1).Join(entities.tbl_item_master, po_items => po_items.item_no,
                        ims => ims.item_no, (item_no, ims) => new { item_no, ims })
                        .Select(i => new PurchaseOrderItemsDto()
                        {
                            office_item_id = i.item_no.office_item_id,
                            officeAmount = i.item_no.officeAmount,
                            officePrice = i.item_no.officePrice,
                            officeQuantity = i.item_no.officeQuantity,
                            officeUnit = i.item_no.officeUnit,
                            item_no = i.item_no.item_no,
                            item_name = i.ims.item_desc

                        }).ToList()

                    }).ToList().GroupBy(g => g.officePoId).Select(sel => sel.First());
                return Ok(output);
            }
        }
        [Route("api/Receiving/ReceiveItem")]
        public IHttpActionResult PostItem([FromBody]ReceiveItemDto items)
        {
            HttpResponseMessage responseMessage;
            using (sato_imsdb_testEntities entities = new sato_imsdb_testEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;

                using (DbContextTransaction trans = entities.Database.BeginTransaction())
                {
                    try
                    {
                        bool isLast = items.isLast;

                        var entity = entities.tbl_office_po_items.SingleOrDefault(x => x.officePoNumber == items.purchaseOrderNumber
                        && x.item_no == items.item_no);
                        if (entity != null)
                        {
                            entity.status = 2;
                            entities.SaveChanges();

                            tbl_receiving receiving = new tbl_receiving
                            {
                                item_no = items.item_no,
                                officePoNumber = items.purchaseOrderNumber,
                                serialNo = "",
                                date_stored = DateTime.Now,
                                rec_qty = items.Rec_qty,
                                current_qty = items.Curr_qty,
                                price = items.Price,
                                user_id = items.UserId,
                                status = items.Status,
                                RackCode = items.RackCode,
                                currency = "PHP"
                            };


                            entities.tbl_receiving.Add(receiving);
                            entities.SaveChanges();

                            if (isLast)
                            {
                                var poEntity = entities.tbl_office_po.SingleOrDefault(x => x.officePoNumber == items.purchaseOrderNumber);
                                if (poEntity != null)
                                {
                                    poEntity.status = 2;
                                    entities.SaveChanges();
                                }

                            }

                            trans.Commit();
                            responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                            return Ok(new { success = true });
                        }
                        else
                        {
                            trans.Rollback();
                            responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                            return Ok(new { success = false });
                        }
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }

                        trans.Rollback();
                        responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                        return Ok(new { success = false });
                    }
                }

            }
        }
        [Route("api/Receiving/ReceivePO")]
        public IHttpActionResult PostPO([FromBody]tbl_office_po office_Po)
        {
            using (sato_imsdb_testEntities entities = new sato_imsdb_testEntities())
            {
                try
                {
                    if (office_Po != null)
                    {
                        var entity = entities.tbl_office_po.SingleOrDefault(x => x.officePoNumber == office_Po.officePoNumber);
                        if (entity != null)
                        {
                            entity.status = 2;
                            entities.SaveChanges();
                            return Ok(new { success = true });
                        }
                        else
                        {
                            return Ok(new { success = false });
                        }
                    }
                    else
                    {
                        return Ok(new { success = false });
                    }
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    return Ok(new { success = false });
                }
            }
        }
        [Route("api/Receiving/BulkReceive")]
        public IHttpActionResult PostBulkItem([FromBody]tbl_office_po_items items)
        {
            using (sato_imsdb_testEntities entities = new sato_imsdb_testEntities())
            {
                try
                {
                    var entity = entities.tbl_office_po_items.Where(x => x.officePoNumber
                        == items.officePoNumber).ToList();
                    if (entity != null)
                    {
                        foreach (var item in entity)
                        {
                            item.status = 2;
                        }
                        entities.SaveChanges();
                        return Ok(new { success = true });
                    }
                    else
                    {
                        return Ok(new { success = false });
                    }

                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    return Ok(new { success = false });
                }

            }
        }
    }
}
