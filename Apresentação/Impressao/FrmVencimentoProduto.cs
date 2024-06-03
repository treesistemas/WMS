using ClosedXML.Excel;
using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Aspose.Cells;
using Aspose.Cells.Charts;
using System.Linq;
using Spire.Xls.AI;
using System.Threading;

namespace Wms
{
    public partial class FrmVencimentoProduto : Form
    {
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public string empresa;
        public List<Empresa> empresaCollection;

        public FrmVencimentoProduto()
        {
            InitializeComponent();
        }

        private void FrmVencimentoProduto_Load(object sender, EventArgs e)
        {
            if (empresaCollection != null)
            {
                //Preenche o combobox região
                empresaCollection.ForEach(n => cmbEmpresa.Items.Add(n.siglaEmpresa));
                //Seleciona a primeira empresa
                cmbEmpresa.SelectedIndex = 0;

                //Verifica se existe mais de uma empresa
                if (empresaCollection[0].multiEmpresa == false)
                {
                    cmbEmpresa.Enabled = false;

                }
                else
                {
                    cmbEmpresa.Enabled = true;
                }
            }
        }

        //Changed
        private void txtFornecedor_TextChanged(object sender, EventArgs e)
        {
            if (txtFornecedor.TextLength == 0)
            {
                lblFornecedor.Text = "-";
            }
        }

        private void txtProduto_TextChanged(object sender, EventArgs e)
        {
            if (txtProduto.TextLength == 0)
            {
                lblProduto.Text = "-";
            }
        }

        private void txtPrazo_TextChanged(object sender, EventArgs e)
        {
            if (txtPrazo.TextLength == 0)
            {
                dtmFinal.Value = DateTime.Now;
            }
            else
            {
                dtmFinal.Value = DateTime.Now;
                dtmFinal.Value = dtmFinal.Value.AddDays(Convert.ToInt32(txtPrazo.Text));
            }
        }

