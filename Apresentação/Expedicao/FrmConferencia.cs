using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Negocios;
using Negocios.Movimentacao;
using ObjetoTransferencia;
using Wms.Relatorio;
using static System.Windows.Forms.LinkLabel;

namespace Wms
{
    public partial class FrmConferencia : Form
    {
        //Perfíl do usuário
        //public string perfilUsuario;
        public int codUsuario;
        public List<Empresa> empresaCollection;
        //Controle de acesso
        //public List<Acesso> acesso;
        bool devolucao = false;

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmConferencia()
        {
            InitializeComponent();
        }

        private void FrmConferencia_Load(object sender, EventArgs e)
        {
            //Foca no campo manifesto
            txtPedido.Focus();

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
        private void txtSeparador_TextChanged(object sender, EventArgs e)
        {
            if (txtSeparador.Text == string.Empty)
            {
                lblSeparador.Text = "-";
            }
        }

        //KeyPress
        private void txtPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Pesquisa o manifesto
                PesqPedido();
            }
        }

        private void txtSeparador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Instância o negocios
                UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                //Instância a coleçãO
                UsuarioCollection usuarioCollection = new UsuarioCollection();
                //A coleção recebe o resultado da consulta
                usuarioCollection = usuarioNegocios.PesqUsuario(txtSeparador.Text, "", "SEPARADOR", null);

                if (usuarioCollection.Count > 0)
                {
                    lblSeparador.Text = usuarioCollection[0].login;

                    //Instância a camada de negocios
                    ConferenciaManifestoNegocios manifestoNegocios = new ConferenciaManifestoNegocios();
                    manifestoNegocios.RegistraSeparador(Convert.ToInt32(txtSeparador.Text), Convert.ToInt32(txtPedido.Text));

                    txtBarra.Focus();//Foca no campo
                }
                else
                {
                    MessageBox.Show("Separador não encontrado!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtBarra.Text.Equals(string.Empty))
                {
                    txtQuantidade.Focus();
                    txtQuantidade.SelectAll();
                }
                else
                {
                    //Pesquisa o produto
                    PesqProduto();
                }
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtBarra.Focus();//Foca no campo
            }
        }

