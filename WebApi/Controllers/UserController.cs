using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiClassModel.Entities;
using WebApiClassModel.Model;
using WebApiClassModel.Model.Dto;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public async Task<List<User>> Get()
        {
            List<User> output = new List<User>();
            User um;
            using (sato_imsdb_testEntities entities = new sato_imsdb_testEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                foreach (var users in entities.tbl_user.ToList())
                {
                    um = new User
                    {
                        user_id = users.user_id,
                        firstname = users.firstname,
                        lastname = users.lastname,
                        password = users.password,
                        priv_id = users.priv_id,
                        status = users.status
                    };

                    output.Add(um);
                }
                return await Task.Run(() => output);
            }
        }

        [Route("api/User/Login")]
        public IHttpActionResult Login([FromBody]User user)
        {
            using (sato_imsdb_testEntities entities = new sato_imsdb_testEntities())
            {
                LoginDto callback = null;
                entities.Configuration.ProxyCreationEnabled = false;
                var output = entities.tbl_user.Where(x => x.user_id == user.user_id
               && x.password == user.password).ToList();
                foreach (var item in output)
                {
                    callback = new LoginDto
                    {
                        firstname = item.firstname,
                        lastname = item.lastname,
                        user_id = item.user_id,
                        priv_id = item.priv_id
                    };
                }
                if (callback != null)
                    return Ok(new LoginDto[] { callback });
                else
                    return Content(HttpStatusCode.BadRequest, new string[] { });
            }
        }

        // GET: api/User/Post
        [Route("api/User/Individual")]
        public tbl_user Post([FromBody]tbl_user id)
        {
            using (sato_imsdb_testEntities entities = new sato_imsdb_testEntities())
            {
                entities.Configuration.ProxyCreationEnabled = false;
                return entities.tbl_user.Where(x => x.user_id == id.user_id).FirstOrDefault();
            }
        }

        [Route("api/User/AddUser")]
        public HttpResponseMessage AddUser([FromBody]tbl_user user)
        {
            using (sato_imsdb_testEntities entities = new sato_imsdb_testEntities())
            {
                try
                {
                    entities.tbl_user.Add(user);
                    entities.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.Created);
                    return response;
                }
                catch (Exception)
                {
                    HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                    return response;
                }
            }
        }

        [Route("api/User/BulkAdd")]
        public HttpResponseMessage BulkAdd([FromBody]List<tbl_user> user)
        {
            HttpResponseMessage response;
            using (sato_imsdb_testEntities entities = new sato_imsdb_testEntities())
            {
                try
                {
                    entities.tbl_user.AddRange(user);
                    entities.SaveChanges();
                    response = new HttpResponseMessage(System.Net.HttpStatusCode.Created);
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
                    response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                }
                return response;
                //catch (Exception ex)
                //{
                //    Console.WriteLine("Error: " + ex.Message);
                //    HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                //    return response;
                //}
            }
        }

    }
}
