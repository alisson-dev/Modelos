using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.DAL
{
    public class Dados
    {
        public static string StringDeConexao
        {
            get
            {
                return "server=MAQ-607567\\SQLEXPRESS; database=Loja; user=username; pwd=senha";
                // buscar dados para conexão no sql server
            }
        }

    }
}
