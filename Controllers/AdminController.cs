using healt_Center.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using healt_Center.Migrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO.Enumeration;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Hosting;
using System.Numerics;
//using healt_Center.Migrations;

namespace healt_Center.Controllers
{
    public class AdminController : Controller
    {
        private readonly Patient_DbContext _context;
        private IWebHostEnvironment _Environment;



        public AdminController(Patient_DbContext patient_Db, IWebHostEnvironment environment)
        {
            _context = patient_Db;
            _Environment = environment;
        }
        public IActionResult Login_Form()
        {

            //var row = _Context.admin_tbl.FirstOrDefault(a => a.admin_email == admin_email);
            //if (row != null && row.admin_password == admin_password)
            //var admin = _context.tbl_admin.FirstOrDefault(a => a.email == email);
            //if (admin != null && admin.Password == password)
            //{
            //    HttpContext.Session.SetString("admin_session",admin.Id.ToString());
            //    return RedirectToAction("admin");
            //}
            //else
            //{
            //    ViewBag.message = "Useremail and Password is incorrect!!";

            //}
            return View();

        }
        [HttpPost]
        public IActionResult Login_Form(Models.admin login)
        {
            var admin = _context.tbl_admin.Where(a=> a.admin_email == login.admin_email && a.admin_Password==login.admin_Password).FirstOrDefault();
            if (admin != null)
            {
                HttpContext.Session.SetString("admin_session", admin.Id.ToString());
                return RedirectToAction("admin");
            }
            else
            {
                ViewBag.message = "Email and Password is incorrect!!";
                return View();
            }
           
        }
        public IActionResult admin()
        {

            if (HttpContext.Session.GetString("admin_session") != null)
            {
                
                return View();
            }
            else
            {

            }
            return RedirectToAction("Login_Form");
        }
        public IActionResult New_Appointments()
        {

            return View(_context.patients.Where(a => a.status == 0).ToList());
        }
        public IActionResult Confirmed_Appointments()
        {
            return View(_context.patients.Where(a => a.status == 1).ToList());
        }

        public IActionResult FixShedule(int id)
        {
            var fix = _context.patients.FirstOrDefault(a => a.Id == id);
            HttpContext.Session.SetString("appointment_session" , fix.Id.ToString());
            return View(fix);
        }
        [HttpPost] 
        public IActionResult FixShedule(patient patient)
        {
            // Fetch the existing appointment from the database using the appoint_id
            string appoint_id = HttpContext.Session.GetString("appointment_session");
            var existingAppointment = _context.patients.FirstOrDefault(a => a.Id == int.Parse(appoint_id));

            if (existingAppointment == null)
            {
                return NotFound("Appointment not found.");
            }
            // Update the status and any other necessary fields
            existingAppointment.status = 1;
            existingAppointment = _context.patients.FirstOrDefault(a => a.Id == patient.Id);
            //existingAppointment.date = patient.date;
            //existingAppointment.message = patient.message;
            //existingAppointment.Id = patient.Id;
            existingAppointment.admin_time = patient.admin_time;
            existingAppointment.admin_message = patient.admin_message;

            _context.patients.Update(existingAppointment);
            _context.SaveChanges();
            SendEmail(existingAppointment);
            return RedirectToAction("admin");
        }