        //KeyDow
        private void txtSeparador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqUsuario frame = new FrmPesqUsuario();
                frame.perfilUsuario = "SEPARADOR";
                //Nome da empresa
                frame.empresa = cmbEmpresa.Text;

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe o código
                    txtSeparador.Text = Convert.ToString(frame.codUsuario);
                    //Recebe o login
                    lblSeparador.Text = frame.nmUsuario;
                    //Foca no campo
                    txtSeparador.Focus();


                }
            }
        }

        //Click
        private void btnNovo_Click(object sender, EventArgs e)
        {
            //Limpa todos os campos
            LimparCampos();
        }

        private void btnCorteProduto_Click(object sender, EventArgs e)
        {
            //Corte do item no pedido
            CortePedido();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }


        //Pesquisa o Pedido
        private void PesqPedido()
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaPedidoNegocios pedidoNegocios = new ConferenciaPedidoNegocios();
                //Instância a camada de objêto
                Pedido pedido = new Pedido();
                //A coleção recebe o resultado da consulta
                pedido = pedidoNegocios.PesqPedido(Convert.ToInt32(txtPedido.Text), Convert.ToString(cmbEmpresa.Text));

                if (pedido.tipoPedido.Contains("DEV"))
                {
                    lblTipo.BackColor = Color.Red;
                    lblTipo.ForeColor = Color.White;
                    //Desabilita o campos separador
                    txtSeparador.Enabled = false;
                    //Desabilita o botão de corte
                    btnCorteProduto.Enabled = false;
                    //Foca no campo separador
                    txtBarra.Focus();
                    devolucao = true;
                }
                else
                {
                    lblTipo.BackColor = Color.White;
                    lblTipo.ForeColor = Color.Black;
                    //Habilita o campo separador
                    txtSeparador.Enabled = true;
                    //Habilita o botão de corte
                    btnCorteProduto.Enabled = true;
                    //Foca no campo separador
                    txtSeparador.Focus();
                    devolucao = false;
                }

                if (pedido.manifestoPedido == 0 && !pedido.tipoPedido.Contains("DEV"))
                {
                    MessageBox.Show("Por favor colocar pedido em um manifesto!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (pedido.codPedido > 0)
                {
                    lblDataPedido.Text = Convert.ToString(pedido.dataPedido);
                    lblCliente.Text = Convert.ToString(pedido.nomeCliente);
                    lblPlaca.Text = Convert.ToString(pedido.veiculoPedido);
                    lblTipo.Text = Convert.ToString(pedido.tipoPedido);
                    lblPeso.Text = String.Format("{0:N}", pedido.pesoPedido);


                    //Verifica se foi iniciado a conferencia
                    if (pedido.inicioConferencia == null)
                    {
                        pedidoNegocios.RegistraInicioConferencia(codUsuario, Convert.ToInt32(txtPedido.Text), Convert.ToString(cmbEmpresa.Text));

                        lblInicioConferencia.Text = Convert.ToString(DateTime.Now);
                    }
                    else
                    {
                        lblInicioConferencia.Text = Convert.ToString(pedido.inicioConferencia);

                        //txtSeparador.Text = Convert.ToString(pedido.codSeparador);
                        //lblSeparador.Text = pedido.loginSeparador;
                    }

                    //Calcula o tempo de conferencia
                    if (pedido.fimConferencia != null)
                    {
                        lblTempo.Text = Convert.ToString(Convert.ToDateTime(pedido.fimConferencia).Subtract(Convert.ToDateTime(pedido.inicioConferencia)));

                        lblFimConferencia.Text = Convert.ToString(pedido.fimConferencia);
                        txtSeparador.Enabled = false;
                        txtBarra.Enabled = false;
                        txtQuantidade.Enabled = false;
                        btnCorteProduto.Enabled = false;

                        MessageBox.Show("Pedido já conferido!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    //Pesquisa os itens do manifesto
                    PesqItemPedido();
                    //Desabilita o campo manifesto
                    txtPedido.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Manifesto não encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os itens do manifesto
        private void PesqItemPedido()
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaPedidoNegocios pedidoNegocios = new ConferenciaPedidoNegocios();
                //Instância a camada de objêto
                ItensPedidoCollection itemPedidoCollection = new ItensPedidoCollection();
                //A coleção recebe o resultado da consulta
                itemPedidoCollection = pedidoNegocios.PesqItensPedido(Convert.ToInt32(txtPedido.Text), cmbEmpresa.Text);
                //Limpa o grid
                gridItem.Rows.Clear();

                MessageBox.Show(""+ itemPedidoCollection.Count);

                if (itemPedidoCollection.Count > 0)
                {
                    for (int i = 0; itemPedidoCollection.Count > i; i++)
                    {
                        if (itemPedidoCollection[i].qtdProduto == itemPedidoCollection[i].qtdConferida + itemPedidoCollection[i].qtdCorte)
                        {
                            //Grid Recebe o resultado da coleção conferida
                            gridItemConferidos.Rows.Add(gridItemConferidos.Rows.Count + 1, itemPedidoCollection[i].enderecoProduto, itemPedidoCollection[i].idProduto,
                                                          itemPedidoCollection[i].descProduto,
                                                          itemPedidoCollection[i].qtdProduto, itemPedidoCollection[i].uniUnidade, itemPedidoCollection[i].qtdConferida,
                                                          itemPedidoCollection[i].qtdCorte + " " + itemPedidoCollection[i].uniUnidade,
                                                          itemPedidoCollection[i].qtdCorte,
                                                          itemPedidoCollection[i].qtdCaixaConferida + " " + itemPedidoCollection[i].uniCaixa,
                                                          itemPedidoCollection[i].qtdUnidadeConferida + " " + itemPedidoCollection[i].uniUnidade,
                                                          itemPedidoCollection[i].qtdProduto - (itemPedidoCollection[i].qtdConferida + itemPedidoCollection[i].qtdCorte) + " " + itemPedidoCollection[i].uniUnidade,
                                                          string.Format("{0:d}", itemPedidoCollection[i].vencimentoProduto),
                                                          itemPedidoCollection[i].loteProduto, itemPedidoCollection[i].dataConferencia, itemPedidoCollection[i].uniCaixa,
                                                          itemPedidoCollection[i].qtdCaixaProduto, itemPedidoCollection[i].pesoVariavel);

                        }
                        else
                        {
                            //Grid Recebe o resultado da coleção não conferida
                            gridItem.Rows.Add(gridItem.Rows.Count + 1, itemPedidoCollection[i].enderecoProduto, itemPedidoCollection[i].idProduto,
                                                          itemPedidoCollection[i].descProduto,
                                                          itemPedidoCollection[i].qtdProduto, itemPedidoCollection[i].uniUnidade, itemPedidoCollection[i].qtdConferida,
                                                          itemPedidoCollection[i].qtdCorte + " " + itemPedidoCollection[i].uniUnidade,
                                                          itemPedidoCollection[i].qtdCorte,
                                                          itemPedidoCollection[i].qtdCaixaConferida + " " + itemPedidoCollection[i].uniCaixa,
                                                          itemPedidoCollection[i].qtdUnidadeConferida + " " + itemPedidoCollection[i].uniUnidade,
                                                          itemPedidoCollection[i].qtdProduto - (itemPedidoCollection[i].qtdConferida + itemPedidoCollection[i].qtdCorte) + " " + itemPedidoCollection[i].uniUnidade,
                                                          itemPedidoCollection[i].vencimentoProduto,
                                                          itemPedidoCollection[i].loteProduto, itemPedidoCollection[i].dataConferencia, itemPedidoCollection[i].uniCaixa,
                                                          itemPedidoCollection[i].qtdCaixaProduto, itemPedidoCollection[i].pesoVariavel);

                        }
                    }




                    //Quantidade de item
                    lblItemPendente.Text = Convert.ToString(gridItem.Rows.Count);
                    lblItemConferidos.Text = Convert.ToString(gridItemConferidos.Rows.Count);
                    lblItem.Text = Convert.ToString(gridItem.Rows.Count + gridItemConferidos.Rows.Count);
                }
                else
                {
                    MessageBox.Show("Nenhum item encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os itens do manifesto
        private void PesqProduto()
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaPedidoNegocios pedidoNegocios = new ConferenciaPedidoNegocios();
                //Instância a camada de objêto
                Barra barra = new Barra();
                //A coleção recebe o resultado da consulta
                barra = pedidoNegocios.PesqProduto(Convert.ToString(txtBarra.Text), cmbEmpresa.Text);

                if (barra.idProduto > 0)
                {
                    bool verificarProduto = false;

                    //Percorre todo o grid
                    for (int i = 0; gridItem.Rows.Count > i; i++)
                    {
                        //Verifica se o item existe no grid
                        if (gridItem.Rows[i].Cells[2].Value.Equals(barra.idProduto))
                        {
                            //Seleciona a linha do grid
                            gridItem.CurrentCell = gridItem.Rows[i].Cells[1];
                            //Pesquisa a informação do endereço do produto
                            PesqEndereco(barra.idProduto, i, barra.multiplicador);

                            verificarProduto = false;

                            break; //para o laço de pesquisa
                        }
                        else
                        {
                            verificarProduto = true;
                        }
                    }

                    if (verificarProduto == true)
                    {
                        MessageBox.Show("Verifique se o código de barra está: \n1º - Cadastrado no produto correto, e \n2º - Cadastrado em vários produtos. ", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Código de barra não encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os itens do manifesto
        private void PesqEndereco(int idProduto, int linha, int multiplicador)
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaPedidoNegocios pedidoNegocios = new ConferenciaPedidoNegocios();
                //Instância a camada de objêto
                EnderecoPicking endereco = new EnderecoPicking();
                //pesquisa informações sobre o produto no picking
                endereco = pedidoNegocios.PesqEndereco(idProduto, cmbEmpresa.Text);

                if (endereco.idProduto > 0)
                {
                    //Vencimento do produto na área de separação
                    lblVencimento.Text = string.Format("{0: dd/MM/yyyy}", endereco.vencimento);
                    //Lote do produto na área de separação
                    lblLote.Text = endereco.lote;

                    //Confere o item
                    ConferenciaItem(txtPedido.Text, idProduto, linha, multiplicador);
                }
                else
                {
                    MessageBox.Show("Produto não endereçado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Confere o item
        private void ConferenciaItem(string manifesto, int idProduto, int linha, int multiplicador)
        {
            try
            {
                if (txtQuantidade.Text.Equals("") || txtQuantidade.Text.Equals("0"))
                {
                    MessageBox.Show("Por favor digite uma quantidade!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int quantidade = Convert.ToInt32(gridItem.Rows[linha].Cells[4].Value); //Quantidade do grid
                    int qtdConferida = Convert.ToInt32(gridItem.Rows[linha].Cells[6].Value); //Quantidade do grid conferida
                    int qtdCorte = Convert.ToInt32(gridItem.Rows[gridItem.CurrentRow.Index].Cells[8].Value); //Quantidade do grid corte
                    string uniUnidade = Convert.ToString(gridItem.Rows[linha].Cells[5].Value); //tipo de unidade fracionada  
                    string uniCaixa = Convert.ToString(gridItem.Rows[linha].Cells[15].Value); //tipo de unidade da caixa
                    int caixaProduto = Convert.ToInt32(gridItem.Rows[linha].Cells[16].Value); //quantidade da caixa do produto
                    bool pesoVariavel = Convert.ToBoolean(gridItem.Rows[linha].Cells[17].Value); //peso variável

                    //Seta a quantidade conferida
                    qtdConferida = qtdConferida + Convert.ToInt32(txtQuantidade.Text) * multiplicador;

                    if ((qtdConferida + qtdCorte) > quantidade)
                    {
                        txtQuantidade.SelectAll();
                        txtQuantidade.Focus();
                        MessageBox.Show("Quantidade conferida ultrapassa a quantidade do pedido", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (pesoVariavel == false)
                        {
                            //Seta a quantidade conferida
                            gridItem.Rows[linha].Cells[6].Value = qtdConferida;
                            //Exibe a quantidade de caixa
                            gridItem.Rows[linha].Cells[9].Value = (qtdConferida + qtdCorte) / caixaProduto + " " + uniCaixa;
                            //Exibe a quantidade de unidade (conferida % quantidade)
                            gridItem.Rows[linha].Cells[10].Value = (qtdConferida + qtdCorte) % caixaProduto + " " + uniUnidade;
                            //Subtrai a quantidade conferida na quantidade que falta
                            gridItem.Rows[linha].Cells[11].Value = quantidade - (qtdConferida + qtdCorte) + " " + uniUnidade;
                            //Seta o vencimento
                            gridItem.Rows[linha].Cells[12].Value = lblVencimento.Text;
                            //Seta o lote
                            gridItem.Rows[linha].Cells[13].Value = lblLote.Text;
                            //Seta a data de conferencia
                            gridItem.Rows[linha].Cells[14].Value = DateTime.Now;
                        }
                        else
                        {
                            FrmPesoVariavel frame = new FrmPesoVariavel();
                            frame.idProduto = Convert.ToInt32(gridItem.Rows[linha].Cells[2].Value);
                            frame.produto = Convert.ToString(gridItem.Rows[linha].Cells[3].Value);
                            frame.manifesto = Convert.ToInt32(txtPedido.Text);
                            frame.quantidade = Convert.ToDouble(qtdConferida - qtdCorte);
                            frame.tipo = uniUnidade;

                            //Recebe as informações
                            if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                //Seta a quantidade conferida
                                gridItem.Rows[linha].Cells[4].Value = string.Format("{0:N3}", frame.pesoConferido);
                                gridItem.Rows[linha].Cells[6].Value = string.Format("{0:N3}", frame.pesoConferido);
                                gridItem.Rows[linha].Cells[7].Value = string.Format("{0:N3}", frame.corteConferido) + " " + uniUnidade;
                                gridItem.Rows[linha].Cells[9].Value = frame.volume + " VOL";

                                //Subtrai a quantidade conferida na quantidade que falta
                                gridItem.Rows[linha].Cells[11].Value = 0 + " " + uniUnidade;
                                //Seta o vencimento
                                gridItem.Rows[linha].Cells[12].Value = lblVencimento.Text;
                                //Seta o lote
                                gridItem.Rows[linha].Cells[13].Value = lblLote.Text;
                                //Seta a data de conferencia
                                gridItem.Rows[linha].Cells[14].Value = DateTime.Now;

                            }
                        }

                        //Limpa o campos
                        txtBarra.Text = string.Empty;
                        txtQuantidade.Text = "1";
                        //Foca no campo
                        txtBarra.Focus();

                        //Instância a camada de negocios
                        ConferenciaPedidoNegocios conferenciaNegocios = new ConferenciaPedidoNegocios();

                        if (quantidade == qtdConferida + qtdCorte)
                        {
                            //Registrar conferencia
                            conferenciaNegocios.RegistrarConferenciaItem(
                                codUsuario,
                               idProduto, //Id do produto
                                qtdConferida - qtdCorte, //Quantidade conferida
                                qtdCorte, //Quantidade de corte
                                Convert.ToDateTime(gridItem.Rows[linha].Cells[12].Value), //Vencimento
                                Convert.ToString(gridItem.Rows[linha].Cells[13].Value), //Lote
                                Convert.ToInt32(txtPedido.Text),
                                devolucao,
                                cmbEmpresa.Text); //Código do manifesto

                            //Transfere o item para o grid de itens conferidos
                            gridItemConferidos.Rows.Add(
                                gridItem.Rows[linha].Cells[0].Value,
                                gridItem.Rows[linha].Cells[1].Value,
                                gridItem.Rows[linha].Cells[2].Value,
                                gridItem.Rows[linha].Cells[3].Value,
                                gridItem.Rows[linha].Cells[4].Value,
                                gridItem.Rows[linha].Cells[5].Value,
                                gridItem.Rows[linha].Cells[6].Value,
                                gridItem.Rows[linha].Cells[7].Value,
                                gridItem.Rows[linha].Cells[9].Value,
                                gridItem.Rows[linha].Cells[10].Value,
                                gridItem.Rows[linha].Cells[11].Value,
                                gridItem.Rows[linha].Cells[12].Value,
                                gridItem.Rows[linha].Cells[13].Value,
                                gridItem.Rows[linha].Cells[14].Value,
                                gridItem.Rows[linha].Cells[15].Value,
                                gridItem.Rows[linha].Cells[16].Value,
                                gridItem.Rows[linha].Cells[17].Value
                                );

                            //Remove a linha do grid
                            gridItem.Rows.RemoveAt(linha);

                            //Quantidade de item
                            lblItemPendente.Text = Convert.ToString(gridItem.Rows.Count);
                            lblItemConferidos.Text = Convert.ToString(gridItemConferidos.Rows.Count);
                            lblItem.Text = Convert.ToString(gridItem.Rows.Count + gridItemConferidos.Rows.Count);

                            //Verifica a finalização da conferencia
                            FecharConferencia();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Corte a quantidade do pedido
        private void CortePedido()
        {
            try
            {
                //envia as informações
                if (gridItem.Rows.Count > 0)
                {

                    //Instância a camada de negocios
                    ConferenciaFlowRackNegocios itensFlowRackNegocios = new ConferenciaFlowRackNegocios();
                    //Instância a camada de negocios
                    ConferenciaPedidoNegocios pedidoNegocios = new ConferenciaPedidoNegocios();

                    int idProduto = Convert.ToInt32(gridItem.Rows[gridItem.CurrentRow.Index].Cells[2].Value); //Id do produto
                    string descProduto = Convert.ToString(gridItem.Rows[gridItem.CurrentRow.Index].Cells[3].Value); //Descrição do produto
                    int quantidade = Convert.ToInt32(gridItem.Rows[gridItem.CurrentRow.Index].Cells[4].Value); //Quantidade do grid
                    int qtdConferida = Convert.ToInt32(gridItem.Rows[gridItem.CurrentRow.Index].Cells[6].Value); //Quantidade do grid conferida
                    int qtdCorte = Convert.ToInt32(gridItem.Rows[gridItem.CurrentRow.Index].Cells[8].Value); //Quantidade do grid corte
                    string uniUnidade = Convert.ToString(gridItem.Rows[gridItem.CurrentRow.Index].Cells[5].Value); //tipo de unidade fracionada  
                    string uniCaixa = Convert.ToString(gridItem.Rows[gridItem.CurrentRow.Index].Cells[15].Value); //tipo de unidade da caixa
                    int caixaProduto = Convert.ToInt32(gridItem.Rows[gridItem.CurrentRow.Index].Cells[16].Value); //quantidade da caixa do produto
                    bool pesoVariavel = Convert.ToBoolean(gridItem.Rows[gridItem.CurrentRow.Index].Cells[17].Value); //peso variável

                    //Instância o frame de corte
                    FrmCorteFlow frame = new FrmCorteFlow();
                    //Pesquisa o estoque do produto
                    frame.estoque = itensFlowRackNegocios.PesqEstoque(idProduto);
                    //Passa a descrição do produto
                    frame.sku = Convert.ToString(descProduto);
                    //Passa o saldo de conferencia para o corte
                    frame.quantidade = Convert.ToInt32(quantidade - qtdConferida);

                    //Recebe as informações
                    if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //Recebe o valor do corte
                        qtdCorte = quantidade - frame.resultado;

                        //Exibe a quantidade de caixa
                        gridItem.Rows[gridItem.CurrentRow.Index].Cells[9].Value = (qtdConferida + qtdCorte) / caixaProduto + " " + uniCaixa;
                        //Exibe a quantidade de unidade (conferida % quantidade)
                        gridItem.Rows[gridItem.CurrentRow.Index].Cells[10].Value = (qtdConferida + qtdCorte) % caixaProduto + " " + uniUnidade;
                        //Subtrai a quantidade conferida na quantidade que falta
                        gridItem.Rows[gridItem.CurrentRow.Index].Cells[11].Value = quantidade - (qtdConferida + qtdCorte) + " " + uniUnidade;
                        //Seta a data de conferencia
                        gridItem.Rows[gridItem.CurrentRow.Index].Cells[14].Value = DateTime.Now;

                        //Seta o corte
                        gridItem.Rows[gridItem.CurrentRow.Index].Cells[7].Value = qtdCorte + " " + uniUnidade;
                        gridItem.Rows[gridItem.CurrentRow.Index].Cells[8].Value = qtdCorte;

                        //Instância a camada de negocios
                        ConferenciaPedidoNegocios conferenciaNegocios = new ConferenciaPedidoNegocios();

                        if (quantidade == (qtdConferida + qtdCorte))
                        {
                            //Registrar conferencia
                            conferenciaNegocios.RegistrarConferenciaItem(
                                codUsuario,
                                Convert.ToInt32(idProduto), //Id do produto
                                Convert.ToInt32(qtdConferida), //Quantidade conferida
                                Convert.ToInt32(qtdCorte), //Quantidade corte
                                Convert.ToDateTime(null), //Vencimento
                                Convert.ToString(null), //Lote
                                Convert.ToInt32(txtPedido.Text),
                                devolucao,
                                cmbEmpresa.Text); //Código do manifesto

                            //Transfere o item para o grid de itens conferidos
                            gridItemConferidos.Rows.Add(
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[0].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[1].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[2].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[3].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[4].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[5].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[6].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[7].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[9].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[10].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[11].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[12].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[13].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[14].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[15].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[16].Value,
                                gridItem.Rows[gridItem.CurrentRow.Index].Cells[17].Value
                                );

                            //Remove a linha do grid
                            gridItem.Rows.RemoveAt(gridItem.CurrentRow.Index);

                            //Quantidade de item
                            lblItemPendente.Text = Convert.ToString(gridItem.Rows.Count);
                            lblItemConferidos.Text = Convert.ToString(gridItemConferidos.Rows.Count);
                            lblItem.Text = Convert.ToString(gridItem.Rows.Count + gridItemConferidos.Rows.Count);

                            //Verifica a finalização da conferencia
                            FecharConferencia();
                        }

                    }
                }

                txtBarra.Focus(); //Foca no campo
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao realizar o corte!", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Fechar manifesto
        private void FecharConferencia()
        {
            try
            {
                if (gridItem.Rows.Count == 0)
                {
                    //Instância a camada de negocios
                    ConferenciaPedidoNegocios conferenciaNegocios = new ConferenciaPedidoNegocios();

                    if (lblTipo.Text.Contains("DEV"))
                    {
                        //Executa
                        conferenciaNegocios.RegistraFimConferencia(Convert.ToInt32(txtPedido.Text), null, cmbEmpresa.Text);


                        if (DialogResult.Yes == MessageBox.Show("Conferencia finalizada com sucesso! \nDeseja imprimir o mapa de devolução?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                        {
                            //Imprime o pedido
                            ImprimirPedido();
                        }
                    }
                    else
                    {
                        //Executa
                        conferenciaNegocios.RegistraFimConferencia(Convert.ToInt32(txtPedido.Text), Convert.ToInt32(txtSeparador.Text), cmbEmpresa.Text);

                        MessageBox.Show("Conferencia finalizada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //Limpa o campos
                    LimparCampos();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Impressao de Pedido
        private void ImprimirPedido()
        {
            try
            {
                //Instância a camada de negocios
                ManifestoNegocios manifestoNegocios = new ManifestoNegocios();

                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância o relatório
                    FrmMapaSeparacao frame = new FrmMapaSeparacao();
                    frame.Text = "Pedido " + Convert.ToInt32(txtPedido.Text);
                    int cont = frame.GerarRelatorio(cmbEmpresa.Text, 0, Convert.ToInt32(txtPedido.Text), "", false, false, "desc");
                    //Exibe o relatório
                    frame.Show();

                    //Verifica se a impressão deu certo
                    if (cont == 1)
                    {
                        //Registra a impressão
                        manifestoNegocios.RegistraImpressao(cmbEmpresa.Text, codUsuario, 0, Convert.ToInt32(txtPedido.Text));
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o mapa de separação! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void LimparCampos()
        {
            txtPedido.Text = string.Empty;
            txtSeparador.Text = string.Empty;
            lblSeparador.Text = "-";
            txtBarra.Text = string.Empty;
            txtQuantidade.Text = "1";

            lblVencimento.Text = "-";
            lblLote.Text = "-";
            lblDataPedido.Text = "-";
            lblCliente.Text = "-";
            lblPlaca.Text = "-";
            lblTipo.Text = "-";
            lblPeso.Text = "-";
            lblInicioConferencia.Text = "-";
            lblFimConferencia.Text = "-";
            lblTempo.Text = "-";

            lblItem.Text = "00";
            lblItemConferidos.Text = "00";
            lblItemPendente.Text = "00";

            gridItem.Rows.Clear();
            gridItemConferidos.Rows.Clear();

            txtPedido.Enabled = true;
            txtSeparador.Enabled = true;
            txtBarra.Enabled = true;
            txtQuantidade.Enabled = true;
            btnCorteProduto.Enabled = true;

            txtPedido.Focus();
        }


    }
}



