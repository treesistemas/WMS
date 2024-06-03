using CrystalDecisions.Shared.Json;
using Negocios;
using Negocios.DashBoard;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Wms
{
    public partial class FrmDashboardExpedicao : Form
    {
        Thread thrPedidos, //Thread
               thrRankingEmpilhador,
               thrRankingConferencia,
               thrRankingSeparador,
               thrItemHora,
               thrRankingCategoria,
              // thrSeparador,
            thrReciclagem;

        public List<Empresa> empresaCollection;

        public FrmDashboardExpedicao()
        {
            InitializeComponent();
        }

        

        private void FrmDashboardExpedicao_Load(object sender, EventArgs e)
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
                    thrPedidos = new Thread(PesqPedidos);
                    thrPedidos.Start();

                    //Pesquisa o ranking das estações
                    thrRankingEmpilhador = new Thread(PesqRankingEmpilhador);
                    thrRankingEmpilhador.Start();

                    //Pesquisa o ranking de categorias
                    thrRankingConferencia = new Thread(PesqRankingConferencia);
                    thrRankingConferencia.Start();

                    //Pesquisa os produto top 10
                    thrRankingSeparador = new Thread(PesqRankingSeparador);
                    thrRankingSeparador.Start();

                    //Pesquisa a quantidade de itens por hora
                    thrItemHora = new Thread(PesqItemHora);
                    thrItemHora.Start();

                    //Pesquisa a quantidade de itens de cada estação
                    thrRankingCategoria = new Thread(PesqRankingCategoria);
                    thrRankingCategoria.Start();

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


        //Pesquisa os pedidos
        private void PesqPedidos()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashExpedicaoNegocios dashNegocios = new DashExpedicaoNegocios();

                    //Instância a camda de Objêtos
                    DashExpedicao dash = new DashExpedicao();

                    //Pesquisa o ranking das estações
                    dash = dashNegocios.PesqPedido(dtmDataInicial.Value.ToString(), dtmDataFinal.Value.ToString(), cmbEmpresa.Text);

                    if (dash != null)
                    {
                        //Limpa o título
                        chartPedidos.Titles.Clear();
                        //Limpa os dados
                        chartPedidos.Series["Series1"].Points.Clear();

                        //Títuo do Chart
                        chartPedidos.Titles.Add("Análise de Pedidos").Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter, ((byte)(1)), true);

                        chartPedidos.ChartAreas[0].Area3DStyle.Enable3D = true;  // set the chartarea to 3D!
                        chartPedidos.Series["Series1"].IsValueShownAsLabel = true;

                        //Dados do gráfico
                        chartPedidos.Series["Series1"].Points.AddXY("Pedidos", dash.valor2);
                        chartPedidos.Series["Series1"].Points.AddXY("Conferidos", dash.valor3);
                        chartPedidos.Series["Series1"].Points.AddXY("Pendente", dash.valor2 - dash.valor3);

                        lblManifesto.Text = "Manifesto - " + dash.valor1;
                        lblPeso.Text = "Peso - " + string.Format("{0:n}", dash.valor4);

                        pnlManifesto.Visible = true;
                        lblManifesto.Visible = true;
                        pnlPeso.Visible = true;
                        lblPeso.Visible = true;
                    }
                    else
                    {
                        pnlManifesto.Visible = false;
                        lblManifesto.Visible = false;
                        pnlPeso.Visible = false;
                        lblPeso.Visible = false;
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o ranking dos empilhadores
        private void PesqRankingEmpilhador()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashExpedicaoNegocios dashNegocios = new DashExpedicaoNegocios();
                    //Instância a camda de Objêtos
                    DashExpedicaoCollection dashCollection = new DashExpedicaoCollection();
                    //Pesquisa o ranking
                    dashCollection = dashNegocios.PesqEmpilhador(dtmDataInicial.Value.ToString(), dtmDataFinal.Value.ToString(), cmbEmpresa.Text);

                    if (dashCollection != null)
                    {
                        //Limpa o título
                        chartEmpilhador.Titles.Clear();
                        //Limpa os dados
                        chartEmpilhador.Series["Series1"].Points.Clear();

                        //Títuo do Chart
                        chartEmpilhador.Titles.Add("Ranking de Empilhador").Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter, ((byte)(1)), true);

                        chartEmpilhador.ChartAreas[0].Area3DStyle.Enable3D = true;  // set the chartarea to 3D!
                        chartEmpilhador.Series["Series1"].IsValueShownAsLabel = true;

                        //chartEmpilhador.Legends[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter, ((byte)(1)), true);

                        int posicao = 1;

                        //Ordena e percorre a lista
                        for (int ii = dashCollection.Count - 1; ii >= 0; ii--)
                        {
                            //Dados do gráfico
                            chartEmpilhador.Series["Series1"].Points.AddXY(dashCollection[ii].texto1 + "\n VOLUME " + dashCollection[ii].valor2, dashCollection[ii].valor1);

                            if (posicao == 1)
                            {
                                //Declara um vetor de imagem
                                byte[] vetorImagens = null;

                                //Recebe a imagem
                                vetorImagens = (byte[])(dashCollection[ii].foto);

                                if (vetorImagens == null)
                                {
                                    if (posicao == 1)
                                    {
                                        //Limpa a imagem
                                        picFoto3.Image = null;
                                    }
                                }
                                else
                                {
                                    if (posicao == 1)
                                    {
                                        //Exibe o conferente em primeiro lugar
                                        lblEmpilhador.Text = Convert.ToString(dashCollection[ii].texto1);

                                        //exibe a imagem
                                        string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                                        FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                                        fs.Write(vetorImagens, 0, vetorImagens.Length);
                                        fs.Flush();
                                        fs.Close();
                                        picFoto3.Image = Image.FromFile(strNomeArquivo);
                                    }
                                }
                            }

                            posicao++;
                        }
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o ranking de conferência
        private void PesqRankingConferencia()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashExpedicaoNegocios dashNegocios = new DashExpedicaoNegocios();
                    //Instância a camda de Objêtos
                    DashExpedicaoCollection dashCollection = new DashExpedicaoCollection();
                    //Pesquisa o ranking das estações
                    dashCollection = dashNegocios.PesqConferencia(dtmDataInicial.Value.ToString(), dtmDataFinal.Value.ToString(), cmbEmpresa.Text);

                    //Limpa o título
                    chartConferencia.Titles.Clear();
                    //Limpa os dados
                    chartConferencia.Series["Series1"].Points.Clear();
                    //Títuo do Chart
                    chartConferencia.Titles.Add("Ranking da Conferência").Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter, ((byte)(1)), true);
                    chartConferencia.ChartAreas[0].Area3DStyle.Inclination = 0; //Inclinação do chart
                    chartConferencia.Series["Series1"]["DoughnutRadius"] = "30"; //Espessura do chart
                    chartConferencia.Series["Series1"]["PieStartAngle"] = "270"; //Anglo do chart
                    chartConferencia.Series["Series1"].IsValueShownAsLabel = true; //exibe os dados chart                
                    chartConferencia.Series["Series1"].IsVisibleInLegend = false; //Não exibe as legendas
                    chartConferencia.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0; //Remove a linhas horizontais
                    chartConferencia.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0; //Remove as linhas verticais

                    chartConferencia.ChartAreas[0].AxisX.Interval = 1; //Permite exiber todos as descrições

                    int posicao = 1;
                    //Instância a camda de Objêtos                    
                    List<DashExpedicao> ordenarCollection = dashCollection.OrderBy(n => n.valor2).ToList();

                    //Ordena e percorre a lista
                    for (int ii = ordenarCollection.Count - 1; ii >= 0; ii--)
                    {
                        //Dados do gráfico
                        chartConferencia.Series["Series1"].Points.AddXY(ordenarCollection[ii].texto1 + "\n PED: " + ordenarCollection[ii].valor1, ordenarCollection[ii].valor2);

                        if (posicao <= 2)
                        {
                            //Primeiro separador
                            if (posicao == 1)
                            {
                                //Exibe o conferente em primeiro lugar
                                lblConferentePrimeiro.Text = Convert.ToString(ordenarCollection[ii].texto1);

                                //Declara um vetor de imagem
                                byte[] vetorFoto1 = null;

                                //Recebe a imagem
                                vetorFoto1 = (byte[])(ordenarCollection[ii].foto);

                                if (vetorFoto1 == null)
                                {
                                    //Limpa a imagem
                                    picFoto1.Image = null;
                                }
                                else
                                {
                                    //exibe a imagem
                                    string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                                    FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                                    fs.Write(vetorFoto1, 0, vetorFoto1.Length);
                                    fs.Flush();
                                    fs.Close();
                                    picFoto1.Image = Image.FromFile(strNomeArquivo);

                                }
                            }

                            if (posicao == 2)
                            {
                                //Exibe o conferente em segundo lugar
                                lblConferenteSegundo.Text = Convert.ToString(ordenarCollection[ii].texto1);

                                //Declara um vetor de imagem
                                byte[] vetorFoto2 = null;

                                //Recebe a imagem
                                vetorFoto2 = (byte[])(ordenarCollection[ii].foto);

                                if (vetorFoto2 == null)
                                {
                                    //Limpa a imagem
                                    picFoto2.Image = null;
                                }
                                else
                                {
                                    //exibe a imagem
                                    string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                                    FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                                    fs.Write(vetorFoto2, 0, vetorFoto2.Length);
                                    fs.Flush();
                                    fs.Close();
                                    picFoto2.Image = Image.FromFile(strNomeArquivo);
                                }
                            }
                        }
                        posicao++;
                    }

                    //Exibe a quantidade de endereços
                    //lblTotalRank.Text = total + " Produtos Contados";

                });

                thrRankingConferencia.Abort();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o raking do seprador
        private void PesqRankingSeparador()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashExpedicaoNegocios dashNegocios = new DashExpedicaoNegocios();
                    //Instância a camda de Objêtos
                    DashExpedicaoCollection dashCollection = new DashExpedicaoCollection();
                    //Pesquisa o ranking das estações
                    dashCollection = dashNegocios.PesqSeparacao(dtmDataInicial.Value.ToString(), dtmDataFinal.Value.ToString(), cmbEmpresa.Text);

                    //Limpa o título
                    chartSeparador.Titles.Clear();
                    //Limpa os dados
                    chartSeparador.Series["Series1"].Points.Clear();
                    //Títuo do Chart
                    chartSeparador.Titles.Add("Ranking da Separação").Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter, ((byte)(1)), true);
                    chartSeparador.ChartAreas[0].Area3DStyle.Inclination = 0; //Inclinação do chart
                    chartSeparador.Series["Series1"]["DoughnutRadius"] = "30"; //Espessura do chart
                    chartSeparador.Series["Series1"]["PieStartAngle"] = "270"; //Anglo do chart
                    chartSeparador.Series["Series1"].IsValueShownAsLabel = true; //exibe os dados chart                
                    chartSeparador.Series["Series1"].IsVisibleInLegend = false; //Não exibe as legendas

                    chartSeparador.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0; //Remove a linhas horizontais
                    chartSeparador.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0; //Remove as linhas verticais

                    chartSeparador.ChartAreas[0].AxisX.Interval = 1; //Permite exiber todos as descrições da estação

                    //int? total = 0;

                    //Instância a camda de Objêtos                    
                    List<DashExpedicao> ordenarCollection = dashCollection.OrderByDescending(n => n.valor2).ToList();

                    int count = (ordenarCollection.Count);

                    for (int ii = ordenarCollection.Count - 1; ii >= 0; ii--)
                    {
                        //Dados do gráfico
                        chartSeparador.Series["Series1"].Points.AddXY((count--) + "ª " + ordenarCollection[ii].texto1 + " /PD " + ordenarCollection[ii].valor1, ordenarCollection[ii].valor2);

                        if (ii <= 3)
                        {
                            //Primeiro separador
                            if (ii == 0)
                            {
                                //Exibe o separador em primeiro lugar
                                lblSeparador1.Text = Convert.ToString(ordenarCollection[ii].texto1);

                                //Declara um vetor de imagem
                                byte[] vetorFoto1 = null;

                                //Recebe a imagem
                                vetorFoto1 = (byte[])(ordenarCollection[ii].foto);

                                if (vetorFoto1 == null)
                                {
                                    //Limpa a imagem
                                    picFoto4.Image = null;
                                }
                                else
                                {
                                    //exibe a imagem
                                    string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                                    FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                                    fs.Write(vetorFoto1, 0, vetorFoto1.Length);
                                    fs.Flush();
                                    fs.Close();
                                    picFoto4.Image = Image.FromFile(strNomeArquivo);
                                }
                            }

                            if (ii == 1)
                            {
                                //Exibe o separador em segundo lugar
                                lblSeparador2.Text = Convert.ToString(ordenarCollection[ii].texto1);

                                //Declara um vetor de imagem
                                byte[] vetorFoto2 = null;

                                //Recebe a imagem
                                vetorFoto2 = (byte[])(ordenarCollection[ii].foto);

                                if (vetorFoto2 == null)
                                {
                                    //Limpa a imagem
                                    picFoto5.Image = null;
                                }
                                else
                                {
                                    //exibe a imagem
                                    string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                                    FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                                    fs.Write(vetorFoto2, 0, vetorFoto2.Length);
                                    fs.Flush();
                                    fs.Close();
                                    picFoto5.Image = Image.FromFile(strNomeArquivo);
                                }
                            }

                            if (ii == 2)
                            {
                                //Exibe o separador em terceiro lugar
                                lblSeparador3.Text = Convert.ToString(ordenarCollection[ii].texto1);

                                //Declara um vetor de imagem
                                byte[] vetorFoto3 = null;

                                //Recebe a imagem
                                vetorFoto3 = (byte[])(ordenarCollection[ii].foto);

                                if (vetorFoto3 == null)
                                {
                                    //Limpa a imagem
                                    picFoto6.Image = null;
                                }
                                else
                                {
                                    //exibe a imagem
                                    string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                                    FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                                    fs.Write(vetorFoto3, 0, vetorFoto3.Length);
                                    fs.Flush();
                                    fs.Close();
                                    picFoto6.Image = Image.FromFile(strNomeArquivo);
                                }
                            }
                        }
                    }

                    //Exibe a quantidade de endereços
                    //lblTotalRank.Text = total + " Produtos Contados";

                });

                thrRankingSeparador.Abort();
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        //Pesquisa os itens por hora
        private void PesqItemHora()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashExpedicaoNegocios dashNegocios = new DashExpedicaoNegocios();
                    //Instância a camda de Objêtos
                    DashExpedicaoCollection dashConferenciaCollection = new DashExpedicaoCollection();
                    //Pesquisa o ranking das estações
                    dashConferenciaCollection = dashNegocios.PesqItemHoraConferencia(dtmDataInicial.Value.ToString(), dtmDataFinal.Value.ToString(), cmbEmpresa.Text);

                    //Instância a camda de Objêtos
                    DashExpedicaoCollection dashSeparacaoCollection = new DashExpedicaoCollection();
                    //Pesquisa o ranking das estações
                    dashSeparacaoCollection = dashNegocios.PesqItemHoraSeparacao(dtmDataInicial.Value.ToString(), dtmDataFinal.Value.ToString(), cmbEmpresa.Text);

                    //Instância a camda de Objêtos
                    DashExpedicaoCollection dashAbastecimentoCollection = new DashExpedicaoCollection();
                    //Pesquisa o ranking das estações
                    dashAbastecimentoCollection = dashNegocios.PesqItemHoraAbastecimento(dtmDataInicial.Value.ToString(), dtmDataFinal.Value.ToString(), cmbEmpresa.Text);

                    //Limpa o título
                    chartHora.Titles.Clear();
                    //Limpa os dados
                    chartHora.Series["Conferência"].Points.Clear();
                    //Títuo do Chart
                    chartHora.Titles.Add("Produtividade de Produto Por Hora").Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter, ((byte)(1)), true);
                    chartHora.ChartAreas[0].Area3DStyle.Inclination = 0; //Inclinação do chart
                    chartHora.Series["Conferência"]["DoughnutRadius"] = "30"; //Espessura do chart
                    chartHora.Series["Conferência"]["PieStartAngle"] = "270"; //Anglo do chart
                    chartHora.Series["Conferência"].IsValueShownAsLabel = true; //exibe os dados chart                
                                                                                //chartHora.Series["Series1"].IsVisibleInLegend = false; //Não exibe as legendas
                    chartHora.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0; //Remove a linhas horizontais
                    chartHora.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0; //Remove as linhas verticais

                    chartHora.ChartAreas[0].AxisX.Interval = 1; //Permite exiber todos as descrições da estação

                    //int? total = 0;

                    for (int ii = dashConferenciaCollection.Count - 1; ii >= 0; ii--)
                    {
                        //Dados do gráfico                        
                        chartHora.Series["Conferência"].Points.AddXY(dashConferenciaCollection[ii].texto1, dashConferenciaCollection[ii].valor1);
                        //chartHora.Legends.Clear();
                    }

                    for (int ii = dashSeparacaoCollection.Count - 1; ii >= 0; ii--)
                    {
                        //Dados do gráfico                        
                        chartHora.Series["Separação"].Points.AddXY(dashSeparacaoCollection[ii].texto1, dashSeparacaoCollection[ii].valor1);

                    }

                    for (int ii = dashAbastecimentoCollection.Count - 1; ii >= 0; ii--)
                    {
                        //Dados do gráfico                        
                        chartHora.Series["Abastecimento"].Points.AddXY(dashAbastecimentoCollection[ii].texto1, dashAbastecimentoCollection[ii].valor1);

                    }

                    //Exibe a quantidade de endereços
                    //lblTotalRank.Text = total + " Produtos Contados";
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o raking de categorias
        private void PesqRankingCategoria()
        {
            try
            {
                // Garante que o label seja atualizado da tread que o invocou
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de Negocios
                    DashExpedicaoNegocios dashNegocios = new DashExpedicaoNegocios();
                    //Instância a camda de Objêtos
                    DashExpedicaoCollection dashCollection = new DashExpedicaoCollection();
                    //Pesquisa o ranking das estações
                    dashCollection = dashNegocios.PesqRankingCategoria(dtmDataInicial.Value.ToString(), dtmDataFinal.Value.ToString(), cmbEmpresa.Text);

                    //Limpa o título
                    chartCategoria.Titles.Clear();
                    //Limpa os dados
                    chartCategoria.Series["Series1"].Points.Clear();
                    //Títuo do Chart
                    chartCategoria.Titles.Add("Ranking da Categorias").Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter, ((byte)(1)), true);
                    chartCategoria.ChartAreas[0].Area3DStyle.Inclination = 0; //Inclinação do chart
                    chartCategoria.Series["Series1"]["DoughnutRadius"] = "30"; //Espessura do chart
                    chartCategoria.Series["Series1"]["PieStartAngle"] = "270"; //Anglo do chart
                    chartCategoria.Series["Series1"].IsValueShownAsLabel = true; //exibe os dados chart                
                    chartCategoria.Series["Series1"].IsVisibleInLegend = true;
                    chartCategoria.ChartAreas[0].Area3DStyle.Enable3D = true;  // set the chartarea to 3D!

                    chartCategoria.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0; //Remove a linhas horizontais
                    chartCategoria.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0; //Remove as linhas verticais

                    chartCategoria.ChartAreas[0].AxisX.Interval = 1; //Permite exiber todos as descrições da estação

                    for (int ii = dashCollection.Count - 1; ii >= 0; ii--)
                    {
                        //Dados do gráfico
                        chartCategoria.Series["Series1"].Points.AddXY(dashCollection[ii].texto1, dashCollection[ii].valor1.ToString("C3"));
                    }

                });


            }
            catch (Exception ex)
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
                            //FileSecurity fileSecurity = System.IO.File.GetAccessControl("" + arquivo);
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
