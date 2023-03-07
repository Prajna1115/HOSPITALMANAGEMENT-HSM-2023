using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyClassLibrary.Models
{
    public class Patient
    {

        public int Id { get; set; }

        public string PatientAplhaId { get; set; }

        [DisplayName("Name:")]
        public string PatientName { get; set; }

        [DisplayName("Mobile:")]
        public string PatientMobile { get; set; }

        [DisplayName("Age:")]
        public int PatientAge { get; set; }

        [DisplayName("Gender:")]
        public Gender PatientGender { get; set; }

        [DisplayName("BloodGroup:")]
        public BloodGroup PatientBloodGroup { get; set; }

        [DisplayName("Address:")]
        public string PatientAddress { get; set; }

         [DisplayName("Image:")]
        public string PatientImagePath { get; set; }

    }

    public enum Gender
    {
        Male,
        Female,
        Others
    }

    public enum BloodGroup
    {
        [Display(Name = "A+")]
        APlus,

        [Display(Name = "A-")]
        AMinus,

        [Display(Name = "B+")]
        BPlus,

        [Display(Name = "B-")]
        BMinus,

        [Display(Name = "AB+")]
        ABPlus,

        [Display(Name = "AB-")]
        ABMinus,


        [Display(Name = "O+")]
        OPlus,

        [Display(Name = "O-")]
        OMinus,


    }
}
