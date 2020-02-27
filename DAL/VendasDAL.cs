using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Loja.Modelos;

namespace Loja.DAL
{
    public class VendasDAL
    {
        public DataTable ListaDeProdutos
        {
            get
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = Dados.StringDeConexao;
                cn.Open();

                SqlDataAdapter da = new SqlDataAdapter("select * from produto", cn);
                DataTable dt = new DataTable();

                da.Fill(dt);
                cn.Close();
                return dt;                    
            }
        }

        //Propriedade que retorna uma lista de Clientes

        public DataTable ListaDeClientes
        {
            get
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = Dados.StringDeConexao;
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from clientes", cn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();
                return dt;

            }
        }

        public void Incluir(VendaInformation venda)
        {
            //conexao
            SqlConnection cn = new SqlConnection();
            SqlTransaction t = null;

            try
            {
                cn.ConnectionString = Dados.StringDeConexao;

                //command
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = cn;
                cmd1.CommandText = @"insert into vendas(CodigoCliente, CodigoProduto, Data, Quantidade, Faturado) VALUES (@CodigoCliente, @CodigoProduto, @Data, @Quantidade, @Faturado); select @@IDENTITY;";

                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = cn;
                cmd2.CommandText = @"Update Produtos Set Estoque = Estoque - @Quantidade where Codigo=@CodigoProduto";

                cn.Open();

                t = cn.BeginTransaction(IsolationLevel.Serializable);// default  obs: não entendi isso aqui

                cmd1.Transaction = t; //não entendi isso aqui
                cmd2.Transaction = t; //não entendi isso aqui

                cmd1.Parameters.AddWithValue("@codigocliente", venda.CodigoCliente);
                cmd1.Parameters.AddWithValue("@codigoproduto", venda.CodigoProduto);
                cmd1.Parameters.AddWithValue("@data", venda.Data);
                cmd1.Parameters.AddWithValue("@quantidade", venda.Quantidade);
                cmd1.Parameters.AddWithValue("@faturado",  venda.Faturado);

                cmd2.Parameters.AddWithValue("@quantidade", venda.Quantidade);
                cmd2.Parameters.AddWithValue("@codigoproduto", venda.CodigoProduto);
                venda.Codigo = Convert.ToInt32(cmd1.ExecuteScalar());

                cmd2.ExecuteNonQuery();

                t.Commit();
            }
            catch(Exception ex)
            {
                t.Rollback();
                throw new Exception("Servidor no Servidor: "+ ex.Message);
            }        
            finally
            {
                cn.Close();
            }
        }
    }
}
