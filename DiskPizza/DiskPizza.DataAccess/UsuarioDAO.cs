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
                string strSQL = @"INSERT INTO TB_USUARIO (ST_NOME, ST_TELEFONE, ST_EMAIL, ST_CPF, ST_SENHA, DT_ADMINISTRADOR)
                                  VALUES (@ST_NOME, @ST_TELEFONE, @ST_EMAIL, @ST_CPF, @ST_SENHA, @DT_ADMINISTRADOR);";

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

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
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
                            Administrador = Convert.ToBoolean(row["DT_ADMINISTRADOR"])
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
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ST_EMAIL", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@ST_SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.CommandText = strSQL;
                    //Executando instrução sql
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    //Percorrendo todos os registros encontrados na base de dados e adicionando em uma lista
                    var usuario = new Usuario()
                    {
                        Id = Convert.ToInt32(row["ID_USUARIO"]),
                        Nome = row["ST_NOME"].ToString(),
                        Telefone = row["ST_TELEFONE"].ToString(),
                        Email = row["ST_EMAIL"].ToString(),
                        Cpf = row["ST_CPF"].ToString(),
                        Senha = row["ST_SENHA"].ToString(),
                        Administrador = Convert.ToBoolean(row["DT_ADMINISTRADOR"])
                    };

                    return usuario;
                }
            }
        }
    }
}
