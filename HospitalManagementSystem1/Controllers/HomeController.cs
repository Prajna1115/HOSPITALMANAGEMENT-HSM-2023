using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MyClassLibrary;
using MyClassLibrary.Models;

namespace HospitalManagementSystem1.Controllers
{
    public class HomeController : Controller
    {
        EcomContext db =new EcomContext();
        public ActionResult Index()
        {
            List<Appointment> appointments = db.Appointments.Include(e => e.fbid).Include(e => e.ptid).Where(e=>e.isFeedbackShow == true).ToList();

            return View(appointments);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult OurSpecialists()
        {
            return View(db.Doctors.Include(e=>e.dptid).ToList());
        }

    }
}