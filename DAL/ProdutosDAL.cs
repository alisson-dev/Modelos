using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Loja.Modelos;
using System.Collections;

namespace Loja.DAL
{
    public class ProdutosDAL
    {
        public ArrayList ProdutosEmfalta()
        {
            SqlConnection cn = new SqlConnection(Dados.StringDeConexao);
            SqlCommand cmd = new SqlCommand("select * from Produtos where Estoque < 10", cn);
            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            ArrayList lista = new ArrayList();

            while (dr.Read())
            {
                ProdutoInformation produto = new ProdutoInformation();
                produto.Codigo = Convert.ToInt32(dr["codigo"]);
                produto.Nome = dr["nome"].ToString();
                produto.Estoque = Convert.ToInt32(dr["estoque"]);
                produto.Preco = Convert.ToDecimal(dr["preco"]);
                lista.Add(produto);
            }

            dr.Close();
            cn.Close();
            return lista;
        }

        public void Incluir(ProdutoInformation produto)
        {
            //conexao
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Dados.StringDeConexao;
                //command

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                cmd.CommandText = "insert into Produtos(nome, preco, estoque) values (@nome, @preco, @estoque); select @@EDENTITY;";

                cmd.Parameters.AddWithValue("@nome", produto.Nome);
                cmd.Parameters.AddWithValue("@preco", produto.Preco);
                cmd.Parameters.AddWithValue("@estoque", produto.Estoque);
                cn.Open();

                produto.Codigo = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (SqlException ex)
            {
                throw new Exception("Servidor SQl ERRO: " + ex.Number);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public void Alterar(ProdutoInformation produto)
        {
            //conexao
            SqlConnection cn = new SqlConnection();

            try
            {
                cn.ConnectionString = Dados.StringDeConexao;
                //command

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "AlterarProduto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo", produto.Codigo);
                cmd.Parameters.AddWithValue("@nome", produto.Nome);
                cmd.Parameters.AddWithValue("@preco", produto.Preco);
                cmd.Parameters.AddWithValue("@estoque", produto.Estoque);
                cmd.Parameters["@valorEstoque"].Direction = ParameterDirection.Output;

                cn.Open();
                cmd.ExecuteNonQuery();

                decimal valorEstoque = Convert.ToDecimal(cmd.Parameters["@valorEstoque"]);
                if (valorEstoque < 500)
                {
                    throw new Exception("Atenção! Valor baixo no Estoque.");
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Servidor SQL Erro: " + ex.Number);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public void Excluir(int codigo)
        {
            //pendente
        }

        public DataTable Listagem()
        {
            DataTable tabela = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter();
            da.Fill(tabela);
            return tabela;
        }
    }            
}
  


