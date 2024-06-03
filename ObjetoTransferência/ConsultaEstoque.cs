using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class ConsultaEstoque
    {
        public string descEndereco { get; set; }

        public int idProduto { get; set; }
        public string codProduto { get; set; }
        public string descProduto { get; set; }

        public int codFornecedor { get; set; }
        public string nomeFornecedor { get; set; }

        public double estoque { get; set; }
        public double reserva { get; set; }
        public double saldo { get; set; }
        public double avaria { get; set; }
        public double wms { get; set; }

        public double qtdCaixa { get; set; }
        public int unidadeVenda { get; set; }
        public string unidadePicking { get; set; }
        public string numeroBarra { get; set; }
        public int mutiploVenda { get; set; }
      
        public string categoria { get; set; }
        public double preco { get; set; }
        public bool status { get; set; }
        public bool controlaValidade { get; set; }
        public bool flowrack { get; set; }
        public bool auditaFlowrack { get; set; }
        public bool paleteBlocado { get; set; }

        public int diasValidade { get; set; }
        public int tolerancia { get; set; }
        public string tipoArmazenamento { get; set; }
        public string tamanhoPalete { get; set; }
        public int nivel { get; set; }
        public int lastro { get; set; }
        public int altura { get; set; }
        public int totalPalete { get; set; }

        public DateTime vencimento { get; set; }
        public string lote { get; set; }
        public string nomeEmpresa { get; set; }

        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }
    }
}