        private void SendEmail(patient patient)
        {
            var fromAddress = new MailAddress("ankitsahudm573@gmail.com", "Helth Care");
            var toAddress = new MailAddress(patient.email, patient.name);
            const string fromPassword = "mzwo hqtb tkvy akua"; // Use your App Password here if 2FA is enabled, otherwise use your Gmail password
            const string subject = "Appointment Scheduled";
            string body = $"Dear {patient.name},\n\n" +
                          "Your appointment has been scheduled.\n\n" +
                          $"Details:\n" +
                          $"Your Subject is :{patient.department}  \n" +
                          $"Date Time: {patient.date}  \n" +
                          $"you can come Today at {patient.admin_time} \n" +
                          $"Message: {patient.admin_message}\n\n" +
                          "Thank you.";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                try
                {
                    smtp.Send(message);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine("Error sending email: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }
            }
        }
        public IActionResult Adminlogout()
        {
            //HttpContext.Session.GetString("yy")
            HttpContext.Session.Remove("admin_session");
            return RedirectToAction("Login_form");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Set the ViewData value
            ViewData["total_user"] = _context.patients.Count();
            ViewData["total_appointments"] = _context.patients.Count();
            ViewData["pending_appoint"] = _context.patients.Count(a => a.status == 0);
            ViewData["confirmed_appoint"] = _context.patients.Count(a => a.status == 1);

            var admin_profile = HttpContext.Session.GetString("admin_session");
            if (admin_profile !=null)
            {
                var profile = _context.tbl_admin.AsNoTracking().FirstOrDefault(u => u.Id == int.Parse(admin_profile));
                if (profile != null)
                {
                    ViewData["admin_pic"] = profile.admin_profile;
                    ViewData["admin_namee"] = profile.admin_name;
                    
                }
            }

            var allbranch = _context.tbl_Branch.AsNoTracking().FirstOrDefault();
            if (allbranch != null)
            {
                ViewData["Branch_Logo"] = Url.Content(allbranch.Branch_Logo);
            }

            base.OnActionExecuting(context);

        }

        public IActionResult Doctor()
        {
            ViewData["viewDoctore"] = _context.tbl__Doctor.ToList();
            return View(_context.tbl__Doctor.ToList());
        }

        [HttpPost]

        public IActionResult Doctor(Doctor doctor, IFormFile doctor_profile)
        {
            if (doctor_profile != null)
            {

                var filepath = Path.Combine(_Environment.WebRootPath, "Dr_pic", doctor_profile.FileName);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    //doctor.doctor_profile = profile.FileName;
                    doctor.doctor_profile = doctor_profile.FileName;
                    doctor_profile.CopyTo(stream);

                }
            }
            if (doctor.doctor_id != null)

            {
                var existdoc = _context.tbl__Doctor.Find(doctor.doctor_id);

                _context.SaveChanges();
            }
            else
            {
                _context.tbl__Doctor.Add(doctor);
                _context.SaveChanges();
            }
            return RedirectToAction("Doctor");
        }

        [HttpGet]
        public IActionResult Doctor_get(int id)
        {
            var sub = _context.tbl__Doctor.Find(id);
            var subclass = _context.tbl__Doctor.ToList();
            //ViewData["viewDoctore"] = _context.tbl__Doctor.ToList();

            ViewBag.id = sub.doctor_id;
            ViewBag.name = sub.doctor_name;
            ViewBag.email = sub.doctor_email;
            ViewBag.number = sub.doctor_number;
            ViewBag.linkedin_profile = sub.doctor_linkedin_profile;
            ViewData["image"] = sub.doctor_profile;
            //ViewData["number"]=sub.doctor_number;
            //ViewBag.gender = sub.doctor_gender;
            ViewData["department"] = sub.doctor_department;
            ViewData["Gender"] = sub.doctor_gender;
            //ViewData["profile"] = sub.doctor_profile;
            //ViewBag.ImagePath = sub.doctor_profile;
            //ViewBag.Department = sub.doctor_department;
            return View("Doctor", subclass);

        }

        //[HttpPost]
        //public IActionResult SaveDoctor(int? doctor_id,Doctor doctor,IFormFile doctor_profile)
        //{

        //       // var exist_doctor = _context.tbl__Doctor.Find(doctor_id);
        //    //if (doctor_profile != null)
        //    //{
        //    //    var filepata = Path.Combine(_Environment.WebRootPath, "Dr_pic", doctor_profile.FileName);
        //    //    using (var Stream = new FileStream(filepata, FileMode.Create))
        //    //    {
        //    //        doctor_profile.CopyTo(Stream);
        //    //        doctor.doctor_profile = doctor_profile.FileName;
        //    //    }
        //    //}

        //    _context.tbl__Doctor.Update(doctor);
        //    _context.SaveChanges();

        //    return RedirectToAction("Doctor");
        //}
        public IActionResult Doctor_Update(int id)
        {

            var upda = _context.tbl__Doctor.Find(id);
            ViewBag.image = upda.doctor_profile;
            // ViewData["img"] = upda.doctor_profile;
            //ViewData["department"] = upda.doctor_department;
            //ViewData["Gender"] = upda.doctor_gender;
            return View(upda);

        }

        [HttpPost]

