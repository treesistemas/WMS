using Negocios;
using ObjetoTransferencia.Relatorio;
using System;
using System.Windows.Forms;

namespace Wms.Relatorio
{
    public partial class FrmRendimentoFlowRack : Form
    {
        public FrmRendimentoFlowRack()
        {
            InitializeComponent();
        }

        public void GerarRelatorioRendimento(string dataInicial, string dataFinal)
        {
            try
            {
                //Instância o relatório
                RelRendimentoFlowRack rendimentoFlowRack = new RelRendimentoFlowRack();

                //Instância a camada de Negocios
                ProcessamentoFlowNegocios processamentoFlowNegocios = new ProcessamentoFlowNegocios();
                //Instância a camada de objêto
                RendimentoFlowRackCollection rendimentoCollection = new RendimentoFlowRackCollection();
                //Passa os dados da pesquisa
                rendimentoCollection = processamentoFlowNegocios.PesquisarRendimentoFlowRack(dataInicial, dataFinal);

                if (rendimentoCollection.Count > 0)
                {                    
                    //Passa os dados para o dataset do relatório
                    rendimentoFlowRack.Database.Tables["RendimentoFlowRack"].SetDataSource(rendimentoCollection);
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = rendimentoFlowRack;
                    crystalReportViewer1.RefreshReport();
                    Show();

                }
                else
                {
                    MessageBox.Show("Não existe rendimento no período!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o rendimento do flow rack! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
