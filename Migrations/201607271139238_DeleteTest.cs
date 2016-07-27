namespace E_LearningWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "TestId", "dbo.Tests");
            DropIndex("dbo.Questions", new[] { "TestId" });
            DropTable("dbo.Tests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Questions", "TestId");
            AddForeignKey("dbo.Questions", "TestId", "dbo.Tests", "Id", cascadeDelete: true);
        }
    }
}
