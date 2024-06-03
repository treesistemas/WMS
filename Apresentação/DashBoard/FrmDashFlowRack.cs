using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;
using System.IO;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Wms
{
    public partial class FrmDashFlowRack : Form
    {
        Thread thrInformacao, //Thread Categoria
               thrRankingEstacao,  //Thread Cliente 
               thrRankingCategorias,  //Thread Estados 
               thrRankingProdutos,  //Thread Fornecedor 
               thrEquilibrioEstacao,  //Thread Frota  
               thrItemHora,  //Thread Rotas
            thrConferente1,
               thrOrganizador,
            thrReciclagem;//

        public List<Empresa> empresaCollection;


        public FrmDashFlowRack()
        {
            InitializeComponent();
        }

        private void FrmDashFlowRack_Load(object sender, EventArgs e)
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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate ()
            {
                //Verifica as atualizações a cada 40 segundos
                timer1.Tick += new System.EventHandler(PesqMetodos);
                timer1.Interval = 1000 * 60 * 1;
                timer1.Start();
            });

        }

        private void PesqMetodos(object sender, EventArgs e)
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Pesquisa as informações
                    thrInformacao = new Thread(PesqInformacao);
                    thrInformacao.Start();

                    //Pesquisa o ranking das estações
                    thrRankingEstacao = new Thread(PesqRankingEstacao);
                    thrRankingEstacao.Start();

                    //Pesquisa o ranking de categorias
                    thrRankingCategorias = new Thread(PesqRankingCategorias);
                    thrRankingCategorias.Start();

                    //Pesquisa os produto top 10
                    thrRankingProdutos = new Thread(PesqRankingProdutos);
                    thrRankingProdutos.Start();

                    //Pesquisa a quantidade de itens de cada estação
                    thrEquilibrioEstacao = new Thread(PesqEquilibrioEstacao);
                    thrEquilibrioEstacao.Start();

                    //Pesquisa a quantidade de itens por hora
                    thrItemHora = new Thread(PesqItemHora);
                    thrItemHora.Start();

                    //Pesquisa o rendimento dos conferentes
                    thrConferente1 = new Thread(PesqRendimentoConferente1);
                    thrConferente1.Start();

                    //Pesquisa o rendimento dos conferentes
                    thrConferente1 = new Thread(PesqRendimentoConferente2);
                    thrConferente1.Start();


                    //Pesquisa o organizador
                    thrOrganizador = new Thread(PesqOrganizador);
                    thrOrganizador.Start();

                    //Pesquisa o organizador
                    thrReciclagem = new Thread(Reciclagem);
                    thrReciclagem.Start();

                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void PesqInformacao()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashFlowRackNegocios dashNegocios = new DashFlowRackNegocios();

                    //Instância a camda de Objêtos
                    DashFlowRack dash = new DashFlowRack();
                    //Instância uma coleção de Objêtos
                    DashFlowRackCollection dashCollection = new DashFlowRackCollection();

                    //Pesquisa a quantidade de cliente
                    dash = dashNegocios.PesqCliente(dtmData.Value.ToShortDateString(),cmbEmpresa.Text);
                    //Exibe o total de volumes
                    lblClientes.Text = Convert.ToString(dash.valor);

                    //Pesquisa a quantidade de pedidos
                    dash = dashNegocios.PesqPedido(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);
                    //Exibe o total de volumes
                    lblPedido.Text = Convert.ToString(dash.valor);

                    //Pesquisa o total de volumes
                    dash = dashNegocios.PesqVolumes(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);
                    //Exibe o total de volumes
                    lblVolumes.Text = Convert.ToString(dash.valor);

                    //Passa o valor para o progressbar
                    progressBar.Maximum = Convert.ToInt32(dash.valor);

                    //Pesquisa o total de volumes auditados
                    dash = dashNegocios.PesqVolumesAuditados(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);
                    //Exibe o total de volumes
                    lblAuditados.Text = Convert.ToString(dash.valor);

                    //Pesquisa o total de volumes auditados
                    dash = dashNegocios.PesqVolumesEnderecados(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);
                    //Exibe o total de volumes endereçados
                    lblEnderecados.Text = Convert.ToString(dash.valor);

                    lblVolumesFinalizados.Text = Convert.ToString(Convert.ToInt32(lblVolumes.Text) - Convert.ToInt32(lblEnderecados.Text));

                    //Adiciona o valor
                    progressBar.Value = Convert.ToInt32(dash.valor);

                    //Pesquisa o total de volumes auditados
                    dash = dashNegocios.PesqMediaItens(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);
                    //Exibe o total de volumes endereçados
                    lblMediaItens.Text = Convert.ToString(dash.valor);


                    //Pesquisa o total de volumes auditados
                    dash = dashNegocios.PesqAbastecimento(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);
                    //Exibe o total de volumes endereçados
                    lblAbastecimento.Text = Convert.ToString(dash.valor);

                    //Vamos considerar que a data seja o dia de hoje, mas pode ser qualquer data.
                    DateTime data = DateTime.Today;

                    //DateTime com o primeiro dia do mês
                    DateTime primeiroDiaDoMes = new DateTime(data.Year, data.Month - 1, 1);

                    //DateTime com o último dia do mês
                    DateTime ultimoDiaDoMes = new DateTime(data.Year, data.Month - 1, DateTime.DaysInMonth(data.Year, data.Month - 1));

                    //Pesquisa o conferente do mês
                    dashCollection = dashNegocios.PesqConferenteMes(primeiroDiaDoMes.ToShortDateString(), ultimoDiaDoMes.ToShortDateString(), cmbEmpresa.Text);

                    if (dashCollection.Count > 0)

                    {//Exibe o conferente em primeiro lugar
                        lblConferenteMes.Text = "CONFERENTE DO MÊS " + Convert.ToString(dashCollection[0].texto);
                        //Declara um vetor de imagem
                        byte[] vetorImagens = null;
                        //Recebe a imagem
                        vetorImagens = (byte[])(dashCollection[0].foto);

                        if (vetorImagens == null)
                        {
                            //Limpa a imagem
                            picFoto4.Image = null;
                        }
                        else
                        {
                            //exibe a imagem
                            string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                            FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                            fs.Write(vetorImagens, 0, vetorImagens.Length);
                            fs.Flush();
                            fs.Close();
                            picFoto4.Image = Image.FromFile(strNomeArquivo);
                        }
                    }

                });
            }
            catch (Exception)
            {
                //MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o organizador que endereçou o volume
        private void PesqOrganizador()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashFlowRackNegocios dashNegocios = new DashFlowRackNegocios();

                    //Instância a camda de Objêtos
                    DashFlowRack dash = new DashFlowRack();
                    //Instância uma coleção de Objêtos
                    DashFlowRackCollection dashCollection = new DashFlowRackCollection();

                    //Pesquisa o total de volumes auditados
                    dashCollection = dashNegocios.PesqRendimentoEnderecamento(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);

                    if (dashCollection.Count > 0)
                    {
                        //Exibe o conferente em primeiro lugar
                        lblEnderecador.Text = Convert.ToString(dashCollection[0].texto);

                        //Declara um vetor de imagem
                        byte[] vetorImagens = null;

                        //Recebe a imagem
                        vetorImagens = (byte[])(dashCollection[0].foto);

                        if (vetorImagens == null)
                        {
                            //Limpa a imagem
                            picFoto3.Image = null;
                        }
                        else
                        {
                            //exibe a imagem
                            string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                            FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                            fs.Write(vetorImagens, 0, vetorImagens.Length);
                            fs.Flush();
                            fs.Close();
                            picFoto3.Image = Image.FromFile(strNomeArquivo);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o rendimento dos conferentes
        private void PesqRendimentoConferente1()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashFlowRackNegocios dashNegocios = new DashFlowRackNegocios();

                    //Instância a camda de Objêtos
                    DashFlowRack dash = new DashFlowRack();
                    //Instância uma coleção de Objêtos
                    DashFlowRackCollection dashCollection = new DashFlowRackCollection();

                    //Pesquisa o total de volumes auditados
                    dashCollection = dashNegocios.PesqRendimentoConferente1(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);

                    if (dashCollection.Count > 0)
                    {
                        //Exibe o conferente em primeiro lugar
                        lblConferentePrimeiro.Text = Convert.ToString(dashCollection[0].texto);

                        //Declara um vetor de imagem
                        byte[] vetorImagens = null;

                        //Recebe a imagem
                        vetorImagens = (byte[])(dashCollection[0].foto);

                        if (vetorImagens == null)
                        {
                            //Limpa a imagem
                            picFoto1.Image = null;
                        }
                        else
                        {
                            //exibe a imagem
                            string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                            FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                            fs.Write(vetorImagens, 0, vetorImagens.Length);
                            fs.Flush();
                            fs.Close();
                            picFoto1.Image = Image.FromFile(strNomeArquivo);
                        }
                    }
                });

            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o rendimento dos conferentes
        private void PesqRendimentoConferente2()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashFlowRackNegocios dashNegocios = new DashFlowRackNegocios();

                    //Instância a camda de Objêtos
                    DashFlowRack dash = new DashFlowRack();
                    //Instância uma coleção de Objêtos
                    DashFlowRackCollection dashCollection = new DashFlowRackCollection();

                    //Pesquisa o total de volumes auditados
                    dashCollection = dashNegocios.PesqRendimentoConferente2(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);

                    if (dashCollection.Count > 0)
                    {
                        //Exibe o conferente em segundo lugar
                        lblConferenteSegundo.Text = Convert.ToString(dashCollection[0].texto);

                        //Declara um vetor de imagem
                        byte[] vetorImagens = null;
                        //Recebe a imagem
                        vetorImagens = (byte[])(dashCollection[0].foto);

                        if (vetorImagens == null)
                        {
                            //Limpa a imagem
                            picFoto2.Image = null;
                        }
                        else
                        {
                            //exibe a imagem
                            string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                            FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                            fs.Write(vetorImagens, 0, vetorImagens.Length);
                            fs.Flush();
                            fs.Close();
                            picFoto2.Image = Image.FromFile(strNomeArquivo);
                        }
                    }
                });

            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        private void PesqRankingEstacao()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashFlowRackNegocios dashNegocios = new DashFlowRackNegocios();

                    //Instância a camda de Objêtos
                    DashFlowRackCollection dashCollection = new DashFlowRackCollection();


                    //Pesquisa o ranking das estações
                    dashCollection = dashNegocios.PesqProdutividadeEstacao(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);

                    chartRankEstacao.Series["Series1"].Points.Clear(); //Limpa os dados
                    chartRankEstacao.ChartAreas[0].Area3DStyle.Inclination = 0; //Inclinação do chart
                    chartRankEstacao.Series["Series1"]["DoughnutRadius"] = "30"; //Espessura do chart
                    chartRankEstacao.Series["Series1"]["PieStartAngle"] = "270"; //Anglo do chart
                    chartRankEstacao.Series["Series1"].IsValueShownAsLabel = true; //exibe os dados chart                
                    chartRankEstacao.Series["Series1"].IsVisibleInLegend = false; //Não exibe as legendas

                    chartRankEstacao.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0; //Remove a linhas horizontais
                    chartRankEstacao.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0; //Remove as linhas verticais

                    chartRankEstacao.ChartAreas[0].AxisX.Interval = 1; //Permite exiber todos as descrições da estação

                    //int? total = 0;

                    for (int ii = dashCollection.Count - 1; ii >= 0; ii--)
                    {
                        //Dados do gráfico
                        chartRankEstacao.Series["Series1"].Points.AddXY(dashCollection[ii].texto, dashCollection[ii].valor);

                        //total += dashCollection[ii].contEstoqueTerceira;
                    }

                    //Exibe a quantidade de endereços
                    //lblTotalRank.Text = total + " Produtos Contados";

                });


            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        private void PesqRankingCategorias()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashFlowRackNegocios dashNegocios = new DashFlowRackNegocios();

                    //Instância a camda de Objêtos
                    DashFlowRackCollection dashCollection = new DashFlowRackCollection();


                    //Pesquisa o ranking das estações
                    dashCollection = dashNegocios.PesqRankingCategorias(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);

                    chartRankCategorias.Series["Series1"].Points.Clear(); //Limpa os dados
                    chartRankCategorias.ChartAreas[0].Area3DStyle.Inclination = 0; //Inclinação do chart
                    chartRankCategorias.Series["Series1"]["DoughnutRadius"] = "30"; //Espessura do chart
                    chartRankCategorias.Series["Series1"]["PieStartAngle"] = "270"; //Anglo do chart
                    chartRankCategorias.Series["Series1"].IsValueShownAsLabel = true; //exibe os dados chart                
                                                                                      //chartRankCategorias.Series["Series1"].IsVisibleInLegend = false; //Não exibe as legendas

                    //chartRankCategorias.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0; //Remove a linhas horizontais
                    //chartRankCategorias.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0; //Remove as linhas verticais

                    chartRankCategorias.ChartAreas[0].AxisX.Interval = 1; //Permite exiber todos as descrições da estação

                    //int? total = 0;

                    for (int ii = dashCollection.Count - 1; ii >= 0; ii--)
                    {
                        //Dados do gráfico
                        chartRankCategorias.Series["Series1"].Points.AddXY(dashCollection[ii].texto + " - " + dashCollection[ii].valor, dashCollection[ii].valor);

                        //total += dashCollection[ii].contEstoqueTerceira;
                    }

                    //Exibe a quantidade de endereços
                    //lblTotalRank.Text = total + " Produtos Contados";

                });


            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        private void PesqRankingProdutos()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashFlowRackNegocios dashNegocios = new DashFlowRackNegocios();

                    //Instância a camda de Objêtos
                    DashFlowRackCollection dashCollection = new DashFlowRackCollection();


                    //Pesquisa o ranking das estações
                    dashCollection = dashNegocios.PesqRankingProdutos(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);

                    //Lista dos produtos
                    lblProduto01.Text = dashCollection[0].texto.Substring(0, 20);
                    lblProduto02.Text = dashCollection[1].texto.Substring(0, 20);
                    lblProduto03.Text = dashCollection[2].texto.Substring(0, 20);
                    lblProduto04.Text = dashCollection[3].texto.Substring(0, 20);
                    lblProduto05.Text = dashCollection[4].texto.Substring(0, 20);
                    lblProduto06.Text = dashCollection[5].texto.Substring(0, 20);
                    lblProduto07.Text = dashCollection[6].texto.Substring(0, 20);
                    lblProduto08.Text = dashCollection[7].texto.Substring(0, 20);
                    lblProduto09.Text = dashCollection[8].texto.Substring(0, 20);
                    lblProduto10.Text = dashCollection[9].texto.Substring(0, 20);

                    lblQTD01.Text = Convert.ToString(dashCollection[0].valor);
                    lblQTD02.Text = Convert.ToString(dashCollection[1].valor);
                    lblQTD03.Text = Convert.ToString(dashCollection[2].valor);
                    lblQTD04.Text = Convert.ToString(dashCollection[3].valor);
                    lblQTD05.Text = Convert.ToString(dashCollection[4].valor);
                    lblQTD06.Text = Convert.ToString(dashCollection[5].valor);
                    lblQTD07.Text = Convert.ToString(dashCollection[6].valor);
                    lblQTD08.Text = Convert.ToString(dashCollection[7].valor);
                    lblQTD09.Text = Convert.ToString(dashCollection[8].valor);
                    lblQTD10.Text = Convert.ToString(dashCollection[9].valor);

                    lblEstacao01.Text = "ESTAÇÃO " + dashCollection[0].texto2;
                    lblEstacao02.Text = "ESTAÇÃO " + dashCollection[1].texto2;
                    lblEstacao03.Text = "ESTAÇÃO " + dashCollection[2].texto2;
                    lblEstacao04.Text = "ESTAÇÃO " + dashCollection[3].texto2;
                    lblEstacao05.Text = "ESTAÇÃO " + dashCollection[4].texto2;
                    lblEstacao06.Text = "ESTAÇÃO " + dashCollection[5].texto2;
                    lblEstacao07.Text = "ESTAÇÃO " + dashCollection[6].texto2;
                    lblEstacao08.Text = "ESTAÇÃO " + dashCollection[7].texto2;
                    lblEstacao09.Text = "ESTAÇÃO " + dashCollection[8].texto2;
                    lblEstacao10.Text = "ESTAÇÃO " + dashCollection[9].texto2;
                });

            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        private void PesqEquilibrioEstacao()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashFlowRackNegocios dashNegocios = new DashFlowRackNegocios();

                    //Instância a camda de Objêtos
                    DashFlowRackCollection dashCollection = new DashFlowRackCollection();


                    //Pesquisa o ranking das estações
                    dashCollection = dashNegocios.PesqEquilibrioEstacao(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);

                    chartEstacao.Series["Series1"].Points.Clear(); //Limpa os dados
                    chartEstacao.ChartAreas[0].Area3DStyle.Inclination = 0; //Inclinação do chart
                    chartEstacao.Series["Series1"]["DoughnutRadius"] = "30"; //Espessura do chart
                    chartEstacao.Series["Series1"]["PieStartAngle"] = "270"; //Anglo do chart
                    chartEstacao.Series["Series1"].IsValueShownAsLabel = true; //exibe os dados chart                
                                                                               //chartEstacao.Series["Series1"].IsVisibleInLegend = false; //Não exibe as legendas

                    chartEstacao.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0; //Remove a linhas horizontais
                    chartEstacao.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0; //Remove as linhas verticais

                    chartEstacao.ChartAreas[0].AxisX.Interval = 1; //Permite exiber todos as descrições da estação

                    //Localizando as estações fixas
                    List<DashFlowRack> dashFixa = dashCollection.FindAll(delegate (DashFlowRack n) { return n.texto2 == "Fixa"; });

                    List<DashFlowRack> dashMovel = dashCollection.FindAll(delegate (DashFlowRack n) { return n.texto2 != "Fixa"; });
                    //Exibe o valor da movel
                    lblMovel.Text = Convert.ToString(dashMovel[0].valor);
                    lblMovel.Visible = true;
                    circularProgressBar1.Visible = true;

                    int total = 0;

                    for (int ii = dashFixa.Count - 1; ii >= 0; ii--)
                    {
                        //Dados do gráfico
                        chartEstacao.Series["Series1"].Points.AddXY(dashFixa[ii].texto + " - " + dashFixa[ii].valor, dashFixa[ii].valor);

                        total += dashFixa[ii].valor;
                    }

                    //Exibe o valor da Fixa
                    lblFixa.Text = Convert.ToString(total);
                    lblFixa.Visible = true;

                    //Exibe a quantidade de endereços
                    lblTotalSku.Text = "Total de SKU: " + (total + Convert.ToInt32(dashMovel[0].valor));

                });

            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        private void PesqItemHora()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashFlowRackNegocios dashNegocios = new DashFlowRackNegocios();

                    //Instância a camda de Objêtos
                    DashFlowRackCollection dashCollection = new DashFlowRackCollection();


                    //Pesquisa o ranking das estações
                    dashCollection = dashNegocios.PesqItemHora(dtmData.Value.ToShortDateString(), cmbEmpresa.Text);

                    chartHora.Series["Series1"].Points.Clear(); //Limpa os dados
                    chartHora.ChartAreas[0].Area3DStyle.Inclination = 0; //Inclinação do chart
                    chartHora.Series["Series1"]["DoughnutRadius"] = "30"; //Espessura do chart
                    chartHora.Series["Series1"]["PieStartAngle"] = "270"; //Anglo do chart
                    chartHora.Series["Series1"].IsValueShownAsLabel = true; //exibe os dados chart                
                                                                            //chartHora.Series["Series1"].IsVisibleInLegend = false; //Não exibe as legendas

                    chartHora.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0; //Remove a linhas horizontais
                    chartHora.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0; //Remove as linhas verticais

                    chartHora.ChartAreas[0].AxisX.Interval = 1; //Permite exiber todos as descrições da estação

                    //int? total = 0;

                    for (int ii = dashCollection.Count - 1; ii >= 0; ii--)
                    {
                        //Dados do gráfico
                        chartHora.Series["Series1"].Points.AddXY(dashCollection[ii].texto, dashCollection[ii].valor);

                        //total += dashCollection[ii].contEstoqueTerceira;
                    }

                    //Exibe a quantidade de endereços
                    //lblTotalRank.Text = total + " Produtos Contados";
                });

            }
            catch (Exception)
            {
                //MessageBox.Show("" + ex);
            }
        }

        private void Reciclagem()
        {
            try
            {
                var diretorio = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

                //Deletar arquivo criado sem explicação
                foreach (FileInfo arquivo in diretorio.GetFiles())
                {
                    if (arquivo.Extension.Equals(""))
                    {
                        try
                        {
                           // FileSecurity fileSecurity = System.IO.File.GetAccessControl("" + arquivo);
                            //IdentityReference identityReference = fileSecurity.GetOwner(typeof(SecurityIdentifier));

                            //NTAccount ntAccount = identityReference.Translate(typeof(NTAccount)) as NTAccount;

                            //string pc = Environment.MachineName + @"\" + Environment.UserName;

                            //if (Convert.ToString(ntAccount).Equals(pc))
                            //{
                                arquivo.Delete();

                                //MessageBox.Show("Excluíndo " + arquivo);
                            //}

                        }
                        catch
                        {
                            //MessageBox.Show("Não é possível excluir " + arquivo);
                        }


                    }
                }
            }
            catch (Exception)
            {

            }
        }


    }
}
