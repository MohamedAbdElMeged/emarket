namespace emarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Number_Of_Products", c => c.Int(nullable: false));
            DropColumn("dbo.Categories", "NumberOfProducts");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "NumberOfProducts", c => c.Int(nullable: false));
            DropColumn("dbo.Categories", "Number_Of_Products");
        }
    }
}
