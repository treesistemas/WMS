using CrystalDecisions.ReportAppServer.DataDefModel;
using DocumentFormat.OpenXml.Drawing.Charts;
using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wms.Relatorio;

namespace Wms
{
    public partial class FrmProcessamentoFlow : Form
    {
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmProcessamentoFlow()
        {
            InitializeComponent();
        }

        private void FrmProcessamento_Load(object sender, EventArgs e)
        {
            if (empresaCollection!= null)
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

        private void FrmProcessamentoFlow_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                //   MessageBox.Show("Esconde", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //   MessageBox.Show("Exibe", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //KeyPress
        private void txtManifesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtManifesto.Text == "")
                {
                    //Foca no campo pedido
                    btnPesquisar.Focus();
                }
                else
                {
                    //Pesquisa
                    PesqPedido();
                }
            }
        }

        private void txtPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtPedido.Text == "")
                {
                    //Foca no campo manifesto
                    btnPesquisar.Focus();
                }
                else
                {
                    //Pesquisa
                    PesqPedido();
                }
            }
        }

        private void dtmInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo data final
                dtmFinal.Focus();
            }
        }

        private void dtmFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botao pesquisar
                btnPesquisar.Focus();
            }
        }


        private void dtmInicialProcessamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botao pesquisar
                dtmFinalProcessamento.Focus();
            }
        }

        private void dtmFinalProcessamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botao pesquisar
                btnPesquisar.Focus();
            }
        }

        //CellContent
        private void gridRota_CellContentClick(object sender, DataGridViewCellEventArgs e)//Controla o chekbox no gridview
        {
            //Verifica se e somente se a celula checkbox (Todos) foi clicada
            if (e.ColumnIndex == gridRota.Columns[0].Index)
            {
                if (e.RowIndex >= 0 && Convert.ToBoolean(gridRota.Rows[e.RowIndex].Cells[0].Value) == false)
                {
                    gridRota.Rows[e.RowIndex].Cells[0].Value = "true";
                }
                else if (e.RowIndex >= 0 && Convert.ToBoolean(gridRota.Rows[e.RowIndex].Cells[0].Value) == true)
                {
                    gridRota.Rows[e.RowIndex].Cells[0].Value = "false";
                }
            }
        }

        //Changed
        private void chkDataPedido_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDataPedido.Checked == true)
            {
                dtmInicial.Enabled = true;
                dtmFinal.Enabled = true;

                dtmInicialProcessamento.Enabled = false;
                dtmFinalProcessamento.Enabled = false;
            }
            else
            {
                dtmInicial.Enabled = false;
                dtmFinal.Enabled = false;

                dtmInicialProcessamento.Enabled = true;
                dtmFinalProcessamento.Enabled = true;
            }
        }

        //Click
        private void chkSelecionar_Click(object sender, EventArgs e) //Seleciona todas as rotas
        {
            if (gridRota.Rows.Count > 0)
            {
                if (chkSelecionar.Checked == true)
                {
                    for (int i = 0; gridRota.Rows.Count > i; i++)
                    {
                        gridRota.Rows[i].Cells[0].Value = true;
                    }
                }
                else
                {
                    for (int i = 0; gridRota.Rows.Count > i; i++)
                    {
                        gridRota.Rows[i].Cells[0].Value = false;
                    }
                }
            }
        }

        private void rbtEmProcessamento_Click(object sender, EventArgs e)
        {
            if (rbtEmProcessamento.Checked == true)
            {
                pnlProcessamento.Visible = true; //Exibe o painel
                rbtAuditoria.Checked = false; //Desmarca Auditoria
                rbtEmConferencia.Checked = false; //Desmarca Em conferencia
                rbtEnderecamento.Checked = false; //Desmarca o Endereçamento
            }
            else
            {
                pnlProcessamento.Visible = false; //Esconde Painel
                rbtAuditoria.Checked = false; //Desmarca Auditoria
                rbtEmConferencia.Checked = false; //Desmarca Em conferencia
                rbtEnderecamento.Checked = false; //Desmarca o Endereçamento

            }

        }

        private void rbtTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtTodos.Checked == true)
            {
                rbtProcessados.Checked = false;
                rbtNaoProcessados.Checked = false;
                rbtEmProcessamento.Checked = false;
                rbtEmConferencia.Checked = false;
                rbtAuditoria.Checked = false;
                rbtEnderecamento.Checked = false;
            }
        }

        private void rbtProcessados_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtProcessados.Checked == true)
            {
                rbtTodos.Checked = false;
                rbtNaoProcessados.Checked = false;
                rbtEmProcessamento.Checked = false;
                rbtEmConferencia.Checked = false;
                rbtAuditoria.Checked = false;
                rbtEnderecamento.Checked = false;
            }
        }

        private void rbtNaoProcessados_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtNaoProcessados.Checked == true)
            {
                rbtTodos.Checked = false;
                rbtProcessados.Checked = false;
                rbtEmProcessamento.Checked = false;
                rbtEmConferencia.Checked = false;
                rbtAuditoria.Checked = false;
                rbtEnderecamento.Checked = false;
            }
        }

        private void rbtEmConferencia_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtEmConferencia.Checked == true)
            {
                rbtTodos.Checked = false;
                rbtProcessados.Checked = false;
                rbtEmProcessamento.Checked = true;
            }
        }

        private void rbtAuditoria_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtAuditoria.Checked == true)
            {
                rbtTodos.Checked = false;
                rbtProcessados.Checked = false;
                rbtEmProcessamento.Checked = true;
            }
        }

        private void rbtEnderecamento_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtEnderecamento.Checked == true)
            {
                rbtTodos.Checked = false;
                rbtProcessados.Checked = false;
                rbtEmProcessamento.Checked = true;
            }
        }

        private void mniProcessamento_Click(object sender, EventArgs e)
        {
            //Envia os pedidos selecionados para processamento
            Thread processameto = new Thread(EnviarPedidosProcessamento);
            processameto.Start();

        }

        private void mniReenviarProcessamento_Click(object sender, EventArgs e)
        {
            //Reenvia o pedido
            ReenviarPedidosProcessamento();
        }


        private void mniEstação_Click(object sender, EventArgs e)
        {
            FrmAcompanhamentoEstacao frame = new FrmAcompanhamentoEstacao();
            frame.ShowDialog();
        }

        private void mniAcompanharPedido_Click(object sender, EventArgs e)
        {
            //Instância as linha da tabela
            DataGridViewRow linha = gridPedido.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;

            FrmAcompanhamentoConfFlow frame = new FrmAcompanhamentoConfFlow();
            //Passa o código do pedido
            frame.codPedido = Convert.ToInt32(gridPedido.Rows[indice].Cells[4].Value);
            //Passa a data de envio
            frame.dataProcessamento = Convert.ToString(gridPedido.Rows[indice].Cells[7].Value);
            //Passa o usuário que iniciou o pedido
            frame.usuario = Convert.ToString(gridPedido.Rows[indice].Cells[17].Value);
            frame.ShowDialog();
        }

        private void mniRendimentoFlowRack_Click(object sender, EventArgs e)
        {
            //Instância a camada de apresentação
            FrmImpressaoRendimentoFlowRack frame = new FrmImpressaoRendimentoFlowRack();
            //Exibe o frame
            frame.Show();
        }

        private void mniSeparacaoFlowRack_Click(object sender, EventArgs e)
        {
            if (gridPedido.Rows.Count > 0)
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridPedido.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                try
                {
                    //Garante que o processo seja executado da thread que foi iniciado
                    Invoke((MethodInvoker)delegate ()
                    {
                        //Instância o relatório
                        FrmSeparacaoFlowRack frame = new FrmSeparacaoFlowRack();
                        frame.GerarRelatorio(Convert.ToInt32(gridPedido.Rows[indice].Cells[4].Value));
                    });

                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPesquisarRota_Click(object sender, EventArgs e)
        {
            PesqRota(); //Pesquisa as rotas
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            PesqPedido();//Pesquisa os pedidos
        }

        //Pesquisa as rotas cadastradas
        private void PesqRota()
        {
            try
            {
                //Instância o negocios
                RotaNegocios rotaNegocios = new RotaNegocios();
                //Instância a coleção
                RotaCollection rotaCollection = new RotaCollection();
                //A coleção recebe o resultado da consulta
                rotaCollection = rotaNegocios.PesqRota("", 0, null);
                //Limpa o grid
                gridRota.Rows.Clear();
                //Grid Recebe o resultado da coleção
                rotaCollection.ForEach(n => gridRota.Rows.Add(false, n.codRota, n.codRota + " - " + n.descRota));

                if (gridRota.RowCount > 0)
                {
                    //Seleciona a primeira linha do grid
                    gridRota.CurrentCell = gridRota.Rows[0].Cells[2];
                    //Foca no grid
                    gridRota.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhuma rota encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o pedido
        private void PesqPedido()
        {
            try
            {
                //Instância o objêto negocios
                ProcessamentoFlowNegocios pedidoProcFlowNegocios = new ProcessamentoFlowNegocios();
                //Instância a coleção
                ProcessamentoFlowCollection pedidoProcFlowCollection = new ProcessamentoFlowCollection();
                //Instância a coleção
                ProcessamentoFlowCollection pedidoCollection = new ProcessamentoFlowCollection();

                int[] codigoRota = new int[gridRota.Rows.Count];
                //Qtd de Rota Selecionada
                int qtdRotaSelecionada = 0;

                //Verifica se existe alguma rota selacionada
                for (int i = 0; gridRota.Rows.Count > i; i++)
                {

                    if (Convert.ToBoolean(gridRota.Rows[i].Cells[0].Value) == true)
                    {
                        //Seta no array os códigos das rotas
                        codigoRota[i] = Convert.ToInt32(gridRota.Rows[i].Cells[1].Value);
                        qtdRotaSelecionada++;

                    }
                }

                //Pesquisa pela data do pedido
                if (chkDataPedido.Checked == true)
                {
                    if (qtdRotaSelecionada > 0)
                    {
                        for (int i = 0; gridRota.Rows.Count > i; i++)
                        {
                            if (Convert.ToBoolean(gridRota.Rows[i].Cells[0].Value) == true)
                            {
                                //A coleção recebe o resultado da consulta
                                pedidoCollection = pedidoProcFlowNegocios.PesqDataPedido(txtManifesto.Text, txtPedido.Text, Convert.ToInt32(gridRota.Rows[i].Cells[1].Value),
                                                   dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(),
                                                   rbtNaoProcessados.Checked, rbtProcessados.Checked, rbtEmProcessamento.Checked,
                                                   rbtAuditoria.Checked, rbtEmConferencia.Checked, rbtEnderecamento.Checked, cmbEmpresa.Text);

                                //Concatena o objêto encontrado
                                pedidoProcFlowCollection.AddRange(pedidoCollection);
                            }
                        }
                    }
                    else
                    {
                        //A coleção recebe o resultado da consulta
                        pedidoCollection = pedidoProcFlowNegocios.PesqDataPedido(txtManifesto.Text, txtPedido.Text, 0,
                                               dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(),
                                               rbtNaoProcessados.Checked, rbtProcessados.Checked, rbtEmProcessamento.Checked,
                                               rbtAuditoria.Checked, rbtEmConferencia.Checked, rbtEnderecamento.Checked, cmbEmpresa.Text);

                        //Concatena o objêto encontrado
                        pedidoProcFlowCollection.AddRange(pedidoCollection);
                    }
                }
                else //Pesquisa pelo envio de flowrack
                {
                    //A coleção recebe o resultado da consulta
                    pedidoProcFlowCollection = pedidoProcFlowNegocios.PesqDataProcessamentoPedido(txtManifesto.Text, txtPedido.Text, codigoRota,
                                               dtmInicialProcessamento.Value.ToShortDateString(), dtmFinalProcessamento.Value.ToShortDateString(),
                                               rbtNaoProcessados.Checked, rbtProcessados.Checked, rbtEmProcessamento.Checked,
                                               rbtAuditoria.Checked, rbtEmConferencia.Checked, rbtEnderecamento.Checked, cmbEmpresa.Text);


                }

                if (qtdRotaSelecionada == 0)
                {
                    //Limpa o grid
                    gridPedido.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    pedidoProcFlowCollection.ForEach(n => gridPedido.Rows.Add(gridPedido.Rows.Count + 1, n.rotaPedido, n.manifestoPedido, n.dataPedido, n.codPedido,
                        n.itensPedido, "Inicial: " + String.Format("{0:00}", n.estInicial) + " - Final: " + String.Format("{0:00}", n.estFinal), n.dataEnvioProcessamento,
                        n.estAtual, n.volumePedido, n.dataInicialProcessamento, n.dataFinalProcessamento, n.tempoProcessamento, n.itensAuditar, n.itensAuditado, n.volumeEnderecado, "", n.usuInicioFlowRack));


                }
                else
                {
                    //Limpa o grid
                    gridPedido.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    pedidoProcFlowCollection.ForEach(n => gridPedido.Rows.Add(gridPedido.Rows.Count + 1, n.rotaPedido, n.manifestoPedido, n.dataPedido, n.codPedido,
                                n.itensPedido, "Inicial: " + String.Format("{0:00}", n.estInicial) + " - Final: " + String.Format("{0:00}", n.estFinal), n.dataEnvioProcessamento,
                                n.estAtual, n.volumePedido, n.dataInicialProcessamento, n.dataFinalProcessamento, n.tempoProcessamento, n.itensAuditar, n.itensAuditado, n.volumeEnderecado, "", n.usuInicioFlowRack));

                    tabControl1.SelectedTab = tabPage1;
                    //}
                    // }
                }

                if (pedidoProcFlowCollection.Count > 0)
                {
                    //Qtd de pedidos encontrados
                    lblQtd.Text = gridPedido.RowCount.ToString();
                    //Preenche o Dashboard
                    PreencherInformacao();
                }
                else
                {
                    MessageBox.Show("Nenhum Pedido encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       //Preenche o Dashboard e algumas informações adicionais
        private void PreencherInformacao()
        {
            //Limpa o título
            chartPedido.Titles.Clear();
            //Limpa os dados
            chartPedido.Series["Pedido"].Points.Clear();

            //Títuo do Chart
            chartPedido.Titles.Add("Dashboard Flow Rack").Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter, ((byte)(1)), true);

            chartPedido.ChartAreas[0].Area3DStyle.Enable3D = true;  // set the chartarea to 3D!
            chartPedido.Series["Pedido"].IsValueShownAsLabel = true;
            //Total de pedidos
            chartPedido.Series["Pedido"].Points.AddXY("Pedidos", gridPedido.Rows.Count);
            //Variáveis de controle
            string dataEnvio;
            string conferenciaInicial;
            string conferenciaFinal;
            int itensAuditar;
            int itensAuditado;
            int endereco;

            //Variáveis que controla a qtd no gráfico
            int enviado = 0;
            int naoEnviado = 0;
            int conferido = 0;
            int naoConferidos = 0;
            int qtdItens = 0;
            int qtdVolumes = 0;
            int contPedidosVolume = 0;//Pedidos que já geraram volumes



            for (int ii = 0; gridPedido.Rows.Count > ii; ii++)
            {
                dataEnvio = Convert.ToString(gridPedido.Rows[ii].Cells[7].Value); //Passa a data de envio para a variável
                conferenciaInicial = Convert.ToString(gridPedido.Rows[ii].Cells[10].Value); //Passa a data incial de conferencia para a variável
                conferenciaFinal = Convert.ToString(gridPedido.Rows[ii].Cells[11].Value); //Passa a data final de conferêcnia para a variável
                itensAuditar = Convert.ToInt32(gridPedido.Rows[ii].Cells[13].Value); //Passa a quantidade itens para ser auditado para a variável
                itensAuditado = Convert.ToInt32(gridPedido.Rows[ii].Cells[14].Value); //Passa a quantidade itens auditado para a variável
                endereco = Convert.ToInt32(gridPedido.Rows[ii].Cells[15].Value); //Passa a quantidade de volume desindereçado

                //Verifica se  foi enviado para processamento
                if (!dataEnvio.Equals(""))
                {
                    if (!dataEnvio.Equals("") && conferenciaInicial.Equals(""))
                    {
                        //Altera as cores
                        gridPedido.Rows[ii].Cells[16].Style.BackColor = Color.Orange;
                        gridPedido.Rows[ii].Cells[16].Style.ForeColor = Color.White;
                        gridPedido.Rows[ii].Cells[16].Value = "Em processamento";
                    }
                    else if (!conferenciaInicial.Equals("") && conferenciaFinal.Equals(""))
                    {
                        //Altera as cores
                        gridPedido.Rows[ii].Cells[16].Style.BackColor = Color.LimeGreen;
                        gridPedido.Rows[ii].Cells[16].Style.ForeColor = Color.White;
                        gridPedido.Rows[ii].Cells[16].Value = "Em Conferência";
                    }

                    else if (!conferenciaFinal.Equals("") && itensAuditar != itensAuditado)
                    {
                        //Altera as cores
                        gridPedido.Rows[ii].Cells[16].Style.BackColor = Color.Red;
                        gridPedido.Rows[ii].Cells[16].Style.ForeColor = Color.White;
                        gridPedido.Rows[ii].Cells[16].Value = "Em Auditoria";
                    }

                    else if (!conferenciaFinal.Equals("") && itensAuditar == itensAuditado && endereco > 0)
                    {
                        //Altera as cores
                        gridPedido.Rows[ii].Cells[16].Style.BackColor = Color.Yellow;
                        gridPedido.Rows[ii].Cells[16].Style.ForeColor = Color.Gray;
                        gridPedido.Rows[ii].Cells[16].Value = "Para Endereçamento";
                    }

                    else if (!conferenciaFinal.Equals("") && itensAuditar == itensAuditado && endereco == 0)
                    {
                        //Altera as cores
                        gridPedido.Rows[ii].Cells[16].Style.BackColor = Color.Blue;
                        gridPedido.Rows[ii].Cells[16].Style.ForeColor = Color.White;
                        gridPedido.Rows[ii].Cells[16].Value = "Finalizado";
                    }

                    enviado++;
                }
                else
                {

                    gridPedido.Rows[ii].Cells[16].Value = "Não Processado";
                    naoEnviado++;
                }

                //Verifica se o pedido foi finalizado
                if (Convert.ToString(gridPedido.Rows[ii].Cells[16].Value).Equals("Finalizado"))
                {
                    conferido++;
                }
                else
                {
                    naoConferidos++;
                }

                //Soma a qtd de Itens
                if (!Convert.ToString(gridPedido.Rows[ii].Cells[5].Value).Equals(""))
                {
                    qtdItens += Convert.ToInt32(gridPedido.Rows[ii].Cells[5].Value);
                    //Quantidade de itens
                    lblQtdItens.Text = Convert.ToString(qtdItens);
                }

                //Soma a qtd de Itens
                if (!Convert.ToString(gridPedido.Rows[ii].Cells[9].Value).Equals(""))
                {
                    qtdVolumes += Convert.ToInt32(gridPedido.Rows[ii].Cells[9].Value);
                    //Quantidade de volume
                    lblQtdVolumes.Text = Convert.ToString(qtdVolumes);

                    //Pedidos que já geraram volumes
                    contPedidosVolume++;

                    //Exibe o valor volume em negrito
                    gridPedido.Rows[ii].Cells[9].Style.Font = new Font(gridPedido.Font, FontStyle.Bold);

                }
            }

            //Dados do gráfico
            chartPedido.Series["Pedido"].Points.AddXY("Enviado", enviado);
            chartPedido.Series["Pedido"].Points.AddXY("Não Enviados", naoEnviado);
            chartPedido.Series["Pedido"].Points.AddXY("Finalizados", conferido);
            chartPedido.Series["Pedido"].Points.AddXY("Pendentes", naoConferidos);

            //Média de itens
            lblMediaItem.Text = String.Format("{0:00}", qtdItens / gridPedido.Rows.Count);

            if (qtdVolumes > 0 && contPedidosVolume > 0)
            {
                //Média de volume
                lblMediaVolume.Text = String.Format("{0:00}", qtdVolumes / contPedidosVolume);
            }


        }

        //Envia os pedidos selecionados para processamento
        private void EnviarPedidosProcessamento()
        {
            try
            {
                if (gridPedido.Rows.Count == 0)
                {
                    //Mensagem
                    MessageBox.Show("Pesquise os pedidos a serem processados no flow rack!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Garante que o progressbar seja executado da thread que foi iniciado
                    Invoke((MethodInvoker)delegate ()
                    {
                        //Instância a camada de negocios
                        ProcessamentoFlowNegocios pedidoProcFlowNegocios = new ProcessamentoFlowNegocios();
                        int codPedido; //Variável responsável de receber o código do pedido

                        //Exibe o texto a quantidade de pedidos no processo
                        lblMsgProcessamento.Text = gridPedido.SelectedRows.Count + " Pedidos Processados";

                        //Define um valor para o progressbar
                        progressBar.Maximum = (gridPedido.SelectedRows.Count);
                        //Controla o progressbar
                        System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
                        //Inicie o cronômetro.
                        time.Start();

                        foreach (DataGridViewRow row in gridPedido.SelectedRows)
                        {
                            //Verifica se o pedido está com status de não processado
                            if (Convert.ToString(gridPedido.Rows[row.Index].Cells[16].Value).Equals("Não Processado"))
                            {

                                //Passa para a variável o código do pedido
                                codPedido = Convert.ToInt32(gridPedido.Rows[row.Index].Cells[4].Value); //Código do pedido


                                // Passa o código do pedido para a camada de negocios
                                pedidoProcFlowNegocios.ProcessarPedidos(codPedido, cmbEmpresa.Text);

                                //Insere a data de envio de procesamento no grid
                                gridPedido.Rows[row.Index].Cells[7].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                                //Altera as cores e o status da linha do pedido
                                gridPedido.Rows[row.Index].Cells[16].Style.BackColor = Color.Orange;
                                gridPedido.Rows[row.Index].Cells[16].Style.ForeColor = Color.White;
                                gridPedido.Rows[row.Index].Cells[16].Value = "Em processamento";


                                //Incrementar o valor da ProgressBar um valor de uma de cada vez.
                                progressBar.Increment(1);
                            }

                        }
                        MessageBox.Show("Pedidos enviados para processamento com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Limpa a mensagem
                        lblMsgProcessamento.Text = "0 Pedidos Processando";
                        //Pare o cronômetro.
                        time.Stop();
                        //Zera o progressbar
                        progressBar.Value = 0;

                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Envia os pedidos selecionados para processamento
        private void ReenviarPedidosProcessamento()
        {
            try
            {
                if (gridPedido.Rows.Count == 0)
                {
                    //Mensagem
                    MessageBox.Show("Pesquise os pedidos a serem processados no flow rack!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    //Garante que o progressbar seja executado da thread que foi iniciado
                    Invoke((MethodInvoker)delegate ()
                    {
                        //Instância a camada de negocios
                        ProcessamentoFlowNegocios pedidoProcFlowNegocios = new ProcessamentoFlowNegocios();
                        int codPedido; //Variável responsável de receber o código do pedido

                        //Exibe o texto a quantidade de pedidos no processo
                        lblMsgProcessamento.Text = gridPedido.SelectedRows.Count + " Pedidos Processados";

                        //Define um valor para o progressbar
                        progressBar.Maximum = (gridPedido.SelectedRows.Count);
                        //Controla o progressbar
                        System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
                        //Inicie o cronômetro.
                        time.Start();

                        foreach (DataGridViewRow row in gridPedido.SelectedRows)
                        {
                            //Passa para a variável o código do pedido
                            codPedido = Convert.ToInt32(gridPedido.Rows[row.Index].Cells[4].Value); //Código do pedido

                            // Passa o código do pedido para a camada de negocios
                            pedidoProcFlowNegocios.ReiniciarProcessamento(codPedido, cmbEmpresa.Text);
                            // Passa o código do pedido para a camada de negocios
                            pedidoProcFlowNegocios.ProcessarPedidos(codPedido, cmbEmpresa.Text);

                            //Insere a data de envio de procesamento no grid
                            gridPedido.Rows[row.Index].Cells[7].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                            //Limpa a coluna
                            gridPedido.Rows[row.Index].Cells[8].Value = "";
                            gridPedido.Rows[row.Index].Cells[9].Value = "";
                            gridPedido.Rows[row.Index].Cells[10].Value = "";
                            gridPedido.Rows[row.Index].Cells[11].Value = "";


                            //Altera as cores e o status da linha do pedido
                            gridPedido.Rows[row.Index].Cells[16].Style.BackColor = Color.Orange;
                            gridPedido.Rows[row.Index].Cells[16].Style.ForeColor = Color.White;
                            gridPedido.Rows[row.Index].Cells[16].Value = "Em processamento";


                            //Incrementar o valor da ProgressBar um valor de uma de cada vez.
                            progressBar.Increment(1);
                        }

                        MessageBox.Show("Pedidos reenviados para processamento com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Limpa a mensagem
                        lblMsgProcessamento.Text = "0 Pedidos Processando";
                        //Pare o cronômetro.
                        time.Stop();
                        //Zera o progressbar
                        progressBar.Value = 0;

                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
