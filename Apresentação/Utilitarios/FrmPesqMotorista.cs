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
using System.Web.ModelBinding;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmPesqMotorista : Form
    {
        public int codMotorista;
        public string nmMotorista;
        public string apelidoMotorista;
        public string empresa;

        public FrmPesqMotorista()
        {
            InitializeComponent();
        }

        private void txtSeparador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão pesquisar
                btnPesquisar.Focus();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa o usuário
            pesqSeparador();
        }

        private void gridUsuario_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Trânsfere os dados selecionados
            transfereDados();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Trânsfere os dados selecionados
            transfereDados();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o frame
            Close();
        }

        private void pesqSeparador()
        {
            try
            {
                //Instância o negocios
                MotoristaNegocios motoristaNegocios = new MotoristaNegocios();
                //Instância a coleçãO
                MotoristaCollection motoristaCollection = new MotoristaCollection();
                //A coleção recebe o resultado da consulta
                motoristaCollection = motoristaNegocios.PesqMotorista(empresa, "", txtUsuario.Text, true);
                //Limpa o grid
                gridUsuario.Rows.Clear();
                //Grid Recebe o resultado da coleção
                motoristaCollection.ForEach(n => gridUsuario.Rows.Add(n.codMotorista, n.apelidoMotorista, n.nomeMotorista));

                //Total encontrado
                lblTotal.Text = gridUsuario.RowCount.ToString();

                if (gridUsuario.RowCount > 0)
                {
                    //Seleciona a primeira linha do grid
                    gridUsuario.CurrentCell = gridUsuario.Rows[0].Cells[1];
                    //Foca no grid
                    gridUsuario.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum usuário encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Trânsfere os dados selecionados
        private void transfereDados()
        {
            if (gridUsuario.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum uauário selecionado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Foca no campo fornecedor
                txtUsuario.Focus();
                return;
            }
            else
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridUsuario.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Seta os valores
                codMotorista = Convert.ToInt32(gridUsuario.Rows[indice].Cells[0].Value.ToString());
                apelidoMotorista = gridUsuario.Rows[indice].Cells[1].Value.ToString();

                //controla  a ação do frame
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                //Fecha a tela
                Close();
            }
        }

    }
}
