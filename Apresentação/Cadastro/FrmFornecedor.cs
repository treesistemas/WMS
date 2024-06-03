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
    public partial class FrmFornecedor : Form
    {
        //instância a camada de objêto
        RepresentanteCollection representanteCollection = new RepresentanteCollection();

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmFornecedor()
        {
            InitializeComponent();
        }

        private void FrmFornecedor_Load(object sender, EventArgs e)
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

        private void txtPesqCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //foca no campo fornecedor
                txtPesqFornecedor.Focus();
            }
        }

        private void txtPesqFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //foca no botão pesquisar
                btnPesquisar.Focus();
            }
        }

        private void gridFornecedor_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        private void gridFornecedor_MouseClick(object sender, MouseEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa o fornecedor
            PesqFornecedor();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o Form
            Close();
        }

        private void PesqFornecedor()
        {
            try
            {
                //instância a camada de negocios
                FornecedorNegocios fornecedorNegocios = new FornecedorNegocios();
                //instância a camada de objêto
                FornecedorCollection fornecedorCollection = new FornecedorCollection();
                //Pesquisa
                fornecedorCollection = fornecedorNegocios.pesqFornecedor(cmbEmpresa.Text, txtPesqCodigo.Text, txtPesqFornecedor.Text, chkPesqAtivo.Checked);
                //Limpa o grid
                gridFornecedor.Rows.Clear();
                //Preenche o grid
                fornecedorCollection.ForEach(n => gridFornecedor.Rows.Add(n.codFornecedor, n.nomeFornecedor, n.enderecoFornecedor, n.ufFornecedor, n.cidadeFornecedor, n.bairroFornecedor, n.foneFornecedor, n.emailFornecedor, n.statusFornecedor));

                if (fornecedorCollection.Count > 0)
                {
                    //Quantidade de fornecedor
                    lblQtd.Text = gridFornecedor.Rows.Count.ToString();
                    //Exibe os dados nos campos
                    DadosCampos();
                    //foca no grid
                    gridFornecedor.Focus();
                    //pesquisa o representante
                    PesqRepresentante();
                }
                else
                {
                    MessageBox.Show("Nenhum fornecedor encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DadosCampos()
        {
            try
            {
                if (gridFornecedor.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridFornecedor.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valores do grid
                    txtCodigo.Text = gridFornecedor.Rows[indice].Cells[0].Value.ToString();
                    txtFornecedor.Text = Convert.ToString(gridFornecedor.Rows[indice].Cells[1].Value);
                    txtEndereco.Text = Convert.ToString(gridFornecedor.Rows[indice].Cells[2].Value);
                    txtUF.Text = Convert.ToString(gridFornecedor.Rows[indice].Cells[3].Value);
                    txtCidade.Text = Convert.ToString(gridFornecedor.Rows[indice].Cells[4].Value);
                    txtBairro.Text = Convert.ToString(gridFornecedor.Rows[indice].Cells[5].Value);
                    txtFone.Text = Convert.ToString(gridFornecedor.Rows[indice].Cells[6].Value);
                    txtEmail.Text = Convert.ToString(gridFornecedor.Rows[indice].Cells[7].Value);
                    chkAtivo.Checked = Convert.ToBoolean(gridFornecedor.Rows[indice].Cells[8].Value);

                    if (representanteCollection.Count > 0)
                    {
                        //Exibe os dados nos campos
                        DadosRepresentanteCampos();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PesqRepresentante()
        {
            try
            {
                //instância a camada de negocios
                FornecedorNegocios fornecedorNegocios = new FornecedorNegocios();

                //Pesquisa
                representanteCollection = fornecedorNegocios.pesqRepresentante(cmbEmpresa.Text, txtPesqCodigo.Text, txtPesqFornecedor.Text, chkPesqAtivo.Checked);


                if (representanteCollection.Count > 0)
                {
                    //Exibe os dados nos campos
                    DadosRepresentanteCampos();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DadosRepresentanteCampos()
        {
            try
            {

                //Instância as linha da tabela
                DataGridViewRow linhaForn = gridFornecedor.CurrentRow;
                //Recebe o indice   
                int indiceForn = linhaForn.Index;

                //Localizando os representantes
                List<Representante> representantes = representanteCollection.FindAll(delegate (Representante n) { return n.codFornecedor == Convert.ToInt32(gridFornecedor.Rows[indiceForn].Cells[0].Value); }).OrderBy(c => c.nomeRepresentante).ToList();
                //Limpa o grid
                gridRepresentante.Rows.Clear();
                //Preenche o grid
                representantes.ForEach(n => gridRepresentante.Rows.Add(n.codRepresentante, n.nomeRepresentante, n.foneRepresentante, n.emailRepresentante, n.codFornecedor));

                if (representantes.Count > 0)
                {

                    //Instância as linha da tabela
                    DataGridViewRow linha = gridRepresentante.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valores do grid
                    txtCelRepresentante.Text = Convert.ToString(gridRepresentante.Rows[indice].Cells[2].Value);
                    txtEmailRepresentante.Text = Convert.ToString(gridRepresentante.Rows[indice].Cells[3].Value);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
