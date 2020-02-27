using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Modelos
{
    public class ClienteInformation
    {
        private int _codigo;
        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        private string _nome;
        public String Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _telefone;
        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }

    }
}
