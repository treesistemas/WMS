using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Utilitarios;

namespace Wms
{
    public partial class FrmConferenciaFlowRack : Form
    {
        public int codUsuario; //Código do usuário
        public int codEstacao; //Código da estação
        public string descEstacao; //Descriçao da estação
        public int nivelEstacao; //Nivel da estação
        public string empresa; //Empresa

        int posicao = 0;//Controla a posição dos itens - Pulo
        int codCaixa; //Variável responsável pelo Código da caixa     
        int novaSugestao = 0; //Controla a sugestão de caixa - Pula

        //Instância a coleção de itens
        ItensFlowRackCollection itensFlowRackCollection = new ItensFlowRackCollection();
        //Instância a camada de objêto - Coleção
        CaixaCollection caixaCollection = new CaixaCollection();

        //Instância o frame de mensagem
        FrmMensagem mensagem = new FrmMensagem();

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmConferenciaFlowRack()
        {
            InitializeComponent();
        }

        //Load
        private void FrmConferenciaFlowRack_Load(object sender, EventArgs e)
        {
            lblDescEstacao.Text = descEstacao.ToUpper().ToString();


            //timer.Tick += new System.EventHandler(GerarAbastecimento);
            //timer.Interval = 1000 * 60 * 2; //2 minutos
            //timer.Start();


            timer.Tick += new System.EventHandler(PesqPedidosNaoProcessados);
            timer.Interval = 1000 * 30; //1 minutos
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

        //KeyPress
        private void txtPesqItens_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesqItemPedido(); //Pesquisa os itens do Flow Rack
            }
        }

