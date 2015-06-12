namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriaCardapio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cardapios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Preco = c.Decimal(precision: 18, scale: 2),
                        DateTime = c.DateTime(),
                        Categoria = c.Int(nullable: false),
                        InformacaoLegal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cardapios");
        }
    }
}
