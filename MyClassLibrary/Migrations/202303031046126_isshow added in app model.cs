namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isshowaddedinappmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "isFeedbackShow", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "isFeedbackShow");
        }
    }
}
