using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Models
{
    public class Admin
    {

        public int Id { get; set; }

        public string AdminAlphaId { get; set; }

        [DisplayName("Name:")]
        public string AdminName { get; set; }

        [DisplayName("Age:")]
        public int AdminAge { get; set; }

        [DisplayName("Mobile:")]
        public string AdminMobile { get; set; }

        [DisplayName("Address:")]
        public string AdminAddress { get; set; }

        [DisplayName("Gender:")]
        public Gender AdminGender { get; set; }

        public string AdminImagePath { get; set; }
    }


}
