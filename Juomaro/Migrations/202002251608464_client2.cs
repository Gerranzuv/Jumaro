namespace Juomaro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class client2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Country = c.String(),
                        MerchantId = c.Long(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        AttachmentId = c.Long(nullable: false),
                        Creator_Id = c.String(maxLength: 128),
                        Merchant_Id = c.String(maxLength: 128),
                        Modifier_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Merchant_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Modifier_Id)
                .Index(t => t.Creator_Id)
                .Index(t => t.Merchant_Id)
                .Index(t => t.Modifier_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "Modifier_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Clients", "Merchant_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Clients", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Clients", new[] { "Modifier_Id" });
            DropIndex("dbo.Clients", new[] { "Merchant_Id" });
            DropIndex("dbo.Clients", new[] { "Creator_Id" });
            DropTable("dbo.Clients");
        }
    }
}
