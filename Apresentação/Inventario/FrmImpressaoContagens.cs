using DocumentFormat.OpenXml.Spreadsheet;
using Negocios;
using Negocios.Impressao;
using Negocios.Inventario;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilitarios;
using Wms.Relatorio;
using Wms.Relatorio.DataSet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Color = System.Drawing.Color;

namespace Wms.Inventario
{
    public partial class FrmImpressaoContagens : Form
    {

        public List<Empresa> empresaCollection;


        private int[] codInventario;
        //Array com id
        private int[] regiao;
        private int[] rua;
        private int[] estacao;

        public FrmImpressaoContagens()
        {
            InitializeComponent();
        }

        private void FrmImpressaoContagens_Load(object sender, EventArgs e)
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


        private void rbtContagem_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtContagem.Checked == true)
            {
                rbtContagem.BackColor = Color.DimGray;
                rbtContagem.ForeColor = Color.White;
                rbtContagem3.BackColor = Color.White;
                rbtContagem3.ForeColor = Color.Black;

                rbtCriticaContagens.BackColor = Color.White;
                rbtCriticaContagens.ForeColor = Color.Black;

                rbtSemContagem.BackColor = Color.White;
                rbtSemContagem.ForeColor = Color.Black;

                rbtSemContagem2.BackColor = Color.White;
                rbtSemContagem2.ForeColor = Color.Black;
            }

        }

        private void rbtContagem3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtContagem3.Checked == true)
            {
                rbtContagem3.BackColor = Color.DimGray;
                rbtContagem3.ForeColor = Color.White;
                rbtContagem.BackColor = Color.White;
                rbtContagem.ForeColor = Color.Black;

                rbtCriticaContagens.BackColor = Color.White;
                rbtCriticaContagens.ForeColor = Color.Black;

                rbtSemContagem.BackColor = Color.White;
                rbtSemContagem.ForeColor = Color.Black;

                rbtSemContagem2.BackColor = Color.White;
                rbtSemContagem2.ForeColor = Color.Black;
            }

        }

        private void rbtCriticaContagens_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtCriticaContagens.Checked == true)
            {
                rbtCriticaContagens.BackColor = Color.DimGray;
                rbtCriticaContagens.ForeColor = Color.White;
                rbtContagem.BackColor = Color.White;
                rbtContagem.ForeColor = Color.Black;

                rbtContagem3.BackColor = Color.White;
                rbtContagem3.ForeColor = Color.Black;

                rbtSemContagem.BackColor = Color.White;
                rbtSemContagem.ForeColor = Color.Black;

                rbtSemContagem2.BackColor = Color.White;
                rbtSemContagem2.ForeColor = Color.Black;
            }
        }

        private void rbtSemContagem_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSemContagem.Checked == true)
            {
                rbtSemContagem.BackColor = Color.DimGray;
                rbtSemContagem.ForeColor = Color.White;

                rbtContagem.BackColor = Color.White;
                rbtContagem.ForeColor = Color.Black;

                rbtContagem3.BackColor = Color.White;
                rbtContagem3.ForeColor = Color.Black;

                rbtCriticaContagens.BackColor = Color.White;
                rbtCriticaContagens.ForeColor = Color.Black;

                rbtSemContagem2.BackColor = Color.White;
                rbtSemContagem2.ForeColor = Color.Black;
            }
        }

        private void rbtSemContagem2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSemContagem2.Checked == true)
            {
                rbtSemContagem2.BackColor = Color.DimGray;
                rbtSemContagem2.ForeColor = Color.White;

                rbtContagem.BackColor = Color.White;
                rbtContagem.ForeColor = Color.Black;

                rbtContagem3.BackColor = Color.White;
                rbtContagem3.ForeColor = Color.Black;

                rbtCriticaContagens.BackColor = Color.White;
                rbtCriticaContagens.ForeColor = Color.Black;

                rbtSemContagem.BackColor = Color.White;
                rbtSemContagem.ForeColor = Color.Black;
            }
        }

        private void txtProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Pesquisa o produto no flow rack
                PesqProduto();
            }
        }

        private void mniRemover_Click(object sender, EventArgs e)
        {
            if (gridProduto.Rows.Count > 0)
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridProduto.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;
                //Remove a linha seleciona
                gridProduto.Rows.RemoveAt(indice);

                int numeracao = 1;

                //Verifica a numeração novamente
                for (int i = 0; gridProduto.Rows.Count > i; i++)
                {
                    gridProduto.Rows[i].Cells[0].Value = numeracao;

                    numeracao++;
                }
            }

        }

        private void cbmInventario_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbInventario.Items.Count == 0)
            {
                PesqInventario();
            }
        }

        private void cmbEstacao_MouseClick(object sender, MouseEventArgs e)
        {
            //Pesquisa a estação
            PesqEstacao();
        }

        private void cmbRegiao_MouseClick(object sender, MouseEventArgs e)
        {
            PesqRegiao();
        }

        private void cbmInventario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInventario.Items.Count > 0)
            {
                cmbTipo.Enabled = true;
            }
            else
            {
                cmbTipo.Enabled = false;
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.Text.Equals("PICKING"))
            {
                rbtCaixa.Enabled = true;
                rbtFlowRack.Enabled = true;
                cmbRegiao.Enabled = true;
                cmbRua.Enabled = true;
                cmbLado.Enabled = true;
            }
            else if (cmbTipo.Text.Equals("PULMAO"))
            {
                cmbRegiao.Enabled = true;
                cmbRua.Enabled = true;
                cmbLado.Enabled = true;
                cmbEstacao.Enabled = false;
                rbtCaixa.Enabled = false;
                rbtFlowRack.Enabled = false;

            }

        }

        private void rbtCaixa_CheckedChanged(object sender, EventArgs e)
        {
            cmbEstacao.Items.Clear();
            cmbRegiao.Items.Clear();
            cmbRua.Items.Clear();
            cmbBloco.Items.Clear();
            cmbEstacao.Text = "SELECIONE";
            cmbRegiao.Text = "SEL...";
            cmbRua.Text = "SEL...";
            cmbBloco.Text = "SEL...";
            cmbLado.Text = "SELECIONE";
        }

        private void rbtFlowRack_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtFlowRack.Checked == true)
            {
                cmbEstacao.Enabled = true;
                cmbBloco.Enabled = true;
            }
            else
            {
                cmbEstacao.Enabled = false;
                cmbBloco.Enabled = false;
            }
        }

        private void cmbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa a rua
            PesqRua();
        }

        private void cmbRua_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa o bloco
            PesqBloco();
        }

        private void btnAnalisar_Click(object sender, EventArgs e)
        {
            if (gridProduto.Rows.Count > 0)
            {
                ImpressaoEnderecoProduto();
            }
            else
            {

                if (rbtSemContagem.Checked == true || rbtSemContagem2.Checked == true)
                {
                    //Gera o relatório
                    Thread thread = new Thread(ImpressaoSemContagem);
                    thread.Start(); //inicializa
                }
                else if (rbtCriticaContagens.Checked == true)
                {
                    //Gera o relatório
                    Thread thread = new Thread(ImpressaoCriticaContagem);
                    thread.Start(); //inicializa
                }
                else if (rbtVolumeSemContagem.Checked == true)
                {
                    //Gera o relatório
                    Thread thread = new Thread(ImpressaoVolumeSemContagem);
                    thread.Start(); //inicializa
                }
                else
                {
                    //Gera o relatório
                    Thread thread = new Thread(ImpressaoContagem);
                    thread.Start(); //inicializa
                }
            }
        }

        //Pesquisa o inventário
        private void PesqInventario()
        {
            try
            {
                cmbInventario.Items.Add("SELECIONE");
                //Instância a coleção
                InventarioCollection inventarioCollection = new InventarioCollection();
                //Instância o negocios
                impressaoContagensNegocios inventarioNegocios = new impressaoContagensNegocios();
                //Limpa o combobox regiao
                cmbInventario.Items.Clear();
                //Preenche a coleção com apesquisa
                inventarioCollection = inventarioNegocios.PesqInventario();
                //Preenche o combobox região
                inventarioCollection.ForEach(n => cmbInventario.Items.Add(n.descInventario));

                //Define o tamanho do array para o combobox
                codInventario = new int[inventarioCollection.Count];

                for (int i = 0; i < inventarioCollection.Count; i++)
                {
                    //Preenche o array combobox
                    codInventario[i] = inventarioCollection[i].codInventario;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa as estações
        private void PesqEstacao()
        {
            try
            {
                //Limpa os campos
                cmbEstacao.Items.Clear();
                cmbRegiao.Items.Clear();
                cmbRua.Items.Clear();
                cmbBloco.Items.Clear();
                //Adiciona o texto
                cmbRegiao.Text = "SEL...";
                cmbRua.Text = "SEL...";
                cmbBloco.Text = "SEL...";
                cmbLado.Text = "SELECIONE";

                //Instância a camada de negocios
                impressaoContagensNegocios separacaoNegocios = new impressaoContagensNegocios();
                //Instância a camada de objêtos (coleção)
                EstacaoCollection estacaoCollection = new EstacaoCollection();
                //A coleção recebe os objêtos encontrados
                estacaoCollection = separacaoNegocios.PesqEstacao();

                if (estacaoCollection.Count > 0)
                {
                    //Preenche o combobox região
                    estacaoCollection.ForEach(n => cmbEstacao.Items.Add(n.descEstacao));

                    //Define o tamanho do array
                    estacao = new int[estacaoCollection.Count];

                    for (int i = 0; i < estacaoCollection.Count; i++)
                    {
                        //Preenche o array com os códigos
                        estacao[i] = estacaoCollection[i].codEstacao;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa região
        private void PesqRegiao()
        {
            try
            {
                cmbRegiao.Text = "SEL...";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Limpa o combobox regiao
                cmbRegiao.Items.Clear();
                //Preenche a coleção com apesquisa
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRegiao(cmbEmpresa.Text);
                //Preenche o combobox região
                gerarEnderecoCollection.ForEach(n => cmbRegiao.Items.Add(n.numeroRegiao));

                //Define o tamanho do array para o combobox
                regiao = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array combobox
                    regiao[i] = gerarEnderecoCollection[i].codRegiao;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa rua combobox
        private void PesqRua()
        {
            try
            {
                //Limpa o combobox
                cmbRua.Items.Clear();
                cmbBloco.Items.Clear();
                //Adiciona o texto
                cmbRua.Text = "SEL...";
                cmbBloco.Text = "SEL...";
                cmbLado.Text = "TODOS";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa as ruas da região selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRua(regiao[cmbRegiao.SelectedIndex]);
                //Preenche o combobox rua
                gerarEnderecoCollection.ForEach(n => cmbRua.Items.Add(n.numeroRua));

                rua = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    rua[i] = gerarEnderecoCollection[i].codRua;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o Bloco
        private void PesqBloco()
        {
            try
            {
                //Limpa o combobox rua
                cmbBloco.Items.Clear();
                //Adiciona o texto
                cmbBloco.Text = "SEL...";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa os bloco da rua selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqBloco(rua[cmbRua.SelectedIndex]);
                //Preenche o combobox bloco
                gerarEnderecoCollection.ForEach(n => cmbBloco.Items.Add(n.numeroBloco));

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o produto no flow rack
        private void PesqProduto()
        {
            try
            {
                //Instância a camada de negocios
                impressaoContagensNegocios produtoNegocios = new impressaoContagensNegocios();
                //Instância o objêto
                Produto produto = new Produto();
                //A coleção recebe o resultado da consulta
                produto = produtoNegocios.PesqProduto(txtProduto.Text, cmbEmpresa.Text);

                //Verifica se o produto existe
                if (produto.idProduto > 0)
                {
                    bool controle = true;
                    //Verifica se o item já existe
                    for (int i = 0; gridProduto.Rows.Count > i; i++)
                    {
                        if (Convert.ToInt32(gridProduto.Rows[i].Cells[1].Value) == produto.idProduto)
                        {
                            controle = false;
                            MessageBox.Show("Produto já adicionado!");
                            break;
                        }
                    }

                    if (controle == true)
                    {
                        //Insere o cadastro no grid
                        gridProduto.Rows.Add((gridProduto.Rows.Count + 1), produto.idProduto, produto.descProduto);
                        //Seleciona a linha      
                        gridProduto.CurrentCell = gridProduto.Rows[gridProduto.Rows.Count - 1].Cells[0];
                    }

                    txtProduto.Text = string.Empty;
                    //Foca no campo
                    txtProduto.Focus();
                }
                else
                {
                    MessageBox.Show("Produto não encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Gera a impressão das contagens
        private void ImpressaoContagem()
        {
            try
            {

                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    if (cmbInventario.Text.Equals("SELECIONE") || cmbInventario.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Selecione um inventário!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (cmbTipo.Text.Equals("SELECIONE") || cmbTipo.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Selecione o tipo de endereço!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (cmbTipo.Text.Equals("PICKING") && rbtCaixa.Checked == true && cmbRegiao.Text.Equals("SEL...") || cmbTipo.Text.Equals("PICKING") && rbtCaixa.Checked == true && cmbRegiao.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Selecione uma região!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (cmbTipo.Text.Equals("PICKING") && rbtCaixa.Checked == true && cmbRua.Text.Equals("SEL...") || cmbTipo.Text.Equals("PICKING") && rbtCaixa.Checked == true && cmbRua.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Selecione uma rua!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int inventario = 0, regiao = 0, rua = 0, bloco = 0, codEstacao = 0;
                        string tipoPicking = null, contagem = null;

                        if (!(cmbInventario.Text.Equals("SELECIONE") || cmbInventario.Text.Equals(string.Empty)))
                        {
                            inventario = codInventario[cmbInventario.SelectedIndex];
                        }

                        if (cmbTipo.Text.Equals("PICKING") && rbtCaixa.Checked == true)
                        {
                            tipoPicking = "CAIXA";
                        }
                        else if (cmbTipo.Text.Equals("PICKING") && rbtFlowRack.Checked == true)
                        {
                            if (!cmbEstacao.Text.Equals("SEL...") || !cmbEstacao.Text.Equals(string.Empty))
                            {
                                codEstacao = estacao[cmbEstacao.SelectedIndex];
                            }

                            if (!(cmbBloco.Text.Equals("SEL...") || cmbBloco.Text.Equals("")))
                            {
                                bloco = Convert.ToInt32(cmbBloco.Text);
                            }

                            tipoPicking = "FLOW RACK";
                        }

                        if (!cmbRegiao.Text.Equals("SEL...") || cmbRegiao.Text.Equals(string.Empty))
                        {
                            regiao = Convert.ToInt32(cmbRegiao.Text);
                        }

                        if (!cmbRua.Text.Equals("SEL...") || cmbRua.Text.Equals(string.Empty))
                        {
                            rua = Convert.ToInt32(cmbRua.Text);
                        }

                        if (rbtContagem.Checked == true)
                        {
                            contagem = "CONTAGEM";
                        }

                        if (rbtContagem3.Checked == true)
                        {
                            contagem = "CONTAGEM 3";
                        }

                        //Instância o relatório
                        FrmContagem frame = new FrmContagem();
                        //Passa o número do manifesto
                        frame.GerarRelatorio(inventario, regiao, rua, bloco, cmbTipo.Text, tipoPicking, codEstacao, cmbEstacao.Text, cmbLado.Text, contagem);

                    }
                });


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar a impressão das contagens! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Gera a impressão das críticas das contagens
        private void ImpressaoCriticaContagem()
        {
            try
            {

                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    if (cmbInventario.Text.Equals("SELECIONE") || cmbInventario.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Selecione um inventário!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (cmbTipo.Text.Equals("SELECIONE") || cmbTipo.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Selecione o tipo de endereço!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (cmbTipo.Text.Equals("PICKING") && rbtCaixa.Checked == true && cmbRegiao.Text.Equals("SEL...") || cmbTipo.Text.Equals("PICKING") && rbtCaixa.Checked == true && cmbRegiao.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Selecione uma região!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (cmbTipo.Text.Equals("PICKING") && rbtCaixa.Checked == true && cmbRua.Text.Equals("SEL...") || cmbTipo.Text.Equals("PICKING") && rbtCaixa.Checked == true && cmbRua.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Selecione uma rua!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int inventario = 0, regiao = 0, rua = 0, bloco = 0, codEstacao = 0;
                        string tipoPicking = null, contagem = null;

                        if (!(cmbInventario.Text.Equals("SELECIONE") || cmbInventario.Text.Equals(string.Empty)))
                        {
                            inventario = codInventario[cmbInventario.SelectedIndex];
                        }

                        if (rbtCaixa.Checked == true)
                        {
                            tipoPicking = "CAIXA";
                        }

                        if (rbtFlowRack.Checked == true)
                        {
                            if (!cmbEstacao.Text.Equals("SEL...") || !cmbEstacao.Text.Equals(string.Empty))
                            {
                                codEstacao = estacao[cmbEstacao.SelectedIndex];
                            }

                            if (!(cmbBloco.Text.Equals("SEL...") || cmbBloco.Text.Equals("")))
                            {
                                bloco = Convert.ToInt32(cmbBloco.Text);
                            }

                            tipoPicking = "FLOW RACK";
                        }

                        if (!cmbRegiao.Text.Equals("SEL...") || cmbRegiao.Text.Equals(string.Empty))
                        {
                            regiao = Convert.ToInt32(cmbRegiao.Text);
                        }

                        if (!cmbRua.Text.Equals("SEL...") || cmbRua.Text.Equals(string.Empty))
                        {
                            rua = Convert.ToInt32(cmbRua.Text);
                        }

                        //Instância o relatório
                        FrmCriticaContagem frame = new FrmCriticaContagem();
                        //Passa o número do manifesto
                        frame.GerarRelatorio(inventario, regiao, rua, bloco, cmbTipo.Text, tipoPicking, codEstacao, cmbEstacao.Text, cmbLado.Text, contagem);

                    }
                });


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar a impressão das contagens! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Gera a impressão das contagens
        private void ImpressaoSemContagem()
        {
            try
            {

                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    if (cmbInventario.Text.Equals("SELECIONE") || cmbInventario.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Selecione um inventário!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (cmbTipo.Text.Equals("SELECIONE") || cmbTipo.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Selecione o tipo de endereço!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        int inventario = 0;
                        string contagem = null;

                        if (!(cmbInventario.Text.Equals("SELECIONE") || cmbInventario.Text.Equals(string.Empty)))
                        {
                            inventario = codInventario[cmbInventario.SelectedIndex];
                        }

                        if (rbtSemContagem.Checked == true)
                        {
                            contagem = "1";
                        }
                        if (rbtSemContagem2.Checked == true)
                        {
                            contagem = "2";
                        }

                        //Instância o relatório
                        FrmSemContagem frame = new FrmSemContagem();
                        //Passa o número do manifesto
                        frame.GerarRelatorio(inventario, cmbTipo.Text, contagem);

                    }
                });


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar a impressão de endereços sem contagem! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Pesquisa os endereços do produto
        private void ImpressaoEnderecoProduto()
        {
            try
            {

                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    if (gridProduto.Rows.Count == 0)
                    {
                        MessageBox.Show("Insira um produto para gerar o relatório!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        int inventario = 0;

                        if (!(cmbInventario.Text.Equals("SELECIONE") || cmbInventario.Text.Equals(string.Empty)))
                        {
                            inventario = codInventario[cmbInventario.SelectedIndex];
                        }

                        //Array de id
                        int[] idProduto = new int[gridProduto.Rows.Count];

                        //Preenche o array com as informações do grid
                        for (int i = 0; i < gridProduto.Rows.Count; i++)
                        {
                            idProduto[i] = Convert.ToInt32(gridProduto.Rows[i].Cells[1].Value);
                        }

                        if (inventario == 0)
                        {
                            MessageBox.Show("Selecione o inventário!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            //Instância o relatório
                            FrmEnderecoProdutoInventario frame = new FrmEnderecoProdutoInventario();
                            //Passa o número do manifesto
                            frame.GerarRelatorio(inventario, idProduto);
                        }

                        //Limpa o grid
                        gridProduto.Rows.Clear();


                    }
                });


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar a impressão de endereços sem contagem! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Gera a impressão das contagens
        private void ImpressaoVolumeSemContagem()
        {
            try
            {
                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    if (cmbInventario.Text.Equals("SELECIONE") || cmbInventario.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Selecione um inventário!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        int inventario = 0;

                        if (!(cmbInventario.Text.Equals("SELECIONE") || cmbInventario.Text.Equals(string.Empty)))
                        {
                            inventario = codInventario[cmbInventario.SelectedIndex];
                        }

                        //Instância o relatório
                        FrmVolumeInventario frame = new FrmVolumeInventario();
                        //Passa o número do manifesto
                        frame.GerarRelatorio(inventario);

                    }
                });


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar a impressão de volume sem contagem! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
