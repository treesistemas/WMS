using Negocios;
using Negocios.Impressao;
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

namespace Wms.Impressao
{
    public partial class FrmProdutoFlowRack : Form
    {
        public List<Empresa> empresaCollection;

        public FrmProdutoFlowRack()
        {
            InitializeComponent();
        }

        private void FrmProdutoFlowRack_Load(object sender, EventArgs e)
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

        private void txtProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Pesquisa o produto no flow rack
                PesqProdutoFlowRack();
            }
        }

        private void dtmData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmHora.Focus();
            }
        }

        private void dtmHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtProduto.Focus();
            }
        }

        private void chkData_CheckedChanged(object sender, EventArgs e)
        {
            if (chkData.Checked == true)
            {
                //Habilita os campos
                dtmData.Enabled = true;
                dtmHora.Enabled = true;

                txtProduto.Focus();
            }
            else
            {
                //Desabilita os campos
                dtmData.Enabled = false;
                dtmHora.Enabled = false;

                txtProduto.Focus();
            }
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Pesquisa o produto no flow rack
        private void PesqProdutoFlowRack()
        {
            try
            {
                //Instância a camada de negocios
                ProdutoFlowRackNegocios produtNegocios = new ProdutoFlowRackNegocios();
                //Instância o objêto
                ItensFlowRack itensFlowRack = new ItensFlowRack();
                //A coleção recebe o resultado da consulta
                itensFlowRack = produtNegocios.PesqProdutoFlowRack(txtProduto.Text, dtmData.Text + " " + dtmHora.Text, chkData.Checked, cmbEmpresa.Text);

                //Verifica se o produto existe
                if (itensFlowRack.idProduto > 0)
                {
                    //Verifica se o item já existe
                    for(int i = 0; gridProduto.Rows.Count > i; i++)
                    {
                        if (Convert.ToInt32(gridProduto.Rows[i].Cells[1].Value) == itensFlowRack.idProduto)
                        {
                            gridProduto.Rows.RemoveAt(i);
                        }
                    }

                    //Insere o cadastro no grid
                    gridProduto.Rows.Add((gridProduto.Rows.Count + 1), itensFlowRack.idProduto, itensFlowRack.descProduto, itensFlowRack.qtdProduto);
                    //Seleciona a linha      
                    gridProduto.CurrentCell = gridProduto.Rows[gridProduto.Rows.Count - 1].Cells[0];

                    txtProduto.Text = string.Empty;
                    //Foca no campo
                    txtProduto.Focus();
                }
                else
                {
                    MessageBox.Show("Produto não encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
