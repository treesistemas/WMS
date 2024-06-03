using DocumentFormat.OpenXml.Presentation;
using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmInventario : Form
    {
        public int codUsuario; //Instância a variável responsável pelo código do usuário
        private int idProduto; //Id do produto
        private int codFornecedor; //Código do fornecedor

        public List<Empresa> empresaCollection;

        //Opção para salvar ou alterar
        bool opcao = false;

        //Instância um objêto
        Estrutura endereco = new Estrutura();

        public FrmInventario()
        {
            InitializeComponent();
        }

        private void FrmInventario_Load(object sender, EventArgs e)
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

        //KeyPress
        private void txtCodProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gridFornecedor.Rows.Count > 0 || gridRegiao.Rows.Count > 0)
                {
                    MessageBox.Show("Você só pode selecionar um tipo de inevntário rotativo!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    PesqProduto(); //Pesquisa o produto
                }
            }
        }

        private void txtCodFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gridProduto.Rows.Count > 0 || gridRegiao.Rows.Count > 0)
                {
                    MessageBox.Show("Você só pode selecionar um tipo de inventário rotativo!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    PesqFornecedor(); //Pesquisa o fornecedor
                }
            }
        }

        private void txtRegiao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gridProduto.Rows.Count > 0 || gridFornecedor.Rows.Count > 0)
                {
                    MessageBox.Show("Você só pode selecionar um tipo de inventário rotativo!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    PesqRegiaoInformacao(); //Pesquisa as informações da região
                }
            }
        }

        //Changed

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.Text.Equals("Rotativo") && txtDescricao.Enabled == true)
            {
                //Habilita os campos
                txtCodProduto.Enabled = true; //Habilita o código do produto
                txtCodFornecedor.Enabled = true; //Habilita o código do fornecedor
                txtRegiao.Enabled = true; //Habilita o campo região 

                gridProduto.Enabled = true; //Habilita o grid do produto
                gridFornecedor.Enabled = true; //Habilita o grid do fornecedor
                gridRegiao.Enabled = true; //Habilita o grid da região

                btnProduto.Enabled = true; //Habilita o botão produto
                btnFornecedor.Enabled = true; //Habilita o botão fornecedor
                btnRegiao.Enabled = true; //Habilita o botão região
            }
            else
            {
                txtCodProduto.Enabled = false; //Desabilita o código do produto
                txtCodFornecedor.Enabled = false; //Desabilita o código do fornecedor
                txtRegiao.Enabled = false; //Desabilita o campo região

                gridProduto.Enabled = false; //Desabilita o grid do produto
                gridFornecedor.Enabled = false; //Desabilita o grid do fornecedor
                gridRegiao.Enabled = false; //Desabilita o grid da região

                btnProduto.Enabled = false; //Desabilita o botão produto
                btnFornecedor.Enabled = false; //Desabilita o botão fornecedor
                btnRegiao.Enabled = false; //Desabilita o botão região
            }
        }

        private void chkImportarERP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImportarERP.Checked == true)
            {
                chkImportarERP.BackColor = Color.OrangeRed;
                chkImportarERP.ForeColor = Color.White;
            }
            else
            {
                chkImportarERP.BackColor = Color.White;
                chkImportarERP.ForeColor = Color.Black;
            }
        }

        private void chkContagemPicking_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContagemPicking.Checked == true)
            {
                chkContagemPicking.BackColor = Color.MediumSeaGreen;
                chkContagemPicking.ForeColor = Color.White;
            }
            else
            {
                chkContagemPicking.BackColor = Color.White;
                chkContagemPicking.ForeColor = Color.Black;
            }
        }

        private void chkContagemPickingFlow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContagemPickingFlow.Checked == true)
            {
                chkContagemPickingFlow.BackColor = Color.MediumSeaGreen;
                chkContagemPickingFlow.ForeColor = Color.White;
            }
            else
            {
                chkContagemPickingFlow.BackColor = Color.White;
                chkContagemPickingFlow.ForeColor = Color.Black;
            }
        }

        private void chkContagemPulmao_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContagemPulmao.Checked == true)
            {
                chkContagemPulmao.BackColor = Color.MediumSeaGreen;
                chkContagemPulmao.ForeColor = Color.White;
            }
            else
            {
                chkContagemPulmao.BackColor = Color.White;
                chkContagemPulmao.ForeColor = Color.Black;
            }
        }

        private void chkContabilizarAvarias_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContabilizarAvarias.Checked == true)
            {
                chkContabilizarAvarias.BackColor = Color.SteelBlue;
                chkContabilizarAvarias.ForeColor = Color.White;
            }
            else
            {
                chkContabilizarAvarias.BackColor = Color.White;
                chkContabilizarAvarias.ForeColor = Color.Black;
            }
        }

        private void chkContabilizarVolumes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContabilizarVolumes.Checked == true)
            {
                chkContabilizarVolumes.BackColor = Color.SteelBlue;
                chkContabilizarVolumes.ForeColor = Color.White;
            }
            else
            {
                chkContabilizarVolumes.BackColor = Color.White;
                chkContabilizarVolumes.ForeColor = Color.Black;
            }
        }

        private void chkNaoVencimento_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVencimento.Checked == true)
            {
                chkVencimento.BackColor = Color.Tomato;
                chkVencimento.ForeColor = Color.White;
            }
            else
            {
                chkVencimento.BackColor = Color.White;
                chkVencimento.ForeColor = Color.Black;
            }
        }

        private void chkNaoLote_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLote.Checked == true)
            {
                chkLote.BackColor = Color.Tomato;
                chkLote.ForeColor = Color.White;
            }
            else
            {
                chkLote.BackColor = Color.White;
                chkLote.ForeColor = Color.Black;
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            PesqInventario(); //Pesquisa o inventário
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            GerarCodigo(); //Pesquisa um novo código do inventário
        }

        private void btnProduto_Click(object sender, EventArgs e)
        {
            AdicionaProdutoGrid(); //Adiciona o produto no grid
        }

        private void btnFornecedor_Click(object sender, EventArgs e)
        {
            AdicionaFornecedorGrid(); //Adiciona o fornecedor no grid
        }

        private void btnRegiao_Click(object sender, EventArgs e)
        {
            AdicionaRegiaoGrid(); //Adiciona a região
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (opcao == true)
            {
                GerarInventario(); //Gera um novo inventário
            }
            else
            {
                //Atualiza o inventário
                AtualizarInventario();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            //Fecha o inventário
            Thread thread = new Thread(FecharInventario);
            thread.Start();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarInventario(); //Cancela o inventário
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close(); //Fecha o frame
        }

        //Pesquisa as informações região
        private void PesqRegiaoInformacao()
        {
            try
            {
                //Instância o negocios
                InventarioNegocios inventarioNegocios = new InventarioNegocios();

                //Pesquisa
                endereco = inventarioNegocios.PesqRegiaoInformacao(Convert.ToInt32(txtRegiao.Text));

                if (endereco.codRegiao > 0)
                {
                    //Seta o tipo de região
                    lblRegiao.Text = endereco.tipoRegiao;
                    //Foca no botão 
                    btnRegiao.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhuma região encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o fornecedor
        private void PesqFornecedor()
        {
            try
            {
                //Instância o negocios
                FornecedorNegocios pesqFornecedorNegocios = new FornecedorNegocios();
                //Instância a coleçãO
                FornecedorCollection fornecedorCollection = new FornecedorCollection();
                //A coleção recebe o resultado da consulta
                fornecedorCollection = pesqFornecedorNegocios.pesqFornecedor(cmbEmpresa.Text, "", Convert.ToInt32(txtCodFornecedor.Text));

                if (fornecedorCollection.Count > 0)
                {

                    //Seta os dados do fornecedor
                    codFornecedor = fornecedorCollection[0].codFornecedor;
                    lblFornecedor.Text = fornecedorCollection[0].nomeFornecedor;
                    btnFornecedor.Focus();//Foca no botão
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
                produtoCollection = produtoNegocios.PesqProduto(cmbEmpresa.Text,"", txtCodProduto.Text);

                if (produtoCollection.Count > 0)
                {
                    //Seta o id do Produto
                    idProduto = produtoCollection[0].idProduto;
                    //Seta a descrição do produto
                    lblProduto.Text = produtoCollection[0].codProduto + "-" + produtoCollection[0].descProduto;
                    btnProduto.Focus();//Foca no botão
                }
                else
                {
                    MessageBox.Show("Nenhum produto encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Adiciona o produto no grid
        private void AdicionaProdutoGrid()
        {
            if (lblProduto.Text.Equals("-"))
            {
                lblProduto.Text = "-";
                MessageBox.Show("Por favor, digite o código do produto!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (gridProduto.Rows.Count == 0)
                {
                    //Adiciona o produto no grid
                    gridProduto.Rows.Add(gridProduto.Rows.Count + 1, idProduto, lblProduto.Text);

                    //Limpa o campo
                    idProduto = 0;
                    txtCodProduto.Clear();
                    lblProduto.Text = "-";
                    txtCodProduto.Focus();
                }
                else
                {
                    //Verifica se o produto já foi digitado
                    for (int i = 0; gridProduto.Rows.Count > i; i++)
                    {
                        if (gridProduto.Rows[i].Cells[1].Value.Equals(idProduto))
                        {
                            txtCodProduto.Focus();
                            txtCodProduto.SelectAll();
                            MessageBox.Show("Produto já digitado!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                        else if (gridProduto.Rows.Count - 1 == i)
                        {
                            //Adiciona o produto no grid
                            gridProduto.Rows.Add(gridProduto.Rows.Count + 1, idProduto, lblProduto.Text);

                            //Limpa o campo
                            idProduto = 0;
                            lblProduto.Text = "-";
                            txtCodProduto.Clear();
                            txtCodProduto.Focus();
                            break;
                        }

                    }
                }
            }
        }

        //Adiciona o fornecedor no grid
        private void AdicionaFornecedorGrid()
        {
            if (lblFornecedor.Text.Equals("-"))
            {
                lblFornecedor.Text = "-";
                MessageBox.Show("Por favor, digite o código do fornecedor!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (gridFornecedor.Rows.Count == 0)
                {
                    //Adiciona o fornecedor no grid
                    gridFornecedor.Rows.Add(gridFornecedor.Rows.Count + 1, codFornecedor, lblFornecedor.Text);

                    //Limpa o campo
                    codFornecedor = 0;
                    txtCodFornecedor.Clear();
                    lblFornecedor.Text = "-";
                    txtCodFornecedor.Focus();
                }
                else
                {
                    //Verifica se o fornecedor já foi digitado
                    for (int i = 0; gridFornecedor.Rows.Count > i; i++)
                    {
                        if (gridFornecedor.Rows[i].Cells[1].Value.Equals(codFornecedor))
                        {
                            lblFornecedor.Text = "-";
                            txtCodFornecedor.Focus();
                            txtCodFornecedor.SelectAll();
                            MessageBox.Show("Fornecedor já digitado!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                        else if (gridFornecedor.Rows.Count - 1 == i)
                        {
                            //Adiciona o fornecedor no grid
                            gridFornecedor.Rows.Add(gridFornecedor.Rows.Count + 1, codFornecedor, lblFornecedor.Text);

                            //Limpa o campo
                            codFornecedor = 0;
                            txtCodFornecedor.Clear();
                            lblFornecedor.Text = "-";
                            txtCodFornecedor.Focus();
                            break;
                        }
                    }

                }
            }
        }

        //Adiciona a região no grid
        private void AdicionaRegiaoGrid()
        {
            if (lblRegiao.Text.Equals("-"))
            {
                lblRegiao.Text = "-";
                MessageBox.Show("Por favor, selecione uma região!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (gridRegiao.Rows.Count == 0)
                {
                    //Adiciona a região no grid
                    gridRegiao.Rows.Add(gridRegiao.Rows.Count + 1, endereco.codRegiao, endereco.numeroRegiao, endereco.tipoRegiao, string.Format("{0:g}", endereco.qtdPulmao),
                        string.Format("{0:g}", endereco.qtdPicking));

                    //Limpa o campo
                    lblRegiao.Text = "-";
                    txtRegiao.Clear();
                    txtRegiao.Focus();
                }
                else
                {
                    //Verifica se a região já foi digitado
                    for (int i = 0; gridRegiao.Rows.Count > i; i++)
                    {
                        if (gridRegiao.Rows[i].Cells[1].Value.Equals(endereco.codRegiao))
                        {
                            lblRegiao.Text = "-";
                            txtRegiao.Focus();
                            txtRegiao.SelectAll();
                            MessageBox.Show("Região já adicionada!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                        else if (gridRegiao.Rows.Count - 1 == i)
                        {
                            //Adiciona a região no grid
                            gridRegiao.Rows.Add(gridRegiao.Rows.Count + 1, endereco.codRegiao, endereco.numeroRegiao, endereco.tipoRegiao, string.Format("{0:g}", endereco.qtdPulmao),
                            string.Format("{0:g}", endereco.qtdPicking));

                            //Limpa o campo
                            lblRegiao.Text = "-";
                            txtRegiao.Clear();
                            txtRegiao.Focus();
                            break;
                        }
                    }

                }
            }
        }

        //Gera um novo código do inventário
        private void GerarCodigo()
        {
            try
            {
                //Instância a camada de negócios
                InventarioNegocios inventarioNegocios = new InventarioNegocios();
                //Instância um objêto
                Inventarios inventario = new Inventarios();
                //Pesquisa
                inventario = inventarioNegocios.PesqInventario(Convert.ToString(cmbEmpresa.Text));
                //Verifica se existe inventário aberto
                if (inventario.codInventario == 0)
                {
                    //Limpa os campos
                    LimparCampos();
                    //Pesquisa um novo código    
                    txtCodigo.Text = inventarioNegocios.PesqCodigo().ToString();
                    //Habilita componentes
                    HabilitarCampos();
                }
                else
                {
                    MessageBox.Show("Já existe um inventário aberto!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Pesquisa o inventário
        private void PesqInventario()
        {
            try
            {
                //Instância a camada de negocios
                InventarioNegocios inventarioNegocios = new InventarioNegocios();
                //Instância um objêto
                Inventarios inventario = new Inventarios();
                //A coleção recebe o resultado da consulta
                inventario = inventarioNegocios.PesqInventario(Convert.ToString(cmbEmpresa.Text));

                if (inventario.codInventario != 0)
                {
                    //Seta os dados nos campos
                    txtCodigo.Text = Convert.ToString(inventario.codInventario);
                    txtDescricao.Text = Convert.ToString(inventario.descInventario);
                    lblDataInicial.Text = Convert.ToString(inventario.dataInicial);
                    lblUsuario.Text = Convert.ToString(inventario.usuarioInicial);
                    cmbTipo.Text = Convert.ToString(inventario.tipoInventario);
                    cmbAuditoria.Text = Convert.ToString(inventario.tipoAuditoria);
                    chkImportarERP.Checked = Convert.ToBoolean(inventario.importarERP);
                    chkContagemPicking.Checked = Convert.ToBoolean(inventario.contPicking);
                    chkContagemPickingFlow.Checked = Convert.ToBoolean(inventario.contPickingFlow);
                    chkContagemPulmao.Checked = Convert.ToBoolean(inventario.contPulmao);
                    chkContabilizarAvarias.Checked = Convert.ToBoolean(inventario.contAvaria);
                    chkContabilizarVolumes.Checked = Convert.ToBoolean(inventario.contVolumeFlow);
                    chkVencimento.Checked = Convert.ToBoolean(inventario.contVencimento);
                    chkLote.Checked = Convert.ToBoolean(inventario.contLote);

                    if (inventario.descRotativo == "PRODUTO")
                    {
                        //Pesquisa o produto do inventário
                        PesqItensInventario();

                    }
                    else if (inventario.descRotativo == "FORNECEDOR")
                    {
                        //Pesquisa o fornecedor do inventário
                        PesqFornecedorInventario();
                    }
                    else if (inventario.descRotativo == "ENDERECO")
                    {
                        //Pesquisa os endereço do inventário
                        PesqEnderecoInventario();
                    }

                    //Habilita os campos
                    chkImportarERP.Enabled = true;
                    chkVencimento.Enabled = true;
                    chkLote.Enabled = true;

                    btnSalvar.Enabled = true;//Habilita o botão novo
                    btnFechar.Enabled = true;//Habilita o botão novo
                    btnCancelar.Enabled = true;//Habilita o botão novo

                    btnNovo.Enabled = false; //Desabilita o botão novo
                }
                else
                {
                    //Limpa os campos
                    LimparCampos();
                    MessageBox.Show("Não existe inventário aberto!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os itens do inventário aberto
        private void PesqItensInventario()
        {
            try
            {
                //Instância a camada de negocios
                InventarioNegocios inventarioNegocios = new InventarioNegocios();
                //Instância uma coleção de objêto
                ProdutoCollection produtoCollection = new ProdutoCollection();
                //A coleção recebe o resultado da consulta
                produtoCollection = inventarioNegocios.PesqItensInventario(Convert.ToInt32(txtCodigo.Text));
                //Limpa o grid
                gridProduto.Rows.Clear();
                //Grid Recebe o resultado da coleção
                produtoCollection.ForEach(n => gridProduto.Rows.Add(gridProduto.Rows.Count + 1, n.idProduto, n.codProduto + "-" + n.descProduto));

                //Seleciona a primeira página
                tabControl1.SelectedTab = tabPage1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os fornecedores do inventário aberto
        private void PesqFornecedorInventario()
        {
            try
            {
                //Instância a camada de negocios
                InventarioNegocios inventarioNegocios = new InventarioNegocios();
                //Instância uma coleção de objêto
                FornecedorCollection fornecedorCollection = new FornecedorCollection();
                //A coleção recebe o resultado da consulta
                fornecedorCollection = inventarioNegocios.PesqFornecedorInventario(Convert.ToInt32(txtCodigo.Text));
                //Limpa o grid
                gridFornecedor.Rows.Clear();
                //Grid Recebe o resultado da coleção
                fornecedorCollection.ForEach(n => gridFornecedor.Rows.Add(gridFornecedor.Rows.Count + 1, n.codFornecedor, n.codFornecedor + "-" + n.nomeFornecedor));

                //Seleciona a segunda página
                tabControl1.SelectedTab = tabPage2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os endereço do inventário aberto
        private void PesqEnderecoInventario()
        {
            try
            {
                //Instância a camada de negocios
                InventarioNegocios inventarioNegocios = new InventarioNegocios();
                //Instância uma coleção de objêto
                EstruturaCollection estruturaCollection = new EstruturaCollection();
                //A coleção recebe o resultado da consulta
                estruturaCollection = inventarioNegocios.PesqEnderecoInventario(Convert.ToInt32(txtCodigo.Text));
                //Limpa o grid
                gridRegiao.Rows.Clear();
                //Grid Recebe o resultado da coleção
                estruturaCollection.ForEach(n => gridRegiao.Rows.Add(gridRegiao.Rows.Count + 1, n.codRegiao, n.numeroRegiao, n.tipoRegiao, string.Format("{0:g}", n.qtdPulmao),
                        string.Format("{0:g}", n.qtdPicking)));

                //Seleciona a terceira página
                tabControl1.SelectedTab = tabPage3;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Gera o inventario
        private void GerarInventario()
        {
            try
            {
                if (txtDescricao.Text.Equals(""))
                {
                    MessageBox.Show("Preencha uma descrição para o inventário!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo
                    txtDescricao.Focus();
                }
                else if (cmbTipo.Text.Equals("") || cmbTipo.Text.Equals("Selecione"))
                {
                    MessageBox.Show("Por favor, selecine um tipo de inventário!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo
                    cmbTipo.Focus();
                }
                else if (cmbAuditoria.Text.Equals("") || cmbAuditoria.Text.Equals("Selecione"))
                {
                    MessageBox.Show("Por favor, selecine um tipo de auditoria para o inventário!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo
                    cmbAuditoria.Focus();
                }
                else if (cmbTipo.Text.Equals("Rotativo") && gridProduto.Rows.Count == 0 && gridFornecedor.Rows.Count == 0 && gridRegiao.Rows.Count == 0)
                {
                    MessageBox.Show("Por favor, configure um dos tipos de inventário rotativo! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string tipoRotativo = null;

                    if (cmbTipo.Text.Equals("Rotativo") && gridProduto.Rows.Count > 0)
                    {
                        tipoRotativo = "PRODUTO";
                    }
                    else if (cmbTipo.Text.Equals("Rotativo") && gridProduto.Rows.Count == 0 && gridFornecedor.Rows.Count > 0)
                    {
                        tipoRotativo = "FORNECEDOR";
                    }
                    else if (cmbTipo.Text.Equals("Rotativo") && gridProduto.Rows.Count == 0 && gridFornecedor.Rows.Count == 0 && gridRegiao.Rows.Count > 0)
                    {
                        tipoRotativo = "ENDERECO";
                    }

                    //Instância a camada de negocios
                    InventarioNegocios inventarioNegocios = new InventarioNegocios();
                    //Instância um objêto
                    Inventarios inventario = new Inventarios();

                    //Passas os dados para o objêto
                    inventario.codInventario = Convert.ToInt32(txtCodigo.Text); //código do invetário
                    inventario.descInventario = txtDescricao.Text;
                    inventario.codUsuarioInicial = codUsuario;
                    inventario.tipoInventario = cmbTipo.Text;
                    inventario.descRotativo = tipoRotativo;
                    inventario.tipoAuditoria = cmbAuditoria.Text;
                    inventario.importarERP = chkImportarERP.Checked;
                    inventario.contPicking = chkContagemPicking.Checked;
                    inventario.contPickingFlow = chkContagemPickingFlow.Checked;
                    inventario.contPulmao = chkContagemPulmao.Checked;
                    inventario.contAvaria = chkContabilizarAvarias.Checked;
                    inventario.contVolumeFlow = chkContabilizarVolumes.Checked;
                    inventario.contVencimento = chkVencimento.Checked;
                    inventario.contLote = chkLote.Checked;


                    //Gera o inventário
                    inventarioNegocios.GeraInventario(inventario, Convert.ToString(cmbEmpresa.Text));
                    //Gera os itens do inventário
                    GerarItensInventario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Gera os itens do inventario
        private void GerarItensInventario()
        {
            try
            {
                //Array responsável pelos id
                int[] idProdutoGrid = new int[gridProduto.Rows.Count]; //Define o tamanho do array
                int[] codFornecedorGrid = new int[gridFornecedor.Rows.Count]; //Define o tamanho do array
                int[] codRegiaoGrid = new int[gridRegiao.Rows.Count]; //Define o tamanho do array

                if (cmbTipo.Text.Equals("Rotativo"))
                {
                    //Verifica os produtos selecionadas
                    if (gridProduto.Rows.Count > 0)
                    {
                        //Preenche o array
                        for (int i = 0; gridProduto.Rows.Count > i; i++)
                        {
                            idProdutoGrid[i] = Convert.ToInt32(gridProduto.Rows[i].Cells[1].Value); //Passa o id do produto
                        }
                    }
                    else if (gridProduto.Rows.Count == 0 && gridFornecedor.Rows.Count > 0)
                    {
                        //Preenche o array
                        for (int i = 0; gridFornecedor.Rows.Count > i; i++)
                        {
                            codFornecedorGrid[i] = Convert.ToInt32(gridFornecedor.Rows[i].Cells[1].Value); //Passa o código do fornecedor                        }
                        }
                    }
                    else if (gridProduto.Rows.Count == 0 && gridFornecedor.Rows.Count == 0 && gridRegiao.Rows.Count > 0)
                    {
                        //Preenche o array
                        for (int i = 0; gridRegiao.Rows.Count > i; i++)
                        {
                            codRegiaoGrid[i] = Convert.ToInt32(gridRegiao.Rows[i].Cells[1].Value); //Passa o código da regiao                        }
                        }
                    }
                }

                //Instância a camada de negocios
                InventarioNegocios inventarioNegocios = new InventarioNegocios();

                if (chkContagemPulmao.Checked == true)
                {
                    //Passa a objêto para a camada de negocios
                    inventarioNegocios.GeraItensPulmaoInventario(Convert.ToInt32(txtCodigo.Text), codUsuario, idProdutoGrid, codFornecedorGrid, codRegiaoGrid);
                }

                if (chkContagemPicking.Checked == true || chkContagemPickingFlow.Checked == true)
                {
                    //Passa a objêto para a camada de negocios
                    inventarioNegocios.GeraItensPickingInventario(Convert.ToInt32(txtCodigo.Text), codUsuario, chkContagemPicking.Checked, chkContagemPickingFlow.Checked, idProdutoGrid, codFornecedorGrid, codRegiaoGrid);
                }

                //Gera um histórico do inventário com o picking
                inventarioNegocios.GeraHistoricoItensPicking(Convert.ToInt32(txtCodigo.Text), idProdutoGrid, codFornecedorGrid, codRegiaoGrid);
                //Gera um histórico do inventário com o produtos que existe no pulmão e que não existe no picking
                inventarioNegocios.GeraHistoricoItensPulmao(Convert.ToInt32(txtCodigo.Text), idProdutoGrid, codFornecedorGrid, codRegiaoGrid);

                //Desabilita todos os campos
                DesablitarCampos();

                MessageBox.Show("Inventário gerado com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Atualiza o inventário
        private void AtualizarInventario()
        {
            try
            {
                if (MessageBox.Show("Você deseja realmente atualizar o inventário?", "Wms - Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //Instância a camada de negocios
                    InventarioNegocios inventarioNegocios = new InventarioNegocios();
                    //Passa a objêto para a camada de negocios
                    inventarioNegocios.AtualizaInventario(Convert.ToInt32(txtCodigo.Text), chkImportarERP.Checked, chkVencimento.Checked, chkLote.Checked);

                    //Habilita o botão novo
                    btnNovo.Enabled = true;
                    MessageBox.Show("Inventário atualizado com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Fecha o inventário
        private void FecharInventario()
        {
            try
            {
                if (MessageBox.Show("Você deseja realmente fechar o inventário?", "Wms - Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    
                    //Instância a camada de negocios
                    InventarioNegocios inventarioNegocios = new InventarioNegocios();
                    //Instância um array
                    //posição 0 = contagem 1 do picking , posição 1 = contagem 1 do pulmão,  posição 2 = contagem 2, posição 3 = contagem 3, posição 4 = vencimento 2, posição 5 = vencimento 3, posição 6 = lote 2, posição 7 = lote 3
                    int[] dadosContagem = new int[8];

                    MsgLabel("Analisando inventário");
                    //Passa a objêto para a camada de negocios
                    dadosContagem = inventarioNegocios.AnalisarContagemInventario(Convert.ToInt32(txtCodigo.Text));

                    string mensagem = null;

                    //Verifica se existe algum endereço do picking que não foi contado na primeira contagem
                    if (dadosContagem[0] > 0)
                    {
                        mensagem = "Há " + dadosContagem[0] + " endereço(s) de picking pendente(s) na primeira contagem!\n";
                    }

                    //Verifica se existe algum endereço do pulmão que não foi contado na primeira contagem
                    if (dadosContagem[1] > 0)
                    {
                        mensagem = "Há " + dadosContagem[1] + " endereço(s) de pulmão pendente(s) na primeira contagem!\n";
                    }

                    //Verifica se existe algum endereço que não foi contado na segunda contagem
                    if (dadosContagem[2] > 0)
                    {
                        mensagem += "Há " + dadosContagem[2] + " endereço(s) pendente(s) na segunda contagem!\n";
                    }

                    //Verifica se existe algum endereço que não foi contado na terceira contagem
                    if (dadosContagem[3] > 0)
                    {
                        mensagem += "Há " + dadosContagem[3] + " endereço(s) pendente(s) para terceira contagem!\n";
                    }

                    //Se existir o controle de vencimento
                    if (chkVencimento.Checked == true)
                    {
                        //Verifica se existe algum endereço sem vencimento na segunda contagem
                        if (dadosContagem[4] > 0)
                        {
                            mensagem += "Há " + dadosContagem[4] + " endereço(s) com divergência de vencimento na segunda contagem!\n";
                        }

                        //Verifica se existe algum endereço com divergência de vencimento sem terceira contagem
                        if (dadosContagem[5] > 0)
                        {
                            mensagem += "Há " + dadosContagem[5] + " endereço(s) com divergência de vencimento na terceira contagem!\n";
                        }
                    }

                    if (chkLote.Checked == true)
                    {
                        //Verifica se existe algum endereço sem lote na segunda contagem
                        if (dadosContagem[6] > 0)
                        {
                            mensagem += "Há " + dadosContagem[6] + " endereço(s) com divergência de lote na segunda contagem!\n";
                        }

                        //Verifica se existe algum endereço com divergência de lote sem terceira contagem
                        if (dadosContagem[7] > 0)
                        {
                            mensagem += "Há " + dadosContagem[7] + " endereço(s) com divergência de lote para terceira contagem!\n";
                        }
                    }

                    if (mensagem != null)
                    {
                        //Limpa o label de processo
                        MsgLabel("");
                        MessageBox.Show(mensagem, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Exibe o texto
                        MsgLabel("Atualizando endereços de picking");
                        //Atualiza os  endereços de picking com as contagens
                        inventarioNegocios.AtualizaPicking(Convert.ToInt32(txtCodigo.Text));
                        //Exibe o texto
                        MsgLabel("Atualizando endereços do pulmão");
                        //Atualiza os endereços de pulmão com as contagens
                        inventarioNegocios.AtualizaPulmao(Convert.ToInt32(txtCodigo.Text));
                        //Exibe o texto
                        MsgLabel("Atualizando histórico do inventário");
                        //Atualiza o histórico do inventário
                        inventarioNegocios.AtualizaHistorico(Convert.ToInt32(txtCodigo.Text));
                        
                        //Fecha o inventário
                        inventarioNegocios.FecharInventario(Convert.ToInt32(txtCodigo.Text), codUsuario, "FINLIZADO");

                        Invoke((MethodInvoker)delegate ()
                        {
                            //Limpa o s campos
                            LimparCampos();
                            //Desabilita todos os campos
                            DesablitarCampos();
                        });

                        //Limpa o label de processo
                        MsgLabel("");

                        MessageBox.Show("Inventário finalizado com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Cancela o inventário
        private void CancelarInventario()
        {
            try
            {
                if (MessageBox.Show("Você deseja realmente cancelar o inventário?", "Wms - Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //Instância a camada de negocios
                    InventarioNegocios inventarioNegocios = new InventarioNegocios();
                    //Passa a objêto para a camada de negocios
                    inventarioNegocios.FecharInventario(Convert.ToInt32(txtCodigo.Text), codUsuario, "CANCELADO");

                    //Limpa os campos
                    LimparCampos();
                    //Desabilita os campos
                    DesablitarCampos();
                    //Habilita o botão novo
                    btnNovo.Enabled = true;
                    MessageBox.Show("Inventário cancelado com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Limpa os campos
        private void LimparCampos()
        {
            txtCodigo.Clear(); //Limpa o código do inventário
            txtDescricao.Clear(); //Limpa a descrição do inventário
            txtCodProduto.Clear();//Limpa o código do produto
            txtCodFornecedor.Clear(); //Limpa o código do fornecedor
            txtRegiao.Clear(); //Limpa o campo região

            cmbTipo.Text = "Selecione"; //Limpa o campo tipo do inventário
            cmbAuditoria.Text = "Selecione"; //Limpa o campo auditoria do inventário

            lblProduto.Text = "-"; //Limpa a descrição do produto
            lblFornecedor.Text = "-"; //Limpa o nome do fornecedor
            lblRegiao.Text = "-"; //Limpa a descrição da região
            lblDataInicial.Text = "-"; //Limpa a descrição da região
            lblUsuario.Text = "-"; //Limpa a descrição da região

            gridProduto.Rows.Clear(); //Limpa o grid do produto
            gridFornecedor.Rows.Clear(); //Limpa o grid do fornecedor
            gridRegiao.Rows.Clear(); //Limpa o grid da região

            chkImportarERP.Checked = false; //Desmarca a opção de importação
            chkContagemPicking.Checked = false;//Desmarca a opção de contar o picking
            chkContagemPickingFlow.Checked = false;//Desmarca a opção de contar o picking do flow rack
            chkContagemPulmao.Checked = false;//Desmarca a opção de contar o pulmão
            chkContabilizarVolumes.Checked = false;//Desmarca a opção de contabilizar o flow rack
            chkContabilizarAvarias.Checked = false;//Desmarca a opção de contabilizar a avarias
            chkVencimento.Checked = false;//Desmarca a opção de controlar o vencimento
            chkLote.Checked = false;//Desmarca a opção de controlar o lote

        }

        //Habilita os campos
        private void HabilitarCampos()
        {
            txtDescricao.Enabled = true; //Habilita a descrição do inventário    

            cmbTipo.Enabled = true; //Habilita o campo tipo do inventário
            cmbAuditoria.Enabled = true; //Habilita o campo auditoria do inventário   

            chkImportarERP.Enabled = true; //Habilita a opção de importação
            chkContagemPicking.Enabled = true;//Habilita a opção de contar o picking
            chkContagemPickingFlow.Enabled = true; //Habilita a opção de contar o picking do flow rack
            chkContagemPulmao.Enabled = true;//Habilita a opção de contar o pulmão
            chkContabilizarVolumes.Enabled = true;//Habilita a opção de contabilizar o flow rack
            chkContabilizarAvarias.Enabled = true;//Habilita a opção de contabilizar a avarias
            chkVencimento.Enabled = true;//Habilita a opção de controlar o vencimento
            chkLote.Enabled = true;//Habilita a opção de controlar o lote

            btnNovo.Enabled = false; //Desabilita o botão nono

            btnSalvar.Enabled = true; //Habilita o botão salvar

            txtDescricao.Focus(); //Foca no campo descrição

            //Controle para salvar 
            opcao = true;
        }

        //Desabilitar os campos
        private void DesablitarCampos()
        {
            txtDescricao.Enabled = false; //Desabilita a descrição do inventário        

            cmbTipo.Enabled = false; //Desabilita o campo tipo do inventário
            cmbAuditoria.Enabled = false; //Desabilita o campo auditoria do inventário     

            chkImportarERP.Enabled = false; //Desabilita a opção de importação
            chkContagemPicking.Enabled = false;//Desabilita a opção de contar o picking
            chkContagemPickingFlow.Enabled = false; //Desabilita a opção de contar o picking do flow rack
            chkContagemPulmao.Enabled = false;//Desabilita a opção de contar o pulmão
            chkContabilizarVolumes.Enabled = false;//Desabilita a opção de contabilizar o flow rack
            chkContabilizarAvarias.Enabled = false;//Desabilita a opção de contabilizar a avarias
            chkVencimento.Enabled = false;//Desabilita a opção de controlar o vencimento
            chkLote.Enabled = false;//Desabilita a opção de controlar o lote

            btnNovo.Enabled = true; //Habilita o botão nono

            btnSalvar.Enabled = false; //Desabilita o botão salvar

            //Controle para alterar 
            opcao = false;
        }

        //Exibe o texto do processo do progressbar
        private void MsgLabel(string texto)
        {
            Invoke((MethodInvoker)delegate ()
            {
                lblProcesso.Text = texto;

            });
        }

    }
}
