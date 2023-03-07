namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminAphaIdadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "AdminAlphaId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "AdminAlphaId");
        }
    }
}
