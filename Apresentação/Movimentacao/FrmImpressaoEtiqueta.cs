using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using Utilitarios;

namespace Wms.Movimentacao
{
    public partial class FrmImpressaoEtiqueta : Form
    {
        //Perfíl do usuário
        public string perfilUsuario;
        //Nome Empresa
        public string empresa;
        //Controle de acesso
        public List<Acesso> acesso;
        public string impressora;

        public FrmImpressaoEtiqueta()
        {
            InitializeComponent();
        }

        private void chkVolume_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVolume.Checked == true)
            {
                lblTipo.Text = "Volume";
                lblInicio.Text = "Usuário";
                this.txtInicio.AutoSize = false;
                this.txtInicio.Size = new System.Drawing.Size(108, 21);
                lblFim.Visible = false;
                txtFim.Visible = false;
            }
            else
            {
                lblTipo.Text = "Pedido";
                lblInicio.Text = "Inicio";
                this.txtInicio.AutoSize = false;
                this.txtInicio.Size = new System.Drawing.Size(50, 21);
                lblFim.Visible = true;
                txtFim.Visible = true;
            }
        }

        private void txtVolume_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (chkVolume.Checked == true)
                {
                    btnImprimir.Focus();
                }
                else
                {
                    txtInicio.Focus();
                }
            }
        }

        private void txtInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtFim.Focus();
            }
        }

        private void txtFim_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnImprimir.Focus();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (chkVolume.Checked == true)
            {
                if (txtVolume.Text != string.Empty)
                {
                    //Imprime o volume do flow rack
                    ImprimirVolumeFlowRack();
                }
                else
                {
                    //Imprime o volume do flow rack
                    ImprimirVolumeUsuario();
                }
            }
            else
            {
                //Imprime o volume do pedido
                ImprimirVolumePedido();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Pesquisa a quantidade total de volume do pedido para impressão
        private void ImprimirVolumePedido()
        {
            try
            {
                //Instância a camada de negocios
                ImpressaoEtiquetaNegocios volume = new ImpressaoEtiquetaNegocios();
                //Pesquisa o volume atual do pedido + informações da etiqueta
                Pedido pedido = volume.PesqVolumePedido(txtVolume.Text);

                if (pedido.codPedido == 0)
                {
                    MessageBox.Show("Pedido não encontrado!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Convert.ToString(pedido.fimConferencia) == string.Empty)
                {
                    MessageBox.Show("Conferência do pedido não finalizada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txtInicio.Text.Equals(""))
                {
                    MessageBox.Show("Digite um volume inicial!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txtFim.Text.Equals(""))
                {
                    MessageBox.Show("Digite um volume final!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Convert.ToInt32(pedido.numeroVolume) < Convert.ToInt32(txtFim.Text))
                {
                    MessageBox.Show("A quantidade de volume ultrapassa a do pedido!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int inicio = Convert.ToInt32(txtInicio.Text);
                    int fim = Convert.ToInt32(txtFim.Text);

                    for (int i = inicio; inicio <= fim; inicio++)
                    {
                        pedido.numeroVolume = string.Format("{0:00}", inicio);
                        pedido.barraVolume = pedido.codPedido + string.Format("{0:00}", inicio);

                        //Imprime a etiqueta
                        ImprimirEtiqueta(pedido);
                    }

                    //Limpa os campos
                    txtInicio.Text = string.Empty;
                    txtFim.Text = string.Empty;

                    //Seleciona o campo
                    txtVolume.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Pesquisa a quantidade de volume para impressão
        private void ImprimirVolumeFlowRack()
        {
            //Instância a camada de negocios
            ImpressaoEtiquetaNegocios volume = new ImpressaoEtiquetaNegocios();
            //Instância uma coleção de objêto 
            Pedido pedido = new Pedido();

            //Pesquisa o volume atual do pedido + informações da etiqueta
            pedido = volume.PesqVolumeFlowRack(txtVolume.Text);

            if (pedido.numeroVolume.Equals(""))
            {
                MessageBox.Show("Volume não encontrado!");
            }
            else
            {
                //Imprime a etiqueta
                ImprimirEtiqueta(pedido);
                

                //Seleciona o campo
                txtVolume.SelectAll();
            }
        }

        //Pesquisa o volume não impresso por usuário
        private void ImprimirVolumeUsuario()
        {
            //Instância a camada de negocios
            ImpressaoEtiquetaNegocios volume = new ImpressaoEtiquetaNegocios();
            //Instância uma coleção de objêto 
            PedidoCollection pedidoCollection = new PedidoCollection();

            //Pesquisa o volume atual do pedido + informações da etiqueta
            pedidoCollection = volume.PesqVolumeUsuario(Convert.ToInt32(txtInicio.Text));

            if (pedidoCollection.Count == 0)
            {
                MessageBox.Show("Nenhum volume encontrado!");
            }
            else
            {
                foreach(Pedido pedido in pedidoCollection)
                {
                    //Imprime a etiqueta
                    ImprimirEtiqueta(pedido);

                    //Atualiza o status
                    volume.AtualizarSatatus(pedido.barraVolume);
                }
                

                //Seleciona o campo
                txtInicio.SelectAll();
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
                    string NovaEtiqueta = AppDomain.CurrentDomain.BaseDirectory + pedido.barraVolume + ".txt";

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
                    conteudo = conteudo.Replace("VOLUME", "VOLUME " + pedido.numeroVolume);
                    conteudo = conteudo.Replace("PEDIDO", "" + pedido.codPedido);
                    conteudo = conteudo.Replace("ESTACAO", "ESTACAO " + pedido.codEstacao);
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


    }
}
