namespace emarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfbdf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Product_Id = c.Int(nullable: false, identity: true),
                        Added_At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Carts");
        }
    }
}
