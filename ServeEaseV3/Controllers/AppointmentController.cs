using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServeEaseV3.Models;
namespace ServeEaseV3.Controllers
{
    public class AppointmentController : ApiController
    {
        myDacProjectEntities1 db = new myDacProjectEntities1();
        // GET: api/Appointment
        public List<appointment> Get()
        {
            return db.appointments.ToList();
        }

        // GET: api/Appointment/5
        public appointment Get(int id)
        {
            return db.appointments.Find(id);
        }

        // POST: api/Appointment
        public IHttpActionResult Post([FromBody] appointment apt)
        {
            if (apt == null)
            {
                return BadRequest("Failed!!!");
            }

            try
            {
                // Assuming you have the UTC order_date
                DateTime utcOrderDate = DateTime.UtcNow;

                // Specify the Indian Standard Time (IST) time zone
                TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                // Convert the UTC time to Indian time zone
                DateTime indianOrderDate = TimeZoneInfo.ConvertTimeFromUtc(utcOrderDate, indianTimeZone);

                // Set the corrected Indian time to the order_date property
                apt.order_date = indianOrderDate;

                db.appointments.Add(apt);
                db.SaveChanges();
                return Ok("Appointment added successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Appointment/5
        public IHttpActionResult Put(int id, [FromBody] appointment apt)
        {
            if (apt == null)
            {
                return BadRequest("Update Failed!!!");
            }
            appointment apt1 = db.appointments.Find(id);
            try
            {
                // Assuming you have the UTC order_date
                DateTime utcOrderDate = DateTime.UtcNow;

                // Specify the Indian Standard Time (IST) time zone
                TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                // Convert the UTC time to Indian time zone
                DateTime indianOrderDate = TimeZoneInfo.ConvertTimeFromUtc(utcOrderDate, indianTimeZone);

                // Set the corrected Indian time to the order_date property
                apt1.order_date = indianOrderDate;

                apt1.cust_id = apt.cust_id;
                apt1.sp_id = apt1.sp_id;
                apt1.ord_description = apt.ord_description;
                //apt1.order_date=DateTime.UtcNow;
                apt1.apt_date = apt.apt_date;
                apt1.apt_status = apt.apt_status;

                db.SaveChanges();
                return Ok("Address Updated successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Appointment/5
        public IHttpActionResult Delete(int id)
        {
            appointment apt1 = db.appointments.Find(id);
            if (apt1 != null)
            {
                db.appointments.Remove(apt1);
                db.SaveChanges();
                return Ok("Appointment Cancelled successfully");
            }
            else
            {
                return BadRequest("Appointment Cancellation Failed");
            }
        }
    }
}
