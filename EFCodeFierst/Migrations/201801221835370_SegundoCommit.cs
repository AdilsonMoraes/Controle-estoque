namespace EFCodeFierst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SegundoCommit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Nome", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Nome");
        }
    }
}
