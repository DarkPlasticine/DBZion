namespace DBZion.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        ReceiptId = c.Int(nullable: false),
                        ReceiptType = c.String(nullable: false, maxLength: 15),
                        ServiceType = c.String(nullable: false, maxLength: 20),
                        Price = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        Note = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsReady = c.Boolean(nullable: false),
                        Call = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                        Worker = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Surname = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        MiddleName = c.String(nullable: false, maxLength: 20),
                        PhoneNumber = c.String(maxLength: 20),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserID", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
        }
    }
}
