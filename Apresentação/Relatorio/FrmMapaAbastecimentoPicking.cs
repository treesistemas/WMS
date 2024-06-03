using CrystalDecisions.CrystalReports.Engine;
using Negocios;
using ObjetoTransferencia.Relatorio;
using System;
using System.Windows.Forms;

namespace Wms.Relatorio
{
    public partial class FrmMapaAbastecimentoPicking : Form
    {
        public FrmMapaAbastecimentoPicking()
        {
            InitializeComponent();
        }


        private void crystalReportViewer_Load(object sender, EventArgs e)
        {
            //Chama o relatório
            //GerarRelatorio(codAbastecimento);
        }

        public void GerarRelatorioPicking(int codAbastecimento, string tipo)
        {
            try
            {
                //Instância o relatório
                RelAbastecimentoGrandeza mapaAbastecimento = new RelAbastecimentoGrandeza();
                ReportDocument itemPulmaoAbastecimento;
                //ReportDocument flowRackPedido;
                //ReportDocument grandezaPedido;

                //Instância a camada de Negocios
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();
                //Instância a camada de objêto
                MapaAbastecimentoCollection abastecimentoCollection = new MapaAbastecimentoCollection();

                //Passa os dados da pesquisa
                abastecimentoCollection = abastecimentoNegocios.RelatorioAbastecimentoGrandeza(codAbastecimento);


                if (!tipo.Equals(abastecimentoCollection[0].tipoAbastecimento))
                {
                    MessageBox.Show("Por favor selecione o tipo de impressão correta!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (abastecimentoCollection.Count > 0)
                    {
                        //Passa os dados para o dataset do relatório
                        mapaAbastecimento.Database.Tables["MapaAbastecimento"].SetDataSource(abastecimentoCollection);


                        //Instância a camada de objêto
                        ItemMapaAbastecimentoCollection itemPulmaoCollection = new ItemMapaAbastecimentoCollection();
                        //Passa os dados da pesquisa
                        itemPulmaoCollection = abastecimentoNegocios.RelatorioItensAbastecimentoPulmao(codAbastecimento);

                        if (itemPulmaoCollection.Count > 0)
                        {
                            //Instância o relatório
                            itemPulmaoAbastecimento = mapaAbastecimento.OpenSubreport("RelItemAbastecimentoGrandeza.rpt");
                            //Passa os dados para o dataset do relatório
                            itemPulmaoAbastecimento.Database.Tables["ItemPulmao"].SetDataSource(itemPulmaoCollection);
                        }

                        /*//Instância a camada de Negocios Gerar Flow Rack
                        ItemPedidoNegocios flowPedidoNegocios = new ItemPedidoNegocios();
                        //Instância a camada de objêto
                        FlowRackMapaSeparacaoCollection flowRackCollection = new FlowRackMapaSeparacaoCollection();
                        //Passa os dados da pesquisa
                        flowRackCollection = itemPedidoNegocios.PesqVolumeFlowRack(codManifesto);

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
                        grandezaCollection = itemPedidoNegocios.PesqGrandeza(codManifesto);

                        if (grandezaCollection.Count > 0)
                        {
                            //Instância o relatório
                            grandezaPedido = pedido.OpenSubreport("GrandezaMapaSeparacao.rpt");
                            //Passa os dados para o dataset do relatório
                            grandezaPedido.Database.Tables["Grandeza"].SetDataSource(grandezaCollection);
                        }

                        */


                        crystalReportViewer1.ReportSource = null;
                        crystalReportViewer1.ReportSource = mapaAbastecimento;
                        crystalReportViewer1.RefreshReport();
                        Show();
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma ordem encontrada", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o mapa de abastecimento! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
