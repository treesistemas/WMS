using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using ObjetoTransferencia;
using Negocios;
using Wms.Impressao;
using Wms.Movimentacao;
using Wms.Inventario;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.IO;
using System.Linq;
using Wms.Integracao;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Wms.Cadastro;
using Wms.Expedicao;

namespace Wms
{
    public partial class FrmMenu : Form
    {
        //Controle para mover a tela
        public const int WM_NCLBUTTONDOWN = 0XA1;
        public const int HT_CAPITION = 0X2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private MdiClient mdi;
        public static int idUsuario;
        public static int inventario;


        public string nomeEmpresa;
        public int codUsuario;
        public string perfil;
        public string loginUsuario;
        public string controlaSequenciaCarregamento;
        public int codEstacao;
        public int nivelEstacao;
        public string impressora;
        public string versao;
        //Vertor de imagens
        public byte[] vetorImagens;
        //public static int codEstacao = Convert.ToInt32(codEstacao);
        //public string descEstacao = "Estação Fixa " + codEstacao.ToString();

        //Controle de acessos

        public AcessoCollection controleAcesso = new AcessoCollection();
        public EmpresaCollection empresaCollection = new EmpresaCollection();


        public FrmMenu()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            ControlaAcesso(); //Pesquisa o acesso

            lblLogin.Text = loginUsuario; //Login do usuário
            lblPerfil.Text = perfil; //Perfíl do usuário        

            DateTime data = DateTime.Now; //Instância o objêto
            lblAcesso.Text = data.ToString("G"); //Data e hora de acesso

            //Altera a cor do formulario principal (MDI)
            foreach (Control ctl in this.Controls)
            {
                if (ctl.GetType().Name == "MdiClient")
                {
                    ctl.BackColor = Color.FromName("white");                   

                }
            }

             

            //Adiciona a tela principal
            lblVersao.Text = "Wms: " + versao;
            lblLicenca.Text = "Licenciado para: " + nomeEmpresa;
            lblCaminho.Text = "Caminho: " + System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            //calcula o tamanho total da pasta e subpastas
            DirectoryInfo infoDiretorio = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            long tamanhoDoDiretorio = TamanhoTotalDiretorio(infoDiretorio, true);
            lblTamanho.Text = "Tamanho: " + FormataExibicaoTamanhoArquivo(tamanhoDoDiretorio);
            
            //Quantidade de arquivo
            int qtdArquivo = 0;
            //exibe os arquivos no ListBox
            foreach (string f in Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory.ToString()))
            {
                qtdArquivo++;
            }

            lblArquivos.Text = "Total de arquivos: "+qtdArquivo;

            //Verifica se existe a imagem
            if (vetorImagens == null)
            {
                //Limpa a imagem
                picFoto.Image = null;
            }
            else
            {
                //exibe a imagem
                string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                fs.Write(vetorImagens, 0, vetorImagens.Length);
                fs.Flush();
                fs.Close();
                picFoto.Image = Image.FromFile(strNomeArquivo);
            }

            //Altera a imagem

