using CrystalDecisions.CrystalReports.Engine;
using Negocios;
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
    public partial class FrmMapaAbastecimentoFlowRack : Form
    {
        public FrmMapaAbastecimentoFlowRack()
        {
            InitializeComponent();
        }

        private void FrmMapaAbastecimentoFlowRack_Load(object sender, EventArgs e)
        {

            //GerarRelatorio(929);
        }

        public void GerarRelatorio(int codAbastecimento, string tipo)
        {
            try
            {

                //Instância o relatório
                RelAbastecimentoFlowRack mapaAbastecimento = new RelAbastecimentoFlowRack();
                ReportDocument itemPicking;
                ReportDocument itemPulmao;

                //ReportDocument grandezaPedido;

                //Instância a camada de Negocios
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();
                //Instância a camada de objêto
                MapaAbastecimentoCollection abastecimentoCollection = new MapaAbastecimentoCollection();

                //Passa os dados da pesquisa
                abastecimentoCollection = abastecimentoNegocios.RelatorioAbastecimentoFlowRack(codAbastecimento);

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

                        //* Sub-Relatório do picking

                        //Instância a camada de objêto
                        ItemMapaAbastecimentoCollection itemPickingCollection = new ItemMapaAbastecimentoCollection();
                        //Passa os dados da pesquisa
                        itemPickingCollection = abastecimentoNegocios.RelatorioItensAbastecimentoFlowrackGrandeza(codAbastecimento);

                        if (itemPickingCollection.Count > 0)
                        {
                            //Instância o relatório
                            itemPicking = mapaAbastecimento.OpenSubreport("RelItemAbastecimentoFlowRackGrandeza.rpt");
                            //Passa os dados para o dataset do relatório
                            itemPicking.Database.Tables["ItemPicking"].SetDataSource(itemPickingCollection);

                            crystalReportViewer1.ReportSource = null;
                            crystalReportViewer1.ReportSource = mapaAbastecimento;
                            crystalReportViewer1.RefreshReport();
                            Show();
                        }

                        /* Sub-Relatório do Pulmao

                        //Instância a camada de objêto
                        ItemMapaAbastecimentoCollection itemPulmaoCollection = new ItemMapaAbastecimentoCollection();
                        //Passa os dados da pesquisa
                        itemPulmaoCollection = abastecimentoNegocios.RelatorioItensAbastecimentoFlowrackPulmao(codAbastecimento);

                        if (itemPulmaoCollection.Count > 0)
                        {
                            //Instância o relatório
                            itemPulmao = mapaAbastecimento.OpenSubreport("RelItemAbastecimentoFlowRackPulmao.rpt");
                            //Passa os dados para o dataset do relatório
                            itemPulmao.Database.Tables["ItemPulmao"].SetDataSource(itemPulmaoCollection);
                        }*/


                    }
                    else
                    {
                        MessageBox.Show("Nenhuma ordem encontrada", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o mapa de abastecimento do flow rack! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GerarRelatorioPulmao(int codAbastecimento)
        {
            try
            {

                //Instância o relatório
                RelAbastecimentoFlowRackPulmao mapaAbastecimento = new RelAbastecimentoFlowRackPulmao();
                ReportDocument itemPulmao;

                //ReportDocument grandezaPedido;

                //Instância a camada de Negocios
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();
                //Instância a camada de objêto
                MapaAbastecimentoCollection abastecimentoCollection = new MapaAbastecimentoCollection();

                //Passa os dados da pesquisa
                abastecimentoCollection = abastecimentoNegocios.RelatorioAbastecimentoFlowRack(codAbastecimento);

                if (abastecimentoCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    mapaAbastecimento.Database.Tables["MapaAbastecimento"].SetDataSource(abastecimentoCollection);

                    // Sub-Relatório do Pulmao

                    //Instância a camada de objêto
                    ItemMapaAbastecimentoCollection itemPulmaoCollection = new ItemMapaAbastecimentoCollection();
                    //Passa os dados da pesquisa
                    itemPulmaoCollection = abastecimentoNegocios.RelatorioItensAbastecimentoFlowrackPulmao(codAbastecimento);

                    if (itemPulmaoCollection.Count > 0)
                    {
                        //Instância o relatório
                        itemPulmao = mapaAbastecimento.OpenSubreport("RelItemAbastecimentoFlowRackPulmao.rpt");
                        //Passa os dados para o dataset do relatório
                        itemPulmao.Database.Tables["ItemPulmao"].SetDataSource(itemPulmaoCollection);

                        crystalReportViewer1.ReportSource = null;
                        crystalReportViewer1.ReportSource = mapaAbastecimento;
                        crystalReportViewer1.RefreshReport();
                        Show();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o mapa de abastecimento do flow rack! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
