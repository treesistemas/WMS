using Negocios;
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
    public partial class FrmEndereco : Form
    {
        public FrmEndereco()
        {
            InitializeComponent();
        }

        private void FrmEndereco_Load(object sender, EventArgs e)
        {
            //GerarRelatorio("TODOS", "4", "2", "","","","","Todos");
        }

        public void GerarRelatorio(string empresa,string tipoEndereco, string numeroRegiao, string numeroRua, string numeroBloco, string numeroNivel, string status, string disponibilidade, string lado)
        {
            try
            {
                //Instância o relatório
                RelEndereco endereco = new RelEndereco();

                //Instância a camada de Negocios
                EnderecamentoNegocios enderecoNegocios = new EnderecamentoNegocios();

                if(numeroBloco == "Selecione")
                    numeroBloco = "";
                if (numeroNivel == "Selecione")
                    numeroNivel = "";
                if (status == "Selecione")
                    status = "";
                if (disponibilidade == "Selecione")
                    disponibilidade = "";
               // MessageBox.Show("Tipo" + tipoEndereco + "  Regiao " + numeroRegiao + "  Rua " + numeroRua +
                //   " bloco  " + numeroBloco + " Nivel  " + numeroNivel + "  Status " + status + "  Disponibilidade " + disponibilidade + "   " + lado);
                
                //Instância a camada de objêto
                EnderecoCollection enerecoCollectionPi = new EnderecoCollection();
                EnderecoCollection enerecoCollectionPU = new EnderecoCollection();
                EnderecoCollection enerecoCollection = new EnderecoCollection();
                //Passa os dados da pesquisa
                if(tipoEndereco.Equals("PICKING"))
                    enerecoCollectionPi = enderecoNegocios.PesqRelatorioEnderecoPicking(empresa, tipoEndereco, Convert.ToInt32(numeroRegiao), Convert.ToInt32(numeroRua), numeroBloco, numeroNivel, status, disponibilidade, lado);
                else if (tipoEndereco.Equals("PULMAO"))
                    enerecoCollectionPU = enderecoNegocios.PesqRelatorioEnderecoPulmao(empresa, tipoEndereco, Convert.ToInt32(numeroRegiao), Convert.ToInt32(numeroRua), numeroBloco, numeroNivel, status, disponibilidade, lado);
                else
                {
                    enerecoCollectionPi = enderecoNegocios.PesqRelatorioEnderecoPicking(empresa, tipoEndereco, Convert.ToInt32(numeroRegiao), Convert.ToInt32(numeroRua), numeroBloco, numeroNivel, status, disponibilidade, lado);
                    enerecoCollectionPU = enderecoNegocios.PesqRelatorioEnderecoPulmao(empresa, tipoEndereco, Convert.ToInt32(numeroRegiao), Convert.ToInt32(numeroRua), numeroBloco, numeroNivel, status, disponibilidade, lado);
                }

                ///enerecoCollection = enderecoNegocios.PesqRelatorioEndereco("PULMAO", 1, 5, "", "", "", "", "TODOS");
                if(enerecoCollectionPi.Any())
                   enerecoCollection.AddRange(enerecoCollectionPi);
                if(enerecoCollectionPU.Any())
                enerecoCollection.AddRange(enerecoCollectionPU);
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
                MessageBox.Show("Ocorreu um erro ao gerar o relatório de endereços! \nDetalhes: " + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
