using EFCodeFierst.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EFCodeFierst.DAL
{
    public class EFContext : DbContext
    {
        public EFContext() : base("Controle_Estoque") { }  //Construtor que usa a strng de conecxão

        //Romove a convenção para nao ficar pluralizando as tabelas na criação
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Grupo_Produto> Grupo_Produtos { get; set; }
        public DbSet<SobreInformacoes> SobreInformacoes { get; set; }
    }
}