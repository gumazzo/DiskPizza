using DiskPizza.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DiskPizza.DataAccess
{
    public class UsuarioDAO
    {
        public void Inserir(Usuario obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de estados
                string strSQL = @"INSERT INTO TB_USUARIO (ST_NOME, ST_TELEFONE, ST_EMAIL, ST_CPF, ST_SENHA, DT_ADMINISTRADOR, ST_CEP, ST_RUA, ST_NUMEROLOCAL)
                                  VALUES (@ST_NOME, @ST_TELEFONE, @ST_EMAIL, @ST_CPF, @ST_SENHA, @DT_ADMINISTRADOR, @ST_CEP, @ST_RUA, @ST_NUMEROLOCAL);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@ST_NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@ST_TELEFONE", SqlDbType.VarChar).Value = obj.Telefone;
                    cmd.Parameters.Add("@ST_EMAIL", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@ST_CPF", SqlDbType.VarChar).Value = obj.Cpf;
                    cmd.Parameters.Add("@ST_SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@DT_ADMINISTRADOR", SqlDbType.Bit).Value = obj.Administrador;
                    cmd.Parameters.Add("@ST_CEP", SqlDbType.VarChar).Value = obj.Cep;
                    cmd.Parameters.Add("@ST_RUA", SqlDbType.VarChar).Value = obj.Rua;
                    cmd.Parameters.Add("@ST_NUMEROLOCAL", SqlDbType.VarChar).Value = obj.Numero;

                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        if (parameter.Value == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public void Atualizar(Usuario obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de estados
                string strSQL = @"UPDATE TB_USUARIO SET ST_CEP = @ST_CEP, ST_RUA = @ST_RUA, ST_NUMEROLOCAL = @ST_NUMEROLOCAL WHERE ID_USUARIO = @ID_USUARIO;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@ST_CEP", SqlDbType.VarChar).Value = obj.Cep;
                    cmd.Parameters.Add("@ST_RUA", SqlDbType.VarChar).Value = obj.Rua;
                    cmd.Parameters.Add("@ST_NUMEROLOCAL", SqlDbType.VarChar).Value = obj.Numero;
                    cmd.Parameters.Add("@ID_USUARIO", SqlDbType.Int).Value = obj.Id;

                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        if (parameter.Value == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public Usuario BuscarPorId(int usuarioId)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM TB_USUARIO WHERE ID_USUARIO = @ID_USUARIO;";

                //Criando um comando sql que será executado na base d edados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_USUARIO", SqlDbType.Int).Value = usuarioId;
                    cmd.CommandText = strSQL;
                    //Executando instrução sql
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    //Perconrrendo todos os registros encontrados na base de dados e adicionando em uma lista
                    var row = dt.Rows[0];
                    var usuario = new Usuario()
                    {
                        Id = Convert.ToInt32(row["ID_USUARIO"]),
                        Nome = row["ST_NOME"].ToString(),
                        Telefone = row["ST_TELEFONE"].ToString(),
                        Email = row["ST_EMAIL"].ToString(),
                        Cpf = row["ST_CPF"].ToString(),
                        Senha = row["ST_SENHA"].ToString(),
                        Administrador = Convert.ToBoolean(row["DT_ADMINISTRADOR"]),
                        Cep = row["ST_CEP"].ToString(),
                        Rua = row["ST_RUA"].ToString(),
                        Numero = row["ST_NUMEROLOCAL"].ToString()
                    };

                    return usuario;
                }
            }
        }

        public List<Usuario> BuscarTodos()
        {
            var lst = new List<Usuario>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM TB_USUARIO;";

                //Criando um comando sql que será executado na base d edados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;
                    //Executando instrução sql
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    //Perconrrendo todos os registros encontrados na base de dados e adicionando em uma lista
                    foreach (DataRow row in dt.Rows)
                    {
                        var usuario = new Usuario()
                        {
                            Id = Convert.ToInt32(row["ID_USUARIO"]),
                            Nome = row["ST_NOME"].ToString(),
                            Telefone = row["ST_TELEFONE"].ToString(),
                            Email = row["ST_EMAIL"].ToString(),
                            Cpf = row["ST_CPF"].ToString(),
                            Senha = row["ST_SENHA"].ToString(),
                            Administrador = Convert.ToBoolean(row["DT_ADMINISTRADOR"]),
                            Cep = row["ST_CEP"].ToString(),
                            Rua = row["ST_RUA"].ToString(),
                            Numero = row["ST_NUMEROLOCAL"].ToString()
                        };

                        lst.Add(usuario);
                    }
                }
            }
            return lst;
        }

        public Usuario Logar(Usuario obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT TOP 1 * FROM TB_USUARIO WHERE ST_EMAIL = @ST_EMAIL AND ST_SENHA = @ST_SENHA;";

                //Criando um comando sql que será executado na base d edados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ST_EMAIL", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@ST_SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.CommandText = strSQL;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var usuario = new Usuario()
                    {
                        Id = Convert.ToInt32(row["ID_USUARIO"]),
                        Nome = row["ST_NOME"].ToString(),
                        Telefone = row["ST_TELEFONE"].ToString(),
                        Email = row["ST_EMAIL"].ToString(),
                        Cpf = row["ST_CPF"].ToString(),
                        Senha = row["ST_SENHA"].ToString(),
                        Administrador = Convert.ToBoolean(row["DT_ADMINISTRADOR"]),
                        Cep = row["ST_CEP"].ToString(),
                        Rua = row["ST_RUA"].ToString(),
                        Numero = row["ST_NUMEROLOCAL"].ToString()
                    };

                    return usuario;
                }
            }
        }
    }
}
