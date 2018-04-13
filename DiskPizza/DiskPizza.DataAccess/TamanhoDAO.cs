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
    public class TamanhoDAO
    {
        public void Inserir(Tamanho obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=TAKEPIZZA;
                                    Data Source=localhost;
                                    Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de estados
                string strSQL = @"INSERT INTO TB_TAMANHO(ST_NOME)
                                    VALUES (@ST_NOME);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@ST_NOME", SqlDbType.VarChar).Value = obj.Nome;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public List<Tamanho> BuscarTodos()
        {
            var lst = new List<Tamanho>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=TAKEPIZZA;
                                    Data Source=localhost;
                                    Integrated Security=SSPI"))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM TB_TAMANHO;";

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
                        var tamanho = new Tamanho()
                        {
                            Id = Convert.ToInt32(row["ID_TAMANHO"]),
                            Nome = row["ST_NOME"].ToString()
                        };

                        lst.Add(tamanho);
                    }
                }
            }
            return lst;
        }
    }
}
