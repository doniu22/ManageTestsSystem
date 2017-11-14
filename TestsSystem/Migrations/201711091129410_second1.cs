namespace TestsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PossibleAnswers", "isCorrect", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PossibleAnswers", "isCorrect", c => c.Boolean(nullable: false));
        }
    }
}
