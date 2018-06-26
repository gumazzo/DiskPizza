using DiskPizza.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DiskPizza.DataAccess
{
    public class PedidoDAO
    {
        public void Inserir(Pedido obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de estados
                string strSQL = @"INSERT INTO TB_PEDIDO(DT_DATA, ST_TAMANHO, QNT_SABORES, ST_STATUS, ID_USUARIO)
                                  VALUES (@DT_DATA, @ST_TAMANHO, @QNT_SABORES, @ST_STATUS, @ID_USUARIO);
                                  SELECT SCOPE_IDENTITY();";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@DT_DATA", SqlDbType.DateTime).Value = obj.Data;
                    cmd.Parameters.Add("@ST_TAMANHO", SqlDbType.VarChar).Value = obj.Tamanho;
                    cmd.Parameters.Add("@QNT_SABORES", SqlDbType.Int).Value = obj.QtdSabores;
                    cmd.Parameters.Add("@ST_STATUS", SqlDbType.VarChar).Value = obj.Status;
                    cmd.Parameters.Add("@ID_USUARIO", SqlDbType.Int).Value = obj.Usuario.Id;

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
                    obj.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public Pedido BuscarPorUsuario(int usuario)
        {
            var lst = new List<Pedido>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT TOP 1 * FROM TB_PEDIDO WHERE ID_USUARIO = @ID_USUARIO AND ST_STATUS = 'PENDENTE' ORDER BY ID_PEDIDO DESC;";

                //Criando um comando sql que será executado na base d edados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@ID_USUARIO", SqlDbType.Int).Value = usuario;
                    cmd.CommandText = strSQL;
                    //Executando instrução sql
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var pedido = new Pedido()
                    {
                        Id = Convert.ToInt32(row["ID_PEDIDO"]),
                        Data = Convert.ToDateTime(row["DT_DATA"]),
                        QtdSabores = Convert.ToInt32(row["QNT_SABORES"]),
                        Status = row["ST_STATUS"].ToString(),
                        Tamanho = row["ST_TAMANHO"].ToString()
                    };

                    return pedido;
                }
            }
        }

        public List<Pedido> BuscarTodos()
        {
            var lst = new List<Pedido>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM TB_PEDIDO;";

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
                        var pedido = new Pedido()
                        {
                            Id = Convert.ToInt32(row["ID_PEDIDO"]),
                            Data = Convert.ToDateTime(row["DT_DATA"]),
                            QtdSabores = Convert.ToInt32(row["QNT_SABORES"]),
                            Status = row["ST_STATUS"].ToString(),
                            Tamanho = row["ST_TAMANHO"].ToString()
                        };

                        lst.Add(pedido);
                    }
                }
            }
            return lst;
        }
    }
}
