using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia.Impressao
{
    public class NotaCega
    {
        public string empresa { get; set; }
        public DateTime data { get; set; }
        public string usuNotaCega { get; set; }
        public int notaCega {get; set;}
        public string fornecedor { get; set; }
        public string notaEntrada { get; set; }

        public int qtdNota { get; set; }
        public int qtdItens { get; set; }
        public int qtdVolumes { get; set; }
        public double totalPeso { get; set; }

        public string usuConferente { get; set; }
        public string dataInicial { get; set; }
        public string dataFinal { get; set; }




    }
}
