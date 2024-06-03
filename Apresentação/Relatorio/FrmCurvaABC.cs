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
    public partial class FrmCurvaABC : Form
    {
        public FrmCurvaABC()
        {
            InitializeComponent();
        }

        private void FrmCurvaABC_Load(object sender, EventArgs e)
        {
            //GerarRelatorioCurvaAbcCaixa(1, 12, "01/01/2024", "28/01/2024");

            //GerarRelatorioCurvaAbcFlowRack(100, "01/01/2024", "28/01/2024");
        }

        public void GerarRelatorioCurvaAbcCaixa(int regiao, int rua, string dataInicial, string dataFinal)
        {
            try
            {
                //Instância o relatório
                RelCurvaABC relCurvaAbc = new RelCurvaABC();

                //Instância a camada de Negocios
                ImpressaoCurvaABCNegocios curvaNegocios = new ImpressaoCurvaABCNegocios();

                //Instância a camada de objêto
                ImpressaoCurvaAbcCollection curvaCollection = new ImpressaoCurvaAbcCollection();

                //Exclui a quantidade de dias do final de semana para se ter uma média
                int contDia = 0;

                //Calcula a quantidade de dias
                int dia = (int)Convert.ToDateTime(dataInicial).Subtract(Convert.ToDateTime(dataFinal)).TotalDays;

                //Verifica os dias
                for (int i = 0; dia >= i; i++)
                {
                    DateTime numeroDia = Convert.ToDateTime(dataFinal).AddDays(i);

                    if (Convert.ToInt32(numeroDia.DayOfWeek) == 0 || Convert.ToInt32(numeroDia.DayOfWeek) == 6)
                    {
                        contDia++;
                    }
                }
                
                //Passa os dados da pesquisa
                curvaCollection = curvaNegocios.PesqCaixaCurvaABC(regiao, rua, dataInicial, dataFinal, (dia - contDia));

                if (curvaCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    relCurvaAbc.Database.Tables["CurvaAbc"].SetDataSource(curvaCollection.OrderBy(n => n.curva).ThenBy(n => n.endereco));

                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = relCurvaAbc;
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
                MessageBox.Show("Ocorreu um erro ao gerar o relatório da curva abc! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GerarRelatorioCurvaAbcFlowRack(int codEstacao, string dataInicial, string dataFinal)
        {
            try
            {
                //Instância o relatório
                RelCurvaABC relCurvaAbc = new RelCurvaABC();

                //Instância a camada de Negocios
                ImpressaoCurvaABCNegocios curvaNegocios = new ImpressaoCurvaABCNegocios();

                //Instância a camada de objêto
                ImpressaoCurvaAbcCollection curvaCollection = new ImpressaoCurvaAbcCollection();

                //Exclui a quantidade de dias do final de semana para se ter uma média
                int contDia = 0;

                //Calcula a quantidade de dias
                int dia = (int)Convert.ToDateTime(dataInicial).Subtract(Convert.ToDateTime(dataFinal)).TotalDays;

                //Verifica os dias
                for (int i = 0; dia >= i; i++)
                {
                    DateTime numeroDia = Convert.ToDateTime(dataFinal).AddDays(i);

                    if (Convert.ToInt32(numeroDia.DayOfWeek) == 0 || Convert.ToInt32(numeroDia.DayOfWeek) == 6)
                    {
                        contDia++;
                    }
                }

                //Passa os dados da pesquisa
                curvaCollection = curvaNegocios.PesqFlowCurvaABC(codEstacao, dataInicial, dataFinal, (dia - contDia));

                if (curvaCollection.Count > 0)
                {
                    //Passa os dados para o dataset do relatório
                    relCurvaAbc.Database.Tables["CurvaAbc"].SetDataSource(curvaCollection.OrderBy(n => n.curva).ThenBy(n => n.endereco));

                    crystalReportViewer1.ReportSource = null;
                    crystalReportViewer1.ReportSource = relCurvaAbc;
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
                MessageBox.Show("Ocorreu um erro ao gerar o relatório da curva abc! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
