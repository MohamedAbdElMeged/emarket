namespace emarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductViewModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                        category_name = c.String(),
                        price = c.Double(nullable: false),
                        category_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Categories", "ProductViewModel_id", c => c.Int());
            CreateIndex("dbo.Categories", "ProductViewModel_id");
            AddForeignKey("dbo.Categories", "ProductViewModel_id", "dbo.ProductViewModels", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "ProductViewModel_id", "dbo.ProductViewModels");
            DropIndex("dbo.Categories", new[] { "ProductViewModel_id" });
            DropColumn("dbo.Categories", "ProductViewModel_id");
            DropTable("dbo.ProductViewModels");
        }
    }
}
