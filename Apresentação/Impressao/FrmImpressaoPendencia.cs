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
using Wms.Relatorio;

namespace Wms
{
    public partial class FrmImpressaoPendencia : Form
    {

        public List<Empresa> empresaCollection;

        public FrmImpressaoPendencia()
        {
            InitializeComponent();
        }

        private void FrmImpressaoPendencia_Load(object sender, EventArgs e)
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
                    FrmMapaPendencia frame = new FrmMapaPendencia();
                    frame.GerarRelatorio(dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), cmbEmpresa.Text);
                    //Exibe o relatório
                    frame.Show();
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar às pendências! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

      
    }
}
