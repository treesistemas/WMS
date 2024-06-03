using Negocios.Impressao;
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
    public partial class FrmPickingSemEstoque : Form
    {
        public FrmPickingSemEstoque()
        {
            InitializeComponent();
        }

        private void FrmPickingSemEstoque_Load(object sender, EventArgs e)
        {

        }


        public void GerarRelatorio(string tipo)
        {
            try
            {
                //Instância o relatório
                RelPickingSemEstoque produto = new RelPickingSemEstoque();

                //Instância a camada de Negocios
                ImpressaoEstoquexPickingNegocios estoqueNegocios = new ImpressaoEstoquexPickingNegocios();
                //Instância a camada de objêto
                EstoquexPickingCollection estoqueCollection = new EstoquexPickingCollection();

                //Passa os dados da pesquisa
                estoqueCollection = estoqueNegocios.PesqPickingSemEstoque(tipo);

                if (estoqueCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    produto.Database.Tables["produto"].SetDataSource(estoqueCollection);

                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = produto;
                    crystalReportViewer1.RefreshReport();
                    Show();
                }
                else
                {
                    MessageBox.Show("Nenhuma informação!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o relatório! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
