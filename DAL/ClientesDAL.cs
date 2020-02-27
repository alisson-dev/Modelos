using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loja.Modelos;
using System.Data.SqlClient;
using System.Data;

namespace Loja.DAL
{
    public class ClientesDAL
    {
        //peganda dados dos clientes
        public void Incluir(ClienteInformation cliente)
        {
            //conexao
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Dados.StringDeConexao;
                //command
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "insert into CLIENTES(nome, email, telefone) values (@nome, @email, @telefone); select @@IDENTITY;";
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@email", cliente.Email);
                cmd.Parameters.AddWithValue("telefone", cliente.Telefone);
                cn.Open();
                cliente.Codigo = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                throw new Exception("Servidor SQL Erro:" + ex.Number);
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

        public void Alterar(ClienteInformation cliente)
        {
            //conexao
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Dados.StringDeConexao;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "update CLIENTES set nome=@nome, email=@email, telefone=@telefone where codigo = @codigo;";

                cmd.Parameters.AddWithValue("@codigo", cliente.Codigo);
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@email", cliente.Email);
                cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Servidor SQL Erro:" + ex.Number);
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
            //conexao
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Dados.StringDeConexao;
                //
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "delete from CLIENTES where codigo = " + codigo;
                cn.Open();

                int resultado = cmd.ExecuteNonQuery();
                if (resultado != 1) // se o resultado for diferente de um não será possível excluir resultado.
                {
                    throw new Exception("Não foi possível excluir o cliente" + codigo);
                }
            }

            catch (SqlException ex)
            {
                throw new Exception("Servidor SQL Erro: " + ex.Number);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cn.Close();
            }

        }

        public DataTable Listagem()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from CLIENTES", Dados.StringDeConexao);
            da.Fill(tabela);
            return tabela;
        }
    }
}

   