            //this.BackgroundImageLayout = ImageLayout.None;
            //usando - this.BackgroundImageLayout = ImageLayout.Center;
            //this.BackgroundImageLayout = ImageLayout.Stretch;
            // this.BackgroundImageLayout = ImageLayout.Tile;
            //this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        //cultar o rótulo enquanto você tiver filhos MDI ativos e mostrá-lo novamente quando não houver mais nenhum filho ativo
        private void FrmMenu_MdiChildActivate(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                lblTreeSistemas.SendToBack();
                lblVersao.SendToBack();
                lblLicenca.SendToBack();
                lblCaminho.SendToBack();
                lblArquivos.SendToBack();       
                lblTamanho.SendToBack();
                picFoto.SendToBack();
                lblLogin1.SendToBack();
                lblLogin.SendToBack();
                lblPerfil1.SendToBack();
                lblPerfil.SendToBack();
                lblAcesso1.SendToBack();
                lblAcesso.SendToBack();
            }
            else
            {
                lblTreeSistemas.BringToFront();
                lblVersao.BringToFront();
                lblLicenca.BringToFront();
                lblCaminho.BringToFront();
                lblArquivos.BringToFront();
                lblTamanho.BringToFront();
                picFoto.BringToFront();
                lblLogin1.BringToFront();
                lblLogin.BringToFront();
                lblPerfil1.BringToFront();
                lblPerfil.BringToFront();
                lblAcesso1.BringToFront();
                lblAcesso.BringToFront();
            }
        }

        private void pnlCabecalho_MouseMove(object sender, MouseEventArgs e)
        {
            //Controla o movimento do frame
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPITION, 0);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você deseja finalizar aplicação ?", "WMS - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Encerra aplicação                    
                Application.ExitThread();
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            //Restaura a tela
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            //Restaura a tela
            this.WindowState = FormWindowState.Maximized;
            btnRestaurar.Visible = true;
            btnMaximizar.Visible = false;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            //Minimiza a tela
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if(mnuMenu.Visible == false)
            {
                label2.Visible = true;
                lblRotina.Visible = true;
                txtRotina.Visible = true;
                pictureBox1.Visible = true;
                mnuMenu.Visible = true;
            }
            else
            {
                label2.Visible = false;
                lblRotina.Visible = false;
                txtRotina.Visible = false;
                pictureBox1.Visible = false;
                mnuMenu.Visible = false;
            }
        }

        //Controle a saida do sistema
        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Você deseja finalizar aplicação ?", "WMS - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                //permanece no formulario
                e.Cancel = true;
            }
            else
            {
                //encerro a aplicação                    
                Application.ExitThread();
            }

        }


        //Teclas de atalho
        private void FrmMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                //Instância o form produto
                FrmProduto frame = new FrmProduto();
                //código do usuário
                frame.codUsuario = this.codUsuario;
                //Perfíl do usuário
                frame.perfilUsuario = perfil;
                //Localizando o acesso
                frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "PRODUTO"; });
                //Exibe o frame
                ExibirFrame(frame, "FrmProduto");
            }

            //Frame 
            if (e.KeyCode == Keys.F6)
            {
                //Verifica se o Form esta aberto
                if (Application.OpenForms["FrmTrasferencia"] == null)
                {
                    //Instância o form tranferência de endereços
                    FrmTrasferenciaProduto frame = new FrmTrasferenciaProduto();
                    frame.codUsuario = this.codUsuario;
                    //Perfíl do usuário
                    frame.perfilUsuario = perfil;
                    //Localizando o acesso
                    frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "TRANSFERÊNCIA DE PRODUTO"; });
                    //Coleção de empresa
                    frame.empresaCollection = empresaCollection;
                    //Adiciona a tela principal
                    frame.MdiParent = this;
                    //Exibe o frame
                    frame.Show();
                }
                else
                {
                    //Traz o form para a frente
                    Application.OpenForms["FrmTrasferencia"].BringToFront();
                }

            }

            //Frame de impressão por manifesto
            if (e.KeyCode == Keys.F11)
            {
                //Instância o form 
                FrmImpressaoManifesto frame = new FrmImpressaoManifesto();
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;
                //Adiciona o código do usuário
                frame.codUsuario = this.codUsuario;
                //Exibe o frame
                frame.Show();
            }


        }

        private void mnuPainelControle_Click(object sender, EventArgs e)
        {
            //Instância o form
            FrmDashOperacional frame = new FrmDashOperacional();
            //Exibe o frame
            ExibirFrame(frame, "FrmPainel");
        }

        //Controla o acesso do usuário
        private void ControlaAcesso()
        {
            //Se não for administrador
            if (controleAcesso.Count > 0)
            {
                if (perfil.Trim() != "ADMINISTRADOR")
                {
                    //Verifica os acessos as funções
                    for (int i = 0; controleAcesso.Count > i; i++)
                    {
                        #region 01 - Menu Cadastro
                        //Verifica o acesso do menu cadastro
                        if (controleAcesso[i].descFuncao.Trim() == "01 - CADASTRO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu cadastro
                            mnuCadastro.Visible = false;
                        }

                        //Verifica o acesso do menu cadastro de categoria
                        if (controleAcesso[i].descFuncao.Trim() == "101 - CATEGORIA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu controle de categoria
                            mniCategoria.Visible = false;
                        }

                        //Verifica o acesso do menu caixa
                        if (controleAcesso[i].descFuncao.Trim() == "102 - CAIXA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu caixa
                            mniCaixa.Visible = false;
                        }

                        //Verifica o acesso do menu cliente
                        if (controleAcesso[i].descFuncao.Trim() == "103 - CLIENTE" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu cliente
                            mniCliente.Visible = false;
                        }

                        //Verifica o acesso do menu empilhador
                        if (controleAcesso[i].descFuncao.Trim() == "104 - EMPILHADOR" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu empilhador
                            mniEmpilhador.Visible = false;
                        }

                        //Verifica o acesso do menu estrutura
                        if (controleAcesso[i].descFuncao.Trim() == "105 - ESTRUTURA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu estrutura
                            mniEstrutura.Visible = false;
                        }

                        //Verifica o acesso do menu estação
                        if (controleAcesso[i].descFuncao.Trim() == "106 - ESTAÇÃO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu estação
                            mniEstacao.Visible = false;
                        }

                        //Verifica o acesso do menu fornecedor
                        if (controleAcesso[i].descFuncao.Trim() == "107 - FORNECEDOR" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu fornecedor
                            mniFornecedor.Visible = false;
                        }

                        //Verifica o acesso do menu motorista
                        if (controleAcesso[i].descFuncao.Trim() == "108 - MOTORISTA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu motorista
                            mniMotorista.Visible = false;
                        }

                        //Verifica o acesso do menu perfíl
                        if (controleAcesso[i].descFuncao.Trim() == "109 - PERFÍL" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu perfíl
                            mniPerfil.Visible = false;
                        }

                        //Verifica o acesso do menu produto
                        if (controleAcesso[i].descFuncao.Trim() == "110 - PRODUTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu produto
                            mniProduto.Visible = false;
                        }

                        //Verifica o acesso do menu protátil
                        if (controleAcesso[i].descFuncao.Trim() == "111 - RASTREADOR PORTÁTIL" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu protátil
                            mniRastreador.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "112 - RASTREAMENTO DE CADASTRO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniRastreamentoCadastro.Visible = false;
                        }

                        //Verifica o acesso do menu restrição de categorias
                        if (controleAcesso[i].descFuncao.Trim() == "113 - RESTRIÇÃO DE CATEGORIAS" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu restrição de categorias
                            mniRestricaoCategoria.Visible = false;
                        }

                        //Verifica o acesso do menu rotas
                        if (controleAcesso[i].descFuncao.Trim() == "114 - ROTAS" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu rotas
                            mniRota.Visible = false;
                        }

                        //Verifica o acesso do menu tipo ocorrência
                        if (controleAcesso[i].descFuncao.Trim() == "115 - TIPO OCORRÊNCIA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu tipo ocorrência
                            mniTipoOcorrencia.Visible = false;
                        }

                        //Verifica o acesso do menu tipo rota
                        if (controleAcesso[i].descFuncao.Trim() == "116 - TIPO ROTA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu tipo rota
                            mniTipoRota.Visible = false;
                        }

                        //Verifica o acesso do menu tipo veículo
                        if (controleAcesso[i].descFuncao.Trim() == "117 - TIPO DE VEÍCULO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu tipo veículo
                            mniTipoVeiculo.Visible = false;
                        }

                        //Verifica o acesso do menu unidade
                        if (controleAcesso[i].descFuncao.Trim() == "118 - UNIDADE" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu tipo unidade
                            mniUnidade.Visible = false;
                        }

                        //Verifica o acesso do menu usuário
                        if (controleAcesso[i].descFuncao.Trim() == "119 - USUÁRIO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu usuário
                            mniUsuario.Visible = false;
                        }

                        //Verifica o acesso do menu veículo
                        if (controleAcesso[i].descFuncao.Trim() == "120 - VEÍCULO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu veículo
                            mniVeiculo.Visible = false;
                        }

                        //Verifica o acesso do menu senha
                        if (controleAcesso[i].descFuncao.Trim() == "121 - SENHA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu senha
                            mniSenha.Visible = false; 
                        }

                        //Verifica o acesso do menu valor
                        if (controleAcesso[i].descFuncao.Trim() == "122 - VALOR" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu senha
                            mniValor.Visible = false;
                        }

                        #endregion

                        #region 02 - Menu Portaria
                        //Verifica o acesso do menu monitoramento
                        if (controleAcesso[i].descFuncao.Trim() == "02 - PORTARIA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mnuPortaria.Visible = false;
                        }
                        #endregion

                        #region 03 - Menu Auditoria
                        //Verifica o acesso do menu monitoramento
                        if (controleAcesso[i].descFuncao.Trim() == "03 - AUDITORIA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mnuAuditoria.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "301 - AUDITORIA DE PRODUTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniAuditoriaProduto.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "302 - AUDITORIA DE OCORRÊNCIA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniAuditoriaOcorrencia.Visible = false;
                        }

                        #endregion

                        #region 04 - Menu Estoque
                        //Verifica o acesso do menu monitoramento
                        if (controleAcesso[i].descFuncao.Trim() == "04 - ESTOQUE" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mnuEstoque.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "401 - ARMAZENAMENTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniArmazenamento.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "402 - ABASTECIMENTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniOrdemAbastecimento.Visible = false;
                        }

                        //Verifica o acesso do menu endereçamento
                        if (controleAcesso[i].descFuncao.Trim() == "403 - ENDEREÇAMENTO DE PRODUTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu endereçamento
                            mniEnderecamento.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "404 - DE PARA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniDePara.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "405 - CONSULTA DE ESTOQUE" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniEstoque.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "407 - RESERVA DE PRODUTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniReservaProdutos.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "408 - TRANSFERÊNCIA DE PRODUTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniTransferencia.Visible = false;
                        }

                        #endregion

                        #region 05 - Menu Expedição

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "05 - EXPEDIÇÃO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mnuExpedicao.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "501 - ALTERAR VOLUME DE FLOW RACK" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniAlterarVolumeFlowRack.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "502 - CONFERENÊNCIA AUTOMÁTICA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniConferenciaAutomatica.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "503 - CONFERÊNCIA DA NOTA CEGA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniNotaCega.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "504 - CONFERÊNCIA DE FLOW RACK" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniConfereciaFlow.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "505 - CONFERÊNCIA POR MANIFESTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniManifesto.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "506 - CONFERÊNCIA DE PEDIDO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniPedido.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "507 - MONITOR DE PEDIDOS" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniMonitorPedido.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "508 - PROCESSAMENTO DE FLOW RACK" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniProcessamentoFlow.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "509 - RASTREAMENTO DE OPERAÇÕES" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniRastreamentoOperacoes.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "510 - INTEGRAÇÃO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniIntegracao.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "511 - ROTEIRIZADOR" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniRoteirizador.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "512 - CHECK NOW" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniCheckNow.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "513 - SAIDA MANIFESTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniSaidaManifesto.Visible = false;
                        }


                        #endregion

                        #region 06 - Menu Recebimento
                        //Verifica o acesso do menu monitoramento
                        if (controleAcesso[i].descFuncao.Trim() == "06 - RECEBIMENTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mnuRecebimento.Visible = false;
                        }

                        //Verifica o acesso do menu movimentação
                        if (controleAcesso[i].descFuncao.Trim() == "601 - NOTA ENTRADA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mniNotaEntrada.Visible = false;
                        }

                        #endregion

                        #region 07 - Menu Monitoramento
                        //Verifica o acesso do menu monitoramento
                        if (controleAcesso[i].descFuncao.Trim() == "07 - MONITORAMENTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu movimentação
                            mnuMonitoramento.Visible = false;
                        }

                        //Verifica o acesso do menu veículo
                        if (controleAcesso[i].descFuncao.Trim() == "701 - OCORRÊNCIA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu veículo
                            mniOcorrencia.Visible = false;
                        }

                        #endregion

                        #region 08 - Menu DashBoard

                        //Verifica o acesso do menu gerenciamento
                        if (controleAcesso[i].descFuncao.Trim() == "08 - DASHBOARD" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mnuDashBoard.Visible = false;
                        }

                        //Verifica o acesso do menu gerenciamento
                        if (controleAcesso[i].descFuncao.Trim() == "801 - DASHBOARD FLOW RACK" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniDashFlowRack.Visible = false;
                        }

                        //Verifica o acesso do menu gerenciamento
                        if (controleAcesso[i].descFuncao.Trim() == "802 - DASHBOARD EXPEDIÇÃO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniDashExpedicao.Visible = false;
                        }

                        //Verifica o acesso do menu inventário
                        if (controleAcesso[i].descFuncao.Trim() == "803 - DASHBOARD INVENTÁRIO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu inventário
                            mniDashInventario.Visible = false;
                        }

                        #endregion

                        #region 09 - Menu Inventário
                        //Verifica o acesso do menu inventário
                        if (controleAcesso[i].descFuncao.Trim() == "09 - INVENTÁRIO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu inventário
                            mnuInventario.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "901 - ABRIR INVENTÁRIO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu inventário
                            mniAbrirInventario.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "902 - DIGITAÇÃO DAS CONTAGENS" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu inventário
                            mniDigitacaoContagens.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "903 - RELATÓRIO DAS CONTAGENS" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu inventário
                            mniRelatorioContagem.Visible = false;
                        }

                        #endregion

                        #region 10 - Menu Oficina

                        //Verifica o acesso do menu gerenciamento
                        if (controleAcesso[i].descFuncao.Trim() == "10 - OFICINA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mnuOficina.Visible = false;
                        }

                        #endregion

                        #region 11 - Menu Relatório
                        //Verifica o acesso do menu gerenciamento
                        if (controleAcesso[i].descFuncao.Trim() == "11 - RELATÓRIO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mnuRelatorio.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1101 - ESTOQUE VS PICKING" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniEstoquexPicking.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1102 - IMPRESSÃO DE ETIQUETA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniImpressaoEtiqueta.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1103 - MAPA DE SEPARAÇÃO POR MANIFESTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniMapaSeparacao.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1104 - PENDÊNCIA DE ENTREGA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniPendencia.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1105 - PICKING NEGATIVO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniPickingNegativo.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1106 - PICKING ACIMA DA CAPACIDADE" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniAcimaCapacidade.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1107 - PRODUTO NO FLOW RACK" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniProdutoFlowRack.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1108 - RENDIMENTO DA EXPEDIÇÃO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniRendimentoExpedicao.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1109 - VENCIMENTO DE PRODUTO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniVencimento.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1110 - WMS VS ERP" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniWmsxErp.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1111 - CURVA ABC" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu gerenciamento
                            mniCurvaABC.Visible = false;
                        }

                        #endregion

                        #region 12 - Menu Configuração
                        //Verifica o acesso do menu configuração
                        if (controleAcesso[i].descFuncao.Trim() == "12 - CONFIGURAÇÃO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu configuração
                            mnuConfiguracao.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1201 - SISTEMA" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu configuração
                            mnuConfiguracao.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1202 - ATUALIZAR CHAVE (PÓS IMPORTAÇÃO)" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu configuração
                            mnuAtualizarChaves.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1203 - GERAR NÍVEL ZERO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu configuração
                            mnuGerarNivelZero.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1204 - ATUALIZAR PESO" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu configuração
                            mnuAtualizarPeso.Visible = false;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "1205 - ZERAR PICKING SEM ESTOQUE" && controleAcesso[i].lerFuncao == false)
                        {
                            //Oculta o menu configuração
                            mnuZerarPickingSemEstoque.Visible = false;
                        }

                        #endregion


                    }

                    //Verifica os acessos as funções
                    for (int i = 0; controleAcesso.Count > i; i++)
                    {
                        if (controleAcesso[i].descFuncao.Trim() == "01 - CADASTRO" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuCadastro.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "02 - PORTARIA" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuPortaria.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "03 - AUDITORIA" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuAuditoria.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "04 - ESTOQUE" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuEstoque.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "05 - EXPEDIÇÃO" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuExpedicao.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "06 - RECEBIMENTO" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuRecebimento.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "07 - MONITORAMENTO" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuMonitoramento.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "08 - DASHBOARD" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuDashBoard.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "09 - INVENTÁRIO" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuInventario.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "10 - OFICINA" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuOficina.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "11 - RELATÓRIO" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuRelatorio.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "12 - CONFIGURAÇÃO" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuConfiguracao.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }

                        if (controleAcesso[i].descFuncao.Trim() == "13 - AJUDA" && controleAcesso[i].lerFuncao == true)
                        {
                            mnuAjuda.Margin = new Padding(0, 180, 0, 10);

                            break;
                        }
                    }
                }
            }
        }

        #region Menu Cadastro
        private void mniControleCategoria_Click(object sender, EventArgs e)
        {
            //Instância o form categoria
            FrmCategoria frame = new FrmCategoria();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "101 - CATEGORIA"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmCategoria");

        }

        private void mniCaixa_Click(object sender, EventArgs e)
        {
            //Instância o form caixa
            FrmCaixa frame = new FrmCaixa();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "102 - CAIXA"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmCaixa");
        }

        private void mniCliente_Click(object sender, EventArgs e)
        {
            //Instância o form ciente
            FrmCliente frame = new FrmCliente();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "103 - CLIENTE"; });
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmCliente");
        }

        private void mniEmpilhador_Click(object sender, EventArgs e)
        {
            //Instância o form empilhador
            FrmEmpilhador frame = new FrmEmpilhador();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "104 - EMPILHADOR"; });
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmEmpilhador");
        }

        private void mniEstrutura_Click(object sender, EventArgs e)
        {
            //Instância o form estrutura
            FrmEstrutura frame = new FrmEstrutura();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "105 - ESTRUTURA"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmEstrutura");
        }

        private void mniEstacao_Click(object sender, EventArgs e)
        {
            //Instância o form estação
            FrmEstacao frame = new FrmEstacao();
            //código do usuário
            frame.codUsuario = this.codUsuario;
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "106 - ESTAÇÃO"; });
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmEstacao");
        }

        private void mniFornecedor_Click(object sender, EventArgs e)
        {
            //Instância o form fornecedor
            FrmFornecedor frame = new FrmFornecedor();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.empresaCollection = empresaCollection;
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "107 - FORNECEDOR"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmFornecedor");
        }

        private void mniMotorista_Click(object sender, EventArgs e)
        {
            //Instância o form Motorista
            FrmMotorista frame = new FrmMotorista();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "108 - MOTORISTA"; });
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmMotorista");
        }

        private void mniPerfil_Click(object sender, EventArgs e)
        {
            //Instância o form perfíl
            FrmPerfil frame = new FrmPerfil();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "109 - PERFÍL"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmPerfil");
        }

        private void mniProduto_Click(object sender, EventArgs e)
        {
            //Instância o form produto
            FrmProduto frame = new FrmProduto();
            //código do usuário
            frame.codUsuario = this.codUsuario;
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "110 - PRODUTO"; });
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmProduto");

        }

        private void mniRastreador_Click(object sender, EventArgs e)
        {
            //Instância o form 
            FrmRastreador frame = new FrmRastreador();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "111 - RASTREADOR PORTÁTIL"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmRastreador");
        }

        private void mniRastreamentoCadastro_Click(object sender, EventArgs e)
        {
            //Instância o form
            FrmRastreamentoCadastro frame = new FrmRastreamentoCadastro();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "112 - RASTREAMENTO DE CADASTRO"; });
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmRastreamentoCadastro");
        }

        private void mniRestricaoCategoria_Click(object sender, EventArgs e)
        {
            //Instância o form restrição
            FrmCategoriaRestricao frame = new FrmCategoriaRestricao();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "113 - RESTRIÇÃO DE CATEGORIAS"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmCategoriaRestricao");
        }

        private void mniRota_Click(object sender, EventArgs e)
        {
            //Instância o form 
            FrmRotas frame = new FrmRotas();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "114 - ROTAS"; });
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmRotas");
        }

        private void mniTipoOcorrencia_Click(object sender, EventArgs e)
        {
            //Instância o form 
            FrmTipoOcorrencia frame = new FrmTipoOcorrencia();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "115 - TIPO OCORRÊNCIA"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmTipoOcorrencia");
        }

        private void mniTipoRota_Click(object sender, EventArgs e)
        {
            //Instância o form 
            FrmTipoRota frame = new FrmTipoRota();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "116 - TIPO ROTA"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmTipoRota");
        }

        private void mniTipoVeiculo_Click(object sender, EventArgs e)
        {
            //Instância o form 
            FrmVeiculoTipo frame = new FrmVeiculoTipo();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "117 - TIPO DE VEÍCULO"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmVeiculoTipo");

        }

        private void mniUnidade_Click(object sender, EventArgs e)
        {
            //Instância o form unidade
            FrmUnidade frame = new FrmUnidade();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "118 - UNIDADE"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmUnidade");
        }

        private void mniUsuario_Click(object sender, EventArgs e)
        {
            //Instância o form usuario
            FrmUsuario frame = new FrmUsuario();
            //frame.TopLevel = false;
            //frame.FormBorderStyle = FormBorderStyle.None;
            //frame.Dock = DockStyle.Fill;
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "119 - USUÁRIO"; });
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmUsuario");
        }

        private void mniVeiculo_Click(object sender, EventArgs e)
        {
            //Instância o form 
            FrmVeiculo frame = new FrmVeiculo();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "120 - VEÍCULO"; });
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmVeiculo");
        }

        private void mniSenha_Click(object sender, EventArgs e)
        {
            //Instância o form senha
            FrmSenha frame = new FrmSenha();
            frame.idUsuario = codUsuario;
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "121 - SENHA"; });
            //Exibe o frame
            frame.ShowDialog();
        }

        private void mniValor_Click(object sender, EventArgs e)
        {
            //Instância o form
            FrmValorMotorista frame = new FrmValorMotorista();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "122 - VALOR"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmValorMotorista");
        }


        private void mnuPortaria_Click(object sender, EventArgs e)
        {
            /*
            //Instância o form categoria
            FrmMonitorPedido frame = new FrmMonitorPedido();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "201 - CADASTRO"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmMonitorPedido");
            */
        }

        #endregion

        #region Menu Auditoria
        private void mniAuditoria_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            //Instância o frame
            FrmAuditoria frame = new FrmAuditoria();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "301 - AUDITORIA DE PRODUTO"; });
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;

            frame.codUsuario = this.codUsuario;
            //Adiciona a tela principal
            frame.MdiParent = this;
            //Exibe o frame
            frame.Show();
            /*
            if (frm.Visible)
            {
                //panel2.Controls.Clear();
                frm.Close();
                frm = new FrmAuditoria();
                frm.TopLevel = false;
                //panel2.Controls.Add(frm);
                frm.Show();
            }
            else
            {
                panel2.Controls.Clear();
                frm = new FrmAuditoria();
                frm.TopLevel = false;
                panel2.Controls.Add(frm);
                frm.Show();
            }*/
        }

        private void mniOcorrenciaAuditoria_Click(object sender, EventArgs e)
        {
            //Instância o form gerar nota entrada
            FrmAuditoriaOcorrencia frame = new FrmAuditoriaOcorrencia();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "302 - AUDITORIA DE OCORRÊNCIA"; });
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;

            frame.codUsuario = this.codUsuario;
            //Adiciona a tela principal
            frame.MdiParent = this;
            //Exibe o frame
            frame.Show();
        }

        #endregion

        #region Menu Estoque

        private void mniArmazenamento_Click(object sender, EventArgs e)
        {
            //Instância o frama
            FrmArmazenamento frame = new FrmArmazenamento();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "401 - ARMAZENAMENTO"; });
            //Passa o código do usuário
            frame.codUsuario = codUsuario;
            frame.impressora = impressora;
            frame.empresa = nomeEmpresa;
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmArmazenamento");
        }

        private void mniOrdemAbastecimento_Click(object sender, EventArgs e)
        {
            //Instância o form gerar nota entrada
            FrmAbastecimento frame = new FrmAbastecimento();
            frame.codUsuario = this.codUsuario;
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "402 - ABASTECIMENTO"; });
            frame.empresaCollection = empresaCollection;
            //Adiciona a tela principal
            frame.MdiParent = this;
            //Exibe o frame
            frame.Show();
        }

        private void mniEnderecamentoSKU_Click(object sender, EventArgs e)
        {
            //Instância o form empilhador
            //public AcessoCollection controleAcesso = new AcessoCollection();
        FrmEnderecamentoPicking frame = new FrmEnderecamentoPicking();
            //código do usuário
            frame.codUsuario = this.codUsuario;
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "403 - ENDEREÇAMENTO DE PRODUTO"; });
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmEnderecamento");

        }

        private void mniDePara_Click(object sender, EventArgs e)
        {

            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmDePara"] == null)
            {
                //Instância o form 
                FrmDePara frame = new FrmDePara();
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmDePara"].BringToFront();
            }
        }

        private void mniEstoque_Click(object sender, EventArgs e)
        {
            //Instância o form consulta de Estoque
            FrmEstoque frame = new FrmEstoque();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "405 - CONSULTA DE ESTOQUE"; });
            //Passa o código do usuário
            frame.codUsuario = codUsuario;
            frame.impressora = impressora;
            frame.empresa = nomeEmpresa;
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmEstoque");
        }

        private void mniReservaProdutos_Click(object sender, EventArgs e)
        {
            //Instância o form
            FrmReserva frame = new FrmReserva();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "407 - RESERVA DE PRODUTO"; });
            //Passa o código do usuario
            frame.codUsuario = this.codUsuario;
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmReserva");
        }

        private void mniTransferencia_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmTrasferencia"] == null)
            {
                //Instância o form tranferência de endereços
                FrmTrasferenciaProduto frame = new FrmTrasferenciaProduto();
                frame.codUsuario = this.codUsuario;
                //Perfíl do usuário
                frame.perfilUsuario = perfil;
                //Localizando o acesso
                frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "408 - TRANSFERÊNCIA DE PRODUTO"; });
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmTrasferencia"].BringToFront();
            }
        }

        #endregion

        #region Menu Expedicao

        private void mniAlterarVolumeFlowRack_Click(object sender, EventArgs e)
        {
            //Instância o form gerar nota entrada
            FrmAlterarVolume frame = new FrmAlterarVolume();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Nome da Empresa
            frame.empresa = nomeEmpresa;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "501 - ALTERAR VOLUME DE FLOW RACK"; });
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;
            //Adiciona a tela principal
            frame.MdiParent = this;
            //Exibe o frame
            frame.Show();
        }

        private void mniConferenciaAutomatica_Click(object sender, EventArgs e)
        {
            //Instância o form conferecia automatica
            FrmConferenciaAutomatica frame = new FrmConferenciaAutomatica();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "502 - CONFERENÊNCIA AUTOMÁTICA"; });
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;
            //Passa o código do usuário
            frame.codUsuario = codUsuario;
            //Exibe o frame
            frame.ShowDialog();
        }

        private void mniNotaCega_Click(object sender, EventArgs e)
        {
            //Instância o form conferência cega
            FrmConferenciaCega frame = new FrmConferenciaCega();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "503 - CONFERÊNCIA DA NOTA CEGA"; });
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;
            //Passa o código do usuario
            frame.codUsuario = this.codUsuario;
            //Exibe o frame
            ExibirFrame(frame, "FrmConferenciaCega");
        }

        private void mniConfereciaFlow_Click(object sender, EventArgs e)
        {
            //Instância o form conferência de manifesto
            FrmConferenciaFlowRack frame = new FrmConferenciaFlowRack();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "504 - CONFERÊNCIA DE FLOW RACK"; });
            //Passa o código do usuário
            frame.codUsuario = codUsuario;
            //Passa o código da estação
            frame.codEstacao = codEstacao;
            //Passa a descrição da estação
            frame.descEstacao = "ESTAÇÃO " + codEstacao;
            //Passa o nível da estação
            frame.nivelEstacao = nivelEstacao;
            frame.empresa = nomeEmpresa;
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            frame.Show();
        }

        private void mniManifesto_Click(object sender, EventArgs e)
        {
            //Instância o form conferência cega
            FrmConferenciaManifesto frame = new FrmConferenciaManifesto();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "505 - CONFERÊNCIA POR MANIFESTO"; });
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;
            //Passa o código do usuario
            frame.codUsuario = this.codUsuario;
            //Exibe o frame
            ExibirFrame(frame, "FrmConferenciaManifesto");
        }

        private void mniPedido_Click(object sender, EventArgs e)
        {
            //Instância o form conferência de pedido
            FrmConferencia frame = new FrmConferencia();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "506 - CONFERÊNCIA DE PEDIDO"; });
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;
            //Passa o código do usuario
            frame.codUsuario = this.codUsuario;
            //Exibe o frame
            ExibirFrame(frame, "FrmConferencia");
        }

        private void mniMonitorPedido_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmMonitorPedido"] == null)
            {
                //Instância o form monitor de pedido
                FrmMonitorPedido frame = new FrmMonitorPedido();
                //Perfíl do usuário
                frame.perfilUsuario = perfil;
                //Localizando o acesso
                frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "507 - MONITOR DE PEDIDOS"; });
                //Passa a coleção de empresas
                frame.empresaCollection = empresaCollection;
                //Passa a impressora
                frame.impressora = impressora;
                //Código do usuário
                frame.codUsuario = this.codUsuario;
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmMonitorPedido"].BringToFront();
            }
        }

        private void mniProcessamentoFlow_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmProcessamentoFlow"] == null)
            {
                //Instância o form monitor de pedido
                FrmProcessamentoFlow frame = new FrmProcessamentoFlow();
                //Perfíl do usuário
                frame.perfilUsuario = perfil;
                //Localizando o acesso
                frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "508 - PROCESSAMENTO DE FLOW RACK"; });
                frame.empresaCollection = empresaCollection;
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmProcessamentoFlow"].BringToFront();
            }
        }

        private void mniRastreamentoOperacoes_Click(object sender, EventArgs e)
        {
            //Instância o form
            FrmRastreamentoOperacoes frame = new FrmRastreamentoOperacoes();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "509 - RASTREAMENTO DE OPERAÇÕES"; });
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;

            //Exibe o frame
            ExibirFrame(frame, "FrmRastreamentoOperacoes");
        }

        private void mniIntegracaoRoteirizador_Click(object sender, EventArgs e)
        {
            //Instância o form
            FrmRoteirizador frame = new FrmRoteirizador();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "511 - ROTEIRIZADOR"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmRoteirizador");
        }

        private void mniSaidaManifesto_Click(object sender, EventArgs e)
        {
            //Instância o form
            FrmResumoSaida frame = new FrmResumoSaida();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "513 - SAIDA MANIFESTO"; });
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmResumoSaida");

        }

        private void mniCheckNow_Click(object sender, EventArgs e)
        {
            //Instância o form
            FrmExportacaoCheckNow frame = new FrmExportacaoCheckNow();
            //Perfíl do usuário
            frame.perfilUsuario = perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "512 - CHECK NOW"; });
            //Exibe o frame
            ExibirFrame(frame, "FrmExportacaoCheckNow");
        }

        /*
        private void mniPortatil_Click(object sender, EventArgs e)
        {
            

        }
        */

        #endregion

        #region Menu Recebimento

        private void mniNotaEntrada_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmNotaEntrada"] == null)
            {
                //Instância o form gerar nota entrada
                FrmNotaEntrada frame = new FrmNotaEntrada();
                //Perfíl do usuário
                frame.perfilUsuario = perfil;
                //Localizando o acesso
                frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "601 - NOTA ENTRADA"; });
                //Passa a coleção de empresas
                frame.empresaCollection = empresaCollection;
                frame.codUsuario = this.codUsuario;
                frame.loginUsuario = this.loginUsuario;
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmNotaEntrada"].BringToFront();
            }
        }

        #endregion

        #region Menu Monitoramento
        private void mniOcorrencia_Click(object sender, EventArgs e)
        {
            //Instância o form ocorrência
            FrmOcorrencia frame = new FrmOcorrencia();
            //código do usuário
            frame.nomeEmpresa = this.nomeEmpresa;
            frame.codUsuario = this.codUsuario;
            frame.loginUsuario = this.loginUsuario;
            //Perfíl do usuário
            frame.perfilUsuario = this.perfil;
            //Localizando o acesso
            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "701 - OCORRÊNCIA"; });
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            ExibirFrame(frame, "FrmOcorrencia");
        }
        #endregion

        #region Menu Dashboard

        private void mniDashFlowRack_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmDashFlowRack"] == null)
            {
                //Instância o form 
                FrmDashFlowRack frame = new FrmDashFlowRack();
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmDashFlowRack"].BringToFront();
            }
        }

        private void mniDashExpedicao_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmDashboardExpedicao"] == null)
            {
                //Instância o form 
                FrmDashboardExpedicao frame = new FrmDashboardExpedicao();
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmDashboardExpedicao"].BringToFront();
            }
        }

        private void mniDashInventario_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmDashboardInventario"] == null)
            {
                //Instância o form 
                FrmDashboardInventario frame = new FrmDashboardInventario();
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmDashboardInventario"].BringToFront();
            }
        }


        #endregion

        #region Menu Inventário
        private void mniAbrirInventario_Click(object sender, EventArgs e)
        {

            //Instância o form monitor de pedido
            FrmInventario frame = new FrmInventario();
            //Adiciona a código do usuário
            frame.codUsuario = this.codUsuario;
            //Coleção de empresa
            frame.empresaCollection = empresaCollection;
            //Exibe o frame
            frame.ShowDialog();
        }

        private void mniDigitacaoContagens_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmDigitacaoInventario"] == null)
            {
                //Instância o form 
                FrmDigitacaoInventario frame = new FrmDigitacaoInventario();
                //Adiciona a código do usuário
                frame.codUsuario = this.codUsuario;
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;
                //Adiciona o perfíl do usuário
                frame.perfil = this.perfil;
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmDigitacaoInventario"].BringToFront();
            }
        }

        private void mniRelatorioContagem_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmImpressaoContagens"] == null)
            {
                //Instância o form 
                FrmImpressaoContagens frame = new FrmImpressaoContagens();
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmImpressaoContagens"].BringToFront();
            }

        }

        #endregion

        #region Menu Relatório
        private void mniEstoquexPicking_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmImpressaoEstoquexPicking"] == null)
            {
                //Instância o form 
                FrmImpressaoEstoquexPicking frame = new FrmImpressaoEstoquexPicking();
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmImpressaoEstoquexPicking"].BringToFront();
            }
        }

        private void mniImpressaoEtiqueta_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmImpressaoEtiqueta"] == null)
            {
                //Instância o form 
                FrmImpressaoEtiqueta frame = new FrmImpressaoEtiqueta();
                //Perfíl do usuário
                frame.perfilUsuario = perfil;
                //Passa a impressora
                frame.impressora = impressora;
                //Nome da Empresa
                frame.empresa = nomeEmpresa;
                //Localizando o acesso
                frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1102 - IMPRESSÃO DE ETIQUETA"; });
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmImpressaoEtiqueta"].BringToFront();
            }
        }

        private void mniMapaSeparacao_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmImpressaoManifesto"] == null)
            {
                //Instância o form 
                FrmImpressaoManifesto frame = new FrmImpressaoManifesto();
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;

                //Adiciona o código do usuário
                frame.codUsuario = this.codUsuario;
                frame.controlaSequenciaCarregamento = controlaSequenciaCarregamento;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmImpressaoManifesto"].BringToFront();
            }
        }

        private void mniPendencia_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmImpressaoPendencia"] == null)
            {
                //Instância o form 
                FrmImpressaoPendencia frame = new FrmImpressaoPendencia();
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmImpressaoPendencia"].BringToFront();
            }
        }

        private void mniPickingNegativo_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmImpressaoPickingNegativo"] == null)
            {
                //Instância o form 
                FrmImpressaoPickingNegativo frame = new FrmImpressaoPickingNegativo();
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmImpressaoPickingNegativo"].BringToFront();
            }
        }

        private void mniAcimaCapacidade_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmImpressaoPickingAcimaCapacidade"] == null)
            {
                //Instância o form 
                FrmImpressaoPickingAcimaCapacidade frame = new FrmImpressaoPickingAcimaCapacidade();
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmImpressaoPickingAcimaCapacidade"].BringToFront();
            }
        }

        private void mniProdutoFlowRack_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["FrmProdutoFlowRack"] == null)
            {
                //Instância o form 
                FrmProdutoFlowRack frame = new FrmProdutoFlowRack();
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmProdutoFlowRack"].BringToFront();
            }
        }

        private void mniRendimentoExpedicao_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmImpressaoRendimentoExpedicao"] == null)
            {
                //Instância o form 
                FrmImpressaoRendimentoExpedicao frame = new FrmImpressaoRendimentoExpedicao();
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmImpressaoRendimentoExpedicao"].BringToFront();
            }
        }

        private void mniVencimento_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmVencimentoProduto"] == null)
            {
                //Instância o form 
                FrmVencimentoProduto frame = new FrmVencimentoProduto();
                //Nome da empresa
                frame.empresa = nomeEmpresa;
                //Adiciona a tela principal
                //Coleção de empresa
                frame.empresaCollection = empresaCollection;
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmVencimentoProduto"].BringToFront();
            }
        }

        private void mniWmsxErp_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmWmsxErp"] == null)
            {
                //Instância o form 
                FrmWmsxErp frame = new FrmWmsxErp();
                //Usuario
                frame.usuario = loginUsuario;
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmWmsxErp"].BringToFront();
            }
        }

        private void mniCurvaABC_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmImpressaoCurvaABC"] == null)
            {
                //Instância o form 
                FrmImpressaoCurvaABC frame = new FrmImpressaoCurvaABC();
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmImpressaoCurvaABC"].BringToFront();
            }
        }

        #endregion

        #region Menu Configuracao

        private void mniSistema_Click(object sender, EventArgs e)
        {
            //Instância o form configuração
            FrmConfiguracao frame = new FrmConfiguracao();
            //Exibe o frame
            frame.ShowDialog();

        }

        private void mnuChaves_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Menu Ajuda

        #endregion

        #region Menu Janelas

        private void mniCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void mniHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void mniVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void mniMinimizarTodos_Click(object sender, EventArgs e)
        {

        }

        private void mniFecharTodos_Click(object sender, EventArgs e)
        {
            foreach (Form mdiChildForm in MdiChildren)
            {
                mdiChildForm.Close();
            }
        }

        #endregion


        private void mniPainelPedidos_Click(object sender, EventArgs e)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms["FrmPainelPedido"] == null)
            {
                //Instância o form gerar nota entrada
                FrmPainelPedido frame = new FrmPainelPedido();
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmPainelPedido"].BringToFront();
            }
        }

        private void pnlCabecalho_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ExibirFrame(Form frame, string nmFrame)
        {
            //Verifica se o Form esta aberto
            if (Application.OpenForms[nmFrame] == null)
            {
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms[nmFrame].BringToFront();
            }
        }

        private void mnuSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Você deseja finalizar aplicação ?", "WMS - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //encerro a aplicação                    
                Application.ExitThread();
            }
        }


        private void txtRotina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                #region Menu Cadastro

                //Manu - Controle de categorias
                if (txtRotina.Text.Equals("101"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "101 - CATEGORIA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form categoria
                        FrmCategoria frame = new FrmCategoria();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Empresas
                        //frame.empresaCollection = empresaCollection;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmCategoria");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Caixa
                if (txtRotina.Text.Equals("102"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "102 - CAIXA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form caixa
                        FrmCaixa frame = new FrmCaixa();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Empresas
                        //frame.empresaCollection = empresaCollection;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmCaixa");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                //Menu - Cliente
                if (txtRotina.Text.Equals("103"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "103 - CLIENTE"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form ciente
                        FrmCliente frame = new FrmCliente();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Empresas
                        frame.empresaCollection = empresaCollection;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Empresas
                        frame.empresaCollection = empresaCollection;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmCliente");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Empilhador
                if (txtRotina.Text.Equals("104"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "104 - EMPILHADOR"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form empilhador
                        FrmEmpilhador frame = new FrmEmpilhador();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Empresas
                        frame.empresaCollection = empresaCollection;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmEmpilhador");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Estrutura
                if (txtRotina.Text.Equals("105"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "105 - ESTRUTURA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form estrutura
                        FrmEstrutura frame = new FrmEstrutura();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;

                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmEstrutura");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Estação
                if (txtRotina.Text.Equals("106"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "106 - ESTAÇÃO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form estação
                        FrmEstacao frame = new FrmEstacao();
                        //código do usuário
                        frame.codUsuario = this.codUsuario;
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmEstacao");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Fornecedor
                if (txtRotina.Text.Equals("107"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "107 - FORNECEDOR"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form fornecedor
                        FrmFornecedor frame = new FrmFornecedor();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Empresas
                        frame.empresaCollection = empresaCollection;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmFornecedor");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Motorista 
                if (txtRotina.Text.Equals("108"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "108 - MOTORISTA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form Motorista
                        FrmMotorista frame = new FrmMotorista();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        frame.empresaCollection = empresaCollection;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmMotorista");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Perfíl
                if (txtRotina.Text.Equals("109"))
                {
                    //Localizando menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "109 - PERFÍL"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form perfíl
                        FrmPerfil frame = new FrmPerfil();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmPerfil");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Produto
                if (txtRotina.Text.Equals("110"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "110 - PRODUTO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form produto
                        FrmProduto frame = new FrmProduto();
                        //código do usuário
                        frame.codUsuario = this.codUsuario;
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Empresas
                        frame.empresaCollection = empresaCollection;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmProduto");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Ratreador Protátil
                if (txtRotina.Text.Equals("111"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "111 - RASTREADOR PORTÁTIL"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form 
                        FrmRastreador frame = new FrmRastreador();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmRastreador");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Localizando o menu
                if (txtRotina.Text.Equals("112"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "112 - RASTREAMENTO DE CADASTRO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form
                        FrmRastreamentoCadastro frame = new FrmRastreamentoCadastro();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmRastreamentoCadastro");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Localizando o menu
                if (txtRotina.Text.Equals("113"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "113 - RESTRIÇÃO DE CATEGORIAS"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form restrição
                        FrmCategoriaRestricao frame = new FrmCategoriaRestricao();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmCategoriaRestricao");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Rotas
                if (txtRotina.Text.Equals("114"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "114 - ROTAS"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form 
                        FrmRotas frame = new FrmRotas();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        frame.empresaCollection = empresaCollection;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmRotas");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Tipo de Ocorrência
                if (txtRotina.Text.Equals("115"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "115 - TIPO OCORRÊNCIA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form 
                        FrmTipoOcorrencia frame = new FrmTipoOcorrencia();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmTipoOcorrencia");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Tipo de Rota
                if (txtRotina.Text.Equals("116"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "116 - TIPO ROTA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form 
                        FrmTipoRota frame = new FrmTipoRota();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmTipoRota");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Tipo de Veículo
                if (txtRotina.Text.Equals("117"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "117 - TIPO DE VEÍCULO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form 
                        FrmVeiculoTipo frame = new FrmVeiculoTipo();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmVeiculoTipo");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Unidade
                if (txtRotina.Text.Equals("118"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "118 - UNIDADE"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form unidade
                        FrmUnidade frame = new FrmUnidade();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmUnidade");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Usuário
                if (txtRotina.Text.Equals("119"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "119 - USUÁRIO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form usuario
                        FrmUsuario frame = new FrmUsuario();
                        //frame.TopLevel = false;
                        //frame.FormBorderStyle = FormBorderStyle.None;
                        //frame.Dock = DockStyle.Fill;
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmUsuario");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Veículo
                if (txtRotina.Text.Equals("120"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "120 - VEÍCULO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form 
                        FrmVeiculo frame = new FrmVeiculo();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        frame.empresaCollection = empresaCollection;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmVeiculo");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Senha
                if (txtRotina.Text.Equals("121"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "121 - SENHA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form senha
                        FrmSenha frame = new FrmSenha();
                        frame.idUsuario = codUsuario;
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        frame.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Valor
                if (txtRotina.Text.Equals("122"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "122 - VALOR"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form
                        FrmValorMotorista frame = new FrmValorMotorista();
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmValorMotorista");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }


                #endregion

                #region Menu Auditoria 

                //Menu - Auditoria de Produto
                if (txtRotina.Text.Equals("301"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "301 - AUDITORIA DE PRODUTO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o frame
                        FrmAuditoria frame = new FrmAuditoria();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Passa o código do usuário
                        frame.codUsuario = codUsuario;
                        frame.empresaCollection = empresaCollection;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmAuditoria");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Auditoria de Ocorrência
                if (txtRotina.Text.Equals("302"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "302 - AUDITORIA DE OCORRÊNCIA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form gerar nota entrada
                        FrmAuditoriaOcorrencia frame = new FrmAuditoriaOcorrencia();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Código do usuário
                        frame.codUsuario = this.codUsuario;
                        frame.empresaCollection = empresaCollection;
                        //Adiciona a tela principal
                        frame.MdiParent = this;
                        //Exibe o frame
                        frame.Show();
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                #endregion

                #region Menu Estoque

                //Menu - Armazenamento
                if (txtRotina.Text.Equals("401"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "401 - ARMAZENAMENTO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o frama
                        FrmArmazenamento frame = new FrmArmazenamento();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Passa o código do usuário
                        frame.codUsuario = codUsuario;
                        //Passa o modelo da impressora
                        frame.impressora = impressora;
                        //Passa o nome da empresa
                        frame.empresa = nomeEmpresa;
                        frame.empresaCollection = empresaCollection;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmArmazenamento");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Abastecimento
                if (txtRotina.Text.Equals("402"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "402 - ABASTECIMENTO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form gerar nota entrada
                        FrmAbastecimento frame = new FrmAbastecimento();
                        frame.codUsuario = this.codUsuario;
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        frame.empresaCollection = empresaCollection;
                        //Adiciona a tela principal
                        frame.MdiParent = this;
                        //Exibe o frame
                        frame.Show();
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Endereçamento de Produto
                if (txtRotina.Text.Equals("403"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "403 - ENDEREÇAMENTO DE PRODUTO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form empilhador
                        FrmEnderecamentoPicking frame = new FrmEnderecamentoPicking();
                        //código do usuário
                        frame.codUsuario = this.codUsuario;
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmEnderecamento");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - De Para
                if (txtRotina.Text.Equals("404"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "404 - DE PARA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmDePara"] == null)
                        {
                            //Instância o form 
                            FrmDePara frame = new FrmDePara();
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmDePara"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Consulta de Estoque
                if (txtRotina.Text.Equals("405"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "405 - CONSULTA DE ESTOQUE"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form consulta de Estoque
                        FrmEstoque frame = new FrmEstoque();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Passa o código do usuário
                        frame.codUsuario = codUsuario;
                        //Passa o modelo da impressora
                        frame.impressora = impressora;
                        //Passa o nome da empresa
                        frame.empresa = nomeEmpresa;
                        frame.empresaCollection = empresaCollection;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmEstoque");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Consulta de Lote
                if (txtRotina.Text.Equals("406"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "406 - CONSULTA DE LOTE"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        /* //Instância o form gerar nota entrada
                         FrmAuditoriaOcorrencia frame = new FrmAuditoriaOcorrencia();
                         //Perfíl do usuário
                         frame.perfilUsuario = perfil;
                         //Localizando o acesso
                         frame.acesso = menu;
                         //Código do usuário
                         frame.codUsuario = this.codUsuario;
                         //Adiciona a tela principal
                         frame.MdiParent = this;
                         //Exibe o frame
                         frame.Show();*/
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Reserva de Produto
                if (txtRotina.Text.Equals("407"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "407 - RESERVA DE PRODUTO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form
                        FrmReserva frame = new FrmReserva();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Passa o código do usuario
                        frame.codUsuario = this.codUsuario;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmReserva");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Transferência de produto
                if (txtRotina.Text.Equals("408"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "408 - TRANSFERÊNCIA DE PRODUTO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmTrasferencia"] == null)
                        {
                            //Instância o form tranferência de endereços
                            FrmTrasferenciaProduto frame = new FrmTrasferenciaProduto();
                            frame.codUsuario = this.codUsuario;
                            //Perfíl do usuário
                            frame.perfilUsuario = perfil;
                            //Passa o nome da empresa
                            // frame.empresa = nomeEmpresa;
                            frame.empresaCollection = empresaCollection;
                            //Localizando o acesso
                            frame.acesso = menu;
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmTrasferencia"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                #endregion

                #region Menu Expedicao

                //Menu - Alterar Volume de Flow Rack
                if (txtRotina.Text.Equals("501"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "501 - ALTERAR VOLUME DE FLOW RACK"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form gerar nota entrada
                        FrmAlterarVolume frame = new FrmAlterarVolume();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Nome da Empresa
                        frame.empresa = nomeEmpresa;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Adiciona a tela principal
                        frame.MdiParent = this;
                        //Exibe o frame
                        frame.Show();
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Conferência automática
                if (txtRotina.Text.Equals("502"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "502 - CONFERENÊNCIA AUTOMÁTICA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form conferecia automatica
                        FrmConferenciaAutomatica frame = new FrmConferenciaAutomatica();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Passa o código do usuário
                        frame.codUsuario = codUsuario;
                        //Exibe o frame
                        frame.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Conferência da Nota Cega
                if (txtRotina.Text.Equals("503"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "503 - CONFERÊNCIA DA NOTA CEGA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form conferência cega
                        FrmConferenciaCega frame = new FrmConferenciaCega();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Passa o código do usuario
                        frame.codUsuario = this.codUsuario;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmConferenciaCega");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Conferência de Flow Rack
                if (txtRotina.Text.Equals("504"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "504 - CONFERÊNCIA DE FLOW RACK"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form conferência de manifesto
                        FrmConferenciaFlowRack frame = new FrmConferenciaFlowRack();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Passa o código do usuário
                        frame.codUsuario = codUsuario;
                        //Passa o código da estação
                        frame.codEstacao = codEstacao;
                        //Passa a descrição da estação
                        frame.descEstacao = "ESTAÇÃO " + codEstacao;
                        //Passa o nível da estação
                        frame.nivelEstacao = nivelEstacao;
                        frame.empresa = nomeEmpresa;
                        //Exibe o frame
                        frame.Show();
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Conferência por Manifesto
                if (txtRotina.Text.Equals("505"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "505 - CONFERÊNCIA POR MANIFESTO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form conferência cega
                        FrmConferenciaManifesto frame = new FrmConferenciaManifesto();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Passa o código do usuario
                        frame.codUsuario = this.codUsuario;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmConferenciaManifesto");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Conferência de Pedido
                if (txtRotina.Text.Equals("506"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "506 - CONFERÊNCIA DE PEDIDO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form conferência de pedido
                        FrmConferencia frame = new FrmConferencia();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Passa o código do usuario
                        frame.codUsuario = this.codUsuario;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmConferencia");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Monitor de Pedidos
                if (txtRotina.Text.Equals("507"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "507 - MONITOR DE PEDIDOS"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmMonitorPedido"] == null)
                        {
                            //Instância o form monitor de pedido
                            FrmMonitorPedido frame = new FrmMonitorPedido();
                            //Perfíl do usuário
                            frame.perfilUsuario = perfil;
                            //Localizando o acesso
                            frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "MONITOR DE PEDIDOS"; });
                            //Passa a impressora
                            frame.impressora = impressora;
                            //Código do usuário
                            frame.codUsuario = this.codUsuario;
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            frame.empresaCollection = empresaCollection;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmMonitorPedido"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Processamento de Flow Rack
                if (txtRotina.Text.Equals("508"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "508 - PROCESSAMENTO DE FLOW RACK"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmProcessamentoFlow"] == null)
                        {
                            //Instância o form monitor de pedido
                            FrmProcessamentoFlow frame = new FrmProcessamentoFlow();
                            //Perfíl do usuário
                            frame.perfilUsuario = perfil;
                            //Localizando o acesso
                            frame.acesso = menu;
                            frame.empresaCollection = empresaCollection;
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmProcessamentoFlow"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Rastreamento de Operações
                if (txtRotina.Text.Equals("509"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "509 - RASTREAMENTO DE OPERAÇÕES"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form
                        FrmRastreamentoOperacoes frame = new FrmRastreamentoOperacoes();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmRastreamentoOperacoes");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Rastreamento de Operações
                if (txtRotina.Text.Equals("511"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "511 - ROTEIRIZADOR"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form
                        FrmRoteirizador frame = new FrmRoteirizador();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmRoteirizador");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                //Menu - Rastreamento de Operações
                if (txtRotina.Text.Equals("512"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "512 - CHECK NOW"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form
                        FrmExportacaoCheckNow frame = new FrmExportacaoCheckNow();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmExportacaoCheckNow");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }                

                //Menu - Saída do manifesto de entrega
                if (txtRotina.Text.Equals("513"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "513 - SAIDA MANIFESTO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form
                        FrmResumoSaida frame = new FrmResumoSaida();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu; 
                        frame.empresaCollection = empresaCollection;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmResumoSaida");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                //Menu - Monitor Portatil
                if (txtRotina.Text.Equals("514"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "514 - MONITOR PORTATIL"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form
                        FrmPortatil frame = new FrmPortatil();
                        //Perfíl do usuário
                        frame.perfilUsuario = perfil;
                        //Localizando o acesso
                        frame.acesso = menu;
                        frame.empresaCollection = empresaCollection;
                        //Exibe o frame
                        ExibirFrame(frame, "FrmPortatil");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


                #endregion

                #region Menu Recebimento

                //Menu - Nota de Entrada
                if (txtRotina.Text.Equals("601"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "601 - NOTA ENTRADA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmNotaEntrada"] == null)
                        {
                            //Instância o form gerar nota entrada
                            FrmNotaEntrada frame = new FrmNotaEntrada();
                            //Perfíl do usuário
                            frame.perfilUsuario = perfil;
                            //Localizando o acesso
                            frame.acesso = menu;
                            //Empresas
                            frame.empresaCollection = empresaCollection;
                            frame.codUsuario = this.codUsuario;
                            frame.loginUsuario = this.loginUsuario;
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmNotaEntrada"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                #endregion

                #region Menu Monitoramento

                //Menu - Nota de Entrada
                if (txtRotina.Text.Equals("701"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "701 - OCORRÊNCIA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form ocorrência
                        FrmOcorrencia frame = new FrmOcorrencia();
                        //código do usuário
                        frame.nomeEmpresa = this.nomeEmpresa;
                        frame.codUsuario = this.codUsuario;
                        frame.loginUsuario = this.loginUsuario;
                        //Perfíl do usuário
                        frame.perfilUsuario = this.perfil;
                        //Localizando o acesso
                        frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "OCORRÊNCIA"; });
                        //Exibe o frame
                        ExibirFrame(frame, "FrmOcorrencia");
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                #endregion

                #region Menu Dashboard

                //Menu - Dash Flowrack
                if (txtRotina.Text.Equals("801"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "801 - DASHBOARD FLOW RACK"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmDashFlowRack"] == null)
                        {
                            //Instância o form 
                            FrmDashFlowRack frame = new FrmDashFlowRack();
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmDashFlowRack"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Dash Expedicao
                if (txtRotina.Text.Equals("802"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "802 - DASHBOARD EXPEDIÇÃO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmDashboardExpedicao"] == null)
                        {
                            //Instância o form 
                            FrmDashboardExpedicao frame = new FrmDashboardExpedicao();
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmDashboardExpedicao"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Dash Inventário
                if (txtRotina.Text.Equals("803"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "803 - DASHBOARD INVENTÁRIO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmDashboardInventario"] == null)
                        {
                            //Instância o form 
                            FrmDashboardInventario frame = new FrmDashboardInventario();
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmDashboardInventario"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                #endregion

                #region Menu Inventário

                //Menu - Abrir Inventário
                if (txtRotina.Text.Equals("901"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "901 - ABRIR INVENTÁRIO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form monitor de pedido
                        FrmInventario frame = new FrmInventario();
                        //Adiciona a código do usuário
                        frame.codUsuario = this.codUsuario;
                        //Exibe o frame
                        frame.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Digitação das Contagens
                if (txtRotina.Text.Equals("902"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "902 - DIGITAÇÃO DAS CONTAGENS"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmDigitacaoInventario"] == null)
                        {
                            //Instância o form 
                            FrmDigitacaoInventario frame = new FrmDigitacaoInventario();
                            //Adiciona a código do usuário
                            frame.codUsuario = this.codUsuario;
                            //Adiciona o perfíl do usuário
                            frame.perfil = this.perfil;
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmDigitacaoInventario"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Relatório da Contagens
                if (txtRotina.Text.Equals("903"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "903 - RELATÓRIO DAS CONTAGENS"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmImpressaoContagens"] == null)
                        {
                            //Instância o form 
                            FrmImpressaoContagens frame = new FrmImpressaoContagens();
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmImpressaoContagens"].BringToFront();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }


                #endregion

                #region Menu Impressao

                //Menu - Estoque VS Picking
                if (txtRotina.Text.Equals("1101"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1101 - ESTOQUE VS PICKING"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmImpressaoEstoquexPicking"] == null)
                        {
                            //Instância o form 
                            FrmImpressaoEstoquexPicking frame = new FrmImpressaoEstoquexPicking();
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmImpressaoEstoquexPicking"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Impressão de Etiqueta
                if (txtRotina.Text.Equals("1102"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1102 - IMPRESSÃO DE ETIQUETA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmImpressaoEtiqueta"] == null)
                        {
                            //Instância o form 
                            FrmImpressaoEtiqueta frame = new FrmImpressaoEtiqueta();
                            //Perfíl do usuário
                            frame.perfilUsuario = perfil;
                            //Passa a impressora
                            frame.impressora = impressora;
                            //Nome da Empresa
                            frame.empresa = nomeEmpresa;
                            //Localizando o acesso
                            frame.acesso = menu;
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmImpressaoEtiqueta"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Mapa de Separação Por Manifesto
                if (txtRotina.Text.Equals("1103"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1103 - MAPA DE SEPARAÇÃO POR MANIFESTO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmImpressaoManifesto"] == null)
                        {
                            //Instância o form 
                            FrmImpressaoManifesto frame = new FrmImpressaoManifesto();
                            //Adiciona o código do usuário
                            frame.codUsuario = this.codUsuario;
                            //Coleção de empresa
                            frame.empresaCollection = empresaCollection;
                            frame.controlaSequenciaCarregamento = controlaSequenciaCarregamento;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmImpressaoManifesto"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Pendência de Entrega
                if (txtRotina.Text.Equals("1104"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1104 - PENDÊNCIA DE ENTREGA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmImpressaoPendencia"] == null)
                        {
                            //Instância o form 
                            FrmImpressaoPendencia frame = new FrmImpressaoPendencia();
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmImpressaoPendencia"].BringToFront();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Picking Negativo
                if (txtRotina.Text.Equals("1105"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1105 - PICKING NEGATIVO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmImpressaoPickingNegativo"] == null)
                        {
                            //Instância o form 
                            FrmImpressaoPickingNegativo frame = new FrmImpressaoPickingNegativo();
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmImpressaoPickingNegativo"].BringToFront();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Picking Acima da Capacidade
                if (txtRotina.Text.Equals("1106"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1106 - PICKING ACIMA DA CAPACIDADE"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmImpressaoPickingAcimaCapacidade"] == null)
                        {
                            //Instância o form 
                            FrmImpressaoPickingAcimaCapacidade frame = new FrmImpressaoPickingAcimaCapacidade();
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmImpressaoPickingAcimaCapacidade"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Produto no Flow Rack
                if (txtRotina.Text.Equals("1107"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1107 - PRODUTO NO FLOW RACK"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        if (Application.OpenForms["FrmProdutoFlowRack"] == null)
                        {
                            //Instância o form 
                            FrmProdutoFlowRack frame = new FrmProdutoFlowRack();
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmProdutoFlowRack"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Rendimento da Expedição
                if (txtRotina.Text.Equals("1108"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1108 - RENDIMENTO DA EXPEDIÇÃO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmImpressaoRendimentoExpedicao"] == null)
                        {
                            //Instância o form 
                            FrmImpressaoRendimentoExpedicao frame = new FrmImpressaoRendimentoExpedicao();
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmImpressaoRendimentoExpedicao"].BringToFront();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Vencimento de Produto
                if (txtRotina.Text.Equals("1109"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1109 - VENCIMENTO DE PRODUTO"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmVencimentoProduto"] == null)
                        {
                            //Instância o form 
                            FrmVencimentoProduto frame = new FrmVencimentoProduto();
                            //Nome da empresa
                            frame.empresa = nomeEmpresa;
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmVencimentoProduto"].BringToFront();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - Wms vs ERP
                if (txtRotina.Text.Equals("1110"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1110 - WMS VS ERP"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmWmsxErp"] == null)
                        {
                            //Instância o form 
                            FrmWmsxErp frame = new FrmWmsxErp();
                            //Usuario
                            frame.usuario = loginUsuario;
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmWmsxErp"].BringToFront();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                //Menu - CURVA ABC
                if (txtRotina.Text.Equals("1111"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1111 - CURVA ABC"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Verifica se o Form esta aberto
                        if (Application.OpenForms["FrmImpressaoCurvaABC"] == null)
                        {
                            //Instância o form 
                            FrmImpressaoCurvaABC frame = new FrmImpressaoCurvaABC();
                            //Adiciona a tela principal
                            frame.MdiParent = this;
                            //Exibe o frame
                            frame.Show();
                        }
                        else
                        {
                            //Traz o form para a frente
                            Application.OpenForms["FrmImpressaoCurvaABC"].BringToFront();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }


                #endregion

                #region Menu Configuracao 

                //Menu - Sistema
                if (txtRotina.Text.Equals("1201"))
                {
                    //Localizando o menu
                    List<Acesso> menu = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "1201 - SISTEMA"; });

                    //Verifica se o menu foi encontrado
                    if (menu.Count == 0)
                    {
                        MessageBox.Show("Rotina não encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (menu[0].lerFuncao == true || perfil.Trim() == "ADMINISTRADOR")
                    {
                        //Instância o form configuração
                        FrmConfiguracao frame = new FrmConfiguracao();
                        //Exibe o frame
                        frame.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Rotina não liberada para acesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                #endregion

                #region Menu Ajuda

                #endregion

                #region Menu Janelas

                //Menu - EM CASCATA 
                if (txtRotina.Text.Equals("1401"))
                {
                    this.LayoutMdi(MdiLayout.Cascade);
                }

                //Menu - EM HORIZONTAL
                if (txtRotina.Text.Equals("1402"))
                {
                    this.LayoutMdi(MdiLayout.TileHorizontal);
                }

                //Menu - VERTICAL
                if (txtRotina.Text.Equals("1403"))
                {
                    this.LayoutMdi(MdiLayout.TileVertical);
                }

                //Menu - MINIMIZAR TODOS
                if (txtRotina.Text.Equals("1404"))
                {
                    this.WindowState = FormWindowState.Minimized;
                }

                //Menu - FECHAR TODOS
                if (txtRotina.Text.Equals("1405"))
                {
                    foreach (Form mdiChildForm in MdiChildren)
                    {
                        mdiChildForm.Close();
                    }
                }

                #endregion

                //Limpa o campo
                txtRotina.Clear();
            }
        }

        private long TamanhoTotalDiretorio(DirectoryInfo dInfo, bool includeSubDir)
        {
            //percorre os arquivos da pasta e calcula o tamanho somando o tamanho de cada arquivo
            long tamanhoTotal = dInfo.EnumerateFiles().Sum(file => file.Length);
            if (includeSubDir)
            {
                tamanhoTotal += dInfo.EnumerateDirectories().Sum(dir => TamanhoTotalDiretorio(dir, true));
            }
            return tamanhoTotal;
        }

        // Retorna o tamanho do arquivo para um tamanho
        // O formato padrão é "0.### XB", Ex: "4.2 KB" ou "1.434 GB"
        public string FormataExibicaoTamanhoArquivo(long i)
        {
            // Obtém o valor absoluto
            long i_absoluto = (i < 0 ? -i : i);
            // Determina o sufixo e o valor
            string sufixo;
            double leitura;
            if (i_absoluto >= 0x1000000000000000) // Exabyte
            {
                sufixo = "EB";
                leitura = (i >> 50);
            }
            else if (i_absoluto >= 0x4000000000000) // Petabyte
            {
                sufixo = "PB";
                leitura = (i >> 40);
            }
            else if (i_absoluto >= 0x10000000000) // Terabyte
            {
                sufixo = "TB";
                leitura = (i >> 30);
            }
            else if (i_absoluto >= 0x40000000) // Gigabyte
            {
                sufixo = "GB";
                leitura = (i >> 20);
            }
            else if (i_absoluto >= 0x100000) // Megabyte
            {
                sufixo = "MB";
                leitura = (i >> 10);
            }
            else if (i_absoluto >= 0x400) // Kilobyte
            {
                sufixo = "KB";
                leitura = i;
            }
            else
            {
                return i.ToString("0 bytes"); // Byte
            }
            // Divide por 1024 para obter o valor fracionário
            leitura = (leitura / 1024);
            // retorna o número formatado com sufixo
            return leitura.ToString("0.### ") + sufixo;
        }

        private void monitorDePortatilToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Application.OpenForms["FrmPortatil"] == null)
            {
                //Instância o form monitor de pedido
                FrmPortatil frame = new FrmPortatil();
                //Perfíl do usuário
                frame.perfilUsuario = perfil;
                //Localizando o acesso
                frame.acesso = controleAcesso.FindAll(delegate (Acesso n) { return n.descFuncao.Trim() == "514 - MONITOR  PORTATIL"; });
                //Passa a coleção de empresas
                frame.empresaCollection = empresaCollection;
                //Passa a impressora
                frame.impressora = impressora;
                //Código do usuário
                frame.codUsuario = this.codUsuario;
                //Adiciona a tela principal
                frame.MdiParent = this;
                //Exibe o frame
                frame.Show();
            }
            else
            {
                //Traz o form para a frente
                Application.OpenForms["FrmPortatil"].BringToFront();
            }

        }
    }
}
