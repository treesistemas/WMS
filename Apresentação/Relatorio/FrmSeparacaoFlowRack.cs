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
    public partial class FrmSeparacaoFlowRack : Form
    {
        public FrmSeparacaoFlowRack()
        {
            InitializeComponent();
        }

        private void FrmSeparacaoFlowRack_Load(object sender, EventArgs e)
        {

        }

        public void GerarRelatorio(int pedido)
        {
            try
            {
                //Instância o relatório
                RelItemFlowRack produto = new RelItemFlowRack();

                //Instância a camada de Negocios
                ConsultarFlowRackNegocios itemNegocios = new ConsultarFlowRackNegocios();

                //Instância a camada de objêto
                RelItemFlowRackCollection itemCollection = new RelItemFlowRackCollection();
                //Passa os dados da pesquisa
                itemCollection = itemNegocios.PesqItensFlowRack(pedido);

                if (itemCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    produto.Database.Tables["item"].SetDataSource(itemCollection);

                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = produto;
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
                MessageBox.Show(""+ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
