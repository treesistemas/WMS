using System;
using System.Windows.Forms;
using Wms.Relatorio;

namespace Wms
{
    public partial class FrmImpressaoRendimentoFlowRack : Form
    {
        public FrmImpressaoRendimentoFlowRack()
        {
            InitializeComponent();
        }

        private void dtmInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmFinal.Focus();
            }
        }

        private void dtmFinal_KeyPress(object sender, KeyPressEventArgs e)
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
                    FrmRendimentoFlowRack frame = new FrmRendimentoFlowRack();
                    frame.GerarRelatorioRendimento(dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString());
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o rendimento do flow rack! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
