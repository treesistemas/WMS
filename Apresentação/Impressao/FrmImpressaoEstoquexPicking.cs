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
using Wms.Relatorio;

namespace Wms.Impressao
{
    public partial class FrmImpressaoEstoquexPicking : Form
    {
        public List<Empresa> empresaCollection;


        public FrmImpressaoEstoquexPicking()
        {
            InitializeComponent();

        }

        private void FrmImpressaoEstoquexPicking_Load(object sender, EventArgs e)
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

        

        private void rbtComEstoque_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtComEstoque.Checked == true)
            {
                rbtComEstoque.BackColor = Color.DimGray;
                rbtComEstoque.ForeColor = Color.White;

                rbtSemEstoque.BackColor = Color.White;
                rbtSemEstoque.ForeColor = Color.Black;

                chkSemPicking.BackColor = Color.MediumSeaGreen;
                chkSemPicking.ForeColor = Color.White;

                chkComPicking.BackColor = Color.White;
                chkComPicking.ForeColor = Color.Black;

                chkSemPicking.Checked = true;
                chkComPicking.Checked = false;

                //chkSemPicking.Enabled = true;
                //chkComPicking.Enabled = false;

                cmbTipoPicking.Enabled = false;
            }
        }

        private void rbtSemEstoque_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSemEstoque.Checked == true)
            {
                rbtSemEstoque.BackColor = Color.DimGray;
                rbtSemEstoque.ForeColor = Color.White;
                rbtComEstoque.BackColor = Color.White;
                rbtComEstoque.ForeColor = Color.Black;

                chkComPicking.BackColor = Color.Orange;
                chkComPicking.ForeColor = Color.White;

                chkSemPicking.BackColor = Color.White;
                chkSemPicking.ForeColor = Color.Black;

                chkComPicking.Checked = true;
                chkSemPicking.Checked = false;

                //chkComPicking.Enabled = true;
                //chkSemPicking.Enabled = false;

                cmbTipoPicking.Enabled = true;
            }

        }

        private void cmbCategoria_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbCategoria.Items.Count == 0)
            {
                //Preenche o combobox categoria
                PesqCategorias();
            }
        }

        private void btnAnalisar_Click(object sender, EventArgs e)
        {
            //Gera a impressão
            GerarImpressao();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Pesquisa as categorias cadastradas
        private void PesqCategorias()
        {
            try
            {
                //Instância a coleção
                CategoriaCollection categoriaCollection = new CategoriaCollection();
                //Instância o negocios
                CategoriaNegocios categoriaNegocios = new CategoriaNegocios();
                //Limpa o combobox de cadastro
                cmbCategoria.Items.Clear();
                //Adiciona
                cmbCategoria.Items.Add("SELECIONE");

                //Preenche a coleção com a pesquisa
                categoriaCollection = categoriaNegocios.PesqCategoria();
                //Preenche o combobox categoria de cadastro
                categoriaCollection.ForEach(n => cmbCategoria.Items.Add(n.descCategoria));

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }


        //Gera a impressão por resumo
        private void GerarImpressao()
        {
            try
            {
                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    if (rbtComEstoque.Checked == true)
                    {
                        //Instância o relatório
                        FrmEstoqueSemPicking frame = new FrmEstoqueSemPicking();
                        frame.GerarRelatorio(cmbCategoria.Text);
                        //Exibe o relatório
                        frame.Show();
                    }

                    if (rbtSemEstoque.Checked == true)
                    {
                        string tipo = null;

                        if(cmbTipoPicking.Text.Equals("SELECIONE"))
                        {
                            tipo = "TODOS";
                        }

                        if (cmbTipoPicking.Text.Equals("GRANDEZA"))
                        {
                            tipo = "CAIXA";
                        }

                        if (cmbTipoPicking.Text.Equals("FLOW RACK"))
                        {
                            tipo = "FLOWRACK";
                        }

                        //Instância o relatório
                        FrmPickingSemEstoque frame = new FrmPickingSemEstoque();
                        frame.GerarRelatorio(tipo);
                        //Exibe o relatório
                        frame.Show();
                    }

                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o relatório! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}