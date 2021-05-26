namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeManagerIDNotRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "ManagerID", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "ManagerID" });
            AlterColumn("dbo.Projects", "ManagerID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Projects", "ManagerID");
            AddForeignKey("dbo.Projects", "ManagerID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "ManagerID", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "ManagerID" });
            AlterColumn("dbo.Projects", "ManagerID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Projects", "ManagerID");
            AddForeignKey("dbo.Projects", "ManagerID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
