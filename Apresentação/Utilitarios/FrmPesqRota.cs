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
    public partial class FrmPesqRota : Form
    {
        public string codRota;
        public string descRota;
        public string empresa;

        public FrmPesqRota()
        {
            InitializeComponent();
        }

        private void txtRota_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botãor
                btnPesquisar.Focus();
            }
        }

        private void gridRota_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Trânsfere os dados selecionados
            TransferirDados();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa a rota
            PesqRota(txtRota.Text, 0);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Trânsfere os dados selecionados
            TransferirDados();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //fechar
            Close();
        }

        private void PesqRota(string rota, int codRota)
        {
            try
            {
                //Instância o negocios
                RotaNegocios rotaNegocios = new RotaNegocios();
                //Instância a coleção
                RotaCollection rotaCollection = new RotaCollection();
                //A coleção recebe o resultado da consulta
                rotaCollection = rotaNegocios.PesqRota(rota, codRota, empresa);
                //Limpa o grid
                gridRota.Rows.Clear();
                //Grid Recebe o resultado da coleção
                rotaCollection.ForEach(n => gridRota.Rows.Add(n.codRota, n.descRota));

                //Total encontrado
                lblTotal.Text = gridRota.RowCount.ToString();

                if (gridRota.RowCount > 0)
                {
                    //Seleciona a primeira linha do grid
                    gridRota.CurrentCell = gridRota.Rows[0].Cells[1];
                    //Foca no grid
                    gridRota.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhuma rota encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Trânsfere os dados selecionados
        private void TransferirDados()
        {
            if (gridRota.SelectedRows.Count == 0)
            {

                MessageBox.Show("Nenhuma rota selecionada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Foca no campo produto
                txtRota.Focus();
                return;
            }
            else
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridRota.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Seta os valores
                codRota = gridRota.Rows[indice].Cells[0].Value.ToString();
                descRota = gridRota.Rows[indice].Cells[1].Value.ToString();

                //controla  a ação do frame
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                //Fecha a tela
                Close();
            }
        }

    }
}