using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using ServeEaseV2.Models;
namespace ServeEaseV2.Controllers
{
    public class AddressController : ApiController
    {
        dacProjectEntities db = new dacProjectEntities();
        // GET: api/Address
        public IHttpActionResult GetAddress()
        {
            var combinedData = db.addresses.ToList();
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(combinedData, settings);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return ResponseMessage(response);
        }

        // GET: api/Address/id
        public IHttpActionResult GetAddressById(int id)
        {
            var Data = db.addresses.Find(id);
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(Data, settings);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return ResponseMessage(response);
        }

        //POST: api/address
        public IHttpActionResult PostAddress([FromBody] address add)
        {
            if (add == null)
            {
                return BadRequest("Failed!!!");
            }

            try
            {
                db.addresses.Add(add);
                db.SaveChanges();
                return Ok("Address added successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Address/5
        public IHttpActionResult Put(int id, [FromBody] address add)
        {
            if (add == null)
            {
                return BadRequest("Update Failed!!!");
            }
            address add1= db.addresses.Find(id);
            try
            {
                add1.city = add.city;
                add1.state=add.state;
                add1.address1 = add.address1;
                add1.district = add.district;
                add1.pin_code = add.pin_code;

                db.SaveChanges();
                return Ok("Address Updated successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Address/5
        public IHttpActionResult Delete(int id)
        {
            address add1= db.addresses.Find(id);
            if(add1 !=null)
            {
                db.addresses.Remove(add1);
                db.SaveChanges();
                return Ok("Address Deleted Successfully");
            }
            else
            {
                return BadRequest("Deletion Failed");
            };
            
        }
    }
}
