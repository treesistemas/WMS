using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmTrasferenciaProduto : Form
    {
        //Código do usuario
        public int codUsuario;
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        //Instância a camada de  objêto
        Produto produto = new Produto();
        //Instância a coleção de objêto do pulmao
        EnderecoPulmaoCollection enderecoPulmaoCollection = new EnderecoPulmaoCollection();
        //Instância a coleção de objêto picking
        EnderecoPickingCollection enderecoPickingCollection = new EnderecoPickingCollection();

        public FrmTrasferenciaProduto()
        {
            InitializeComponent();
        }

        private void FrmTrasferenciaProduto_Load(object sender, EventArgs e)
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
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesqProduto();
            }
        }

        private void cmbPulmao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtQtd.Focus();
            }
        }

        private void txtNovoEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtQtd.Focus();
            }
        }

        private void txtQtd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnTransferir.Focus(); //Foca no botão
            }
        }

        private void txtEndereco_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnderecoPulmao enderecoPulmao = new EnderecoPulmao();
            //Localizando o endereço
            enderecoPulmao = enderecoPulmaoCollection.Find(delegate (EnderecoPulmao n) { return n.descEndereco1 == cmbPulmao.Text; });
            //Adiciona o endereço
            lblEnderecoOrigem.Text = cmbPulmao.Text;
            //Adiciona o fator no texto estoque
            lblEstoqueOrigem.Text = "Estoque " + "(" + Convert.ToString(enderecoPulmao.undCaixaDestino) + "):";
            //Adiciona a qtd do pulmão
            lblQtdOrigem.Text = Convert.ToString(enderecoPulmao.qtdCaixaOrigem);
            //Adiciona a reserva do endereço pulmão
            lblReservaPulmao.Text = Convert.ToString(enderecoPulmao.estoqueReservado);
            //Adiciona o peso do pulmão
            lblPesoOrigem.Text = string.Format("{0:f}", enderecoPulmao.pesoProduto1);
            //Adiciona a validade do pulmão
            lblVencimentoOrigem.Text = string.Format("{0:d}", enderecoPulmao.vencimentoProduto1);
            //Adiciona o lote do pulmão
            lblLoteOrigem.Text = enderecoPulmao.loteProduto1;
            //Adiciona o tamanho do pulmão
            lblTamanhoOrigem.Text = enderecoPulmao.tamanhoApartamento1;

            if ((enderecoPulmao.vencimentoProduto1 <= Convert.ToDateTime(lblDtProximoVencimento.Text)) && (enderecoPulmao.vencimentoProduto1 <= Convert.ToDateTime(lblVencido.Text)))
            {
                //Seta a cor vermelho
                lblVencimentoOrigem.ForeColor = Color.Red;
            }
            else if (enderecoPulmao.vencimentoProduto1 <= Convert.ToDateTime(lblDtProximoVencimento.Text))
            {
                //Seta a cor azul
                lblVencimentoOrigem.ForeColor = Color.Blue;
            }
            else
            {
                //Seta a cor vermelho
                lblVencimentoOrigem.ForeColor = Color.Black;
            }


        }

        private void rbtPulmao_CheckedChanged(object sender, EventArgs e)
        {
            lblPulmaoTexto.Text = "Pulmão:";
            //Adiciona o fator no texto estoque
            lblEstoqueOrigem.Text = "Estoque:";
            lblEnderecoOrigem.Text = "-";
            lblQtdOrigem.Text = "-";
            lblVencimentoOrigem.Text = "-";
            lblPesoOrigem.Text = "-";
            lblLoteOrigem.Text = "-";
            lblTamanhoOrigem.Text = "-";

            //Habilita o campo de endereço
            cmbPulmao.Enabled = true;
        }

        private void rbtPicking_CheckedChanged(object sender, EventArgs e)
        {
            //Picking
            if (rbtPicking.Checked == true)
            {
                //Limpa o texto selecionado
                cmbPulmao.Text = "";
                //Desabilita
                cmbPulmao.Enabled = false;

                if (rbtNovoPicking.Checked == true && rbtPicking.Checked == true)
                {
                    //Seleciona novo picking
                    rbtNovoFlowrack.Checked = true;
                }

                EnderecoPicking enderecoPicking = new EnderecoPicking();
                //Localizando o endereço
                enderecoPicking = enderecoPickingCollection.Find(delegate (EnderecoPicking n) { return n.tipoEndereco == "CAIXA"; });

                //Verifica se o objêto é diferente de null
                if (enderecoPicking != null)
                {
                    lblPulmaoTexto.Text = "Picking:";
                    //Adiciona o fator no texto estoque
                    lblEstoqueOrigem.Text = "Estoque " + "(" + Convert.ToString(enderecoPicking.unidadeEstoque) + "):";
                    lblEnderecoOrigem.Text = Convert.ToString(enderecoPicking.endereco);
                    lblQtdOrigem.Text = Convert.ToString(enderecoPicking.estoque);
                    lblVencimentoOrigem.Text = string.Format("{0:d}", enderecoPicking.vencimento);
                    lblPesoOrigem.Text = string.Format("{0:f}", enderecoPicking.peso);
                    lblLoteOrigem.Text = Convert.ToString(enderecoPicking.lote);
                    lblTamanhoOrigem.Text = Convert.ToString(enderecoPicking.tamanhoEndereco);

                    //Verifica estoque e vencimento do produto
                    VerificaEstoque(enderecoPicking.estoque, Convert.ToDateTime(enderecoPicking.vencimento));

                }
            }
        }

        private void rbtFlowRack_CheckedChanged(object sender, EventArgs e)
        {
            //Flow rack
            if (rbtFlowRack.Checked == true)
            {
                //Limpa o texto selecionado
                cmbPulmao.Text = "";
                //Desabilita
                cmbPulmao.Enabled = false;

                if (rbtNovoFlowrack.Checked == true && rbtFlowRack.Checked == true)
                {
                    //Seleciona novo picking
                    rbtNovoPicking.Checked = true;
                }

                EnderecoPicking enderecoPicking = new EnderecoPicking();
                //Localizando o endereço
                enderecoPicking = enderecoPickingCollection.Find(delegate (EnderecoPicking n) { return n.tipoEndereco == "FLOWRACK"; });

                //Verifica se o objêto é diferente de null
                if (enderecoPicking != null)
                {
                    lblPulmaoTexto.Text = "Picking:";
                    lblEnderecoOrigem.Text = Convert.ToString(enderecoPicking.endereco);
                    //Adiciona o fator no texto estoque
                    lblEstoqueOrigem.Text = "Estoque " + "(" + Convert.ToString(enderecoPicking.unidadeEstoque) + "):";
                    lblQtdOrigem.Text = Convert.ToString(enderecoPicking.estoque);
                    lblVencimentoOrigem.Text = string.Format("{0:d}", enderecoPicking.vencimento);
                    lblPesoOrigem.Text = string.Format("{0:f}", enderecoPicking.peso);
                    lblLoteOrigem.Text = Convert.ToString(enderecoPicking.lote);
                    lblTamanhoOrigem.Text = Convert.ToString(enderecoPicking.tamanhoEndereco);

                    //Verifica estoque e vencimento do produto
                    VerificaEstoque(enderecoPicking.estoque, Convert.ToDateTime(enderecoPicking.vencimento));
                }
            }
        }

        private void rbtNovoPulmao_CheckedChanged(object sender, EventArgs e)
        {
            //Pulmão para Picking
            if ((rbtNovoPulmao.Checked == true))
            {
                //Seta o texto endereço
                lblNovoEndereco.Text = "Endereço de Pulmão";
                //Habilita o campo novo endereço 
                txtNovoEndereco.Enabled = true;
                //Limpa o campo
                txtNovoEndereco.Clear();
                //Foca no campo
                txtNovoEndereco.Focus();

                lblEnderecoDestino.Text = "-";
                //Adiciona o fator no texto estoque
                lblEstoqueDestino.Text = "Estoque:";
                lblQtdDestino.Text = "-";
                lblVencimentoDestino.Text = "-";
                lblPesoDestino.Text = "-";
                lblLoteDestino.Text = "-";
                lblCapacidadeDestino.Text = "-";
                lblAbastecimentoDestino.Text = "-";
                lblTamanhoDestino.Text = "-";

            }
        }

        private void rbtNovoPicking_CheckedChanged(object sender, EventArgs e)
        {
            //Picking para Picking
            if (rbtNovoPicking.Checked == true)
            {
                EnderecoPicking enderecoPicking = new EnderecoPicking();
                //Localizando o endereço
                enderecoPicking = enderecoPickingCollection.Find(delegate (EnderecoPicking n) { return n.tipoEndereco == "CAIXA"; });

                //Seta o texto novo endereço
                lblNovoEndereco.Text = "Endereço de Picking";
                //Desabilita o campo novo endereço 
                txtNovoEndereco.Enabled = false;
                //Foca no campo
                txtQtd.Focus();

                if (rbtNovoPicking.Checked == true && rbtPicking.Checked == true)
                {

                    //Seleciona novo picking
                    rbtNovoFlowrack.Checked = true;
                    MessageBox.Show("Operação inválida", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //Verifica se o objêto é diferente de null
                    if (enderecoPicking != null)
                    {
                        //Recebe o endereço
                        txtNovoEndereco.Text = enderecoPicking.endereco;

                        lblEnderecoDestino.Text = Convert.ToString(enderecoPicking.endereco);
                        //Adiciona o fator no texto estoque
                        lblEstoqueDestino.Text = "Estoque " + "(" + Convert.ToString(enderecoPicking.unidadeEstoque) + "):";
                        lblQtdDestino.Text = Convert.ToString(enderecoPicking.estoque);
                        lblVencimentoDestino.Text = string.Format("{0:d}", enderecoPicking.vencimento);
                        lblPesoDestino.Text = string.Format("{0:f}", enderecoPicking.peso);
                        lblLoteDestino.Text = Convert.ToString(enderecoPicking.lote);
                        lblCapacidadeDestino.Text = string.Format("{0:#00}", enderecoPicking.capacidade);
                        lblAbastecimentoDestino.Text = string.Format("{0:#00}", enderecoPicking.abastecimento);
                        lblTamanhoDestino.Text = Convert.ToString(enderecoPicking.tamanhoEndereco);

                        //Verifica estoque e vencimento do produto
                        VerificaEstoque(enderecoPicking.estoque, Convert.ToDateTime(enderecoPicking.vencimento));

                    }
                }

            }

        }

        private void rbtNovoFlowRack_CheckedChanged(object sender, EventArgs e)
        {
            if ((rbtNovoFlowrack.Checked == true))
            {
                EnderecoPicking enderecoPicking = new EnderecoPicking();
                //Localizando o endereço
                enderecoPicking = enderecoPickingCollection.Find(delegate (EnderecoPicking n) { return n.tipoEndereco == "FLOWRACK"; });

                //Seta o texto
                lblNovoEndereco.Text = "Endereço de Flow Rack";
                //Desabilita o campo novo endereço 
                txtNovoEndereco.Enabled = false;
                //Foca no campo
                txtQtd.Focus();

                if (rbtNovoFlowrack.Checked == true && rbtFlowRack.Checked == true)
                {

                    //Seleciona novo picking
                    rbtNovoPicking.Checked = true;
                    MessageBox.Show("Operação inválida", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //Verifica se o objêto é diferente de null
                    if (enderecoPicking != null)
                    {
                        txtNovoEndereco.Text = enderecoPicking.endereco;

                        lblEnderecoDestino.Text = Convert.ToString(enderecoPicking.endereco);
                        //Adiciona o fator no texto estoque
                        lblEstoqueDestino.Text = "Estoque " + "(" + Convert.ToString(enderecoPicking.unidadeEstoque) + "):";
                        lblQtdDestino.Text = Convert.ToString(enderecoPicking.estoque);
                        lblVencimentoDestino.Text = string.Format("{0:d}", enderecoPicking.vencimento);
                        lblPesoDestino.Text = string.Format("{0:f}", enderecoPicking.peso);
                        lblLoteDestino.Text = Convert.ToString(enderecoPicking.lote);
                        lblCapacidadeDestino.Text = string.Format("{0:#00}", enderecoPicking.capacidade);
                        lblAbastecimentoDestino.Text = string.Format("{0:#00}", enderecoPicking.abastecimento);
                        lblTamanhoDestino.Text = Convert.ToString(enderecoPicking.tamanhoEndereco);

                        //Verifica estoque e vencimento do produto
                        VerificaEstoque(enderecoPicking.estoque, Convert.ToDateTime(enderecoPicking.vencimento));
                    }
                }


            }
        }

        private void txtQtd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int qtd = 0;

                if (txtQtd.Text == "")
                {
                    qtd = 0;
                }
                else if (Convert.ToInt32(txtQtd.Text) > 0)
                {
                    qtd = Convert.ToInt32(txtQtd.Text);
                }

                double resultado = (produto.pesoProduto * qtd);

                txtPeso.Text = string.Format("{0:f}", resultado);
            }
            catch
            {
                MessageBox.Show("Digite apenas valores numérico maior que zero", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnTransferir_Click(object sender, EventArgs e)
        {
            if (rbtPicking.Checked == true && rbtNovoFlowrack.Checked == true || rbtFlowRack.Checked == true && rbtNovoPicking.Checked == true)
            {
                //Transferência de picking para picking
                TransfenciaPickingPicking();
            }
            else if ((rbtPulmao.Checked == true && rbtNovoPicking.Checked == true) || (rbtPulmao.Checked == true && rbtNovoFlowrack.Checked == true))
            {
                //Transferência de pulmão para picking
                TransferenciaPulmaoPicking();
            }
            else if ((rbtPicking.Checked == true && rbtNovoPulmao.Checked == true) || (rbtFlowRack.Checked == true && rbtNovoPulmao.Checked == true))
            {
                //Transferência de picking para pulmão 
                TransferenciaPickingPulmao(txtNovoEndereco.Text);
            }
            else if ((rbtPulmao.Checked == true) && rbtNovoPulmao.Checked == true)
            {
                //Transferência de pulmão para pulmão
                TransferenciaPulmaoPulmao(txtNovoEndereco.Text);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha a tela
            Close();
        }

        //Pesquisa o produto
        private void PesqProduto()
        {
            try
            {
                //Instância a camada de negocios
                TransferenciaNegocios transferenciaNegocios = new TransferenciaNegocios();
                //Executa a consulta
                produto = transferenciaNegocios.PesqProduto(txtCodigo.Text, cmbEmpresa.Text);

                //Se o id do produto for maior que zero
                if (produto.idProduto > 0)
                {
                    //Adiciona as informções do produto
                    lblProduto.Text = Convert.ToString(produto.descProduto);
                    lblEmbalagem.Text = Convert.ToString(Convert.ToString(produto.undPulmao) + " C/" + produto.fatorPulmao + " " + produto.undPicking);
                    lblCategoria.Text = Convert.ToString(produto.descCategoria);
                    lblArmazenagem.Text = Convert.ToString(produto.tipoArmazenagem);
                    lblNivel.Text = Convert.ToString(produto.nivelMaximo);

                    if (Convert.ToString(produto.tipoPalete).Equals("PP"))
                    {
                        lblPaletePadrao.Text = Convert.ToString(produto.tipoPalete) +
                        " ( " + Convert.ToString(produto.totalPequeno) + " " + Convert.ToString(produto.undPulmao) + " )";
                    }
                    else if (Convert.ToString(produto.tipoPalete).Equals("PM"))
                    {
                        lblPaletePadrao.Text = Convert.ToString(produto.tipoPalete) +
                        " ( " + Convert.ToString(produto.totalMedio) + " " + Convert.ToString(produto.undPulmao) + " )";
                    }
                    else if (Convert.ToString(produto.tipoPalete).Equals("PG"))
                    {
                        lblPaletePadrao.Text = Convert.ToString(produto.tipoPalete) +
                        " ( " + Convert.ToString(produto.totalGrande) + " " + Convert.ToString(produto.undPulmao) + " )";
                    }
                    else if (Convert.ToString(produto.tipoPalete).Equals("PB"))
                    {
                        lblPaletePadrao.Text = Convert.ToString(produto.tipoPalete) +
                        " ( " + Convert.ToString(produto.totalBlocado) + " " + Convert.ToString(produto.undPulmao) + " )";
                    }



                    if (Convert.ToString(produto.controlaValidade).Equals("True"))
                    {
                        lblControlaValidade.Text = "SIM";
                    }
                    else
                    {
                        lblControlaValidade.Text = "NÃO";
                    }

                    if (Convert.ToString(produto.separacaoFlowrack).Equals("True"))
                    {
                        lblSeparacaoFlow.Text = "SIM";
                    }
                    else
                    {
                        lblSeparacaoFlow.Text = "NÃO";
                    }

                    //Adiciona a data de tolerância
                    lblDtProximoVencimento.Text = produto.dataTolerancia.ToString("dd/MM/yyyy");
                    //Adiciona a data atual mais 15 dias
                    lblVencido.Text = DateTime.Now.AddDays(15).ToString("dd/MM/yyyy");

                    //Pesquisa o picking
                    PesqPicking(produto.idProduto);

                }
                else
                {
                    //seleciona o campo
                    txtCodigo.SelectAll();
                    //Foca no campo
                    txtCodigo.Focus();
                    MessageBox.Show("Produto não encontrado! ", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - Informação!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //Método consulta de picking
        private void PesqPicking(int idProduto)
        {
            try
            {
                //Instância a camada de negocios
                TransferenciaNegocios transferenciaNegocios = new TransferenciaNegocios();
                //Executa a consulta
                enderecoPickingCollection = transferenciaNegocios.PesqPicking(idProduto, cmbEmpresa.Text);

                if (enderecoPickingCollection.Count > 0)
                {
                    //Adiciona o endereço de picking
                    txtNovoEndereco.Text = Convert.ToString(enderecoPickingCollection[0].endereco);

                    //Adiciona o endereço de picking
                    lblEnderecoDestino.Text = Convert.ToString(enderecoPickingCollection[0].endereco);
                    //Adiciona o fator no texto estoque
                    lblEstoqueDestino.Text = "Estoque " + "(" + Convert.ToString(enderecoPickingCollection[0].unidadeEstoque) + "):";
                    //Adiciona a quantidade armazenado
                    lblQtdDestino.Text = Convert.ToString(enderecoPickingCollection[0].estoque);
                    //Adiciona a validade
                    lblVencimentoDestino.Text = string.Format("{0:d}", enderecoPickingCollection[0].vencimento);
                    //Adiciona o peso
                    lblPesoDestino.Text = string.Format("{0:f}", enderecoPickingCollection[0].peso);
                    //Adiciona o lote
                    lblLoteDestino.Text = Convert.ToString(enderecoPickingCollection[0].lote);
                    //Adiciona a capacidade
                    lblCapacidadeDestino.Text = string.Format("{0:#00}", enderecoPickingCollection[0].capacidade);
                    //Adiciona a ordem de abastecimento
                    lblAbastecimentoDestino.Text = string.Format("{0:#00}", enderecoPickingCollection[0].abastecimento);
                    //Adiciona o tamnaho do endereço
                    lblTamanhoDestino.Text = Convert.ToString(enderecoPickingCollection[0].tamanhoEndereco);

                    //Desabilita o campo
                    txtCodigo.Enabled = false;
                    //Desabilita o picking do produto
                    txtNovoEndereco.Enabled = false;

                    //Pesquisa o Pulmão
                    PesqPulmao(idProduto);
                }
                else
                {
                    MessageBox.Show("Produto não possui endereço de picking", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Pesquisa os endereços do pulmão
        private void PesqPulmao(int produto)
        {
            try
            {
                //Instância a camada de negocios
                TransferenciaNegocios transferenciaNegocios = new TransferenciaNegocios();
                //Limpa a coleção de objêtos
                enderecoPulmaoCollection.Clear();
             
                //Executa a consulta
                enderecoPulmaoCollection = transferenciaNegocios.PesqPulmao(produto, cmbEmpresa.Text);

                //Adiciona a quantidade de pulmão
                lblContagemEndereco.Text = Convert.ToString(enderecoPulmaoCollection.Count);

                if (enderecoPulmaoCollection.Count > 0)
                {
                    cmbPulmao.Items.Clear();
                    //Pesquisa e adiciona no combobox pulmão
                    enderecoPulmaoCollection.ForEach(n => cmbPulmao.Items.Add(n.descEndereco1));

                    //Foca no primeiro endereco
                    cmbPulmao.SelectedIndex = 0;

                    //Foca no campo pulmão
                    cmbPulmao.Focus();
                }
                else
                {
                    MessageBox.Show("Produto não possui estoque no pulmão", "Wms - ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Verifica estoque e vencimento do produto
        private void VerificaEstoque(int? estoque, DateTime vencimento)
        {
            //Estoque Negativo
            if (estoque < 0)
            {
                //Seta a cor vermelho
                lblQtdDestino.ForeColor = Color.Red;
                //Seta a cor vermelho
                lblPesoDestino.ForeColor = Color.Red;
            }

            //Verifica o vencimento do picking
            if ((Convert.ToDateTime(vencimento) <= Convert.ToDateTime(lblDtProximoVencimento.Text)) && (Convert.ToDateTime(vencimento) <= Convert.ToDateTime(lblVencido.Text)))
            {
                //Seta a cor vermelho
                lblVencimentoDestino.ForeColor = Color.Red;
            }
            else if (Convert.ToDateTime(vencimento) <= Convert.ToDateTime(lblDtProximoVencimento.Text))
            {
                //Seta a cor azul
                lblVencimentoDestino.ForeColor = Color.Blue;
            }
            else
            {
                //Seta a cor vermelho
                lblVencimentoDestino.ForeColor = Color.Black;
            }

        }

        //Transferênci de Picking para picking
        private void TransfenciaPickingPicking()
        {
            try
            {
                //Instância a camada de negocios
                TransferenciaNegocios transferenciaNegocios = new TransferenciaNegocios();
                //Instância a camada de objêto
                EnderecoPicking enderecoPicking = new EnderecoPicking();
                //Localizando o endereço
                enderecoPicking = enderecoPickingCollection.Find(delegate (EnderecoPicking n) { return n.tipoEndereco == "CAIXA"; });
                //Instância a camada de objêto
                EnderecoPicking enderecoFlowRack = new EnderecoPicking();
                //Localizando o endereço
                enderecoFlowRack = enderecoPickingCollection.Find(delegate (EnderecoPicking n) { return n.tipoEndereco == "FLOWRACK"; });

                if (rbtPicking.Checked == true && rbtNovoFlowrack.Checked == true)
                {
                    if(enderecoFlowRack == null)
                    {
                        MessageBox.Show("Produto não possui endereço de Flow Rack!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else if (Convert.ToInt32(txtQtd.Text) <= 0)
                    {
                        MessageBox.Show("A quantidade digitada não pode ser igual ou menor que zero!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Convert.ToInt32(txtQtd.Text) > enderecoPicking.estoque / enderecoPicking.quantidadeCaixa)
                    {
                        MessageBox.Show("A quantidade digitada ultrapassa a quantidade do picking!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if ((enderecoPicking.estoque / enderecoPicking.quantidadeCaixa) + Convert.ToInt32(txtQtd.Text) > enderecoPicking.capacidade)
                        {
                            if (MessageBox.Show("A quantidade ultrapassa a capacidade do picking! \nDeseja continuar?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                        }

                        transferenciaNegocios.TransferirPickingPicking(
                        enderecoPicking.idProduto, //ID do produto
                        enderecoPicking.codApartamento, //Código do Apartamento que irá transferir
                        enderecoFlowRack.codApartamento, //Código do Apartamento que recebe a transferência
                        Convert.ToInt32(txtQtd.Text) * enderecoPicking.quantidadeCaixa, //Estoque do Produto
                        Convert.ToDouble(txtPeso.Text), //Peso do produto
                        Convert.ToString(enderecoPicking.vencimento), //Vencimento do produto
                        enderecoPicking.lote,
                        cmbEmpresa.Text); 

                        //Resgistra a operação no rastreamento
                        transferenciaNegocios.InserirRastreamento(codUsuario, enderecoPicking.idProduto, //ID do produto
                        enderecoPicking.codApartamento, //Código de Origem
                        Convert.ToInt32(lblQtdOrigem.Text), //Quantidade de origem
                        Convert.ToString(lblVencimentoOrigem.Text), //Vencimento de origem
                        Convert.ToDouble(lblPesoOrigem.Text), //Peso de origem
                        lblLoteOrigem.Text, //Lote de origem
                        enderecoFlowRack.codApartamento, //Endereço de destino                  
                        Convert.ToInt32(txtQtd.Text) * enderecoPicking.quantidadeCaixa, //Quantidade de destino
                        Convert.ToString(enderecoPicking.vencimento), //Vencimento de destino
                        Convert.ToDouble(txtPeso.Text), //Peso de destino               
                        enderecoPicking.lote,
                        cmbEmpresa.Text); //Lote de destino

                        //Limpa todos os campos
                        LimparCampos();
                        MessageBox.Show("Transferência de picking para picking de flow rack realizada com sucesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else if (rbtFlowRack.Checked == true && rbtNovoPicking.Checked == true)
                {
                    if (Convert.ToInt32(txtQtd.Text) <= 0)
                    {
                        MessageBox.Show("A quantidade digitada não pode ser igual ou menor que zero!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Convert.ToInt32(txtQtd.Text) > enderecoFlowRack.estoque / enderecoFlowRack.quantidadeCaixa)
                    {
                        MessageBox.Show("A quantidade digitada ultrapassa a quantidade do picking!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if ((enderecoFlowRack.estoque / enderecoFlowRack.quantidadeCaixa) + Convert.ToInt32(txtQtd.Text) > enderecoFlowRack.capacidade)
                        {
                            if (MessageBox.Show("A quantidade ultrapassa a capacidade do picking! \nDeseja continuar?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                        }

                        transferenciaNegocios.TransferirPickingPicking(
                        enderecoFlowRack.idProduto, //ID do produto
                        enderecoFlowRack.codApartamento, //Código do Apartamento que irá transferir
                        enderecoPicking.codApartamento, //Código do Apartamento que recebe a transferência
                        Convert.ToInt32(txtQtd.Text) * enderecoFlowRack.quantidadeCaixa, //Estoque do Produto
                        Convert.ToDouble(txtPeso.Text), //Peso do produto
                        Convert.ToString(enderecoFlowRack.vencimento), //Vencimento do produto
                        enderecoFlowRack.lote, cmbEmpresa.Text); //Lote do produto

                        //Resgistra a operação no rastreamento
                        transferenciaNegocios.InserirRastreamento(codUsuario, enderecoFlowRack.idProduto, //ID do produto
                          enderecoFlowRack.codApartamento, //Código de Origem
                          Convert.ToInt32(lblQtdOrigem.Text), //Quantidade de origem
                          Convert.ToString(lblVencimentoOrigem.Text), //Vencimento de origem
                          Convert.ToDouble(lblPesoOrigem.Text), //Peso de origem
                          lblLoteOrigem.Text, //Lote de origem
                          enderecoPicking.codApartamento, //Endereço de destino                  
                          Convert.ToInt32(txtQtd.Text) * enderecoFlowRack.quantidadeCaixa, //Quantidade de destino
                          Convert.ToString(enderecoFlowRack.vencimento), //Vencimento de destino
                          Convert.ToDouble(txtPeso.Text), //Peso de destino               
                          enderecoPicking.lote,
                          cmbEmpresa.Text); //Lote de destino

                        //Limpa todos os campos
                        LimparCampos();
                        MessageBox.Show("Transferência de picking de flow rack para picking realizada com sucesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Transferência de Pulmão para Pulmão
        private void TransferenciaPulmaoPulmao(string endereco)
        {
            try
            {
                //Se a transferência for de pulmão para pulmão
                if ((rbtPulmao.Checked == true) && rbtNovoPulmao.Checked == true)
                {
                    if (cmbPulmao.Text.Equals(""))
                    {
                        MessageBox.Show("Por favor, selecione um endereço de pulmão!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //Instância a camada de negocios
                        TransferenciaNegocios transferenciaNegocios = new TransferenciaNegocios();
                        //Instância a camada de objêto
                        Estrutura pulmaoNovo = new Estrutura();
                        //Executa a consulta
                        pulmaoNovo = transferenciaNegocios.PesqNovoEndereco(endereco);

                        //Se for o mesmo endereço
                        if (cmbPulmao.Text.Equals(txtNovoEndereco.Text))
                        {
                            MessageBox.Show("Endereço digitado não pode ser o mesmo de origem!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (pulmaoNovo.codApartamento == 0)
                        {
                            MessageBox.Show("Endereço digitado não encontrado!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //Se for endereço de separação
                        else if (pulmaoNovo.tipoApartamento == "Picking")
                        {
                            MessageBox.Show("Digite um endereço de pulmão!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //Se o endereço estiver indisponível
                        else if (pulmaoNovo.disposicaoApartamento.Equals("Nao"))
                        {
                            MessageBox.Show("O endereço digitado se encontra indisponível!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //Se o endereço estiver indisponível
                        else if (pulmaoNovo.numeroNivel > Convert.ToInt32(lblNivel.Text))
                        {
                            MessageBox.Show("O endereço digitado utrapassa o nível máximo do produto!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            //Se o tipo de região do produto for diferente da do endereço
                            if (pulmaoNovo.tipoRegiao != lblArmazenagem.Text)
                            {
                                if ((MessageBox.Show("O tipo de armazenamento do produto é diferente do endereço " + "(" + pulmaoNovo.tipoRegiao + ")" + " digitado! \nDeseja continuar?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.No)
                                {
                                    return;
                                }
                            }

                            //Instância a camada de objêto
                            EnderecoPulmaoCollection pulmaoCollection = new EnderecoPulmaoCollection();
                            //Pesquisa se há produto no novo endereço
                            pulmaoCollection = transferenciaNegocios.PesqProdutoNovoEndereco(pulmaoNovo.codApartamento, cmbEmpresa.Text);


                            //Localizando o endereço
                            EnderecoPulmao produtoPulmaoNovo = pulmaoCollection.Find(delegate (EnderecoPulmao n) { return n.idProduto == produto.idProduto; });

                            //Localizando o endereço existente
                            EnderecoPulmao produtoExistente = enderecoPulmaoCollection.Find(delegate (EnderecoPulmao n) { return n.descEndereco1 == cmbPulmao.Text; });

                            //Se existe ou não no endereço digitado
                            string existeProduto = null;


                            //Verifica se o produto já existe no endereço digitado
                            if (produtoPulmaoNovo == null)
                            {
                                existeProduto = "NÃO";

                                //Verifica a quantidade de produto no endereço
                                if (pulmaoCollection.Count > 0 && pulmaoCollection.Count + 1 > Convert.ToInt32(pulmaoCollection[0].qtdProdutoPalete))
                                {
                                    MessageBox.Show("A quantidade de produto por palete de endereço não pode ser ultrapassada (Máximo: " + pulmaoCollection[0].qtdProdutoPalete + " produto por palete)!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else if (produtoPulmaoNovo != null)
                            {
                                existeProduto = "SIM";
                            }

                            if (Convert.ToInt32(txtQtd.Text) > (produtoExistente.qtdCaixaOrigem - produtoExistente.estoqueReservado))
                            {
                                MessageBox.Show("A quantidade digitada ultrapassa a quantidade do pulmão!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                //Verifica a quantidade a ser transferida se cabe no endereço
                                if (pulmaoNovo.tamanhoApartamento == "PP" && produtoPulmaoNovo == null && Convert.ToInt32(txtQtd.Text) > produto.totalPequeno ||
                                         pulmaoNovo.tamanhoApartamento == "PP" && produtoPulmaoNovo != null && (Convert.ToInt32(produtoPulmaoNovo.qtdCaixaOrigem) / Convert.ToInt32(produto.fatorPulmao)) + Convert.ToInt32(txtQtd.Text) > produto.totalPequeno)
                                {
                                    MessageBox.Show("O endereço não suporta a quantidade! \nTamanho do Endereço: " + pulmaoNovo.tamanhoApartamento + "\nMáximo Suportado: " + produto.totalPequeno + " " + produto.undPulmao, "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if (pulmaoNovo.tamanhoApartamento == "PM" && produtoPulmaoNovo == null && Convert.ToInt32(txtQtd.Text) > produto.totalMedio ||
                                         pulmaoNovo.tamanhoApartamento == "PM" && produtoPulmaoNovo != null && (Convert.ToInt32(produtoPulmaoNovo.qtdCaixaOrigem) / Convert.ToInt32(produto.fatorPulmao)) + Convert.ToInt32(txtQtd.Text) > produto.totalMedio)
                                {
                                    MessageBox.Show("O endereço não suporta a quantidade! \nTamanho do Endereço: " + pulmaoNovo.tamanhoApartamento + "\nMáximo Suportado: " + produto.totalMedio + " " + produto.undPulmao, "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if (pulmaoNovo.tamanhoApartamento == "PG" && produtoPulmaoNovo == null && Convert.ToInt32(txtQtd.Text) > produto.totalGrande ||
                                         pulmaoNovo.tamanhoApartamento == "PG" && produtoPulmaoNovo != null && (Convert.ToInt32(produtoPulmaoNovo.qtdCaixaOrigem) / Convert.ToInt32(produto.fatorPulmao)) + Convert.ToInt32(txtQtd.Text) > produto.totalGrande)
                                {
                                    MessageBox.Show("O endereço não suporta a quantidade! \nTamanho do Endereço: " + pulmaoNovo.tamanhoApartamento + "\nMáximo Suportado: " + produto.totalGrande + " " + produto.undPulmao, "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if (pulmaoNovo.tamanhoApartamento == "PB" && produtoPulmaoNovo == null && Convert.ToInt32(txtQtd.Text) > produto.totalBlocado ||
                                         pulmaoNovo.tamanhoApartamento == "PB" && produtoPulmaoNovo != null && (Convert.ToInt32(produtoPulmaoNovo.qtdCaixaOrigem) / Convert.ToInt32(produto.fatorPulmao)) + Convert.ToInt32(txtQtd.Text) > produto.totalBlocado)
                                {
                                    MessageBox.Show("O endereço não suporta a quantidade! \nTamanho do Endereço: " + pulmaoNovo.tamanhoApartamento + "\nMáximo Suportado: " + produto.totalBlocado + " " + produto.undPulmao, "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else
                                {

                                    //Transfere
                                    transferenciaNegocios.TransferirPulmaoPulmao(existeProduto, produto.idProduto, produtoExistente.codApartamento1, pulmaoNovo.codApartamento,
                                        Convert.ToInt32(txtQtd.Text) * produto.fatorPulmao, Convert.ToDouble(txtPeso.Text), produtoExistente.vencimentoProduto1, produtoExistente.loteProduto1, produtoExistente.impresso,
                                        produtoExistente.notaCega, produtoExistente.dataEntrada, produtoExistente.codUsuario, cmbEmpresa.Text);

                                    //Resgistra a operação no rastreamento
                                    transferenciaNegocios.InserirRastreamento(codUsuario, produto.idProduto, //ID do produto
                                    produtoExistente.codApartamento1, //Código de Origem
                                    Convert.ToInt32(lblQtdOrigem.Text), //Quantidade de origem
                                    Convert.ToString(lblVencimentoOrigem.Text), //Vencimento de origem
                                    Convert.ToDouble(lblPesoOrigem.Text), //Peso de origem
                                    lblLoteOrigem.Text, //Lote de origem

                                    pulmaoNovo.codApartamento, //Endereço de destino                  
                                    Convert.ToInt32(txtQtd.Text) * produto.fatorPulmao, //Quantidade de destino
                                    Convert.ToString(produtoExistente.vencimentoProduto1), //Vencimento de destino
                                    Convert.ToDouble(txtPeso.Text), //Peso de destino               
                                    produtoExistente.loteProduto1,
                                    cmbEmpresa.Text); //Lote de destino

                                    //Limpa todos os campos
                                    LimparCampos();
                                    MessageBox.Show("Transferência realizada com sucesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }



                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Transferência de Pulmão para Picking
        private void TransferenciaPulmaoPicking()
        {
            try
            {
                //Instância a camada de negocios
                TransferenciaNegocios transferenciaNegocios = new TransferenciaNegocios();

                //Se a transferência for de pulmão para picking
                if ((rbtPulmao.Checked == true && rbtNovoPicking.Checked == true) || (rbtPulmao.Checked == true && rbtNovoFlowrack.Checked == true))
                {
                    if (cmbPulmao.Text.Equals(""))
                    {
                        MessageBox.Show("Por favor, selecione um endereço de pulmão!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //Se não existir o endereço
                        if (txtNovoEndereco.Text.Equals(""))
                        {
                            MessageBox.Show("Produto não possui endereço de picking!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            //Localizando o endereço de pulmão
                            EnderecoPulmao pulmaoProduto = enderecoPulmaoCollection.Find(delegate (EnderecoPulmao n) { return n.descEndereco1 == cmbPulmao.Text; });

                            //Localizando o endereço de picking
                            EnderecoPicking pickingProduto = enderecoPickingCollection.Find(delegate (EnderecoPicking n) { return n.endereco == txtNovoEndereco.Text; });

                            //Verifica o estoque do pulmão - a reserva

                            if (txtQtd.Text.Equals(string.Empty))
                            {
                                MessageBox.Show("Informe uma quantidade!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else if (Convert.ToInt32(txtQtd.Text) <= 0)
                            {
                                MessageBox.Show("A quantidade digitada não pode ser igual ou menor que zero!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else if (Convert.ToInt32(txtQtd.Text) > (pulmaoProduto.qtdCaixaOrigem - pulmaoProduto.estoqueReservado))
                            {
                                MessageBox.Show("A quantidade digitada ultrapassa a quantidade do pulmão, \n produto possui uma reserva de "+ pulmaoProduto.estoqueReservado +"!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                //Verifica o estoque em unidade com a capacidade em unidade
                                if ((Convert.ToInt32(txtQtd.Text) * produto.fatorPulmao) + pickingProduto.estoque > pickingProduto.capacidade * produto.fatorPulmao)
                                {
                                    if (MessageBox.Show("A quantidade ultrapassa a capacidade do picking! \nDeseja continuar?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    {
                                        return;
                                    }
                                }

                                transferenciaNegocios.TransferirPulmaoPicking(
                                pulmaoProduto.idProduto, //ID do produto
                                pulmaoProduto.codApartamento1, //Código do Apartamento que irá transferir
                                pickingProduto.codApartamento, //Código do Apartamento que recebe a transferência
                                Convert.ToInt32(txtQtd.Text) * pulmaoProduto.fatorPulmao, //Estoque do Produto
                                Convert.ToDouble(txtPeso.Text), //Peso do produto
                                Convert.ToString(pulmaoProduto.vencimentoProduto1), //Vencimento do produto
                                pulmaoProduto.loteProduto1,
                                cmbEmpresa.Text); //Lote do produto

                                //Resgistra a operação no rastreamento
                                transferenciaNegocios.InserirRastreamento(codUsuario, pulmaoProduto.idProduto, //ID do produto
                                pulmaoProduto.codApartamento1, //Código de Origem
                                Convert.ToInt32(lblQtdOrigem.Text), //Quantidade de origem
                                Convert.ToString(lblVencimentoOrigem.Text), //Vencimento de origem
                                Convert.ToDouble(lblPesoOrigem.Text), //Peso de origem
                                lblLoteOrigem.Text, //Lote de origem

                                pickingProduto.codApartamento, //Endereço de destino                  
                                Convert.ToInt32(txtQtd.Text) * pulmaoProduto.fatorPulmao, //Quantidade de destino
                                Convert.ToString(pulmaoProduto.vencimentoProduto1), //Vencimento de destino
                                Convert.ToDouble(txtPeso.Text), //Peso de destino               
                                pulmaoProduto.loteProduto1, cmbEmpresa.Text);  //Lote de destino
                               
                                //Limpa todos os campos
                                LimparCampos();
                                MessageBox.Show("Transferência realizada com sucesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Transferência de Pulmão para Picking
        private void TransferenciaPickingPulmao(string endereco)
        {
            try
            {
                //Se a transferência for de pulmão para picking
                if ((rbtPicking.Checked == true && rbtNovoPulmao.Checked == true) || (rbtFlowRack.Checked == true && rbtNovoPulmao.Checked == true))
                {
                    if (txtNovoEndereco.Text.Equals(""))
                    {
                        MessageBox.Show("Por favor, digite um endereço de pulmão!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //Instância a camada de negocios
                        TransferenciaNegocios transferenciaNegocios = new TransferenciaNegocios();
                        Estrutura pulmaoNovo = new Estrutura();
                        //Executa a consulta
                        pulmaoNovo = transferenciaNegocios.PesqNovoEndereco(endereco);

                        if (pulmaoNovo.codApartamento == 0)
                        {
                            MessageBox.Show("Endereço digitado não encontrado!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //Se for endereço de separação
                        else if (pulmaoNovo.tipoApartamento == "Picking")
                        {
                            MessageBox.Show("Digite um endereço de pulmão!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //Se o endereço estiver indisponível
                        else if (pulmaoNovo.disposicaoApartamento.Equals("Nao"))
                        {
                            MessageBox.Show("O endereço digitado se encontra indisponível!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //Se o endereço estiver indisponível
                        else if (pulmaoNovo.numeroNivel > Convert.ToInt32(produto.nivelMaximo))
                        {
                            MessageBox.Show("O endereço digitado utrapassa o nível máximo do produto!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            //Se o tipo de região do produto for diferente da do endereço
                            if (pulmaoNovo.tipoRegiao != produto.tipoArmazenagem)
                            {
                                if ((MessageBox.Show("O tipo de armazenamento do produto é diferente do endereço " + "(" + pulmaoNovo.tipoRegiao + ")" + " digitado! \nDeseja continuar?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.No)
                                {
                                    return;
                                }
                            }

                            //Instância a camada de objêto
                            EnderecoPulmaoCollection pulmaoCollection = new EnderecoPulmaoCollection();
                            //Pesquisa os produtos há produto no novo endereço
                            pulmaoCollection = transferenciaNegocios.PesqProdutoNovoEndereco(pulmaoNovo.codApartamento, cmbEmpresa.Text);


                            //Instância um novo objêto
                            EnderecoPicking enderecoPicking = null;

                            //Verifica o picking selecionado
                            if (rbtPicking.Checked == true)
                            {
                                enderecoPicking = enderecoPickingCollection.Find(delegate (EnderecoPicking n) { return n.tipoEndereco == "CAIXA"; });
                            }
                            else if (rbtFlowRack.Checked == true)
                            {
                                //Localizando o endereço
                                enderecoPicking = enderecoPickingCollection.Find(delegate (EnderecoPicking n) { return n.tipoEndereco == "FLOWRACK"; });
                            }

                            //Localizando o endereço
                            EnderecoPulmao produtoPulmaoNovo = pulmaoCollection.Find(delegate (EnderecoPulmao n) { return n.idProduto == produto.idProduto; });

                            //Se existe ou não no endereço digitado
                            string existe = null;


                            //Verifica se o produto já existe no endereço digitado
                            if (produtoPulmaoNovo == null)
                            {
                                existe = "NÃO";

                                //Verifica a quantidade de produto no endereço
                                if (pulmaoCollection.Count > 0 && pulmaoCollection.Count + 1 > Convert.ToInt32(pulmaoCollection[0].qtdProdutoPalete))
                                {
                                    MessageBox.Show("A quantidade de produto por palete de endereço não pode ser ultrapassada (Máximo: " + pulmaoCollection[0].qtdProdutoPalete + " produto por palete)!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                            }
                            else if (produtoPulmaoNovo != null)
                            {
                                existe = "SIM";
                            }

                            if (Convert.ToInt32(txtQtd.Text) > enderecoPicking.estoque / produto.fatorPulmao)
                            {
                                MessageBox.Show("A quantidade digitada ultrapassa a quantidade do picking!", "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                //Verifica a quantidade a ser transferida se cabe no endereço
                                if (pulmaoNovo.tamanhoApartamento == "PP" && produtoPulmaoNovo == null && Convert.ToInt32(txtQtd.Text) > produto.totalPequeno ||
                                         pulmaoNovo.tamanhoApartamento == "PP" && produtoPulmaoNovo != null && (Convert.ToInt32(produtoPulmaoNovo.qtdCaixaOrigem) / Convert.ToInt32(produto.fatorPulmao)) + Convert.ToInt32(txtQtd.Text) > produto.totalPequeno)
                                {
                                    MessageBox.Show("O endereço não suporta a quantidade! \nTamanho do Endereço: " + pulmaoNovo.tamanhoApartamento + "\nMáximo Suportado: " + produto.totalPequeno + " " + produto.undPulmao, "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if (pulmaoNovo.tamanhoApartamento == "PM" && produtoPulmaoNovo == null && Convert.ToInt32(txtQtd.Text) > produto.totalMedio ||
                                         pulmaoNovo.tamanhoApartamento == "PM" && produtoPulmaoNovo != null && (Convert.ToInt32(produtoPulmaoNovo.qtdCaixaOrigem) / Convert.ToInt32(produto.fatorPulmao)) + Convert.ToInt32(txtQtd.Text) > produto.totalMedio)
                                {
                                    MessageBox.Show("O endereço não suporta a quantidade! \nTamanho do Endereço: " + pulmaoNovo.tamanhoApartamento + "\nMáximo Suportado: " + produto.totalMedio + " " + produto.undPulmao, "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if (pulmaoNovo.tamanhoApartamento == "PG" && produtoPulmaoNovo == null && Convert.ToInt32(txtQtd.Text) > produto.totalGrande ||
                                         pulmaoNovo.tamanhoApartamento == "PG" && produtoPulmaoNovo != null && (Convert.ToInt32(produtoPulmaoNovo.qtdCaixaOrigem) / Convert.ToInt32(produto.fatorPulmao)) + Convert.ToInt32(txtQtd.Text) > produto.totalGrande)
                                {
                                    MessageBox.Show("O endereço não suporta a quantidade! \nTamanho do Endereço: " + pulmaoNovo.tamanhoApartamento + "\nMáximo Suportado: " + produto.totalGrande + " " + produto.undPulmao, "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if (pulmaoNovo.tamanhoApartamento == "PB" && produtoPulmaoNovo == null && Convert.ToInt32(txtQtd.Text) > produto.totalBlocado ||
                                         pulmaoNovo.tamanhoApartamento == "PB" && produtoPulmaoNovo != null && (Convert.ToInt32(produtoPulmaoNovo.qtdCaixaOrigem) / Convert.ToInt32(produto.fatorPulmao)) + Convert.ToInt32(txtQtd.Text) > produto.totalBlocado)
                                {
                                    MessageBox.Show("O endereço não suporta a quantidade! \nTamanho do Endereço: " + pulmaoNovo.tamanhoApartamento + "\nMáximo Suportado: " + produto.totalBlocado + " " + produto.undPulmao, "Wms - Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else
                                {
                                    //Transfere
                                    transferenciaNegocios.TransferirPickingPulmao(
                                        existe, //Se existe ou não o produto no pulmão
                                        produto.idProduto, //Código do produto 
                                        enderecoPicking.codApartamento,
                                        pulmaoNovo.codApartamento,
                                        Convert.ToInt32(txtQtd.Text) * produto.fatorPulmao,
                                        enderecoPicking.vencimento,
                                        Convert.ToDouble(txtPeso.Text),
                                        enderecoPicking.lote,
                                        codUsuario,
                                        cmbEmpresa.Text);

                                    //Resgistra a operação no rastreamento
                                    transferenciaNegocios.InserirRastreamento(codUsuario, produto.idProduto, //ID do produto
                                    enderecoPicking.codApartamento, //Código de Origem
                                    Convert.ToInt32(lblQtdOrigem.Text), //Quantidade de origem
                                    Convert.ToString(lblVencimentoOrigem.Text), //Vencimento de origem
                                    Convert.ToDouble(lblPesoOrigem.Text), //Peso de origem
                                    lblLoteOrigem.Text, //Lote de origem
                                    pulmaoNovo.codApartamento, //Endereço de destino                  
                                    Convert.ToInt32(txtQtd.Text) * produto.fatorPulmao, //Quantidade de destino
                                    Convert.ToString(enderecoPicking.vencimento), //Vencimento de destino
                                    Convert.ToDouble(txtPeso.Text), //Peso de destino               
                                   enderecoPicking.lote,
                                   cmbEmpresa.Text);  //Lote de destino


                                    //Limpa todos os campos
                                    LimparCampos();
                                    MessageBox.Show("Transferência realizada com sucesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Limpa todos os campos
        private void LimparCampos()
        {
            lblProduto.Text = "-"; //Limpa o label do produto
            lblEmbalagem.Text = "-";//Limpa o label de embalagem           
            lblCategoria.Text = "-";///Limpa o label da categoria           
            lblArmazenagem.Text = "-";//Limpa o label da armzenagem           
            lblNivel.Text = "-";//Limpa o label do nível
            lblPaletePadrao.Text = "-";//Limpa o label do palete
            lblControlaValidade.Text = "-";//Limpa o label do controla vencimento
            lblSeparacaoFlow.Text = "-";//Limpa o label da separação de flow rack
            lblDtProximoVencimento.Text = "-"; //Limpa a validade de tolerancia           
            lblVencido.Text = "-"; //Limpa a validade próximo ao vencimento

            lblEnderecoOrigem.Text = "-";  //Limpa a qtd  do pulmão      
            lblQtdOrigem.Text = "-";  //Limpa a qtd  do pulmão            
            lblReservaPulmao.Text = "-"; //Limpa a reserva do endereço pulmão
            lblVencimentoOrigem.Text = "-";  //Limpa a validade do pulmão   
            lblPesoOrigem.Text = "-"; //Limpa o peso do pulmão                     
            lblLoteOrigem.Text = "-";//Limpa o lote do pulmão
            lblTamanhoOrigem.Text = "-";//Limpa o lote do pulmão

            lblEnderecoDestino.Text = "-";//Limpa o endereço de picking            
            lblQtdDestino.Text = "-";//Limpa a qtd do picking            
            lblCapacidadeDestino.Text = "-";//Limpa a capacidade do picking            
            lblAbastecimentoDestino.Text = "-";//Limpa a ordem de abastecimento           
            lblPesoDestino.Text = "-"; //Limpa o peso            
            lblVencimentoDestino.Text = "-";//Limpa a validade            
            lblLoteDestino.Text = "-";//Limpa o lote            
            lblTamanhoDestino.Text = "-";//Limpa o tamnaho do picking

            //Zera a qtd de endereços
            lblContagemEndereco.Text = "0";

            lblVencimentoOrigem.ForeColor = Color.Black;//Seta a cor preto na validade do aéreo

            lblQtdDestino.ForeColor = Color.Black;//Seta a cor preto na qtd do picking            
            lblVencimentoDestino.ForeColor = Color.Black;//Seta a cor preto na validade do picking


            enderecoPulmaoCollection.Clear(); //limpa a coleção de objêto do pulmao

            enderecoPickingCollection.Clear();//Limpa a coleção de objêto picking


            txtCodigo.Clear(); //Limpa o campo código

            txtCodigo.Enabled = true;//Habilita o campo

            txtQtd.Clear(); //Zera a qtd nova

            txtPeso.Clear();//Zera o novo peso

            txtNovoEndereco.Clear(); //Zera o novo endereço

            cmbPulmao.Text = "";//Limpa o campo endereço
            cmbPulmao.Items.Clear();//Limpa o campo endereço

            rbtPulmao.Checked = true; //Seleciona o pulmão
            rbtNovoPicking.Checked = true; //Seleciona o picking

            txtCodigo.Focus(); //Foca no campo código

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
