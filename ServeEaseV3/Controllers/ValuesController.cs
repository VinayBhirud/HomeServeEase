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
    public class CombinedData
    {
        public List<customer> Customers { get; set; }
        public List<service_providers> ServiceProviders { get; set; }
    }
    public class ValuesController : ApiController
    {
        dacProjectEntities db = new dacProjectEntities();
        // GET: api/values
          public List<customer> GetCustomers()
          {
              return db.customers.ToList();
          }
        /*
          // GET: api/Home
          public List<service_providers> Get()
          {
              return db.service_providers.ToList();
          }*/

        /*public IHttpActionResult GetCombinedData()
        {
            var combinedData = new CombinedData
            {
                Customers = db.customers.ToList(),
                ServiceProviders = db.service_providers.ToList()
            };

            return Ok(combinedData);
        }*/

        public IHttpActionResult GetCombinedData()
        {
            var combinedData = new CombinedData
            {
                Customers = db.customers.ToList(),
                ServiceProviders = db.service_providers.ToList()
            };

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(combinedData, settings);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return ResponseMessage(response);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
