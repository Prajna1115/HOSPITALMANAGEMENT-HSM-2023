using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Models
{
    public class Appointment
    {

        public int Id { get; set; }


        [ForeignKey("dptid")]
        public int DepartmentId { get; set; }

        public Department dptid { get; set; }
        
        [ForeignKey("docid")]
        public int DoctorId { get; set; }

        public Doctor docid { get; set; }

        [ForeignKey("ptid")]
        public int PatientId { get; set; }

        public Patient ptid { get; set; }

        [DisplayName("Appointment Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentDate { get; set; }

        [DisplayName("Appointment Day:")]
        public string AppointmentDay { get; set; }

        [DisplayName("Appointment Slot:")]
        public string AppointmentSlot { get; set; }

        public Status AppointmentStatus { get; set; }

        public string IsFeedbackGiven { get; set; }

        [ForeignKey("fbid")]
        public int? FeedbackId { get; set; }

        public Feedback fbid { get; set; }

        public bool isFeedbackShow { get; set; }

    }

    public enum Status
    {
        
        Pending,
        Completed
    }  
    
    


}
