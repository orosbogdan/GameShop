namespace GameShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        user_id = c.Long(nullable: false),
                        game_id = c.Long(nullable: false),
                        comment = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Game", t => t.game_id)
                .ForeignKey("dbo.Users", t => t.user_id)
                .Index(t => t.user_id)
                .Index(t => t.game_id);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        company_id = c.Long(nullable: false),
                        added_date = c.DateTime(nullable: false, storeType: "date"),
                        release_date = c.DateTime(nullable: false, storeType: "date"),
                        price = c.Decimal(nullable: false, storeType: "money"),
                        is_available = c.Boolean(nullable: false),
                        age_restriction = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Company", t => t.company_id)
                .Index(t => t.company_id);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Discount",
                c => new
                    {
                        game_id = c.Long(nullable: false),
                        percentage_discount = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.game_id)
                .ForeignKey("dbo.Game", t => t.game_id)
                .Index(t => t.game_id);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        user_id = c.Long(nullable: false),
                        game_id = c.Long(nullable: false),
                        rating = c.Short(nullable: false),
                        date = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => new { t.user_id, t.game_id })
                .ForeignKey("dbo.Users", t => t.user_id)
                .ForeignKey("dbo.Game", t => t.game_id)
                .Index(t => t.user_id)
                .Index(t => t.game_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        username = c.String(nullable: false, maxLength: 20, unicode: false),
                        email = c.String(nullable: false, maxLength: 250),
                        password = c.String(nullable: false, maxLength: 20, unicode: false),
                        balance = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.username, unique: true)
                .Index(t => t.email, unique: true);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        user_id = c.Long(nullable: false),
                        game_id = c.Long(nullable: false),
                        date = c.DateTime(nullable: false, storeType: "date"),
                        price = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.user_id)
                .ForeignKey("dbo.Game", t => t.game_id)
                .Index(t => t.user_id)
                .Index(t => t.game_id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.GenreGame",
                c => new
                    {
                        game_id = c.Long(nullable: false),
                        genre_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.game_id, t.genre_id })
                .ForeignKey("dbo.Game", t => t.game_id, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.genre_id, cascadeDelete: true)
                .Index(t => t.game_id)
                .Index(t => t.genre_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "game_id", "dbo.Game");
            DropForeignKey("dbo.Rating", "game_id", "dbo.Game");
            DropForeignKey("dbo.Transactions", "user_id", "dbo.Users");
            DropForeignKey("dbo.Rating", "user_id", "dbo.Users");
            DropForeignKey("dbo.Comments", "user_id", "dbo.Users");
            DropForeignKey("dbo.GenreGame", "genre_id", "dbo.Genre");
            DropForeignKey("dbo.GenreGame", "game_id", "dbo.Game");
            DropForeignKey("dbo.Discount", "game_id", "dbo.Game");
            DropForeignKey("dbo.Game", "company_id", "dbo.Company");
            DropForeignKey("dbo.Comments", "game_id", "dbo.Game");
            DropIndex("dbo.GenreGame", new[] { "genre_id" });
            DropIndex("dbo.GenreGame", new[] { "game_id" });
            DropIndex("dbo.Transactions", new[] { "game_id" });
            DropIndex("dbo.Transactions", new[] { "user_id" });
            DropIndex("dbo.Users", new[] { "email" });
            DropIndex("dbo.Users", new[] { "username" });
            DropIndex("dbo.Rating", new[] { "game_id" });
            DropIndex("dbo.Rating", new[] { "user_id" });
            DropIndex("dbo.Discount", new[] { "game_id" });
            DropIndex("dbo.Game", new[] { "company_id" });
            DropIndex("dbo.Comments", new[] { "game_id" });
            DropIndex("dbo.Comments", new[] { "user_id" });
            DropTable("dbo.GenreGame");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Transactions");
            DropTable("dbo.Users");
            DropTable("dbo.Rating");
            DropTable("dbo.Genre");
            DropTable("dbo.Discount");
            DropTable("dbo.Company");
            DropTable("dbo.Game");
            DropTable("dbo.Comments");
        }
    }
}
