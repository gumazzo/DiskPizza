using DiskPizza.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DiskPizza.DataAccess
{
    public class Item_PedidoDAO
    {
        public void Inserir(Item_Pedido obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de estados
                string strSQL = @"INSERT INTO TB_ITEM_PEDIDO(ID_PEDIDO, ID_PRODXTAMANHO, PRECO_ITEM)
                                    VALUES (@ID_PEDIDO, @ID_PRODXTAMANHO, @PRECO_ITEM);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@ID_PEDIDO", SqlDbType.Decimal).Value = obj.Pedido.Id;
                    cmd.Parameters.Add("@ID_PRODXTAMANHO", SqlDbType.Int).Value = obj.Produto_x_Tamanho.Id;
                    cmd.Parameters.Add("@PRECO_ITEM", SqlDbType.Decimal).Value = obj.Preco_item;

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

        public List<Item_Pedido> BuscarTodos()
        {
            var lst = new List<Item_Pedido>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT 
                                    ip.*,
                                    p.DT_DATA AS DATA_PEDIDO,
                                    p.QNT_SABORES AS QUANTIDADE_SABORES,
                                    p.ST_STATUS AS STATUS_PEDIDO,
                                    p.ST_TAMANHO AS TAMANHO_PEDIDO,
                                    pt.DT_PRECO_TOTAL AS PRECO_TOTAL,
                                    pt.ST_CEP AS CEP_PEDIDO,
                                    pt.ST_RUA AS RUA_PEDIDO,
                                    pt.ST_NUMEROLOCAL AS NUMEROLOCAL_PEDIDO
                                FROM TB_ITEM_PEDIDO ip
                                INNER JOIN TB_PEDIDO p ON (p.ID_PEDIDO = ip.ID_PEDIDO)
                                INNER JOIN TB_PRODUTO_X_TAMANHO pt ON (pt.ID_TAMANHO = ip.ID_TAMANHO);";

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
                        var item_pedido = new Item_Pedido()
                        {
                            Id = Convert.ToInt32(row["ID_ITEM"]),
                            Preco_item = Convert.ToDecimal(row["PRECO_ITEM"]),
                            Pedido = new Pedido()
                            {
                                Id = Convert.ToInt32(row["ID_PEDIDO"]),
                                Data = Convert.ToDateTime(row["DATA_PEDIDO"]),
                                QtdSabores = Convert.ToInt32(row["QUANTIDADE_SABORES"]),
                                Status = row["STATUS_PEDIDO"].ToString(),
                                Tamanho = row["TAMANHO_PEDIDO"].ToString(),
                                Cep = row["ST_CEP"].ToString(),
                                Rua = row["ST_RUA"].ToString(),
                                NumeroL = row["ST_NUMEROLOCAL"].ToString()
                            },
                            Produto_x_Tamanho = new Produto_x_Tamanho()
                            {
                                Id = Convert.ToInt32(row["ID_PRODXTAMANHO"]),
                                Preco_Total = Convert.ToDecimal(row["PRECO_TOTAL"]),
                            }
                        };

                        lst.Add(item_pedido);
                    }
                }
            }
            return lst;
        }
    }
}
