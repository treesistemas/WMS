using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia.Relatorio
{
    public class MapaSeparacao
    {
        public string empresa { get; set; }
        public int codManifesto { get; set; }
        public int qtdPedidoManifesto { get; set; }
        public double PesoTotalManifesto { get; set; }
        public int sequenciaPedido { get; set; }
        public string letraPedido { get; set; }
        public string rotaPedido { get; set; }
        public string veiculoPedido { get; set; }
        public DateTime dataPedido { get; set; }
        public int codPedido { get; set; }
        public string nomeCliente { get; set; }
        public string FantasiaCliente { get; set; }
        public int qtdGrandeza { get; set; }
        public int qtdFracionada { get; set; }
        public int qtdFlowRack { get; set; }
        public double pesoPedido { get; set; }
        public double cubagemPedido { get; set; }
        public string observacaoPedido { get; set; }
        public string observacaoCliente { get; set; }
        public string flowRackPedido { get; set; }       
        public string exigeDataCliente { get; set; }
        public DateTime dataExigidaCliente { get; set; }
        public string exigePaletizacaoCliente { get; set; }
        public string exigeCxaFechadaCliente { get; set; }
        public string naoAceitaDividirCargaCliente { get; set; }
        public string rotaCompartilhada { get; set; }

        public string cidadeCliente { get; set; }
        public string bairroCliente { get; set; }

        public string pedEndereco { get; set; }
        public string pedEndNumero{ get; set; }

        public string pedNomeRepresentante { get; set; }

        public string impressaoMapa { get; set; }

        public string pedPrazo { get; set; }
        public int tipoPedido { get; set; }
        public int tipoTabela { get; set; }
        public int tipoPedidoVencimento { get; set; }

        public string pedPagamento { get; set; }
        public string nomeMotorista { get; set; }

    }
}
