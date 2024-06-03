using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia.Impressao
{
    public class Endereco
    {
        public string empresa { get; set; }
        public int regiao { get; set; }
        public string tipoRegiao { get; set; }
        public int rua { get; set; }
        public int Bloco { get; set; }
        public int nivel { get; set; }
        public string lado { get; set; }
        public string endereco { get; set; }
        public string tipoEndereco { get; set; }
        public string produto { get; set; }
        public string pesoVariavel { get; set; }
        public double estoque { get; set; }
        public double estoqueUnidade { get; set; }
        public double estoqueCaixa { get; set; }
        public double estoqueReservado { get; set; }
        public string unidadePicking { get; set; }
        public string unidadePulmao { get; set; }
        public string vencimento { get; set; }
        public string peso { get; set; }
        public string lote { get; set; }
        public string bloqueio { get; set; }
        public string dataBloqueio { get; set; }
        public string motivoBloqueio { get; set; }

        public int capacidade { get; set; }
    }
}
