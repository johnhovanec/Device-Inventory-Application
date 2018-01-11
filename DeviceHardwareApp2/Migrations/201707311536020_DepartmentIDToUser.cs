namespace DeviceHardwareApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentIDToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "DepartmentID", c => c.Int());
            AlterColumn("dbo.User", "UserName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.User", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.User", "FirstMidName", c => c.String(maxLength: 50));
            CreateIndex("dbo.User", "DepartmentID");
            AddForeignKey("dbo.User", "DepartmentID", "dbo.Department", "DepartmentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "DepartmentID", "dbo.Department");
            DropIndex("dbo.User", new[] { "DepartmentID" });
            AlterColumn("dbo.User", "FirstMidName", c => c.String());
            AlterColumn("dbo.User", "LastName", c => c.String());
            AlterColumn("dbo.User", "UserName", c => c.String());
            DropColumn("dbo.User", "DepartmentID");
        }
    }
}
