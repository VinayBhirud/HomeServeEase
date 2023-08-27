using ServeEaseV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServeEaseV3.Controllers
{
    public class ServiceProviderController : ApiController
    {
        myDacProjectEntities1 db=new myDacProjectEntities1();
        // GET: api/ServiceProvider
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ServiceProvider/5
        public IHttpActionResult Get(int id)
        {
            /*service_providers sp = db.service_providers
                     .Where(service_provider => service_provider.user_id == id)
                     .FirstOrDefault();*/
            return Ok(db.service_providers.Find(id));
        }

        // POST: api/ServiceProvider
        public void Post([FromBody]string value)
        {
        }

        
        public IHttpActionResult Put(int id, [FromBody] service_providers sp)
        {
            if (sp == null)
            {
                return BadRequest("user Update Failed!!!");
            }

            service_providers sp1 = db.service_providers.Find(id);

            try
            {
                sp1.profession = sp.profession;
                sp1.expertise = sp.expertise;
                sp1.experience = sp.experience;
                sp1.description = sp.description;
                sp1.charges= sp.charges;
                sp1.profile_pic = sp.profile_pic;
                sp1.other_images = sp.other_images;

                db.SaveChanges();
                return Ok("SP Details Updated successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

       

        // DELETE: api/ServiceProvider/5
        public void Delete(int id)
        {
        }
    }
}
