namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDeadlineNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectTasks", "DeadlineNotificationSent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectTasks", "DeadlineNotificationSent");
        }
    }
}
