namespace FA.JustBlog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        UrlSlug = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 500),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 255),
                        ShortDescription = c.String(nullable: false, maxLength: 1000),
                        ImageUrl = c.String(maxLength: 255),
                        PostContent = c.String(nullable: false),
                        UrlSlug = c.String(nullable: false, maxLength: 255),
                        Published = c.Boolean(nullable: false),
                        PublishedDate = c.DateTime(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        UrlSlug = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 500),
                        Count = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.PostTags",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("common.PostTags", "TagId", "dbo.Tags");
            DropForeignKey("common.PostTags", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            DropIndex("common.PostTags", new[] { "TagId" });
            DropIndex("common.PostTags", new[] { "PostId" });
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            DropTable("common.PostTags");
            DropTable("dbo.Tags");
            DropTable("dbo.Posts");
            DropTable("dbo.Categories");
        }
    }
}
