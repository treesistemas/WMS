using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmPesqUsuario : Form
    {
        public int codUsuario;
        public string nmUsuario;
        public string perfilUsuario;
        public string empresa;

        public FrmPesqUsuario()
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
            pesqUsuario();
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

        private void pesqUsuario()
        {
            try
            {
                //Instância o negocios
                UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                //Instância a coleçãO
                UsuarioCollection usuarioCollection = new UsuarioCollection();
                //A coleção recebe o resultado da consulta
                usuarioCollection = usuarioNegocios.PesqUsuario("", txtUsuario.Text, perfilUsuario, empresa);
                //Limpa o grid
                gridUsuario.Rows.Clear();
                //Grid Recebe o resultado da coleção
                usuarioCollection.ForEach(n => gridUsuario.Rows.Add(n.codUsuario, n.login, n.perfil));

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
                codUsuario = Convert.ToInt32(gridUsuario.Rows[indice].Cells[0].Value.ToString());
                nmUsuario = gridUsuario.Rows[indice].Cells[1].Value.ToString();

                //controla  a ação do frame
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                //Fecha a tela
                Close();
            }
        }

    }
}
