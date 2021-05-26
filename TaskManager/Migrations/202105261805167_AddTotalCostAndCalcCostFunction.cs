namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTotalCostAndCalcCostFunction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "TotalCost", c => c.Double(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "TotalCost");
        }
    }
}
