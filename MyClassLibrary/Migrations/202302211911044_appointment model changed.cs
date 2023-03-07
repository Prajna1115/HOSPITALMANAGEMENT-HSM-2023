namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentmodelchanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "DepartmentId");
            AddForeignKey("dbo.Appointments", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Appointments", new[] { "DepartmentId" });
            DropColumn("dbo.Appointments", "DepartmentId");
        }
    }
}
