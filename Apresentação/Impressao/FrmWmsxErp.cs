using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;
using Utilitarios;

namespace Wms.Impressao
{
    public partial class FrmWmsxErp : Form
    {
        //Array com id
        private int[] regiao;
        private int[] rua;

        public string usuario;

        public FrmWmsxErp()
        {
            InitializeComponent();
        }

        private void cmbTipo_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbTipo.Text.Equals("ENDERECO"))
            {
                cmbRegiao.Enabled = true;
                cmbRua.Enabled = true;
                cmbLado.Enabled = true;
            }
            else
            {
                cmbRegiao.Enabled = false;
                cmbRua.Enabled = false;
                cmbLado.Enabled = false;
            }
        }

        private void cmbRegiao_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbRegiao.Items.Count == 0)
            {
                PesqRegiao(cmbRegiao);
            }
        }

        private void txtProduto_TextChanged(object sender, EventArgs e)
        {
            if (txtProduto.Text.Equals(""))
            {
                lblDescProduto.Text = "-";
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.Text.Equals("ENDERECO"))
            {
                txtProduto.Enabled = false;
                txtProduto.Clear();
                cmbRegiao.Enabled = true;
                cmbRua.Enabled = true;
                cmbLado.Enabled = true;
            }
            else if (cmbTipo.Text.Equals("PRODUTO"))
            {
                txtProduto.Enabled = true;
                cmbRegiao.Enabled = false;
                cmbRua.Enabled = false;
                cmbLado.Enabled = false;
                cmbRegiao.Items.Clear();
                cmbRua.Items.Clear();
                cmbLado.Text = "TODOS";
            }
            else
            {
                cmbRegiao.Enabled = false;
                cmbRua.Enabled = false;
                cmbLado.Enabled = false;
            }
        }

        private void cmbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa a rua
            PesqRua(cmbRua, cmbRegiao, regiao);
        }

        private void txtProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqProduto frameProduto = new FrmPesqProduto();

                //Recebe as informações
                if (frameProduto.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe os valores novos
                    lblDescProduto.Text = frameProduto.codProduto;
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
                    btnAnalisar.Focus();
                }
                else
                {
                    //Instância o negocios
                    ProdutoNegocios produtoNegocios = new ProdutoNegocios();
                    //Instância a coleção
                    ProdutoCollection produtoCollection = new ProdutoCollection();
                    //A coleção recebe o resultado da consulta
                    produtoCollection = produtoNegocios.PesqProduto(null,"", txtProduto.Text);

                    if (produtoCollection.Count > 0)
                    {
                        //Pesquisa
                        lblDescProduto.Text = produtoCollection[0].descProduto;
                    }
                    else
                    {
                        MessageBox.Show("Produto não encontrado", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }

        private void btnAnalisar_Click(object sender, EventArgs e)
        {
            Relatorio.FrmWmsxErp frame = new Relatorio.FrmWmsxErp();

            int regiao = 0;
            int rua = 0;
            string lado = null;

            if (cmbRegiao.Text.Equals("SELEC") || cmbRegiao.Text.Equals(string.Empty))
            {
                regiao = 0;
            }
            else
            {
                regiao = Convert.ToInt32(cmbRegiao.Text);
            }

            if (cmbRua.Text.Equals("SELEC") || cmbRua.Text.Equals(string.Empty))
            {
                rua = 0;
            }
            else
            {
                rua = Convert.ToInt32(cmbRua.Text);
            }

            if (!cmbLado.Text.Equals("SELEC") || !cmbLado.Text.Equals(string.Empty))
            {
                if (cmbLado.Text.Equals("PAR"))
                {
                    lado = "Par";
                }

                if (cmbLado.Text.Equals("IMPAR"))
                {
                    lado = "Impar";
                }

                if (cmbLado.Text.Equals("TODOS"))
                {
                    lado = "TODOS";
                }
            }

            frame.GerarRelatorio(cmbTipo.Text, regiao, rua, lado, txtProduto.Text, usuario);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Pesquisa região
        private void PesqRegiao(ComboBox cmbRegiao)
        {
            try
            {
                cmbRegiao.Text = "SELEC";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Limpa o combobox regiao
                cmbRegiao.Items.Clear();
                //Preenche a coleção com apesquisa
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRegiao("");
                //Preenche o combobox região
                gerarEnderecoCollection.ForEach(n => cmbRegiao.Items.Add(n.numeroRegiao));

                if (cmbRegiao.Name == "cmbRegiao")
                {


                    //Define o tamanho do array para o combobox
                    regiao = new int[gerarEnderecoCollection.Count];

                    for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                    {
                        //Preenche o array combobox
                        regiao[i] = gerarEnderecoCollection[i].codRegiao;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa rua combobox
        private void PesqRua(ComboBox cmbRua, ComboBox cmbRegiao, int[] idRegiao)
        {
            try
            {
                //Limpa o combobox rua inícial
                cmbRua.Items.Clear();
                //Adiciona o texto
                cmbRua.Text = "SELEC";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa as ruas da região selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRua(idRegiao[cmbRegiao.SelectedIndex]);
                //Preenche o combobox rua
                gerarEnderecoCollection.ForEach(n => cmbRua.Items.Add(n.numeroRua));
                //Define o tamanho do array


                if (cmbRua.Name == "cmbRua")
                {
                    rua = new int[gerarEnderecoCollection.Count];

                    for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                    {
                        //Preenche o array
                        rua[i] = gerarEnderecoCollection[i].codRua;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

    }
}
