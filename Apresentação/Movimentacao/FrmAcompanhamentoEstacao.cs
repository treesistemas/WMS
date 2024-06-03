using Negocios;
using ObjetoTransferencia;
using System;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmAcompanhamentoEstacao : Form
    {
        public FrmAcompanhamentoEstacao()
        {
            InitializeComponent();

            //Foca no grupo pesquisa para ativa o foco no campo data inicial
            grpPesquisa.Focus();
        }

        //KeyPress
        private void dtmInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmFinal.Focus(); //Foca da data inicial
            }

        }

        private void dtmFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisar.Focus(); //Foca no botão salvar
            }
        }

        //KeyUp
        private void gridEstacao_KeyUp(object sender, KeyEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        //CellClick
        private void gridEstacao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Seta os dados nos campos
            DadosCampos();
        }

        //Click
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa os dados das estações
            PesqDados();
        }

        //Pesquisa os dados das estações
        private void PesqDados()
        {
            try
            {
                //Instância a camada de negocios
                AcompanhamentoEstacaoNegocios acompanhaNegocios = new AcompanhamentoEstacaoNegocios();
                //Instância a coleção de objêto
                AcompanhamentoEstacaoCollection acompanhaCollection = new AcompanhamentoEstacaoCollection();
                //A coleção recebe o resultado da consulta
                acompanhaCollection = acompanhaNegocios.PesqDados(dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString());
                //Limpa o grid
                gridEstacao.Rows.Clear();
                //Grid Recebe o resultado da coleção
                acompanhaCollection.ForEach(n => gridEstacao.Rows.Add(n.codEstacao, n.descEstacao, n.usuEstacao, n.qtdProdutos, n.qtdCategoria, n.emAbastecimento,
                    n.pedidosEnviados, n.pedidosConferidos, n.produtosEnviados, n.produtosConferidos, n.volumesConferidos, "", "", n.pedidoEmConferencia, n.faltaAuditoria, n.sobraAuditoria, n.trocaAuditoria));

                if (acompanhaCollection.Count > 0)
                {

                    //Preenche o chart
                    PreencherInformacao();

                    //Controla o tempo de conferência
                    TimeSpan time;
                    int segundos = 0;
                    int produtosConferidos;
                    string tempoConferencia;
                    string mediaConferencia;

                    //Pesquisa o tempo de conferência da estação
                    for (int i = 0; gridEstacao.Rows.Count > i; i++)
                    {

                        //Passa o código da estação - Tempo de conferência
                        segundos = acompanhaNegocios.PesqTempoConferencia(Convert.ToInt32(gridEstacao.Rows[i].Cells[0].Value), dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString());

                        if (segundos > 0)
                        {
                            //Passa os segundos para o time
                            time = TimeSpan.FromSeconds(segundos);
                            //Recebe em segundos e converte em dias e horas
                            tempoConferencia = time.ToString(@"dd\.hh\:mm\:ss");
                            //Preenche o grid com o tempo de conferência da estação
                            gridEstacao.Rows[i].Cells[11].Value = tempoConferencia;
                        }
                        else
                        {
                            //Preenche o grid com o tempo de conferência da estação
                            gridEstacao.Rows[i].Cells[11].Value = "-";

                        }

                        //Recebe a quantidade de produtos conferidos da estação
                        produtosConferidos = Convert.ToInt32(gridEstacao.Rows[i].Cells[9].Value);


                        //Verifica se existe produto conferido
                        if (produtosConferidos > 0)
                        {
                            time = TimeSpan.FromSeconds(segundos / produtosConferidos);
                            mediaConferencia = time.ToString(@"mm\:ss");

                            //Preenche o grid com a média de conferência da estação
                            gridEstacao.Rows[i].Cells[12].Value = mediaConferencia;
                        }
                        else
                        {
                            //Preenche o grid com a média de conferência da estação
                            gridEstacao.Rows[i].Cells[12].Value = "-";
                        }

                    }

                    //Seta os dados nos campos
                    DadosCampos();

                    //Qtd de categoria encontrada
                    //lblQtd.Text = gridCategoria.RowCount.ToString();

                    //Seleciona a primeira linha do grid
                    gridEstacao.CurrentCell = gridEstacao.Rows[0].Cells[1];
                    //Foca no grid
                    gridEstacao.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhuma informãção encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Seta os dados nos campos
        private void DadosCampos()
        {
            try
            {
                if (gridEstacao.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridEstacao.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o login do usuário
                    lblUsuario.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[2].Value);
                    //Seta a qtd de produtos da estação
                    lblqtdProdutos.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[3].Value);
                    //Seta a qtd de categorias
                    lblqtdCategorias.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[4].Value);
                    //Seta a qtd de produtos em abastecimento
                    lnkAbatecimento.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[5].Value);
                    //Seta a qtd de pedidos enviados
                    lblPedidosEnviados.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[6].Value);
                    //Seta a qtd de pedidos conferidos
                    lblPedidosConferidos.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[7].Value);
                    //Seta a qtd de produtos enviados
                    lblProdutosEnviados.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[8].Value);
                    //Seta a qtd de produtos conferidos
                    lblProdutosConferidos.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[9].Value);
                    //Seta a qtd de volumes 
                    lblVolumes.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[10].Value);
                    //Seta o tempo de conferência 
                    lblTempoConferencia.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[11].Value);
                    //Seta o média de conferência 
                    lblMediaConferencia.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[12].Value);

                    //Seta o pedido em conferência 
                    if (Convert.ToInt32(gridEstacao.Rows[indice].Cells[13].Value) > 0)
                    {
                        lnkEmConferencia.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[13].Value);
                    }
                    else
                    {
                        lnkEmConferencia.Text = "-";
                    }
                    //Seta o falta resgistrada na auditoria 
                    lblFalta.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[14].Value);
                    //Seta a sobre resgistrada na auditoria 
                    lblSobra.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[15].Value);
                    //Seta a troca resgistrada na auditoria 
                    lblTroca.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[16].Value);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        //Preenche o Dashboard e algumas informações adicionais
        private void PreencherInformacao()
        {
            //Limpa o título
            chartPedidos.Titles.Clear();
            //Limpa os dados
            chartPedidos.Series["Produtos"].Points.Clear();

            //Títuo do Chart
            chartPedidos.Titles.Add("Dashboard Produtos Conferidos").Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter, ((byte)(1)), true);

            chartPedidos.ChartAreas[0].Area3DStyle.Enable3D = true;  // set the chartarea to 3D!
            chartPedidos.Series["Produtos"].IsValueShownAsLabel = true;
            //Permite exiber todos as descrições da estação
            chartPedidos.ChartAreas[0].AxisX.Interval = 1;


            for (int ii = gridEstacao.Rows.Count - 1; ii >= 0; ii--)
            {
                //Dados do gráfico
                chartPedidos.Series["Produtos"].Points.AddXY(Convert.ToString(gridEstacao.Rows[ii].Cells[1].Value), Convert.ToString(gridEstacao.Rows[ii].Cells[9].Value));
            }







            //Total de pedidos
            // chartProdutos.Series["Produtos"].Points.AddXY("Produtos", gridPedido.Rows.Count);           


            /*
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
            */
            //Dados do gráfico
            //chartProdutos.Series["Produtos"].Points.AddXY("Enviado", enviado);
            //chartProdutos.Series["Pedido"].Points.AddXY("Não Enviados", naoEnviado);
            //chartProdutos.Series["Pedido"].Points.AddXY("Finalizados", conferido);
            //chartProdutos.Series["Pedido"].Points.AddXY("Pendentes", naoConferidos);

        }

       
    }
}


/* private void pnlCabecalho_MouseMove(object sender, MouseEventArgs e)
          {
              //Controla o movimento do frame
              if (e.Button == MouseButtons.Left)
              {
                  AparenciaForm.ReleaseCapture();
                  AparenciaForm.SendMessage(Handle, AparenciaForm.WM_NCLBUTTONDOWN, AparenciaForm.HT_CAPITION, 0);
              }
          }*/