using System;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Wms.Relatorio
{
    public partial class FrmMapaPendencia : Form
    {
        public FrmMapaPendencia()
        {
            InitializeComponent();
        }

        private void FrmMapaPendencia_Load(object sender, EventArgs e)
        {

        }

        public void GerarRelatorio(string dataInicial, string dataFinal, string empresa)
        {
            try
            {
                //Instância o relatório
                RelPedidoPendencia pedido = new RelPedidoPendencia();
                ReportDocument itemPedido;

                //Instância a camada de Negocios
                PedidoPendenciaNegocios pedidoNegocios = new PedidoPendenciaNegocios();
                //Instância a camada de objêto
                OcorrenciaCollection ocorrenciaCollection = new OcorrenciaCollection();

                //Passa os dados da pesquisa
                ocorrenciaCollection = pedidoNegocios.PesqPendencia(dataInicial, dataFinal);

                if (ocorrenciaCollection.Count > 0)
                {                    
                    //Passa os dados para o dataset do relatório
                    pedido.Database.Tables["dsOcorrencia"].SetDataSource(ocorrenciaCollection);

                    //Instância a camada de objêto
                    ItemPendenciaCollection itemCollection = new ItemPendenciaCollection();
                    //Passa os dados da pesquisa
                    itemCollection = pedidoNegocios.PesqItemPendencia(dataInicial, dataFinal);

                    if (itemCollection.Count > 0)
                    {
                        //Instância o relatórioDataSet não dá suporte a System.Nullable<>
                        itemPedido = pedido.OpenSubreport("RelItemPendencia.rpt");
                        //Passa os dados para o dataset do relatório
                        itemPedido.Database.Tables["ItemOcorrencia"].SetDataSource(itemCollection);
                    }

                    crystalReportViewer.ReportSource = null;
                    crystalReportViewer.ReportSource = pedido;
                    crystalReportViewer.RefreshReport();
                    Show();
                }
                else
                {
                    MessageBox.Show("Não existe informação para o período pesquisado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar às pendências! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
