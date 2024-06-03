using Negocios;
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
    public partial class FrmBloqueiaEstoque : Form
    {
        public int codUsuario;
        public int idProduto;
        public string bloqueia;
        public int[] codEndereco;
        public string[] endereco;

        public FrmBloqueiaEstoque()
        {
            InitializeComponent();
        }

        private void FrmBloqueiaEstoque_Load(object sender, EventArgs e)
        {
            //Exibição do texto no frame
            if (bloqueia.Equals("SIM"))
            {
                lblTexto.Text = "Bloqueio de Estoque";
            }
            else if (bloqueia.Equals("NAO"))
            {
                lblTexto.Text = "Desbloqueio de Estoque";
            }

            //Passa os endereços para o combobox
            for (int i = 0; endereco.Count() > i; i++)
            {
                cmbEndereco.Items.Add(endereco[i]);
            }

            cmbEndereco.SelectedIndex = 0; //Seleciona o primeiro endereço

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            BloqueiaDesbloqueia(); //Bloqueia ou desbloqueia os endereços selecionados
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close(); //Fecha o frame
        }

        //Bloqueia ou desbloqueia os endereços selecionados
        private void BloqueiaDesbloqueia()
        {
            try
            {
                if (cmbMotivo.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, selecione um motivo!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    //instância a camda de objêto
                    EstoqueNegocios estoqueNegocios = new EstoqueNegocios();

                    for (int i = 0; cmbEndereco.Items.Count > i; i++)
                    {
                        if (bloqueia.Equals("SIM"))
                        {
                            //Pesquisa o produto e adiciona na coleção
                            estoqueNegocios.BloqueiaEstoque(codUsuario, idProduto, codEndereco[i], cmbMotivo.Text, "True");
                        }
                        else if (bloqueia.Equals("NAO"))
                        {
                            //Pesquisa o produto e adiciona na coleção
                            estoqueNegocios.BloqueiaEstoque(codUsuario, idProduto, codEndereco[i], "", "False");
                        }

                    }

                    MessageBox.Show("Operação realizada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
