using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia.Relatorio
{
    public class RelEnderecoProduto
    {        
        public int codEndereco { get; set; }
        public string endereco { get; set; }
        public string tipoEndereco { get; set; }
        public int idProduto { get; set; }
        public string pesoVariavel { get; set; }
        public double estoque { get; set; }
        public double estoqueUnidade { get; set; }
        public double estoqueCaixa { get; set; }
        public string estoqueReservado { get; set; }
        public string unidadePicking { get; set; }
        public string unidadePulmao { get; set; }
        public DateTime vencimento { get; set; }
        public double peso { get; set; }
        public string lote { get; set; }      
        public string bloqueio  { get; set; }
        public string dataBloqueio { get; set; }
        public string motivoBloqueio { get; set; }


    }
   
}
