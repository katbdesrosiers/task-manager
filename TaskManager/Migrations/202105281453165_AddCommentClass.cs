namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DeveloperID = c.String(nullable: false, maxLength: 128),
                        TaskID = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        Urgent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeveloperID, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTasks", t => t.TaskID, cascadeDelete: true)
                .Index(t => t.DeveloperID)
                .Index(t => t.TaskID);
            
            AddColumn("dbo.ProjectTasks", "DeadlineNotificationSent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "TaskID", "dbo.ProjectTasks");
            DropForeignKey("dbo.Comments", "DeveloperID", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "TaskID" });
            DropIndex("dbo.Comments", new[] { "DeveloperID" });
            DropColumn("dbo.ProjectTasks", "DeadlineNotificationSent");
            DropTable("dbo.Comments");
        }
    }
}
