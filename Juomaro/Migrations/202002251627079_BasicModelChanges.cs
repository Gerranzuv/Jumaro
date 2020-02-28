namespace Juomaro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicModelChanges : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Clients", new[] { "Creator_Id" });
            DropIndex("dbo.Clients", new[] { "Modifier_Id" });
            DropColumn("dbo.Clients", "CreatorId");
            DropColumn("dbo.Clients", "ModifierId");
            RenameColumn(table: "dbo.Clients", name: "Creator_Id", newName: "CreatorId");
            RenameColumn(table: "dbo.Clients", name: "Modifier_Id", newName: "ModifierId");
            AlterColumn("dbo.Clients", "CreatorId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Clients", "ModifierId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Clients", "CreatorId");
            CreateIndex("dbo.Clients", "ModifierId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Clients", new[] { "ModifierId" });
            DropIndex("dbo.Clients", new[] { "CreatorId" });
            AlterColumn("dbo.Clients", "ModifierId", c => c.Int(nullable: false));
            AlterColumn("dbo.Clients", "CreatorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Clients", name: "ModifierId", newName: "Modifier_Id");
            RenameColumn(table: "dbo.Clients", name: "CreatorId", newName: "Creator_Id");
            AddColumn("dbo.Clients", "ModifierId", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "CreatorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "Modifier_Id");
            CreateIndex("dbo.Clients", "Creator_Id");
        }
    }
}
