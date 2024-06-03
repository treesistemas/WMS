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
    public partial class FrmEdicaoEstoque : Form
    {
        public int codUsuario;
        public string tipoOperacao; //Picking ou Pulmao
        public int codEndereco;
        public string endereco;
        public int idProduto;
        public string codProduto;
        public string descProduto;
        public int qtdCaixa;
        public double pesoCaixa;
        public int estoque;
        public string tipoEstoque;
        public DateTime validade;
        public int capacidade;
        public int abastecimento;
        public string tipoAbastecimento;
        public string lote;
        public double peso;
        public int totalPalete;
        public int vidaUtil;
        public string empresa;

        public FrmEdicaoEstoque()
        {
            InitializeComponent();
        }

        private void FrmEdicaoEstoque_Load(object sender, EventArgs e)
        {
            

            txtCodigo.Text = codProduto;
            lblDescProduto.Text = descProduto;
            lblEstoque.Text = lblEstoque.Text + " (" + tipoEstoque + ")";
            txtEstoque.Text = Convert.ToString(estoque);
            txtVencimento.Text = (validade).ToString("dd/MM/yyyy");
            lblPeso.Text = Convert.ToString(peso);
            txtLote.Text = Convert.ToString(lote);


            lblEndereco.Text = endereco;



            if (tipoOperacao == "Picking")
            {
                lblCapacidade.Text = lblCapacidade.Text + " (" + tipoAbastecimento + ")";
                txtCapacidade.Text = Convert.ToString(capacidade);
                lblAbastecimento.Text = lblAbastecimento.Text + " (" + tipoAbastecimento + ")";
                txtAbastecimento.Text = Convert.ToString(abastecimento);

                lblAbastecimento.Visible = true;
                txtAbastecimento.Visible = true;
                lblCapacidade.Visible = true;
                txtCapacidade.Visible = true;
            }
        }

        //KeyPress
        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtVencimento.Focus();
            }
        }

        private void txtValidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtLote.Focus();
            }
        }

        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                if (tipoOperacao == "Picking")
                {
                    txtCapacidade.Focus();
                }
                else
                {
                    btnSalvar.Focus();
                }
            }
        }

        private void txtCapacidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtAbastecimento.Focus();
            }
        }

        private void txtAbastecimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSalvar.Focus();
            }
        }

        //Changed
        private void txtEstoque_TextChanged(object sender, EventArgs e)
        {
            calculaPeso();//Calcula o peso do produto
        }

        //Click
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            EdicaoEstoque();//Edita o estoque
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Fecha o frame
            Close();
        }


        //Salva a edição de estoque
        private void EdicaoEstoque()
        {
            try
            {
                //instância a camda de objêto
                EstoqueNegocios estoqueNegocios = new EstoqueNegocios();

                if (tipoOperacao == "Picking")
                {
                    if (Convert.ToDateTime(txtVencimento.Text) <= DateTime.Now.Date)
                    {
                        MessageBox.Show("O vencimento digitado não pode ser menor ou igual \na data atual!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (Convert.ToDateTime(txtVencimento.Text) > DateTime.Now.Date.AddDays(vidaUtil))
                    {
                        MessageBox.Show("O vencimento digitado não pode ultrapassar \na vida útil do produto!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }                    
                    else
                    {
                        estoqueNegocios.EdicaoEstoquePicking(codUsuario, idProduto, codEndereco, Convert.ToInt32(txtEstoque.Text), Convert.ToDateTime(txtVencimento.Text), Convert.ToInt32(txtCapacidade.Text), Convert.ToInt32(txtAbastecimento.Text), txtLote.Text, Convert.ToDouble(lblPeso.Text), empresa);
                        
                    }
                }
                else
                {
                    if (Convert.ToInt32(txtEstoque.Text) == 0)
                    {
                        MessageBox.Show("O estoque não pode ser zerado!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (totalPalete < (Convert.ToInt32(txtEstoque.Text)))
                    {
                        MessageBox.Show("A quantidade digitada não pode ser maior \nque a quantidade do palete padrão!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }                    
                    else if (Convert.ToDateTime(txtVencimento.Text) <= DateTime.Now.Date)
                    {
                        MessageBox.Show("O vencimento digitado não pode ser menor ou igual \na data atual!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (Convert.ToDateTime(txtVencimento.Text) > DateTime.Now.Date.AddDays(vidaUtil))
                    {
                        MessageBox.Show("O vencimento digitado não pode ultrapassar \na vida útil do produto!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        estoqueNegocios.EdicaoEstoquePulmao(codUsuario, idProduto, codEndereco, (Convert.ToInt32(txtEstoque.Text) * qtdCaixa), Convert.ToDateTime(txtVencimento.Text), txtLote.Text, Convert.ToDouble(lblPeso.Text) / (Convert.ToInt32(txtEstoque.Text) * qtdCaixa), empresa);
                    }
                }

                MessageBox.Show("Estoque editado com sucesso!", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Fecha o frame
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void calculaPeso()
        {
            try
            {
                double r = 0;

                if (txtEstoque.Text != "0" && peso != 0 && qtdCaixa != 0)
                {
                    if (txtEstoque.Text != "")
                    {
                        if (tipoOperacao == "Picking")
                        {
                            //Unidade
                            r = (Convert.ToDouble(txtEstoque.Text) * (pesoCaixa / qtdCaixa));
                        }
                        else
                        {
                            //Caixa ou unidade
                            r = (Convert.ToDouble(txtEstoque.Text) * qtdCaixa) * (pesoCaixa / qtdCaixa);
                        }
                    }
                }

                lblPeso.Text = string.Format(@"{0:N}", r);
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor, verifique os valores informados! ", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }


    }
}
