namespace Scheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        EventID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Event", t => t.EventID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.PersonID)
                .Index(t => t.EventID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Room = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Need",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NeedType = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                        EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Event", t => t.EventID, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.XPersonRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        PersonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignment", "RoleID", "dbo.Role");
            DropForeignKey("dbo.XPersonRole", "RoleID", "dbo.Role");
            DropForeignKey("dbo.XPersonRole", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Assignment", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Need", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Need", "EventID", "dbo.Event");
            DropForeignKey("dbo.Assignment", "EventID", "dbo.Event");
            DropIndex("dbo.XPersonRole", new[] { "PersonID" });
            DropIndex("dbo.XPersonRole", new[] { "RoleID" });
            DropIndex("dbo.Need", new[] { "EventID" });
            DropIndex("dbo.Need", new[] { "RoleID" });
            DropIndex("dbo.Assignment", new[] { "RoleID" });
            DropIndex("dbo.Assignment", new[] { "EventID" });
            DropIndex("dbo.Assignment", new[] { "PersonID" });
            DropTable("dbo.Person");
            DropTable("dbo.XPersonRole");
            DropTable("dbo.Role");
            DropTable("dbo.Need");
            DropTable("dbo.Event");
            DropTable("dbo.Assignment");
        }
    }
}
