using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServeEaseV3.Models;
namespace ServeEaseV3.Controllers
{
    public class TransactionController : ApiController
    {
        myDacProjectEntities1 db = new myDacProjectEntities1();
        // GET: api/Transaction
        public List<transaction> Get()
        {
            return db.transactions.ToList();
        }

        // GET: api/Transaction/5
        public transaction Get(int id)
        {
            return db.transactions.Find(id);
        }

        // POST: api/Transaction
        public IHttpActionResult Post([FromBody] transaction tx)
        {
            if (tx == null)
            {
                return BadRequest("Failed!!!");
            }

            try
            {
                // Assuming you have the UTC order_date
                DateTime txDate = DateTime.UtcNow;

                // Specify the Indian Standard Time (IST) time zone
                TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                // Convert the UTC time to Indian time zone
                DateTime indianTxDate = TimeZoneInfo.ConvertTimeFromUtc(txDate, indianTimeZone);

                // Set the corrected Indian time to the order_date property
                tx.tx_date = indianTxDate;
                db.transactions.Add(tx);
                db.SaveChanges();
                return Ok("Transaction added successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
