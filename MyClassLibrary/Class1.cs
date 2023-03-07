using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClassLibrary.Models;

namespace MyClassLibrary
{
    public class Class1
    {
    }
 
    public class EcomContext:DbContext{
    
    public DbSet<Patient>Patients { get; set;}
    public DbSet<Department>Departments { get; set;}
    public DbSet<Doctor>Doctors { get; set;}
    public DbSet<Appointment>Appointments { get; set;}
    public DbSet<Admin>Admins { get; set;}
    public DbSet<Feedback>Feedbacks { get; set;}

      

    }
}
