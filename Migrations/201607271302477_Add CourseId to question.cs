namespace E_LearningWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseIdtoquestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "CourseId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "CourseId");
        }
    }
}
