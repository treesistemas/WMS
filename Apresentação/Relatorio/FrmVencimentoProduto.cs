using CrystalDecisions.CrystalReports.Engine;
using Negocios;
using ObjetoTransferencia;
using System;
using System.Windows.Forms;

namespace Wms.Relatorio
{
    public partial class FrmVencimentoProduto : Form
    {
        public FrmVencimentoProduto()
        {
            InitializeComponent();
        }

        private void FrmVencimentoProduto_Load(object sender, EventArgs e)
        {

        }

        //Gera o relatório analítico
        public void RelatorioAnalitico(string codFornecedor, string codProduto, string dataInicial, string dataFinal, bool picking, bool pulmao, string status)
        {
            try
            {
                //Instância o relatório
                RelVencimentoProduto vencimentoProduto = new RelVencimentoProduto();
                ReportDocument produtoPulmao;
                ReportDocument produtoPicking;

                //Instância a camada de Negocios
                VencimentoProdutoNegocios vencimentoNegocios = new VencimentoProdutoNegocios();
                //Instância a camada de objêto
                ConsultaEstoqueCollection pesquisa = new ConsultaEstoqueCollection();

                if (codFornecedor != string.Empty)
                {
                    //Passa os dados da pesquisa
                    pesquisa = vencimentoNegocios.PesqFornecedor(codFornecedor, dataInicial, dataFinal);
                }

                if (codProduto != string.Empty)
                {
                    //Passa os dados da pesquisa
                    pesquisa = vencimentoNegocios.PesqProduto(codProduto, dataInicial, dataFinal);
                }

                if (codFornecedor == string.Empty && codProduto == string.Empty)
                {
                    ConsultaEstoque estoque = new ConsultaEstoque();
                    estoque.dataInicial = Convert.ToDateTime(dataInicial);
                    estoque.dataFinal = Convert.ToDateTime(dataFinal);
                    //Passa os dados da pesquisa
                    pesquisa.Add(estoque);
                }

                //Passa os dados para o dataset do relatório
                vencimentoProduto.Database.Tables["Estoque"].SetDataSource(pesquisa);

                if (picking == true)
                {
                    //Instância a camada de objêto
                    ConsultaEstoqueCollection pickingCollection = new ConsultaEstoqueCollection();
                    //Passa os dados da pesquisa
                    pickingCollection = vencimentoNegocios.PesqVencimentoPickingAnalitico(codFornecedor, codProduto,  dataInicial, dataFinal);

                    if (pickingCollection.Count > 0)
                    {                        
                        //Instância o relatório
                        produtoPicking = vencimentoProduto.OpenSubreport("RelVencimentoPicking.rpt");

                        //Passa os dados para o dataset do subrelatório
                        produtoPicking.Database.Tables["EstoquePicking"].SetDataSource(pickingCollection);
                       
                    }
                }

                
                if (pulmao == true)
                {
                    //Instância a camada de objêto
                    ConsultaEstoqueCollection pulmaoCollection = new ConsultaEstoqueCollection();
                    //Passa os dados da pesquisa
                    pulmaoCollection = vencimentoNegocios.PesqVencimentoPulmaoAnalitico(codFornecedor, codProduto, dataInicial, dataFinal, status);

                    if (pulmaoCollection.Count > 0)
                    {
                        //Instância o relatório
                        produtoPulmao = vencimentoProduto.OpenSubreport("RelVencimentoPulmao.rpt");
                        //Passa os dados para o dataset do subrelatório
                        produtoPulmao.Database.Tables["EstoquePulmao"].SetDataSource(pulmaoCollection);

                    }
                }               

                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = vencimentoProduto;
                crystalReportViewer.RefreshReport();
                Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(""+ex);
            }

        }


        public void RelatorioSintetico(string codFornecedor, string codProduto, string dataInicial, string dataFinal, bool picking, bool pulmao, string status)
        {
            try
            {
                //Instância o relatório
                RelVencimentoProduto vencimentoProduto = new RelVencimentoProduto();
                ReportDocument produtoPulmao;
                ReportDocument produtoPicking;

                //Instância a camada de Negocios
                VencimentoProdutoNegocios vencimentoNegocios = new VencimentoProdutoNegocios();
                //Instância a camada de objêto
                ConsultaEstoqueCollection pesquisa = new ConsultaEstoqueCollection();

                if (codFornecedor != string.Empty)
                {
                    //Passa os dados da pesquisa
                    pesquisa = vencimentoNegocios.PesqFornecedor(codFornecedor, dataInicial, dataFinal);
                }

                if (codProduto != string.Empty)
                {
                    //Passa os dados da pesquisa
                    pesquisa = vencimentoNegocios.PesqProduto(codProduto, dataInicial, dataFinal);
                }

                //Passa os dados para o dataset do relatório
                vencimentoProduto.Database.Tables["Estoque"].SetDataSource(pesquisa);

                if (picking == true)
                {
                    //Instância a camada de objêto
                    ConsultaEstoqueCollection pickingCollection = new ConsultaEstoqueCollection();
                    //Passa os dados da pesquisa
                    pickingCollection = vencimentoNegocios.PesqVencimentoPickingSintetico(codFornecedor, codProduto, dataInicial, dataFinal);

                    if (pickingCollection.Count > 0)
                    {
                        //Instância o relatório
                        produtoPicking = vencimentoProduto.OpenSubreport("RelVencimentoPicking.rpt");

                        //Passa os dados para o dataset do subrelatório
                        produtoPicking.Database.Tables["EstoquePicking"].SetDataSource(pickingCollection);

                    }
                }

                if (pulmao == true)
                {
                    //Instância a camada de objêto
                    ConsultaEstoqueCollection pulmaoCollection = new ConsultaEstoqueCollection();
                    //Passa os dados da pesquisa
                   // pulmaoCollection = vencimentoNegocios.PesqVencimentoPulmaoSintetico(codFornecedor, codProduto, dataInicial, dataFinal, status);

                    if (pulmaoCollection.Count > 0)
                    {
                        //Instância o relatório
                        produtoPulmao = vencimentoProduto.OpenSubreport("RelVencimentoPulmao.rpt");
                        //Passa os dados para o dataset do subrelatório
                        produtoPulmao.Database.Tables["Estoque"].SetDataSource(pulmaoCollection);

                    }
                }

                crystalReportViewer.ReportSource = null;
                crystalReportViewer.ReportSource = vencimentoProduto;
                crystalReportViewer.RefreshReport();
                Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }

        }




    }
}
