using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeFierst.Models
{
    public class Grupo_Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Ativo { get; set; }

    }
}