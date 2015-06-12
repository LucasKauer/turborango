namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qualquerCoisa : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cardapios", "InformacaoLegal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cardapios", "InformacaoLegal", c => c.Int(nullable: false));
        }
    }
}
