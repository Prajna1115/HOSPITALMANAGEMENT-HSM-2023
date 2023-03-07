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
    public class Feedback
    {

        public int Id { get; set; }

        [DisplayName("Feedback Description :")]
        public string FeedbackDesc { get; set; }

        [DisplayName("Feedback Rating:")]
        public int FeedbackRating { get; set; }

    }

   
}
