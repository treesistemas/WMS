using CrystalDecisions.CrystalReports.Engine;
using Negocios.Relatorio;
using ObjetoTransferencia.Relatorio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Wms.Relatorio
{
    public partial class FrmMapaSeparacao : Form
    {
        public int codManifesto;
        public int codUsuario;
        public string controlaSequenciaCarregamento;

        public FrmMapaSeparacao()
        {
            InitializeComponent();
        }

        private void crystalReportViewer_Load(object sender, EventArgs e)
        {
            //Chama o relatório
            //GerarRelatorio(codManifesto);
        }



        public int GerarRelatorio(string empresa, int codManifesto, int codPedido, string controlaSequenciaCarregamento, bool naoConferido, bool naoImpresso, string ordem)
        {
            //Instância o relatório
            PedidoMapaSeparacao pedido = new PedidoMapaSeparacao();
            ReportDocument itemPedido;
            ReportDocument flowRackPedido;
            ReportDocument grandezaPedido;

            //Instância a camada de Negocios
            PedidoNegocios pedidoNegocios = new PedidoNegocios();
            //Instância a camada de objêto
            MapaSeparacaoCollection pedidoCollection = new MapaSeparacaoCollection();

            //Passa os dados da pesquisa
            pedidoCollection = pedidoNegocios.PesqPedidoManifesto(empresa, codManifesto, codPedido, controlaSequenciaCarregamento, naoConferido, naoImpresso, ordem);
            //Passa os dados para o dataset do relatório
            pedido.Database.Tables["Mapa"].SetDataSource(pedidoCollection);

            //Instância a camada de objêto
            ItemMapaSeparacaoCollection itemPedidoCollection = new ItemMapaSeparacaoCollection();
            //Passa os dados da pesquisa
            itemPedidoCollection = pedidoNegocios.PesqItemPedidoManifesto(empresa, codManifesto, codPedido, naoConferido, naoImpresso);

            if (itemPedidoCollection.Count > 0)
            {
                //Instância o relatório
                itemPedido = pedido.OpenSubreport("RelItemMapaSeparacao.rpt");
                //Passa os dados para o dataset do relatório
                itemPedido.Database.Tables["Item"].SetDataSource(itemPedidoCollection.OrderBy(n => n.ordem));

            }

            //Instância a camada de objêto
            FlowRackMapaSeparacaoCollection flowRackCollection = new FlowRackMapaSeparacaoCollection();
            //Passa os dados da pesquisa
            flowRackCollection = pedidoNegocios.PesqVolumeFlowRack(empresa, codManifesto, codPedido);

            if (flowRackCollection.Count > 0)
            {

                //Instância o relatório
                flowRackPedido = pedido.OpenSubreport("RelFlowRackMapaSeparacao.rpt");
                //Passa os dados para o dataset do relatório
                flowRackPedido.Database.Tables["FlowRack"].SetDataSource(flowRackCollection);

            }

            //Instância a camada de objêto - pesquisar Grandeza
            GrandezaMapaSeparacaoCollection grandezaCollection = new GrandezaMapaSeparacaoCollection();
            //Passa os dados da pesquisa
            grandezaCollection = pedidoNegocios.PesqGrandeza(empresa, codManifesto, codPedido);

            if (grandezaCollection.Count > 0)
            {
                //Instância o relatório
                grandezaPedido = pedido.OpenSubreport("GrandezaMapaSeparacao.rpt");
                //Passa os dados para o dataset do relatório
                grandezaPedido.Database.Tables["Grandeza"].SetDataSource(grandezaCollection);

            }

            crystalReportViewer.ReportSource = null;
            crystalReportViewer.ReportSource = pedido;
            crystalReportViewer.RefreshReport();
            Show();

            return  1;


        }
    }
}
