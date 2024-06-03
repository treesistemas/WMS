using System;

namespace ObjetoTransferencia
{
    public class Pedido : Cliente
    {
        public string empresa { get; set; }

        public int codPedido { get; set; }
        public int notaFiscal { get; set; }
        public int serieNotaFiscal { get; set; }
        public DateTime dataPedido { get; set; }
        public DateTime dataFaturamento { get; set; }
        public int? manifestoPedido { get; set; }
        public string placaVeiculo { get; set; }

        public string cfop { get; set; }
        public string tipoPedido { get; set; }
        public string bonificacaoEntrega { get; set; }// se a bonificação é para ir para entrega
        public string formaPagamento { get; set; }
        public string prazo { get; set; }

        public double valorSubstituicaoPedido { get; set; }
        public double totalPedido { get; set; }
        public int qtdItensPedido { get; set; }
        public double cubagemPedido { get; set; }
        public double pesoPedido { get; set; }

        public string observacaoPedido { get; set; }
        public string representante { get; set; }
        public string pedFormaCarga { get; set; } //Pedido irá para rota SIM = S ou NÃO = N
        public string tipoServico { get; set; } //Tipo de pedido (SERVIÇO/ENTREGA)
        public string pedStatus { get; set; }


        public string numeroVolume { get; set; }

        public string barraVolume { get; set; }

        public DateTime? inicioConferencia { get; set; }
        public DateTime? fimConferencia { get; set; }

        public int codSeparador { get; set; }

        public string loginSeparador { get; set; }
        public string restricaoveiculoPedido { get; set; }
        public string veiculoPedido { get; set; }

        public int codEstacao { get; set; }
    }
}
