using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia.Impressao
{
    public class ItemNotaCega
    {
        //Objeto controla os itens da nota, nota cega e armazenagem
        
       public int codNotaCega { get; set; }
        public string codProduto { get; set; }
        public string descProduto { get; set; }
        public int fatorPulmao { get; set; }
        public double quantidadeNota { get; set; }
        public double quantidadeNotaCaixa { get; set; }
        public double quantidadeNotaUnidade { get; set; }

        public DateTime dataConferencia { get; set; }
        public double quantidadeConferida { get; set; }
        public double quantidadeConferidaCaixa { get; set; }
        public double quantidadeConferidaUnidade { get; set; }
        public double quantidadeAvariada { get; set; }
        public double quantidadeFalta { get; set; }

        public string loteProduto { get; set; }
        public string validadeProduto { get; set; }
        public int paleteAssociado { get; set; }
        public double pesoProduto { get; set; }      
        public string undPulmao { get; set; }
        public string undPicking { get; set; }

        public string PesoVariavel { get; set; }

    }
}
