namespace EFCodeFierst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeiroCommit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SobreInformacoes", "Buildversao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SobreInformacoes", "Buildversao", c => c.String());
        }
    }
}
