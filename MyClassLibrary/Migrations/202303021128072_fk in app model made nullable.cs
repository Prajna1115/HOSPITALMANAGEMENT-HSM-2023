namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkinappmodelmadenullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "FeedbackId", "dbo.Feedbacks");
            DropIndex("dbo.Appointments", new[] { "FeedbackId" });
            AlterColumn("dbo.Appointments", "FeedbackId", c => c.Int());
            CreateIndex("dbo.Appointments", "FeedbackId");
            AddForeignKey("dbo.Appointments", "FeedbackId", "dbo.Feedbacks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "FeedbackId", "dbo.Feedbacks");
            DropIndex("dbo.Appointments", new[] { "FeedbackId" });
            AlterColumn("dbo.Appointments", "FeedbackId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "FeedbackId");
            AddForeignKey("dbo.Appointments", "FeedbackId", "dbo.Feedbacks", "Id", cascadeDelete: true);
        }
    }
}
