namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectTaskNameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProjectTasks", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProjectTasks", "Name", c => c.String());
        }
    }
}
