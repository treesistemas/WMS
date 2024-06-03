using System;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmPesqCliente : Form
    {
        public int codCliente;
        public string nmCliente;

        public FrmPesqCliente()
        {
            InitializeComponent();
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Pesquisa cliente
                pesqCliente(txtCliente.Text);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa cliente
            pesqCliente(txtCliente.Text);
        }

        private void gridCliente_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Trânsfere os dados selecionados
            transfereDados();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Trânsfere os dados selecionados
            transfereDados();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pesqCliente(string cliente)
        {
            try
            {
                if (txtCliente.Text.Length <= 2)
                {
                    MessageBox.Show("Digite pelo menos 3 caracteres para efetuar a pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o negocios
                    PesqClienteNegocios pesqClienteNegocios = new PesqClienteNegocios();
                    //Instância a coleçãO
                    PesqClienteCollection pesqClienteCollection = new PesqClienteCollection();
                    //A coleção recebe o resultado da consulta
                    pesqClienteCollection = pesqClienteNegocios.pesqCliente(cliente, 0);
                    //Limpa o grid
                    gridCliente.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    pesqClienteCollection.ForEach(n => gridCliente.Rows.Add(n.codCliente, n.nmCliente));

                    //Total encontrado
                    lblTotal.Text = gridCliente.RowCount.ToString();

                    if (gridCliente.RowCount > 0)
                    {
                        //Seleciona a primeira linha do grid
                        gridCliente.CurrentCell = gridCliente.Rows[0].Cells[1];
                        //Foca no grid
                        gridCliente.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum cliente encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Trânsfere os dados selecionados
        private void transfereDados()
        {
            if (gridCliente.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Foca no campo cliente
                txtCliente.Focus();
                return;
            }
            else
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridCliente.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Seta os valores
                codCliente = Convert.ToInt32(gridCliente.Rows[indice].Cells[0].Value.ToString());
                nmCliente = gridCliente.Rows[indice].Cells[1].Value.ToString();

                //controla  a ação do frame
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                //Fecha a tela
                Close();
            }
        }

    }
}
