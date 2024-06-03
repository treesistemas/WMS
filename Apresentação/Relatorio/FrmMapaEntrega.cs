using CrystalDecisions.CrystalReports.Engine;
using Negocios.Relatorio;
using ObjetoTransferencia.Relatorio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wms.Relatorio
{
    public partial class FrmMapaEntrega : Form
    {
        public FrmMapaEntrega()
        {
            InitializeComponent();
        }

        private void FrmMapaEntrega_Load(object sender, EventArgs e)
        {
            //GerarRelatorio(27110, 0, "");
        }


        public int GerarRelatorio(string empresa, int codManifesto, int codPedido, string controlaSequenciaCarregamento, string ordem)
        {
            //Instância o relatório
            RelMapaEntrega pedido = new RelMapaEntrega();
            ReportDocument itemPedido;


            //Instância a camada de Negocios
            PedidoNegocios pedidoNegocios = new PedidoNegocios();
            //Instância a camada de objêto
            MapaSeparacaoCollection pedidoCollection = new MapaSeparacaoCollection();
            //Passa os dados da pesquisa
            pedidoCollection = pedidoNegocios.PesqPedidoManifesto(empresa, codManifesto, codPedido, controlaSequenciaCarregamento, false, false, "desc");


            if (pedidoCollection.Count > 0)
            {
                //Passa os dados para o dataset do relatório
                pedido.Database.Tables["Mapa"].SetDataSource(pedidoCollection);

                //Instância a camada de objêto
                ItemMapaSeparacaoCollection itemPedidoCollection = new ItemMapaSeparacaoCollection();
                //Passa os dados da pesquisa
                itemPedidoCollection = pedidoNegocios.PesqItemPedidoManifesto(empresa, codManifesto, codPedido, false, false);

                if (itemPedidoCollection.Count > 0)
                {
                    //Instância o relatório
                    itemPedido = pedido.OpenSubreport("RelItemMapaSeparacao.rpt");
                    //Passa os dados para o dataset do relatório
                    itemPedido.Database.Tables["Item"].SetDataSource(itemPedidoCollection.OrderBy(n => n.ordem));

                }

                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.ReportSource = pedido;
                crystalReportViewer1.RefreshReport();
                Show();
            }
            else
            {
                MessageBox.Show("Nenhuma informação encontrada", "WMS - Informação");
            }

            return 1;


        }


    }
}
