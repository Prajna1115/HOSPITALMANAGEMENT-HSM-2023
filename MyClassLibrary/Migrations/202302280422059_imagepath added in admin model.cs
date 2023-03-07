namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagepathaddedinadminmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "AdminImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "AdminImagePath");
        }
    }
}
