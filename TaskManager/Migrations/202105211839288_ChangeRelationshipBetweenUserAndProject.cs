namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ChangeRelationshipBetweenUserAndProject : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserProjectTasks", newName: "ProjectTaskApplicationUsers");
            DropForeignKey("dbo.ApplicationUserProjects", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserProjects", "Project_ID", "dbo.Projects");
            DropIndex("dbo.ApplicationUserProjects", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserProjects", new[] { "Project_ID" });
            DropPrimaryKey("dbo.ProjectTaskApplicationUsers");
            AddColumn("dbo.Projects", "ManagerID", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.ProjectTaskApplicationUsers", new[] { "ProjectTask_ID", "ApplicationUser_Id" });
            CreateIndex("dbo.Projects", "ManagerID");
            AddForeignKey("dbo.Projects", "ManagerID", "dbo.AspNetUsers", "Id");
            DropTable("dbo.ApplicationUserProjects");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserProjects",
                c => new
                {
                    ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    Project_ID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Project_ID });

            DropForeignKey("dbo.Projects", "ManagerID", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "ManagerID" });
            DropPrimaryKey("dbo.ProjectTaskApplicationUsers");
            DropColumn("dbo.Projects", "ManagerID");
            AddPrimaryKey("dbo.ProjectTaskApplicationUsers", new[] { "ApplicationUser_Id", "ProjectTask_ID" });
            CreateIndex("dbo.ApplicationUserProjects", "Project_ID");
            CreateIndex("dbo.ApplicationUserProjects", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserProjects", "Project_ID", "dbo.Projects", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserProjects", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.ProjectTaskApplicationUsers", newName: "ApplicationUserProjectTasks");
        }
    }
}
