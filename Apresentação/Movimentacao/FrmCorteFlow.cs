using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmCorteFlow : Form
    {
       
        public string sku;
        public int quantidade;
        public int quantidadeCorte;
        public int estoque;
        public int resultado;

        public FrmCorteFlow()
        {
            InitializeComponent();
        }

        //Load
        private void FrmCorteFlow_Load(object sender, EventArgs e)
        {            
            lblSKU.Text = sku;
            txtQuantidade.Text = quantidade.ToString();
            txtCorte.Text = quantidade.ToString();
            lblqtdEstoque.Text = estoque.ToString();
        }

        //KeyPress
        private void txtCorte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnConfirmar.Focus();
            }
        }

        //Changed
        private void txtCorte_TextChanged(object sender, EventArgs e)
        {
            //Calcula a corte
            CalcularCorte();
        }

        //Click
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            //Instância o frame de mensagem
            FrmMensagem mensagem = new FrmMensagem();

            if (Convert.ToInt32(txtQuantidade.Text) < Convert.ToInt32(txtCorte.Text))
            {
                mensagem.Texto("O CORTE NÃO PODE SER MAIOR QUE A QUANTIDADE EXISTENTE!", "RED");
                //Abre o frame
                mensagem.ShowDialog();
            }
           else
            {

                resultado = Convert.ToInt32(txtResultado.Text);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                
                //Exibe a mensagem
                mensagem.Texto("CORTE REALIZADO COM SUCESSO!", "Blue");
                //Abre o frame
                mensagem.ShowDialog();

                //Fecha tela
                Close();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha tela
            Close();
        }

        private void CalcularCorte()
        {
            try
            {
                if (txtCorte.Text == "")
                {
                    txtResultado.Text = "0";
                }
                else if (Convert.ToInt32(txtQuantidade.Text) < Convert.ToInt32(txtCorte.Text))
                {
                    txtResultado.Text = "0";
                }
                else if (Convert.ToInt32(txtCorte.Text) > 0)
                {
                    int r = (Convert.ToInt32(txtQuantidade.Text) - Convert.ToInt32(txtCorte.Text));

                    txtResultado.Text = string.Format(@"{0:00}", r);
                }
                
                
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor, verifique os valores informados! ", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

        }

    }
}
