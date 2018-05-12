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
    public class ProdutoDAO
    {
        public void Inserir(Produto obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=TAKEPIZZA;
                                    Data Source=localhost;
                                    Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de estados
                string strSQL = @"INSERT INTO TB_PRODUTO ( ST_NOME, ST_TIPO, DT_PRECO)
                                    VALUES ( @ST_NOME, @ST_TIPO, @DT_PRECO);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@ST_NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@ST_TIPO", SqlDbType.VarChar).Value = obj.Tipo;
                    cmd.Parameters.Add("@DT_PRECO", SqlDbType.Decimal).Value = obj.Preco;
                    

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public List<Produto> BuscarTodos()
        {
            var lst = new List<Produto>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=TAKEPIZZA;
                                    Data Source=localhost;
                                    Integrated Security=SSPI"))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM TB_PRODUTO;";

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
                        var produto = new Produto()
                        {
                            Id = Convert.ToInt32(row["ID_PRODUTO"]),
                            Nome = row["ST_NOME"].ToString(),
                            Tipo = row["ST_TIPO"].ToString(),
                            Preco = Convert.ToDecimal(row["DT_PRECO"])
                        };

                        lst.Add(produto);
                    }
                }
            }
            return lst;
        }
    }
}
