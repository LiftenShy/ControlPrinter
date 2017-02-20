using System.Data.Entity.Migrations;

namespace CP.Data.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameImage = c.String(),
                        DateLoad = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            this.CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameRole = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            this.CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            this.DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            this.DropIndex("dbo.Users", new[] { "RoleId" });
            this.DropTable("dbo.Users");
            this.DropTable("dbo.Roles");
            this.DropTable("dbo.Images");
        }
    }
}
