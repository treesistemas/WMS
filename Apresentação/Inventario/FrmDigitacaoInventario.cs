using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmDigitacaoInventario : Form
    {

        int codInventario; //Código do inventário        
        public int codUsuario; //Digitador        
        public string perfil; //Perfíl do usuário controla o enabled dos campos
        public List<Empresa> empresaCollection;


        //Controle do inventário
        bool controlaVencimento = false, controlaLote = false;

        //Instância a camada de objêto
        ItemInventario itemContagem = new ItemInventario();

        public FrmDigitacaoInventario()
        {
            InitializeComponent();
        }

        //Load
        private void FrmDigitacaoInventario_Load(object sender, EventArgs e)
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


            //Pesquisa o inventário aberto
            PesqInventario();
        }

        //KeyPress
        private void txtAuditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!txtCodAuditor.Text.Equals(""))
                {
                    //Pesquisa o auditor
                    pesqAuditor();
                }
                else
                {
                    //Foca no campo endereço
                    txtCodEndereco.Focus();
                }
            }

        }

        private void txtCodEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtCodEndereco.Text.Equals(""))
                {
                    //Foca no campo do produto
                    txtCodProduto.Focus();
                }
                else
                {
                    //Pesquisa o endereço
                    PesqEndereco();
                }
            }
        }

        private void txtCodProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtCodProduto.Text.Equals(""))
                {
                    //Foca no campo quantidade fechada
                    txtQtdFechada.Focus();
                    //Seleciona o que estiver no campo
                    txtQtdFechada.SelectAll();

                }
                else
                {
                    //Pesquisa o produto
                    PesqProduto();
                }
            }
        }

        private void txtQtdFechada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo fracionada
                txtQtdFracionada.Focus();
                //Seleciona o que estiver no campo
                txtQtdFracionada.SelectAll();
            }
        }

        private void txtQtdFracionada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo vencimento
                dtmVencimento.Focus();
                //Seleciona o que estiver no campo
                dtmVencimento.Select();
            }
        }

        private void dtmVencimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo lote
                txtLote.Focus();
                //Seleciona o que estiver no campo
                txtLote.SelectAll();
            }
        }

        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão confirmar
                btnConfirmar.Focus();
            }
        }

        //KeyDown
        private void txtAuditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqUsuario frame = new FrmPesqUsuario();
                frame.perfilUsuario = "";
                //Nome da empresa
                frame.empresa = cmbEmpresa.Text;

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe o código
                    txtCodAuditor.Text = Convert.ToString(frame.codUsuario);
                    //Receme o login
                    lblAuditor.Text = frame.nmUsuario;
                }
            }
        }

        //Changed
        private void txtCodAuditor_TextChanged(object sender, EventArgs e)
        {
            if (txtCodAuditor.Text.Length == 0)
            {
                lblAuditor.Text = string.Empty;
            }

        }
        private void rbtPrimeira_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtPrimeira.Checked == true)
            {
                //Foca no campo endereço
                txtCodEndereco.Focus();
                //Seleciona o código do endereço
                txtCodEndereco.SelectAll();
            }

        }

        private void rbtSegunda_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSegunda.Checked == true)
            {
                //Foca no campo endereço
                txtCodEndereco.Focus();
                //Seleciona o código do endereço
                txtCodEndereco.SelectAll();
            }
        }

        private void rbtTerceira_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtTerceira.Checked == true)
            {
                //Foca no campo endereço
                txtCodEndereco.Focus();
                //Seleciona o código do endereço
                txtCodEndereco.SelectAll();
            }
        }

        private void txtCodEndereco_TextChanged(object sender, EventArgs e)
        {
            if (txtCodEndereco.TextLength == 0)
            {
                lblEndereco.Text = "-";
            }
        }

        private void txtCodProduto_TextChanged(object sender, EventArgs e)
        {
            if (txtCodProduto.TextLength == 0)
            {
                lblProduto.Text = "-";
            }
        }

        //Click
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(""+ dtmVencimento.Value.Subtract(Convert.ToDateTime("14.10.2025 00:00:00")).Days);
            //Insere as contagens
            EnviarContagem();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            //Limpa os campos
            LimparCampos();
        }

        //Pesquisa o inventário aberto
        private void PesqInventario()
        {
            try
            {
                //Instância a camada de negocios
                DigitacaoInventarioNegocios digitacaoNegocios = new DigitacaoInventarioNegocios();
                //Instância o objêto
                Inventarios invetario = new Inventarios();
                //Pesquisa
                invetario = digitacaoNegocios.PesqInventario();

                if (invetario.codInventario != 0)
                {
                    //Recebe
                    codInventario = invetario.codInventario; //Código do inventário
                    controlaLote = invetario.contLote; //Controla o lote
                    controlaVencimento = invetario.contVencimento; //Controla o vencimento    
                    lblDescInventario.Text = invetario.codInventario + "-" + invetario.descInventario; //Descrição do inventário
                    //Foca no campo
                    txtCodAuditor.Focus();
                }
                else
                {
                    MessageBox.Show("Não existe inventário aberto!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o auditor
        private void pesqAuditor()
        {
            try
            {
                //Instância o negocios
                UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                //Instância a coleçãO
                UsuarioCollection usuarioCollection = new UsuarioCollection();
                //A coleção recebe o resultado da consulta
                usuarioCollection = usuarioNegocios.PesqUsuario(txtCodAuditor.Text, "", "", null);

                if (usuarioCollection.Count > 0)
                {
                    //Seta o login do usuário no campo
                    lblAuditor.Text = usuarioCollection[0].login;
                    //Foca no código do usuário
                    txtCodEndereco.Focus();
                }
                else
                {
                    //Limpa                    
                    lblAuditor.Text = "-";
                    //Seleciona
                    txtCodAuditor.SelectAll();
                    MessageBox.Show("Nenhum auditor encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o endereço
        private void PesqEndereco()
        {
            try
            {
                //Instância a camada de negocios
                DigitacaoInventarioNegocios digitacaoNegocios = new DigitacaoInventarioNegocios();
                //Instância o objêto
                Apartamento apartamento = new Apartamento();
                //Pesquisa o endereco
                apartamento = digitacaoNegocios.PesqEndereco(txtCodEndereco.Text);

                if (apartamento.descApartamento != null)
                {
                    if (apartamento.disposicaoApartamento.Equals("Nao"))
                    {
                        MessageBox.Show("O endereço " + apartamento.descApartamento + " se encontra indisponível!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Exibe o endereço
                        lblEndereco.Text = apartamento.descApartamento;
                        //Foca no campo
                        txtCodProduto.Focus();
                    }
                }
                else
                {
                    lblEndereco.Text = "-";
                    MessageBox.Show("Endereço não encontrado!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o produto
        private void PesqProduto()
        {
            try
            {
                if (txtCodEndereco.Text.Equals(""))
                {
                    MessageBox.Show("Por favor digite o endereço!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância a camada de negocios
                    DigitacaoInventarioNegocios digitacaoNegocios = new DigitacaoInventarioNegocios();
                    //Instância a camada de objêto
                    ItemInventario itensInventario = new ItemInventario();

                    //Pesquisa o produto
                    itensInventario = digitacaoNegocios.PesqProduto(codInventario, cmbEmpresa.Text, txtCodEndereco.Text, txtCodProduto.Text);

                    //Objeto recebe o objêto de pesquisa
                    itemContagem = itensInventario;

                    if (itensInventario.idProduto != 0)
                    {
                        //Verifica se o produto pertence ao picking digitado
                        if (itensInventario.tipoApartemento.Equals("Separacao") && itensInventario.idProduto != itensInventario.idProdutoPicking)
                        {
                            MessageBox.Show("O produto " + itensInventario.codProduto + "-" + itensInventario.descProduto +
                            "\nNão pertence ao endereço de picking digitado!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {

                            //Verifica se existe primeira contagem se a segunda estiver selecionada
                            if (rbtSegunda.Checked == true && itensInventario.contPrimeira == null)
                            {
                                //Seleciona a primeira contagem
                                rbtPrimeira.Checked = true;
                                MessageBox.Show("Não existe 1ª Contagem do produto " + itensInventario.codProduto + "-" + itensInventario.descProduto +
                                "\npara esse endereço.", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            //Verifica se existe segunda contagem se a terceira estiver selecionada
                            else if (rbtTerceira.Checked == true && itensInventario.contSegunda == null)
                            {
                                //Seleciona a segunda contagem
                                rbtSegunda.Checked = true;
                                MessageBox.Show("Não existe 2ª Contagem do produto " + itensInventario.codProduto + "-" + itensInventario.descProduto +
                                "\npara esse endereço.", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            //Verifica se a primeira contagem já foi digitada em uma nova tentativa de digitar a primeira novamente
                            else if (rbtPrimeira.Checked == true && itensInventario.contPrimeira != null)
                            {
                                //Limpa os campos
                                LimparCampos();
                                MessageBox.Show("1ª Contagem já registrada do produto " + itensInventario.codProduto + "-" + itensInventario.descProduto + " para esse endereço!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (rbtSegunda.Checked == true && itensInventario.contSegunda != null)
                            {
                                //Limpa os campos
                                LimparCampos();
                                MessageBox.Show("2ª Contagem já registrada do produto" + itensInventario.codProduto + "-" + itensInventario.descProduto + " para esse endereço!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (!perfil.Equals("ADMINISTRADOR"))
                                {
                                    //Verifica a contagem
                                    if (itensInventario.contagemFinal != null)
                                    {
                                        //Desabilita o campo quantidade fechada
                                        txtQtdFechada.Enabled = false;
                                        //Desabilita o campo quantidade fracionado
                                        txtQtdFracionada.Enabled = false;
                                    }

                                    //Verifica o vencimento
                                    if (itensInventario.vencFinal != null)
                                    {
                                        //Desabilita o campo vencimento
                                        dtmVencimento.Enabled = false;
                                    }

                                    //Verifica o lote
                                    if (itensInventario.loteFinal != null)
                                    {
                                        //Desabilita o campo vencimento
                                        txtLote.Enabled = false;
                                    }
                                }
                                else
                                {
                                    //Habilita o campo quantidade fechada
                                    txtQtdFechada.Enabled = true;
                                    //Habilita o campo quantidade fracionado
                                    txtQtdFracionada.Enabled = true;
                                    //Habilita o campo vencimento
                                    dtmVencimento.Enabled = true;
                                    //Habilita o campo vencimento
                                    txtLote.Enabled = true;
                                }

                                //Exibe a descrição do produto
                                lblProduto.Text = itensInventario.codProduto + "-" + itensInventario.descProduto;

                                //Verifica a unidade do produto para digitação
                                if (itensInventario.fatorPulmao == 1)
                                {
                                    //Exibe a unidade de pulmão
                                    lblUniFechada.Text = "-";
                                    //Desabilita o campo quantidade fechada
                                    txtQtdFechada.Enabled = false;
                                    //Exibe a unidade de picking
                                    lblUniFracionada.Text = itensInventario.unidadePicking;
                                    //Foca no campo fracionado
                                    txtQtdFracionada.Focus();
                                }
                                else
                                {
                                    //Exibe a unidade do pulmão
                                    lblUniFechada.Text = itensInventario.unidadePulmao;
                                    //Habilita o campo quantidade fechada
                                    txtQtdFechada.Enabled = true;
                                    //Exibe a unidade de picking
                                    lblUniFracionada.Text = itensInventario.unidadePicking;
                                    //Foca no campo
                                    txtQtdFechada.Focus();

                                }


                                //Exibe as informações
                                if (rbtTerceira.Checked == true)
                                {
                                    if (itensInventario.contagemFinal != null)
                                    {
                                        //Exibe a quantidade convertida
                                        txtQtdFechada.Text = Convert.ToString(Convert.ToInt32((itensInventario.contagemFinal - (itensInventario.contagemFinal % itensInventario.fatorPulmao)) / itensInventario.fatorPulmao));
                                        //Exibe a quantidade fracionada
                                        txtQtdFracionada.Text = Convert.ToString(itensInventario.contagemFinal % itensInventario.fatorPulmao);
                                        //Seleciona o que estiver no campo
                                        txtQtdFechada.SelectAll();
                                    }

                                    if (itensInventario.vencFinal != null)
                                    {
                                        //Exibe o vencimento 
                                        dtmVencimento.Text = Convert.ToDateTime(itensInventario.vencFinal).Date.ToString();
                                    }

                                    //Exibe o lote
                                    txtLote.Text = itensInventario.loteFinal;
                                }
                                //Foca no campo
                                txtQtdFechada.Focus();


                            }


                            /*Verifica se a segunda contagem já foi digitada em uma nova tentativa de digitar a segunda novamente
                            if (rbtSegunda.Checked == true && itensInventario.contSegunda != null)
                            {
                                if (!perfil.Equals("ADMINISTRADOR"))
                                {
                                    MessageBox.Show("2ª Contagem já registrada do produto" + itensInventario.codProduto + "-" + itensInventario.descProduto + " para esse endereço!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    //Exibe a quantidade convertida
                                    txtQtdFechada.Text = Convert.ToString(itensInventario.contSegunda / itensInventario.fatorPulmao);
                                    //Exibe a quantidade fracionada
                                    txtQtdFracionada.Text = Convert.ToString(itensInventario.contSegunda % itensInventario.fatorPulmao);
                                    //Seleciona o que estiver no campo
                                    txtQtdFechada.SelectAll();
                                    //Exibe o vencimento 
                                    dtmVencimento.Value = Convert.ToDateTime(itensInventario.vencSegunda).Date;
                                    //Exibe o lote
                                    txtLote.Text = itensInventario.loteSegundo;
                                }
                            }
                            else
                            {
                                //Exibe o produto
                                lblProduto.Text = itensInventario.codProduto + "-" + itensInventario.descProduto;
                                //Preenche a variável com o código do item inventário
                                codItemInventario = itensInventario.codItensInventario;
                                //Preenche a variável com o código da operação
                                codOperacao = itensInventario.codOperacao;
                                //Preenche a variável id do produto
                                idProduto = itensInventario.idProduto;
                                //Preenche a variável peso do produto
                                pesoProduto = itensInventario.pesoProduto;
                                //Preenche a variável fator do pulmão
                                FatorPulmao = itensInventario.fatorPulmao;

                                
                                //Verifica se a primeira contagem está igual a segunda ou há contagem três
                                if (rbtTerceira.Checked == true && itensInventario.contSegunda == itensInventario.contPrimeira || rbtTerceira.Checked == true && itensInventario.contTerceira != null)
                                {
                                    //Exibe a quantidade convertida
                                    txtQtdFechada.Text = Convert.ToString(itensInventario.contPrimeira / itensInventario.fatorPulmao);
                                    //Exibe a quantidade fracionada
                                    txtQtdFracionada.Text = Convert.ToString(itensInventario.contPrimeira % itensInventario.fatorPulmao);
                                    //Seleciona o que estiver no campo
                                    txtQtdFechada.SelectAll();

                                    if (!perfil.Equals("ADMINISTRADOR"))
                                    {
                                        //Desabilita o campo quantidade fechada
                                        txtQtdFechada.Enabled = false;
                                        //Desabilita o campo quantidade fracionado
                                        txtQtdFracionada.Enabled = false;
                                    }
                                }
                                else if (rbtTerceira.Checked == true)
                                {
                                    //Habilita o campo quantidade fechada
                                    txtQtdFechada.Enabled = true;
                                    //Habilita o campo quantidade fracionado
                                    txtQtdFracionada.Enabled = true;
                                }

                                //Verifica se a primeiro vencimento está igual ao segundo
                                if (rbtTerceira.Checked == true && itensInventario.vencSegunda == itensInventario.vencPrimeira || rbtTerceira.Checked == true && itensInventario.vencTerceira != null)
                                {

                                    //Exibe o vencimento 
                                    dtmVencimento.Value = Convert.ToDateTime(itensInventario.vencPrimeira);

                                    if (!perfil.Equals("ADMINISTRADOR"))
                                    {
                                        //Desabilita o campo vencimento
                                        dtmVencimento.Enabled = false;
                                    }
                                }
                                else
                                {
                                    //Habilita o campo vencimento
                                    dtmVencimento.Enabled = true;
                                }

                                //Verifica se a primeiro vencimento está igual ao segundo
                                if (rbtTerceira.Checked == true && itensInventario.lotePrimeiro == itensInventario.loteSegundo || rbtTerceira.Checked == true && itensInventario.loteTerceiro != null)
                                {
                                    //Exibe o lote
                                    txtLote.Text = itensInventario.lotePrimeiro;

                                    if (!perfil.Equals("ADMINISTRADOR"))
                                    {
                                        //Desabilita o campo lote
                                        txtLote.Enabled = false;
                                    }
                                }
                                else
                                {
                                    //Habilita o campo lote
                                    txtLote.Enabled = true;
                                }

                                //Controle do vencimento (opção do inventário)
                                if (controlaVencimento == false)
                                {
                                    if (itensInventario.vencimentoPulmao != null)
                                    {
                                        //Exibe o vencimento 
                                        dtmVencimento.Value = Convert.ToDateTime(itensInventario.vencimentoPulmao).Date;
                                        //Desabilita o campo
                                        dtmVencimento.Enabled = false;
                                    }
                                }
                            }
                            */
                        }

                    }
                    else
                    {
                        lblProduto.Text = "-";
                        MessageBox.Show("Produto não encontrado!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Insere as contagens
        private void EnviarContagem()
        {
            try
            {
                if (txtCodAuditor.Text.Equals(""))
                {
                    //Foca no campo
                    txtCodAuditor.Focus();
                    MessageBox.Show("Digite ou pesquise o código do auditor!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (lblEndereco.Text.Equals("-"))
                {
                    MessageBox.Show("Digite o código do endereço!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (lblProduto.Text.Equals("-"))
                {
                    MessageBox.Show("Digite o código do endereço!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (controlaLote == true)
                {
                    MessageBox.Show("Digite o lote do produto!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtQtdFechada.Enabled == false && txtQtdFracionada.Enabled == false && txtLote.Enabled == false)
                {
                    MessageBox.Show("Essa contagem não pode ser efetuada, pois já foi realizada!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //Quantidade fechada
                    int qtdFechada = 0;
                    //Quantidade fracionada
                    int qtdFracionada = 0;

                    if (!txtQtdFechada.Text.Equals(""))
                    {
                        qtdFechada = Convert.ToInt32(txtQtdFechada.Text);
                    }

                    if (!txtQtdFracionada.Text.Equals(""))
                    {
                        qtdFracionada = Convert.ToInt32(txtQtdFracionada.Text);
                    }

                    if (!(txtQtdFechada.Text.Equals("") && txtQtdFracionada.Text.Equals("")))
                    {
                        //Converte a contagem
                        int estoque = (qtdFechada * itemContagem.fatorPulmao) + qtdFracionada;

                        //Converte o peso
                        double peso = (itemContagem.pesoProduto / itemContagem.fatorPulmao) * estoque;

                        string contagem = null;

                        if (rbtPrimeira.Checked == true)
                        {
                            contagem = "1ª";
                        }
                        else if (rbtSegunda.Checked == true)
                        {
                            contagem = "2ª";
                        }
                        else if (rbtTerceira.Checked == true)
                        {
                            contagem = "3ª+";
                        }

                        //Adiciona informações
                        itemContagem.codApartamento = Convert.ToInt32(txtCodEndereco.Text);
                        itemContagem.contagemFinal = estoque;
                        itemContagem.vencFinal = Convert.ToDateTime(dtmVencimento.Text);
                        itemContagem.pesoFinal = peso;
                        itemContagem.loteFinal = txtLote.Text;
                        itemContagem.dataContagem = DateTime.Now;
                        itemContagem.usuContFinal = Convert.ToInt32(txtCodAuditor.Text);

                        //Controla a contagem final
                        bool cont = false, venc = false, lote = false;

                        if (itemContagem.contPrimeira == itemContagem.contagemFinal)
                        {
                            cont = true;
                        }
                        else if (itemContagem.contSegunda == itemContagem.contagemFinal)
                        {
                            cont = true;
                        }
                        else if (itemContagem.contTerceira == itemContagem.contagemFinal)
                        {
                            cont = true;
                        }
                        else if (itemContagem.contQuarta == itemContagem.contagemFinal)
                        {
                            cont = true;
                        }
                        else if (itemContagem.contQuinta == itemContagem.contagemFinal)
                        {
                            cont = true;
                        }
                        else if (itemContagem.contSesta == itemContagem.contagemFinal)
                        {
                            cont = true;
                        }
                        else if (itemContagem.contSetima == itemContagem.contagemFinal)
                        {
                            cont = true;
                        }
                        else if (itemContagem.contOitava == itemContagem.contagemFinal)
                        {
                            cont = true;
                        }
                        else if (itemContagem.contNona == itemContagem.contagemFinal)
                        {
                            cont = true;
                        }
                        else if (itemContagem.contDecima == itemContagem.contagemFinal)
                        {
                            cont = true;
                        }

                        //MessageBox.Show("Dia 1" + itemContagem.vencPrimeira.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days +" Dia 2 "+ itemContagem.vencPrimeira.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days);

                        //Verifica vencimento final
                        if (itemContagem.vencPrimeira == itemContagem.vencFinal || itemContagem.contagemFinal == 0 ||
                            itemContagem.vencPrimeira != null && (itemContagem.vencPrimeira.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) <= 10 && (itemContagem.vencPrimeira.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) >= -10)
                        {
                            venc = true; 
                        }
                        else if (itemContagem.vencSegunda != null && itemContagem.vencSegunda == itemContagem.vencFinal || itemContagem.contagemFinal == 0 ||
                            itemContagem.vencSegunda != null && (itemContagem.vencSegunda.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) <= 10 && (itemContagem.vencSegunda.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) >= -10)
                        {
                            venc = true;
                        }
                        else if (itemContagem.vencTerceira != null && itemContagem.vencTerceira == itemContagem.vencFinal || itemContagem.contagemFinal == 0 ||
                            itemContagem.vencTerceira != null && (itemContagem.vencTerceira.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) <= 10 && (itemContagem.vencTerceira.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) >= -10)
                        {
                            venc = true;
                        }
                        else if (itemContagem.vencQuarta != null && itemContagem.vencQuarta == itemContagem.vencFinal || itemContagem.contagemFinal == 0 ||
                            itemContagem.vencQuarta != null && (itemContagem.vencQuarta.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) <= 10 && (itemContagem.vencQuarta.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) >= -10)
                        {
                            venc = true;
                        }
                        else if (itemContagem.vencQuinta != null && itemContagem.vencQuinta == itemContagem.vencFinal || itemContagem.contagemFinal == 0 ||
                            itemContagem.vencQuinta != null && (itemContagem.vencQuinta.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) <= 10 && (itemContagem.vencQuinta.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) >= -10)
                        {
                            venc = true;
                        }
                        else if (itemContagem.vencSesta != null && itemContagem.vencSesta == itemContagem.vencFinal || itemContagem.contagemFinal == 0 ||
                            itemContagem.vencSesta != null && (itemContagem.vencSesta.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) <= 10 && (itemContagem.vencSesta.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) >= -10)
                        {
                            venc = true;
                        }
                        else if (itemContagem.vencSetima != null && itemContagem.vencSetima == itemContagem.vencFinal || itemContagem.contagemFinal == 0 ||
                            itemContagem.vencSetima != null && (itemContagem.vencSetima.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) <= 10 && (itemContagem.vencSetima.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) >= -10)
                        {
                            venc = true;
                        }
                        else if (itemContagem.vencOitava != null && itemContagem.vencOitava == itemContagem.vencFinal || itemContagem.contagemFinal == 0 ||
                            itemContagem.vencOitava != null && (itemContagem.vencOitava.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) <= 10 && (itemContagem.vencOitava.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) >= -10)
                        {
                            venc = true;
                        }
                        else if (itemContagem.vencNona != null && itemContagem.vencNona == itemContagem.vencFinal || itemContagem.contagemFinal == 0 ||
                            itemContagem.vencNona != null && (itemContagem.vencNona.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) <= 10 && (itemContagem.vencNona.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) >= -10)
                        {
                            venc = true;
                        }
                        else if (itemContagem.vencDecima != null && itemContagem.vencDecima == itemContagem.vencFinal || itemContagem.contagemFinal == 0 ||
                            itemContagem.vencDecima != null && (itemContagem.vencDecima.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) <= 10 && (itemContagem.vencDecima.Value.Subtract(Convert.ToDateTime(itemContagem.vencFinal)).Days) >= -10)
                        {
                            venc = true;
                        }                        

                        //Verifica o lote final
                        if (itemContagem.lotePrimeiro == itemContagem.loteFinal)
                        {
                            lote = true;
                        }
                        else if (itemContagem.loteSegundo == itemContagem.loteFinal)
                        {
                            lote = true;
                        }
                        else if (itemContagem.loteTerceiro == itemContagem.loteFinal)
                        {
                            lote = true;
                        }
                        else if (itemContagem.loteQuarto == itemContagem.loteFinal)
                        {
                            lote = true;
                        }
                        else if (itemContagem.loteQuinto == itemContagem.loteFinal)
                        {
                            lote = true;
                        }
                        else if (itemContagem.loteSesto == itemContagem.loteFinal)
                        {
                            lote = true;
                        }
                        else if (itemContagem.loteSetimo == itemContagem.loteFinal)
                        {
                            lote = true;
                        }
                        else if (itemContagem.loteOitavo == itemContagem.loteFinal)
                        {
                            lote = true;
                        }
                        else if (itemContagem.loteNono == itemContagem.loteFinal)
                        {
                            lote = true;
                        }
                        else if (itemContagem.loteDecimo == itemContagem.loteFinal)
                        {
                            lote = true;
                        }


                       
                        //Instância a camada de negocios
                        DigitacaoInventarioNegocios digitacaoNegocios = new DigitacaoInventarioNegocios();
                        //Instâcia a camada de objêto
                        digitacaoNegocios.EnviarContagem(itemContagem, codInventario, contagem, codUsuario, cont, venc, lote);

                        //Insere a contagem no grid de digitação
                        gridContagens.Rows.Add(gridContagens.Rows.Count + 1, contagem + " CONTAGEM", lblAuditor.Text, lblEndereco.Text,
                            lblProduto.Text, txtQtdFechada.Text + " " + lblUniFechada.Text, txtQtdFracionada.Text + " " + lblUniFracionada.Text, dtmVencimento.Text, txtLote.Text);

                        // Limpa os campos
                        LimparCampos();
                        //Foca no campo endereço
                        txtCodEndereco.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Digite uma quantidade para a contagem", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        //Limpa os campos
        private void LimparCampos()
        {
            txtCodEndereco.Clear();
            txtCodProduto.Clear();
            txtQtdFechada.Clear();
            txtQtdFracionada.Clear();
            txtLote.Clear();
            dtmVencimento.Clear();
            lblProduto.Text = "-";
            lblUniFechada.Text = "CXA";
            lblUniFracionada.Text = "UND";

            //Foca no campo endereço
            txtCodEndereco.Focus();


        }

    }
}
