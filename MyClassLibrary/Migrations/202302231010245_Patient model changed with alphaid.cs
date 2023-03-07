namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Patientmodelchangedwithalphaid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "PatientAplhaId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "PatientAplhaId");
        }
    }
}
