using CrystalDecisions.CrystalReports.Engine;
using Negocios;
using Negocios.Inventario;
using ObjetoTransferencia.Impressao;
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
    public partial class FrmEnderecoProdutoInventario : Form
    {
        public FrmEnderecoProdutoInventario()
        {
            InitializeComponent();
        }

        private void FrmEnderecoProdutoInventario_Load(object sender, EventArgs e)
        {

        }


        public void GerarRelatorio(int codInventario, int[] idProduto)
        {
            try
            {
                //Instância o relatório
                RelEnderecoProdutoInventario endereco = new RelEnderecoProdutoInventario();
                ReportDocument produtoPicking;
                ReportDocument produtoPulmao;

                //Instância a camada de Negocios
                impressaoContagensNegocios enderecoNegocios = new impressaoContagensNegocios();

                //Instância a camada de objêto
                RelItemInventarioCollection produtoCollection = new RelItemInventarioCollection();

                for (int i = 0; idProduto.Count() > i; i++)
                {
                    //Instância a camada de objêto
                    RelItemInventario produto = new RelItemInventario();
                    //Passa os dados da pesquisa
                    produto = enderecoNegocios.PesqRelatorioEnderecoProduto(codInventario, idProduto[i]);

                    produtoCollection.Add(produto);
                }

                if (produtoCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    endereco.Database.Tables["Contagem"].SetDataSource(produtoCollection);
                   
                    //Instância a camada de objêto
                    RelItemInventarioCollection pickingCollection = new RelItemInventarioCollection();

                    for (int i = 0; idProduto.Count() > i; i++)
                    {
                        //Instância a camada de objêto
                        RelItemInventarioCollection endCollection = new RelItemInventarioCollection();
                        //Passa os dados da pesquisa
                        endCollection = enderecoNegocios.PesqRelatorioEnderecoPicking(codInventario, idProduto[i]);

                        //Adiciona o resultado a coleção
                        pickingCollection.AddRange(endCollection);
                    }

                    if (pickingCollection.Count > 0)
                    {
                        //Instância o relatório
                        produtoPicking = endereco.OpenSubreport("RelEnderecoProdutoPickingInventario.rpt");
                        //Passa os dados para o dataset do relatório
                        produtoPicking.Database.Tables["Contagem"].SetDataSource(pickingCollection);
                    }


                    //Instância a camada de objêto
                    RelItemInventarioCollection pulmaoCollection = new RelItemInventarioCollection();

                    for (int i = 0; idProduto.Count() > i; i++)
                    {
                        //Instância a camada de objêto
                        RelItemInventarioCollection endCollection = new RelItemInventarioCollection();
                        //Passa os dados da pesquisa
                        endCollection = enderecoNegocios.PesqRelatorioEnderecoPulmao(codInventario, idProduto[i]);

                        //Adiciona o resultado a coleção
                        pulmaoCollection.AddRange(endCollection);
                    }

                    if (pulmaoCollection.Count > 0)
                    {
                        //Instância o relatório
                        produtoPulmao = endereco.OpenSubreport("RelEnderecoProdutoPulmaoInventario.rpt");
                        //Passa os dados para o dataset do relatório
                        produtoPulmao.Database.Tables["Contagem"].SetDataSource(pulmaoCollection);
                    }
                   
                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = endereco;
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
