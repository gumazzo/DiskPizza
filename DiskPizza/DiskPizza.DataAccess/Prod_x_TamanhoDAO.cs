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
    public class Prod_x_TamanhoDAO
    {
        public void Inserir(Produto_x_Tamanho obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=TAKEPIZZA;
                                    Data Source=localhost;
                                    Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de estados
                string strSQL = @"INSERT INTO TB_PRODUTO_X_TAMANHO(DT_PRECO_TOTAL, ID_PRODUTO, ID_TAMANHO)
                                    VALUES (@DT_PRECO_TOTAL, @ID_PRODUTO, @ID_TAMANHO);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@DT_PRECO_TOTAL", SqlDbType.Decimal).Value = obj.Preco_Total;
                    cmd.Parameters.Add("@ID_PRODUTO", SqlDbType.Int).Value = obj.Produto.Id;
                    cmd.Parameters.Add("@ID_TAMANHO", SqlDbType.Int).Value = obj.Tamanho.Id;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public List<Produto_x_Tamanho> BuscarTodos()
        {
            var lst = new List<Produto_x_Tamanho>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=TAKEPIZZA;
                                    Data Source=localhost;
                                    Integrated Security=SSPI"))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT 
                                    pt.*, 
                                    p.ST_NOME AS NOME_PRODUTO,  
                                    p.ST_TIPO AS TIPO_PRODUTO, 
                                    p.DT_PRECO AS PRECO_PRODUTO,
                                    t.ST_NOME AS TAMANHO_NOME
                                FROM TB_PRODUTO_X_TAMANHO pt
                                INNER JOIN TB_PRODUTO p ON (p.ID_PRODUTO = pt.ID_PRODUTO)
                                INNER JOIN TB_TAMANHO t ON (t.ID_TAMANHO = pt.ID_TAMANHO);";

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
                        var produto_x_tamanho = new Produto_x_Tamanho()
                        {
                            Id = Convert.ToInt32(row["ID_PRODXTAMANHO"]),
                            Preco_Total = Convert.ToDecimal(row["DT_PRECO_TOTAL"]),
                            Produto = new Produto()
                            {
                                Id = Convert.ToInt32(row["ID_PRODUTO"]),
                                Nome = row["NOME_PRODUTO"].ToString(),
                                Tipo = row["TIPO_PRODUTO"].ToString(),
                                Preco = Convert.ToDecimal(row["PRECO_PRODUTO"])
                            },
                            Tamanho = new Tamanho()
                            {
                                Id = Convert.ToInt32(row["ID_TAMANHO"]),
                                Nome = row["TAMANHO_NOME"].ToString()
                            }
                        };

                        lst.Add(produto_x_tamanho);
                    }
                }
            }
            return lst;
        }
    }
}
