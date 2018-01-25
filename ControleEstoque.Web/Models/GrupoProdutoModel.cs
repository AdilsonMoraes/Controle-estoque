using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ControleEstoque.Web.Models
{
    public class GrupoProdutoProp
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome!")]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }

    public class GrupoProdutoModel : GrupoProdutoProp
    {
        public static List<GrupoProdutoModel> RecuperarLista(int pagina, int tampagina)
        {
            var ret = new List<GrupoProdutoModel>();
            bool atv = false;

            using (var conexao = new SqlConnection())
            {
                var pos = (pagina - 1) * tampagina;
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format(
                        "select * from grupo_produto order by nome offset {0} rows fetch next {1} rows only", 
                        pos>0 ? pos - 1 : 0, tampagina);

                    var reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        if((int)reader["Ativo"] == 1)
                        {
                            atv = true;
                        }

                        ret.Add(new GrupoProdutoModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["Nome"],
                            Ativo = atv
                        });
                    }
                }
            }

            return ret;
        }

        public static GrupoProdutoModel RecuperarPerloId(int id)
        {
            GrupoProdutoModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString =  ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from grupo_produto where (id = @id)";
                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    var reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        bool atv = false;

                        if ((int)reader["Ativo"] == 1)
                        {
                            atv = true;
                        }

                        ret = new GrupoProdutoModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["Nome"],
                            Ativo = atv

                        };
                    }
                }
            }

            return ret;
        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;

            if (RecuperarPerloId(id) != null)
            {

                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                    conexao.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "Delete from grupo_produto where (id = @id)";

                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        ret = (comando.ExecuteNonQuery() > 0); //ExecuteNonQuery quantidade de registro afetados pelo comando
                    }
                }
            }
            return ret;
        }

        public int Salvar()
        {
            // INSERE x ALTERA GRUPO DE PRODUTOS.
            var ret = 0;
            var model = RecuperarPerloId(this.Id);  //Busca o cara pelo ID.

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString =  ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    if (model == null) // se existir
                    {// INSERE
                        comando.CommandText = "INSERT INTO grupo_produto (NOME, ATIVO) VALUES (@nome, @ativo); select convert(int, scope_identity());";

                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (this.Ativo ? 1 : 0);

                        ret = (int)comando.ExecuteScalar(); //ExecuteScalar retorna um object, convertido para inteiro
                    }
                    else
                    {// ALTERA
                        comando.CommandText = "UPDATE grupo_produto SET NOME = @nome, ATIVO = @ativo WHERE Id = @id;";

                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (this.Ativo ? 1 : 0);
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;

                        if (comando.ExecuteNonQuery() > 0) //ExecuteNonQuery e retorna um inteiro, qtde de registros
                        {
                            ret = this.Id;
                        }
                    }
                }
            }
            return ret;
        }

    }
}