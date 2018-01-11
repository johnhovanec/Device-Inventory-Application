namespace DeviceHardwareApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        DeviceID = c.Int(nullable: false, identity: true),
                        DepartmentID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        InventoryNumber = c.Int(nullable: false),
                        Name = c.String(),
                        Model = c.String(),
                        Manufacturer = c.String(),
                        IP = c.String(),
                        MAC = c.String(),
                        SerialNumber = c.String(),
                        ServiceTag = c.String(),
                        WallJack = c.String(),
                        SwitchPortNumber = c.String(),
                        Active = c.Boolean(nullable: false),
                        OperatingSystem = c.String(),
                        Notes = c.String(),
                        CriticalRating = c.Int(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DeviceID)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.DepartmentID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                        EmployeeID = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        Position = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Device", "UserID", "dbo.User");
            DropForeignKey("dbo.Device", "DepartmentID", "dbo.Department");
            DropIndex("dbo.Device", new[] { "UserID" });
            DropIndex("dbo.Device", new[] { "DepartmentID" });
            DropTable("dbo.User");
            DropTable("dbo.Device");
            DropTable("dbo.Department");
        }
    }
}
