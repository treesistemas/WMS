using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;
using ObjetoTransferencia.Impressao;
using Utilitarios;
using Wms.Relatorio;

namespace Wms
{
    public partial class FrmMonitorPedido : Form
    {
        public int codUsuario;
        //Perfíl do usuário
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;
        public string impressora;

        public FrmMonitorPedido()
        {
            InitializeComponent();
        }

        private void FrmMonitorPedido_Load(object sender, EventArgs e)
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

        //Changed
        private void txtRota_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigoRota.Text.Equals(""))
            {
                //Limpa o campo
                txtDescRota.Clear();
            }
            else
            {
                //Instância o negocios
                RotaNegocios rotaNegocios = new RotaNegocios();
                //Instância a coleção
                RotaCollection rotaCollection = new RotaCollection();
                //A coleção recebe o resultado da consulta
                rotaCollection = rotaNegocios.PesqRota("", Convert.ToInt32(txtCodigoRota.Text), cmbEmpresa.Text);

                if (rotaCollection.Count > 0)
                {
                    //Seleciona a primeira linha do grid
                    txtDescRota.Text = rotaCollection[0].descRota;
                }
            }
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigoCliente.Text.Equals(""))
            {
                //Limpa o campo
                txtNomeCliente.Clear();
            }
        }

        private void chkConferidos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConferidos.Checked == true)
            {
                chkNaoConferidos.Enabled = false; //Desabilita os não conferidos
            }
            else
            {
                chkNaoConferidos.Enabled = true; //Habilita os não conferidos
            }
        }

        private void chkNaoConferidos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNaoConferidos.Checked == true)
            {
                chkNaoFaturados.Enabled = false; //Desabilita os não faturados
                chkFaturados.Enabled = false; //Desabilita os Faturados
                chkOcorrencia.Enabled = false; //Desabilita os ocorrência
                chkReentrega.Enabled = false; //Desabilita os reentrega
            }
            else
            {
                chkNaoFaturados.Enabled = true; //Habilita os não faturados
                chkFaturados.Enabled = true; //Habilita os Faturados
                chkOcorrencia.Enabled = true; //Habilita os ocorrência
                chkReentrega.Enabled = true; //Habilita os reentrega                
            }
        }

        private void chkFaturados_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFaturados.Checked == true)
            {
                chkNaoConferidos.Enabled = false; //Desabilita os não conferidos
                chkNaoFaturados.Enabled = false; //Desabilita os não faturados
            }
            else
            {
                chkNaoConferidos.Enabled = true; //Habilita os não conferidos
                chkNaoFaturados.Enabled = true; //Habilita os não faturados
            }
        }

        private void chkNaoFaturados_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNaoFaturados.Checked == true)
            {
                chkFaturados.Enabled = false; //Desabilita os Faturados
                chkOcorrencia.Enabled = false; //Desabilita os ocorrência
                chkReentrega.Enabled = false; //Desabilita os reentrega
            }
            else
            {
                chkFaturados.Enabled = true; //Habilita os Faturados
                chkOcorrencia.Enabled = true; //Habilita os ocorrência
                chkReentrega.Enabled = true; //Habilita os reentrega

            }
        }

        private void chkImpresso_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImpresso.Checked == true)
            {
                chkNaoImpresso.Enabled = false; //Desabilita o não impresso
            }
            else
            {
                chkNaoImpresso.Enabled = true; //Habilita o não impresso
            }
        }

        private void chkNaoImpresso_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNaoImpresso.Checked == true)
            {
                chkImpresso.Enabled = false; //Desabilita o impresso
            }
            else
            {
                chkImpresso.Enabled = true; //Habilita o impresso
            }

        }

        private void chkManifestado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManifestado.Checked == true)
            {
                chkNaoManifestado.Enabled = false; //Desabilita o não manifestado
            }
            else
            {
                chkNaoManifestado.Enabled = true; //Habilita o  não manifestado
            }
        }

        private void chkNaoManifestado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNaoManifestado.Checked == true)
            {
                chkManifestado.Enabled = false; //Desabilita o manifestado
            }
            else
            {
                chkManifestado.Enabled = true; //Habilita o  manifestado
            }
        }

        private void chkFlowRack_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFlowRack.Checked == true)
            {
                chkSemFlowRack.Enabled = false; //Desabilita o sem flow rack
            }
            else
            {
                chkSemFlowRack.Enabled = true; //Habilita o sem flow rack
            }
        }

        private void chkSemFlowRack_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSemFlowRack.Checked == true)
            {
                chkFlowRack.Enabled = false; //Desabilita com sem flow rack
            }
            else
            {
                chkFlowRack.Enabled = true; //Habilita com sem flow rack
            }
        }

        //KeyDown
        private void txtRota_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqRota frame = new FrmPesqRota();
                //Passa o nome da empresa

                frame.empresa = cmbEmpresa.Text;
                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe os valores novos
                    txtCodigoRota.Text = frame.codRota;
                    txtDescRota.Text = frame.descRota;
                }
            }
        }

        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqCliente frame = new FrmPesqCliente();

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe os valores novos
                    txtCodigoCliente.Text = Convert.ToString(frame.codCliente);
                    txtNomeCliente.Text = Convert.ToString(frame.nmCliente);
                }
            }
        }

        //KeyPress
        private void txtPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtPedido.Text == "")
                {
                    //Foca no campo manifesto
                    txtManifesto.Focus();
                }
                else
                {
                    //Pesquisa
                    PesqPedido();
                }
            }
        }

        private void txtManifesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtManifesto.Text == "")
                {
                    //Foca no campo rota
                    txtCodigoRota.Focus();
                }
                else
                {
                    //Pesquisa
                    PesqPedido();
                }
            }
        }

        private void txtRota_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtCodigoRota.Text.Equals(""))
                {
                    //Foca no campo cliente
                    txtCodigoCliente.Focus();
                }
                else
                {
                    //Instância a camada de Negocios
                    RotaNegocios rotaNegocios = new RotaNegocios();
                    //Pesquis a rota pelo código
                    RotaCollection rota = rotaNegocios.PesqRota("", Convert.ToInt32(txtCodigoRota.Text), cmbEmpresa.Text);
                    if (rota.Count > 0)
                    {
                        txtDescRota.Text = rota[0].descRota;
                        //Foca no campo cliente
                        txtCodigoCliente.Focus();
                    }
                    else
                    {
                        txtDescRota.Clear();
                    }
                }
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtCodigoCliente.Text.Equals(""))
                {
                    //Foca no campo data inicial
                    dtmInicial.Focus();
                }
                else
                {
                    //Instância o objeto
                    PesqClienteNegocios pesqClienteNegocios = new PesqClienteNegocios();
                    //Instância a coleção
                    PesqClienteCollection pesqClienteCollection = new PesqClienteCollection();
                    //A coleção recebe o resultado da consulta
                    pesqClienteCollection = pesqClienteNegocios.pesqCliente("null", Convert.ToInt32(txtCodigoCliente.Text));

                    if (pesqClienteCollection.Count > 0)
                    {
                        txtNomeCliente.Text = pesqClienteCollection[0].nmCliente;
                        //Foca no campo data inicial
                        dtmInicial.Focus();
                    }
                    else
                    {
                        txtNomeCliente.Clear();
                    }
                }
            }


        }

        private void dtmInicio_KeyPress(object sender, KeyPressEventArgs e)
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

        //KeyUp
        private void gridPedido_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe os dados no label
            DadosCampos();
        }

        //Menu item
        private void mnuImprimirPedido_Click(object sender, EventArgs e)
        {
            //Imprime o pedido
            ImprimirPedido();
        }

        private void mniConsultarPedido_Click(object sender, EventArgs e)
        {
            if (gridPedido.Rows.Count > 0)
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridPedido.CurrentRow;
                //Instância o frame
                FrmConsultaPedido frame = new FrmConsultaPedido();
                //Seta o código do pedido
                frame.codPedido = Convert.ToInt32(gridPedido.Rows[linha.Index].Cells[2].Value);
                //Seta o nome da empresa
                frame.empresa = cmbEmpresa.Text;
                //Exibe o frame
                frame.ShowDialog();
            }
        }

        private void mniConsutarVolumeDeFlowRack_Click(object sender, EventArgs e)
        {
            if (gridPedido.Rows.Count > 0)
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridPedido.CurrentRow;
                //Instância o frame
                FrmConsultaFlowRack frame = new FrmConsultaFlowRack();
                //Passa o código do pedido
                frame.codPedido = Convert.ToString(gridPedido.Rows[linha.Index].Cells[2].Value);
                //Exibe
                frame.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione o pedido!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void menu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gridPedido.Focused && gridPedido.Rows.Count > 0)
            {
                mniConsultarPedido.Visible = true; //Exibe a consuta de pedido se o foco está no grid
            }
        }

        private void mniEnviarSeparacao_Click(object sender, EventArgs e)
        {
            if (gridPedido.SelectedRows.Count > 0)
            {
                //Envia os pedidos
                EnviarPedidoSeparacao();
            }

        }

        private void gridPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Exibe os dados no label
            DadosCampos();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtPedido.TextLength > 0 && txtManifesto.TextLength > 0)
            {
                MessageBox.Show("Selecione apenas um modo de pesquisa!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Pesquisa
                PesqPedido();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o frame
            Close();
        }


        //Pesquisa os pedidos
        private void PesqPedido()
        {
            try
            {
                //Instância objeto
                MonitorPedidoCollection monitorPedidoCollection = new MonitorPedidoCollection();
                //Instância o objeto
                MonitorPedidoNegocios monitorPedidoNegocios = new MonitorPedidoNegocios();
                //Pesquisa
                monitorPedidoCollection = monitorPedidoNegocios.PesqPedido(txtManifesto.Text, txtPedido.Text, txtNotaFiscal.Text, txtCodigoRota.Text,
                                          txtCodigoCliente.Text, dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), cmbTipoPedido.Text,
                                          chkConferidos.Checked, chkNaoConferidos.Checked, chkFaturados.Checked, chkNaoFaturados.Checked, chkImpresso.Checked, chkNaoImpresso.Checked,
                                          chkManifestado.Checked, chkNaoManifestado.Checked, chkBloqueado.Checked, chkOcorrencia.Checked, chkReentrega.Checked, chkAgendamento.Checked,
                                          chkFlowRack.Checked, chkSemFlowRack.Checked, cmbEmpresa.Text);
                //Limpa o grid
                gridPedido.Rows.Clear();
                //Percorre a coleção
                monitorPedidoCollection.ForEach(n => gridPedido.Rows.Add(gridPedido.Rows.Count + 1, n.dataPedido, n.codPedido, n.tipoPedido, n.notaFiscal, n.dataFaturamento, n.manifesto, n.codCliente, n.nmCliente,
                    n.itensPedido, string.Format(@"{0:N}", n.pesoPedido), n.dataImpressao,
                    n.nmConferente, n.inicioConferencia, n.fimConferencia, n.tempoConferencia, n.nmSeparador, n.inicioSeparacao, n.fimSeparacao, n.tempoSeparacao,
                    n.ufCliente, n.cidadeCliente, n.bairroCliente, n.enderecoCliente + ", " + n.numeroCliente, n.rota, n.nmMotorista, n.Placa, n.marcacao, n.pedBloqueado, n.pedOcorrencia, n.pedReentrega,
                    n.pedMotivoBloqueio, n.pedDesbloPor, n.pedDataDesbloqueio, n.pedDataImportacao, n.pedImplantador));


                if (gridPedido.Rows.Count > 0)
                {
                    //Seleciona a primeira linha
                    gridPedido.CurrentCell = gridPedido.Rows[0].Cells[1];
                    //Foca no grid
                    gridPedido.Focus();

                    //Exibe os dados no label
                    DadosCampos();

                    //Informações dos pedidos encontrados
                    Informacao();
                }
                else
                {
                    MessageBox.Show("Nenhum pedido encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Exibe os dados no label
        private void DadosCampos()
        {
            try
            {
                if (gridPedido.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridPedido.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o nome do motorista
                    lblMotorista.Text = Convert.ToString(gridPedido.Rows[indice].Cells[25].Value);
                    //Seta a placa do veículo
                    lblPlaca.Text = Convert.ToString(gridPedido.Rows[indice].Cells[26].Value);
                    //Seta a marcação do manifesto
                    lblMarcacao.Text = Convert.ToString(gridPedido.Rows[indice].Cells[27].Value);


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        //Informação de pedido
        private void Informacao()
        {
            //Qtd de nota encontrada
            lblTotalPedidos.Text = gridPedido.RowCount.ToString();
            lblTotalItens.Text = "0";
            lblTotalConferidos.Text = "0";
            lblTotalNaoConferidos.Text = "0";
            lblTotalPeso.Text = "0,00";

            int i = 0;
            //Qtd conferido
            int conferido = 1;
            //Qtd não conferido
            int naoConferido = 1;
            //Qtd de itens
            int itens = 0;
            //Total de peso
            double peso = 0;

            while (gridPedido.RowCount > i)
            {
                gridPedido.Rows[i].Cells[6].ToolTipText = "ROTA: " + gridPedido.Rows[i].Cells[24].Value +
                                                          "\nUF: " + gridPedido.Rows[i].Cells[20].Value +
                                                          "\nCIDADE: " + gridPedido.Rows[i].Cells[21].Value +
                                                          "\nBAIRRO: " + gridPedido.Rows[i].Cells[22].Value +
                                                          "\nENDEREÇO: " + gridPedido.Rows[i].Cells[23].Value;

                //Itens
                if (Convert.ToInt32(gridPedido.Rows[i].Cells[9].Value) > 0)
                {
                    itens = Convert.ToInt32(lblTotalItens.Text) + Convert.ToInt32(gridPedido.Rows[i].Cells[9].Value);
                    lblTotalItens.Text = Convert.ToString(itens);
                }

                //Peso
                if (Convert.ToDouble(gridPedido.Rows[i].Cells[10].Value) > 0)
                {
                    peso = Convert.ToDouble(lblTotalPeso.Text) + Convert.ToDouble(gridPedido.Rows[i].Cells[10].Value);
                    lblTotalPeso.Text = string.Format(@"{0:N}", peso);
                }

                //Conferido
                if (!Convert.ToString(gridPedido.Rows[i].Cells[13].Value).Equals(""))
                {
                    lblTotalConferidos.Text = Convert.ToString(conferido++);
                }


                //Não Conferido
                if (Convert.ToString(gridPedido.Rows[i].Cells[14].Value).Equals(""))
                {
                    lblTotalNaoConferidos.Text = Convert.ToString(naoConferido++);
                }

                //Pedido para Reentrega
                if (Convert.ToString(gridPedido.Rows[i].Cells[30].Value).Equals("True"))
                {
                    gridPedido.Rows[i].Cells[0].Style.ForeColor = Color.White;
                    gridPedido.Rows[i].Cells[0].Style.BackColor = Color.RoyalBlue;
                }

                //Pedido para Ocorrência
                if (Convert.ToString(gridPedido.Rows[i].Cells[29].Value).Equals("True"))
                {
                    gridPedido.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    gridPedido.Rows[i].DefaultCellStyle.BackColor = Color.MediumSeaGreen;
                }

                //Pedido Bloqueado
                if (Convert.ToString(gridPedido.Rows[i].Cells[28].Value).Equals("True"))
                {
                    gridPedido.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    gridPedido.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }

                i++;
            }

        }

        //Envia o produto para separação
        private void EnviarPedidoSeparacao()
        {
            try
            {
                if (MessageBox.Show("Deseja enviar os pedidos selecionados para separação?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //Instância a camada de negocios
                    MonitorPedidoNegocios pedidoNegocios = new MonitorPedidoNegocios();

                    foreach (DataGridViewRow row in gridPedido.SelectedRows)
                    {
                        //Atualiza o status para separacao
                        pedidoNegocios.EnviarPedidoSeparacao(cmbEmpresa.Text, codUsuario, Convert.ToInt32(row.Cells[2].Value));

                        ImprimirEtiqueta(
                            Convert.ToInt32(row.Cells[2].Value), //código do pedido
                            Convert.ToString(row.Cells[24].Value), //Rota 
                            Convert.ToString(row.Cells[7].Value + "-" + row.Cells[8].Value), //Cliente
                            Convert.ToString(row.Cells[23].Value), //Endereço
                            Convert.ToString(row.Cells[22].Value), //bairro
                            Convert.ToString(row.Cells[21].Value)); //Cidade


                    }

                    MessageBox.Show("Pedidos enviados com sucesso! ", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Imprimi a etiqueta
        private void ImprimirEtiqueta(int codPedido, string rota, string cliente, string endereco, string bairro, string cidade)
        {
            try
            {
                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    //Pega o caminho da etiqueta prn
                    string etiqueta = null;

                    if (impressora.Equals("ARGOX 214"))
                    {
                        //Pega o caminho da etiqueta prn
                        etiqueta = AppDomain.CurrentDomain.BaseDirectory + "CLIENTE_FLOW_50X80.prn";
                    }

                    if (impressora.Equals("ARGOX 214 PLUS"))
                    {
                        etiqueta = AppDomain.CurrentDomain.BaseDirectory + "CLIENTE_FLOW_50X80.prn";
                    }

                    if (impressora.Equals("ZEBRA"))
                    {
                        etiqueta = AppDomain.CurrentDomain.BaseDirectory + "ZEBRA CLIENTE 50X80.prn";
                    }

                    //Caminho do novo arquivo atualizado
                    string NovaEtiqueta = AppDomain.CurrentDomain.BaseDirectory + codPedido + ".txt";

                    // Abre o arquivo para escrita
                    StreamReader streamReader;
                    streamReader = File.OpenText(etiqueta);

                    string contents = streamReader.ReadToEnd();

                    streamReader.Close();

                    string conteudo = contents;

                    StreamWriter streamWriter = File.CreateText(NovaEtiqueta);

                    // Atualizo as variaveis do arquivo
                    // streamWriter.WriteLine("<STX>L");
                    conteudo = conteudo.Replace("ROTA", "ROTA - " + rota);
                    conteudo = conteudo.Replace("DATA", "" + DateTime.Now);
                    conteudo = conteudo.Replace("CLIENTE", cliente);
                    conteudo = conteudo.Replace("ENDERECO", endereco);
                    conteudo = conteudo.Replace("CIDADE", bairro + "/" + cidade);
                    conteudo = conteudo.Replace("VOLUME", "VOLUME 01");
                    conteudo = conteudo.Replace("PEDIDO", "" + codPedido);
                    conteudo = conteudo.Replace("ESTACAO", "-");
                    conteudo = conteudo.Replace("BARRA", ""+ codPedido);
                    conteudo = conteudo.Replace("EMPRESA", cmbEmpresa.Text);
                    streamWriter.Write(conteudo);

                    streamWriter.WriteLine("E"); //Fim do modo de formatação e imprime
                    streamWriter.WriteLine("/220"); //Avanço para corte da etiqueta
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

                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro imprimir a etiqueta: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    foreach (DataGridViewRow row in gridPedido.SelectedRows)
                    {
                        //Gera as grandezas
                        manifestoNegocios.PesquisaGrandeza(cmbEmpresa.Text, codUsuario, 0, Convert.ToInt32(row.Cells[2].Value));

                        //Instância o relatório
                        FrmMapaSeparacao frame = new FrmMapaSeparacao();
                        frame.Text = "Pedido " + row.Cells[2].Value;
                        int cont = frame.GerarRelatorio(cmbEmpresa.Text, 0, Convert.ToInt32(row.Cells[2].Value), "", false, false, "desc");
                        //Exibe o relatório
                        frame.Show();

                        //Verifica se a impressão deu certo
                        if (cont == 1)
                        {
                            //Registra a impressão
                            manifestoNegocios.RegistraImpressao(cmbEmpresa.Text, codUsuario, 0, Convert.ToInt32(row.Cells[2].Value));
                            //Atualiza o grid com o status de impressão
                            gridPedido.Rows[gridPedido.CurrentRow.Index].Cells[11].Value = DateTime.Now;
                        }
                    }


                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o mapa de separação! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


    }
}
