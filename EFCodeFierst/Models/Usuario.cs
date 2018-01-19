using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeFierst.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string  Login { get; set; }
        public string Senha { get; set; }

    }
}