namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patietmodelbgadded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "PatientBloodGroup", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "PatientBloodGroup", c => c.String());
        }
    }
}
