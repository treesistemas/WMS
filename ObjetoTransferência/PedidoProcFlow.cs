using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class PedidoProcFlow
    {
        public DateTime dataPedido { get; set; } //Data de Implantação do Pedido
        public long codPedido { get; set; } //Código Pedido
        public int rotaPedido { get; set; } //Rota Pedido
        public int? manifestoPedido { get; set; } //Manifesto Pedido
        public int estInicial { get; set; } //Estação inicial
        public int estFinal { get; set; } //Estação final
        public string estAtual { get; set; } //Estação atual
        public int itensPedido { get; set; } //quantidade de itens do pedido
        public int? volumePedido { get; set; } //Volumes realizados (Online)
        public DateTime? dataEnvioProcessamento { get; set; } //Data de Processamento
        public DateTime? dataInicialProcessamento { get; set; } //Data inicial de processamento
        public DateTime? dataFinalProcessamento { get; set; } //Data Final de Processamento
        public string tempoProcessamento { get; set; } //Tempo de Processamento
        public int itensAuditar { get; set; } //Quantidade de itens para serem auditados
        public int itensAuditado { get; set; } //Quantidade de itens auditados
        public int volumeEnderecado { get; set; } //Volume endereçado = 0

    }
}
