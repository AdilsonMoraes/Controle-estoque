using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ControleEstoque.Web.Models
{
    public class UsuaioProp
    {
        #region Propertys

        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Informe o senha")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }

        #endregion
    }

    public class UsuarioModel : UsuaioProp
    {
        public static UsuarioModel ValidarUsuario(string login, string senha)
        {
            UsuarioModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select Usuarioid, login, nome,senha from usuario where login=@login and senha=@senha";

                    comando.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
                    comando.Parameters.Add("@senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(senha);

                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new UsuarioModel
                        {
                            Id = (int)reader["Usuarioid"],
                            Login = (string)reader["login"],
                            Nome = (string)reader["nome"],
                            Senha = (string)reader["senha"],

                        };
                    }
                }
            }

            return ret;
        }

        public static List<UsuarioModel> RecuperarLista()
        {
            var ret = new List<UsuarioModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select Usuarioid, login, nome,senha from Usuario order by nome";
                    var reader = comando.ExecuteReader();

                    while (reader.Read())
                    {

                        ret.Add(new UsuarioModel
                        {
                            Id = (int)reader["Usuarioid"],
                            Login = (string)reader["login"],
                            Nome = (string)reader["nome"],
                            Senha = (string)reader["senha"],

                        });
                    }
                }
            }

            return ret;
        }

        public static UsuarioModel RecuperarPeloId(int id)
        {
            UsuarioModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from usuario where (UsuarioId = @id)";
                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    var reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        ret = new UsuarioModel
                        {
                            Id = (int)reader["Usuarioid"],
                            Login = (string)reader["login"],
                            Nome = (string)reader["nome"],
                            Senha = (string)reader["senha"],

                        };
                    }
                }
            }

            return ret;
        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;

            if (RecuperarPeloId(id) != null)
            {

                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                    conexao.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "Delete from usuario where (Usuarioid = @id)";

                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        ret = (comando.ExecuteNonQuery() > 0); //ExecuteNonQuery quantidade de registro afetados pelo comando
                    }
                }
            }
            return ret;
        }

        public int Salvar()
        {
            var ret = 0;

            var model = RecuperarPeloId(this.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    if (model == null)
                    {
                        comando.CommandText = "insert into usuario (nome, login, senha) values (@nome, @login, @senha); select convert(int, scope_identity())";

                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@login", SqlDbType.VarChar).Value = this.Login;
                        comando.Parameters.Add("@senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(this.Senha);

                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText =
                            "update usuario set nome=@nome, login=@login" +
                            (!string.IsNullOrEmpty(this.Senha) ? ", senha=@senha" : "") +
                            " where Usuarioid = @id";

                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@login", SqlDbType.VarChar).Value = this.Login;

                        if (!string.IsNullOrEmpty(this.Senha))
                        {
                            comando.Parameters.Add("@senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(this.Senha);
                        }

                        comando.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;

                        if (comando.ExecuteNonQuery() > 0)
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