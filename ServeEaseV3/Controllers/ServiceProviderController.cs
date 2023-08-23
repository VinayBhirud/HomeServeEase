using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ServeEaseV2.Models;
namespace ServeEaseV2.Controllers
{
    public class ServiceProviderController : ApiController
    {
        dacProjectEntities db=new dacProjectEntities();
        // GET: api/ServiceProvider
        public IHttpActionResult GetServiceProviders()
        {
            var combinedData = db.service_providers.ToList();
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(combinedData, settings);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return ResponseMessage(response);
        }

        // GET: api/ServiceProvider/5
        public IHttpActionResult GetSP(int id)
        {
            var Data = db.service_providers.Find(id);
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(Data, settings);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return ResponseMessage(response);
        }

        // POST: api/ServiceProvider
        public IHttpActionResult PostSP([FromBody] service_providers sp)
        {
            if (sp == null)
            {
                return BadRequest("Failed!!!");
            }

            try
            {
                db.service_providers.Add(sp);
                db.SaveChanges();
                return Ok("Service Provider added successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        // PUT: api/ServiceProvider/5
        public IHttpActionResult PUT(int id, [FromBody] service_providers sp)
        {
            if (sp == null)
            {
                return BadRequest("Update Failed!!!");
            }
            service_providers sp1=db.service_providers.Find(id);
            try
            {
                sp1.first_name = sp.first_name;
                sp1.last_name = sp.last_name;
                sp1.email = sp.email;
                sp1.mobile = sp.mobile;
                sp1.dob = sp.dob;
                sp1.password = sp.password;
                sp1.profile_pic = sp.profile_pic;
                sp1.profession = sp.profession;
                sp1.expertise = sp.expertise;
                sp1.experience = sp.experience;
                sp1.description = sp.description;
                sp1.charges = sp.charges;
                sp1.other_images = sp.other_images;

                db.SaveChanges();
                return Ok("Profile Updated successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /*public IHttpActionResult UpdateSpOtherDetails(int id, [FromBody] service_providers sp)
        {
            if (sp == null)
            {
                return BadRequest("Update Failed!!!");
            }
            service_providers sp1 = db.service_providers.Find(id);
            try
            {
                

                db.SaveChanges();
                return Ok("Other Details Updated successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }*/

        // DELETE: api/ServiceProvider/5
        public IHttpActionResult Delete(int id)
        {
            service_providers sp = db.service_providers.Find(id);
            if (sp != null)
            {
                db.service_providers.Remove(sp);
                db.SaveChanges();
                return Ok("Service Provider Deleted Successfully");
            }
            else
            {
                return BadRequest("Deletion Failed");
            };

            
        }
    }
}
