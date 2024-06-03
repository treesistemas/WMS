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

namespace Wms
{
    public partial class FrmImpressaoRendimentoExpedicao : Form
    {
        public FrmImpressaoRendimentoExpedicao()
        {
            InitializeComponent();

            txtDataInicial.Text = DateTime.Now.ToShortDateString() + "00:00:00".Replace("/",".");
            txtDataFinal.Text = DateTime.Now.ToShortDateString() + "23:59:59".Replace("/", ".");
        }

        private void txtDataInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtDataFinal.Focus();
            }
        }

        private void txtDataFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAnalisar.Focus();
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

        //Gera a impressão por resumo
        private void GerarImpressao()
        {
            try
            {
                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância o relatório
                    FrmRendimentoExpedicao frame = new FrmRendimentoExpedicao();
                    frame.GerarRelatorio(txtDataInicial.Text, txtDataFinal.Text);
                    //Exibe o relatório
                    frame.Show();
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o relatório da expedição! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
