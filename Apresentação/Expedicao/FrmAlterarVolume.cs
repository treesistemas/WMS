using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilitarios;

namespace Wms
{
    public partial class FrmAlterarVolume : Form
    {
        //Perfíl do usuário
        public string perfilUsuario;
        //Nome Empresa
        public string empresa;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmAlterarVolume()
        {
            InitializeComponent();
        }

        private void FrmAlterarVolume_Load(object sender, EventArgs e)
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

        private void txtPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (chkReduzir.Checked == true)
                {
                    btnGerar.Focus();
                }
                else
                {
                    txtProduto.Focus();
                }
            }
        }

        private void txtProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtProduto.Text.Length == 0)
                {
                    //Foca no campo 
                    btnGerar.Focus();
                }
                else
                {
                    //Instância o negocios
                    ProdutoNegocios produtoNegocios = new ProdutoNegocios();
                    //Instância a coleção
                    ProdutoCollection produtoCollection = new ProdutoCollection();
                    //A coleção recebe o resultado da consulta
                    produtoCollection = produtoNegocios.PesqProduto(cmbEmpresa.Text,"", txtProduto.Text);

                    if (produtoCollection.Count > 0)
                    {
                        //Pesquisa
                        lblDescProduto.Text = produtoCollection[0].descProduto;
                        //Foca no campo 
                        btnGerar.Focus();
                    }
                    else
                    {
                        txtProduto.SelectAll();

                        MessageBox.Show("Produto não encontrado", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void txtProduto_TextChanged(object sender, EventArgs e)
        {
            if (txtProduto.Text.Equals(""))
            {
                lblDescProduto.Text = "-";
            }
        }

        private void chkReduzir_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReduzir.Checked == true)
            {
                txtProduto.Enabled = false;
                txtProduto.Text = string.Empty;
                lblDescProduto.Text = "-";
                txtPedido.Focus();
                btnGerar.Text = "Reduzir Volume";
            }

            if (chkReduzir.Checked == false)
            {
                txtProduto.Enabled = true;
                txtPedido.Focus();
                btnGerar.Text = "Gerar Volume";
            }
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            if (chkReduzir.Checked == true)
            {
                ReduzirVolume();
            }
            else
            {
                //Gera o volume
                GerarVolume();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GerarVolume()
        {
            try
            {
                //Instância a camada de negocios
                AlterarVolumeNegocios volumeNegocios = new AlterarVolumeNegocios();
                //instância a camada de objêto
                ItensFlowRackCollection itensCollection = new ItensFlowRackCollection();
                //Pesquisa
                itensCollection = volumeNegocios.PesqItensFlowRack(txtPedido.Text, txtProduto.Text, cmbEmpresa.Text);

                if (itensCollection.Count == 0)
                {
                    LimparCampos();

                    MessageBox.Show("Não existe item dentro do volume!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (itensCollection.Count == 1)
                {
                    LimparCampos();

                    MessageBox.Show("Só existe um item nesse pedido, operação não pode ser realizada!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (itensCollection.Count > 0)
                {
                    //Recebe a quantidade de volumes para controle
                    int controlaVolume = itensCollection.Count;
                    int novoVolume = 0;
                    bool operacao = true;

                    foreach (var itens in itensCollection)
                    {

                        //Verifica o produto para alterar o volume
                        if (itens.codProduto.Equals(txtProduto.Text))
                        {
                            //Verifica se existe mais de um item dentro do volume
                            if (volumeNegocios.PesqQtdItemVolume(itens.barraVolume) == 1)
                            {
                                operacao = false;
                                MessageBox.Show("Operação não permitida!, existe somente um item dentro do volume!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                            else
                            {
                                novoVolume = itens.numeroVolume;

                                volumeNegocios.AlterarVolume(itens.codPedido, itens.idProduto,
                                itens.codPedido  + string.Format("{0:00}", Convert.ToInt32(novoVolume + 1)), string.Format("{0:00}", Convert.ToInt32(novoVolume + 1)), cmbEmpresa.Text);

                                //Recebe o numero de volume
                                controlaVolume = itens.numeroVolume;
                            }
                        }

                        //Se existir mais volumes 
                        if (controlaVolume < itens.numeroVolume)
                        {
                            novoVolume = itens.numeroVolume;

                            volumeNegocios.AlterarVolume(itens.codPedido, itens.idProduto,
                            itens.codPedido + string.Format("{0:00}", Convert.ToInt32(novoVolume + 1)), string.Format("{0:00}", Convert.ToInt32(novoVolume + 1)), cmbEmpresa.Text);
                        }
                    }

                    LimparCampos();

                    if (operacao == true)
                    {
                        MessageBox.Show("Volume alterado com sucesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ReduzirVolume()
        {
            try
            {
                //Instância a camada de negocios
                AlterarVolumeNegocios volumeNegocios = new AlterarVolumeNegocios();
                //instância a camada de objêto
                ItensFlowRackCollection itensCollection = new ItensFlowRackCollection();
                //Pesquisa
                itensCollection = volumeNegocios.PesqItensVolume(txtPedido.Text, cmbEmpresa.Text);

                if (itensCollection.Count == 0)
                {
                    LimparCampos();

                    MessageBox.Show("Volume não encontrado!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (itensCollection[0].numeroVolume == 1)
                {
                    LimparCampos();
                    MessageBox.Show("O volume um não pode ser reduzido!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (itensCollection.Count > 0)
                {
                    //Recebe a quantidade de volumes para controle
                    int controlaVolume = (itensCollection[0].numeroVolume - 1);

                    foreach (var itens in itensCollection)
                    {
                        volumeNegocios.AlterarVolume(itens.codPedido, itens.idProduto,
                        itens.codPedido + string.Format("{0:00}", Convert.ToInt32(controlaVolume)), string.Format("{0:00}", Convert.ToInt32(controlaVolume)), cmbEmpresa.Text);
                    }

                    LimparCampos();

                    MessageBox.Show("Volume alterado com sucesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LimparCampos()
        {
            txtPedido.Clear();
            txtProduto.Clear();
            lblDescProduto.Text = "-";
            txtPedido.Focus();

        }


    }
}
