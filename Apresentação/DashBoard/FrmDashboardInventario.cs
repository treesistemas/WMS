using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Wms
{
    public partial class FrmDashboardInventario : Form
    {
        //Variável responsável pelo código do inventário
        int codInventario = 0;
        int codResponsavel = 0;
        //Instância um array de inteiro
        double[] dados = new double[4];
        //Tempo de pesquisa
        Timer timer = new Timer();
        //Thead de pesquisa
        Thread thread, thread1, thread2, thread3, thread4;


        public FrmDashboardInventario()
        {
            InitializeComponent();
        }

        private void FrmDashboardInventario_Load(object sender, EventArgs e)
        {
            //Pesquisa o inventário           
            timer.Tick += new System.EventHandler(PesqInventario);
            timer.Interval = 1000 * 1; //2 segundos      
            timer.Start();

        }

        
        private void FrmDashboardInventario_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Para o time ao fechar a tela
            timer.Stop();

            if (thread != null)
            {
                //Para o theard ao fechar a tela
                thread.Abort();
            }

            if (thread1 != null)
            {
                //Para o theard ao fechar a tela
                thread1.Abort();
            }

            if (thread2 != null)
            {
                //Para o theard ao fechar a tela
                thread2.Abort();
            }
            
            if (thread3 != null)
            {
                //Para o theard ao fechar a tela
                thread3.Abort();
            }

            if (thread4 != null)
            {
                //Para o theard ao fechar a tela
                thread4.Abort();
            }
        }

        //Pesquisa o inventário
        private void PesqInventario(object sender, System.EventArgs e)
        {
            try
            {
                //Instância a camada de negocios
                DashInventarioNegocios inventarioNegocios = new DashInventarioNegocios();
                //Instância o objêto
                Inventarios inventario = new Inventarios();
                //Pesquisa
                inventario = inventarioNegocios.PesqInventario();

                if (inventario.codInventario > 0)
                {
                    //Passa o código do inventário para a variável
                    codInventario = inventario.codInventario;
                    //Passa o código do inventário para a variável
                    codResponsavel = inventario.codUsuarioInicial;
                    //Exibe as informações
                    lblInventario.Text = inventario.codInventario + "-" + inventario.descInventario; //Descrição do inventário
                    lblTipo.Text = inventario.tipoInventario.ToUpper(); //Tipo do inventário
                    lblAuditoria.Text = inventario.tipoAuditoria.ToUpper(); //Tipo de auditoria
                    lblResponsavel.Text = inventario.usuarioInicial; //usuário responsável por abrir o inventário
                    //Data e hora da ultima atualização
                    lblAtualizacao.Text = DateTime.Now.ToString();

                    if (!inventario.tipoInventario.Equals("Completo"))
                    {
                        lblTipo.Text += " (" + inventario.descRotativo.ToUpper() + ")"; //Tipo de inventário rotativo
                    }

                    //Pesquisa o progresso do inventário
                    thread = new Thread(PesqProgresso);
                    thread.Start();

                    //Pesquisa as contagens do inventário
                    thread1 = new Thread(PesqContagens);
                    thread1.Start();

                    //Pesquisa o rank
                    thread2 = new Thread(PesqRanking);
                    thread2.Start();

                    //Pesquisa a quanditade de produtos
                    thread3 = new Thread(PesqProdutos);
                    thread3.Start();

                    //Pesquisa a quanditade de produtos
                    thread4 = new Thread(PesqContagem);
                    thread4.Start();                    

                    //Altera a pesquisa para 
                    timer.Interval = 1000 * 60 * 4; //2 minutos

                }
                else
                {
                    //Para o time
                    timer.Stop();
                    MessageBox.Show("Não existe inventário aberto!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o progresso do inventário
        private void PesqProgresso()
        {
            try
            {

                //Instância a camada de negocios
                DashInventarioNegocios inventarioNegocios = new DashInventarioNegocios();

                Invoke((MethodInvoker)delegate ()
                {
                    dados = inventarioNegocios.PesqProgresso(codInventario);
                
                
                    chart1.Series["Series1"].Points.Clear(); //Limpa os dados
                    chart1.ChartAreas[0].Area3DStyle.Enable3D = true;  // Exibe o chart em 3D
                    chart1.ChartAreas[0].Area3DStyle.Inclination = 0; //Inclinação do chart
                    chart1.Series["Series1"]["DoughnutRadius"] = "30"; //Espessura do chart
                    chart1.Series["Series1"]["PieStartAngle"] = "270"; //Anglo do chart
                    chart1.Series["Series1"].IsValueShownAsLabel = false; //Não exibe os dados no grid
                    chart1.Series["Series1"].IsVisibleInLegend = false; //Não exibe as legendas
                                                                        //Total de pedidos
                    chart1.Series["Series1"].Points.AddY(dados[2]); //Dados do Picking           
                    chart1.Series["Series1"].Points.AddY(dados[1]); //Dados do Pulmão
                    chart1.Series["Series1"].Points.AddY(dados[0] - (dados[1] + dados[2])); //Dados Pendentes


                    //Exibe o progresso do inventário
                    lblProgresso.Text = string.Format("{0:n}",(dados[3] / dados[0]) * 100) + "%";
                    //Exibe a quantidade de endereços
                    lblTotalEndereco.Text = dados[0] + " Endereços";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Pesquisa as contagens do inventário
        private void PesqContagens()
        {
            try
            {
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de negocios
                    DashInventarioNegocios inventarioNegocios = new DashInventarioNegocios();
                    dados = inventarioNegocios.PesqContagens(codInventario);

                    //Exibe as contagens
                    lblContPulmao.Text = Convert.ToInt32(dados[0]).ToString(); //Contagem no pulmão
                    lblContPicking.Text = Convert.ToInt32(dados[1]).ToString(); //Contagem no picking
                    lblcontAcertos.Text = Convert.ToInt32(dados[2]).ToString(); //Contagem corretas
                    lblContErros.Text = Convert.ToInt32(dados[3]).ToString(); //Contsgem erradas
                                                                              //Exibe a quantidade de endeços contados
                    lblEnderecoContados.Text = Convert.ToInt32(dados[0] + dados[1]) + " Endereços contados";

                    //Pesquisa a acuricidade
                    PesqAcuricidade();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Pesquisa o ranking
        private void PesqRanking()
        {
            try
            {
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de negocios
                    DashInventarioNegocios inventarioNegocios = new DashInventarioNegocios();
                    //Instância uma coleção de objêto
                    ItemInventarioCollection itensCollection = new ItemInventarioCollection();
                    //Pesquisa
                    itensCollection = inventarioNegocios.PesqRankingUsuario(codInventario, codResponsavel);

                    chartRank.Series["Series1"].Points.Clear(); //Limpa os dados
                    chartRank.ChartAreas[0].Area3DStyle.Inclination = 0; //Inclinação do chart
                    chartRank.Series["Series1"]["DoughnutRadius"] = "30"; //Espessura do chart
                    chartRank.Series["Series1"]["PieStartAngle"] = "270"; //Anglo do chart
                    chartRank.Series["Series1"].IsValueShownAsLabel = true; //exibe os dados chart                
                    chartRank.Series["Series1"].IsVisibleInLegend = false; //Não exibe as legendas

                    chartRank.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0; //Remove a linhas horizontais
                    chartRank.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0; //Remove as linhas verticais

                    chartRank.ChartAreas[0].AxisX.Interval = 1; //Permite exiber todos as descrições da estação

                    int? total = 0;

                    for (int ii = itensCollection.Count - 1; ii >= 0; ii--)
                    {
                        //Dados do gráfico
                        chartRank.Series["Series1"].Points.AddXY(itensCollection[ii].usuarioTerceira, itensCollection[ii].contTerceira);

                        total += itensCollection[ii].contTerceira;
                    }

                    //Exibe a quantidade de endereços
                    lblTotalRank.Text = total + " Produtos Contados";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Pesquisa o ranking
        private void PesqContagem()
        {
            try
            {
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de negocios
                    DashInventarioNegocios inventarioNegocios = new DashInventarioNegocios();
                    //Instância uma coleção de objêto
                    ItemInventarioCollection itensCollection = new ItemInventarioCollection();
                    //Pesquisa
                    itensCollection = inventarioNegocios.PesqRankingContagem(codInventario);

                    chartContagem.Series["Series1"].Points.Clear(); //Limpa os dados
                    chartContagem.ChartAreas[0].Area3DStyle.Inclination = 0; //Inclinação do chart
                    chartContagem.Series["Series1"]["DoughnutRadius"] = "30"; //Espessura do chart
                    chartContagem.Series["Series1"]["PieStartAngle"] = "270"; //Anglo do chart
                    chartContagem.Series["Series1"].IsValueShownAsLabel = true; //exibe os dados chart                
                    chartContagem.Series["Series1"].IsVisibleInLegend = false; //Não exibe as legendas

                    chartContagem.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0; //Remove a linhas horizontais
                    chartContagem.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0; //Remove as linhas verticais

                    chartContagem.ChartAreas[0].AxisX.Interval = 1; //Permite exiber todos as descrições da estação

                    int? total = 0;

                    foreach (var item in itensCollection.OrderBy(n => n.usuarioTerceira))
                    {
                        //Dados do gráfico
                        chartContagem.Series["Series1"].Points.AddXY(item.usuarioTerceira, item.contTerceira);

                        total += item.contTerceira;
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        //Pesquisa acuricidade
        private void PesqAcuricidade()
        {
            try
            {
                Invoke((MethodInvoker)delegate ()
                {
                    double qtdPulmao = Convert.ToDouble(lblContPulmao.Text);
                    double qtdPicking = Convert.ToDouble(lblContPicking.Text);

                    if (qtdPulmao > 0)
                    {
                        //Instância a camada de negocios
                        DashInventarioNegocios inventarioNegocios = new DashInventarioNegocios();
                        dados = inventarioNegocios.PesqAcuricidade(codInventario);

                        //Exibe a acuricidade do pulmão
                        lblAcuricidadePulmao.Text = string.Format("{0:n}", ((qtdPulmao - dados[0]) / qtdPulmao) * 100) + "%";

                        if (qtdPicking > 0)
                        {
                            //Exibe a curicidade do picking
                            lblAcuricidadePicking.Text = string.Format("{0:n}", ((qtdPicking - dados[1]) / qtdPicking) * 100) + "%";
                        }
                        //Exibe a acuricidade média
                        lblAcuricidadeMedia.Text = string.Format("{0:n}", (((qtdPulmao + qtdPicking) - (dados[0] + dados[1])) / (qtdPulmao + qtdPicking)) * 100) + "%";
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Pesquisa a quanditade de produtos
        private void PesqProdutos()
        {
            try
            {
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância a camada de negocios
                    DashInventarioNegocios inventarioNegocios = new DashInventarioNegocios();
                    dados = inventarioNegocios.PesqProdutos(codInventario);

                    //Exibe a acuricidade do pulmão
                    lblTotalProdutos.Text = dados[0].ToString();
                    //Exibe a curicidade do picking
                    lblProdutosFinalizados.Text = (dados[0] - dados[1]) + " Produtos";
                    //Exibe a acuricidade média
                    lblProdutosPendentes.Text = dados[1] + " Produtos";

                    //Adiciona o valor
                    progressBarProdutos.Maximum = Convert.ToInt32(dados[0]);
                    progressBarProdutos.Value = Convert.ToInt32(dados[0] - dados[1]);
                    //Exibe o texto
                    progressBarProdutos.Text = Convert.ToInt32(((dados[0] - dados[1]) / dados[0]) * 100) + "%";
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

       
    }
}
