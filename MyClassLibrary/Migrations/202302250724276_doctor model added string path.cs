namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctormodeladdedstringpath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "DoctorImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "DoctorImagePath");
        }
    }
}
