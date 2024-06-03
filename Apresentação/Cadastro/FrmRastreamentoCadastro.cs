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
    public partial class FrmRastreamentoCadastro : Form
    {
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmRastreamentoCadastro()
        {
            InitializeComponent();
        }

        private void FrmRastreamentoCadastro_Load(object sender, EventArgs e)
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
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo data inicial
                dtmDataInicial.Focus();
            }
        }

        private void dtmDataInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo data final
                dtmDataFinal.Focus();
            }
        }

        private void dtmDataFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca botão de pesquisa
                btnPesquisar.Focus();
            }
        }

        //Click
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa o produto
            PesqProdutos();
        }

        //Pesquisa o produto
        public void PesqProdutos()
        {
            try
            {
                if (txtCodigo.Text.Equals("") && dtmDataInicial.Value.ToShortDateString().Equals("") && dtmDataFinal.Value.ToShortDateString().Equals(""))
                {
                    MessageBox.Show("Por favor, preencha um dos campos de pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objeto de negocios
                    RastreamentoProdutoNegocios produtoNegocios = new RastreamentoProdutoNegocios();
                    //Instância a coleção 
                    ProdutoCollection produtoCollection = new ProdutoCollection();
                    //A coleção recebe o resultado da consulta
                    produtoCollection = produtoNegocios.PesqProduto(txtCodigo.Text, cmbEmpresa.Text, dtmDataInicial.Value.ToShortDateString(), dtmDataFinal.Value.ToShortDateString());
                    //Limpa o grid
                    gridProduto.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    produtoCollection.ForEach(n => gridProduto.Rows.Add(gridProduto.Rows.Count + 1, n.dataAlteracao, n.loginUsuario, n.codProduto +" - "+ n.descProduto,
                    n.codCategoria +" - "+ n.descCategoria, n.fatorCompra +" "+ n.undCompra, n.fatorPulmao + " " + n.undPulmao, n.fatorPicking + " " + n.undPicking,
                    n.multiploProduto, n.shelfLife, n.tolerancia, n.tipoArmazenagem, n.nivelMaximo, n.tipoPalete, n.lastroPequeno, n.alturaPequeno,
                    n.lastroMedio, n.alturaMedio, n.lastroGrande, n.alturaGrande, n.lastroBlocado, n.alturaBlocado, n.auditaFlowrack, n.controlaValidade,
                    n.paleteBlocado, n.paletePadrao, n.separacaoFlowrack, n.status));

                    if (produtoCollection.Count == 0)
                    {
                        MessageBox.Show("Produto não encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Exibe a quantidade de endereços do pulmão 
                        lblQtd.Text = gridProduto.Rows.Count.ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
