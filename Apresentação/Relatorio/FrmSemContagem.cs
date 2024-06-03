using Negocios.Inventario;
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
    public partial class FrmSemContagem : Form
    {
        public FrmSemContagem()
        {
            InitializeComponent();
        }

        private void FrmSemContagem_Load(object sender, EventArgs e)
        {

        }

        public void GerarRelatorio(int codInventario, string tipo, string contagem)
        {
            try
            {
                //Instância o relatório
                RelSemContagem endereco = new RelSemContagem();

                //Instância a camada de Negocios
                impressaoContagensNegocios inventariooNegocios = new impressaoContagensNegocios();
                //Instância a camada de objêto
                RelItemInventarioCollection itemCollection = new RelItemInventarioCollection();

                if(tipo.Equals("PICKING"))
                {
                    //Passa os dados da pesquisa
                    itemCollection = inventariooNegocios.PesqSemContagemPicking(codInventario, tipo, contagem);
                }
                else if (tipo.Equals("PULMAO"))
                {
                    //Passa os dados da pesquisa
                    itemCollection = inventariooNegocios.PesqSemContagemPulmao(codInventario, tipo, contagem);
                }


                    if (itemCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    endereco.Database.Tables["Contagem"].SetDataSource(itemCollection);

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
                MessageBox.Show("Ocorreu um erro ao gerar o relatório de endereços sem contagem! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
