namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorProjectAndTaskModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "ManagerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectTasks", "DeveloperID", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "ManagerID" });
            DropIndex("dbo.ProjectTasks", new[] { "DeveloperID" });
            AlterColumn("dbo.Projects", "ManagerID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ProjectTasks", "DeveloperID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Projects", "ManagerID");
            CreateIndex("dbo.ProjectTasks", "DeveloperID");
            AddForeignKey("dbo.Projects", "ManagerID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectTasks", "DeveloperID", "dbo.AspNetUsers", "Id", cascadeDelete: false );
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTasks", "DeveloperID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "ManagerID", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectTasks", new[] { "DeveloperID" });
            DropIndex("dbo.Projects", new[] { "ManagerID" });
            AlterColumn("dbo.ProjectTasks", "DeveloperID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Projects", "ManagerID", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProjectTasks", "DeveloperID");
            CreateIndex("dbo.Projects", "ManagerID");
            AddForeignKey("dbo.ProjectTasks", "DeveloperID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Projects", "ManagerID", "dbo.AspNetUsers", "Id");
        }
    }
}
