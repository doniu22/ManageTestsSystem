namespace TestsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PossibleAnswers",
                c => new
                    {
                        Id_posans = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        isCorrect = c.Int(nullable: false),
                        Question_Id_question = c.Int(),
                    })
                .PrimaryKey(t => t.Id_posans)
                .ForeignKey("dbo.Questions", t => t.Question_Id_question)
                .Index(t => t.Question_Id_question);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id_question = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Test_Id_testu = c.Int(),
                    })
                .PrimaryKey(t => t.Id_question)
                .ForeignKey("dbo.Tests", t => t.Test_Id_testu)
                .Index(t => t.Test_Id_testu);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id_testu = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Subject = c.String(),
                        Created_at = c.String(),
                        Created_by = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id_testu)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Test_Id_testu", "dbo.Tests");
            DropForeignKey("dbo.Tests", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PossibleAnswers", "Question_Id_question", "dbo.Questions");
            DropIndex("dbo.Tests", new[] { "Owner_Id" });
            DropIndex("dbo.Questions", new[] { "Test_Id_testu" });
            DropIndex("dbo.PossibleAnswers", new[] { "Question_Id_question" });
            DropTable("dbo.Tests");
            DropTable("dbo.Questions");
            DropTable("dbo.PossibleAnswers");
        }
    }
}
