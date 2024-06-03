using Negocios;
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
    public partial class FrmContagem : Form
    {
        public FrmContagem()
        {
            InitializeComponent();
        }

        private void FrmContagem_Load(object sender, EventArgs e)
        {

        }


        public void GerarRelatorio(int codInventario, int regiao, int rua, int bloco, string tipo, string tipoPicking, int estacao, string descEstacao, string lado, string contagem)
        {
            try
            {
                //Instância o relatório
                RelContagem endereco = new RelContagem();

                //Instância a camada de Negocios
                impressaoContagensNegocios inventariooNegocios = new impressaoContagensNegocios();
                //Instância a camada de objêto
                RelItemInventarioCollection itemCollection = new RelItemInventarioCollection();

                if(lado.Equals("PAR"))
                {
                    lado = "Par";
                }

                if (lado.Equals("IMPAR"))
                {
                    lado = "Impar";
                }

                if (contagem.Equals("CONTAGEM"))
                {
                    if (tipo.Equals("PICKING"))
                    {
                        //Passa os dados da pesquisa
                        itemCollection = inventariooNegocios.PesqContagemPicking(codInventario, regiao, rua, bloco, tipo, tipoPicking, estacao, descEstacao, lado);
                    }
                    else if (tipo.Equals("PULMAO"))
                    {
                        //Passa os dados da pesquisa
                        itemCollection = inventariooNegocios.PesqContagemPulmao(codInventario, regiao, rua, bloco, tipo, tipoPicking, estacao, descEstacao, lado);
                    }
                }

                if (contagem.Equals("CONTAGEM 3"))
                {
                    if (tipo.Equals("PICKING"))
                    {
                        //Passa os dados da pesquisa
                        itemCollection = inventariooNegocios.PesqContagemPicking3(codInventario, regiao, rua, bloco, tipo, tipoPicking, estacao, descEstacao, lado, contagem);
                    }
                    else if (tipo.Equals("PULMAO"))
                    {
                        //Passa os dados da pesquisa
                        itemCollection = inventariooNegocios.PesqContagemPulmao3(codInventario, regiao, rua, tipo, tipoPicking, lado, contagem);
                    }
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
                MessageBox.Show("Ocorreu um erro ao gerar o relatório de endereços! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
