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
    public partial class FrmPaletizacaoNotaCega : Form
    {
        public FrmPaletizacaoNotaCega()
        {
            InitializeComponent();
        }

        private void FrmPaletizacaoNotaCega_Load(object sender, EventArgs e)
        {
            //GerarRelatorio();
        }


        public void GerarRelatorio(int notaCega, string empresa)
        {
            try
            {
                //Instância o relatório
                RelPaletizacaoNotaCega produto = new RelPaletizacaoNotaCega();

                //Instância a camada de Negocios
                NotaEntradaNegocios estoqueNegocios = new NotaEntradaNegocios();
                //Instância a camada de objêto
                PaletizacaoNotaCegaCollection produtoCollection = new PaletizacaoNotaCegaCollection();

                //Passa os dados da pesquisa
                produtoCollection = estoqueNegocios.PesqRelPaletizacaoNotaCega(notaCega, empresa);

                if (produtoCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    produto.Database.Tables["Paletizacao"].SetDataSource(produtoCollection);



                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = produto;
                    crystalReportViewer1.RefreshReport();
                    Show();
                }
                else
                {
                    MessageBox.Show("Nenhuma informação encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o relatório! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