        public IActionResult doctor_update(Doctor id, IFormFile doctor_profile)
        {
            
            if (doctor_profile != null)
            {//
                //old image
                
                var filepate = Path.Combine(_Environment.WebRootPath, "Dr_pic", doctor_profile.FileName);

                using (var Stream = new FileStream(filepate, FileMode.Create))
                {
                    doctor_profile.CopyTo(Stream);
                    id.doctor_profile = doctor_profile.FileName;
                }

                

                //  ViewBag.ImagePath = doctor_profile.FileName;
            }
            else
            {
                // Doctor_pic is null, retain existing image path
                var existingDoc = _context.tbl__Doctor.AsNoTracking().FirstOrDefault(d => d.doctor_id == id.doctor_id);
                if (existingDoc != null)
                {
                    id.doctor_profile = existingDoc.doctor_profile; // Retain existing image path
                }
            }
            _context.tbl__Doctor.Update(id);
            _context.SaveChanges();
            return RedirectToAction("Doctor");




            //@ViewData["profile"] = "/Dr_pic/"+doctor_profile;


        }
        public IActionResult Branch()
        {
            
            return View(_context.tbl_Branch.ToList());

        }
        [HttpPost]
        public IActionResult Branch(Branch add,IFormFile logo)
        {
            if(logo!=null)
            {
                var Filepata = Path.Combine(_Environment.WebRootPath, "Dr_pic", logo.FileName);
                using (var Stream = new FileStream(Filepata, FileMode.Create))
                {
                    logo.CopyTo(Stream);
                    add.Branch_Logo=logo.FileName;
                }
            }
            _context.tbl_Branch.Add(add);
            _context.SaveChanges();
            return View("Branch");
        }
        [HttpGet]
        public IActionResult Branch_get(int id)
        {
            var FindData=_context.tbl_Branch.Find(id);
            var Subclass = _context.tbl_Branch.ToList();
            
            if(FindData !=null)
            { 
            ViewBag.branchID = FindData.Branch_Id;
            ViewBag.number = FindData.Branch_number;
            ViewBag.Description = FindData.Branch_Description;
            ViewBag.email = FindData.Branch_email;
            ViewBag.Day_Mon_Fri = FindData.Branch_mon_fri;
            ViewBag.saturday= FindData.Branch_Saturday;
            ViewBag.sanday = FindData.Branch_sunday;
            ViewBag.facebook = FindData.Branch_facebook;
            ViewBag.instagram = FindData.Branch_instagram;
            ViewBag.twitter = FindData.Branch_twitter;
            ViewData["Branch_Logoo"] = FindData.Branch_Logo;
            }
            return View("Branch", Subclass);
        }
        public IActionResult Branch_Update(int id)
        {
            var BranchUpdate = _context.tbl_Branch.Find(id);
            return View(BranchUpdate);

        }
        [HttpPost]
        public IActionResult Branch_Update(Branch branch,IFormFile Branch_Logo)
        {
            if(Branch_Logo != null)
            {
                var Filepata = Path.Combine(_Environment.WebRootPath, "Dr_pic", Branch_Logo.FileName);
                using (var stream=new FileStream(Filepata,FileMode.Create))
                {
                    Branch_Logo.CopyTo(stream);
                    branch.Branch_Logo= Branch_Logo.FileName;
                }
            }
            else
            {
                var existingDoc = _context.tbl_Branch.AsNoTracking().FirstOrDefault(d => d.Branch_Id == branch.Branch_Id);
                if (existingDoc != null)
                {
                    branch.Branch_Logo=existingDoc.Branch_Logo;
                }
            }
            _context.tbl_Branch.Update(branch);
            _context.SaveChanges();
            return RedirectToAction("Branch");

        }

        public IActionResult Admin_Profile()
        {
          
            return View();
        }

        [HttpPost]
        public IActionResult Admin_Profile(Models.admin add, IFormFile admin_profile)
        {
            if (admin_profile != null && admin_profile.Length > 0)
            {
                var fileName = Path.GetFileName(admin_profile.FileName);
                var filePath = Path.Combine(_Environment.WebRootPath, "Dr_pic", fileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    admin_profile.CopyTo(stream);
                }

                // Set the file name in the model
                add.admin_profile = fileName;
            }

            if (add.Id > 0) // Assuming Id is a positive integer for existing records
            {
                var existdoc = _context.tbl_admin.Find(add.Id);
                if (existdoc != null)
                {
                    // Update existing record
                    existdoc.admin_name = add.admin_name;
                    existdoc.admin_email = add.admin_email;
                    existdoc.admin_profile = add.admin_profile;
                    // Update other fields as necessary

                    _context.tbl_admin.Update(existdoc);
                }
            }
            else
            {
                // Add new record
                _context.tbl_admin.Add(add);
            }

            _context.SaveChanges();

            // Optionally, return to the same view or redirect to a different action
            return RedirectToAction("Admin_Profile"); // Use RedirectToAction to avoid re-posting data
        }

        //[HttpPost]
        //public IActionResult Admin_Profile(Models.admin ad, IFormFile admin_profile)
        //{

