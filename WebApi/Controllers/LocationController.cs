using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiClassModel.Entities;
using WebApiClassModel.Model.Dto;

namespace WebApi.Controllers
{
    public class LocationController : ApiController
    {
        [Route("api/Location")]
        public async Task<List<LocationDto>> GetLocations()
        {
            List<LocationDto> locationDtos = new List<LocationDto>();
            using (sato_imsdb_testEntities entities = new sato_imsdb_testEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                var result = entities.tbl_racks.Where(x => x.Status == 1).ToList();

                foreach (var item in result)
                {
                    locationDtos.Add(new LocationDto
                    {
                        RackId = item.RackId,
                        RackCode = item.RackCode,
                        StatusCode = item.Status,
                        Status = item.Status == 1 ? "Free" : "Occupied"
                    });
                }

                return await Task.Run(() => locationDtos);
            }
        }

        [Route("api/Location/InsertLocation")]
        [HttpPost]
        public IHttpActionResult InsertLocations([FromBody] LocationDto location)
        {
            HttpResponseMessage responseMessage;
            using (sato_imsdb_testEntities entities = new sato_imsdb_testEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                try
                {
                    if (entities != null && location != null)
                    {
                        tbl_racks rack = new tbl_racks
                        {
                            RackCode = location.RackCode,
                            Status = 1
                        };
                        entities.tbl_racks.Add(rack);
                        bool inserted = entities.SaveChanges() > 0;

                        responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                        return inserted ? Ok(new { success = true }) : Ok(new { success = false });
                    }
                    else
                    {
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
                    responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                    return Ok(new { success = false });
                }
            }
        }

    }
}
