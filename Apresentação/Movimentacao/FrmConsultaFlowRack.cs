using DocumentFormat.OpenXml.Drawing.Charts;
using Negocios;
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
    public partial class FrmConsultaFlowRack : Form
    {
        public string codPedido;
        public FrmConsultaFlowRack()
        {
            InitializeComponent();
        }

        private void FrmConsultaFlowRack_Load(object sender, EventArgs e)
        {
            //Seta o texto
            txtCodPedido.Text = codPedido;
            //Pesquisa os itens do pedido
            PesqItens(codPedido);
        }
        private void mniRelatorioFlowRack_Click(object sender, EventArgs e)
        {
            RelatorioSeparacaoFlowRack(Convert.ToInt32(txtCodPedido.Text));
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Pesquisa os itens do pedido
        private void PesqItens(string codPedido)
        {
            try
            {
                //Instância a camada de negocios
                ConsultarFlowRackNegocios consultarFlowRackNegocios = new ConsultarFlowRackNegocios();
                //Instância a camada de objêto
                ItensFlowRackCollection itensFlowRackCollection = new ItensFlowRackCollection();
                //O objêto recebe o resultado da consulta
                itensFlowRackCollection = consultarFlowRackNegocios.PesqItens(codPedido);
                //Limpa o grid
                gridItens.Rows.Clear();
                //Grid Recebe o resultado da coleção
                itensFlowRackCollection.ForEach(n => gridItens.Rows.Add(gridItens.Rows.Count + 1, n.numeroVolume, n.descEstacao, n.endereco, n.codProduto + " - " + n.descProduto, n.qtdConferidaProduto + " " + n.uniProduto, n.dataConferencia, n.auditaProduto));


                if (itensFlowRackCollection.Count > 0)
                {
                    //Data de processamento
                    if (!itensFlowRackCollection[0].dataProcessamento.Equals(null))
                    {
                        lblDataProcessamento.Text = Convert.ToString(itensFlowRackCollection[0].dataProcessamento.Value);
                    }
                    else
                    {
                        lblDataProcessamento.Text = "-";
                    }

                    //Quantidade de volumes
                    lblVolume.Text = Convert.ToString(itensFlowRackCollection[itensFlowRackCollection.Count-1].numeroVolume);
                    //Qtd de itens encontrado
                    lblItens.Text = gridItens.RowCount.ToString();
                    //Zera a quantidade
                    lblQuantidade.Text = "0";
                    //soma a quantidade de unidade
                    for (int i = 0; itensFlowRackCollection.Count > i; i++)
                    {
                        lblQuantidade.Text = Convert.ToString(Convert.ToInt32(lblQuantidade.Text) + itensFlowRackCollection[i].qtdConferidaProduto);
                    }                    

                    //Seleciona a primeira linha do grid
                    gridItens.CurrentCell = gridItens.Rows[0].Cells[1];
                    //Foca no grid
                    gridItens.Focus();
                }
                else
                {
                    Close();
                    MessageBox.Show("Não existe volumes de flow rack para esse pedido!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Gera o relatório de separação no flow rack
        public void RelatorioSeparacaoFlowRack(int pedido)
        {
            try
            {
                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {                   
                    //Instância o relatório
                    FrmSeparacaoFlowRack frame = new FrmSeparacaoFlowRack();
                    frame.GerarRelatorio(pedido);
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


    }
}
