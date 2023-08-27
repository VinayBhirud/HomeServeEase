using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServeEaseV3.Models;
namespace ServeEaseV3.Controllers
{
    public class ReviewController : ApiController
    {
        // GET: api/Review
        myDacProjectEntities1 db = new myDacProjectEntities1();
        // GET: api/Review
        public List<review> Get()
        {
            return db.reviews.ToList();
        }

        // GET: api/Review/5
        public review Get(int id)
        {
            return db.reviews.Find(id);
        }

        // POST: api/Review
        public IHttpActionResult Post([FromBody] review rv)
        {
            if (rv == null)
            {
                return BadRequest("Failed!!!");
            }

            try
            {
                db.reviews.Add(rv);
                db.SaveChanges();
                return Ok("Reviews added successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Review/5
        public IHttpActionResult Put(int id, [FromBody] review rv)
        {
            if (rv == null)
            {
                return BadRequest("Update Failed!!!");
            }
            review rv1 = db.reviews.Find(id);
            try
            {
                rv1.cust_id = rv.cust_id;
                rv1.sp_id = rv.sp_id;
                rv1.apt_id = rv.apt_id;
                rv1.review1 = rv.review1;
                rv1.complaint = rv.complaint;
                rv1.ratings = rv.ratings;
                db.SaveChanges();
                return Ok("Review Updated successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Review/5
        public IHttpActionResult Delete(int id)
        {
            review rv = db.reviews.Find(id);
            if (rv != null)
            {
                db.reviews.Remove(rv);
                db.SaveChanges();
                return Ok("Reviews Deleted successfully");
            }
            else
            {
                return BadRequest("Review Deletion Failed");
            }
        }
    }
}
