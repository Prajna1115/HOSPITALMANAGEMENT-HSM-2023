namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageaddddtopatientmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "PatientImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "PatientImagePath");
        }
    }
}
