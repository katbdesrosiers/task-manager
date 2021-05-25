namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateCreatedAndDeadlineToProjectAndTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "Deadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "DateCompleted", c => c.DateTime());
            AddColumn("dbo.ProjectTasks", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProjectTasks", "Deadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProjectTasks", "DateCompleted", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectTasks", "DateCompleted");
            DropColumn("dbo.ProjectTasks", "Deadline");
            DropColumn("dbo.ProjectTasks", "DateCreated");
            DropColumn("dbo.Projects", "DateCompleted");
            DropColumn("dbo.Projects", "Deadline");
            DropColumn("dbo.Projects", "DateCreated");
        }
    }
}
