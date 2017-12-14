namespace Comparison_shopping_engine_backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixItemHistory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DBItemHistories", "Name", "dbo.DBItems");
            DropIndex("dbo.DBItemHistories", new[] { "Name" });
            DropPrimaryKey("dbo.DBItemHistories");
            AlterColumn("dbo.DBItemHistories", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.DBItemHistories", new[] { "Name", "Store", "Price", "Date" });
            CreateIndex("dbo.DBItemHistories", "Name");
            AddForeignKey("dbo.DBItemHistories", "Name", "dbo.DBItems", "Name", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DBItemHistories", "Name", "dbo.DBItems");
            DropIndex("dbo.DBItemHistories", new[] { "Name" });
            DropPrimaryKey("dbo.DBItemHistories");
            AlterColumn("dbo.DBItemHistories", "Name", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.DBItemHistories", new[] { "Store", "Price", "Date" });
            CreateIndex("dbo.DBItemHistories", "Name");
            AddForeignKey("dbo.DBItemHistories", "Name", "dbo.DBItems", "Name");
        }
    }
}
