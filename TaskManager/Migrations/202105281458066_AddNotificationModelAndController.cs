namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationModelAndController : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(),
                        TaskID = c.Int(),
                        ApplicationUserID = c.String(maxLength: 128),
                        Read = c.Boolean(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.NotificationID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID, cascadeDelete: false)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: false)
                .ForeignKey("dbo.ProjectTasks", t => t.TaskID, cascadeDelete: false)
                .Index(t => t.ProjectID)
                .Index(t => t.TaskID)
                .Index(t => t.ApplicationUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "TaskID", "dbo.ProjectTasks");
            DropForeignKey("dbo.Notifications", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Notifications", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "ApplicationUserID" });
            DropIndex("dbo.Notifications", new[] { "TaskID" });
            DropIndex("dbo.Notifications", new[] { "ProjectID" });
            DropTable("dbo.Notifications");
        }
    }
}