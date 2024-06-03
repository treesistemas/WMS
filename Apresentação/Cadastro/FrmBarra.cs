using Negocios;
using ObjetoTransferencia;
using System;
using System.Windows.Forms;
using Wms.Relatorio.DataSet;

namespace Wms
{
    public partial class FrmBarra : Form
    {
        public int idProduto;
        public int codBarra;
        public string numeroBarra;
        public int multiplicador;
        public double altura;
        public double largura;
        public double comprimento;
        public double cubagem;
        public double peso;
        public string empresaSigla;

        public bool opcao;
        public FrmBarra()
        {
            InitializeComponent();
        }

        private void FrmBarra_Load(object sender, EventArgs e)
        {
            //Cadastrar um novo
            if(opcao == true)
            {
                txtCodigoBarra.Enabled = true;
                txtMultiplicador.Enabled = true;

                txtCodigoBarra.Focus();
            }
            else if (opcao == false) //Alterar 
            {
                txtCodigoBarra.Text = numeroBarra;
                txtMultiplicador.Text = Convert.ToString(multiplicador);
                txtAltura.Text = string.Format(@"{0:N}", altura);
                txtLargura.Text = string.Format(@"{0:N}", largura);
                txtComprimento.Text = string.Format(@"{0:N}", comprimento);
                txtPeso.Text = string.Format(@"{0:0.000}", peso);

                //Foca no campo
                txtAltura.Focus();
            }

            

        }

        private void txtCodigoBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtMultiplicador.Focus();
            }
        }

        private void txtMultiplicador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtAltura.Focus();
            }
        }

        private void txtAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtLargura.Focus();
            }
        }

        private void txtLargura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtComprimento.Focus();
            }
        }

        private void txtComprimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                txtPeso.Focus();
            }
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo
                btnSalvar.Focus();
            }
        }

        private void txtAltura_TextChanged(object sender, EventArgs e)
        {
            //Calcula a cubagem
            CalcularCubagem();
        }

        private void txtLargura_TextChanged(object sender, EventArgs e)
        {
            //Calcula a cubagem
            CalcularCubagem();
        }

        private void txtComprimento_TextChanged(object sender, EventArgs e)
        {
            //Calcula a cubagem
            CalcularCubagem();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //Salva os dados logísticos do código de barra
            SalvarBarra();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha a tela
            Close();
        }


        //Salva as informações do código de barra
        private void SalvarBarra()
        {

            try
            {
                if (txtAltura.Text == "" || txtLargura.Text == "" || txtComprimento.Text == "" || txtPeso.Text.Equals(""))
                {
                    MessageBox.Show("Preencha todos os campos!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Convert.ToDouble(txtAltura.Text).Equals(0) || Convert.ToDouble(txtLargura.Text).Equals(0) || Convert.ToDouble(txtComprimento.Text).Equals(0) || Convert.ToDouble(txtPeso.Text).Equals(0))
                {
                    MessageBox.Show("Preencha todos os campos!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objêto
                    ProdutoNegocios produtoNegocios = new ProdutoNegocios();

                    altura = Convert.ToDouble(string.Format("{0:0.00}", Convert.ToDouble(txtAltura.Text) / 100.0));
                    largura = Convert.ToDouble(string.Format("{0:0.00}", Convert.ToDouble(txtLargura.Text) / 100.0)); 
                    comprimento = Convert.ToDouble(string.Format("{0:0.00}", Convert.ToDouble(txtComprimento.Text) / 100.0));
                    cubagem = Convert.ToDouble(string.Format("{0:0.00}", Convert.ToDouble(txtCubagem.Text) / 100.0)); 
                    peso = Convert.ToDouble(txtPeso.Text);

                    if (opcao == true)
                    {
                        //Numero de barra
                        numeroBarra = Convert.ToString(txtCodigoBarra.Text);
                        multiplicador = Convert.ToInt32(txtMultiplicador.Text);

                        //Altera as responsabilidades do empilhador
                        produtoNegocios.SalvarCodBarra(idProduto, numeroBarra, multiplicador, altura, largura, comprimento, cubagem, peso, empresaSigla);
                    }
                    else if (opcao == false)
                    {
                        //Altera as responsabilidades do empilhador
                        produtoNegocios.AlterarCodBarra(idProduto, codBarra, altura, largura, comprimento, cubagem, peso , empresaSigla);
                    }


                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    MessageBox.Show("Dados logísticos salvos com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Fecha tela
                    Close();

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularCubagem()
        {
            try
            {
                if (txtAltura.Text != "0" && txtLargura.Text != "0" && txtComprimento.Text != "0")
                {
                    if (txtAltura.Text != "" && txtLargura.Text != "" && txtComprimento.Text != "")
                    {
                        double r = (Convert.ToDouble(txtAltura.Text) * Convert.ToDouble(txtComprimento.Text)) * Convert.ToDouble(txtLargura.Text);

                        txtCubagem.Text = string.Format(@"{0:0.00000}", r);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor, verifique os valores informados! ", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

        }

        
    }
}