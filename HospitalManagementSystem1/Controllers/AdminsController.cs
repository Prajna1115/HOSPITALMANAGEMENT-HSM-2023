using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyClassLibrary;
using MyClassLibrary.Models;

namespace HospitalManagementSystem1.Controllers
{
    [Authorize]
    public class AdminsController : Controller
    {
        private EcomContext db = new EcomContext();

        CRUD crd=new CRUD();

        // GET: Admins
        public ActionResult Index()
        {
            string AlphaCurrentId = HttpContext.User.Identity.GetUserId();
            Admin admin = db.Admins.Where(e => e.AdminAlphaId == AlphaCurrentId).FirstOrDefault<Admin>();

            //if Admin is null i.e no patient data is in the patients table redirect to patient form otherise show patient data
            if (admin == null)
            {
                TempData["msg"] = "Sorry we dont have your details pls create them!";
                return RedirectToAction("Create");
            }
            else
            {

                return View(admin);
            }
        }

       

        // GET: Admins/Create
        public ActionResult Create()
        {
            //if a user clicks on admin form so check if data already exists or not

            string AlphaCurrentId = HttpContext.User.Identity.GetUserId();
            Admin admin = db.Admins.Where(e => e.AdminAlphaId == AlphaCurrentId).FirstOrDefault<Admin>();

            //if admin is null i.e no admin data is in the admin table direct to admin form otherise show admin data
            if (admin == null)
            {
                return View();
            }
            else
            {
                TempData["msg"] = "we already have your details!";
                return RedirectToAction("Index");
            }
        }

        // POST: Admins/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AdminName,AdminAge,AdminMobile,AdminAddress,AdminGender")] Admin admin, HttpPostedFileBase file)
        {

            string name = file.FileName;
            string path = "~/Images/" + name;
            file.SaveAs(Server.MapPath(path)); //file stored on server

            if (ModelState.IsValid)
            {
                //adding the AplhaId to the database of Admin
                admin.AdminAlphaId = HttpContext.User.Identity.GetUserId();
                admin.AdminImagePath = path;
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AdminName,AdminAge,AdminMobile,AdminAddress,AdminGender")] Admin admin, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {


                if (file == null)
                {
                    //setting old image path to edited image path
                    Admin OldAdminData = db.Admins.Where(e => e.Id == admin.Id).FirstOrDefault<Admin>();
                    admin.AdminImagePath = OldAdminData.AdminImagePath;

                }
                else
                {
                    string name = file.FileName;
                    string path = "~/Images/" + name;
                    file.SaveAs(Server.MapPath(path)); //file stored on server
                    admin.AdminImagePath = path;//new image

                }

                admin.AdminAlphaId = HttpContext.User.Identity.GetUserId();

                db.Admins.AddOrUpdate(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        //GET:View All Patients
        public ActionResult ViewAllPatients()
        {

            return View(db.Patients.ToList());
        }

        //delete Patient 
        public ActionResult DeletePatient(int id,string alphaId)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();

            crd.deleteFromAspNetUsers(alphaId);

            return RedirectToAction("ViewAllPatients");

        }


        //GET:View All Doctors
        public ActionResult ViewAllDoctors()
        {
            //include Foreinkey of departments table
            return View(db.Doctors.Include(e=>e.dptid).ToList());
        }


        //delete Doctor 
        public ActionResult DeleteDoctor(int id, string alphaId)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();

            crd.deleteFromAspNetUsers(alphaId);

            return RedirectToAction("ViewAllDoctors");

        }



        //GET:View All Appointments
        public ActionResult ViewAllAppointments()
        {
            //include Fk of departments,pateints.doctors table
            return View(db.Appointments.Include(e => e.fbid).Include(e=>e.docid).Include(e=>e.dptid).Include(e=>e.ptid).ToList());
        }

        //delete Appointment
        public ActionResult DeleteAppointment(int id, string alphaId)
        {
           Appointment appointment= db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();

            crd.deleteFromAspNetUsers(alphaId);

            return RedirectToAction("ViewAllAppointments");

        }


        //ajax call to display count
        public JsonResult fetchCount()
        {
            int patientCount=db.Patients.Count();
            int doctorCount=db.Doctors.Count();
            int adminCount=db.Admins.Count();
            int appointmentCount = db.Appointments.Count();
            int fiveStarAppCount=db.Appointments.Where(e=>e.fbid.FeedbackRating==5).Count();

            int[] countArr = { patientCount, doctorCount, adminCount, appointmentCount, fiveStarAppCount };

            return Json(countArr,JsonRequestBehavior.AllowGet);
        }

      //show feedback get 
      public ActionResult showFeedback(int id) {

            Appointment appointment = db.Appointments.Find(id);
            appointment.isFeedbackShow= true;

            db.Appointments.AddOrUpdate(appointment);
            db.SaveChanges();

            return RedirectToAction("ViewAllAppointments");

        } 
        
        //hide feedback get 
      public ActionResult hideFeedback(int id) {

            Appointment appointment = db.Appointments.Find(id);
            appointment.isFeedbackShow= false;

            db.Appointments.AddOrUpdate(appointment);
            db.SaveChanges();

            return RedirectToAction("ViewAllAppointments");

        }

    }
}
