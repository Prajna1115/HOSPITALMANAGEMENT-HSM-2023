using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Models
{
    public class Doctor
    {

        public int Id { get; set; }

        public string DoctorAplhaId { get; set; }

        [ForeignKey("dptid")]
        public int DepartmentId { get; set; }

        public Department dptid { get; set; }

        [DisplayName("Name:")]
        public string DoctorName { get; set; }

        [DisplayName("Age:")]
        public int DoctorAge { get; set; }

        [DisplayName("Years Of Experience")]
        public int DoctorYoe { get; set; }

        [DisplayName("Available Days:")]
        public string DoctorDaysAvailable { get; set; }

        [DisplayName("Available:")]
        public IsAvailable DoctorIsAvailable { get; set; }

        public string DoctorImagePath { get; set; }

        [DisplayName("Available Slots:")]
        public string DoctorAvailableSlots { get; set; }

    }

    public enum IsAvailable
    {
        No,
        Yes
    }
}
