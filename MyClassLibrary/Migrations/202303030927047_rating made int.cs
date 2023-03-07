namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingmadeint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feedbacks", "FeedbackRating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "FeedbackRating", c => c.String());
        }
    }
}