        //    if (admin_profile != null)
        //    {
        //        var Filepata = Path.Combine(_Environment.WebRootPath, "Admin_profile", admin_profile.FileName);
        //        using (var Stream = new FileStream(Filepata, FileMode.Create))
        //        {
        //            admin_profile.CopyTo(Stream);
        //            ad.admin_profile = admin_profile.FileName;
        //        }
        //    }
        //    _context.tbl_admin.Add(ad);
        //    _context.SaveChanges();
        //    return RedirectToAction("Admin_Profile");

        //}
        //public IActionResult Admin_Profile_Update(int id)
        //{
        //    var HoldData = _context.tbl_admin.Find(id);

        //    return View(HoldData);

        //}



        //[HttpPost]
        //public IActionResult Admin_Profile_Update(Models.admin ad, IFormFile admin_profile)
        //{
        //    // Fetch existing admin details
        //    var existingAdmin = _context.tbl_admin.FirstOrDefault(a => a.Id == ad.Id);
        //    if (existingAdmin != null)
        //    {
        //        // Update email and password
        //        existingAdmin.admin_email = ad.admin_email;
        //        existingAdmin.admin_Password = ad.admin_Password;
        //        existingAdmin.admin_name = ad.admin_name;

        //        if (admin_profile != null)
        //        {
        //            // Save new profile image
        //            var filePath = Path.Combine(_Environment.WebRootPath, "Admin_profile", admin_profile.FileName);
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                admin_profile.CopyTo(stream);
        //                existingAdmin.admin_profile = admin_profile.FileName;
        //            }
        //        }
        //        else
        //        {
        //            // Maintain existing profile image
        //            ad.admin_profile = existingAdmin.admin_profile;
        //        }
        //    }
        //    else
        //    {
        //        // New admin record
        //        if (admin_profile != null)
        //        {
        //            var filePath = Path.Combine(_Environment.WebRootPath, "Admin_profile", admin_profile.FileName);
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                admin_profile.CopyTo(stream);
        //                ad.admin_profile = admin_profile.FileName;
        //            }
        //        }

        //        _context.tbl_admin.Add(ad);
        //    }

        //    _context.SaveChanges();
        //    return RedirectToAction("Admin_Profile");
        //}



        [HttpGet]
        public IActionResult Admin_Profile_Get(int id)
        {
            var FineData = _context.tbl_admin.Find(id);
            var subclass = _context.tbl_admin.ToList();
            if(FineData !=null)
            {
                ViewBag.Id = FineData.Id;
                ViewBag.admin_email = FineData.admin_email;
                ViewBag.admin_Password = FineData.admin_Password;
                ViewBag.admin_profile = FineData.admin_profile;
                ViewBag.admin_name = FineData.admin_name;
                ViewData["admin_picc"] = FineData.admin_profile;
            }
            return View("Admin_Profile", subclass);

        }

        public IActionResult Admin_information()
        {
            string admin_id = HttpContext.Session.GetString("admin_session"); // Corrected session key
            if (!string.IsNullOrEmpty(admin_id))
            {
                if (int.TryParse(admin_id, out int adminId))
                {
                    var row = _context.tbl_admin.Where(a => a.Id == adminId).FirstOrDefault();
                    return View(row);
                }
                else
                {
                    // Handle the case where admin_id is not a valid integer
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        [HttpPost]
        public IActionResult Update_Admin_Profile(Models.admin adminn, IFormFile admin_profile)
        {
            // Check if a new profile picture is provided
            if (admin_profile != null )
            {
                // Generate a safe filename and determine the file path
                //var safeFileName = Path.GetFileName(admin_profile.FileName); // Ensure the filename is safe
                var filePath = Path.Combine(_Environment.WebRootPath, "Admin_Profile", admin_profile.FileName);

                // Save the new profile picture
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    admin_profile.CopyTo(stream);
                    adminn.admin_profile = admin_profile.FileName;
                }
            }
            else
            {
                // Retain the existing profile picture if no new file is provided
                var existingAdmin = _context.tbl_admin.AsNoTracking().FirstOrDefault(d => d.Id == adminn.Id);
                if (existingAdmin != null)
                {
                    adminn.admin_profile = existingAdmin.admin_profile;
                }
            }

            // Check if the model state is valid
            
                // Update the admin information
                _context.tbl_admin.Update(adminn);
                _context.SaveChanges();
                return RedirectToAction("Admin_information");
            

            // If the model state is invalid, return the same view with the admin model
          
        }




    }
}




