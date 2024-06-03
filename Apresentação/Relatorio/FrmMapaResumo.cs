using CrystalDecisions.CrystalReports.Engine;
using Negocios;
using Negocios.Relatorio;
using ObjetoTransferencia.Relatorio;
using System;

using System.Windows.Forms;

namespace Wms.Relatorio
{    
    public partial class FrmMapaResumo : Form
    {
        public int codManifesto;
        public int codUsuario;

        public FrmMapaResumo()
        {
            InitializeComponent();
        }

        private void FrmMapaResumo_Load(object sender, EventArgs e)
        {
           // GerarRelatorio(257521);
        }

       

        public int GerarRelatorio(int codManifesto, bool naoConferido, bool naoImpresso, string tipo)
        {
            try
            {
                //Instância o relatório
                ResumoMapaSeparacao pedido = new ResumoMapaSeparacao();
                ReportDocument itemPedido;
                //ReportDocument grandezaPedido;

                //Instância a camada de Negocios
                ManifestoNegocios manifestoNegocios = new ManifestoNegocios();
                //Instância a camada de objêto
                MapaSeparacaoCollection manifestoCollection = new MapaSeparacaoCollection();

                if (tipo.Equals("ENTREGA"))
                {
                    //Passa os dados da pesquisa
                    manifestoCollection = manifestoNegocios.PesqResumoManifesto(codManifesto, naoConferido, naoImpresso);
                    //Passa os dados para o dataset do relatório
                    pedido.Database.Tables["Mapa"].SetDataSource(manifestoCollection);


                    //Instância a camada de objêto
                    ItemMapaSeparacaoCollection itemPedidoCollection = new ItemMapaSeparacaoCollection();
                    //Passa os dados da pesquisa
                    itemPedidoCollection = manifestoNegocios.PesqItemResumoManifesto(codManifesto, naoConferido, naoImpresso);

                    if (itemPedidoCollection.Count > 0)
                    {
                        //Instância o relatório
                        itemPedido = pedido.OpenSubreport("RelItemMapaSeparacao.rpt");
                        //Passa os dados para o dataset do relatório
                        itemPedido.Database.Tables["Item"].SetDataSource(itemPedidoCollection);

                    }
                }

                if (tipo.Equals("REENTREGA"))
                {
                    //Passa os dados da pesquisa
                    manifestoCollection = manifestoNegocios.PesqReentregaResumoManifesto(codManifesto);
                    //Passa os dados para o dataset do relatório
                    pedido.Database.Tables["Mapa"].SetDataSource(manifestoCollection);


                    //Instância a camada de objêto
                    ItemMapaSeparacaoCollection itemPedidoCollection = new ItemMapaSeparacaoCollection();
                    //Passa os dados da pesquisa
                    itemPedidoCollection = manifestoNegocios.PesqReentregaItemResumoManifesto(codManifesto);

                    if (itemPedidoCollection.Count > 0)
                    {
                        //Instância o relatório
                        itemPedido = pedido.OpenSubreport("RelItemMapaSeparacao.rpt");
                        //Passa os dados para o dataset do relatório
                        itemPedido.Database.Tables["Item"].SetDataSource(itemPedidoCollection);

                    }
                }



                crvMapaResumo.ReportSource = null;
                crvMapaResumo.ReportSource = pedido;
                crvMapaResumo.RefreshReport();
                Show();

                return 1;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao gerar o resumo de carga! \nDetalhes:" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return 0;
            }


        }
    }
}
