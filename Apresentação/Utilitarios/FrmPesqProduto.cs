using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmPesqProduto : Form
    {
        public string codProduto;
        public string descProduto;
        public string separacaoFlowrack;
        public string empresa;

        public FrmPesqProduto()
        {
            InitializeComponent();
        }

        private void txtProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botãor
                btnPesquisar.Focus();
            }
        }

        private void gridProduto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Trânsfere os dados selecionados
            TransferirDados();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa o produto
            PesqProduto(txtProduto.Text);
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

        private void PesqProduto(string produto)
        {
            try
            {
                if (txtProduto.Text.Length <= 2)
                {
                    MessageBox.Show("Digite pelo menos 3 caracteres para efetuar a pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o negocios
                    ProdutoNegocios produtoNegocios = new ProdutoNegocios();
                    //Instância a coleção
                    ProdutoCollection produtoCollection = new ProdutoCollection();
                    //A coleção recebe o resultado da consulta
                    produtoCollection = produtoNegocios.PesqProduto(empresa, produto, "");
                    //Limpa o grid
                    gridProduto.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    produtoCollection.ForEach(n => gridProduto.Rows.Add(n.codProduto, n.descProduto, n.separacaoFlowrack));

                    //Total encontrado
                    lblTotal.Text = gridProduto.RowCount.ToString();

                    if (gridProduto.RowCount > 0)
                    {
                        //Seleciona a primeira linha do grid
                        gridProduto.CurrentCell = gridProduto.Rows[0].Cells[1];
                        //Foca no grid
                        gridProduto.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum SKU encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (gridProduto.SelectedRows.Count == 0)
            {
               
                MessageBox.Show("Nenhum produto selecionado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Foca no campo produto
                txtProduto.Focus();
                return;
            }
            else
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridProduto.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Seta os valores
                codProduto = gridProduto.Rows[indice].Cells[0].Value.ToString();
                descProduto = gridProduto.Rows[indice].Cells[1].Value.ToString();
                separacaoFlowrack = Convert.ToString(gridProduto.Rows[indice].Cells[2].Value);


                //controla  a ação do frame
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                //Fecha a tela
                Close();
            }
        }

    }
}
