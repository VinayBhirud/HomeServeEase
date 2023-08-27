using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServeEaseV3.Models;
namespace ServeEaseV3.Controllers
{
    public class PaymentDetailsController : ApiController
    {
        myDacProjectEntities1 db=new myDacProjectEntities1();
        // GET: api/PaymentDetails
        public List<payment_details> Get()
        {
            return db.payment_details.ToList();
        }

        // GET: api/PaymentDetails/5
        public payment_details Get(int id)
        {
            return db.payment_details.Find(id);
        }

        // POST: api/PaymentDetails
        public IHttpActionResult Post([FromBody] payment_details pd)
        {
            if (pd == null)
            {
                return BadRequest("Failed!!!");
            }

            try
            {
                db.payment_details.Add(pd);
                db.SaveChanges();
                return Ok("Payment Details added successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/PaymentDetails/5
        public IHttpActionResult Put(int id, [FromBody] payment_details pd)
        {
            if (pd == null)
            {
                return BadRequest("Update Failed!!!");
            }
            payment_details pd1 = db.payment_details.Find(id);
            try
            {
                
                pd1.bank_name = pd.bank_name;
                pd1.branch_name = pd.branch_name;
                pd1.ifsc_code = pd.ifsc_code;
                pd1.acc_no = pd.acc_no;
                pd1.card_number = pd.card_number;
                pd1.card_expiry = pd.card_expiry;
                pd1.card_cvv = pd.card_cvv;
                db.SaveChanges();
                return Ok("PaymentMethods Updated successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/PaymentDetails/5
        public IHttpActionResult Delete(int id)
        {
            payment_details pd = db.payment_details.Find(id);
            if (pd != null)
            {
                db.payment_details.Remove(pd);
                db.SaveChanges();
                return Ok("PaymentMethods Deletion successfully");
            }
            else
            {
                return BadRequest("PaymentMethods Deletion Failed");
            }
        }
    }
}
