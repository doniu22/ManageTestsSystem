namespace TestsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id_answer = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Question_Id_question = c.Int(nullable: false),
                        Result_Id_result = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_answer)
                .ForeignKey("dbo.Questions", t => t.Question_Id_question)
                .ForeignKey("dbo.Results", t => t.Result_Id_result, cascadeDelete: true)
                .Index(t => t.Question_Id_question)
                .Index(t => t.Result_Id_result);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id_question = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Test_Id_testu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_question)
                .ForeignKey("dbo.Tests", t => t.Test_Id_testu, cascadeDelete: true)
                .Index(t => t.Test_Id_testu);
            
            CreateTable(
                "dbo.PossibleAnswers",
                c => new
                    {
                        Id_posans = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        isCorrect = c.String(),
                        Question_Id_question = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_posans)
                .ForeignKey("dbo.Questions", t => t.Question_Id_question, cascadeDelete: true)
                .Index(t => t.Question_Id_question);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id_testu = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Subject = c.String(),
                        Created_at = c.String(),
                        Created_by = c.String(),
                        Status = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id_testu)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        Id_result = c.Int(nullable: false, identity: true),
                        Created_At = c.String(),
                        Created_By = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                        Test_Id_testu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_result)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Tests", t => t.Test_Id_testu)
                .Index(t => t.Owner_Id)
                .Index(t => t.Test_Id_testu);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Answers", "Result_Id_result", "dbo.Results");
            DropForeignKey("dbo.Answers", "Question_Id_question", "dbo.Questions");
            DropForeignKey("dbo.Questions", "Test_Id_testu", "dbo.Tests");
            DropForeignKey("dbo.Tests", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Results", "Test_Id_testu", "dbo.Tests");
            DropForeignKey("dbo.Results", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PossibleAnswers", "Question_Id_question", "dbo.Questions");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Results", new[] { "Test_Id_testu" });
            DropIndex("dbo.Results", new[] { "Owner_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Tests", new[] { "Owner_Id" });
            DropIndex("dbo.PossibleAnswers", new[] { "Question_Id_question" });
            DropIndex("dbo.Questions", new[] { "Test_Id_testu" });
            DropIndex("dbo.Answers", new[] { "Result_Id_result" });
            DropIndex("dbo.Answers", new[] { "Question_Id_question" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Results");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tests");
            DropTable("dbo.PossibleAnswers");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
