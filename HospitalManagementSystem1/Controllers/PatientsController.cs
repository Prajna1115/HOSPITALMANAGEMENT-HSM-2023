using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using MyClassLibrary;
using MyClassLibrary.Models;

namespace HospitalManagementSystem1.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private EcomContext db = new EcomContext();

        // GET: Patients
        public ActionResult Index()
        {
            string AlphaCurrentId= HttpContext.User.Identity.GetUserId();
            Patient patient = db.Patients.Where(e => e.PatientAplhaId == AlphaCurrentId).FirstOrDefault<Patient>();

            //if patient is null i.e no patient data is in the patients table redirect to patient form otherise show patient data
            if(patient == null)
            {
                TempData["msg"] = "Sorry we dont have your details pls create them!";
                return RedirectToAction("Create");
            }
            else
            {
               
                return View(patient);
            }
        }


        // GET: Patients/Create
        public ActionResult Create()
        {
            //if a user clicks on pateint form so check if data already exists or not

            string AlphaCurrentId = HttpContext.User.Identity.GetUserId();
            Patient patient = db.Patients.Where(e => e.PatientAplhaId == AlphaCurrentId).FirstOrDefault<Patient>();

            //if patient is null i.e no patient data is in the patients table direct to patient form otherise show patient data
            if (patient == null)
            {
                return View();
            }
            else
            {
                TempData["msg"] = "we already have your details!";
                return RedirectToAction("Index");
            }

           

        }

        // POST: Patients/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PatientName,PatientMobile,PatientAge,PatientGender,PatientBloodGroup,PatientAddress")] Patient patient,HttpPostedFileBase file)
        {
            string name = file.FileName;
            string path = "~/Images/" + name;
            file.SaveAs(Server.MapPath(path)); //file stored on server

            if (ModelState.IsValid)
            {
                patient.PatientAplhaId = HttpContext.User.Identity.GetUserId();
                patient.PatientImagePath = path;
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");

            }


            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientName,PatientMobile,PatientAge,PatientGender,PatientBloodGroup,PatientAddress")] Patient patient,HttpPostedFileBase file)
        {
           

            if (ModelState.IsValid)
            {
                if (file == null)
                {
                //setting old image path to edited image path
                Patient OldPatientData=db.Patients.Where(e=>e.Id==patient.Id).FirstOrDefault<Patient>();
                patient.PatientImagePath = OldPatientData.PatientImagePath;

                }
                else
                {
                    string name = file.FileName;
                    string path = "~/Images/" + name;
                    file.SaveAs(Server.MapPath(path)); //file stored on server
                    patient.PatientImagePath=path;//new image

                }


                patient.PatientAplhaId = HttpContext.User.Identity.GetUserId();

                db.Patients.AddOrUpdate(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Create appointment/5
        public ActionResult CreateAppointment()
        {

            //fetching the currently signed patient id
            string AlphaCurrentId = HttpContext.User.Identity.GetUserId();
            Patient CurrentPatient = db.Patients.Where(e => e.PatientAplhaId == AlphaCurrentId).FirstOrDefault<Patient>();

            if (CurrentPatient == null)
            {
                TempData["msg"] = "Sorry we dont have your details pls create them!";
                return RedirectToAction("Create");
            }
            else
            {
                
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "DoctorName");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName");

            return View();
            }


        }

        // POST: Patients/Create appointment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAppointment([Bind(Include = "Id,DepartmentId,DoctorId,AppointmentDate,AppointmentSlot,AppointmentDay")] Appointment appointment,string AppointmentDay)
        {
            if (appointment.AppointmentDay!=null && appointment.AppointmentSlot != null && appointment.AppointmentSlot != null && appointment.AppointmentSlot != "Select")
            {
                if (ModelState.IsValid)
                {
                    //fetching the currently signed patient id
                    string AlphaCurrentId = HttpContext.User.Identity.GetUserId();
                    Patient CurrentPatient = db.Patients.Where(e => e.PatientAplhaId == AlphaCurrentId).FirstOrDefault<Patient>();

                    if (CurrentPatient == null)
                    {
                        TempData["msg"] = "Sorry we dont have your details pls create them!";
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        int CurrentpatientId = CurrentPatient.Id;
                        appointment.PatientId = CurrentpatientId;

                        appointment.AppointmentStatus = 0; //default status is not completed
                        appointment.IsFeedbackGiven = "false"; //default feedback not given at time of appointment creation
                        appointment.isFeedbackShow = false;//default false

                        //appointment.AppointmentDay = AppointmentDay;
                        db.Appointments.Add(appointment);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

                return View(appointment);
            }
            else
            {
                TempData["msgnew"] = "Sorry pls choose an available doctor and his/her slot time and create again!";
                return RedirectToAction("CreateAppointment");
            }


        }


        // GET: Patients/view appointments/5
        public ActionResult ViewAppointments()
        {
            //passing the list of appointemetns made by the currently signed in user
            string AlphaCurrentId = HttpContext.User.Identity.GetUserId();

            Patient CurrentPatient = db.Patients.Where(e => e.PatientAplhaId == AlphaCurrentId).FirstOrDefault<Patient>();


            if (CurrentPatient==null) {
                TempData["msg"] = "Sorry we dont have your details pls create them!";
                return RedirectToAction("Create");
            }
            else
            {
            int CurrentpatientId = CurrentPatient.Id;

            List<Appointment> patientAppointments = db.Appointments.Include(e=>e.fbid).Include(e=>e.dptid).Include(e=>e.docid).Where(e => e.PatientId == CurrentpatientId).ToList();
           
                return View(patientAppointments);
            }


        }


        //ajaxcall to fetch doctor names
        public JsonResult fetchDoctorNames(int selectedDptId,string dayType)
        {
            //list of doctors having the department selected as given
            List<Doctor> doctorsList = db.Doctors.Where(e => e.DepartmentId == selectedDptId).ToList();
          List<Doctor> doctorNames=new List<Doctor>();//empty list created of Doctor type

            //conditionally adding the doctors with daysAvailable if matched with input dayType in the JsonResult List
            foreach (Doctor doc in doctorsList)
            {
                //convert string to string array
                string AvailableDaysString = doc.DoctorDaysAvailable;
                string[] AvailableDaysArr= AvailableDaysString.Split(',');
                bool exists = false; //default false

                //check if the dayType exists in the array or    
                foreach (string day in AvailableDaysArr)
                {
                   if(day.ToLower() == dayType.ToLower()) { exists = true; break; }
                }


                //if exists then add that doctor to the doctorNames List which is passed As JsonResult
                if (exists)
                {
                         doctorNames.Add(

                                 new Doctor()
                                 {

                                     DoctorName = doc.DoctorName,
                                     Id = doc.Id,

                                 }

                             );
                }
            }

            return Json(doctorNames, JsonRequestBehavior.AllowGet);
        }

        //ajaxcall to fetch available slots of the doctor
        public JsonResult fetchDoctorSlots(int selectedDoctorId)
        {

            Doctor doctor = db.Doctors.Where(e => e.Id == selectedDoctorId).FirstOrDefault<Doctor>();
            var slotsString = doctor.DoctorAvailableSlots;



            return Json(slotsString, JsonRequestBehavior.AllowGet);
        }

        //GET:: CreateFeeback 
        public ActionResult CreateFeeback(int id)
        {
            ViewBag.ClickedAppointmentId = id;
            return View();
        }

        //Post:: CreateFeeback 
        [HttpPost]
        public ActionResult CreateFeeback(Feedback fb,string ClickedAppointmentId)
        {

            db.Feedbacks.Add(fb);
            db.SaveChanges();

            int appointmentId = int.Parse(ClickedAppointmentId);

            //find the appointment by id and update the feedbackstarus and fk id

            Appointment appointment = db.Appointments.Find(appointmentId);
            appointment.IsFeedbackGiven = "true";
            appointment.FeedbackId = fb.Id; //currently created feedback id

            db.Appointments.AddOrUpdate(appointment);
            db.SaveChanges();

            return RedirectToAction("ViewAppointments");
        }

    }
}
