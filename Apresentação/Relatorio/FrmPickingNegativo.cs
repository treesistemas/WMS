using Negocios.Impressao;
using ObjetoTransferencia.Impressao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wms.Relatorio
{
    public partial class FrmPickingNegativo : Form
    {
        public FrmPickingNegativo()
        {
            InitializeComponent();
        }

        private void FrmPickingNegativo_Load(object sender, EventArgs e)
        {
            //GerarRelatorio();// "TODOS", "4", "2", "","","","","Todos");
        }

        public void GerarRelatorio(int numeroRegiao, int numeroRua)
        {
            try
            {
                //Instância o relatório
                RelEstoqueNegativo endereco = new RelEstoqueNegativo();

                //Instância a camada de Negocios
                ImpressaoPickingNegativoNegocios enderecoNegocios = new ImpressaoPickingNegativoNegocios();

               //Instância a camada de objêto
                EnderecoCollection enerecoCollection = new EnderecoCollection();
                //Passa os dados da pesquisa
                enerecoCollection = enderecoNegocios.PesqPickingNegativo(numeroRegiao, numeroRua);// tipoEndereco, Convert.ToInt32(numeroRegiao), Convert.ToInt32(numeroRua), numeroBloco, numeroNivel, status, disponibilidade, lado);

                if (enerecoCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    endereco.Database.Tables["Endereco"].SetDataSource(enerecoCollection);

                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = endereco;
                    crystalReportViewer1.RefreshReport();
                    Show();
                }
                else
                {
                    MessageBox.Show("Nenhuma informação encontrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o relatório de pickings negativos! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
