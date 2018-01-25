using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models
{
    public class SobreProp
    {
        public int Id { get; set; }
        public string LinguagemDev { get; set; }
        public string BancoDados { get; set; }
        public string Buildversao { get; set; }

    }

    public abstract class RecuperaSobre : SobreProp
    {
        public abstract void RecuperarSobreProp();

        public static List<SobreProp> ListaInformacao()
        {
            var RecuperarSobreProp = new List<SobreProp>();
            return RecuperarSobreProp;
        }
    }

    public class ConsultarPessoa : RecuperaSobre
    {
        public override void RecuperarSobreProp()
        {
            Id = 0;
            LinguagemDev = "";
            BancoDados = "";
            Buildversao = "";
        }

    }

    public class SobreModel : ConsultarPessoa 
    {
        public static List<SobreModel> RecuperarVersao()
        {
            var ret = new List<SobreModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("select * from SobreInformacoes");
                    var reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        var substring = (string)reader["BancoDados"];
                        substring = substring.Substring(0, 31);

                        ret.Add(new SobreModel
                        {
                            Id = (int)reader["id"],
                            LinguagemDev = (string)reader["LinguagemDev"],
                            BancoDados = substring,
                            Buildversao = (string)reader["Buildversao"]
                        });
                    }
                }
            }

            return ret;
        }
    }
}
