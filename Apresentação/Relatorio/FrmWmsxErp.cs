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
    public partial class FrmWmsxErp : Form
    {
        public FrmWmsxErp()
        {
            InitializeComponent();
        }

        private void FrmWmsxErp_Load(object sender, EventArgs e)
        {
            //GerarRelatorio("", 4, 2, "PAR");
        }

        public void GerarRelatorio(string tipo, int regiao , int rua, string lado, string codProduto, string usuario)
        {
            try
            {
                //Instância o relatório
                RelWMSXERP produto = new RelWMSXERP();

                //Instância a camada de Negocios
                WmsxErpNegocios produtoNegocios = new WmsxErpNegocios();
                //Instância a camada de objêto
                WmsxErpCollection produtoCollection = new WmsxErpCollection();

                //Passa os dados da pesquisa
                produtoCollection = produtoNegocios.PesqWmsComFlowRackxErp(tipo, regiao, rua, lado, codProduto, usuario);

                if (produtoCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    produto.Database.Tables["Produto"].SetDataSource(produtoCollection);


                    crystalReportViewer.ReportSource = null;
                    crystalReportViewer.ReportSource = produto;
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
                MessageBox.Show("Ocorreu um erro ao gerar o relatório! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
