using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Utilitarios;
using Wms.Relatorio;

namespace Wms
{
    public partial class FrmImpressaoManifesto : Form
    {
        //Código do usuário
        public int codUsuario;
        public List<Empresa> empresaCollection;
        //Array com os nome dos conferentes
        string[] conferente;

        int qtdSelecionadas; //Manifestos selecionados
        int qtdGerada; //Manifestos gerando
        
        public string controlaSequenciaCarregamento;

        // Usado para tornar a célula personalizada consistente com um DataGridViewImageCell
        //static Image emptyImage;


        public FrmImpressaoManifesto()
        {
            InitializeComponent();
        }

        private void FrmImpressaoManifesto_Load(object sender, EventArgs e)
        {
            if (empresaCollection.Count > 0)
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

        //Key press
        private void txtManifesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmDataInicial.Focus();
            }
        }

        private void dtmDataInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmDataFinal.Focus();
            }

        }

        private void dtmDataFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisar.Focus();
            }

        }

        //controla o combobox do conferente selecionado
        private void gridManifesto_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combo = e.Control as ComboBox;

            if (combo != null)
            {
                // Remove an existing event-handler, if present, to avoid 
                // adding multiple handlers when the editing control is reused.
                combo.SelectedIndexChanged -=
                    new EventHandler(combo_SelectedIndexChanged);

                // Add the event handler. 
                combo.SelectedIndexChanged +=
                    new EventHandler(combo_SelectedIndexChanged);
            }
        }

        //Atualiza o manifesto com o nome do conferente
        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Instância as linha do grid
            DataGridViewRow linha = gridManifesto.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;

            //Nome do conferente
            string nmConferente = (sender as ComboBox).SelectedItem.ToString();
            //código do manifesto
            int codManifesto = Convert.ToInt32(gridManifesto.Rows[indice].Cells[4].Value);

            //Instância a camada de negocios
            ManifestoNegocios manifestoNegocios = new ManifestoNegocios();
            //Vincula
            manifestoNegocios.VinculaConferente(cmbEmpresa.Text, nmConferente, codManifesto);
            //Analisa a rota para separação multipedidos
            SeparacaoMultiPedidos();
        }

        //Click
        private void gridManifesto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Verifica se a coluna zero foi clicada
            if (gridManifesto.Columns[e.ColumnIndex].Index == 0)
            {
                //Verifica se a coluna do checkbox foi clicada
                if (Convert.ToBoolean(gridManifesto.Rows[e.RowIndex].Cells[0].Value) == true)
                {
                    gridManifesto.Rows[e.RowIndex].Cells[0].Value = false;
                    //Diminui a contagem
                    lblImpressao.Text = "Selecionadas: " + (--qtdSelecionadas).ToString();

                    if (qtdSelecionadas == 0)
                    {
                        lblImpressao.Text = "";
                    }
                }
                else if (Convert.ToBoolean(gridManifesto.Rows[e.RowIndex].Cells[0].Value) == false)
                {
                    gridManifesto.Rows[e.RowIndex].Cells[0].Value = true;

                    //Soma a contagem
                    lblImpressao.Text = "Selecionadas: " + (++qtdSelecionadas).ToString();

                }
            }


        }

        private void mniManifestoPedido_Click(object sender, EventArgs e)
        {
            //Gera o relatório
            Thread thread = new Thread(ImpressaoManifestoPedido);
            thread.Start(); //inicializa
        }

        private void mniManifestoResumo_Click(object sender, EventArgs e)
        {
            //Gera o relatório
            Thread thread = new Thread(ImpressaoManifestoResumo);
            thread.Start(); //inicializa
        }

        private void mniReentregaResumo_Click(object sender, EventArgs e)
        {
            //Gera o relatório
            Thread thread = new Thread(ImpressaoReentregaManifestoResumo);
            thread.Start(); //inicializa
        }

        private void mniRelatorioEntrega_Click(object sender, EventArgs e)
        {
            //Gera o relatório
            Thread thread = new Thread(ImpressaoRelatorioEntrega);
            thread.Start(); //inicializa
        }

        private void mniBloqueados_Click(object sender, EventArgs e)
        {
            //Remove os pedidos bloqueados
            RemoverBloqueados();
        }

        private void mniNaoFinalizadoFlow_Click(object sender, EventArgs e)
        {
            //Remove os pedidos não finalizados no flow rack
            RemoverNaoFinalizadosFlow();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa os manifestos
            PesqManifesto();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha a tela
            Close();
        }

        private void PesqConferente()
        {
            try
            {
                //Instância a camada de negocios
                ManifestoNegocios manifestoNegocios = new ManifestoNegocios();
                //Instância a camada de objêto
                ManifestoCollection manifestoCollection = new ManifestoCollection();
                //Preenche o array
                conferente = manifestoNegocios.PesqConferente();

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os manifestos
        private void PesqManifesto()
        {
            try
            {
                //Instância a camada de negocios
                ManifestoNegocios manifestoNegocios = new ManifestoNegocios();
                //Instância a camada de objêto
                ManifestoCollection manifestoCollection = new ManifestoCollection();
                //Pesquisa o manifesto
                manifestoCollection = manifestoNegocios.PesqManifesto(txtManifesto.Text, cmbEmpresa.Text, dtmDataInicial.Value.Date, dtmDataFinal.Value.Date);

                //Limpa o grid
                gridManifesto.Rows.Clear();

                if (manifestoCollection.Count > 0)
                {
                    //Grid Recebe o resultado da coleção
                    manifestoCollection.ForEach(n => gridManifesto.Rows.Add(false, gridManifesto.Rows.Count + 1, n.dataManifesto,
                        n.tipoRota, n.codManifesto, string.Format(@"{0:N}", n.cubagemManifesto), n.veiculoManifesto, n.tipoVeiculo, n.cubagemVeiculo, n.pedidoManifesto, n.pedPendenteManifesto,
                        n.pedidoBloqueado, n.pendenteFlow, n.itensManifesto, string.Format(@"{0:N}", n.pesoManifesto), n.impressoManifesto, "", n.inicioConferencia, n.fimConferencia, n.NFFaturar));

                    //Pesquisa os conferentes existentes
                    PesqConferente();

                    //Adiciona no combobox do grid
                    for (int ii = 0; conferente.Count() > ii; ii++)
                    {
                        this.ColConferente.Items.Add(conferente[ii]);
                    }

                    //Adiciona no combobox do grid
                    for (int i = 0; manifestoCollection.Count() > i; i++)
                    {
                        if (manifestoCollection[i].conferenteManifesto != null)
                        {
                            gridManifesto.Rows[i].Cells[16].Value = Convert.ToString(manifestoCollection[i].conferenteManifesto);
                        }
                    }

                    //Qtd de manifestos encontrada
                    lblQtd.Text = gridManifesto.RowCount.ToString();

                    //Preenche o dashboard
                    PreencherInformacao();

                    //Seleciona a primeira linha do grid
                    gridManifesto.CurrentCell = gridManifesto.Rows[0].Cells[1];
                    //Foca no grid
                    gridManifesto.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum manifesto encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Divide a rota para separação
        private void SeparacaoMultiPedidos()
        {
            try
            {
                //Instância a camada de negocios
                ManifestoNegocios manifestoNegocios = new ManifestoNegocios();
                //Instância a camada de objêto
                Manifesto manifesto = new Manifesto();

                //Instância as linha do grid
                DataGridViewRow linha = gridManifesto.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;
                //código do manifesto
                string codManifesto = Convert.ToString(gridManifesto.Rows[indice].Cells[4].Value);

                //Analisa o manifesto
                manifesto = manifestoNegocios.AnalisarManifesto(codManifesto);

                //Variáveis
                int qtdPedidos = 10; //Quantidade de pedidos fixo para separação                    
                int qtdSequencia = manifesto.pedidoManifesto / qtdPedidos; //Verifica a quantidade de sequencia da rota                   
                int resto = (manifesto.pedidoManifesto % qtdPedidos);  //Verifica a quantidade de pedidos que sobra                  


                //Mais que 15 pedidos e Mais de 600 kg
                if (manifesto.pedidoManifesto >= 15 && manifesto.pesoManifesto >= 600)
                {
                    //Sequencia os pedidos
                    for (int i = 1; (qtdSequencia + 2) > i; i++) //Soma mais dois para sequenciar o resto da rota 
                    {
                        //Verifica se o resto de pedidos não ultrapassa a 5
                        if (resto <= 5 && (i == qtdSequencia))
                        {
                            //Soma a quantidade de pedidos mais o restante da rota
                            qtdPedidos += resto;
                        }

                        //Executa a sequenciação
                        manifestoNegocios.SequenciaSeparacao(codManifesto, i, qtdPedidos);

                        //MessageBox.Show("Sequencia por pedidos (Vários separam)");
                    }
                }
                //Menos de 15 pedidos e Menor que 600 kg
                else if (manifesto.pedidoManifesto <= 15 && manifesto.pesoManifesto <= 600)
                {
                    //Executa a sequenciação
                    manifestoNegocios.SequenciaSeparacao(codManifesto, 1, manifesto.pedidoManifesto);

                    //MessageBox.Show("Sequencia por peso (Um só separa)");

                }
                // Maior que 2 pedidos e Mais de 1000 volumes
                else if (manifesto.pedidoManifesto >= 2 && manifesto.itensManifesto >= 2 && manifesto.volumesManifesto >= 500)
                {
                    //Divide a rota para três separadores
                    qtdPedidos = manifesto.pedidoManifesto / 3;
                    //Executa a sequenciação
                    manifestoNegocios.SequenciaSeparacao(codManifesto, 1, manifesto.pedidoManifesto);

                    //MessageBox.Show("Sequencia por volumes (Não completo Vários separam)");
                }
                // Menos de 5 pedidos e Mais de 2000 kg e até 5 itens
                else if (manifesto.pedidoManifesto <= 5 && manifesto.pesoManifesto <= 2000 && manifesto.itensManifesto >= 5)
                {
                    //Executa a sequenciação
                    manifestoNegocios.SequenciaSeparacao(codManifesto, 1, manifesto.pedidoManifesto);

                    //MessageBox.Show("Sequencia por itens (Um só separa)");
                }
                else
                {
                    //Executa a sequenciação
                    manifestoNegocios.SequenciaSeparacao(codManifesto, 1, manifesto.pedidoManifesto);

                    //MessageBox.Show("Um só separa");
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
            chartPedido.Titles.Add("Dashboard de Conferência").Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter, ((byte)(1)), true);

            chartPedido.ChartAreas[0].Area3DStyle.Enable3D = true;  // set the chartarea to 3D!
            chartPedido.Series["Pedido"].IsValueShownAsLabel = true;

            //Variáveis que controla a qtd no gráfico            
            int qtdPedido = 0;
            int qtdConferida = 0;
            int qtdPendente = 0;

            //Verifica a quantidade de pedidos
            for (int ii = 0; gridManifesto.Rows.Count > ii; ii++)
            {
                //Soma a qtd de pedidos
                if (!Convert.ToString(gridManifesto.Rows[ii].Cells[9].Value).Equals(""))
                {
                    qtdPedido += Convert.ToInt32(gridManifesto.Rows[ii].Cells[9].Value);
                }

                //Soma a qtd de pedidos pendentes
                if (!Convert.ToString(gridManifesto.Rows[ii].Cells[10].Value).Equals(""))
                {
                    qtdPendente += Convert.ToInt32(gridManifesto.Rows[ii].Cells[10].Value);
                }

                //Verifica os pedidos bloqueados
                if (Convert.ToInt32(gridManifesto.Rows[ii].Cells[11].Value) > 0)
                {
                    gridManifesto.Rows[ii].DefaultCellStyle.ForeColor = Color.White;
                    gridManifesto.Rows[ii].DefaultCellStyle.BackColor = Color.Red;

                    gridManifesto.Rows[ii].Cells[21].Value = "ATENÇÃO: PEDIDOS BLOQUEADOS";
                }

                //Verifica os pedidos não finalizados no flowrack
                if (Convert.ToInt32(gridManifesto.Rows[ii].Cells[12].Value) > 0)
                {
                    gridManifesto.Rows[ii].DefaultCellStyle.ForeColor = Color.White;
                    gridManifesto.Rows[ii].DefaultCellStyle.BackColor = Color.MediumSeaGreen;

                    gridManifesto.Rows[ii].Cells[21].Value = "ATENÇÃO: PEDIDOS NÃO FINALIZADOS NO FLOW RACK";
                }

                //Multiplica a cubagem do veículo pelo o fator de cubagem padrão corresponde a 300
                if (Convert.ToDouble(gridManifesto.Rows[ii].Cells[8].Value) > 0)
                {
                    gridManifesto.Rows[ii].Cells[8].Value = string.Format(@"{0:N}", Convert.ToDouble(gridManifesto.Rows[ii].Cells[8].Value) * 300 / 1000);


                    //Multiplica a cubagem do veículo pelo o fator de cubagem padrão corresponde a 300
                    if (Convert.ToDouble(gridManifesto.Rows[ii].Cells[5].Value) > Convert.ToDouble(gridManifesto.Rows[ii].Cells[8].Value))
                    {
                        gridManifesto.Rows[ii].DefaultCellStyle.ForeColor = Color.Black;
                        gridManifesto.Rows[ii].DefaultCellStyle.BackColor = Color.Orange;

                        gridManifesto.Rows[ii].Cells[21].Value = "ATENÇÃO: EXECE A CAPACIDADE DO VEÍCULO";
                    }
                }

                //Verifica se existe dois ou mais manifesto para o mesmo veículo
                for (int i = 0; gridManifesto.Rows.Count > i; i++)
                {
                    //Multiplica a cubagem do veículo pelo o fator de cubagem padrão corresponde a 300
                    if (Convert.ToString(gridManifesto.Rows[ii].Cells[6].Value) == Convert.ToString(gridManifesto.Rows[i].Cells[6].Value) && ii != i)
                    {
                        gridManifesto.Rows[ii].Cells[6].Style.ForeColor = Color.White;
                        gridManifesto.Rows[ii].Cells[6].Style.BackColor = Color.SteelBlue;

                        gridManifesto.Rows[ii].Cells[21].Value = "ATENÇÃO: VEÍCULO COM VÁRIOS MANIFESTOS";
                    }
                }

                //Verifica o status da rota - NÃO INICIADA
                if (Convert.ToString(gridManifesto.Rows[ii].Cells[17].Value).Equals(""))
                {
                    gridManifesto.Rows[ii].Cells[20].Value = "PENDENTE";
                    gridManifesto.Rows[ii].Cells[20].Style.ForeColor = Color.White;
                    gridManifesto.Rows[ii].Cells[20].Style.BackColor = Color.Red;
                }

                //Verifica o status da rota - INICIADA
                if (!Convert.ToString(gridManifesto.Rows[ii].Cells[17].Value).Equals(""))
                {
                    gridManifesto.Rows[ii].Cells[20].Value = "EM CONFERÊNCIA";
                    gridManifesto.Rows[ii].Cells[20].Style.ForeColor = Color.Black;
                    gridManifesto.Rows[ii].Cells[20].Style.BackColor = Color.Yellow;
                }

                //Verifica o status da rota - FATURAMENTO
                if (Convert.ToString(gridManifesto.Rows[ii].Cells[10].Value).Equals("0"))
                {
                    gridManifesto.Rows[ii].Cells[20].Value = "FATURAMENTO";
                    gridManifesto.Rows[ii].Cells[20].Style.ForeColor = Color.White;
                    gridManifesto.Rows[ii].Cells[20].Style.BackColor = Color.MediumSeaGreen;
                }

                //Verifica o status da rota - FINALIZADA
                if (Convert.ToInt32(gridManifesto.Rows[ii].Cells[19].Value) == 0)
                {
                    gridManifesto.Rows[ii].Cells[20].Value = "FINALIZADA";
                    gridManifesto.Rows[ii].Cells[20].Style.ForeColor = Color.White;
                    gridManifesto.Rows[ii].Cells[20].Style.BackColor = Color.SteelBlue;
                }
            }

            //verifica a quantidade de pedidos conferidos
            if (qtdPedido != qtdPendente)
            {
                qtdConferida = qtdPedido - qtdPendente;
            }

            //Dados do gráfico
            chartPedido.Series["Pedido"].Points.AddXY("Pedidos", qtdPedido);
            chartPedido.Series["Pedido"].Points.AddXY("Conferidos", qtdConferida);
            chartPedido.Series["Pedido"].Points.AddXY("Pendentes", qtdPendente);

        }

        //Gera a impressão de Pedido
        private void ImpressaoManifestoPedido()
        {
            try
            {
                //Instância a camada de utilitarios
                GerarMarcacao marcacao = new GerarMarcacao();
                //Instância a camada de negocios
                ManifestoNegocios manifestoNegocios = new ManifestoNegocios();

                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    //Mensagem na tela
                    IncrementarProgressBar("Aguarde");
                    //Define um valor para o progressbar
                    Progressbar(qtdSelecionadas);
                    //Exibe o progressbar
                    progressBar1.Visible = true;

                    //Zera a contagem
                    qtdGerada = 0;
                    //Limpa o progressbar
                    progressBar1.Value = 0;

                    //Percorre todas as linhas do grid
                    for (int i = 0; gridManifesto.Rows.Count > i; i++)
                    {
                        //Verifica se o manifesto está selecionado para impressão
                        if (Convert.ToBoolean(gridManifesto.Rows[i].Cells[0].Value) == true)
                        {
                            //Ignora manifestos zerados
                            if (Convert.ToInt32(gridManifesto.Rows[i].Cells[9].Value) == 0)
                            {
                                //Desmarca a impressão
                                gridManifesto.Rows[i].Cells[0].Value = false;
                                //Soma mais um para finalizar o progressbar
                                ++qtdGerada;
                            }
                            else
                            {
                                //Instância o objêto
                                ProdutoCollection produtoCollection = new ProdutoCollection();
                                //Verifica se no manifesto existe produtos com falta de dados logísticos
                                produtoCollection = manifestoNegocios.VerificaProduto(Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value));

                                if (produtoCollection.Count > 0)
                                {
                                    string produtos = "Por favor inserir os dados logíticos do(s) produto(s) abaixo (" + gridManifesto.Rows[i].Cells[4].Value + "):\n";

                                    for (int ii = 0; produtoCollection.Count > ii; ii++)
                                    {
                                        produtos = produtos + produtoCollection[ii].codProduto + " - " + produtoCollection[ii].descProduto + "\n";
                                    }

                                    MessageBox.Show(produtos, "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    //Gerar a marcação do manifesto
                                    manifestoNegocios.RegistraMarcacao(cmbEmpresa.Text, Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value), marcacao.gerar());
                                    //Gera as grandeza
                                    manifestoNegocios.PesquisaGrandeza(cmbEmpresa.Text, codUsuario, Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value), 0);

                                    //Instância o relatório
                                    FrmMapaSeparacao frame = new FrmMapaSeparacao();
                                    frame.Text = "Manifesto " + gridManifesto.Rows[i].Cells[4].Value;

                                    //Por padrão ordem decrescente
                                    string ordem = "desc";

                                    if(rbtCrescente.Checked == true)
                                    {
                                        ordem = "asc";
                                    }

                                    int cont = frame.GerarRelatorio(cmbEmpresa.Text, Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value), 0, controlaSequenciaCarregamento, chkNaoConferidos.Checked, chkNaoImpresso.Checked, ordem);

                                    //Verifica se a impressão deu certo
                                    if (cont == 1)
                                    {
                                        //Registra a impressão
                                        manifestoNegocios.RegistraImpressao(cmbEmpresa.Text, codUsuario, Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value), 0);

                                        //Desmarca a impressão
                                        gridManifesto.Rows[i].Cells[0].Value = false;
                                        //Atualiza o grid com o status de impressão
                                        gridManifesto.Rows[i].Cells[15].Value = "SIM";
                                        //Quanidade gerada recebe mais 1
                                        ++qtdGerada;
                                        //Acrescenta o valor no progressbar
                                        progressBar1.Value = qtdGerada;


                                        //Controla a mensagem de impressão
                                        IncrementarProgressBar("Gerada: " + qtdGerada + "/" + qtdSelecionadas);

                                        if (qtdGerada == qtdSelecionadas)
                                        {
                                            //Oculta o progressbar
                                            progressBar1.Visible = false;
                                            //Oculta a mensagem
                                            lblImpressao.Text = "";
                                            //Zera a quantidade selecionada e qtd gerada
                                            qtdSelecionadas = 0; qtdGerada = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o mapa de separação! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Gera a impressão por resumo
        private void ImpressaoManifestoResumo()
        {
            try
            {
                //Instância a camada de utilitarios
                GerarMarcacao marcacao = new GerarMarcacao();
                //Instância a camada de negocios
                ManifestoNegocios manifestoNegocios = new ManifestoNegocios();

                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    //Mensagem na tela
                    IncrementarProgressBar("Aguarde");
                    //Define um valor para o progressbar
                    Progressbar(qtdSelecionadas);
                    //Exibe o progressbar
                    progressBar1.Visible = true;

                    //Percorre todas as linhas do grid
                    for (int i = 0; gridManifesto.Rows.Count > i; i++)
                    {
                        //Verifica se o manifesto está selecionado para impressão
                        if (Convert.ToBoolean(gridManifesto.Rows[i].Cells[0].Value) == true)
                        {
                            //Ignora manifestos zerados
                            if (Convert.ToInt32(gridManifesto.Rows[i].Cells[9].Value) == 0)
                            {
                                //Desmarca a impressão
                                gridManifesto.Rows[i].Cells[0].Value = false;
                                //Soma mais um para finalizar o progressbar
                                ++qtdGerada;
                            }
                            else
                            {
                                //Gerar a marcação do manifesto
                                manifestoNegocios.RegistraMarcacao(cmbEmpresa.Text, Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value), marcacao.gerar());
                                //Gera as grandeza
                                manifestoNegocios.PesqResumoManifesto(Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value), chkNaoConferidos.Checked, chkNaoImpresso.Checked); //Código do manifesto

                                //Instância o relatório
                                FrmMapaResumo frame = new FrmMapaResumo();
                                frame.Text = "Manifesto " + gridManifesto.Rows[i].Cells[4].Value;
                                //Passa o número do manifesto
                                int cont = frame.GerarRelatorio(Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value), chkNaoConferidos.Checked, chkNaoImpresso.Checked, "ENTREGA");
                                //Exibe o relatório
                                frame.Show();

                                //Verifica se a impressão deu certo
                                if (cont == 1)
                                {
                                    //Registra a impressão
                                    manifestoNegocios.RegistraImpressao(cmbEmpresa.Text, codUsuario, Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value), 0);
                                    //Atualiza o grid com o status de impressão
                                    gridManifesto.Rows[i].Cells[15].Value = "SIM";
                                    //Desmarca a impressão
                                    gridManifesto.Rows[i].Cells[0].Value = false;
                                    //Quanidade gerada recebe mais 1
                                    ++qtdGerada;
                                    //Acrescenta o valor no progressbar
                                    progressBar1.Value = qtdGerada;

                                    //Controla a mensagem de impressão
                                    IncrementarProgressBar("Gerando: " + qtdGerada + "/" + qtdSelecionadas);

                                    if (qtdGerada == qtdSelecionadas)
                                    {
                                        //Oculta o progressbar
                                        progressBar1.Visible = false;
                                        //Oculta a mensagem
                                        lblImpressao.Text = "";
                                        //Zera a quantidade selecionada e qtd gerada
                                        qtdSelecionadas = 0; qtdGerada = 0;
                                    }
                                }
                            }
                        }
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o mapa de separação! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Gera a impressão de reentrega por resumo de manifesto
        private void ImpressaoReentregaManifestoResumo()
        {
            try
            {
                //Instância a camada de utilitarios
                GerarMarcacao marcacao = new GerarMarcacao();
                //Instância a camada de negocios
                ManifestoNegocios manifestoNegocios = new ManifestoNegocios();

                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    //Mensagem na tela
                    IncrementarProgressBar("Aguarde");
                    //Define um valor para o progressbar
                    Progressbar(qtdSelecionadas);
                    //Exibe o progressbar
                    progressBar1.Visible = true;

                    //Percorre todas as linhas do grid
                    for (int i = 0; gridManifesto.Rows.Count > i; i++)
                    {
                        //Verifica se o manifesto está selecionado para impressão
                        if (Convert.ToBoolean(gridManifesto.Rows[i].Cells[0].Value) == true)
                        {
                            //Ignora manifestos zerados
                            if (Convert.ToInt32(gridManifesto.Rows[i].Cells[9].Value) == 0)
                            {
                                //Desmarca a impressão
                                gridManifesto.Rows[i].Cells[0].Value = false;
                                //Soma mais um para finalizar o progressbar
                                ++qtdGerada;
                            }
                            else
                            {                                
                                //Instância o relatório
                                FrmMapaResumo frame = new FrmMapaResumo();
                                frame.Text = "Manifesto " + gridManifesto.Rows[i].Cells[4].Value;
                                //Passa o número do manifesto
                                int cont = frame.GerarRelatorio(Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value), false, false, "REENTREGA");
                                //Exibe o relatório
                                frame.Show();

                                //Verifica se a impressão deu certo
                                if (cont == 1)
                                {                         
                                    //Desmarca a impressão
                                    gridManifesto.Rows[i].Cells[0].Value = false;
                                    //Quanidade gerada recebe mais 1
                                    ++qtdGerada;
                                    //Acrescenta o valor no progressbar
                                    progressBar1.Value = qtdGerada;

                                    //Controla a mensagem de impressão
                                    IncrementarProgressBar("Gerando: " + qtdGerada + "/" + qtdSelecionadas);

                                    if (qtdGerada == qtdSelecionadas)
                                    {
                                        //Oculta o progressbar
                                        progressBar1.Visible = false;
                                        //Oculta a mensagem
                                        lblImpressao.Text = "";
                                        //Zera a quantidade selecionada e qtd gerada
                                        qtdSelecionadas = 0; qtdGerada = 0;
                                    }
                                }
                            }
                        }
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o resumo de reentrega por manifesto! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //Gera a impressão por entrega
        private void ImpressaoRelatorioEntrega()
        {
            try
            {
                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    //Mensagem na tela
                    IncrementarProgressBar("Aguarde");
                    //Define um valor para o progressbar
                    Progressbar(qtdSelecionadas);
                    //Exibe o progressbar
                    progressBar1.Visible = true;

                    //Percorre todas as linhas do grid
                    for (int i = 0; gridManifesto.Rows.Count > i; i++)
                    {
                        //Verifica se o manifesto está selecionado para impressão
                        if (Convert.ToBoolean(gridManifesto.Rows[i].Cells[0].Value) == true)
                        {
                            //Ignora manifestos zerados
                            if (Convert.ToInt32(gridManifesto.Rows[i].Cells[9].Value) == 0)
                            {
                                //Desmarca a impressão
                                gridManifesto.Rows[i].Cells[0].Value = false;
                                //Soma mais um para finalizar o progressbar
                                ++qtdGerada;
                            }
                            else if (Convert.ToString(gridManifesto.Rows[i].Cells[20].Value) != "FINALIZADA")
                            {
                                MessageBox.Show("O manifesto ainda não foi faturado!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                //Instância o relatório
                                FrmMapaEntrega frame = new FrmMapaEntrega();
                                frame.Text = "Manifesto " + gridManifesto.Rows[i].Cells[4].Value;
                                //Passa o número do manifesto
                                int cont = frame.GerarRelatorio(cmbEmpresa.Text, Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value), 0, "", "desc");

                                //Verifica se a impressão deu certo
                                if (cont == 1)
                                {
                                    //Desmarca a impressão
                                    gridManifesto.Rows[i].Cells[0].Value = false;
                                    //Quanidade gerada recebe mais 1
                                    ++qtdGerada;
                                    //Acrescenta o valor no progressbar
                                    progressBar1.Value = qtdGerada;

                                    //Controla a mensagem de impressão
                                    IncrementarProgressBar("Gerando: " + qtdGerada + "/" + qtdSelecionadas);

                                    if (qtdGerada == qtdSelecionadas)
                                    {
                                        //Oculta o progressbar
                                        progressBar1.Visible = false;
                                        //Oculta a mensagem
                                        lblImpressao.Text = "";
                                        //Zera a quantidade selecionada e qtd gerada
                                        qtdSelecionadas = 0; qtdGerada = 0;
                                    }
                                }
                            }
                        }
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o relatório de entrega! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Remove do manifesto os pedidos bloqueados
        private void RemoverBloqueados()
        {
            try
            {
                //Instância a camada de negocios
                ManifestoNegocios manifestoNegocios = new ManifestoNegocios();

                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    //Percorre todas as linhas do grid
                    for (int i = 0; gridManifesto.Rows.Count > i; i++)
                    {
                        //Verifica se o manifesto está selecionado para remoção do pedidos bloqueados
                        if (Convert.ToBoolean(gridManifesto.Rows[i].Cells[0].Value) == true)
                        {
                            //Remove os pedidos bloqueados
                            manifestoNegocios.RemoverBloqueado(Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value));

                        }
                    }

                    MessageBox.Show("Pedidos bloqueados removidos do manifesto com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Remove do manifesto os pedidos bloqueados
        private void RemoverNaoFinalizadosFlow()
        {
            try
            {
                //Instância a camada de negocios
                ManifestoNegocios manifestoNegocios = new ManifestoNegocios();

                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    //Percorre todas as linhas do grid
                    for (int i = 0; gridManifesto.Rows.Count > i; i++)
                    {
                        //Verifica se o manifesto está selecionado para remoção do pedidos bloqueados
                        if (Convert.ToBoolean(gridManifesto.Rows[i].Cells[0].Value) == true)
                        {
                            //Remove os pedidos bloqueados
                            manifestoNegocios.RemoverNaoFinalizadoFlow(Convert.ToInt32(gridManifesto.Rows[i].Cells[4].Value));
                        }
                    }

                    MessageBox.Show("Pedidos não finalizados no flow rack removidos do manifesto com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //Starta a contagem do progressbar
        private void Progressbar(int valor)
        {
            //Garante que o progressbar seja executado da thread que foi iniciado
            Invoke((MethodInvoker)delegate ()
            {
                //Define um valor para o progressbar
                progressBar1.Maximum = (valor);

            });
        }

        //Incrementa a contagem no progressbar
        private void IncrementarProgressBar(string processo)
        {
            Invoke((MethodInvoker)delegate ()
            {
                //Exibe o texto no processo
                lblImpressao.Text = processo;

            });

        }

        
    }

}

