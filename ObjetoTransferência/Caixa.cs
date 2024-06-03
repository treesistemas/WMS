using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Caixa
    {
        //Código da caixa
        public int codCaixa { get; set; }
        //Descrição da caixa
        public string descCaixa { get; set; }
        //Altura da Caixa
        public double altura { get; set; }
        //largura da Caixa
        public double largura { get; set; }
        //comprimento da Caixa
        public double comprimento { get; set; }
        //cubagem da Caixa
        public double cubagem { get; set; }
        //cubagem da Caixa
        public double peso { get; set; }
    }
}

