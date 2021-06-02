namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNotificalModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Notifications", "TaskID", "dbo.ProjectTasks");
            DropIndex("dbo.Notifications", new[] { "ProjectID" });
            DropIndex("dbo.Notifications", new[] { "TaskID" });
            AddColumn("dbo.Notifications", "ItemID", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "IsProject", c => c.Boolean(nullable: false));
            DropColumn("dbo.Notifications", "ProjectID");
            DropColumn("dbo.Notifications", "TaskID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "TaskID", c => c.Int());
            AddColumn("dbo.Notifications", "ProjectID", c => c.Int());
            DropColumn("dbo.Notifications", "IsProject");
            DropColumn("dbo.Notifications", "ItemID");
            CreateIndex("dbo.Notifications", "TaskID");
            CreateIndex("dbo.Notifications", "ProjectID");
            AddForeignKey("dbo.Notifications", "TaskID", "dbo.ProjectTasks", "ID");
            AddForeignKey("dbo.Notifications", "ProjectID", "dbo.Projects", "ID");
        }
    }
}