        private void txtBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                PesCodigoBarra(); //Pesquisa o código de barra do item
            }
        }

        //Changed
        private void rbtSim_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSim.Checked == true)
            {
                if (itensFlowRackCollection.Count > 0)
                {
                    if (codCaixa > 0)
                    {
                        //Salva a sugestão nos itens analizados
                        SalvarSugestaoItem();
                    }
                    else
                    {
                        rbtSim.Checked = false;
                        mensagem.Texto("Nenhuma sugestão encontrada!", "Red");
                        mensagem.ShowDialog();
                    }
                }
                else
                {
                    mensagem.Texto("Nenhum volume iniciado!", "Red");
                    mensagem.ShowDialog();
                }
            }
        }

        private void rbtNao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtNao.Checked == true)
            {
                if (itensFlowRackCollection.Count > 0)
                {
                    if (caixaCollection.Count > 0)
                    {
                        if (caixaCollection.Count - 1 > novaSugestao)
                        {
                            novaSugestao++;

                            //Exibe a sugestão de caixa
                            lblSugestao.Text = "CAIXA DE " + caixaCollection[novaSugestao].descCaixa;
                            //Armazena o código da caixa na variável
                            codCaixa = caixaCollection[novaSugestao].codCaixa;

                            //Desmarca 
                            rbtNao.Checked = false;
                        }
                        else
                        {
                            //Exibe a mensagem caso não haja mais caixa a ser sugerida
                            lblSugestao.Text = "Caixa com as especificações analisadas não encontrada.";
                        }
                    }
                }

            }
        }

        //Click
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            PesCodigoBarra(); //Pesquisa o código de barra do item            
        }

        private void btnVolume_Click(object sender, EventArgs e)
        {
            // Gera um novo volume de conferência e imprime a etiqueta
            NovoVolume();
        }

        private void btnPular_Click(object sender, EventArgs e)
        {
            PularItem(); //Pula o items para o proximo da lista
        }

        private void btnCorte_Click(object sender, EventArgs e)
        {
            if (itensFlowRackCollection[0].notaFiscal > 0)
            {
                mensagem.Texto("Corte não permitido! Pedido já faturado!", "Red");
                //Exibe a mensagem
                mensagem.ShowDialog();

            }
            else
            {
                CortePedido(); //Corta o item do pedido
            }
        }

        private void btnReimpressao_Click(object sender, EventArgs e)
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaFlowRackNegocios itensFlowRackNegocios = new ConferenciaFlowRackNegocios();
                //Instância uma coleção de objêto 
                Pedido pedido = new Pedido();
                //Pesquisa o volume atual do pedido + informações da etiqueta
                pedido = itensFlowRackNegocios.PesqNovoVolume(txtPesqVolume.Text);
                //Seta o número do volume
                pedido.numeroVolume = lblnumeroVolume.Text;
                //Imprime a etiqueta
                ImprimirEtiqueta(pedido);
                //Foca no campo de barra
                txtBarra.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovaConferencia_Click(object sender, EventArgs e)
        {
            LimparCampos(); //Limpa tudo para uma nova conferência
        }

        //Pesquisa pedidos não processados para a estação
        private void PesqPedidosNaoProcessados(object sender, System.EventArgs e)
        {
            try
            {
                if (txtPesqVolume.Enabled == true)
                {
                    //Instância a camada de negocios
                    ConferenciaFlowRackNegocios conferenciaNegocios = new ConferenciaFlowRackNegocios();
                    //Instância uma coleção de objêtos
                    PedidoCollection pedidoCollection = new PedidoCollection();

                    //Garante que seja executado pela thread
                    Invoke((MethodInvoker)delegate ()
                    {
                        //Pesquisa os pedidos enviados para processamento
                        pedidoCollection = conferenciaNegocios.PesqPedidosNaoProcessados(codEstacao);

                        if (pedidoCollection.Count > 0)
                        {

                            //Atualiza e imprime a etiqueta do volume
                            foreach (Pedido pedido in pedidoCollection)
                            {
                                ImprimirEtiqueta(pedido);
                                //Atualiza a data de criação do volume
                                conferenciaNegocios.AtualizaDataVolume(pedido.codPedido);
                            }
                        }
                    });
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao pesquisar os pedidos enviados para a estação! \nDetalhes: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Analisa todo o pedido para enviar uma sugestão de caixa
        private void AnalisarPedidoSugestao()
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaFlowRackNegocios itensFlowRackNegocios = new ConferenciaFlowRackNegocios();
                //Instância a camada de objêto
                ItensFlowRackCollection analisarItens = new ItensFlowRackCollection();


                //A coleção recebe o resultado da consulta
                analisarItens = itensFlowRackNegocios.AnalisaItensSugetao(txtPesqVolume.Text, codEstacao);

                if (analisarItens.Count > 0)
                {
                    //Variável - Categoria do primeiro item
                    int primeiraEstacao = analisarItens[0].codEstacao;

                    int estacaoFinal = 0;

                    //Variável - Categoria do primeiro item
                    string primeiraCategoria = analisarItens[0].descCategoria;

                    //Variável - recebe a soma da cubagem
                    double cubagem = 0;

                    //Variável - recebe a soma do peso
                    double peso = 0;

                    //Percorre todos os itens analisando as categorias
                    for (int i = 0; analisarItens.Count > i; i++)
                    {
                        //1º Verifica se o pedido possui a mesma categoria
                        if (primeiraCategoria.Equals(analisarItens[i].descCategoria))
                        {
                            estacaoFinal = analisarItens[i].codEstacao;

                            cubagem = cubagem + analisarItens[i].cubagemProduto;

                            peso = peso + analisarItens[i].pesoProduto;
                        }
                        //2º Verifica se a categoria diferente são restrita
                        else
                        {
                            string restricao = itensFlowRackNegocios.PesqRestricaoCategoria(primeiraCategoria, analisarItens[i].descCategoria);

                            //Se não são restrita, continua a executar a soma da cubagem
                            if (restricao.Equals('N'))
                            {
                                estacaoFinal = analisarItens[i].codEstacao;

                                cubagem = cubagem + analisarItens[i].cubagemProduto;

                                peso = peso + analisarItens[i].pesoProduto;
                            }
                            else if (restricao.Equals('S'))
                            {
                                //Para de somar para apresentar a sugestão
                                break;
                            }
                        }
                    }

                    //MessageBox.Show("A caixa com a cubagem igual a: " + string.Format(@"{0:0.00000}", cubagem) + "  Com a capacidade de peso: " + string.Format(@"{0:0.000}", peso));

                    //Pesquisa a caixa
                    caixaCollection = itensFlowRackNegocios.PesqCaixa(cubagem, peso);

                    if (caixaCollection.Count > 0)
                    {
                        //Exibe a sugestão de caixa
                        lblSugestao.Text = "CAIXA DE " + caixaCollection[0].descCaixa;
                        lblUtilidade.Text = "A CAIXA SUGERIDA SERÁ UTILIZADA DA ESTAÇÃO " + primeiraEstacao + " ATÉ A \nESTAÇÃO " + estacaoFinal;
                        //Armazena o código da caixa na variável
                        codCaixa = caixaCollection[0].codCaixa;
                    }
                    else
                    {
                        lblSugestao.Text = "CAIXA COM AS ESPECIFICAÇÕES ANALISADAS NÃO ENCONTRADA.";
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os itens do Flow Rack
        private void PesqItemPedido()
        {
            try
            {
                if (txtPesqVolume.Text.Equals(""))
                {
                    mensagem.Texto("POR FAVOR, DIGITE OU BIPE ALGUM VOLUME!", "Red");
                    mensagem.ShowDialog();
                }
                else
                {
                    //Instância a camada de negocios
                    ConferenciaFlowRackNegocios itensFlowRackNegocios = new ConferenciaFlowRackNegocios();

                    //Pesquisa em qual estação o volume deverá está
                    ItensFlowRack itensFlowRack = itensFlowRackNegocios.PesqEstacaoVolume(txtPesqVolume.Text);

                    if (itensFlowRack.codEstacao == 0 && itensFlowRack.codEstacaoAtual == 0 && itensFlowRack.qtdConferidaProduto > 0)
                    {
                        mensagem.Texto("PEDIDO FINALIZADO COM SUCESSO!", "Green");
                        mensagem.ShowDialog();
                        LimparCampos();
                    }
                    else if (itensFlowRack.codEstacao == 0 && itensFlowRack.codEstacaoAtual == 0 && itensFlowRack.qtdConferidaProduto == 0)
                    {
                        mensagem.Texto("PEDIDO NÃO ENCONTRADO!", "Blue");
                        mensagem.ShowDialog();
                        LimparCampos();
                    }
                    else if (codEstacao != itensFlowRack.codEstacao && itensFlowRack.codEstacaoAtual != itensFlowRack.codEstacao)
                    {
                        mensagem.Texto("VOLUME ATUAL " + itensFlowRack.numeroVolume + " ", "Blue");
                        mensagem.ShowDialog();
                        LimparCampos();
                    }
                    else if (codEstacao != itensFlowRack.codEstacao)
                    {

                        mensagem.Texto("O VOLUME DEVERÁ IR PARA A ESTAÇÃO " + itensFlowRack.codEstacao + "!", "Blue");
                        mensagem.ShowDialog();
                        LimparCampos();
                    }

                    else
                    {
                        txtPesqVolume.Enabled = false; //Desabilita o campo de pesquisa de volume
                        txtBarra.Focus(); //Foca no campo código de barra do produto

                        //A coleção recebe o resultado da consulta
                        itensFlowRackCollection = itensFlowRackNegocios.PesqItemPedido(txtPesqVolume.Text, codEstacao);
                        //Limpa o grid
                        gridItens.Rows.Clear();

                        if (itensFlowRackCollection.Count > 0)
                        {
                            //Analisa a sugetão de caixa
                            AnalisarPedidoSugestao();

                            for (posicao = 0; itensFlowRackCollection.Count > posicao; posicao++)
                            {

                                if (itensFlowRackCollection[posicao].dataConferencia == null)
                                {
                                    //Seta os dados nos campos
                                    lblSKU.Text = itensFlowRackCollection[posicao].codProduto.ToString() + " - " + itensFlowRackCollection[posicao].descProduto.ToString();
                                    lblBloco.Text = String.Format("{0:00}", itensFlowRackCollection[posicao].bloco);
                                    lblApartamento.Text = String.Format("{0:000}", itensFlowRackCollection[posicao].apartamento);
                                    lblQuantidade.Text = String.Format("{0:00}", itensFlowRackCollection[posicao].qtdProduto - itensFlowRackCollection[posicao].qtdCorteProduto);
                                    lblnumeroVolume.Text = String.Format("{0:00}", itensFlowRackCollection[posicao].numeroVolume);
                                    lblValidade.Text = itensFlowRackCollection[posicao].validadeProduto.ToString("dd/MM/yyyy");
                                    lblPeso.Text = String.Format("{0:0.00}", itensFlowRackCollection[posicao].pesoProduto);
                                    lblCategoria.Text = itensFlowRackCollection[posicao].descCategoria.ToString();


                                    //Atualiza o início de conferência e a data do volume um criado
                                    itensFlowRackNegocios.AtualizaInicioConferencia(txtPesqVolume.Text, itensFlowRackCollection[posicao].codPedido);

                                    if (Convert.ToDateTime(lblValidade.Text) <= DateTime.Now.AddDays(30))
                                    {
                                        mensagem.Texto("ATENÇÃO: PRODUTO COM 1 MÊS PARA ATINGIR O VENCIMENTO!", "Red");
                                        mensagem.ShowDialog();
                                    }

                                    //Para o laço
                                    break;
                                }
                                else
                                {
                                    //Adiciona ao grid
                                    gridItens.Rows.Add(gridItens.Rows.Count + 1, itensFlowRackCollection[posicao].endereco, itensFlowRackCollection[posicao].codProduto + " - " + itensFlowRackCollection[posicao].descProduto,
                                        itensFlowRackCollection[posicao].qtdConferidaProduto);
                                    //Seleciona a linha do grid
                                    gridItens.CurrentCell = gridItens.Rows[gridItens.Rows.Count - 1].Cells[1];

                                    //Verifica se a conferência está finalizada
                                    //VerificarConferencia();
                                }
                            }

                            //Qtd de itens da estação
                            lblQtd.Text = itensFlowRackCollection.Count.ToString();

                            //Pesquisa as restrições do cliente
                            Cliente cliente = itensFlowRackNegocios.PesqRestricoesCliente(itensFlowRackCollection[0].codPedido);

                            if (cliente.caixaFechadaCliente == true)
                            {
                                lblRestricaoCliente.Text = "ATENÇÃO \nCLIENTE NÃO ACEITA PRODUTO FRACIONADO! \nPOR FAVOR, EFETUAR O CORTE!";
                            }
                            else
                            {
                                if (cliente.validadeCliente == true)
                                {
                                    //Instânica a data atual
                                    DateTime data = DateTime.Now;
                                    //Adiciona os dias exigidos
                                    lblRestricaoCliente.Text = "ATENÇÃO \nCLIENTE EXIGE RECEBER O PRODUTO COM VALIDADE " +
                                        "\nIGUAL OU SUPERIOR A " + data.AddDays(cliente.diasValidadeCliente).ToShortDateString();
                                }
                            }

                        }
                        else
                        {
                            mensagem.Texto("NÃO EXISTE ITEM DE FLOWRACK PARA ESSE PEDIDO!", "Red");
                            mensagem.ShowDialog();

                            txtPesqVolume.Enabled = true; //Habilita o campo de pesquisa de volume                                                          
                            txtPesqVolume.SelectAll();//Seleciona o texto do campo
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o codigo de Barra
        private void PesCodigoBarra()
        {
            try
            {
                if (txtBarra.Text.Equals(""))
                {
                    mensagem.Texto("POR FAVOR, DIGITE OU BIPE O CÓDIGO DE BARRA DO PRODUTO!", "Blue");
                    mensagem.ShowDialog();
                }
                else
                {
                    //Instância a camada de negocios
                    ConferenciaFlowRackNegocios itensFlowRackNegocios = new ConferenciaFlowRackNegocios();
                    //Instância a camada de objêto
                    Barra codigoBarra = new Barra();

                    //A coleção recebe o resultado da consulta
                    codigoBarra = itensFlowRackNegocios.PesqCodigoBarra(txtBarra.Text, cmbEmpresa.Text);

                    //Verifica se o item existe na lista
                    if (itensFlowRackCollection.Exists(n => n.idProduto == codigoBarra.idProduto) == true)
                    {
                        //Limpa o campo de código de barra
                        txtBarra.Clear();

                        if (codigoBarra.multiplicador == 1)
                        {
                            //Localizando o produto
                            var itemConferencia = itensFlowRackCollection.FindAll(delegate (ItensFlowRack n) { return n.idProduto == codigoBarra.idProduto; });

                            if ((itemConferencia[0].qtdConferidaProduto + itemConferencia[0].qtdCorteProduto) == itemConferencia[0].qtdProduto)
                            {
                                mensagem.Texto("PRODUTO JÁ CONFERIDO!", "Green");
                                mensagem.ShowDialog();
                            }
                            //verifica se o item é o mesmo que está na tela
                            else if (!(itemConferencia[0].codProduto + " - " + itemConferencia[0].descProduto).Equals(lblSKU.Text))
                            {
                                txtBarra.Clear();
                                mensagem.Texto("POR FAVOR, CONFIRA O ITEM QUE ESTÁ NA TELA!", "Red");
                                mensagem.ShowDialog();
                            }
                            else
                            {
                                //Altera na coleção o valor de conferido
                                itensFlowRackCollection[posicao].qtdConferidaProduto = Convert.ToInt32(lblQuantidade.Text);
                                //Altera na coleção o valor de corte
                                itensFlowRackCollection[posicao].qtdCorteProduto = itensFlowRackCollection[posicao].qtdProduto - Convert.ToInt32(lblQuantidade.Text);

                                //Adiciona ao grid
                                gridItens.Rows.Add(gridItens.Rows.Count + 1, itensFlowRackCollection[posicao].endereco, itensFlowRackCollection[posicao].codProduto + " - " + itensFlowRackCollection[posicao].descProduto,
                                    itensFlowRackCollection[posicao].qtdConferidaProduto);
                                //Seleciona a linha do grid
                                gridItens.CurrentCell = gridItens.Rows[gridItens.Rows.Count - 1].Cells[1];

                                //Salva a conferencia
                                SalvarConferencia();
                            }
                        }
                        else if (codigoBarra.multiplicador > 1)
                        {
                            txtBarra.Clear();
                            mensagem.Texto("POR FAVOR, BIPE O CÓDIGO DE BARRA DE UNIDADE!", "Red");
                            mensagem.ShowDialog();
                        }
                    }
                    else
                    {
                        txtBarra.Clear();
                        mensagem.Texto("ESSE PRODUTO NÃO PERTENCE A ESSE PEDIDO", "Red");
                        mensagem.ShowDialog();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Gera um novo volume de conferência e imprime a etiqueta
        private void NovoVolume()
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaFlowRackNegocios itensFlowRackNegocios = new ConferenciaFlowRackNegocios();
                //Instância uma coleção de objêto 
                Pedido pedido = new Pedido();

                //Verifica se existe item para a estação
                if (itensFlowRackCollection.Count > 0)
                {
                    //Pesquisa o volume atual do pedido + informações da etiqueta
                    pedido = itensFlowRackNegocios.PesqNovoVolume(txtPesqVolume.Text);

                    //Verifica se o volume está vazio
                    if (Convert.ToInt32(pedido.numeroVolume) == 0)
                    {
                        mensagem.Texto("VOLUME ATUAL SE ENCONTRA VAZIO, UM NOVO VOLUME NÃO PODE SER GERADO!", "Blue");
                    }
                    else
                    {

                        //Recebe o código de barra do volume atual
                        string volumeAtual = txtPesqVolume.Text;
                        //Soma o número do próximo volume no label para exibição                       
                        lblnumeroVolume.Text = String.Format("{0:00}", (Convert.ToInt32(lblnumeroVolume.Text) + 1));
                        //Atualiza o numero do volume
                        pedido.numeroVolume = lblnumeroVolume.Text;
                        //Atualiza o código de barra do volume
                        pedido.barraVolume = pedido.codPedido + lblnumeroVolume.Text;
                        //Atualiza o campo do código de barra
                        txtPesqVolume.Text = pedido.barraVolume;

                        //Gera o novo volume
                        itensFlowRackNegocios.GerarNovoVolume(volumeAtual, txtPesqVolume.Text, pedido.numeroVolume);

                        //Imprime a etiqueta
                        ImprimirEtiqueta(pedido);

                        mensagem.Texto("VOLUME DE NÚMERO: " + lblnumeroVolume.Text + " GERADO COM SUCESSO!", "Blue");
                    }

                    mensagem.ShowDialog(); //Exibe a mensagem
                    txtBarra.Focus(); //Foca no campo de barra
                }
                else
                {
                    mensagem.Texto("NÃO EXISTE CONFERÊNCIA INICIALIZADA!", "Blue");
                    mensagem.ShowDialog();

                    txtPesqVolume.Focus(); //Foca no campo
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Salva a sugestão nos itens analizados
        private void SalvarSugestaoItem()
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaFlowRackNegocios itensFlowRackNegocios = new ConferenciaFlowRackNegocios();
                //Instância a camada de objêto
                ItensFlowRackCollection analisarItens = new ItensFlowRackCollection();

                //A coleção recebe o resultado da consulta
                analisarItens = itensFlowRackNegocios.AnalisaItensSugetao(txtPesqVolume.Text, codEstacao);

                if (analisarItens.Count > 0)
                {
                    //Variável - Categoria do primeiro item
                    string primeiraCategoria = analisarItens[0].descCategoria;


                    //Percorre todos os itens analisando as categorias
                    for (int i = 0; analisarItens.Count > i; i++)
                    {
                        //1º Verifica se o pedido possui a mesma categoria
                        if (primeiraCategoria.Equals(analisarItens[i].descCategoria))
                        {
                            //Salva a caixa nos itens
                            itensFlowRackNegocios.SalvarSugestaoItem(analisarItens[i].idProdutoVolume, codCaixa, cmbEmpresa.Text);
                        }
                        //2º Verifica se a categoria diferente são restrita
                        else
                        {
                            string restricao = itensFlowRackNegocios.PesqRestricaoCategoria(primeiraCategoria, analisarItens[i].descCategoria);

                            //Se não são restrita, continua a executar a soma da cubagem
                            if (restricao.Equals('N'))
                            {
                                //Salva a caixa nos itens
                                itensFlowRackNegocios.SalvarSugestaoItem(analisarItens[i].idProdutoVolume, codCaixa, cmbEmpresa.Text);
                            }
                            else restricao.Equals('S');
                            {
                                //Para 
                                break;
                            }
                        }
                    }
                }

                txtBarra.Focus(); //Foca no campo
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Salva a conferencia do item
        private void SalvarConferencia()
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaFlowRackNegocios itensFlowRackNegocios = new ConferenciaFlowRackNegocios();

                //Salva a conferencia
                itensFlowRackNegocios.SalvarConferencia(itensFlowRackCollection[posicao], codUsuario, cmbEmpresa.Text);

                //Verifica se finaliza a conferencia ou passa adiante
                VerificarConferencia();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Verifica se todos os itens estão conferidos
        private void VerificarConferencia()
        {
            try
            {
                //Verifica se a posição está em um item já conferido
                for (int i = 0; itensFlowRackCollection.Count > i; i++)
                {
                    //Verifica se todos os itens etão conferidos
                    if (itensFlowRackCollection[i].qtdProduto == (itensFlowRackCollection[i].qtdConferidaProduto + itensFlowRackCollection[i].qtdCorteProduto))
                    {
                        if (i == itensFlowRackCollection.Count - 1)
                        {
                            //Instância a camada de negocios
                            ConferenciaFlowRackNegocios itensFlowRackNegocios = new ConferenciaFlowRackNegocios();

                            //Salva a conferencia
                            Estacao proximaEstacao = itensFlowRackNegocios.PesqProximaEstacao(itensFlowRackCollection[i].codPedido, codEstacao);

                            if (!(proximaEstacao.codEstacao == 0))
                            {
                                if (nivelEstacao != proximaEstacao.nivel)
                                {
                                    //Atualiza o nível para true para inpressão quando o vicel da estação for diferente
                                    itensFlowRackNegocios.AtualizaNivel(txtPesqVolume.Text, proximaEstacao.codEstacao);


                                    //Gera o novo volume
                                    itensFlowRackNegocios.GerarNovoVolume(txtPesqVolume.Text, Convert.ToString(Convert.ToInt64(txtPesqVolume.Text) + 1), String.Format("{0:00}", (Convert.ToInt32(lblnumeroVolume.Text) + 1)));

                                }

                                mensagem.Texto("CONFERENCIA FINALIZADA NA ESTAÇÃO! \nPRÓXIMA ESTAÇÃO: " + proximaEstacao.codEstacao, "Red");
                                //Limpa todos os campos
                                LimparCampos();
                            }
                            else
                            {
                                int codPedido = itensFlowRackCollection[i].codPedido;
                                //Atualiza o fim de conferência
                                itensFlowRackNegocios.AtualizaFimConferencia(codPedido);

                                mensagem.Texto("PEDIDO FINALIZADO COM SUCESSO! ", "Green");
                                //Limpa todos os campos
                                LimparCampos();

                            }

                            //Exibe a mensagem
                            mensagem.ShowDialog();
                            break;
                        }
                    }
                    else
                    {
                        //Passa para o próximo item
                        PularItem();
                        //Para o de verificar
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pulao item
        private void PularItem()
        {
            try
            {
                if (itensFlowRackCollection.Count > 0)
                {
                    //Se a posicao for menor que a quantidade de itens
                    if (posicao < itensFlowRackCollection.Count)
                    {
                        //Adiciona mais um na posição
                        posicao++;
                    }

                    //Se a posição for igual a quantidade de itens
                    if (posicao == itensFlowRackCollection.Count)
                    {
                        //Zera para exibir todos os itens novamente
                        posicao = 0;
                    }

                    //Verifica se a posição está em um item já conferido
                    for (int i = 0; itensFlowRackCollection.Count > i; i++)
                    {
                        //Verifica o item que está conferido
                        if (itensFlowRackCollection[posicao].qtdProduto == (itensFlowRackCollection[posicao].qtdConferidaProduto + itensFlowRackCollection[posicao].qtdCorteProduto))
                        {
                            //Pula uma posição caso já estejaconferido
                            if (posicao < itensFlowRackCollection.Count - 1)
                            {
                                //Adiciona mais um na posição
                                posicao++;
                            }
                        }
                    }

                    //Exibe as informações para conferência
                    lblSKU.Text = itensFlowRackCollection[posicao].codProduto.ToString() + " - " + itensFlowRackCollection[posicao].descProduto.ToString();
                    lblBloco.Text = String.Format("{0:00}", itensFlowRackCollection[posicao].bloco);
                    lblApartamento.Text = String.Format("{0:000}", itensFlowRackCollection[posicao].apartamento);
                    lblQuantidade.Text = String.Format("{0:00}", itensFlowRackCollection[posicao].qtdProduto - itensFlowRackCollection[posicao].qtdCorteProduto);
                    lblValidade.Text = itensFlowRackCollection[posicao].validadeProduto.ToString("dd/MM/yyyy");
                    lblPeso.Text = String.Format("{0:0.00}", itensFlowRackCollection[posicao].pesoProduto);
                    lblCategoria.Text = itensFlowRackCollection[posicao].descCategoria.ToString();

                    if (Convert.ToDateTime(lblValidade.Text) <= DateTime.Now.AddDays(30))
                    {
                        mensagem.Texto("ATENÇÃO: PRODUTO COM 1 MÊS PARA ATINGIR O VENCIMENTO!", "Red");
                        mensagem.ShowDialog();
                    }

                    txtBarra.Focus(); //Foca no campo
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
                if (itensFlowRackCollection.Count > 0)
                {
                    //Instância a camada de negocios
                    ConferenciaFlowRackNegocios itensFlowRackNegocios = new ConferenciaFlowRackNegocios();

                    FrmCorteFlow frame = new FrmCorteFlow();
                    frame.estoque = itensFlowRackNegocios.PesqEstoque(Convert.ToInt32(itensFlowRackCollection[posicao].idProduto));
                    //Passa os valores cadastrados
                    frame.sku = lblSKU.Text;
                    frame.quantidade = Convert.ToInt32(lblQuantidade.Text);


                    //Recebe as informações
                    if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //Recebe os valores novos
                        lblQuantidade.Text = string.Format(@"{0:00}", frame.resultado);

                        if (lblQuantidade.Text.Equals("00"))
                        {
                            //Altera na coleção o valor de conferido
                            itensFlowRackCollection[posicao].qtdConferidaProduto = Convert.ToInt32(lblQuantidade.Text);
                            //Altera na coleção o valor de corte
                            itensFlowRackCollection[posicao].qtdCorteProduto = itensFlowRackCollection[posicao].qtdProduto - Convert.ToInt32(lblQuantidade.Text);

                            SalvarConferencia();
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


        //Gera o abastecimento
        private void GerarAbastecimento(object sender, System.EventArgs e)
        {
            try
            {
                //Responsável analisar os itens para o abastecimento
                Thread analisa = new Thread(AnalisarAbastecimentoFlowRack);
                analisa.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Analisar os itens da estação para o abastecimento
        private void AnalisarAbastecimentoFlowRack()
        {
            try
            {
                //Array responsável pelos códigos das estações
                int[] codigoEstacao = new int[1]; //Define o tamanho do array
                                                  //Preenche o array com o código da estação
                codigoEstacao[0] = codEstacao;

                //Instância a camada de negócios
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();

                //Instância a camada de objêto - coleção 
                ItensAbastecimentoCollection itensCollection = new ItensAbastecimentoCollection();
                //Instância a camada de objêto - coleção 
                ItensAbastecimentoCollection itensAbastecimentoCollection = new ItensAbastecimentoCollection();
                //Instância a camada de objêto - coleção 
                ItensAbastecimentoCollection itensAbastecimentoPulmaoCollection = new ItensAbastecimentoCollection();
                //Instância a camada de objêto - coleção 
                ItensAbastecimentoCollection itensObservacaoCollection = new ItensAbastecimentoCollection();

                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    //Pesquisa o id dos Produtos que precisam de abastecimento
                    itensCollection = abastecimentoNegocios.PesqItens(cmbEmpresa.Text,"FLOWRACK", "", "", "", "", "", codigoEstacao); //A coleção recebe o resultado da consulta 
                });

                for (int i = 0; itensCollection.Count > i; i++)
                {
                    //Separa os itens sem observações para abastecer
                    if (itensCollection[i].observacao != null)
                    {

                    }
                    else
                    {
                        //Controla a quantidade de abastecimento
                        int? qtdEncontrada = 0;

                        //Pesquisa o abastecimento no picking de grandeza
                        for (int ii = 0; itensCollection[i].qtdAbastecer > qtdEncontrada; ii++)
                        {
                            //Instância a camada de objêto - coleção
                            ItensAbastecimento itensPicking = new ItensAbastecimento();

                            //Pesquisa o abastecimento no picking de caixa, passando o id do produto
                            itensPicking = abastecimentoNegocios.PesquisaAbstecimentoPicking(itensCollection[i].idProduto, cmbEmpresa.Text);

                            //Veririca se o produto possui picking e estoque de caixa
                            if (itensPicking.idProduto == 0 || itensPicking.qtdPulmao <= 0)
                            {
                                //Altera a menssagem 
                                itensCollection[i].observacao = "SEM PICKING OU SEM ESTOQUE";
                                //Adiciona o produto a coleção com problemas ou observações
                                itensObservacaoCollection.Add(itensCollection[i]);
                                //Pula para o próximo produto
                                break;
                            }
                            else
                            {
                                //Complementa as informações do pulmão para adicionar no grid
                                itensPicking.codEstacao = itensCollection[i].codEstacao;
                                itensPicking.descEstacao = itensCollection[i].descEstacao;
                                itensPicking.codApartamentoPicking = itensCollection[i].codApartamentoPicking;
                                itensPicking.enderecoPicking = itensCollection[i].enderecoPicking;
                                itensPicking.idProduto = itensCollection[i].idProduto;
                                itensPicking.codProduto = itensCollection[i].codProduto;
                                itensPicking.descProduto = itensCollection[i].descProduto;
                                itensPicking.qtdCaixaProduto = itensCollection[i].qtdCaixaProduto;
                                itensPicking.capacidadePicking = itensCollection[i].capacidadePicking;
                                itensPicking.abastecimentoPicking = itensCollection[i].abastecimentoPicking;
                                itensPicking.qtdAbastecer = itensCollection[i].qtdAbastecer;
                                itensPicking.unidadePulmao = itensCollection[i].unidadePulmao;
                                itensPicking.qtdPicking = itensCollection[i].qtdPicking;
                                itensPicking.unidadePicking = itensCollection[i].unidadePicking;

                                qtdEncontrada = itensPicking.qtdPulmao; //Recebe a quantidade encontrada

                                if (Convert.ToInt32(itensCollection[i].qtdAbastecer) <= itensPicking.qtdPulmao)
                                {
                                    //Se sim recebe a Quantidade necessária
                                    itensPicking.qtdPulmao = Convert.ToInt32(itensCollection[i].qtdAbastecer);

                                    //Passa a quantidade necessária
                                    qtdEncontrada = Convert.ToInt32(itensCollection[i].qtdAbastecer);
                                }

                                //Adiciona o objêto a coleção
                                itensAbastecimentoCollection.Add(itensPicking);

                                //Diminui a quantidade de abastecimento
                                itensCollection[i].qtdAbastecer = Convert.ToInt32(itensCollection[i].qtdAbastecer - itensPicking.qtdPulmao);

                                //Pula para o próximo produto
                                break;

                            }
                        }

                        //Verifica o abastecimento no pulmão
                        for (int iii = 0; itensCollection[i].qtdAbastecer >= qtdEncontrada; iii++)
                        {
                            //Instância a camada de objêto - coleção
                            ItensAbastecimento itensPulmao = new ItensAbastecimento();

                            //Pesquisa o abastecimento no pulmão, passando o id do produto
                            itensPulmao = abastecimentoNegocios.PesqAbastecimentoPulmao(itensCollection[i].idProduto, iii, cmbEmpresa.Text); //iii - Quantidade de linha a pular no select

                            //Complementa as informações do pulmão para adicionar no grid - Precisa ficar aqui, se não sobrescre a observação
                            itensPulmao.codEstacao = itensCollection[i].codEstacao;
                            itensPulmao.descEstacao = itensCollection[i].descEstacao;
                            itensPulmao.codApartamentoPicking = itensCollection[i].codApartamentoPicking;
                            itensPulmao.enderecoPicking = itensCollection[i].enderecoPicking;
                            //itensPulmao.idProduto = itensCollection[i].idProduto;
                            itensPulmao.codProduto = itensCollection[i].codProduto;
                            itensPulmao.descProduto = itensCollection[i].descProduto;
                            itensPulmao.qtdCaixaProduto = itensCollection[i].qtdCaixaProduto;
                            itensPulmao.capacidadePicking = itensCollection[i].capacidadePicking;
                            itensPulmao.abastecimentoPicking = itensCollection[i].abastecimentoPicking;
                            itensPulmao.qtdAbastecer = itensCollection[i].qtdAbastecer;
                            itensPulmao.unidadePulmao = itensCollection[i].unidadePulmao;
                            itensPulmao.qtdPicking = itensCollection[i].qtdPicking;
                            itensPulmao.unidadePicking = itensCollection[i].unidadePicking;

                            if (itensPulmao.idProduto == 0)
                            {
                                //Altera a menssagem 
                                itensPulmao.observacao = "NÃO EXISTE ESTOQUE NO PULMÃO";
                                //Adiciona o produto a coleção com problemas ou observações
                                itensObservacaoCollection.Add(itensPulmao);
                                //Pula para o próximo produto
                                break;
                            }
                            else
                            {
                                //Verifica se o pulmão tem mais do que precisa abastecer
                                if (Convert.ToInt32(itensCollection[i].qtdAbastecer) <= itensPulmao.qtdPulmao)
                                {
                                    //Se sim recebe a Quantidade necessária
                                    itensPulmao.qtdPulmao = Convert.ToInt32(itensCollection[i].qtdAbastecer);

                                }

                                //Adiciona o objêto a coleção
                                itensAbastecimentoPulmaoCollection.Add(itensPulmao);

                                //Verifica se há necessidade de continuar a pesquisa
                                if (itensCollection[i].qtdAbastecer <= itensPulmao.qtdPulmao)
                                {
                                    //Pula para o próximo produto
                                    break;
                                }

                                //Diminui a quantidade de abastecimento
                                itensCollection[i].qtdAbastecer = Convert.ToInt32(itensCollection[i].qtdAbastecer - itensPulmao.qtdPulmao);

                            }
                        }
                    }
                }

                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {

                    for (int i = 0; itensAbastecimentoCollection.Count > i; i++)
                    {
                        //Gera o abastecimento de picking para picking
                        GerarItensAbastecimento(
                    Convert.ToInt32(itensAbastecimentoCollection[i].codEstacao),
                    Convert.ToInt32(itensAbastecimentoCollection[i].codApartamentoPicking),
                    Convert.ToInt32(itensAbastecimentoCollection[i].idProduto),
                    Convert.ToInt32(itensAbastecimentoCollection[i].qtdAbastecer * itensAbastecimentoCollection[i].qtdCaixaProduto),
                    Convert.ToInt32(itensAbastecimentoCollection[i].qtdPicking),
                    Convert.ToInt32(itensAbastecimentoCollection[i].codApartamentoPulmao),
                    Convert.ToInt32(itensAbastecimentoCollection[i].qtdPulmao * itensAbastecimentoCollection[i].qtdCaixaProduto),
                    Convert.ToString(itensAbastecimentoCollection[i].vencimentoPulmao),
                    Convert.ToString(itensAbastecimentoCollection[i].lotePulmao),
                    "ANÁLISE DE PICKING");
                    }

                    for (int i = 0; itensAbastecimentoPulmaoCollection.Count > i; i++)
                    {
                        //Gera o abastecimento de pulmão para picking
                        GerarItensAbastecimento(
                    Convert.ToInt32(itensAbastecimentoPulmaoCollection[i].codEstacao),
                    Convert.ToInt32(itensAbastecimentoPulmaoCollection[i].codApartamentoPicking),
                    Convert.ToInt32(itensAbastecimentoPulmaoCollection[i].idProduto),
                    Convert.ToInt32(itensAbastecimentoPulmaoCollection[i].qtdAbastecer * itensAbastecimentoPulmaoCollection[i].qtdCaixaProduto),
                    Convert.ToInt32(itensAbastecimentoPulmaoCollection[i].qtdPicking),
                    Convert.ToInt32(itensAbastecimentoPulmaoCollection[i].codApartamentoPulmao),
                    Convert.ToInt32(itensAbastecimentoPulmaoCollection[i].qtdPulmao * itensAbastecimentoPulmaoCollection[i].qtdCaixaProduto),
                    Convert.ToString(itensAbastecimentoPulmaoCollection[i].vencimentoPulmao),
                    Convert.ToString(itensAbastecimentoPulmaoCollection[i].lotePulmao),
                    "ANÁLISE DO PULMÃO");
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Gerar itens
        private void GerarItensAbastecimento(int codEstacao, int codPicking, int IdProduto, int qtdAbastecer, int estoquePicking, int codPulmao, int estoquePulmao, string vencimento, string lote, string tipoAnalise)
        {
            try
            {
                //Instância a camada de negocios
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();

                abastecimentoNegocios.GerarItens(null, //Código do abastecimento
                    codEstacao, //Código do estação
                    codPicking, //Código do picking
                    IdProduto, //ID do produto
                    qtdAbastecer, //Quantidade a abastecer x qtd Caixa
                    estoquePicking, //Estoque do produto
                    codPulmao, //Código do pulmão
                    estoquePulmao, //Estoque do pulmão x qtd caixa
                    vencimento, //Vencimento do pulmão
                    lote, //Lote do pulmão
                    tipoAnalise); //Tipo de análise
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Imprimi a etiqueta
        private void ImprimirEtiqueta(Pedido pedido)
        {
            try
            {
                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    //Pega o caminho da etiqueta prn
                    string etiqueta = AppDomain.CurrentDomain.BaseDirectory + "CLIENTE_FLOW_50X80.prn";

                    //Caminho do novo arquivo atualizado
                    string NovaEtiqueta = AppDomain.CurrentDomain.BaseDirectory + pedido.barraVolume;

                    // Abre o arquivo para escrita
                    StreamReader streamReader;
                    streamReader = File.OpenText(etiqueta);

                    string contents = streamReader.ReadToEnd();

                    streamReader.Close();

                    string conteudo = contents;

                    StreamWriter streamWriter = File.CreateText(NovaEtiqueta);

                    // Atualizo as variaveis do arquivo
                    // streamWriter.WriteLine("<STX>L");
                    conteudo = conteudo.Replace("ROTA", "ROTA - " + pedido.rotaCliente);
                    conteudo = conteudo.Replace("DATA", "" + DateTime.Now);
                    conteudo = conteudo.Replace("CLIENTE", pedido.nomeCliente);
                    conteudo = conteudo.Replace("ENDERECO", pedido.enderecoCliente + "," + pedido.numeroCliente);
                    conteudo = conteudo.Replace("CIDADE", pedido.bairroCliente + "/" + pedido.cidadeCliente);
                    conteudo = conteudo.Replace("ESTACAO", "ESTACAO " + codEstacao);
                    conteudo = conteudo.Replace("VOLUME", "VOLUME " + pedido.numeroVolume);
                    conteudo = conteudo.Replace("PEDIDO", "" + pedido.codPedido);
                    conteudo = conteudo.Replace("BARRA", pedido.barraVolume);
                    conteudo = conteudo.Replace("EMPRESA", empresa);
                    streamWriter.Write(conteudo);

                    streamWriter.WriteLine("E"); //Fim do modo de formatação e imprime
                    streamWriter.WriteLine("/220"); //Avanço para corte da etiqueta
                    streamWriter.Close();

                    PrintDialog pd = new PrintDialog();

                    pd.PrinterSettings = new PrinterSettings();

                    if (pd.PrinterSettings.IsValid)
                    {
                        ArgoxPPLA.RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, NovaEtiqueta);

                        //Deleta o arquivo
                        File.Delete(NovaEtiqueta);

                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrada nenhuma impressora instalada no seu computador!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro imprimir a etiqueta: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Limpa todos os campos
        private void LimparCampos()
        {
            //Limpa o código do volume
            txtPesqVolume.Text = "";
            //Limpa o código de barra
            txtBarra.Text = "";
            //Limpa o label SKU
            lblSKU.Text = "-";

            //Limpa o label sugestão
            lblRestricaoCliente.Text = "";
            //Limpa o label sugestão
            lblSugestao.Text = "-";
            //Limpa o label utilidade
            lblUtilidade.Text = "";
            //Limpa o label bloco
            lblBloco.Text = "00";
            //Limpa o label apartamento
            lblApartamento.Text = "000";
            //Limpa o label quantidade
            lblQuantidade.Text = "00";
            //Limpa o label volume
            lblnumeroVolume.Text = "-";
            //Limpa o label validade
            lblValidade.Text = "-";
            //Limpa o label peso
            lblPeso.Text = "-";
            //Limpa o label categoria
            lblCategoria.Text = "-";
            //Limpa a coleção de itens
            itensFlowRackCollection.Clear();
            //Limpa a coleção de caixa
            caixaCollection.Clear();
            //Desmarca a sugestão sim
            rbtSim.Checked = false;
            //Desmarca a sugestão não
            rbtNao.Checked = false;
            //Habilita o campo código do volume
            txtPesqVolume.Enabled = true;
            txtPesqVolume.Focus(); //Foca no campo código do volume
        }


    }
}
