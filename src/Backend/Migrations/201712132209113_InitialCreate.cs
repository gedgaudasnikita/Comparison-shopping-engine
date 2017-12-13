namespace Comparison_shopping_engine_backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DBItemHistories",
                c => new
                    {
                        Store = c.String(nullable: false, maxLength: 128),
                        Price = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Store, t.Price, t.Date })
                .ForeignKey("dbo.DBItems", t => t.Name)
                .Index(t => t.Name);
            
            CreateTable(
                "dbo.DBItems",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Store = c.String(),
                        Price = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DBItemHistories", "Name", "dbo.DBItems");
            DropIndex("dbo.DBItemHistories", new[] { "Name" });
            DropTable("dbo.DBItems");
            DropTable("dbo.DBItemHistories");
        }
    }
}
