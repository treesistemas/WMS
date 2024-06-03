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
    public partial class FrmEnderecoProduto : Form
    {
        public FrmEnderecoProduto()
        {
            InitializeComponent();
        }

        private void FrmEnderecoProduto_Load(object sender, EventArgs e)
        {
            //GerarRelatorio(6574);
        }

        public void GerarRelatorio(string empresa, int idProduto)
        {
            try
            {
                //Instância o relatório
                RelEnderecoProduto produto = new RelEnderecoProduto();
                ReportDocument produtoPicking;
                ReportDocument produtoPulmao;

                //Instância a camada de Negocios
                EstoqueNegocios estoqueNegocios = new EstoqueNegocios();

                //Instância a camada de objêto
                RelProdutoCollection produtoCollection = new RelProdutoCollection();
                //Passa os dados da pesquisa
                produtoCollection = estoqueNegocios.PesqRelatorioProduto(empresa, idProduto) ;

                if (produtoCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    produto.Database.Tables["Produto"].SetDataSource(produtoCollection);

                    //Instância a camada de objêto
                    RelEnderecoProdutoCollection pickingCollection = new RelEnderecoProdutoCollection();
                    //Passa os dados da pesquisa
                    pickingCollection = estoqueNegocios.PesqRelatorioPicking(empresa, idProduto);

                    

                    if (pickingCollection.Count > 0)
                    {
                        //Instância o relatório
                        produtoPicking = produto.OpenSubreport("RelEnderecoProdutoPicking.rpt");
                        //Passa os dados para o dataset do relatório
                        produtoPicking.Database.Tables["Endereco"].SetDataSource(pickingCollection);
                    }                    

                    
                    //Instância a camada de objêto
                    RelEnderecoProdutoCollection pulmaoCollection = new RelEnderecoProdutoCollection();
                    //Passa os dados da pesquisa
                    pulmaoCollection = estoqueNegocios.PesqRelatorioPulmao(empresa, idProduto);

                    if (pulmaoCollection.Count > 0)
                    {
                        //Instância o relatório
                        produtoPulmao = produto.OpenSubreport("RelEnderecoProdutoPulmao.rpt");
                        //Passa os dados para o dataset do relatório
                        produtoPulmao.Database.Tables["Endereco"].SetDataSource(pulmaoCollection);
                    }

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
                MessageBox.Show("Ocorreu um erro ao gerar o relatório de endereços! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


       
    }
}
