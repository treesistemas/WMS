using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;
using Utilitarios;
using Wms.Relatorio;
using Wms.Relatorio.DataSet;

namespace Wms
{
    public partial class FrmEstoque : Form
    {
        //Instância o objeto
        BarraCollection barraCollection = new BarraCollection();
        //Instância o objeto
        EnderecoPickingCollection enderecoPickingCollection = new EnderecoPickingCollection();
        //Instância o objeto
        EnderecoPulmaoCollection enderecoPulmaoCollection = new EnderecoPulmaoCollection();
        public int codUsuario;
        public string empresa;
        public string impressora;
        //Caminho das imagens do produto
        string caminho = null;

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmEstoque()
        {
            InitializeComponent();
        }

        private void FrmEstoque_Load(object sender, EventArgs e)
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

        //Key Dow
        private void txtPesqFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqFornecedor frame = new FrmPesqFornecedor();
                //Adiciona o nome da empresa
                frame.empresa = cmbEmpresa.Text;

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe os valores novos
                    txtPesqFornecedor.Text = frame.codFornecedor;
                    txtPesqNmFornecedor.Text = frame.nmFornecedor;
                }
            }
        }

        private void txtPesqCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqProduto frame = new FrmPesqProduto();
                //Adiciona o nome da empresa
                frame.empresa = cmbEmpresa.Text;

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe os valores novos
                    txtPesqCodigo.Text = frame.codProduto;
                }
            }

        }


        //Key Press
        private void txtPesqCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtPesqCodigo.Text.Length == 0)
                {
                    //Foca no campo descrição do produto
                    txtPesqDescricao.Focus();
                }
                else
                {
                    //Pesquisa
                    PesqProduto();
                }
            }

        }

        private void txtPescDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtPesqDescricao.Text.Length == 0)
                {
                    //Foca no campo fornecedor
                    txtPesqFornecedor.Focus();
                }
                else
                {
                    //Pesquisa
                    PesqProduto();
                }
            }
        }

        private void txtPesqFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtPesqFornecedor.Text.Length == 0)
                {
                    //Foca no campo código de barra
                    txtPesqCodBarra.Focus();
                }
                else
                {
                    //Pesquisa
                    PesqProduto();
                }
            }
        }

        private void txtPesqCodBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtPesqCodBarra.Text.Length == 0)
                {
                    //Foca no campo categoria
                    cmbPesqCategoria.Focus();
                }
                else
                {
                    //Pesquisa
                    PesqProduto();
                }
            }
        }

        private void cmbPesqCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão de pesquisa
                btnPesquisar.Focus();
            }
        }

        //Changed
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (!txtCodigo.Text.Equals(""))
            {
                //exibe a imagem
                ExibirImagem();
            }
        }

        private void tabInformacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabInformacao.SelectedTab.Name.Equals("tabPage2"))
            {
                //Limpa o foco
                gridPulmao.ClearSelection();
                gridPicking.Focus();
            }
        }

        //Click
        private void cmbPesqCategoria_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbPesqCategoria.Items.Count == 0)
            {
                //Preenche o combobox categoria
                PesqCategorias();
            }
        }

        private void gridPulmao_MouseClick(object sender, MouseEventArgs e)
        {
            //Limpa o foco do picking
            gridPicking.ClearSelection();
        }

        private void gridPicking_MouseClick(object sender, MouseEventArgs e)
        {
            //Limpa o foco do pulmão
            gridPulmao.ClearSelection();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa o produto
            PesqProduto();
        }

        private void gridConsulta_MouseClick(object sender, MouseEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        private void gridConsulta_KeyDown(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        private void gridConsulta_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        private void mniEndereco_Click(object sender, EventArgs e)
        {
            //Instância as linha da tabela
            DataGridViewRow linha = gridConsulta.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;

            FrmEnderecoProduto frame = new FrmEnderecoProduto();
            frame.GerarRelatorio(cmbEmpresa.Text, Convert.ToInt32(gridConsulta.Rows[indice].Cells[0].Value));
        }

        //Menu
        private void MenuEndereco_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gridPulmao.SelectedRows.Count > 0)
            {
                mniEtiquetaPicking.Visible = false; //Oculta a impressão de etiqueta do picking
                mniEtiquetaPulmao.Visible = true; //Exibe a impressão de etiqueta do pulmao

                mniExcluirEstoque.Text = "Excluir Pulmão";
            }

            if (gridPicking.SelectedRows.Count > 0)
            {
                mniEtiquetaPicking.Visible = true; //Exibe a impressão de etiqueta do picking
                mniEtiquetaPulmao.Visible = false; //Oculta a impressão de etiqueta do picking

                //Instância as linha da tabela
                DataGridViewRow linha = gridPicking.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                mniExcluirEstoque.Text = "Excluir Picking";

                if (gridPicking.Rows[indice].Cells[12].Value.Equals("CAIXA"))
                {
                    mniEtiquetaPicking50X80.Text = "Etiqueta Caixa 50x80";
                    mniEtiqueta75x100.Text = "Etiqueta Caixa 75x100";
                }
                else if (gridPicking.Rows[indice].Cells[12].Value.Equals("FLOWRACK"))
                {
                    mniEtiquetaPicking50X80.Text = "Etiqueta Flow Rack 50x80";
                    mniEtiqueta75x100.Text = "Etiqueta Flow Rack 75x100";
                }
            }
        }

        private void mniEditarEstoque_Click(object sender, EventArgs e)
        {
            //Abre o frame para edição de estoque
            EdicaoEstoque();
        }

        private void mniBloqueioEstoque_Click(object sender, EventArgs e)
        {
            //Abre o frame para o bloqueio de endereco
            BloqueioDesbloqueioEstoque("SIM");
        }

        private void mniDesbloqueioEstoque_Click(object sender, EventArgs e)
        {
            //Abre o frame para o desbloqueio de endereco
            BloqueioDesbloqueioEstoque("NAO");
        }

        private void mniExcluirEstoque_Click(object sender, EventArgs e)
        {
            if (gridPulmao.SelectedRows.Count > 0 && gridPicking.SelectedRows.Count > 0)
            {
                MessageBox.Show("Por favor dê dois click no endereços! Há um pulmão e um picking selecionado!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (gridPulmao.SelectedRows.Count > 0)
            {
                //Exclui os endereços selecionados do pulmão
                DeletarEstoquePulmao();
            }
            else if (gridPicking.SelectedRows.Count > 0)
            {
                DeletarEndecoPicking();
            }

        }

        private void mniImprimirEtiqueta75x100_Click(object sender, EventArgs e)
        {
            //Imprimir Etiqueta
            ImprimirEtiquetaPicking("75X100");
        }

        private void mniEtiquetaPicking50X80_Click(object sender, EventArgs e)
        {
            //Imprimir Etiqueta
            ImprimirEtiquetaPicking("50X80");
        }

        private void mniEtiquetaPulmao40x100_Click(object sender, EventArgs e)
        {
            ImprimirEtiquetaPulmao("40X100");
        }

        private void mniEtiquetaPulmao50x80_Click(object sender, EventArgs e)
        {
            ImprimirEtiquetaPulmao("50X80");
        }

        private void btnImpressao_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Exibe o menu
                menu.Show(this, 750, 190);

            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha a tela
            Close();
        }

        private void PesqCategorias() //Pesquisa as categorias cadastradas
        {
            try
            {
                //Instância a coleção
                CategoriaCollection categoriaCollection = new CategoriaCollection();
                //Instância o negocios
                CategoriaNegocios categoriaNegocios = new CategoriaNegocios();
                //Limpa o combobox de cadastro
                cmbPesqCategoria.Items.Clear();
                //Limpa o combobox de pesquisa
                cmbPesqCategoria.Items.Clear();

                //Preenche a coleção com a pesquisa
                categoriaCollection = categoriaNegocios.PesqCategoria();
                //Preenche o combobox categoria de cadastro
                categoriaCollection.ForEach(n => cmbPesqCategoria.Items.Add(n.descCategoria));

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void PesqProduto() //Pesquisa o produto
        {
            try
            {
                if (txtPesqCodigo.Text.Equals("") && txtPesqDescricao.Text.Equals("") && txtPesqFornecedor.Text.Equals("")
                    && txtPesqCodBarra.Text.Equals("") && cmbPesqCategoria.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, preencha um dos campos de pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objeto
                    ConsultaEstoqueCollection consultaEstoqueCollection = new ConsultaEstoqueCollection();
                    //instância o objeto
                    EstoqueNegocios consultaEstoqueNegocios = new EstoqueNegocios();
                    //Limpa o grid
                    gridConsulta.Rows.Clear();
                    gridCodBarra.Rows.Clear();
                    gridPicking.Rows.Clear();
                    gridPulmao.Rows.Clear();

                    //Pesquisa o produto e adiciona na coleção
                    consultaEstoqueCollection = consultaEstoqueNegocios.PesqProduto(txtPesqCodigo.Text, txtPesqDescricao.Text, txtPesqFornecedor.Text, txtPesqCodBarra.Text, cmbPesqCategoria.Text, chkPesqAtivo.Checked, cmbEmpresa.Text);
                    //Adiciona no grid
                    consultaEstoqueCollection.ForEach(n => gridConsulta.Rows.Add(n.idProduto, n.codProduto, n.descProduto, n.codFornecedor, n.nomeFornecedor, n.estoque,
                        n.reserva, n.saldo, n.avaria, n.preco, n.wms, (n.wms - n.estoque), n.qtdCaixa, n.unidadeVenda, n.mutiploVenda, n.categoria,
                        n.diasValidade, n.tolerancia, n.tipoArmazenamento, n.nivel, n.tamanhoPalete, n.lastro, n.altura, n.totalPalete, n.status, n.auditaFlowrack, n.controlaValidade, n.paleteBlocado, n.flowrack, "", n.numeroBarra));

                    lblQtd.Text = Convert.ToString(gridConsulta.Rows.Count);

                    if (consultaEstoqueCollection.Count > 0)
                    {
                        if (caminho == null)
                        {
                            //pesquisa o caminho das imagens do produto
                            PesqCaminhoImagem();
                        }

                        //seleciona primeira linha do grid
                        gridConsulta.CurrentCell = gridConsulta.Rows[0].Cells[1];
                        //Foca no grid
                        gridConsulta.Focus();

                        //Pesquisa o código de barra do produto
                        PesqCodBarra();
                        //Pesquisa o picking do produto
                        PesqPicking();
                        //Pesquisa os endereço no pulmão
                        PesqPulmao();

                        //Exibe os dados nos campos
                        DadosCampos();

                        //Limpa o foco do pulmão
                        gridPulmao.ClearSelection();

                    }
                    else
                    {
                        MessageBox.Show("Nenhum produto encontrado!", "WMS - Informações", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PesqCaminhoImagem() //Pesquisa o caminho das imagens
        {
            try
            {
                // Instância o objeto de negocios
                ProdutoNegocios produtoNegocios = new ProdutoNegocios();
                //variável global recebe o caminho
                caminho = produtoNegocios.PesqCaminho();

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PesqCodBarra() //Pesquisa o código de barra dos produtos
        {
            try
            {
                //instância o objeto
                EstoqueNegocios estoqueNegocios = new EstoqueNegocios();
                //Limpa o grid
                gridCodBarra.Rows.Clear();
                //Limpa a coleção
                barraCollection.Clear();
                //Pesquisa o código de barra e adiciona na coleção global
                barraCollection = estoqueNegocios.PesqCodBarra(txtPesqCodigo.Text, txtPesqDescricao.Text, txtPesqFornecedor.Text, txtPesqCodBarra.Text, cmbPesqCategoria.Text, chkPesqAtivo.Checked, cmbEmpresa.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PesqPicking()  //Pesquisa os endereços no picking
        {
            try
            {
                //instância a camda de objêto
                EstoqueNegocios estoqueNegocios = new EstoqueNegocios();
                //Limpa o grid
                gridPicking.Rows.Clear();
                //Limpa a coleção
                enderecoPickingCollection.Clear();
                //Pesquisa o produto e adiciona na coleção global
                enderecoPickingCollection = estoqueNegocios.PesqPicking(txtPesqCodigo.Text, txtPesqDescricao.Text, txtPesqFornecedor.Text, txtPesqCodBarra.Text, cmbPesqCategoria.Text, chkPesqAtivo.Checked, cmbEmpresa.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PesqPulmao() //Pesquisa os endereço no pulmão
        {
            try
            {
                //instância a camda de objêto
                EstoqueNegocios estoqueNegocios = new EstoqueNegocios();
                //Limpa o grid
                gridPulmao.Rows.Clear();
                //Limpa a coleção
                enderecoPulmaoCollection.Clear();
                //Pesquisa o produto e adiciona na coleção global
                enderecoPulmaoCollection = estoqueNegocios.PesqPulmao(txtPesqCodigo.Text, txtPesqDescricao.Text, txtPesqFornecedor.Text, txtPesqCodBarra.Text, cmbPesqCategoria.Text, chkPesqAtivo.Checked, cmbEmpresa.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DadosCampos() //Exibe os dados no campos
        {
            try
            {
                if (gridConsulta.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridConsulta.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    txtCodigo.Text = gridConsulta.Rows[indice].Cells[1].Value.ToString();
                    txtDescricao.Text = gridConsulta.Rows[indice].Cells[2].Value.ToString();
                    txtCodFornecedor.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[3].Value);
                    txtNmFornecedor.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[4].Value);
                    txtEstoque.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[5].Value);
                    txtReserva.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[6].Value);
                    txtSaldo.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[7].Value);
                    txtAvaria.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[8].Value);
                    txtPreco.Text = string.Format("{0:C}", gridConsulta.Rows[indice].Cells[9].Value);
                    lblTotalPulmao.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[10].Value);
                    txtCxa.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[12].Value);
                    txtVenda.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[13].Value);
                    txtMultiplo.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[14].Value);
                    txtCategoria.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[15].Value);
                    txtVidaUtil.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[16].Value);
                    txtTolerancia.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[17].Value);
                    txtArmazenamento.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[18].Value);
                    txtNivel.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[19].Value);
                    txtTpPallete.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[20].Value);
                    txtLastro.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[21].Value);
                    txtAltura.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[22].Value);
                    txtPallete.Text = Convert.ToString(gridConsulta.Rows[indice].Cells[23].Value);
                    chkStatus.Checked = Convert.ToBoolean(gridConsulta.Rows[indice].Cells[24].Value);
                    chkAudita.Checked = Convert.ToBoolean(gridConsulta.Rows[indice].Cells[25].Value);
                    chkControlaValidade.Checked = Convert.ToBoolean(gridConsulta.Rows[indice].Cells[26].Value);
                    chkPaleteBlocado.Checked = Convert.ToBoolean(gridConsulta.Rows[indice].Cells[27].Value);
                    chkFlowrack.Checked = Convert.ToBoolean(gridConsulta.Rows[indice].Cells[28].Value);

                    //Regra para o controle de validade
                    if (Convert.ToBoolean(gridConsulta.Rows[indice].Cells[26].Value) == true)
                    {
                        lblTipoControle.Text = "FEFO - Primeiro que vence é o primeiro a sair";
                    }
                    else
                    {
                        lblTipoControle.Text = "FIFO ou PEPS - Primeiro que entra é o primeiro que sai";
                    }

                    //Regra da observação
                    if (Convert.ToInt32(gridConsulta.Rows[indice].Cells[11].Value) < 0)
                    {
                        lblObservacao.Text = "ESTOQUE NEGATIVO! \n\nPOR FAVOR CONTATE SEU GESTOR!";
                    }
                    else if (Convert.ToInt32(gridConsulta.Rows[indice].Cells[11].Value) > 0)
                    {
                        lblObservacao.Text = "DIVERGÊNCIA DE ESTOQUE! \n\nPOR FAVOR CONTATE SEU GESTOR!";
                    }
                    else
                    {
                        lblObservacao.Text = "Sem observação";
                    }

                    //Pesquisa na coleção o código de barra do produto de acordo com o id
                    List<Barra> barra = barraCollection.FindAll(delegate (Barra n) { return n.idProduto == Convert.ToInt32(gridConsulta.Rows[gridConsulta.CurrentRow.Index].Cells[0].Value); });
                    //Limpa o grid de código de barra
                    gridCodBarra.Rows.Clear();
                    //Adiciona no grid os dados encontrados
                    barra.ForEach(n => gridCodBarra.Rows.Add(n.idProduto, n.numeroBarra, n.multiplicador, n.peso));
                    //Limpa o foco do código de barra
                    gridCodBarra.ClearSelection();

                    //Zera o campo total de unidade no pulmao 
                    lblTotalPulmao.Text = "0";
                    //Zera o campo total de unidade no picking 
                    lblTotalPicking.Text = "0";
                    //Zera o campo total de unidade no flow rack
                    lblTotalFlow.Text = "0";
                    //Zera o campo total de unidade no volumes
                    lblTotalVolumes.Text = "0";
                    //Zera o campo total de unidade no picking 
                    lblTotalDevolucao.Text = "0";
                    //Zera o campo total de unidade no pulmao
                    lblEstoqueFinal.Text = "0";

                    //Pesquisa na coleção o picking do produto de acordo com o id
                    List<EnderecoPicking> picking = enderecoPickingCollection.FindAll(delegate (EnderecoPicking n) { return n.idProduto == Convert.ToInt32(gridConsulta.Rows[gridConsulta.CurrentRow.Index].Cells[0].Value); });
                    //Limpa o grid de picking
                    gridPicking.Rows.Clear();
                    //Adiciona no grid os dados encontrados
                    picking.ForEach(n => gridPicking.Rows.Add(n.idProduto, n.pesoCxa, n.codApartamento, n.endereco, n.estoque, n.unidadeEstoque, n.vencimento, n.peso, n.lote, n.capacidade, n.abastecimento, n.unidadeCapacidade, n.tipoEndereco, n.apStatus, n.quantidadeCaixa, n.numeroBarra, n.codEstacao));

                    //Limpa o foco do picking
                    gridPicking.ClearSelection();
                    //Zera a quantidade de picking caixa
                    lblQtdEnderecoPicking.Text = "0";
                    //Zera a quantidade de picking Flow rack
                    lblQtdEnderecoFlow.Text = "0";

                    //Conta a quantidade de endereçõs de picking
                    for (int cont = 0; gridPicking.Rows.Count > cont; cont++)
                    {   //Verifica o picking de caixa
                        if (gridPicking.Rows[cont].Cells[12].Value.Equals("CAIXA"))
                        {
                            //Seta a qtd de endereco de picking
                            lblQtdEnderecoPicking.Text = (Convert.ToInt32(lblQtdEnderecoPicking.Text) + 1).ToString();
                            //Seta o estoque
                            lblTotalPicking.Text = Convert.ToString(gridPicking.Rows[cont].Cells[4].Value);
                        }
                        //Verifica o flow rack
                        if (gridPicking.Rows[cont].Cells[12].Value.Equals("FLOWRACK"))
                        {
                            //Seta a qtd de endereco de flow rack
                            lblQtdEnderecoFlow.Text = (Convert.ToInt32(lblQtdEnderecoFlow.Text) + 1).ToString();
                            //Seta o estoque
                            lblTotalFlow.Text = Convert.ToString(gridPicking.Rows[cont].Cells[4].Value);
                        }
                    }

                    //Limpa o grid
                    gridPulmao.Rows.Clear();
                    //Localizando
                    List<EnderecoPulmao> aereo = enderecoPulmaoCollection.FindAll(delegate (EnderecoPulmao n) { return n.idProduto == Convert.ToInt32(gridConsulta.Rows[gridConsulta.CurrentRow.Index].Cells[0].Value); });
                    //Adiciona no grid
                    aereo.ForEach(n => gridPulmao.Rows.Add(n.codApartamento1, n.descEndereco1, n.idProduto, n.qtdCaixaOrigem, (n.qtdCaixaOrigem / n.fatorPulmao), n.tipoApartamento1, n.vencimentoProduto1, (n.pesoProduto1 * n.qtdCaixaOrigem), n.pesoCxa, n.loteProduto1, n.bloqueado, n.reserva, n.dataMimTolerancia, n.dataMaxTolerancia, n.motivoBloqueio, n.dataEntrada, n.fatorPulmao, n.notaCega, n.lastroM, n.alturaM));
                    //Seta a qtd de endereco
                    lblQtdEnderecoPulmao.Text = Convert.ToString(gridPulmao.Rows.Count);
                    //Limpa o foco do grid pulmão
                    gridPulmao.ClearSelection();

                    //Soma as unidade do pulmão
                    if (gridPulmao.Rows.Count > 0)
                    {
                        //Soma a quantidade de endereco do Pulmão
                        for (int i = 0; gridPulmao.Rows.Count > i; i++)
                        {
                            lblTotalPulmao.Text = (Convert.ToInt32(lblTotalPulmao.Text) + Convert.ToInt32(gridPulmao.Rows[i].Cells[3].Value)).ToString();
                        }
                    }

                    //Seleciona a primeira linha do grid
                    //gridPulmao.CurrentCell = gridPulmao.Rows[0].Cells[1];

                    //Foca no grid
                    // gridPicking.Focus();


                    /* for (int i = 0; i < gridEnderecoPulmao.Rows.Count; i++)
                     {
                         //Qtd do aereo
                         lblQtdEstoque.Text = Convert.ToString(Convert.ToInt32(lblQtdEstoque.Text) + Convert.ToInt32(gridEnderecoPulmao.Rows[i].Cells[2].Value));

                         //Verifica se o vencimento está dentro de 30 dias para se vencer
                         if ((Convert.ToDateTime(gridEnderecoPulmao.Rows[i].Cells[7].Value).Date <= Convert.ToDateTime(gridEnderecoPulmao.Rows[i].Cells[12].Value).Date) && (Convert.ToDateTime(gridEnderecoPulmao.Rows[i].Cells[7].Value).Date <= Convert.ToDateTime(gridEnderecoPulmao.Rows[i].Cells[11].Value).Date))
                         {
                             //Seta a cor vermelho
                             gridEnderecoPulmao.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                         }
                         //Verifica se o vencimento está dentro do prazo de tolerância
                         else if (Convert.ToDateTime(gridEnderecoPulmao.Rows[i].Cells[7].Value).Date <= Convert.ToDateTime(gridEnderecoPulmao.Rows[i].Cells[10].Value).Date)
                         {
                             //Seta a cor azul
                             gridEnderecoPulmao.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                         }
                         else
                         {
                             //Seta a cor preto
                             gridEnderecoPulmao.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                         }

                     }*/

                    if (gridPicking.Rows.Count > 0)
                    {
                        //Total de unidades no Pulmao - Insere o tipo de estoque
                        lblPulmao.Text = "Pulmao (" + gridPicking.Rows[0].Cells[5].Value + "):";
                        //Total de unidades no Picking - Insere o tipo de estoque
                        lblPicking.Text = "Picking (" + gridPicking.Rows[0].Cells[5].Value + "):";
                        //Total de unidades no Flow Rack - Insere o tipo de estoque
                        lblFlow.Text = "Flow Rack (" + gridPicking.Rows[0].Cells[5].Value + "):";
                        //Total de unidades no volumes de Flow Rack - Insere o tipo de estoque
                        lblVolumes.Text = "Volumes de Flow Rack (" + gridPicking.Rows[0].Cells[5].Value + "):";
                        //Total de devoluções - Insere o tipo de estoque
                        lblDevolucao.Text = "Devoluções Não Conferidas (" + gridPicking.Rows[0].Cells[5].Value + "):";
                        //Total - Insere o tipo de estoque
                        lblTotalEstoque.Text = "Total (" + gridPicking.Rows[0].Cells[5].Value + "):";
                    }

                    //Soma o estoque final
                    lblEstoqueFinal.Text = (Convert.ToInt32(lblTotalPulmao.Text) + Convert.ToInt32(lblTotalPicking.Text) + Convert.ToInt32(lblTotalFlow.Text) + Convert.ToInt32(lblTotalVolumes.Text) + Convert.ToInt32(lblTotalDevolucao.Text)).ToString();
                    //Lança no campo estoque wms para confronto WMS X ERP
                    txtWms.Text = lblEstoqueFinal.Text;
                    //Confronta o estoque
                    txtConfronto.Text = (Convert.ToInt32(txtWms.Text) - Convert.ToInt32(txtEstoque.Text)).ToString();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao preencher os campos \nDetalhes: " + ex, "WMS - ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void EdicaoEstoque()  //Edita o estoque do produto
        {
            //Instância o frame
            FrmEdicaoEstoque edicao = new FrmEdicaoEstoque();

            if (gridPulmao.Focused == true && gridPulmao.Rows.Count > 0)
            {
                edicao.codUsuario = codUsuario;
                edicao.tipoOperacao = "Pulmao";
                edicao.idProduto = Convert.ToInt32(gridConsulta.Rows[gridConsulta.CurrentRow.Index].Cells[0].Value);
                edicao.codProduto = Convert.ToString(gridConsulta.Rows[gridConsulta.CurrentRow.Index].Cells[1].Value);
                edicao.descProduto = Convert.ToString(gridConsulta.Rows[gridConsulta.CurrentRow.Index].Cells[2].Value);

                edicao.codEndereco = Convert.ToInt32(gridPulmao.Rows[gridPulmao.CurrentRow.Index].Cells[0].Value);
                edicao.endereco = Convert.ToString(gridPulmao.Rows[gridPulmao.CurrentRow.Index].Cells[1].Value);
                edicao.estoque = Convert.ToInt32(gridPulmao.Rows[gridPulmao.CurrentRow.Index].Cells[4].Value);
                edicao.tipoEstoque = Convert.ToString(gridPulmao.Rows[gridPulmao.CurrentRow.Index].Cells[5].Value);
                edicao.validade = Convert.ToDateTime(gridPulmao.Rows[gridPulmao.CurrentRow.Index].Cells[6].Value);
                edicao.peso = Convert.ToDouble(gridPulmao.Rows[gridPulmao.CurrentRow.Index].Cells[7].Value);
                edicao.pesoCaixa = Convert.ToDouble(gridPulmao.Rows[gridPulmao.CurrentRow.Index].Cells[8].Value);
                edicao.lote = Convert.ToString(gridPulmao.Rows[gridPulmao.CurrentRow.Index].Cells[9].Value);
                edicao.qtdCaixa = Convert.ToInt32(gridPulmao.Rows[gridPulmao.CurrentRow.Index].Cells[16].Value);
                edicao.totalPalete = Convert.ToInt32(txtPallete.Text);
                edicao.vidaUtil = Convert.ToInt32(txtVidaUtil.Text);
                edicao.empresa = cmbEmpresa.Text;


                //Exibe o frame
                edicao.ShowDialog();
            }
            else if (gridPicking.Focused == true && gridPicking.Rows.Count > 0)
            {
                edicao.codUsuario = codUsuario;
                edicao.tipoOperacao = "Picking";
                edicao.idProduto = Convert.ToInt32(gridConsulta.Rows[gridConsulta.CurrentRow.Index].Cells[0].Value);
                edicao.codProduto = Convert.ToString(gridConsulta.Rows[gridConsulta.CurrentRow.Index].Cells[1].Value);
                edicao.descProduto = Convert.ToString(gridConsulta.Rows[gridConsulta.CurrentRow.Index].Cells[2].Value);

                edicao.pesoCaixa = Convert.ToDouble(gridPicking.Rows[gridPicking.CurrentRow.Index].Cells[1].Value);
                edicao.codEndereco = Convert.ToInt32(gridPicking.Rows[gridPicking.CurrentRow.Index].Cells[2].Value);
                edicao.endereco = Convert.ToString(gridPicking.Rows[gridPicking.CurrentRow.Index].Cells[3].Value);
                edicao.estoque = Convert.ToInt32(gridPicking.Rows[gridPicking.CurrentRow.Index].Cells[4].Value);
                edicao.tipoEstoque = Convert.ToString(gridPicking.Rows[gridPicking.CurrentRow.Index].Cells[5].Value);
                edicao.validade = Convert.ToDateTime(gridPicking.Rows[gridPicking.CurrentRow.Index].Cells[6].Value);
                edicao.peso = Convert.ToDouble(gridPicking.Rows[gridPicking.CurrentRow.Index].Cells[7].Value);
                edicao.lote = Convert.ToString(gridPicking.Rows[gridPicking.CurrentRow.Index].Cells[8].Value);

                edicao.capacidade = Convert.ToInt32(gridPicking.Rows[gridPicking.CurrentRow.Index].Cells[9].Value);
                edicao.abastecimento = Convert.ToInt32(gridPicking.Rows[gridPicking.CurrentRow.Index].Cells[10].Value);
                edicao.tipoAbastecimento = Convert.ToString(gridPicking.Rows[gridPicking.CurrentRow.Index].Cells[11].Value);
                edicao.qtdCaixa = Convert.ToInt32(gridPicking.Rows[gridPicking.CurrentRow.Index].Cells[14].Value);
                edicao.vidaUtil = Convert.ToInt32(txtVidaUtil.Text);
                edicao.empresa = cmbEmpresa.Text;

                //Exibe o frame
                edicao.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nenhum endereço selecionado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void BloqueioDesbloqueioEstoque(string bloqueia) //Bloqueia e desbloqueia o produto no endereço selecionados
        {
            //Instância o frame
            FrmBloqueiaEstoque bloqueio = new FrmBloqueiaEstoque();

            if (gridPulmao.Focused == true && gridPulmao.Rows.Count > 0)
            {
                bloqueio.codUsuario = codUsuario; //Código do usuário
                bloqueio.bloqueia = bloqueia; //Bloqueia sim ou nao
                bloqueio.idProduto = Convert.ToInt32(gridConsulta.Rows[gridConsulta.CurrentRow.Index].Cells[0].Value); //Id do Produto

                int i = 0; //controla a posição do array
                bloqueio.endereco = new string[gridPulmao.SelectedRows.Count]; //Define o tamanho array dos endereços
                bloqueio.codEndereco = new int[gridPulmao.SelectedRows.Count]; //Define o tamanho array dos códigos dos endereços

                //Preenche o array
                foreach (DataGridViewRow row in gridPulmao.SelectedRows)
                {
                    bloqueio.codEndereco[i] = Convert.ToInt32(row.Cells[0].Value);
                    bloqueio.endereco[i] = Convert.ToString(row.Cells[1].Value);
                    i++;
                }

                //Exibe o frame
                bloqueio.ShowDialog();
            }
            else if (gridPicking.Focused == true && gridPicking.Rows.Count > 0)
            {
                MessageBox.Show("Essa operação não é permitida para a área de separação!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Nenhum endereço selecionado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void DeletarEstoquePulmao()//Exclui o estoque do produto no endereço selecionados
        {
            try
            {
                if (MessageBox.Show("Tem certeza que deseja excluir o(s) estoque(s) selecionado(s)?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //Instância a camada de objêto negocios
                    EstoqueNegocios estoque = new EstoqueNegocios();
                    //Instância a camada de negocios
                    TransferenciaNegocios transferenciaNegocios = new TransferenciaNegocios();

                    bool excluir = false;

                    //Verifica os endereços selecionados
                    foreach (DataGridViewRow row in gridPulmao.SelectedRows)
                    {
                        if (Convert.ToInt32(row.Cells[11].Value) > 0)
                        {
                            MessageBox.Show("Operação não realizada no endereco "+ row.Cells[1].Value +" produto reservado!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //Deleta os endereços selecionados
                            estoque.DeletarEstoquePulmao(Convert.ToInt32(row.Cells[2].Value), Convert.ToInt32(row.Cells[0].Value));
                            //Deleta o endereço da coleção
                            enderecoPulmaoCollection.RemoveAll((x) => x.codApartamento1 == Convert.ToInt32(row.Cells[0].Value) && x.idProduto == Convert.ToInt32(row.Cells[2].Value));

                            estoque.InserirRastreamento(Convert.ToInt32(row.Cells[2].Value), 
                                                        Convert.ToInt32(row.Cells[0].Value), 
                                                        Convert.ToInt32(row.Cells[3].Value), 
                                                        Convert.ToDateTime(row.Cells[6].Value), 
                                                        Convert.ToDouble(row.Cells[7].Value), 
                                                        Convert.ToString(row.Cells[9].Value),
                                                        codUsuario);

                            excluir = true;


                        }
                    }

                    if (excluir == true)
                    {
                        //exibe os dados nos campos novamente
                        DadosCampos();

                        MessageBox.Show("Estoque excluído(s) com sucesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Excluí o produto do endereço
        private void DeletarEndecoPicking()
        {
            try
            {
                if (gridPicking.Rows.Count > 0)
                {
                    if (MessageBox.Show("Deseja realmente excluir o produto do endereço?", "WMS - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Instância as linha da tabela
                        DataGridViewRow linha = gridPicking.CurrentRow;
                        //Recebe o indice   
                        int indice = linha.Index;

                        //Instância a camada de negocios
                        EnderecamentoNegocios enderecoNegocios = new EnderecamentoNegocios();
                        //Instância a camada de negocios
                        EstoqueNegocios estoqueNegocios = new EstoqueNegocios();

                        enderecoNegocios.DeletaEnderecoSeparacao(Convert.ToInt32(gridPicking.Rows[indice].Cells[2].Value), cmbEmpresa.Text);

                        estoqueNegocios.InserirRastreamento(Convert.ToInt32(gridPicking.Rows[indice].Cells[2].Value),
                                                            Convert.ToInt32(gridPicking.Rows[indice].Cells[0].Value),
                                                            Convert.ToInt32(gridPicking.Rows[indice].Cells[3].Value),
                                                            Convert.ToDateTime(gridPicking.Rows[indice].Cells[6].Value),
                                                            Convert.ToDouble(gridPicking.Rows[indice].Cells[7].Value), 
                                                            Convert.ToString(gridPicking.Rows[indice].Cells[9].Value), 
                                                            codUsuario);

                        //Exclui a linha do grid
                        gridPicking.Rows.RemoveAt(indice);

                        MessageBox.Show("Exclusão realizada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }


        //Imprimi a etiqueta
        private void ImprimirEtiquetaPicking(string tamanho)
        {
            try
            {
                //Pega o caminho da etiqueta prn
                string etiqueta = null;

                //Caminho do novo arquivo atualizado
                string NovaEtiqueta = AppDomain.CurrentDomain.BaseDirectory + "Picking.txt";

                if (impressora.Equals("ARGOX 214") && tamanho == "75X100")
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "PickingArgox214_75x100.prn";
                }

                if (impressora.Equals("ARGOX 214") && tamanho == "50X80")
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "Picking_50X80.prn";
                }

                if (impressora.Equals("ARGOX 214 PLUS") && tamanho == "75X100")
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "PickingArgox214Plus_75x100.prn";
                }

                if (impressora.Equals("ARGOX 214 PLUS") && tamanho == "50X80")
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "Picking_50X80.prn";
                }


                if (impressora.Equals("ZEBRA") && tamanho == "75X100")
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "ZEBRA PICKING 75X100.prn";
                }

                if (impressora.Equals("ZEBRA") && tamanho == "50X80")
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "ZEBRA PICKING 50X80.prn";
                }

                // Abre o arquivo para escrita
                StreamReader streamReader;
                streamReader = File.OpenText(etiqueta);

                string contents = streamReader.ReadToEnd();

                streamReader.Close();

                string conteudo = contents;

                StreamWriter streamWriter = File.CreateText(NovaEtiqueta);

                //Instância as linha da tabela
                DataGridViewRow linha = gridPicking.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                // Atualizo as variaveis do arquivo
                conteudo = conteudo.Replace("Empresa............", empresa);
                conteudo = conteudo.Replace("EAN", Convert.ToString(gridPicking.Rows[indice].Cells[2].Value));
                conteudo = conteudo.Replace("CAD", txtCodigo.Text);
                conteudo = conteudo.Replace("SKU", txtDescricao.Text);
                conteudo = conteudo.Replace("BARRA", Convert.ToString(gridPicking.Rows[indice].Cells[15].Value));
                conteudo = conteudo.Replace("CAP", Convert.ToString(gridPicking.Rows[indice].Cells[9].Value));
                conteudo = conteudo.Replace("AB", Convert.ToString(gridPicking.Rows[indice].Cells[10].Value));
                conteudo = conteudo.Replace("NUMERACAO", Convert.ToString(gridPicking.Rows[indice].Cells[3].Value));

                streamWriter.Write(conteudo);

                streamWriter.WriteLine("E"); //Fim do modo de formatação e imprime

                streamWriter.WriteLine("/222"); //Avanço para corte da etiqueta
                streamWriter.Close();

                PrintDialog pd = new PrintDialog();

                pd.PrinterSettings = new PrinterSettings();

                if (pd.PrinterSettings.IsValid)
                {
                    ArgoxPPLA.RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, NovaEtiqueta);

                    File.Delete(NovaEtiqueta);
                }
                else
                {
                    MessageBox.Show("Não foi encontrada nenhuma impressora instalada no seu computador!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro na geração da etiqueta: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImprimirEtiquetaPulmao(string tamanho)
        {
            try
            {
                //Pega o caminho da etiqueta prn
                string etiqueta = null;

                if (impressora.Equals("ARGOX 214") && tamanho == "40X100")
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "PulmaoArgox214_40x100.prn";
                }

                if (impressora.Equals("ARGOX 214") && tamanho == "50X80")
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "PULMAO_50x80.prn";
                }

                if (impressora.Equals("ARGOX 214 PLUS") && tamanho == "40X100")
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "PulmaoArgox214Plus_40x100.prn";
                }

                if (impressora.Equals("ARGOX 214 PLUS") && tamanho == "50X80")
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "PULMAO_50x80.prn";
                }


                if (impressora.Equals("ZEBRA") && tamanho == "40X100")
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "ZEBRA PULMAO 40X100.prn";
                }

                if (impressora.Equals("ZEBRA") && tamanho == "50X80")
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "ZEBRA PULMAO 50X80.prn";
                }

                //Caminho do novo arquivo atualizado
                string NovaEtiqueta = AppDomain.CurrentDomain.BaseDirectory + "Pulmao.txt";

                foreach (DataGridViewRow row in gridPulmao.SelectedRows)
                {
                    // Abre o arquivo para escrita
                    StreamReader streamReader;
                    streamReader = File.OpenText(etiqueta);
                    StreamWriter streamWriter = File.CreateText(NovaEtiqueta);
                    string contents = streamReader.ReadToEnd();

                    streamReader.Close();

                    string conteudo = contents;

                    // Atualizo as variaveis do arquivo
                    conteudo = conteudo.Replace("EMPRESA....................", empresa);
                    conteudo = conteudo.Replace("SKU", txtCodigo.Text + "-" + txtDescricao.Text);
                    conteudo = conteudo.Replace("L1", Convert.ToString(row.Cells[18].Value));
                    conteudo = conteudo.Replace("L2", Convert.ToString(row.Cells[19].Value));
                    conteudo = conteudo.Replace("Q1", Convert.ToString(row.Cells[4].Value) + " " + Convert.ToString(row.Cells[5].Value));
                    conteudo = conteudo.Replace("DT", string.Format("{0:d}", row.Cells[6].Value));
                    conteudo = conteudo.Replace("PS", string.Format("{0:n}", row.Cells[7].Value));
                    conteudo = conteudo.Replace("LT", Convert.ToString(row.Cells[9].Value).Substring(0));
                    conteudo = conteudo.Replace("D2", string.Format("{0:d}", row.Cells[15].Value));
                    conteudo = conteudo.Replace("NRNOTA", Convert.ToString(row.Cells[17].Value));
                    conteudo = conteudo.Replace("ENDERECO", Convert.ToString(row.Cells[1].Value));
                    //string.Format("{0:d}",string.Format("{0:n}",

                    streamWriter.Write(conteudo);

                    streamWriter.WriteLine("E"); //Fim do modo de formatação e imprime

                    streamWriter.WriteLine("/222"); //Avanço para corte da etiqueta
                    streamWriter.Close();

                    PrintDialog pd = new PrintDialog();

                    pd.PrinterSettings = new PrinterSettings();

                    if (pd.PrinterSettings.IsValid)
                    {
                        ArgoxPPLA.RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, NovaEtiqueta);

                        File.Delete(NovaEtiqueta);
                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrada nenhuma impressora instalada no seu computador!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro na geração da etiqueta: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ExibirImagem() //Exibe a imagem do picturebox
        {
            try
            {
                if (gridConsulta.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridConsulta.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    pictureBox2.Image = Image.FromFile(caminho + Convert.ToString(gridConsulta.Rows[indice].Cells[30].Value) + ".jpg");
                    //Chama o método EscalaPercentual  que retorna a imagem 
                    //redimensionada e armazena-a em uma variavel do tipo Image
                    Image imagemEscalonada = EscalaPercentual(pictureBox2.Image);
                    //Exibe a imagem no picture box.
                    pictureBox2.Image = imagemEscalonada;
                    //Centraliza a imagem
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception)
            {
                //Limpa a imagem
                pictureBox2.Image = null;

            }
        }

        static Image EscalaPercentual(Image imgFoto)//Configura o tamanho da imagem para exibição no frame
        {
            int fonteLargura = imgFoto.Width;     //armazena a largura original da imagem origem
            int fonteAltura = imgFoto.Height;   //armazena a altura original da imagem origem
            int origemX = 0;        //eixo x da imagem origem
            int origemY = 0;        //eixo y da imagem origem

            int destX = 0;          //eixo x da imagem destino
            int destY = 0;          //eixo y da imagem destino

            //Calcula a altura e largura da imagem redimensionada
            int destWidth = 0;
            int destHeight = 0;

            if (fonteAltura >= 225)
            {
                //Calcula a altura e largura da imagem redimensionada
                destWidth = 130;
                destHeight = 140;
            }
            else
            {
                //Calcula a altura e largura da imagem redimensionada
                destWidth = 140;
                destHeight = 170;
            }

            //Cria um novo objeto bitmap
            Bitmap bmImagem = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            //Define a resolu~ção do bitmap.
            bmImagem.SetResolution(imgFoto.HorizontalResolution, imgFoto.VerticalResolution);
            //Crima um objeto graphics e defina a qualidade
            Graphics grImagem = Graphics.FromImage(bmImagem);
            grImagem.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //Desenha a imge usando o método DrawImage() da classe grafica
            grImagem.DrawImage(imgFoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(origemX, origemY, fonteLargura, fonteAltura),
                GraphicsUnit.Pixel);

            grImagem.Dispose();  //libera o objeto grafico
            return bmImagem;
        }

        private void txtPesqCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPesqDescricao_TextChanged(object sender, EventArgs e)
        {

        }
    }

}



/*


                //Limpa o grid picking
                gridPicking.Rows.Clear();
                //Localizando
                List<EnderecoPicking> picking = enderecoPickingCollection.FindAll(delegate (EnderecoPicking n) { return n.idProduto == Convert.ToInt32(gridConsulta.Rows[indice].Cells[0].Value); });
                //Adiciona no grid
                picking.ForEach(n => gridPicking.Rows.Add(n.endereco, n.estoque, n.reserva, n.tipoEndereco, n.peso, n.validade, n.lote, n.capacidade, n.abastecimento, n.apStatus));
                //Limpa a seleção no grid
                gridPicking.ClearSelection();

                if (gridPicking.Rows.Count > 0)
                {
                    if (Convert.ToString(gridPicking.Rows[0].Cells[9].Value) == "Picking Com Problema")
                    {
                        gridPicking.Rows[0].Cells[9].Style.ForeColor = Color.Red;
                    }
                    else if (Convert.ToString(gridPicking.Rows[0].Cells[9].Value) == "Picking Acima da Capacidade")
                    {
                        gridPicking.Rows[0].Cells[9].Style.ForeColor = Color.Green;
                    }
                    else if (Convert.ToString(gridPicking.Rows[0].Cells[9].Value) == "Abastecer Picking")
                    {
                        gridPicking.Rows[0].Cells[9].Style.ForeColor = Color.Blue;
                    }
                    else
                    {
                        gridPicking.Rows[0].Cells[9].Style.ForeColor = Color.Black;
                    }
                }

               
                if (gridPicking.Rows.Count > 0)
                {
                    //Preenche o estoque do wms
                    txtWms.Text = Convert.ToString(Convert.ToInt32(lblQtdEstoque.Text) * 0 /*Convert.ToInt32(txtqtdArmazenada.Text)
+Convert.ToInt32(gridPicking.Rows[0].Cells[1].Value));
                }
                else
{
    //Preenche o estoque do wms
    txtWms.Text = Convert.ToString(Convert.ToInt32(lblQtdEstoque.Text) * 0 /*Convert.ToInt32(txtqtdArmazenada.Text));
}

if (inventario == 1)
{
    //Limpa o grid
    gridAereoInventario.Rows.Clear();
    //Localizando
    List<EnderecoPulmao> aereoInventario = aereoInventarioCollection.FindAll(delegate (EnderecoPulmao n) { return n.idProduto == Convert.ToInt32(gridConsulta.Rows[indice].Cells[0].Value); });
    //Adiciona no grid
    aereoInventario.ForEach(n => gridAereoInventario.Rows.Add(n.endereco, n.dataHoraConsolidacao, n.contagemConsolidacao, n.validadeConsolidacao, n.pesoConsolidacao, n.loteConsolidacao, n.usuarioConsolidacao, n.dataHora1, n.contagem1, n.validade1, n.peso1, n.lote1, n.usuario1, n.dataHora2, n.contagem2, n.validade2, n.peso2, n.lote2, n.usuario2, n.dataHora3, n.contagem3, n.validade3, n.peso3, n.lote3, n.usuario3));
    //Seta a qtd de linha
    lblQtdEnderecoInventario.Text = Convert.ToString(gridAereoInventario.Rows.Count);
    //1 contagem
    lbl1contagemAereo.Text = "0";
    //2 contagem
    lbl2contagemAereo.Text = "0";

    for (int i = 0; i < gridAereoInventario.Rows.Count; i++)
    {
        if (Convert.ToString(gridAereoInventario.Rows[i].Cells[8].Value) != "")
        {
            //1 contagem
            lbl1contagemAereo.Text = Convert.ToString(Convert.ToInt32(lbl1contagemAereo.Text) + Convert.ToInt32(gridAereoInventario.Rows[i].Cells[8].Value));
        }

        if (Convert.ToString(gridAereoInventario.Rows[i].Cells[14].Value) != "")
        {
            //2 contagem
            lbl2contagemAereo.Text = Convert.ToString(Convert.ToInt32(lbl2contagemAereo.Text) + Convert.ToInt32(gridAereoInventario.Rows[i].Cells[14].Value));
        }

        if (Convert.ToString(gridAereoInventario.Rows[i].Cells[2].Value) != "")
        {
            //consolidado
            lblConsolidado.Text = Convert.ToString(Convert.ToInt32(lblConsolidado.Text) + Convert.ToInt32(gridAereoInventario.Rows[i].Cells[2].Value));
        }

        lblDiferencaAereo.Text = Convert.ToString(Convert.ToInt32(lbl1contagemAereo.Text) - Convert.ToInt32(lbl2contagemAereo.Text));
    }

    //Limpa o grid
    gridPickingInventario.Rows.Clear();
    //Localizando
    List<EnderecoPicking> pickingInventario = pickingInventarioCollection.FindAll(delegate (EnderecoPicking n) { return n.idProduto == Convert.ToInt32(gridConsulta.Rows[indice].Cells[0].Value); });
    //Adiciona no grid
    //pickingInventario.ForEach(n => gridPickingInventario.Rows.Add(n.endereco, n.dataHoraConsolidacao, n.contagemConsolidacao, n.validadeConsolidacao, n.pesoConsolidacao, n.loteConsolidacao, n.usuarioConsolidacao, n.dataHora1, n.contagem1, n.validade1, n.peso1, n.lote1, n.usuario1, n.dataHora2, n.contagem2, n.validade2, n.peso2, n.lote2, n.usuario2, n.dataHora3, n.contagem3, n.validade3, n.peso3, n.lote3, n.usuario3));
    //1 contagem
    lbl1contagemPicking.Text = "0";
    //2 contagem
    lbl2contagemPicking.Text = "0";
    //consolidado
    lblConsolidado.Text = "0";

    for (int i = 0; i < gridPickingInventario.Rows.Count; i++)
    {
        if (Convert.ToString(gridPickingInventario.Rows[i].Cells[8].Value) != "")
        {
            //1 contagem
            lbl1contagemPicking.Text = Convert.ToString(Convert.ToInt32(lbl1contagemPicking.Text) + Convert.ToInt32(gridPickingInventario.Rows[i].Cells[8].Value));
        }

        if (Convert.ToString(gridPickingInventario.Rows[i].Cells[14].Value) != "")
        {
            //2 contagem
            lbl2contagemPicking.Text = Convert.ToString(Convert.ToInt32(lbl2contagemPicking.Text) + Convert.ToInt32(gridPickingInventario.Rows[i].Cells[14].Value));
        }

        if (Convert.ToString(gridPickingInventario.Rows[i].Cells[2].Value) != "")
        {
            //consolidado
            lblConsolidado.Text = Convert.ToString(Convert.ToInt32(lblConsolidado.Text) * 0/*Convert.ToInt32(txtqtdArmazenada.Text) + Convert.ToInt32(gridPickingInventario.Rows[i].Cells[2].Value));
        }

        lblDiferencaPicking.Text = Convert.ToString(Convert.ToInt32(lbl1contagemPicking.Text) - Convert.ToInt32(lbl2contagemPicking.Text));
    }
}

*/