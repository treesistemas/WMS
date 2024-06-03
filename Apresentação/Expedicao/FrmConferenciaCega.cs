using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmConferenciaCega : Form
    {
        //Código do usuário
        public int codUsuario;
        //Instância a coleção de objêtos
        private ItensNotaEntradaCollection itensNotaCegaCollection = new ItensNotaEntradaCollection();
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        //Controla a exibição do campo falta e avaria e palete
        private bool clickedFalta = false;
        private bool clickedAvaria = false;
        private bool clickedPalete = true;

        //Permite ultrapassar ou não a quantidade conferida 
        private bool UltrapassarConferencia = false;

        public FrmConferenciaCega()
        {
            InitializeComponent();
        }

        private void FrmConferenciaCega_Load(object sender, EventArgs e)
        {
            timer.Tick += new System.EventHandler(PesqNotaCegaLiberada);
            timer.Interval = 1000 * 60 * 2; //2 minutos
            timer.Start();

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

        private void txtPesqNotaCega_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisar.Focus();
            }
        }

        private void dtmVencimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gridItensConferencia.SelectedRows.Count > 0)
                {
                    AdicionaVencimento();
                }

                txtLote.Focus();
            }
        }

        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gridItensConferencia.SelectedRows.Count > 0)
                {
                    AdicionaLote();
                }
                txtBarra.Focus();
            }
        }

        private void txtBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtQuantidade.Focus();
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnConfirmar.Focus();
            }
        }

        private void txtPendente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnRegistrarControle.Focus();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa a nota cega
            PesqNotaCega();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            //Pesquisa e executa a conferência do produto
            AdicionaProduto();
        }

        private void btnPendencia_Click(object sender, EventArgs e)
        {
            //Exibe os itens que não foram conferidos
            ExibirItensPendentes();
        }

        private void btnFalta_Click(object sender, EventArgs e)
        {
            if (clickedFalta == false)
            {
                //Exibe o campo de falta
                grpControle.Text = "Digite a falta em unidade";
                grpControle.Visible = true;

                clickedFalta = true;
                clickedAvaria = false;
                clickedPalete = false;
                txtControle.Focus();
            }
            else
            {
                //Exibe o texto de palete
                grpControle.Text = "Associação de Palete";
                clickedFalta = false;
                clickedPalete = true;
                txtBarra.Focus();
            }
        }

        private void btnAvaria_Click(object sender, EventArgs e)
        {
            if (clickedAvaria == false)
            {
                //Exibe o campo de avaria
                grpControle.Text = "Digite a avaria em unidade";
                grpControle.Visible = true;

                clickedAvaria = true;
                clickedFalta = false;
                clickedPalete = false;
                txtControle.Focus();
            }
            else
            {
                //Exibe o texto de palete
                grpControle.Text = "Associação de Palete";
                clickedAvaria = false;
                clickedPalete = true;
                txtBarra.Focus();
            }

        }

        private void btnRegistrarControle_Click(object sender, EventArgs e)
        {
            AdicionaFaltaAvariaPalete();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            //Limpa os campos
            LimparCampos();
            //Desabilita os campos
            DesabilitarCampos();
            //Foca no campo de nota cega
            txtPesqNotaCega.Focus();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            //Fecha a conferência
            FinalizarConferencia();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o frame
            Close();
        }

        //Pesquisa as restrições da conferência cega na configuração do sistema
        private void PesqRestricoes()
        {
            try
            {
                //Instância o negocios
                ConferenciaCegaNegocios conferenciaCegaNegocios = new ConferenciaCegaNegocios();

                string restriao1 = "NÃO PERMITIR QUE A CONFERÊNCIA EXCEDA A QUANTIDADE (NOTA CEGA)";

                //Variável recebe o resultado da consulta
                UltrapassarConferencia = conferenciaCegaNegocios.PesqRestricao(restriao1, cmbEmpresa.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa as notas cegas liberadas para conferência
        private void PesqNotaCegaLiberada(object sender, System.EventArgs e)
        {
            try
            {
                //Instância o negocios
                ConferenciaCegaNegocios conferenciaCegaNegocios = new ConferenciaCegaNegocios();
                //Instância a camada de objêtos
                NotaEntradaCollection notaCegaCollection = new NotaEntradaCollection();
                notaCegaCollection = conferenciaCegaNegocios.PesqNotaCegaLiberada();

                //grid recebe o resultado da coleção
                notaCegaCollection.ForEach(n => gridNotaCega.Rows.Add(gridNotaCega.Rows.Count + 1, n.codNotaCega, n.nmFornecedor));
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa a nota cega
        private void PesqNotaCega()
        {
            try
            {
                if (txtPesqNotaCega.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, digite o número da nota cega!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o negocios
                    ConferenciaCegaNegocios conferenciaCegaNegocios = new ConferenciaCegaNegocios();
                    //Instância a coleção
                    NotaEntrada notaCega = new NotaEntrada();
                    //Objeto recebe o resultado da consulta
                    notaCega = conferenciaCegaNegocios.PesqNotaCega(txtPesqNotaCega.Text, cmbEmpresa.Text);

                    if (notaCega.codNotaCega != 0)
                    {
                        //passa os valores para os campos Recebe o resultado da coleção    
                        lblFornecedor.Text = notaCega.codFornecedor + " - " + notaCega.nmFornecedor;
                        lblUsuario.Text = notaCega.usuarioNotaCega;
                        lblQtdNota.Text = Convert.ToString(notaCega.quantidadeNota);
                        lblQtdItens.Text = Convert.ToString(notaCega.quantidadeItens);
                        lblSomaPeso.Text = string.Format(@"{0:N}", notaCega.pesoNota);

                        if (notaCega.exigirValidade == true)
                        {
                            lblExigeVencimento.Text = "SIM";
                        }
                        else
                        {
                            lblExigeVencimento.Text = "NÃO";
                        }

                        if (notaCega.crossDocking == true)
                        {
                            lblCrossDocking.Text = "SIM";
                        }
                        else
                        {
                            lblCrossDocking.Text = "NÃO";
                        }

                        if (!Convert.ToString(notaCega.fimConferencia).Equals(""))
                        {
                            //Exibe os itens
                            ExibirItensPendentes();
                            txtPesqNotaCega.Focus();
                            txtPesqNotaCega.SelectAll();
                            MessageBox.Show("Nota Cega já conferida!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (Convert.ToString(notaCega.inicioConferencia).Equals(""))
                            {
                                //Adiciona inicio de conferencia
                                AdicionarInicioConferencia(Convert.ToString(notaCega.codNotaCega), codUsuario);
                            }

                            //Pesquisa os itens da nota cega
                            PesqItensNotaCega();
                            //Habilita os campos
                            HabilitarCampos();
                            //Pesquisa a restrição da conferência nas configurações do sistema
                            PesqRestricoes();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma nota cega encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os itens da nota cega
        private void PesqItensNotaCega()
        {
            try
            {
                //Instância o negocios
                ConferenciaCegaNegocios conferenciaCegaNegocios = new ConferenciaCegaNegocios();
                //Limpa o grid de itens
                gridItensConferencia.Rows.Clear();
                //Limpa a coleção de objetos
                itensNotaCegaCollection.Clear();
                //Objeto recebe o resultado da consulta
                itensNotaCegaCollection = conferenciaCegaNegocios.PesqItensNotaCega(txtPesqNotaCega.Text, cmbEmpresa.Text);

                if (itensNotaCegaCollection.Count > 0)
                {
                    //Adiciona o itens ao grid
                    for (int i = 0; itensNotaCegaCollection.Count > i; i++)
                    {
                        if (itensNotaCegaCollection[i].quantidadeConferida > 0)
                        {
                            gridItensConferencia.Rows.Add(gridItensConferencia.Rows.Count + 1, itensNotaCegaCollection[i].idProduto, itensNotaCegaCollection[i].codProduto,
                                itensNotaCegaCollection[i].descProduto, itensNotaCegaCollection[i].quantidadeNota, itensNotaCegaCollection[i].fatorPulmao,
                                //Quantidade
                                itensNotaCegaCollection[i].quantidadeConferida,
                                 //Resultado da conferênia em caixa
                                 itensNotaCegaCollection[i].quantidadeConferida / itensNotaCegaCollection[i].fatorPulmao,
                                 //Resultado da conferência em unidade
                                 itensNotaCegaCollection[i].quantidadeConferida % itensNotaCegaCollection[i].fatorPulmao,
                                 itensNotaCegaCollection[i].quantidadeFalta, itensNotaCegaCollection[i].quantidadeAvariada,
                                String.Format("{0:d}", itensNotaCegaCollection[i].validadeProduto), itensNotaCegaCollection[i].loteProduto, itensNotaCegaCollection[i].vidaProduto,
                                itensNotaCegaCollection[i].toleranciaProduto, itensNotaCegaCollection[i].descCategoria, itensNotaCegaCollection[i].controlaVencimentoCategoria, itensNotaCegaCollection[i].controlaLoteCategoria, itensNotaCegaCollection[i].paleteAssociado);

                            //Se a categoria controla o lote
                            if (Convert.ToBoolean(itensNotaCegaCollection[i].controlaLoteCategoria) == true)
                            {
                                lblExigeLote.Text = "SIM";

                            }

                        }
                    }

                    //Verifica se existe item com a quantidade diferente da conferida para zerar
                    for (int i = 0; gridItensConferencia.Rows.Count > i; i++)
                    {
                        if ((Convert.ToInt32(gridItensConferencia.Rows[i].Cells[4].Value) * Convert.ToInt32(gridItensConferencia.Rows[i].Cells[5].Value)) < (Convert.ToInt32(gridItensConferencia.Rows[i].Cells[6].Value) + Convert.ToInt32(gridItensConferencia.Rows[i].Cells[9].Value) + Convert.ToInt32(gridItensConferencia.Rows[i].Cells[10].Value)))
                        {
                            gridItensConferencia.Rows[i].Cells[6].Value = "0";
                            gridItensConferencia.Rows[i].Cells[7].Value = "0";
                            gridItensConferencia.Rows[i].Cells[8].Value = "0";
                            //Zera a conferencia
                            AdicionaQuantidade(0);
                        }                      

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os itens da nota cega
        private void AdicionaProduto()
        {
            try
            {
                if (txtBarra.Text.Equals(""))
                {
                    txtBarra.Focus();
                    MessageBox.Show("Digite o código de barra do produto!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o negocios
                    BarraNegocios barraNegocios = new BarraNegocios();
                    //instância a camada de objêtos
                    Barra barra = new Barra();
                    //Objeto recebe o resultado da consulta
                    barra = barraNegocios.PesqCodBarra(txtBarra.Text);

                    if (barra.idProduto > 0)
                    {
                        //Localiza o produto 
                        List<ItensNotaEntrada> itensNota = itensNotaCegaCollection.FindAll(delegate (ItensNotaEntrada n) { return n.idProduto == barra.idProduto; });

                        if (itensNota.Count > 0)
                        {
                            //Controla a insenção do item no grid
                            bool itemGrid = false;

                            //verifica se o produto já está no grid
                            for (int i = 0; gridItensConferencia.Rows.Count > i; i++)
                            {
                                if (Convert.ToInt32(gridItensConferencia.Rows[i].Cells[1].Value) == barra.idProduto)
                                {

                                    if (txtQuantidade.Text.Equals(""))
                                    {
                                        MessageBox.Show("Digite a quantidade!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        //contole de quantidade de entrada para não ultrapassar
                                        if (UltrapassarConferencia == true &&
                                            //Quantidade de entrada
                                            (Convert.ToInt32(gridItensConferencia.Rows[i].Cells[4].Value) * Convert.ToInt32(gridItensConferencia.Rows[i].Cells[5].Value)) <
                                            //Quandidade conferida
                                            Convert.ToInt32(gridItensConferencia.Rows[i].Cells[6].Value) + Convert.ToInt32(txtQuantidade.Text) * barra.multiplicador +
                                            //Quantidade que faltou e avariou
                                            (Convert.ToInt32(gridItensConferencia.Rows[i].Cells[9].Value) + Convert.ToInt32(gridItensConferencia.Rows[i].Cells[10].Value)))
                                        {
                                            txtBarra.Clear();
                                            txtQuantidade.Clear();
                                            txtBarra.Focus();
                                            MessageBox.Show("Quantidade conferida ultrapassa a quantidade de entrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        else
                                        {
                                            //controle de quantidade conferida
                                            gridItensConferencia.Rows[i].Cells[6].Value = Convert.ToInt32(gridItensConferencia.Rows[i].Cells[6].Value) + Convert.ToInt32(txtQuantidade.Text) * barra.multiplicador;
                                            //caixa
                                            gridItensConferencia.Rows[i].Cells[7].Value = Convert.ToInt32(gridItensConferencia.Rows[i].Cells[6].Value) / Convert.ToInt32(gridItensConferencia.Rows[i].Cells[5].Value);
                                            //unidade
                                            gridItensConferencia.Rows[i].Cells[8].Value = Convert.ToInt32(gridItensConferencia.Rows[i].Cells[6].Value) % Convert.ToInt32(gridItensConferencia.Rows[i].Cells[5].Value);

                                            //Seleciona a linha do grid
                                            gridItensConferencia.CurrentCell = gridItensConferencia.Rows[i].Cells[0];

                                            //Adiciona a quantidade conferida
                                            AdicionaQuantidade(Convert.ToInt32(gridItensConferencia.Rows[i].Cells[6].Value));
                                            //Controle para não inserir o item no grid
                                            itemGrid = true;
                                        }

                                    }
                                }
                            }

                            //inseri o item no grid
                            if (itemGrid == false)
                            {
                                if (txtQuantidade.Text.Equals(""))
                                {
                                    MessageBox.Show("Digite a quantidade!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    //grid recebe o resultado da coleção
                                    itensNota.ForEach(n => gridItensConferencia.Rows.Add(Convert.ToInt32(gridItensConferencia.Rows.Count) + 1, n.idProduto, n.codProduto, n.descProduto, n.quantidadeNota,
                                    n.fatorPulmao,
                                    Convert.ToInt32(txtQuantidade.Text) * barra.multiplicador,
                                    (Convert.ToInt32(txtQuantidade.Text) * barra.multiplicador) / n.fatorPulmao,
                                    (Convert.ToInt32(txtQuantidade.Text) * barra.multiplicador) % n.fatorPulmao,
                                    n.quantidadeFalta, n.quantidadeAvariada, String.Format("{0:d}", n.validadeProduto), txtLote.Text, n.vidaProduto,
                                    n.toleranciaProduto, n.descCategoria, n.controlaVencimentoCategoria, n.controlaLoteCategoria, n.paleteAssociado));

                                    //Seleciona a linha do grid
                                    gridItensConferencia.CurrentCell = gridItensConferencia.Rows[gridItensConferencia.Rows.Count - 1].Cells[0];

                                    int ultimaLinha = gridItensConferencia.Rows.Count - 1;

                                    //Se não controla a validade
                                    if (Convert.ToBoolean(gridItensConferencia.Rows[ultimaLinha].Cells[16].Value) == false)
                                    {
                                        gridItensConferencia.Rows[ultimaLinha].Cells[11].Value = String.Format("{0:d}", dtmVencimento.Value);
                                        AdicionaVencimento();
                                    }

                                    //Se a categoria controla o lote
                                    if (Convert.ToBoolean(gridItensConferencia.Rows[ultimaLinha].Cells[17].Value) == true)
                                    {
                                        lblExigeLote.Text = "SIM";

                                    }


                                    //quantidade de entrada
                                    if (UltrapassarConferencia == true &&
                                    //Quantidade de entrada
                                    (Convert.ToInt32(gridItensConferencia.Rows[ultimaLinha].Cells[4].Value) * Convert.ToInt32(gridItensConferencia.Rows[ultimaLinha].Cells[5].Value)) <
                                    //Quandidade conferida
                                    Convert.ToInt32(gridItensConferencia.Rows[ultimaLinha].Cells[6].Value) +
                                    //Quantidade que faltou e avariou
                                    (Convert.ToInt32(gridItensConferencia.Rows[ultimaLinha].Cells[9].Value) + Convert.ToInt32(gridItensConferencia.Rows[ultimaLinha].Cells[10].Value)))
                                    {
                                        gridItensConferencia.Rows[ultimaLinha].Cells[6].Value = 0;
                                        gridItensConferencia.Rows[ultimaLinha].Cells[7].Value = 0;
                                        gridItensConferencia.Rows[ultimaLinha].Cells[8].Value = 0;
                                        MessageBox.Show("Quantidade conferida ultrapassa a quantidade de entrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                    else
                                    {
                                        //Adiciona a quantidade conferida
                                        AdicionaQuantidade(Convert.ToInt32(gridItensConferencia.Rows[gridItensConferencia.Rows.Count - 1].Cells[6].Value));
                                    }

                                }
                            }
                            //limpa o campo
                            txtBarra.Clear();
                            txtQuantidade.Clear();
                            txtBarra.Focus();
                        }
                        else
                        {
                            txtBarra.Focus();
                            MessageBox.Show("Código de barra não está relacionado a nenhum produto da nota cega!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        txtBarra.Focus();
                        MessageBox.Show("Código de barra não encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Exibe os itens que não foram conferidos
        private void ExibirItensPendentes()
        {
            try
            {
                //Instância o negocios
                ConferenciaCegaNegocios conferenciaCegaNegocios = new ConferenciaCegaNegocios();
                //instância a camada de objêto
                ItensNotaEntradaCollection itenPendentes = new ItensNotaEntradaCollection();
                //Limpa o grid de itens
                gridItensConferencia.Rows.Clear();
                //Objeto recebe o resultado da consulta
                itenPendentes = conferenciaCegaNegocios.PesqItensNotaCega(txtPesqNotaCega.Text, cmbEmpresa.Text);

                if (itenPendentes.Count > 0)
                {
                    itenPendentes.ForEach(n => gridItensConferencia.Rows.Add(gridItensConferencia.Rows.Count + 1, n.idProduto, n.codProduto,
                        n.descProduto, n.quantidadeNota, n.fatorPulmao, n.quantidadeConferida, n.quantidadeConferida / n.fatorPulmao,
                         n.quantidadeConferida % n.fatorPulmao, n.quantidadeFalta, n.quantidadeAvariada, String.Format("{0:d}", n.validadeProduto), n.loteProduto, n.vidaProduto,
                         n.toleranciaProduto, n.descCategoria, n.controlaVencimentoCategoria, n.controlaLoteCategoria, n.paleteAssociado));
                    //Foca no campo de barra
                    txtBarra.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Adiciona a data inicial de conferencia
        private void AdicionarInicioConferencia(string notaCega, int codUsuario)
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaCegaNegocios conferenciaCegaNegocios = new ConferenciaCegaNegocios();
                // Passa a categoria para a camada de negocios
                conferenciaCegaNegocios.AdicionarInicioConferencia(notaCega, codUsuario, cmbEmpresa.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Adiciona a quantidade conferida
        private void AdicionaQuantidade(int quantidade)
        {
            try
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridItensConferencia.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Instância a camada de negocios
                ConferenciaCegaNegocios notaCega = new ConferenciaCegaNegocios();

                //Adiciona a falta do item
                notaCega.AdicionarQuantidade(txtPesqNotaCega.Text, Convert.ToInt32(gridItensConferencia.Rows[indice].Cells[1].Value), quantidade, dtmVencimento.Value);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Adiciona o vencimento
        private void AdicionaVencimento()
        {
            //Instância a data atual
            DateTime dataAtual = DateTime.Today;

            //Instância as linha da tabela
            DataGridViewRow linha = gridItensConferencia.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;

            //Instância a camada de negocios
            ConferenciaCegaNegocios notaCega = new ConferenciaCegaNegocios();

            //Se não controla a validade
            if (Convert.ToBoolean(gridItensConferencia.Rows[indice].Cells[16].Value) == false)
            {
                gridItensConferencia.Rows[indice].Cells[11].Value = String.Format("{0:d}", dtmVencimento.Value);
                //Adiciona o vencimento do item
                notaCega.AdicionarVencimento(txtPesqNotaCega.Text, Convert.ToInt32(gridItensConferencia.Rows[indice].Cells[1].Value), dtmVencimento.Value, cmbEmpresa.Text);

            }


            //Controla a validade ao inserir o produto
            if (String.Format("{0:d}", dtmVencimento.Value) != dataAtual.ToString("d"))
            {

                int ValidadeMaxima = Convert.ToInt32(Convert.ToInt32(gridItensConferencia.Rows[indice].Cells[13].Value) - (Convert.ToDouble(gridItensConferencia.Rows[indice].Cells[14].Value) / 100) * Convert.ToInt32(gridItensConferencia.Rows[indice].Cells[13].Value));

                if (dtmVencimento.Value < dataAtual)
                {
                    MessageBox.Show("A data digitada não pode ser menor que a data atual!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (dtmVencimento.Value > dataAtual.AddDays(Convert.ToInt32(gridItensConferencia.Rows[indice].Cells[13].Value)))
                {
                    MessageBox.Show("A data digitada não pode ultrapassar o shelf life do produto!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (dtmVencimento.Value < dataAtual.AddDays(ValidadeMaxima))
                {
                    if (MessageBox.Show("A data digitada é menor que a tolerância (" + dataAtual.AddDays(ValidadeMaxima).ToString("d") + ") para o recebimento do produto, deseja continuar?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        gridItensConferencia.Rows[indice].Cells[11].Value = String.Format("{0:d}", dtmVencimento.Value);
                        //Adiciona o vencimento do item
                        notaCega.AdicionarVencimento(txtPesqNotaCega.Text, Convert.ToInt32(gridItensConferencia.Rows[indice].Cells[1].Value), dtmVencimento.Value, cmbEmpresa.Text);
                        //Adiciona a data atual
                        dtmVencimento.Value = dataAtual;
                    }
                }
                else
                {
                    gridItensConferencia.Rows[indice].Cells[11].Value = String.Format("{0:d}", dtmVencimento.Value);
                    //Adiciona o vencimento do item
                    notaCega.AdicionarVencimento(txtPesqNotaCega.Text, Convert.ToInt32(gridItensConferencia.Rows[indice].Cells[1].Value), dtmVencimento.Value, cmbEmpresa.Text);
                    //Adiciona a data atual
                    dtmVencimento.Value = dataAtual;
                }
            }
        }

        //Adiciona o lote
        private void AdicionaLote()
        {
            //Instância as linha da tabela
            DataGridViewRow linha = gridItensConferencia.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;

            //Instância a camada de negocios
            ConferenciaCegaNegocios notaCega = new ConferenciaCegaNegocios();

            //Controla o lote ao inserir o produto
            if (!txtLote.Text.Equals(""))
            {
                gridItensConferencia.Rows[indice].Cells[12].Value = txtLote.Text;
                //Adiciona o lote do item
                notaCega.AdicionarLote(txtPesqNotaCega.Text, Convert.ToInt32(gridItensConferencia.Rows[indice].Cells[1].Value), txtLote.Text, cmbEmpresa.Text);
            }

            //Limpa o campo lote
            txtLote.Clear();
        }

        //Adiciona a falta e avaria
        private void AdicionaFaltaAvariaPalete()
        {
            try
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridItensConferencia.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Instância a camada de negocios
                ConferenciaCegaNegocios notaCega = new ConferenciaCegaNegocios();

                //Controla a falta
                if (clickedPalete == true)
                {
                    gridItensConferencia.Rows[indice].Cells[18].Value = txtControle.Text;
                    //Adiciona a falta do item
                    notaCega.AdicionarProdutoPalete(txtPesqNotaCega.Text, Convert.ToInt32(gridItensConferencia.Rows[indice].Cells[1].Value), Convert.ToInt32(txtControle.Text), cmbEmpresa.Text);
                }

                //Controla a falta
                if (clickedFalta == true)
                {
                    gridItensConferencia.Rows[indice].Cells[9].Value = txtControle.Text;
                    //Adiciona a falta do item
                    notaCega.AdicionarFalta(txtPesqNotaCega.Text, Convert.ToInt32(gridItensConferencia.Rows[indice].Cells[1].Value), Convert.ToInt32(txtControle.Text), cmbEmpresa.Text);
                    MessageBox.Show("Falta registrada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Controla a avaria
                if (clickedAvaria == true)
                {
                    gridItensConferencia.Rows[indice].Cells[10].Value = txtControle.Text;
                    //Adiciona a avaria do item
                    notaCega.AdicionarAvaria(txtPesqNotaCega.Text, Convert.ToInt32(gridItensConferencia.Rows[indice].Cells[1].Value), Convert.ToInt32(txtControle.Text), cmbEmpresa.Text);

                    MessageBox.Show("Avaria registrada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Limpa o campo de pendencia
                txtControle.Clear();
                //Exibe o texto de palete
                grpControle.Text = "Associação de Palete";
                //Controla a exibição
                clickedFalta = false;
                clickedAvaria = false;
                clickedPalete = true;
                txtBarra.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Finaliza a conferencia
        private void FinalizarConferencia()
        {
            try
            {
                //Exibir os itens da conferencia
                ExibirItensPendentes();

                bool controleFimConferencia = true;
                string pendencia = string.Empty;

                //Verifica se a conferencia está correta                
                foreach (DataGridViewRow row in gridItensConferencia.Rows)
                {
                    if ((Convert.ToInt32(row.Cells[6].Value) + Convert.ToInt32(row.Cells[9].Value) + Convert.ToInt32(row.Cells[10].Value)) < Convert.ToInt32(row.Cells[4].Value) * Convert.ToInt32(row.Cells[5].Value))
                    {
                        gridItensConferencia.Rows[row.Index].Cells[0].Style.BackColor = Color.Green;
                        //gridItensConferencia.Refresh();
                        pendencia = "ITEM PENDENTE";
                        controleFimConferencia = false;
                    }

                    if (lblExigeVencimento.Text.Equals("SIM"))
                    {
                        if (Convert.ToString(row.Cells[11].Value).Equals("01/01/0001"))
                        {
                            gridItensConferencia.Rows[row.Index].Cells[0].Style.BackColor = Color.Orange;
                            pendencia = "VENCIMENTO";
                            controleFimConferencia = false;
                        }
                    }

                    //Verifica o lote de produto
                    if (Convert.ToBoolean(row.Cells[17].Value) == true)
                    {
                        if (Convert.ToString(row.Cells[12].Value).Equals(""))
                        {
                            gridItensConferencia.Rows[row.Index].Cells[0].Style.BackColor = Color.Yellow;
                            pendencia = "LOTE";
                            controleFimConferencia = false;
                        }
                    }

                    if ((Convert.ToInt32(row.Cells[6].Value) + Convert.ToInt32(row.Cells[9].Value) + Convert.ToInt32(row.Cells[10].Value)) > Convert.ToInt32(row.Cells[4].Value) * Convert.ToInt32(row.Cells[5].Value))
                    {
                        //Zera a conferencia 
                        AdicionaQuantidade(0);
                        gridItensConferencia.Rows[row.Index].Cells[6].Value = 0;
                        gridItensConferencia.Rows[row.Index].Cells[7].Value = 0;
                        gridItensConferencia.Rows[row.Index].Cells[8].Value = 0;
                        gridItensConferencia.Rows[row.Index].Cells[0].Style.BackColor = Color.Red;
                        pendencia = "ULTRAPASSA QUANTIDADE";
                        controleFimConferencia = false;
                    }
                }

                if(pendencia.Equals("ULTRAPASSA QUANTIDADE"))
                {
                    MessageBox.Show("Existem itens que ultrapassa a quantidade de entrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (pendencia.Equals("ITEM PENDENTE"))
                {
                    MessageBox.Show("Existem itens pendentes para conferir, por favor verificar!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (pendencia.Equals("VENCIMENTO"))
                {
                    MessageBox.Show("Existe itens sem data de vencimento, por favor preencher!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (pendencia.Equals("LOTE"))
                {
                    MessageBox.Show("Existem itens sem lote cadastrado, por favor preencher!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (controleFimConferencia == true)
                {
                    //Instância o negocios
                    ConferenciaCegaNegocios conferenciaCegaNegocios = new ConferenciaCegaNegocios();
                    // Adiciona o fim da conferencia
                    conferenciaCegaNegocios.AdicionarFimConferencia(txtPesqNotaCega.Text, codUsuario, cmbEmpresa.Text);
                    //Limpa os campos
                    LimparCampos();
                    //Desabiltar campos
                    DesabilitarCampos();
                    MessageBox.Show("Conferência realizada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            txtPesqNotaCega.Clear();
            txtLote.Clear();
            txtBarra.Clear();
            txtQuantidade.Clear();
            txtControle.Clear();
            lblFornecedor.Text = "-";
            lblUsuario.Text = "-";
            lblQtdNota.Text = "-";
            lblQtdItens.Text = "-";
            lblSomaPeso.Text = "-";
            lblExigeVencimento.Text = "-";
            lblCrossDocking.Text = "-";
            lblExigeLote.Text = "-";

            gridItensConferencia.Rows.Clear();

            txtPesqNotaCega.Focus();
        }

        //Habilita os campos
        private void HabilitarCampos()
        {
            txtPesqNotaCega.Enabled = false;
            dtmVencimento.Enabled = true;
            txtLote.Enabled = true;
            txtBarra.Enabled = true;
            txtQuantidade.Enabled = true;

            btnConfirmar.Enabled = true;
            btnExibirPendencia.Enabled = true;
            btnFalta.Enabled = true;
            btnAvaria.Enabled = true;
            btnFinalizar.Enabled = true;

            txtBarra.Focus();
        }

        //Desabilita os campos
        private void DesabilitarCampos()
        {
            txtPesqNotaCega.Enabled = true;
            dtmVencimento.Enabled = false;
            txtLote.Enabled = false;
            txtBarra.Enabled = false;
            txtQuantidade.Enabled = false;

            btnConfirmar.Enabled = false;
            btnExibirPendencia.Enabled = false;
            btnFalta.Enabled = false;
            btnAvaria.Enabled = false;
            btnFinalizar.Enabled = false;
        }

        private void label54_Click(object sender, EventArgs e)
        {

        }
    }
}
