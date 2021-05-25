namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TasksHaveSingleDeveloper : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectTaskApplicationUsers", "ProjectTask_ID", "dbo.ProjectTasks");
            DropForeignKey("dbo.ProjectTaskApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectTaskApplicationUsers", new[] { "ProjectTask_ID" });
            DropIndex("dbo.ProjectTaskApplicationUsers", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.ProjectTasks", "DeveloperID", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProjectTasks", "DeveloperID");
            AddForeignKey("dbo.ProjectTasks", "DeveloperID", "dbo.AspNetUsers", "Id");
            DropTable("dbo.ProjectTaskApplicationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProjectTaskApplicationUsers",
                c => new
                    {
                        ProjectTask_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ProjectTask_ID, t.ApplicationUser_Id });
            
            DropForeignKey("dbo.ProjectTasks", "DeveloperID", "dbo.AspNetUsers");
            DropIndex("dbo.ProjectTasks", new[] { "DeveloperID" });
            DropColumn("dbo.ProjectTasks", "DeveloperID");
            CreateIndex("dbo.ProjectTaskApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.ProjectTaskApplicationUsers", "ProjectTask_ID");
            AddForeignKey("dbo.ProjectTaskApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectTaskApplicationUsers", "ProjectTask_ID", "dbo.ProjectTasks", "ID", cascadeDelete: true);
        }
    }
}
