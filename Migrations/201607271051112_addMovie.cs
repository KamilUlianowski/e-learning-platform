namespace E_LearningWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMovie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SumOfVotes = c.Double(nullable: false),
                        NumberOfVotes = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Title = c.String(),
                        VideoUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Movies");
        }
    }
}
