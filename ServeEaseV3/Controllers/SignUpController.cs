using Newtonsoft.Json;
using ServeEaseV3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using System.Text;
using System.Web.Http;

using System.Web.Http.Cors;
namespace ServeEaseV3.Controllers
{
    [EnableCors("*", "*", "*")]


namespace ServeEaseV3.Controllers
{
    

    public class SignUpController : ApiController
    {
        myDacProjectEntities1 db = new myDacProjectEntities1();
        public static user user1 = new user();
        
        public int MailedOtp { get; set; }
        public static int Otp { get; set; }
        // GET: api/SignUp
        public IHttpActionResult GetCustomers()
        {
            List<user> allUsers=db.users.ToList();
            return Ok(allUsers);
        }



        // GET: api/SignUp/5
        public user Get(int id)
        {
            return db.users.Find(id);

        }

        private int GenerateOtp()
        {
            Random random = new Random();
            return random.Next(1000, 10000);
        }

        // POST: api/SignUp
        public IHttpActionResult PostCustomer([FromBody] user usr)
        {
            user1 = usr;
            if (user1 == null)
            {
                return BadRequest("Failed!!!");
            }


            #region SenfingMailVerification
            Otp = GenerateOtp();

            // Configure your SMTP settings
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "serveeasehomeservices247@gmail.com";
            string smtpPassword = "ilrvaqhwkwhlfvuf";

            // Create the email message
            MailMessage message = new MailMessage();
            message.From = new MailAddress(smtpUsername);
            message.To.Add(user1.email);
            message.Subject = "Verification Code";
            message.Body = $"Your verification code is: {Otp} please do not share this with anyone";

            // Configure the SMTP client
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true;

            // Send the email
            smtpClient.Send(message);
            return Ok("Otp Send To your email successfully!!");
            #endregion

        }

        [Route("api/saveuser")]
        [HttpPost]
        public IHttpActionResult saveUser(SignUpController sc)
        {
            int otpTOBeVerified=sc.MailedOtp;
            if(Otp == otpTOBeVerified)
            {
                try
                {
                    #region getTime
                    // Assuming you have the UTC order_date
                    DateTime Date = DateTime.UtcNow;

                    // Specify the Indian Standard Time (IST) time zone
                    TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                    // Convert the UTC time to Indian time zone
                    DateTime indianDate = TimeZoneInfo.ConvertTimeFromUtc(Date, indianTimeZone);

                    // Set the corrected Indian time to the order_date property
                    user1.reg_date = indianDate;
                    #endregion
                    db.users.Add(user1);
                    db.SaveChanges();

                    if (user1.role_id == "customer")
                    {
                        customer cst = new customer();
                        var userId = db.users
                        .Where(user => user.email == user1.email)
                        .Select(user => user.user_id)
                        .FirstOrDefault();
                        cst.user_id = userId;
                        db.customers.Add(cst);
                        db.SaveChanges();
                    }
                    else if (user1.role_id == "sp")
                    {
                        service_providers sp = new service_providers();
                        var userId = db.users
                        .Where(user => user.email == user1.email)
                        .Select(user => user.user_id)
                        .FirstOrDefault();

                        sp.user_id = userId;
                        db.service_providers.Add(sp);
                        db.SaveChanges();
                    }

                    return Ok("usr added successfully");
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            else
            {
                return BadRequest("Otp is not matching!!!");
            }
        }
        
        // PUT: api/SignUp/5
        public IHttpActionResult Put(int id, [FromBody] user usr)
        {
            if (usr == null)
            {
                return BadRequest("user Update Failed!!!");
            }
            

            user usr1 = db.users.Find(id);
            try
            {
                usr1.first_name = usr.first_name;
                usr1.last_name = usr.last_name;
                usr1.email = usr.email;
                usr1.mobile = usr.mobile;
                usr1.dob = usr.dob;
                usr1.password = usr.password;

                db.SaveChanges();
                return Ok("User Updated successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        

        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult login([FromBody] user usr)
        {
            if (usr == null) 
            {
                //return BadRequest("user Update Failed!!!");
            }
            
            var userId = db.users
                    .Where(user => user.email == usr.email)
                    .Select(user => user.user_id)
                    .FirstOrDefault();
            user usr1 = db.users.Find(userId);
            if (usr1.email == usr.email && usr1.password == usr.password)
            {
                if (usr1.role_id == "customer")
                {
                     List<user> spUsers = db.users
                     .Where(user => user.role_id == "sp")
                     .ToList();
                    return Ok(spUsers);
                }
                else if (usr1.role_id == "sp")
                {
                    int spid = db.service_providers
                    .Where(service_providers => service_providers.user_id == userId)
                    .Select(service_providers => service_providers.sp_id)
                    .FirstOrDefault();

                    List < appointment> appointments = db.appointments
                     .Where(appointment => appointment.sp_id == spid)
                     .ToList();
                    return Ok(appointments);//Ok("User Logged In successfully and redirecting to Service Providers page");
                }
                else
                {
                    List<user> customerAndSpUsers = db.users
                               .Where(user => user.role_id == "customer" || user.role_id == "sp")
                               .ToList();
                    return Ok(customerAndSpUsers);//Ok("User Logged In successfully and redirecting to Admin page");
                }

            }
            else
                return BadRequest("Login Failed");
           
        }
        // DELETE: api/SignUp/5
        public IHttpActionResult Delete(int id)
        {
            user usr = db.users.Find(id);
            if (usr != null)
            {
                db.users.Remove(usr);
                db.SaveChanges();
                return Ok("User Deleted Successfully");
            }
            else
            {
                return BadRequest("Deletion Failed");
            };
        }
    }
}
