namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriarCardapio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cardapios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pratoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Tipo = c.Int(nullable: false),
                        InformacaoExtra = c.String(),
                        InformacaoLegal = c.Int(nullable: false),
                        Preco = c.Int(nullable: false),
                        UrlImagem = c.String(),
                        Cardapio_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cardapios", t => t.Cardapio_Id)
                .Index(t => t.Cardapio_Id);
            
            AddColumn("dbo.Restaurantes", "Cardapio_Id", c => c.Int());
            CreateIndex("dbo.Restaurantes", "Cardapio_Id");
            AddForeignKey("dbo.Restaurantes", "Cardapio_Id", "dbo.Cardapios", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurantes", "Cardapio_Id", "dbo.Cardapios");
            DropForeignKey("dbo.Pratoes", "Cardapio_Id", "dbo.Cardapios");
            DropIndex("dbo.Pratoes", new[] { "Cardapio_Id" });
            DropIndex("dbo.Restaurantes", new[] { "Cardapio_Id" });
            DropColumn("dbo.Restaurantes", "Cardapio_Id");
            DropTable("dbo.Pratoes");
            DropTable("dbo.Cardapios");
        }
    }
}
