namespace TestsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PossibleAnswers", "Question_Id_question", "dbo.Questions");
            DropForeignKey("dbo.Questions", "Test_Id_testu", "dbo.Tests");
            DropForeignKey("dbo.Tests", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PossibleAnswers", new[] { "Question_Id_question" });
            DropIndex("dbo.Questions", new[] { "Test_Id_testu" });
            DropIndex("dbo.Tests", new[] { "Owner_Id" });
            AlterColumn("dbo.PossibleAnswers", "Question_Id_question", c => c.Int(nullable: false));
            AlterColumn("dbo.Questions", "Test_Id_testu", c => c.Int(nullable: false));
            AlterColumn("dbo.Tests", "Owner_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.PossibleAnswers", "Question_Id_question");
            CreateIndex("dbo.Questions", "Test_Id_testu");
            CreateIndex("dbo.Tests", "Owner_Id");
            AddForeignKey("dbo.PossibleAnswers", "Question_Id_question", "dbo.Questions", "Id_question", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "Test_Id_testu", "dbo.Tests", "Id_testu", cascadeDelete: true);
            AddForeignKey("dbo.Tests", "Owner_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "Test_Id_testu", "dbo.Tests");
            DropForeignKey("dbo.PossibleAnswers", "Question_Id_question", "dbo.Questions");
            DropIndex("dbo.Tests", new[] { "Owner_Id" });
            DropIndex("dbo.Questions", new[] { "Test_Id_testu" });
            DropIndex("dbo.PossibleAnswers", new[] { "Question_Id_question" });
            AlterColumn("dbo.Tests", "Owner_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Questions", "Test_Id_testu", c => c.Int());
            AlterColumn("dbo.PossibleAnswers", "Question_Id_question", c => c.Int());
            CreateIndex("dbo.Tests", "Owner_Id");
            CreateIndex("dbo.Questions", "Test_Id_testu");
            CreateIndex("dbo.PossibleAnswers", "Question_Id_question");
            AddForeignKey("dbo.Tests", "Owner_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Questions", "Test_Id_testu", "dbo.Tests", "Id_testu");
            AddForeignKey("dbo.PossibleAnswers", "Question_Id_question", "dbo.Questions", "Id_question");
        }
    }
}
