namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentmodeladdedslotsstring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "AppointmentDay", c => c.String());
            AddColumn("dbo.Appointments", "AppointmentSlot", c => c.String());
            DropColumn("dbo.Appointments", "AppointmentTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "AppointmentTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "AppointmentSlot");
            DropColumn("dbo.Appointments", "AppointmentDay");
        }
    }
}
