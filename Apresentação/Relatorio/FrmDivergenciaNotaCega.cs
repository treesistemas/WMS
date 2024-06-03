using CrystalDecisions.CrystalReports.Engine;
using Negocios;
using ObjetoTransferencia.Impressao;
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
    public partial class FrmDivergenciaNotaCega : Form
    {
        public FrmDivergenciaNotaCega()
        {
            InitializeComponent();
        }

        private void FrmDivergenciaNotaCega_Load(object sender, EventArgs e)
        {

            //Chama o relatório
            //GerarRelatorio(12);
        }

        public void GerarRelatorio(int codNotaCega, string tipo, string empresa)
        {
            try
            {
                //Instância o relatório
                RelDivergenciaNotaCega notaCega = new RelDivergenciaNotaCega();
                ReportDocument itemNotaCega;

                //Instância a camada de Negocios
                NotaEntradaNegocios notaNegocios = new NotaEntradaNegocios();

                //Instância a camada de objêto
                NotaCegaCollection notaCollection = new NotaCegaCollection();
                //Passa os dados da pesquisa
                notaCollection = notaNegocios.PesqRelNotaCega(codNotaCega);


                //Passa os dados para o dataset do relatório
                notaCega.Database.Tables["Nota"].SetDataSource(notaCollection);


                //Instância a camada de objêto
                ItemNotaCegaCollection itemCollection = new ItemNotaCegaCollection();
                //Passa os dados da pesquisa
                itemCollection = notaNegocios.PesqRelItemNotaCega(codNotaCega, tipo, empresa);

                if (itemCollection.Count > 0)
                {

                    //Instância o relatório
                    itemNotaCega = notaCega.OpenSubreport("RelItemDivergenciaNotaCega.rpt");
                    //Passa os dados para o dataset do relatório
                    itemNotaCega.Database.Tables["Item"].SetDataSource(itemCollection);

                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = notaCega;
                    crystalReportViewer1.RefreshReport();
                    Show();
                }
                else
                {
                    MessageBox.Show("Nenhuma informação encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o relatório de divergência! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
