using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Configuracao
    {
        //Configuração
        public int codConfiguracao { get; set; }
        public string empresa { get; set; }
        public string logoEmpresa { get; set; }
        public string caminhoProducao { get; set; }
        public string caminhoHomologacao { get; set; }
        public string imagemProduto { get; set; }

        public double pesoEndereco { get; set; }
        public int produtoEndereco { get; set; }

        public int pedidoSeparador { get; set; }
        public int itensSeparador { get; set; }
        public double alturaPalete { get; set; }


    }
}
