namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBudgetAndSalary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Budget", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "Salary", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Salary");
            DropColumn("dbo.Projects", "Budget");
        }
    }
}
