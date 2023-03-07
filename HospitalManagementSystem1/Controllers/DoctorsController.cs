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
    public class DoctorsController : Controller
    {
        private EcomContext db = new EcomContext();

        // GET: Doctors
       
        public ActionResult Index()
        {
            string AlphaCurrentId = HttpContext.User.Identity.GetUserId();
            Doctor doctor = db.Doctors.Include(e=>e.dptid).Where(e => e.DoctorAplhaId == AlphaCurrentId).FirstOrDefault<Doctor>();

            //if doctor is null i.e no doctor data is in the doctors table redirect to doctor form otherise show doctor data
            if (doctor == null)
            {
                TempData["msg"] = "Sorry we dont have your details pls create them!";
                return RedirectToAction("Create");
            }
            else
            {
                
                return View(doctor);
            }

           
         
        }

    

        // GET: Doctors/Create
        public ActionResult Create()
        {

            //if a user clicks on doctor form so check if data already exists or not

            string AlphaCurrentId = HttpContext.User.Identity.GetUserId();
            Doctor doctor = db.Doctors.Where(e => e.DoctorAplhaId == AlphaCurrentId).FirstOrDefault<Doctor>();

            //if patient is null i.e no patient data is in the patients table direct to patient form otherise show patient data
            if (doctor == null)
            {
                ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName");
                return View();
            }
            else
            {
                TempData["msg"] = "we already have your details!";
                return RedirectToAction("Index");
            }



          
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartmentId,DoctorName,DoctorAge,DoctorYoe,DoctorDaysAvailable,DoctorIsAvailable,DoctorAvailableSlots")] Doctor doctor, HttpPostedFileBase file)
        {
            string name = file.FileName;
            string path = "~/Images/" + name;
            file.SaveAs(Server.MapPath(path)); //file stored on server

            if (ModelState.IsValid)
            {
                doctor.DoctorAplhaId = HttpContext.User.Identity.GetUserId();
                doctor.DoctorImagePath = path;
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", doctor.DepartmentId);
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", doctor.DepartmentId);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentId,DoctorName,DoctorAge,DoctorYoe,DoctorDaysAvailable,DoctorIsAvailable,DoctorAvailableSlots")] Doctor doctor, HttpPostedFileBase file)
        {


            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    //setting old image path to edited doctor path
                    Doctor oldDoctorData = db.Doctors.Where(e => e.Id == doctor.Id).FirstOrDefault<Doctor>();
                    doctor.DoctorImagePath = oldDoctorData.DoctorImagePath;

                   
                }
                else
                {

                    string name = file.FileName;
                    string path = "~/Images/" + name;
                    file.SaveAs(Server.MapPath(path)); //file stored on server
                    doctor.DoctorImagePath = path;//new iamge

                }

               

                doctor.DoctorAplhaId = HttpContext.User.Identity.GetUserId();

                db.Doctors.AddOrUpdate(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", doctor.DepartmentId);
            return View(doctor);
        }

      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //GET : view pending Appointments
        public ActionResult ViewPendingAppointments()
        {
            
            string AlphaCurrentId = HttpContext.User.Identity.GetUserId();

            Doctor CurrentDoctor = db.Doctors.Where(e => e.DoctorAplhaId == AlphaCurrentId).FirstOrDefault<Doctor>();

            if (CurrentDoctor == null)
            {
                TempData["msg"] = "Sorry we dont have your details pls create them!";
                return RedirectToAction("Create");
            }
            else
            {
                int CurrentDoctorId = CurrentDoctor.Id;

                //all appointemnts of current doctor that are pending
                List<Appointment> DoctorAppointments = db.Appointments.Include(e => e.ptid).Where(e => (e.DoctorId == CurrentDoctorId) && (e.AppointmentStatus == 0)).ToList();

                return View(DoctorAppointments);
            }

          
        }

        //GET : edit app status
        public ActionResult EditAppointmentStatus(int id)
        {

            //find appointment by id
            Appointment appointment = db.Appointments.Find(id);
            appointment.AppointmentStatus = (Status)1;

            //update it
            db.Appointments.AddOrUpdate(appointment);
            db.SaveChanges();

            return RedirectToAction("ViewPendingAppointments");
        }

        //GET : view all appointments
        public ActionResult ViewCompletedAppointments()
        {
           
            string AlphaCurrentId = HttpContext.User.Identity.GetUserId();

            Doctor CurrentDoctor = db.Doctors.Where(e => e.DoctorAplhaId == AlphaCurrentId).FirstOrDefault<Doctor>();



            if (CurrentDoctor == null)
            {
                TempData["msg"] = "Sorry we dont have your details pls create them!";
                return RedirectToAction("Create");
            }
            else
            {
                int CurrentDoctorId = CurrentDoctor.Id;

                //all appointemnts of current doctor that are completed
                List<Appointment> DoctorAppointments = db.Appointments.Include(e => e.ptid).Where(e => (e.DoctorId == CurrentDoctorId) && (e.AppointmentStatus != 0)).ToList();

                return View(DoctorAppointments);
            }



         
        }
    }
}
