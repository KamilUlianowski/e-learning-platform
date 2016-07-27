namespace E_LearningWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        Path = c.String(),
                        NumberOfMovies = c.Int(nullable: false),
                        ShortDescription = c.String(),
                        Author = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Author = c.String(),
                        Body = c.String(),
                        Created = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CorrectAnswer = c.Int(nullable: false),
                        SelectedAnswer = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CourseName = c.String(),
                        CourseId = c.Int(nullable: false),
                        Result = c.String(),
                        DateOfTest = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "TestId", "dbo.Tests");
            DropIndex("dbo.Questions", new[] { "TestId" });
            DropTable("dbo.Tests");
            DropTable("dbo.TestResults");
            DropTable("dbo.Questions");
            DropTable("dbo.Posts");
            DropTable("dbo.Courses");
        }
    }
}
