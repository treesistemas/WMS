using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Negocios;
using ObjetoTransferencia;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Wms
{
    public partial class FrmProduto : Form
    {
        //Array com id
        private int[] categoria;
        private int[] compra;
        private int[] pulmao;
        private int[] picking;

        private int codCategoria;
        //private int codCompra;
        private int codPulmao;
        private int codPicking;
        public int codUsuario;

        string caminho = null;

        //Instância a coleção de barra
        BarraCollection barraCollection = new BarraCollection();

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmProduto()
        {
            InitializeComponent();

        }

        private void FrmProduto_Load(object sender, EventArgs e)
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

        private void txtPesqCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo descrição do produto
                txtPesqDescricao.Focus();
            }
        }

        private void txtPesqDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo código de fornecedor
                txtPesqFornecedor.Focus();
            }
        }

        private void txtPesqFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Pesquisa o fonecedor
                PesqFornecedor();
            }
        }

        private void txtPesqCodBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo categoria
                cmbPesqCategoria.Focus();
            }
        }

        private void cmbPesqCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão pesquisar
                btnPesquisar.Focus();
            }
        }

        private void cmbCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                cmbArmazenamento.Focus();
            }
        }

        private void cmbArmazenamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                cmbPalete.Focus();
            }
        }

        private void cmbPalete_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtShelfLife.Focus();
            }
        }

        private void txtShelfLife_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtTolerancia.Focus();
            }
        }

        private void txtTolerancia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtNivel.Focus();
            }
        }

        private void txtLimiteRecebimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtNivel.Focus();
            }
        }

        private void txtNivel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtLastroPeq.Focus();
            }
        }

        private void txtLastroPeq_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtAlturaPeq.Focus();
            }
        }

        private void txtAlturaPeq_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtLastroMed.Focus();
            }
        }

        private void txtLastroMed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtAlturaMed.Focus();
            }
        }

        private void txtAlturaMed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtLastroGrd.Focus();
            }
        }

        private void txtLastroGrd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtAlturaGrd.Focus();
            }
        }

        private void txtAlturaGrd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtLastroBlc.Focus();
            }
        }

        private void txtLastroBlc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtAlturaBlc.Focus();
            }

        }

        private void txtAlturaBlc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtFatorPulmao.Focus();
            }
        }

        private void txtFatorPulmao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                cmbUndPulmao.Focus();
            }
        }

        private void cmbUndPulmao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtFatorPicking.Focus();
            }
        }

        private void txtFatorPicking_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                cmbUndPicking.Focus();
            }
        }

        private void cmbUndPicking_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                btnSalvar.Focus();
            }
        }

        private void txtPesqFornecedor_TextChanged(object sender, EventArgs e)
        {
            if (txtPesqFornecedor.Text.Equals(""))
            {
                txtPesqNmFornecedor.Clear();
            }
        }

        private void cmbPesqCategoria_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbCategoria.Items.Count == 0)
            {
                //Preenche o combobox categoria
                PesqCategorias();
            }
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            //recebe o código da categoria selecionada
            codCategoria = categoria[cmbCategoria.SelectedIndex];
        }

        private void cmbUndPulmao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //recebe o código da unidade selecionada
            codPulmao = pulmao[cmbUndPulmao.SelectedIndex];
        }

        private void cmbUndPicking_SelectedIndexChanged(object sender, EventArgs e)
        {
            //recebe o código da unidade selecionada
            codPicking = picking[cmbUndPicking.SelectedIndex];
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (!txtCodigo.Equals(""))
            {
                //exibe a imagem
                ExibirImagem();
            }
        }

        private void txtShelfLife_TextChanged(object sender, EventArgs e)
        {
            //Soma automática o shelf life
            somarValores("Limite");
        }

        private void txtTolerancia_TextChanged(object sender, EventArgs e)
        {
            //Soma automática o shelf life
            somarValores("Limite");
        }

        private void txtLastroPeq_TextChanged(object sender, EventArgs e)
        {
            //Soma automática do total do palete e peso
            somarValores("P");
        }

        private void txtAlturaPeq_TextChanged(object sender, EventArgs e)
        {
            //Soma automática do total do palete e peso
            somarValores("P");
        }

        private void txtLastroMed_TextChanged(object sender, EventArgs e)
        {
            //Soma automática do total do palete e peso
            somarValores("M");
        }

        private void txtAlturaMed_TextChanged(object sender, EventArgs e)
        {
            //Soma automática do total do palete e peso
            somarValores("M");
        }

        private void txtLastroGrd_TextChanged(object sender, EventArgs e)
        {
            //Soma automática do total do palete e peso
            somarValores("G");
        }

        private void txtAlturaGrd_TextChanged(object sender, EventArgs e)
        {
            //Soma automática do total do palete e peso
            somarValores("G");
        }

        private void txtLastroBlc_TextChanged(object sender, EventArgs e)
        {
            //Soma automática do total do palete e peso
            somarValores("B");
        }

        private void txtAlturaBlc_TextChanged(object sender, EventArgs e)
        {
            //Soma automática do total do palete e peso
            somarValores("B");
        }

        private void chkPaleteBlocado_CheckedChanged(object sender, EventArgs e)
        {
            //Habilita e desabilita o palete blocado
            if (chkPaleteBlocado.Checked)
            {
                txtLastroBlc.Enabled = true;
                txtAlturaBlc.Enabled = true;
            }
            else
            {
                txtLastroBlc.Enabled = false;
                txtAlturaBlc.Enabled = false;
            }
        }

        private void gridProduto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        private void gridProduto_KeyUp(object sender, KeyEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        private void gridProduto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (gridProduto.RowCount > 0)
                {
                    //Habilita todos os campos
                    HabilitarCampos();
                }
            }
        }


        private void mniNovoBarra_Click(object sender, EventArgs e)
        {
            if (perfilUsuario.Equals("ADMINISTRADOR"))
            {
                //Chama o cadastro de código de barras
                FrmBarra(true);
            }
            else if (acesso[0].escreverFuncao == false && acesso[0].editarFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Chama o cadastro de código de barras
                FrmBarra(true);
            }
        }

        private void mniatualizarDados_Click(object sender, EventArgs e)
        {
            if (perfilUsuario.Equals("ADMINISTRADOR"))
            {
                //Chama o cadastro de código de barras
                FrmBarra(false);
            }
            else if (acesso[0].escreverFuncao == false && acesso[0].editarFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Chama o cadastro de código de barras
                FrmBarra(false);
            }
        }

        private void mniDeletarBarra_Click(object sender, EventArgs e)
        {
            ExcluirBarra();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa os produtos de acordo com o filtro
            PesqProdutos();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            /*if (acesso[0].escreverFuncao == false && acesso[0].editarFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {*/
            //Salva os dados logísticos
            AlterarProduto();
            //}
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
                if (!txtPesqFornecedor.Text.Equals(""))
                {
                    //Instância o negocios
                    FornecedorNegocios pesqFornecedorNegocios = new FornecedorNegocios();
                    //Instância a coleçãO
                    FornecedorCollection fornecedorCollection = new FornecedorCollection();
                    //A coleção recebe o resultado da consulta
                    fornecedorCollection = pesqFornecedorNegocios.pesqFornecedor(cmbEmpresa.Text,"", Convert.ToInt32(txtPesqFornecedor.Text));

                    if (fornecedorCollection.Count > 0)
                    {
                        //Limpa o nome do fornecedor
                        txtPesqNmFornecedor.Clear();
                        //Seta o nome do fornecedor
                        txtPesqNmFornecedor.Text = fornecedorCollection[0].nomeFornecedor;

                        //Foca no campo codigo de barra
                        txtPesqCodBarra.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum fornecedor encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //Foca no campo codigo de barra
                    txtPesqCodBarra.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa as categorias cadastradas
        private void PesqCategorias()
        {
            try
            {
                //Instância a coleção
                CategoriaCollection categoriaCollection = new CategoriaCollection();
                //Instância o negocios
                CategoriaNegocios categoriaNegocios = new CategoriaNegocios();
                //Limpa o combobox de cadastro
                cmbCategoria.Items.Clear();
                //Limpa o combobox de pesquisa
                cmbPesqCategoria.Items.Clear();

                //Preenche a coleção com a pesquisa
                categoriaCollection = categoriaNegocios.PesqCategoria();
                //Preenche o combobox categoria de cadastro
                categoriaCollection.ForEach(n => cmbCategoria.Items.Add(n.descCategoria));
                //Preenche o combobox categori de pesquisa
                categoriaCollection.ForEach(n => cmbPesqCategoria.Items.Add(n.descCategoria));

                //Define o tamanho do array para o combobox
                categoria = new int[categoriaCollection.Count];

                for (int i = 0; i < categoriaCollection.Count; i++)
                {
                    //Preenche o array combobox
                    categoria[i] = categoriaCollection[i].codCategoria;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa as unidades cadastradas
        private void PesqUnidades()
        {
            try
            {
                //Instância a coleção
                UnidadeCollection unidadeCollection = new UnidadeCollection();
                //Instância o negocios
                UnidadeNegocios unidadeNegocios = new UnidadeNegocios();
                //Limpa o combobox
                cmbUndCompra.Items.Clear();
                cmbUndPulmao.Items.Clear();
                cmbUndPicking.Items.Clear();

                //Preenche a coleção com a pesquisa
                unidadeCollection = unidadeNegocios.PesqUnidade();
                //Preenche o combobox
                unidadeCollection.ForEach(n => cmbUndCompra.Items.Add(n.unidade));
                unidadeCollection.ForEach(n => cmbUndPulmao.Items.Add(n.unidade));
                unidadeCollection.ForEach(n => cmbUndPicking.Items.Add(n.unidade));

                //Define o tamanho do array para o combobox
                compra = new int[unidadeCollection.Count];
                pulmao = new int[unidadeCollection.Count];
                picking = new int[unidadeCollection.Count];

                for (int i = 0; i < unidadeCollection.Count; i++)
                {
                    //Preenche o array combobox
                    compra[i] = unidadeCollection[i].codUnidade;
                    pulmao[i] = unidadeCollection[i].codUnidade;
                    picking[i] = unidadeCollection[i].codUnidade;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o produto (chamado também de outro frame)
        public void PesqProdutos()
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
                    //Instância o objeto de negocios
                    ProdutoNegocios produtoNegocios = new ProdutoNegocios();
                    //Instância a coleção 
                    ProdutoCollection produtoCollection = new ProdutoCollection();
                    //A coleção recebe o resultado da consulta
                    produtoCollection = produtoNegocios.PesqProduto(txtPesqCodigo.Text, txtPesqDescricao.Text, txtPesqFornecedor.Text, txtPesqCodBarra.Text, cmbPesqCategoria.Text, chkPesqAtivo.Checked, cmbEmpresa.Text);
                    //Limpa o grid
                    gridProduto.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    produtoCollection.ForEach(n => gridProduto.Rows.Add(n.idProduto, n.codProduto, n.descProduto, n.codFornecedor, n.nomeFornecedor,
                    n.codCategoria, n.descCategoria, n.multiploProduto, n.tipoArmazenagem, n.shelfLife, n.tolerancia, n.nivelMaximo, n.tipoPalete, n.pesoProduto, n.lastroPequeno, n.alturaPequeno,
                    n.lastroMedio, n.alturaMedio, n.lastroGrande, n.alturaGrande, n.lastroBlocado, n.alturaBlocado, n.fatorCompra, n.undCompra,
                    n.fatorPulmao, n.undPulmao, n.fatorPicking, n.undPicking, n.status, n.auditaFlowrack, n.controlaValidade,
                    n.paleteBlocado, n.paletePadrao, n.separacaoFlowrack, n.pesoVariavel));

                    if (produtoCollection.Count > 0)
                    {
                        if (caminho == null)
                        {
                            //pesquisa o caminho das imagens do produto
                            PesqCaminhoImagem();
                        }

                        if (cmbCategoria.Items.Count == 0)
                        {
                            //Preenche o combobox categoria
                            PesqCategorias();
                        }

                        if (cmbUndCompra.Items.Count == 0)
                        {
                            //Preenche o combobox unidade
                            PesqUnidades();
                        }

                        //pesquisa o código de barra
                        PesqCodBarra();
                        //Seta os dados nos campos
                        DadosCampos();

                        //Qtd de categoria encontrada
                        lblQtd.Text = gridProduto.RowCount.ToString();

                        //Seleciona a primeira linha do grid
                        gridProduto.CurrentCell = gridProduto.Rows[0].Cells[1];
                        //Foca no grid
                        gridProduto.Focus();
                    }
                    else
                    {
                        LimparCampos();
                        MessageBox.Show("Produto não encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Altera dados logíitocos do produto
        private void AlterarProduto()
        {
            try
            {
                if (cmbEmpresa.Text.Equals("") || cmbArmazenamento.Text.Equals("") || cmbPalete.Text.Equals("") || txtLimiteRecebimento.Equals("") || txtNivel.Value.Equals(0) ||
                    txtTotalPeq.Text.Equals("0") || txtTotalMed.Text.Equals("0") || txtTotalGrd.Text.Equals("0") || cmbUndCompra.Text.Equals("") ||
                    txtFatorPulmao.Value.Equals(0) || cmbUndPulmao.Text.Equals("") || txtFatorPicking.Value.Equals(0) ||
                    cmbUndPicking.Text.Equals(""))
                {
                    MessageBox.Show("Preencha todos os campos!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (chkPaleteBlocado.Checked == true && txtTotalBlc.Text.Equals("0"))
                {
                    MessageBox.Show("Preencha as informações para o palete blocado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objêto
                    ProdutoNegocios produtoNegocios = new ProdutoNegocios();

                    // Instância as linha da tabela
                    DataGridViewRow linha = gridProduto.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Objêto produto - alterações
                    Produto produto = new Produto();

                    produto.idProduto = Convert.ToInt32(gridProduto.Rows[indice].Cells[0].Value);
                    produto.codCategoria = codCategoria;
                    produto.tipoArmazenagem = cmbArmazenamento.Text;
                    produto.tipoPalete = cmbPalete.Text;
                    produto.shelfLife = Convert.ToInt32(txtShelfLife.Text);
                    produto.tolerancia = Convert.ToInt32(txtTolerancia.Text);
                    produto.nivelMaximo = Convert.ToInt32(txtNivel.Value);
                    produto.lastroPequeno = Convert.ToInt32(txtLastroPeq.Text);
                    produto.alturaPequeno = Convert.ToInt32(txtAlturaPeq.Text);
                    produto.lastroMedio = Convert.ToInt32(txtLastroMed.Text);
                    produto.alturaMedio = Convert.ToInt32(txtAlturaMed.Text);
                    produto.lastroGrande = Convert.ToInt32(txtLastroGrd.Text);
                    produto.alturaGrande = Convert.ToInt32(txtAlturaGrd.Text);
                    produto.lastroBlocado = Convert.ToInt32(txtLastroBlc.Text);
                    produto.alturaBlocado = Convert.ToInt32(txtAlturaBlc.Text);
                    produto.fatorCompra = Convert.ToInt32(txtFatorCompra.Text);
                    produto.codUndCompra = Convert.ToInt32(compra[cmbUndCompra.SelectedIndex]);
                    produto.fatorPulmao = Convert.ToInt32(txtFatorPulmao.Value);
                    produto.codUndPulmao = codPulmao;
                    produto.fatorPicking = Convert.ToInt32(txtFatorPicking.Value);
                    produto.codUndPicking = codPicking;
                    produto.multiploProduto = Convert.ToInt32(txtMultiplo.Text);
                    produto.status = Convert.ToBoolean(chkAtivo.Checked);
                    produto.auditaFlowrack = chkAudita.Checked;
                    produto.controlaValidade = chkControlaValidade.Checked;
                    produto.separacaoFlowrack = chkSeparacaoFlowrack.Checked;
                    produto.paletePadrao = chkPaletePadrao.Checked;
                    produto.paleteBlocado = chkPaleteBlocado.Checked;
                    produto.pesoVariavel = chkPesoVariavel.Checked;
                    produto.conferirCaixa = chkConferirCaixa.Checked;

                    //Altera 
                    produtoNegocios.Alterar(cmbEmpresa.Text, produto);
                    //Insere no rastreamento o novo cadastro
                    produtoNegocios.InserirRastreamento(produto, codUsuario, cmbEmpresa.Text);

                    //Altera os dados no grid  
                    gridProduto.Rows[indice].Cells[6].Value = cmbCategoria.Text;
                    gridProduto.Rows[indice].Cells[8].Value = cmbArmazenamento.SelectedItem;
                    gridProduto.Rows[indice].Cells[9].Value = txtShelfLife.Text;
                    gridProduto.Rows[indice].Cells[10].Value = txtTolerancia.Text;
                    gridProduto.Rows[indice].Cells[11].Value = txtNivel.Value;
                    gridProduto.Rows[indice].Cells[12].Value = cmbPalete.Text;
                    gridProduto.Rows[indice].Cells[14].Value = txtLastroPeq.Text;
                    gridProduto.Rows[indice].Cells[15].Value = txtAlturaPeq.Text;
                    gridProduto.Rows[indice].Cells[16].Value = txtLastroMed.Text;
                    gridProduto.Rows[indice].Cells[17].Value = txtAlturaMed.Text;
                    gridProduto.Rows[indice].Cells[18].Value = txtLastroGrd.Text;
                    gridProduto.Rows[indice].Cells[19].Value = txtAlturaGrd.Text;
                    gridProduto.Rows[indice].Cells[20].Value = txtLastroBlc.Text;
                    gridProduto.Rows[indice].Cells[21].Value = txtAlturaBlc.Text;
                    gridProduto.Rows[indice].Cells[23].Value = cmbUndCompra.SelectedItem;
                    gridProduto.Rows[indice].Cells[24].Value = txtFatorPulmao.Value;
                    gridProduto.Rows[indice].Cells[25].Value = cmbUndPulmao.SelectedItem;
                    gridProduto.Rows[indice].Cells[26].Value = txtFatorPicking.Value;
                    gridProduto.Rows[indice].Cells[27].Value = cmbUndPicking.SelectedItem;
                    gridProduto.Rows[indice].Cells[29].Value = chkAudita.Checked;
                    gridProduto.Rows[indice].Cells[30].Value = chkControlaValidade.Checked;
                    gridProduto.Rows[indice].Cells[31].Value = chkPaleteBlocado.Checked;
                    gridProduto.Rows[indice].Cells[32].Value = chkPaletePadrao.Checked;
                    gridProduto.Rows[indice].Cells[33].Value = chkSeparacaoFlowrack.Checked;
                    gridProduto.Rows[indice].Cells[34].Value = chkPesoVariavel.Checked;
                    gridProduto.Rows[indice].Cells[35].Value = chkConferirCaixa.Checked;

                    //Desabilita todos os campos
                    DesabilitarCampos();

                    MessageBox.Show("Dados logísticos salvos com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o código de barra
        private void PesqCodBarra()
        {
            try
            {
                //Instância o objeto de negocios
                ProdutoNegocios produtoNegocios = new ProdutoNegocios();
                //A coleção recebe o resultado da consulta
                barraCollection = produtoNegocios.PesqCodBarra(chkPesqAtivo.Checked, cmbEmpresa.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Exibe o cadastro de código de barras
        private void FrmBarra(bool opcao)
        {
            try
            {

                //Instância o frame
                FrmBarra frame = new FrmBarra();

                if (opcao == true)//Cadastra um novo código de barra
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridProduto.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    frame.opcao = opcao;
                    frame.empresaSigla = cmbEmpresa.Text;
                    //Passa o id do produto
                    frame.idProduto = Convert.ToInt32(gridProduto.Rows[indice].Cells[0].Value);

                    //Recebe as informações
                    if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        gridCodBarra.Rows.Add(
                            frame.idProduto,
                            0,
                            frame.numeroBarra,
                            frame.multiplicador,
                            string.Format(@"{0:N}", frame.altura),
                            string.Format(@"{0:N}", frame.largura),
                            string.Format(@"{0:N}", frame.comprimento),
                            string.Format(@"{0:0.00000}", frame.cubagem),
                            string.Format(@"{0:0.000}", frame.peso,
                            
                        "INTERNO"));

                        //Atualiza a coleção
                        PesqCodBarra();
                    }
                }
                else if (opcao == false)
                {
                    //envia as informações
                    if (gridCodBarra.Rows.Count > 0)
                    {
                        //Instância as linha da tabela
                        DataGridViewRow linha = gridCodBarra.CurrentRow;
                        //Recebe o indice   
                        int indice = linha.Index;


                        //Passa os valores cadastrados
                        frame.opcao = opcao;
                        frame.idProduto = Convert.ToInt32(gridCodBarra.Rows[indice].Cells[0].Value);
                        frame.codBarra = Convert.ToInt32(gridCodBarra.Rows[indice].Cells[1].Value);
                        frame.numeroBarra = Convert.ToString(gridCodBarra.Rows[indice].Cells[2].Value);
                        frame.multiplicador = Convert.ToInt32(gridCodBarra.Rows[indice].Cells[3].Value);
                        frame.altura = Convert.ToDouble(gridCodBarra.Rows[indice].Cells[4].Value);
                        frame.largura = Convert.ToDouble(gridCodBarra.Rows[indice].Cells[5].Value);
                        frame.comprimento = Convert.ToDouble(gridCodBarra.Rows[indice].Cells[6].Value);
                        frame.peso = Convert.ToDouble(gridCodBarra.Rows[indice].Cells[8].Value);
                        frame.empresaSigla = cmbEmpresa.Text;

                        //Recebe as informações
                        if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //Recebe os valores novos
                            gridCodBarra.Rows[indice].Cells[4].Value = string.Format(@"{0:N}", frame.altura);
                            gridCodBarra.Rows[indice].Cells[5].Value = string.Format(@"{0:N}", frame.largura);
                            gridCodBarra.Rows[indice].Cells[6].Value = string.Format(@"{0:N}", frame.comprimento);
                            gridCodBarra.Rows[indice].Cells[7].Value = string.Format(@"{0:0.00000}", frame.cubagem);
                            gridCodBarra.Rows[indice].Cells[8].Value = string.Format(@"{0:0.000}", frame.peso);
                            //Atualiza a coleção
                            PesqCodBarra();

                        }

                    }




                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao chamar o cadastro de código de barras!"+ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Exibe os dados no campos
        private void DadosCampos()
        {
            try
            {
                if (gridProduto.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridProduto.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = gridProduto.Rows[indice].Cells[1].Value.ToString();
                    //Seta o valor da descrição
                    txtDescricao.Text = gridProduto.Rows[indice].Cells[2].Value.ToString();
                    //Seta o valor do código do fornecedor
                    txtCodFornecedor.Text = gridProduto.Rows[indice].Cells[3].Value.ToString();
                    //Seta o valor o nome do fornecedor
                    txtNmFornecedor.Text = gridProduto.Rows[indice].Cells[4].Value.ToString();
                    //Seta o valor descricao categoria
                    cmbCategoria.Text = Convert.ToString(gridProduto.Rows[indice].Cells[6].Value);
                    //Seta o valor do multiplo de venda
                    txtMultiplo.Text = Convert.ToString(gridProduto.Rows[indice].Cells[7].Value);
                    //Seta o valor tipo de armazengem
                    cmbArmazenamento.Text = Convert.ToString(gridProduto.Rows[indice].Cells[8].Value);
                    //Seta o valor do shelf life
                    txtShelfLife.Text = Convert.ToString(gridProduto.Rows[indice].Cells[9].Value);
                    //Seta o valor da tolerância
                    txtTolerancia.Text = Convert.ToString(gridProduto.Rows[indice].Cells[10].Value);
                    //Seta o valor do nível
                    txtNivel.Text = Convert.ToString(gridProduto.Rows[indice].Cells[11].Value);
                    //Seta o valor do nível
                    cmbPalete.Text = Convert.ToString(gridProduto.Rows[indice].Cells[12].Value);

                    //Seta o valor no lastro pequeno
                    txtLastroPeq.Text = Convert.ToString(gridProduto.Rows[indice].Cells[14].Value);
                    //Seta o valor no altura pequeno
                    txtAlturaPeq.Text = Convert.ToString(gridProduto.Rows[indice].Cells[15].Value);
                    //Seta o valor total pequeno
                    txtTotalPeq.Text = Convert.ToString(Convert.ToInt32(gridProduto.Rows[indice].Cells[14].Value) * Convert.ToInt32(gridProduto.Rows[indice].Cells[15].Value));
                    //Seta o peso do palete pequeno
                    txtPesoPeq.Text = Convert.ToString(Convert.ToInt32(txtTotalPeq.Text) * Convert.ToInt32(gridProduto.Rows[indice].Cells[13].Value));

                    //Seta o valor no lastro médio
                    txtLastroMed.Text = Convert.ToString(gridProduto.Rows[indice].Cells[16].Value);
                    //Seta o valor no altura médio
                    txtAlturaMed.Text = Convert.ToString(gridProduto.Rows[indice].Cells[17].Value);
                    //Seta o valor total médio
                    txtTotalMed.Text = Convert.ToString(Convert.ToInt32(gridProduto.Rows[indice].Cells[16].Value) * Convert.ToInt32(gridProduto.Rows[indice].Cells[17].Value));
                    //Seta o peso do palete médio
                    txtPesoMed.Text = Convert.ToString(Convert.ToInt32(txtTotalMed.Text) * Convert.ToInt32(gridProduto.Rows[indice].Cells[13].Value));

                    //Seta o valor no lastro grande
                    txtLastroGrd.Text = Convert.ToString(gridProduto.Rows[indice].Cells[18].Value);
                    //Seta o valor no altura grande
                    txtAlturaGrd.Text = Convert.ToString(gridProduto.Rows[indice].Cells[19].Value);
                    //Seta o valor total grande
                    txtTotalGrd.Text = Convert.ToString(Convert.ToInt32(gridProduto.Rows[indice].Cells[18].Value) * Convert.ToInt32(gridProduto.Rows[indice].Cells[19].Value));
                    //Seta o peso do palete grande
                    txtPesoGrd.Text = Convert.ToString(Convert.ToInt32(txtTotalGrd.Text) * Convert.ToInt32(gridProduto.Rows[indice].Cells[13].Value));

                    //Seta o valor no lastro blocado
                    txtLastroBlc.Text = Convert.ToString(gridProduto.Rows[indice].Cells[20].Value);
                    //Seta o valor no altura blocado
                    txtAlturaBlc.Text = Convert.ToString(gridProduto.Rows[indice].Cells[21].Value);
                    //Seta o valor total blocado
                    txtTotalBlc.Text = Convert.ToString(Convert.ToInt32(gridProduto.Rows[indice].Cells[20].Value) * Convert.ToInt32(gridProduto.Rows[indice].Cells[21].Value));
                    //Seta o peso do palete blocado
                    txtPesoBlc.Text = Convert.ToString(Convert.ToInt32(txtTotalBlc.Text) * Convert.ToInt32(gridProduto.Rows[indice].Cells[13].Value));

                    //Seta o fator de compra
                    txtFatorCompra.Text = Convert.ToString(gridProduto.Rows[indice].Cells[22].Value);
                    //Seta  und de compra
                    cmbUndCompra.Text = Convert.ToString(gridProduto.Rows[indice].Cells[23].Value);

                    //Seta o fator do pulmão
                    txtFatorPulmao.Value = Convert.ToInt32(gridProduto.Rows[indice].Cells[24].Value);
                    //Seta  und de pulmão
                    cmbUndPulmao.Text = Convert.ToString(gridProduto.Rows[indice].Cells[25].Value);

                    //Seta o fator do picking
                    txtFatorPicking.Value = Convert.ToInt32(gridProduto.Rows[indice].Cells[26].Value);
                    //Seta  und do picking
                    cmbUndPicking.Text = Convert.ToString(gridProduto.Rows[indice].Cells[27].Value);

                    //Seta o status
                    chkAtivo.Checked = Convert.ToBoolean(gridProduto.Rows[indice].Cells[28].Value);
                    //Seta auditoria do flowrack
                    chkAudita.Checked = Convert.ToBoolean(gridProduto.Rows[indice].Cells[29].Value);
                    //Seta o controle de validade
                    chkControlaValidade.Checked = Convert.ToBoolean(gridProduto.Rows[indice].Cells[30].Value);
                    //Seta o palete blocado
                    chkPaleteBlocado.Checked = Convert.ToBoolean(gridProduto.Rows[indice].Cells[31].Value);
                    //seta o palete padrão
                    chkPaletePadrao.Checked = Convert.ToBoolean(gridProduto.Rows[indice].Cells[32].Value);
                    //seta a separação flowrack
                    chkSeparacaoFlowrack.Checked = Convert.ToBoolean(gridProduto.Rows[indice].Cells[33].Value);
                    //seta a peso variável
                    chkPesoVariavel.Checked = Convert.ToBoolean(gridProduto.Rows[indice].Cells[34].Value);
                    //seta a peso variável
                    chkConferirCaixa.Checked = Convert.ToBoolean(gridProduto.Rows[indice].Cells[35].Value);

                    //Limpa o grid
                    gridCodBarra.Rows.Clear();

                    //Localizando o código de barra do produto
                    List<Barra> barra = barraCollection.FindAll(delegate (Barra n) { return n.idProduto == Convert.ToInt32(gridProduto.Rows[indice].Cells[0].Value); }).OrderBy(b => b.multiplicador).ToList();

                    //grid recebe o resultado da coleção
                    barra.ForEach(n => gridCodBarra.Rows.Add(n.idProduto, n.codBarra, n.numeroBarra, n.multiplicador, string.Format(@"{0:N}", n.altura), string.Format(@"{0:N}", n.largura),
                    string.Format(@"{0:N}", n.comprimento), string.Format(@"{0:0.00000}", n.cubagem), string.Format(@"{0:0.000}", n.peso), n.tipo));

                    //Desabilita todos os campos
                    DesabilitarCampos();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void LimparCampos()
        {
            //Seta o valor do código
            txtCodigo.Text = string.Empty;
            //Seta o valor da descrição
            txtDescricao.Text = string.Empty;
            //Seta o valor do código do fornecedor
            txtCodFornecedor.Text = string.Empty;
            //Seta o valor o nome do fornecedor
            txtNmFornecedor.Text = string.Empty;
            //Seta o valor descricao categoria
            cmbCategoria.Text = string.Empty;
            //Seta o valor do multiplo de venda
            txtMultiplo.Text = string.Empty;
            //Seta o valor tipo de armazengem
            cmbArmazenamento.Text = string.Empty;
            //Seta o valor do shelf life
            txtShelfLife.Text = "0";
            //Seta o valor da tolerância
            txtTolerancia.Text = "0";
            //Seta o valor do nível
            txtNivel.Text = "0";
            //Seta o valor do nível
            cmbPalete.Text = "0";

            //Seta o valor no lastro pequeno
            txtLastroPeq.Text = "0";
            //Seta o valor no altura pequeno
            txtAlturaPeq.Text = "0";
            //Seta o valor total pequeno
            txtTotalPeq.Text = "0";
            //Seta o peso do palete pequeno
            txtPesoPeq.Text = "0";

            //Seta o valor no lastro médio
            txtLastroMed.Text = "0";
            //Seta o valor no altura médio
            txtAlturaMed.Text = "0";
            //Seta o valor total médio
            txtTotalMed.Text = "0";
            //Seta o peso do palete médio
            txtPesoMed.Text = "0";

            //Seta o valor no lastro grande
            txtLastroGrd.Text = "0";
            //Seta o valor no altura grande
            txtAlturaGrd.Text = "0";
            //Seta o valor total grande
            txtTotalGrd.Text = "0";
            //Seta o peso do palete grande
            txtPesoGrd.Text = "0";

            //Seta o valor no lastro blocado
            txtLastroBlc.Text = "0";
            //Seta o valor no altura blocado
            txtAlturaBlc.Text = "0";
            //Seta o valor total blocado
            txtTotalBlc.Text = "0";
            //Seta o peso do palete blocado
            txtPesoBlc.Text = "0";

            //Seta o fator de compra
            txtFatorCompra.Text = "0";
            //Seta  und de compra
            cmbUndCompra.Text = "0";

            //Seta o fator do pulmão
            txtFatorPulmao.Value = 0;
            //Seta  und de pulmão
            cmbUndPulmao.Text = "0";

            //Seta o fator do picking
            txtFatorPicking.Value = 0;
            //Seta  und do picking
            cmbUndPicking.Text = "0";

            //Seta o status
            chkAtivo.Checked = false;
            //Seta auditoria do flowrack
            chkAudita.Checked = false;
            //Seta o controle de validade
            chkControlaValidade.Checked = false;
            //Seta o palete blocado
            chkPaleteBlocado.Checked = false;
            //seta o palete padrão
            chkPaletePadrao.Checked = false;
            //seta a separação flowrack
            chkSeparacaoFlowrack.Checked = false;
            //seta a peso variável
            chkPesoVariavel.Checked = false;
            //seta a peso variável
            chkConferirCaixa.Checked = false;

            //Limpa o grid
            gridCodBarra.Rows.Clear();
            //Desabilita todos os campos
           // DesabilitarCampos();
        }
        //Excluí o produto do endereço
        private void ExcluirBarra()
        {
            try
            {
                if (gridCodBarra.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridCodBarra.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    if (Convert.ToString(gridCodBarra.Rows[indice].Cells[9].Value).Equals(string.Empty))
                    {
                        MessageBox.Show("Você não pode excluir um código de barra que pertence ao ERP!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (MessageBox.Show("Deseja realmente excluir o código de barra?", "WMS - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Instância a camada de negocios
                        ProdutoNegocios produtoNegocios = new ProdutoNegocios();

                        produtoNegocios.DeletarCodBarra(Convert.ToInt32(gridCodBarra.Rows[indice].Cells[0].Value), Convert.ToString(gridCodBarra.Rows[indice].Cells[2].Value));
                        //Remove a linha
                        gridCodBarra.Rows.RemoveAt(indice);


                        MessageBox.Show("Exclusão realizada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }


        //Habilita os campos necessários
        private void HabilitarCampos()
        {
            //cmbCategoria.Enabled = true;
            cmbArmazenamento.Enabled = true;
            cmbCategoria.Enabled = true;
            txtShelfLife.Enabled = true;
            txtTolerancia.Enabled = true;
            txtNivel.Enabled = true;
            cmbPalete.Enabled = true;
            txtLastroPeq.Enabled = true;
            txtAlturaPeq.Enabled = true;
            txtLastroMed.Enabled = true;
            txtAlturaMed.Enabled = true;
            txtLastroGrd.Enabled = true;
            txtAlturaGrd.Enabled = true;
            txtFatorPulmao.Enabled = true;
            cmbUndCompra.Enabled = true;
            cmbUndPulmao.Enabled = true;
            txtFatorPicking.Enabled = true;
            cmbUndPicking.Enabled = true;
            chkAudita.Enabled = true;
            chkControlaValidade.Enabled = true;
            chkPaleteBlocado.Enabled = true;
            chkPaletePadrao.Enabled = true;
            chkSeparacaoFlowrack.Enabled = true;
            chkPesoVariavel.Enabled = true;
            chkConferirCaixa.Enabled = true;
            gridCodBarra.Enabled = true;

            btnSalvar.Enabled = true;

            //Habilita e desabilita o palete blocado
            if (chkPaleteBlocado.Checked)
            {
                txtLastroBlc.Enabled = true;
                txtAlturaBlc.Enabled = true;
            }

        }

        //Desabilita os campos necessários
        private void DesabilitarCampos()
        {
            //cmbCategoria.Enabled = false;
            cmbArmazenamento.Enabled = false;
            cmbCategoria.Enabled = false;
            txtShelfLife.Enabled = false;
            txtTolerancia.Enabled = false;
            txtNivel.Enabled = false;
            cmbPalete.Enabled = false;
            txtLastroPeq.Enabled = false;
            txtAlturaPeq.Enabled = false;
            txtLastroMed.Enabled = false;
            txtAlturaMed.Enabled = false;
            txtLastroGrd.Enabled = false;
            txtAlturaGrd.Enabled = false;
            txtLastroBlc.Enabled = false;
            txtAlturaBlc.Enabled = false;
            cmbUndCompra.Enabled = false;
            txtFatorPulmao.Enabled = false;
            cmbUndPulmao.Enabled = false;
            txtFatorPicking.Enabled = false;
            cmbUndPicking.Enabled = false;
            chkAudita.Enabled = false;
            chkControlaValidade.Enabled = false;
            chkPaleteBlocado.Enabled = false;
            chkPaletePadrao.Enabled = false;
            chkSeparacaoFlowrack.Enabled = false;
            chkPesoVariavel.Enabled = false;
            chkConferirCaixa.Enabled = false;
            gridCodBarra.Enabled = false;

            btnSalvar.Enabled = false;

        }

        private void somarValores(String palete)
        {
            try
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridProduto.CurrentRow;
                //Recebe o indice   
                int indice = 0;
                if (linha != null)
                {
                    indice = linha.Index;

                    if (txtLastroPeq.Text != "" && txtAlturaPeq.Text != "" && palete.Equals("P"))
                    {
                        txtTotalPeq.Text = Convert.ToString(Convert.ToInt32(txtLastroPeq.Text) * Convert.ToInt32(txtAlturaPeq.Text));

                        if (Convert.ToInt32(gridProduto.Rows[indice].Cells[13].Value) != 0)
                        {
                            txtPesoPeq.Text = Convert.ToString(Convert.ToInt32(gridProduto.Rows[indice].Cells[13].Value) * Convert.ToInt32(txtTotalPeq.Text));
                        }
                    }

                    if (txtLastroMed.Text != "" && txtAlturaMed.Text != "" && palete.Equals("M"))
                    {
                        txtTotalMed.Text = Convert.ToString(Convert.ToInt32(txtLastroMed.Text) * Convert.ToInt32(txtAlturaMed.Text));

                        if (Convert.ToInt32(gridProduto.Rows[indice].Cells[13].Value) != 0)
                        {
                            txtPesoMed.Text = Convert.ToString(Convert.ToInt32(gridProduto.Rows[indice].Cells[13].Value) * Convert.ToInt32(txtTotalMed.Text));
                        }
                    }

                    if (txtLastroGrd.Text != "" && txtAlturaGrd.Text != "" && palete.Equals("G"))
                    {
                        txtTotalGrd.Text = Convert.ToString(Convert.ToInt32(txtLastroGrd.Text) * Convert.ToInt32(txtAlturaGrd.Text));

                        if (Convert.ToInt32(gridProduto.Rows[indice].Cells[13].Value) != 0)
                        {
                            txtPesoGrd.Text = Convert.ToString(Convert.ToInt32(gridProduto.Rows[indice].Cells[13].Value) * Convert.ToInt32(txtTotalGrd.Text));
                        }
                    }

                    if (txtLastroBlc.Text != "" && txtAlturaBlc.Text != "" && palete.Equals("B"))
                    {
                        txtTotalBlc.Text = Convert.ToString(Convert.ToInt32(txtLastroBlc.Text) * Convert.ToInt32(txtAlturaBlc.Text));

                        if (Convert.ToInt32(gridProduto.Rows[indice].Cells[13].Value) != 0)
                        {
                            txtPesoBlc.Text = Convert.ToString(Convert.ToInt32(gridProduto.Rows[indice].Cells[13].Value) * Convert.ToInt32(txtTotalBlc.Text));
                        }
                    }

                    if (txtShelfLife.Text != "" && txtTolerancia.Text != "" && palete.Equals("Limite"))
                    {
                        txtLimiteRecebimento.Text = Convert.ToInt32(Convert.ToDouble(txtShelfLife.Text) - ((Convert.ToDouble(txtTolerancia.Text) / 100) * Convert.ToDouble(txtShelfLife.Text))).ToString();

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor, verifique os valores informados! ", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

        }
        //Pesquisa o caminho da imagem do produto
        private void PesqCaminhoImagem()
        {
            try
            {
                // Instância o objeto de negocios
                ProdutoNegocios produtoNegocios = new ProdutoNegocios();
                //String recebe o caminho
                caminho = produtoNegocios.PesqCaminho();

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExibirImagem()
        {
            try
            {
                pictureBox1.Image = Image.FromFile(caminho + txtCodigo.Text + ".jpg");
                //Chama o método EscalaPercentual  que retorna a imagem 
                //redimensionada e armazena-a em uma variavel do tipo Image
                Image imagemEscalonada = EscalaPercentual(pictureBox1.Image);
                //Exibe a imagem no picture box.
                pictureBox1.Image = imagemEscalonada;
                //Centraliza a imagem
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                //Retorna a lagura e altura da imagem
                //string imgSize = "Width " + imagemEscalonada.Width + " px  Height " + imagemEscalonada.Height + " px";
                //lblFornecedor.Text = imgSize;
            }
            catch (Exception)
            {
                //Limpa a imagem
                pictureBox1.Image = null;

            }
        }

        static Image EscalaPercentual(Image imgFoto)
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

        
    }
}
