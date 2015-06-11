namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterandoPrecoParaDecimalEColocandoUmDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pratoes", "DateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pratoes", "Preco", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pratoes", "Preco", c => c.Int(nullable: false));
            DropColumn("dbo.Pratoes", "DateTime");
        }
    }
}
