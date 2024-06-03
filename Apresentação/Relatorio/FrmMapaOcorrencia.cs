using Negocios;
using System;
using System.Linq;
using System.Windows.Forms;
using ObjetoTransferencia;

namespace Wms.Relatorio
{
    public partial class FrmMapaOcorrencia : Form
    {
        public FrmMapaOcorrencia()
        {
            InitializeComponent();
        }

        private void FrmOcorrencia_Load(object sender, EventArgs e)
        {
            //GerarRelatorio();
        }


        public void GerarRelatorio(string tipo, string dataIncial, string dataFinal)
        {
            try
            {
                //Instância o relatório
                RelDevolucaoOcorrencia ocorrencia = new RelDevolucaoOcorrencia();

                //Instância a camada de Negocios
                OcorrenciaNegocios ocorrenciaNegocios = new OcorrenciaNegocios();
                //Instância a camada de objêto
                OcorrenciaCollection ocorrenciaCollection = new OcorrenciaCollection();

                //Passa os dados da pesquisa
                ocorrenciaCollection = ocorrenciaNegocios.ImpressaoOcorrencia(tipo, dataIncial, dataFinal);

                if (ocorrenciaCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    ocorrencia.Database.Tables["dsOcorrencia"].SetDataSource(ocorrenciaCollection);

                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = ocorrencia;
                    crystalReportViewer1.RefreshReport();
                    Show();
                }
                else
                {
                    MessageBox.Show("Nenhuma informação para o período selecionado!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - Informação");
            }
        }

    }
}
