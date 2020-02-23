using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiTuesday.Models;
namespace ApiTuesday.Controllers
{
    public class UsersController : ApiController
    {
        public IEnumerable<User> Get() {
            using (UsersEntities db = new UsersEntities())
                return db.Users.ToList();
        }


        public HttpResponseMessage Get(int id) {
            try
            {
                using (UsersEntities db = new UsersEntities())
                {


                    var entity = db.Users.FirstOrDefault(x => x.UserID == id);
                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Person does not exist" + id.ToString());
                    }
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }

          

        }

        public HttpResponseMessage Post([FromBody] User U) {
            try {
                using (UsersEntities db = new UsersEntities()) {
                    db.Users.Add(U);
                    db.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created,U);
                    msg.Headers.Location = new Uri(Request.RequestUri + U.UserID.ToString());
                    return msg;
                }
            }
            catch(Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage Delete(int id) {

            try {
                using (UsersEntities db = new UsersEntities()) {
                    var entity = db.Users.FirstOrDefault(x => x.UserID == id);
                    if (entity != null)
                    {
                        db.Users.Remove(entity);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else {
                        return Request.CreateResponse(HttpStatusCode.NotFound,"It does not exist",id.ToString());
                    }

                }
            }

            catch (Exception ex) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

        }


        public HttpResponseMessage Put(int id, [FromBody] User U) {

            
                using (UsersEntities db = new UsersEntities())
                 {
                try
                {
                    var entity = db.Users.FirstOrDefault(x => x.UserID == id);
                    if (entity != null)
                    {
                        entity.UserName = U.UserName;
                        entity.UserAddress = U.UserAddress;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, db);

                    }
                    else {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Does not exist"+id.ToString());
                    }
                }
            

            catch (Exception ex) {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);


            }
            }
        }



        
    }
}
