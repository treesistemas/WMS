using CrystalDecisions.CrystalReports.Engine;
using Negocios;
using ObjetoTransferencia.Relatorio;
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
    public partial class FrmRendimentoExpedicao : Form
    {
        public FrmRendimentoExpedicao()
        {
            InitializeComponent();
        }

        private void FrmRendimentoExpedicao_Load(object sender, EventArgs e)
        {

        }


        public void GerarRelatorio(string dataInicial, string dataFinal)
        {
            try
            {
                //Instância o relatório
                RelRendimentoExpedicao expedicao = new RelRendimentoExpedicao();
                ReportDocument conferente;
                ReportDocument separacao;

                //Instância a camada de Negocios
                ImpressaoRendimentoExpedicaoNegocios rendimentoNegocios = new ImpressaoRendimentoExpedicaoNegocios();

                //Instância a camada de objêto
                RendimentoExpedicaoCollection conferenteCollection = new RendimentoExpedicaoCollection();
                //Passa os dados da pesquisa
                conferenteCollection = rendimentoNegocios.PesqRendimentoConferente(dataInicial, dataFinal);

                //Instância a camada de objêto
                RendimentoExpedicaoCollection separacaoCollection = new RendimentoExpedicaoCollection();
                //Passa os dados da pesquisa
                separacaoCollection = rendimentoNegocios.PesqRendimentoSeparacao(dataInicial, dataFinal);

                if (conferenteCollection.Count > 0 || separacaoCollection.Count > 0)
                {

                    //Controla o cabeçalho
                    if (conferenteCollection.Count > 0)
                    {
                        //Nome da Empresa + datas
                        expedicao.Database.Tables["rendimento"].SetDataSource(conferenteCollection);
                    }
                    else
                    {
                        //Nome da Empresa + datas
                        expedicao.Database.Tables["rendimento"].SetDataSource(separacaoCollection);

                    }


                    if (conferenteCollection.Count > 0)
                    {

                        //Instância o relatórioDataSet não dá suporte a System.Nullable<>
                        conferente = expedicao.OpenSubreport("RelRendimentoConferente.rpt");
                        //Passa os dados para o dataset do relatório
                        conferente.Database.Tables["rendimento"].SetDataSource(conferenteCollection.OrderByDescending(x => x.qtdTotal).ToList());
                    }

                    if (separacaoCollection.Count > 0)
                    {

                        //Instância o relatórioDataSet não dá suporte a System.Nullable<>
                        separacao = expedicao.OpenSubreport("RelRendimentoSeparador.rpt");
                        //Passa os dados para o dataset de forma ordenada
                        separacao.Database.Tables["rendimento"].SetDataSource(separacaoCollection.OrderByDescending(x => x.qtdTotal).ToList());
                    }

                    crystalReportViewer.ReportSource = null;
                    crystalReportViewer.ReportSource = expedicao;
                    crystalReportViewer.RefreshReport();
                    Show();
                }
                else
                {
                    MessageBox.Show("Não existe informação para o período informado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o relatório de expedição! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
