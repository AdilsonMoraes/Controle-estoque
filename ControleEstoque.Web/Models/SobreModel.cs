using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models
{
    public class SobreModel
    {
        public int Id { get; set; }
        public string LinguagemDev { get; set; }
        public string BancoDados { get; set; }
        public string Buildversao { get; set; }

        public static List<SobreModel> RecuperarVersao()
        {
            var ret = new List<SobreModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("select * from versao");
                    var reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        var substring = (string)reader["BancoDados"];
                        substring = substring.Substring(0, 46);

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