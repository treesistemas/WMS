using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmConferenciaAutomatica : Form
    {
        //Código do usuário
        public int codUsuario;
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;
        //Instância o objêto
        Pedido pedido = new Pedido();

        public FrmConferenciaAutomatica()
        {
            InitializeComponent();
        }

        private void FrmConferenciaAutomatica_Load(object sender, EventArgs e)
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

        //KeyPress
        private void txtPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisar.Focus();
            }
        }

        //Click
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa o pedido
            PesqPedido();
        }

        private void btnConferir_Click(object sender, EventArgs e)
        {
            DateTime data = DateTime.Now;

            //Desconfere o pedido
            StatusConferencia("conferido", Convert.ToInt32(txtPedido.Text), txtMotivo.Text, Convert.ToDateTime(data.ToString("dd/MM/yyyy")), Convert.ToDateTime(data.ToString("dd/MM/yyyy")));

        }

        private void btnDesconferir_Click(object sender, EventArgs e)
        {
            //Desconfere o pedido
            StatusConferencia("desconferido", Convert.ToInt32(txtPedido.Text), txtMotivo.Text, null, null);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o frame
            Close();
        }

        //Pesquisa o pedido
        private void PesqPedido()
        {
            try
            {
                if (txtPedido.Text == "")
                {
                    MessageBox.Show("Digite o numero do pedido", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    //Instância a camada de negocios 
                    PedidoNegocios pedidoNegocios = new PedidoNegocios();
                    //Limpa o pedido
                    pedido = null;
                    //Pesquisa o pedido
                    pedido = pedidoNegocios.PesqPedido(txtPedido.Text, cmbEmpresa.Text);

                    if (!pedido.nomeCliente.Equals("null"))
                    {
                        lblNmCliente.Text = pedido.nomeCliente;
                        lblNmCliente.Visible = true;
                        txtMotivo.Focus();
                    }
                    else
                    {
                        lblNmCliente.Visible = false;
                        MessageBox.Show("Nenhum pedido encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPedido.Focus();
                        txtPedido.SelectAll();
                        return;
                    }
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(""+ex,"WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Confere o pedido
        private void StatusConferencia(string processo, int codPedido, string motivo, DateTime? dataInicial, DateTime? dataFinal)
        {
            try
            {
                if (txtPedido.Text.Equals(""))
                {
                    //Mensagem
                    MessageBox.Show("Digite o numero do pedido!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPedido.Focus();
                    return;
                }
                if (txtMotivo.Text.Equals(""))
                {
                    //Mensagem
                    MessageBox.Show("Por favor, digite um motivo!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMotivo.Focus();
                    return;
                }
                else if (pedido.fimConferencia == null && processo.Equals("desconferido"))
                {
                    //Mensagem
                    MessageBox.Show("Pedido não finalizado na conferência", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPedido.Focus();
                    return;
                }
                else
                {
                    //Instância a camada de negocios 
                    PedidoNegocios pedidoNegocios = new PedidoNegocios();
                    //Passa o pedido para camada de negocios
                    pedidoNegocios.StatusPedido(codPedido, motivo, codUsuario, dataInicial, dataFinal, processo, cmbEmpresa.Text);

                    //limpa todos os campos
                    txtPedido.ResetText();
                    lblNmCliente.ResetText();
                    txtMotivo.ResetText();

                    MessageBox.Show("Pedido "+ processo +" com sucesso! ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        
    }
}
