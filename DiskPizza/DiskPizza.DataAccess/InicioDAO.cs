using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiskPizza.Models;
using System.Data.SqlClient;
using System.Data;

namespace DiskPizza.DataAccess
{
    class InicioDAO
    {
        public void Inserir(Usuario obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=TAKEPIZZA;
                                    Data Source=localhost;
                                    Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de estados
                string strSQL = @"INSERT INTO TB_USUARIO(ST_NOME, ST_TELEFONE, ST_EMAIL,ST_CPF,ST_SENHA, ST_CEP, ST_RUA, ST_NUMEROLOCAL, DT_ADMINISTRADOR)
                                    VALUES (@ST_NOME, @ST_TELEFONE,@ST_EMAIL,@ST_CPF,@ST_SENHA,@ST_CEP,@ST_RUA,@STNUMEROLOCAL,@DT_ADMINISTRADOR);";

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
                    cmd.Parameters.Add("@ST_CEP", SqlDbType.VarChar).Value = obj.Cep;
                    cmd.Parameters.Add("@ST_RUA", SqlDbType.VarChar).Value = obj.Rua;
                    cmd.Parameters.Add("@ST_NUMEROLOCAL", SqlDbType.VarChar).Value = obj.NumeroL;
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
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=TAKEPIZZA;
                                    Data Source=localhost;
                                    Integrated Security=SSPI"))
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
                            Cep = row["ST_CEP"].ToString(),
                            Rua = row["ST_RUA"].ToString(),
                            NumeroL = row["ST_NUMEROLOCAL"].ToString(),
                            Administrador = Convert.ToBoolean(row["DT_ADMINISTRADOR"])
                        };

                        lst.Add(usuario);
                    }
                }
            }
            return lst;
        }
    }
}
