using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Models
{
    public class Department
    {
        public int Id { get; set; }

        [DisplayName("Department Name:")]
        public string DepartmentName { get; set; }

    }
}
