namespace Juomaro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class client3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Clients", new[] { "Merchant_Id" });
            DropColumn("dbo.Clients", "MerchantId");
            RenameColumn(table: "dbo.Clients", name: "Merchant_Id", newName: "MerchantId");
            AlterColumn("dbo.Clients", "MerchantId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Clients", "MerchantId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Clients", new[] { "MerchantId" });
            AlterColumn("dbo.Clients", "MerchantId", c => c.Long(nullable: false));
            RenameColumn(table: "dbo.Clients", name: "MerchantId", newName: "Merchant_Id");
            AddColumn("dbo.Clients", "MerchantId", c => c.Long(nullable: false));
            CreateIndex("dbo.Clients", "Merchant_Id");
        }
    }
}
