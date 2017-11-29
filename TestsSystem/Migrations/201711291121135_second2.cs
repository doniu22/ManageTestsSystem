namespace TestsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "Status");
        }
    }
}
