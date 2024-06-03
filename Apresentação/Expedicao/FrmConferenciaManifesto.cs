using Negocios;
using ObjetoTransferencia;
using ObjetoTransferência;
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
using Wms.Movimentacao;

namespace Wms
{
    public partial class FrmConferenciaManifesto : Form
    {
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public int codUsuario;

        public FrmConferenciaManifesto()
        {
            InitializeComponent();
        }

        private void FrmConferenciaManifesto_Load(object sender, EventArgs e)
        {
                //Foca no campo manifesto
                txtManifesto.Focus();

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
        private void txtManifesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Pesquisa o manifesto
                PesqManifesto();
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
                    manifestoNegocios.RegistraSeparador(Convert.ToInt32(txtSeparador.Text), Convert.ToInt32(txtManifesto.Text));

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

        //Pesquisa o manifesto
        private void PesqManifesto()
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaManifestoNegocios manifestoNegocios = new ConferenciaManifestoNegocios();
                //Instância a camada de objêto
                Manifesto manifesto = new Manifesto();
                //A coleção recebe o resultado da consulta
                manifesto = manifestoNegocios.PesqManifesto(Convert.ToInt32(txtManifesto.Text), chkReentrega.Checked, cmbEmpresa.Text);


                if (manifesto.codManifesto > 0)
                {
                    lblPlaca.Text = Convert.ToString(manifesto.veiculoManifesto);
                    lblPedido.Text = Convert.ToString(manifesto.pedidoManifesto);
                    lblPeso.Text = String.Format("{0:N}", manifesto.pedidoManifesto);

                    //Calcula o tempo de conferencia
                    if (chkReentrega.Checked == false)
                    {

                        //Verifica se foi iniciado a conferencia
                        if (manifesto.inicioConferencia == null)
                        {
                            manifestoNegocios.RegistraInicioConferencia(codUsuario, Convert.ToInt32(txtManifesto.Text));

                            lblIncioConferencia.Text = Convert.ToString(DateTime.Now);
                        }
                        else
                        {
                            lblIncioConferencia.Text = Convert.ToString(manifesto.inicioConferencia);

                            txtSeparador.Text = Convert.ToString(manifesto.codSeparador);
                            lblSeparador.Text = manifesto.loginSeparador;
                        }

                        if (manifesto.fimConferencia != null)
                        {
                            lblTempo.Text = Convert.ToString(Convert.ToDateTime(manifesto.fimConferencia).Subtract(Convert.ToDateTime(manifesto.inicioConferencia)));

                            txtSeparador.Enabled = false;
                            txtBarra.Enabled = false;
                            txtQuantidade.Enabled = false;
                            btnCorteProduto.Enabled = false;

                            MessageBox.Show("Manifesto já conferido!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }

                    //Pesquisa os itens do manifesto
                    PesqItensManifesto();
                    //Desabilita o campo manifesto
                    txtManifesto.Enabled = false;
                    //Desabilita o checkbox
                    chkReentrega.Enabled = false;
                    //Foca no campo separador
                    txtSeparador.Focus();
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
        private void PesqItensManifesto()
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaManifestoNegocios manifestoNegocios = new ConferenciaManifestoNegocios();
                //Instância a camada de objêto
                ItensPedidoCollection itemPedidoCollection = new ItensPedidoCollection();
                //A coleção recebe o resultado da consulta
                itemPedidoCollection = manifestoNegocios.PesqItensManifesto(Convert.ToInt32(txtManifesto.Text), chkReentrega.Checked, cmbEmpresa.Text);
                //Limpa o grid
                gridItem.Rows.Clear();

                if (itemPedidoCollection.Count > 0)
                {
                    for (int i = 0; itemPedidoCollection.Count > i; i++)
                    {
                        //Calcula o tempo de conferencia
                        if (chkReentrega.Checked == false)
                        {
                            if (itemPedidoCollection[i].qtdProduto == itemPedidoCollection[i].qtdConferida)
                            {
                                //Grid Recebe o resultado da coleção conferida
                                gridItemConferidos.Rows.Add(gridItemConferidos.Rows.Count + 1, itemPedidoCollection[i].enderecoProduto, itemPedidoCollection[i].idProduto,
                                                              itemPedidoCollection[i].descProduto,
                                                              itemPedidoCollection[i].qtdProduto, itemPedidoCollection[i].uniUnidade, itemPedidoCollection[i].qtdConferida,
                                                              itemPedidoCollection[i].qtdCorte + " " + itemPedidoCollection[i].uniUnidade,
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
                                                              itemPedidoCollection[i].qtdCaixaConferida + " " + itemPedidoCollection[i].uniCaixa,
                                                              itemPedidoCollection[i].qtdUnidadeConferida + " " + itemPedidoCollection[i].uniUnidade,
                                                              itemPedidoCollection[i].qtdProduto - (itemPedidoCollection[i].qtdConferida + itemPedidoCollection[i].qtdCorte) + " " + itemPedidoCollection[i].uniUnidade,
                                                              itemPedidoCollection[i].vencimentoProduto,
                                                              itemPedidoCollection[i].loteProduto, itemPedidoCollection[i].dataConferencia, itemPedidoCollection[i].uniCaixa,
                                                              itemPedidoCollection[i].qtdCaixaProduto, itemPedidoCollection[i].pesoVariavel);

                            }                          
                        }
                        else
                        {
                            //Grid Recebe o resultado da coleção não conferida
                            gridItem.Rows.Add(gridItem.Rows.Count + 1, itemPedidoCollection[i].enderecoProduto, itemPedidoCollection[i].idProduto,
                                                          itemPedidoCollection[i].descProduto,
                                                          itemPedidoCollection[i].qtdProduto, itemPedidoCollection[i].uniUnidade, itemPedidoCollection[i].qtdConferida,
                                                          itemPedidoCollection[i].qtdCorte + " " + itemPedidoCollection[i].uniUnidade,
                                                          itemPedidoCollection[i].qtdCaixaConferida + " " + itemPedidoCollection[i].uniCaixa,
                                                          itemPedidoCollection[i].qtdUnidadeConferida + " " + itemPedidoCollection[i].uniUnidade,
                                                          itemPedidoCollection[i].qtdProduto - (itemPedidoCollection[i].qtdConferida + itemPedidoCollection[i].qtdCorte) + " " + itemPedidoCollection[i].uniUnidade,
                                                           string.Format("{0:d}", itemPedidoCollection[i].vencimentoProduto),
                                                          itemPedidoCollection[i].loteProduto, itemPedidoCollection[i].dataConferencia, itemPedidoCollection[i].uniCaixa,
                                                          itemPedidoCollection[i].qtdCaixaProduto, itemPedidoCollection[i].pesoVariavel);

                        }
                    }

                    if (gridItem.Rows.Count > 0)
                    {
                        txtBarra.Enabled = true;
                        txtQuantidade.Enabled = true;
                        txtBarra.Focus();
                    }

                    if (gridItem.Rows.Count == 0)
                    {
                        //Verifica a finalização da conferencia
                        FecharConferencia();
                    }

                    //Quantidade de item
                    lblItem.Text = Convert.ToString(itemPedidoCollection.Count());
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
                ConferenciaManifestoNegocios manifestoNegocios = new ConferenciaManifestoNegocios();
                //Instância a camada de objêto
                Barra barra = new Barra();
                //A coleção recebe o resultado da consulta
                barra = manifestoNegocios.PesqProduto(Convert.ToString(txtBarra.Text), cmbEmpresa.Text);

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
                ConferenciaManifestoNegocios manifestoNegocios = new ConferenciaManifestoNegocios();
                //Instância a camada de objêto
                EnderecoPicking endereco = new EnderecoPicking();
                //pesquisa informações sobre o produto no picking
                endereco = manifestoNegocios.PesqEndereco(idProduto, cmbEmpresa.Text);

                if (endereco.idProduto > 0)
                {
                    //Vencimento do produto na área de separação
                    lblVencimento.Text = string.Format("{0: dd/MM/yyyy}", endereco.vencimento);
                    //Lote do produto na área de separação
                    lblLote.Text = endereco.lote;

                    //Confere o item
                    ConferenciaItem(txtManifesto.Text, idProduto, linha, multiplicador);
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

                int quantidade = Convert.ToInt32(gridItem.Rows[linha].Cells[4].Value); //Quantidade do grid
                int qtdConferida = Convert.ToInt32(gridItem.Rows[linha].Cells[6].Value); //Quantidade do grid conferida
                string uniUnidade = Convert.ToString(gridItem.Rows[linha].Cells[5].Value); //tipo de unidade fracionada  
                string uniCaixa = Convert.ToString(gridItem.Rows[linha].Cells[14].Value); //tipo de unidade da caixa
                int caixaProduto = Convert.ToInt32(gridItem.Rows[linha].Cells[15].Value); //quantidade da caixa do produto
                bool pesoVariavel = Convert.ToBoolean(gridItem.Rows[linha].Cells[16].Value); //peso variável

                //Seta a quantidade conferida
                qtdConferida = qtdConferida + Convert.ToInt32(txtQuantidade.Text) * multiplicador;

                if (qtdConferida > quantidade)
                {
                    MessageBox.Show("Quantidade conferida ultrapassa a quantidade do pedido", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (pesoVariavel == false)
                    {
                        //Seta a quantidade conferida
                        gridItem.Rows[linha].Cells[6].Value = qtdConferida;
                        //Exibe a quantidade de caixa
                        gridItem.Rows[linha].Cells[8].Value = qtdConferida / caixaProduto + " " + uniCaixa;
                        //Exibe a quantidade de unidade (conferida % quantidade)
                        gridItem.Rows[linha].Cells[9].Value = qtdConferida % caixaProduto + " " + uniUnidade;
                        //Subtrai a quantidade conferida na quantidade que falta
                        gridItem.Rows[linha].Cells[10].Value = quantidade - qtdConferida + " " + uniUnidade;
                        //Seta o vencimento
                        gridItem.Rows[linha].Cells[11].Value = lblVencimento.Text;
                        //Seta o lote
                        gridItem.Rows[linha].Cells[12].Value = lblLote.Text;
                        //Seta a data de conferencia
                        gridItem.Rows[linha].Cells[13].Value = DateTime.Now;
                    }
                    else
                    {
                        FrmPesoVariavel frame = new FrmPesoVariavel();
                        frame.idProduto = Convert.ToInt32(gridItem.Rows[linha].Cells[2].Value);
                        frame.produto = Convert.ToString(gridItem.Rows[linha].Cells[3].Value);
                        frame.manifesto = Convert.ToInt32(txtManifesto.Text);
                        frame.quantidade = Convert.ToDouble(gridItem.Rows[linha].Cells[4].Value);
                        frame.tipo = uniUnidade;

                        //Recebe as informações
                        if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //Seta a quantidade conferida
                            gridItem.Rows[linha].Cells[4].Value = string.Format("{0:N3}", frame.pesoConferido);
                            gridItem.Rows[linha].Cells[6].Value = string.Format("{0:N3}", frame.pesoConferido);
                            gridItem.Rows[linha].Cells[7].Value = string.Format("{0:N3}", frame.corteConferido) + " " + uniUnidade;
                            gridItem.Rows[linha].Cells[8].Value = frame.volume + " VOL";

                            //Subtrai a quantidade conferida na quantidade que falta
                            gridItem.Rows[linha].Cells[10].Value = 0 + " " + uniUnidade;
                            //Seta o vencimento
                            gridItem.Rows[linha].Cells[11].Value = lblVencimento.Text;
                            //Seta o lote
                            gridItem.Rows[linha].Cells[12].Value = lblLote.Text;
                            //Seta a data de conferencia
                            gridItem.Rows[linha].Cells[13].Value = DateTime.Now;

                        }
                    }

                    //Instância a camada de negocios
                    ConferenciaManifestoNegocios conferenciaNegocios = new ConferenciaManifestoNegocios();

                    if (Convert.ToInt32(gridItem.Rows[linha].Cells[4].Value) == Convert.ToInt32(gridItem.Rows[linha].Cells[6].Value))
                    {
                        //Registrar conferencia
                        conferenciaNegocios.RegistrarConferenciaItem(
                            Convert.ToInt32(gridItem.Rows[linha].Cells[2].Value), //Id do produto
                            Convert.ToInt32(gridItem.Rows[linha].Cells[6].Value), //Quantidade conferida
                            Convert.ToDateTime(gridItem.Rows[linha].Cells[11].Value), //Vencimento
                            Convert.ToString(gridItem.Rows[linha].Cells[12].Value), //Lote
                            Convert.ToInt32(txtManifesto.Text), //Código do manifesto
                            Convert.ToString(cmbEmpresa.Text)); //Empresa Nome

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
                            gridItem.Rows[linha].Cells[8].Value,
                            gridItem.Rows[linha].Cells[9].Value,
                            gridItem.Rows[linha].Cells[10].Value,
                            gridItem.Rows[linha].Cells[11].Value,
                            gridItem.Rows[linha].Cells[12].Value,
                            gridItem.Rows[linha].Cells[13].Value,
                            gridItem.Rows[linha].Cells[14].Value,
                            gridItem.Rows[linha].Cells[15].Value,
                            gridItem.Rows[linha].Cells[16].Value
                            );

                        //Remove a linha do grid
                        gridItem.Rows.RemoveAt(linha);

                        //Verifica a finalização da conferencia
                        FecharConferencia();
                    }

                    //Limpa o campos
                    txtBarra.Text = string.Empty;
                    txtQuantidade.Text = "1";
                    //Foca no campo
                    txtBarra.Focus();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ConferenciaManifestoNegocios conferenciaNegocios = new ConferenciaManifestoNegocios();

                    if (chkReentrega.Checked == false)
                    {
                        //Executa
                        conferenciaNegocios.RegistraFimConferencia(Convert.ToInt32(txtManifesto.Text), chkReentrega.Checked, codUsuario, 0, cmbEmpresa.Text);
                    }
                    else
                    {
                        //Executa
                        conferenciaNegocios.RegistraFimConferencia(Convert.ToInt32(txtManifesto.Text), chkReentrega.Checked, codUsuario, Convert.ToInt32(txtSeparador.Text), cmbEmpresa.Text);

                    }
                    MessageBox.Show("Conferencia finalizada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Limpa o campos
                    LimparCampos();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            txtManifesto.Text = string.Empty;
            txtSeparador.Text = string.Empty;
            lblSeparador.Text = "-";
            txtBarra.Text = string.Empty;
            txtQuantidade.Text = "1";

            lblVencimento.Text = "-";
            lblLote.Text = "-";
            lblPlaca.Text = "-";
            lblPedido.Text = "-";
            lblPeso.Text = "-";
            lblIncioConferencia.Text = "-";
            lblFimConferencia.Text = "-";
            lblTempo.Text = "-";

            gridItem.Rows.Clear();
            gridItemConferidos.Rows.Clear();

            txtManifesto.Enabled = true;
            chkReentrega.Enabled = true;
            txtSeparador.Enabled = true;
            txtBarra.Enabled = true;
            txtQuantidade.Enabled = true;
            btnCorteProduto.Enabled = true;

            txtManifesto.Focus();
        }

        private void txtSeparador_TextChanged(object sender, EventArgs e)
        {
            if (txtSeparador.Text == string.Empty)
            {
                lblSeparador.Text = "-";
            }
        }

        private void btnCorteProduto_Click(object sender, EventArgs e)
        {
            //Instância as linha da tabela
            DataGridViewRow index = gridItem.CurrentRow;
            //Recebe o indice   
            int linha = index.Index;

            //Instância o frame
            FrmCorteManifesto frame = new FrmCorteManifesto();
            frame.idProduto = Convert.ToInt32(gridItem.Rows[linha].Cells[2].Value);
            frame.produto = Convert.ToString(gridItem.Rows[linha].Cells[3].Value);
            frame.manifesto = Convert.ToInt32(txtManifesto.Text);
            frame.quantidade = Convert.ToInt32(gridItem.Rows[linha].Cells[4].Value);
            frame.qtdConferida = Convert.ToInt32(gridItem.Rows[linha].Cells[6].Value);
            frame.codUsuario = codUsuario;


            //Recebe as informações
            if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Seta a quantidade conferida
                gridItem.Rows[linha].Cells[4].Value = Convert.ToInt32(frame.quantidade);
                gridItem.Rows[linha].Cells[6].Value = Convert.ToInt32(frame.qtdConferida);
                gridItem.Rows[linha].Cells[7].Value = Convert.ToInt32(frame.corteConferido);// + " " + uniUnidade;                

                //Subtrai a quantidade conferida na quantidade que falta
                //gridItem.Rows[linha].Cells[10].Value = 0;//+ " " + uniUnidade;


            }

        }
    }
}
