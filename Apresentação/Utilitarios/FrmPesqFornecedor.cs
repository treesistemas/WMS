using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmPesqFornecedor : Form
    {
        public string codFornecedor;
        public string nmFornecedor;
        public string empresa;

        public FrmPesqFornecedor()
        {
            InitializeComponent();
        }

        private void txtFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                //Foca no botãor
                btnPesquisar.Focus();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa Fornecedor
            PesqFornecedor(txtFornecedor.Text);
        }

        private void gridFornecedor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Trânsfere os dados selecionados
            TransferirDados();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Trânsfere os dados selecionados
            TransferirDados();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //fechar
            Close();
        }

        private void PesqFornecedor(string fornecedor)
        {
            try
            {
                if (txtFornecedor.Text.Length <= 2)
                {
                    MessageBox.Show("Digite pelo menos 3 caracteres para efetuar a pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o negocios
                    FornecedorNegocios pesqFornecedorNegocios = new FornecedorNegocios();
                    //Instância a coleçãO
                    FornecedorCollection pesqFornecedorCollection = new FornecedorCollection();
                    //A coleção recebe o resultado da consulta
                    pesqFornecedorCollection = pesqFornecedorNegocios.pesqFornecedor(empresa, fornecedor, 0);
                    //Limpa o grid
                    gridFornecedor.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    pesqFornecedorCollection.ForEach(n => gridFornecedor.Rows.Add(n.codFornecedor, n.nomeFornecedor));

                    //Total encontrado
                    lblTotal.Text = gridFornecedor.RowCount.ToString();

                    if (gridFornecedor.RowCount > 0)
                    {
                        //Seleciona a primeira linha do grid
                        gridFornecedor.CurrentCell = gridFornecedor.Rows[0].Cells[1];
                        //Foca no grid
                        gridFornecedor.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum fornecedor encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Trânsfere os dados selecionados
        private void TransferirDados()
        {
            if (gridFornecedor.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum fornecedor selecionado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Foca no campo fornecedor
                txtFornecedor.Focus();
                return;
            }
            else
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridFornecedor.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Seta os valores
                codFornecedor = gridFornecedor.Rows[indice].Cells[0].Value.ToString();
                nmFornecedor = gridFornecedor.Rows[indice].Cells[1].Value.ToString();

                //controla  a ação do frame
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                //Fecha a tela
                Close();
            }
        }

    }
}
