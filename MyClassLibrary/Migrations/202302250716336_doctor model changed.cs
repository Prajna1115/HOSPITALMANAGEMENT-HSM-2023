namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctormodelchanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "DoctorAplhaId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "DoctorAplhaId");
        }
    }
}
