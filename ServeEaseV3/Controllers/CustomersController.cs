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
    public class CustomersController : ApiController
    {
        
        dacProjectEntities db = new dacProjectEntities();
        // GET: api/Customers
        public IHttpActionResult GetCustomers()
        {
            var combinedData = db.customers.ToList();
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(combinedData, settings);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return ResponseMessage(response);
        }

        // GET: api/Customers/5
        public IHttpActionResult Get(int id)
        {
            customer combinedData = db.customers.Find(id);
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(combinedData, settings);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return ResponseMessage(response);
        }

        // POST: api/Customers
        public IHttpActionResult PostCustomer([FromBody]customer cust)
        {
            if (cust == null)
            {
                return BadRequest("Failed!!!");
            }

            try
            {
                db.customers.Add(cust);
                db.SaveChanges();
                return Ok("Customer added successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Customers/5
        public IHttpActionResult Put(int id, [FromBody] customer cust)
        {
            if (cust == null)
            {
                return BadRequest("Update Failed!!!");
            }
            customer cust1 = db.customers.Find(id);
            try
            {
                cust1.first_name = cust.first_name;
                cust1.last_name = cust.last_name;
                cust1.email = cust.email;
                cust1.mobile = cust.mobile;
                cust1.dob = cust.dob;
                cust1.password = cust.password;
                cust1.profile_pic = cust.profile_pic;

                db.SaveChanges();
                return Ok("Address Updated successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Customers/5
        public IHttpActionResult Delete(int id)
        {
            customer customer = db.customers.Find(id);
            if (customer != null)
            {
                db.customers.Remove(customer);
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
