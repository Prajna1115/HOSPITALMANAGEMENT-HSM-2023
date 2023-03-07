namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Doctormodelslotsadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "DoctorAvailableSlots", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "DoctorAvailableSlots");
        }
    }
}
