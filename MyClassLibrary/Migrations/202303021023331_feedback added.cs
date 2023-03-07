namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedbackadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeedbackDesc = c.String(),
                        FeedbackRating = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Appointments", "IsFeedbackGiven", c => c.String());
            AddColumn("dbo.Appointments", "FeedbackId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "FeedbackId");
            AddForeignKey("dbo.Appointments", "FeedbackId", "dbo.Feedbacks", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "FeedbackId", "dbo.Feedbacks");
            DropIndex("dbo.Appointments", new[] { "FeedbackId" });
            DropColumn("dbo.Appointments", "FeedbackId");
            DropColumn("dbo.Appointments", "IsFeedbackGiven");
            DropTable("dbo.Feedbacks");
        }
    }
}