        private void chkPicking_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPicking.Checked == true)
            {
                chkPicking.BackColor = Color.MediumSeaGreen;
                chkPicking.ForeColor = Color.White;
            }
            else
            {
                chkPicking.BackColor = Color.White;
                chkPicking.ForeColor = Color.Black;
            }
        }

        private void chkPulmao_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPulmao.Checked == true)
            {
                chkPulmao.BackColor = Color.Orange;
                chkPulmao.ForeColor = Color.White;
            }
            else
            {
                chkPulmao.BackColor = Color.White;
                chkPulmao.ForeColor = Color.Black;
            }
        }

        private void rbtAnalitico_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtAnalitico.Checked == true)
            {
                rbtAnalitico.BackColor = Color.DimGray;
                rbtAnalitico.ForeColor = Color.White;
            }
            else
            {
                rbtAnalitico.BackColor = Color.White;
                rbtAnalitico.ForeColor = Color.Black;
            }
        }

        private void rbtSintetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSintetico.Checked == true)
            {
                rbtSintetico.BackColor = Color.DimGray;
                rbtSintetico.ForeColor = Color.White;
            }
            else
            {
                rbtSintetico.BackColor = Color.White;
                rbtSintetico.ForeColor = Color.Black;
            }
        }

        //KeyDow
        private void txtFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqFornecedor frameForn = new FrmPesqFornecedor();

                //Recebe as informações
                if (frameForn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe os valores novos
                    txtFornecedor.Text = frameForn.codFornecedor;
                    lblFornecedor.Text = frameForn.nmFornecedor;
                }
            }
        }

        private void txtProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqProduto frameProduto = new FrmPesqProduto();
                //Adiciona o nome da empresa
                frameProduto.empresa = cmbEmpresa.Text;

                //Recebe as informações
                if (frameProduto.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe os valores novos
                    txtProduto.Text = frameProduto.codProduto;
                    lblProduto.Text = frameProduto.descProduto;
                }
            }
        }

        //KeyPress
        private void txtFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!txtFornecedor.Text.Equals(""))
                {
                    PesqFornecedor();
                }
                else
                {
                    txtProduto.Focus();
                }
            }
        }

        private void txtProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!txtProduto.Text.Equals(""))
                {
                    PesqProduto();
                }
                else
                {
                    txtPrazo.Focus();
                }
            }
        }

        private void txtPrazo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmInicial.Focus();
            }
        }

        private void dtmInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmFinal.Focus();
            }
        }

        private void dtmFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAnalisar.Focus();
            }
        }

        private void btnAnalisar_Click(object sender, EventArgs e)
        {
            //Gera a impressão
            GerarImpressao();

            if (chkEmail.Checked == true)
            {
                //Gera o relatório
                Thread thread = new Thread(GerarArquivoVencimento);
                thread.Start(); //inicializa
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }


        //Pesquisa o fornecedor
        private void PesqFornecedor()
        {
            try
            {

                //Instância o negocios
                FornecedorNegocios pesqFornecedorNegocios = new FornecedorNegocios();
                //Instância a coleçãO
                FornecedorCollection pesqFornecedorCollection = new FornecedorCollection();
                //A coleção recebe o resultado da consulta
                pesqFornecedorCollection = pesqFornecedorNegocios.pesqFornecedor(cmbEmpresa.Text, "", Convert.ToInt32(txtFornecedor.Text));

                if (pesqFornecedorCollection.Count > 0)
                {
                    lblFornecedor.Text = pesqFornecedorCollection[0].nomeFornecedor;

                    txtProduto.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum fornecedor encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o produto
        private void PesqProduto()
        {
            try
            {
                //Instância o negocios
                ProdutoNegocios produtoNegocios = new ProdutoNegocios();
                //Instância a coleção
                ProdutoCollection produtoCollection = new ProdutoCollection();
                //A coleção recebe o resultado da consulta
                produtoCollection = produtoNegocios.PesqProduto(cmbEmpresa.Text, "", txtProduto.Text);

                if (produtoCollection.Count > 0)
                {
                    lblProduto.Text = produtoCollection[0].descProduto;

                    txtPrazo.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum SKU encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Analítico
        private void GerarImpressao()
        {
            try
            {
                if (txtFornecedor.Text != string.Empty && txtProduto.Text != string.Empty)
                {
                    MessageBox.Show("Selecione a pesquisa somente por fornecedor ou por produto!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Garante que o processo seja executado da thread que foi iniciado
                    Invoke((MethodInvoker)delegate ()
                    {

                        //Instância o relatório
                        Wms.Relatorio.FrmVencimentoProduto frame = new Wms.Relatorio.FrmVencimentoProduto();

                        if (rbtAnalitico.Checked == true)
                        {
                            frame.RelatorioAnalitico(txtFornecedor.Text, txtProduto.Text, dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(),
                                            chkPicking.Checked, chkPulmao.Checked, cmbStatus.Text);
                        }

                        if (rbtSintetico.Checked == true)
                        {

                        }
                        //Exibe o relatório
                        frame.Show();
                    });
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o relatório de vencimento! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //Relatório sintético de vencimento
        private void GerarArquivoVencimento()
        {
            try
            {

                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    //Caminho da área de trabalho
                    string caminho = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    //caminho da pasta
                    DirectoryInfo diretorio = null;

                    //Verifica se a pasta existe
                    if (Directory.Exists(caminho))
                    {
                        //Cria a pasta
                        diretorio = Directory.CreateDirectory(caminho + "\\WMS - VENCIMENTO " + empresa);
                    }

                    //Nome do arquivo
                    string nomeArquivo = "VENCIMENTO " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss").Replace(":",".") + ".xlsx";

                    //Instância a camada de Negocios
                    VencimentoProdutoNegocios vencimentoNegocios = new VencimentoProdutoNegocios();

                    ConsultaEstoqueCollection itemCollection = new ConsultaEstoqueCollection();

                    itemCollection = vencimentoNegocios.PesqVencimentoSintetico(txtFornecedor.Text, txtProduto.Text, dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString());

                    //Cria um novo Excel Workbook 
                    Workbook workbook = new Workbook();
                    //Adiciona a primeira aba como padrão
                    Worksheet worksheet = workbook.Worksheets[0];
                    //Renomeia a aba
                    worksheet.Name = "VENCIMENTO";
                    //Get worksheet cells collection from the sheet
                    Cells coluna = worksheet.Cells;
                    coluna["A1"].Value = "Nº";
                    coluna["B1"].Value = "CÓDIGO";
                    coluna["C1"].Value = "DESCRIÇÃO";
                    coluna["D1"].Value = "ESTOQUE";
                    coluna["E1"].Value = "UND";
                    coluna["F1"].Value = "VENCIMENTO";
                    coluna["G1"].Value = "PREÇO";
                    coluna["H1"].Value = "TOTAL";
                    coluna["I1"].Value = "FORNECEDOR";

                    //Redimenciona a celula
                    worksheet.Cells.SetColumnWidth(0, 5.0);
                    worksheet.Cells.SetColumnWidth(1, 10.0);
                    worksheet.Cells.SetColumnWidth(2, 50.0);
                    worksheet.Cells.SetColumnWidth(3, 10.0);
                    worksheet.Cells.SetColumnWidth(4, 5.0);
                    worksheet.Cells.SetColumnWidth(5, 15.0);
                    worksheet.Cells.SetColumnWidth(6, 10.0);
                    worksheet.Cells.SetColumnWidth(7, 15.0);
                    worksheet.Cells.SetColumnWidth(8, 40.0);

                    //Variável de controle
                    int linha = 1, numeracao = 1;
                    //Pula a linha das colunas
                    linha++;

                    // primeira linha é o cabecalho
                    foreach (var item in itemCollection)
                    {
                        coluna["A" + linha].Value = numeracao;
                        coluna["B" + linha].Value = item.codProduto;
                        coluna["C" + linha].Value = item.descProduto;
                        coluna["D" + linha].Value = item.estoque;
                        coluna["E" + linha].Value = item.unidadePicking;
                        coluna["F" + linha].Value = "" + item.vencimento.ToString("dd/MM/yyyy");
                        coluna["G" + linha].Value = string.Format("{0:C}", item.preco);
                        coluna["H" + linha].Value = string.Format("{0:C}", item.estoque * item.preco);
                        coluna["I" + linha].Value = item.nomeFornecedor;
                        linha++;
                        numeracao++;
                    }

                    //Adiciona a aba vencimento
                    workbook.Worksheets.Add("POR FORNECEDOR");
                    //Adiciona a segunda aba como padrão
                    Worksheet worksheetGrafico = workbook.Worksheets[1];
                    //Get worksheet cells collection from the sheet
                    Cells colunaGrafico = worksheetGrafico.Cells;
                    //Adiciona a coluna Fornecedor
                    colunaGrafico["A1"].PutValue("Nº");
                    colunaGrafico["B1"].PutValue("FORNECEDOR");
                    colunaGrafico["C1"].PutValue("VALOR");

                    string fornecedor = null;

                    int contForncedor = 0, linhaGrafico = 2;

                    ConsultaEstoqueCollection fornecedorCollection = new ConsultaEstoqueCollection();

                    //Adiciona as colunas
                    foreach (var item in itemCollection)
                    {
                        if (contForncedor == 0)
                        {
                            //Pesquisa o fornecedor na lista e soma os valores
                            List<ConsultaEstoque> forn = itemCollection.FindAll(delegate (ConsultaEstoque n) { return n.nomeFornecedor == item.nomeFornecedor; });
                            //Soma os valores
                            item.preco = forn.Sum(n => n.estoque * n.preco);
                            //Adiciono a coleção
                            fornecedorCollection.Add(item);
                            //Variáveis recebem os valores
                            fornecedor = item.nomeFornecedor;
                            //Controla para não entrar mais
                            contForncedor++;
                        }

                        //Verifica se o fornecedor é diferente do atual já selecionado
                        if (fornecedor != item.nomeFornecedor)
                        {
                            //Pesquisa o fornecedor na lista e soma os valores
                            List<ConsultaEstoque> forn = itemCollection.FindAll(delegate (ConsultaEstoque n) { return n.nomeFornecedor == item.nomeFornecedor; });
                            //Soma os valores
                            //Soma os valores
                            item.preco = forn.Sum(n => n.estoque * n.preco);
                            //Adiciono a coleção
                            fornecedorCollection.Add(item);
                            //Variável recebe o fornecedor para controle
                            fornecedor = item.nomeFornecedor;
                        }


                    }

                    foreach (var item in fornecedorCollection.OrderByDescending(n => n.preco))
                    {
                        //Adiciona o valor
                        colunaGrafico["A" + linhaGrafico].Value = linhaGrafico;
                        colunaGrafico["B" + linhaGrafico].Value = item.nomeFornecedor;
                        colunaGrafico["C" + linhaGrafico].Value = item.preco;
                        //Variáveis recebem os valores
                        fornecedor = item.nomeFornecedor;
                        //Controla para não entrar mais
                        contForncedor++;
                        linhaGrafico++;

                    }

                    //Redimenciona a celula
                    worksheetGrafico.Cells.SetColumnWidth(0, 5.0);
                    worksheetGrafico.Cells.SetColumnWidth(1, 60.0);
                    worksheetGrafico.Cells.SetColumnWidth(2, 10.0);

                    if (linhaGrafico > 11)
                    {
                        linhaGrafico = 11;
                    }

                    //Create or add excel pie chart
                    int chart_Index = 0;
                    chart_Index = worksheetGrafico.Charts.Add(ChartType.Pie, 0, 4, 40, 13);
                    Chart WorksheetChart = worksheetGrafico.Charts[chart_Index];

                    //Set the data series and category data for the pie chart 
                    WorksheetChart.NSeries.Add("C2:C" + (linhaGrafico), true);
                    WorksheetChart.NSeries.CategoryData = "B2:B" + (linhaGrafico);

                    //Set chart title properties
                    WorksheetChart.Title.Text = "VENCIMENTO POR FORNECEDOR";
                    WorksheetChart.Title.Font.Color = Color.Red;
                    WorksheetChart.Title.Font.IsBold = true;
                    WorksheetChart.Title.Font.Size = 11;

                    //Set the data labels attributes of the pie chart slices
                    DataLabels data_labels;
                    for (int i = 0; i < WorksheetChart.NSeries.Count; i++)
                    {
                        data_labels = WorksheetChart.NSeries[i].DataLabels;
                        data_labels.ShowValue = true;
                        data_labels.ShowPercentage = true;

                    }

                    //Save the excel file containing the chart
                    workbook.Save(caminho+"\\"+diretorio + "\\" + nomeArquivo);
                    //Exclui aba do excel
                    ExcluirAbaExcel(caminho + "\\"+diretorio + "\\" + nomeArquivo);

                });


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ExcluirAbaExcel(string arquivo)
        {
            var xls = new XLWorkbook(arquivo);
            xls.Worksheets.First(w => w.Name == "Evaluation Warning").Delete();

            xls.SaveAs(arquivo);
            
        }


    }
}
