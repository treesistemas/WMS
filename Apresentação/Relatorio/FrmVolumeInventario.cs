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
    public partial class FrmVolumeInventario : Form
    {
        public FrmVolumeInventario()
        {
            InitializeComponent();
        }

        private void FrmVolumeInventario_Load(object sender, EventArgs e)
        {

        }


        public void GerarRelatorio(int codInventario)
        {
            try
            {
                //Instância o relatório
                RelVolumeInventario volume = new RelVolumeInventario();

                //Instância a camada de Negocios
                impressaoContagensNegocios inventariooNegocios = new impressaoContagensNegocios();
                //Instância a camada de objêto
                RelVolumeInventarioCollection volumeCollection = new RelVolumeInventarioCollection();

                //Passa os dados da pesquisa
                volumeCollection = inventariooNegocios.PesqRelatorioVolumeSemContagem(codInventario);

                if (volumeCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    volume.Database.Tables["Volume"].SetDataSource(volumeCollection);

                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = volume;
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
                MessageBox.Show("Ocorreu um erro ao gerar o relatório de endereços! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
