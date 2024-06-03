using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmAcompanhamentoConfFlow : Form
    {
        public string dataProcessamento;
        public string usuario;
        public int codPedido;

        //Recebe a seleção da linha no grid   
        int indice = 0;

        public FrmAcompanhamentoConfFlow()
        {
            InitializeComponent();
        }

        private void FrmAcompanhamentoConfFlow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Encerra aplicação                    
            timer.Stop();
        }

        //Load
        private void FrmAcompanhamentoConfFlow_Load(object sender, EventArgs e)
        {
            lblDataProcessamento.Text = Convert.ToString(dataProcessamento);
            lblUsuário.Text = Convert.ToString(usuario);
            lblPedido.Text = Convert.ToString(codPedido);

            //Pesquisa assim que abrir o frame
            EventHandler handler = PesqPedido;
            handler?.Invoke(this, e);
            //Depois de aberto executa a pesquisa a cada 20 segundos
            timer.Tick += new System.EventHandler(PesqPedido);
            timer.Interval = 20000; //20 segundos
            timer.Start();

        }

        private void gridItens_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Instância as linha da tabela
            DataGridViewRow linha = gridItens.CurrentRow;
            //Recebe a seleção da linha no grid   
            indice = linha.Index;
        }

        private void gridItens_KeyUp(object sender, KeyEventArgs e)
        { //Instância as linha da tabela
            DataGridViewRow linha = gridItens.CurrentRow;
            //Recebe a seleção da linha no grid   
            indice = linha.Index;
        }

        //Pesquisa os itens do pedido
        private void PesqPedido(object sender, System.EventArgs e)
        {
            try
            {
                //Instância a camada de negocios
                AcompanhamentoConfFlowNegocios consultarFlowRackNegocios = new AcompanhamentoConfFlowNegocios();
                //Instância a camada de objêto
                ItensFlowRackCollection itensRatreamentoCollection = new ItensFlowRackCollection();
                ItensFlowRackCollection itensPedidoCollection = new ItensFlowRackCollection();

                //O objêto recebe o resultado da consulta
                itensRatreamentoCollection = consultarFlowRackNegocios.PesqItensRastreamento(codPedido);
                itensPedidoCollection = consultarFlowRackNegocios.PesqItensPedido(codPedido);

                //Adiciona a lista do flowrack o item que não foi conferido
                foreach (ItensFlowRack n in itensPedidoCollection)
                {
                    ItensFlowRack item = new ItensFlowRack();

                    //Pesquisa o objêto
                    item = itensRatreamentoCollection.Find(x => x.codProduto.Equals(n.codProduto));                    

                    if (item == null)
                    {
                        itensRatreamentoCollection.Add(n);
                    }
                }

                //Limpa o grid
                gridItens.Rows.Clear();
                //Grid Recebe o resultado da coleção
                itensRatreamentoCollection.ForEach(n => gridItens.Rows.Add(gridItens.Rows.Count + 1, n.dataProcessamento, n.descEstacao, n.numeroVolume, n.endereco, n.codProduto + " - " + n.descProduto, n.qtdProduto + " " + n.uniProduto, n.qtdConferidaProduto + " " + n.uniProduto, n.qtdCorteProduto + " " + n.uniProduto, n.dataConferencia, n.nomeUsuario));

                lblConferidos.Text = "0"; //Zera a contagem dos conferidos
                lblCorte.Text = "0"; //Zera a contagem dos corte

                if (itensRatreamentoCollection.Count > 0)
                {
                    for (int i = 0; itensRatreamentoCollection.Count > i; i++)
                    {
                        if (Convert.ToInt32(itensRatreamentoCollection[i].qtdProduto) + Convert.ToInt32(itensRatreamentoCollection[i].qtdCorteProduto) == (Convert.ToInt32(itensRatreamentoCollection[i].qtdConferidaProduto) + Convert.ToInt32(itensRatreamentoCollection[i].qtdCorteProduto)))
                        {
                            gridItens.Rows[i].Cells[0].Style.BackColor = Color.SteelBlue;
                            gridItens.Rows[i].Cells[0].Style.ForeColor = Color.White;

                            lblConferidos.Text = Convert.ToString(Convert.ToInt32(lblConferidos.Text) + 1);
                        }
                        else 
                        {
                            gridItens.Rows[i].Cells[0].Style.BackColor = Color.Red;
                            gridItens.Rows[i].Cells[0].Style.ForeColor = Color.White;                            
                        }

                        //Verifica se houve corte
                        if (Convert.ToInt32(itensRatreamentoCollection[i].qtdCorteProduto) > 0)
                        {
                            gridItens.Rows[i].Cells[8].Style.BackColor = Color.OrangeRed;
                            gridItens.Rows[i].Cells[8].Style.ForeColor = Color.White;

                            //Exibe a quantidade de itens que foram cortados
                            lblCorte.Text = Convert.ToString(Convert.ToInt32(lblCorte.Text) + 1);
                        }

                        //Verifica se houve corte
                        /*if (!Convert.ToString(itensRatreamentoCollection[i].tipoEndereco).Equals("FLOWRACK"))
                        {
                            gridItens.Rows[i].Cells[4].Style.BackColor = Color.Gray;
                            gridItens.Rows[i].Cells[4].Style.ForeColor = Color.Black;                            
                        }*/

                    }

                    //Exibe a quantidade de itens
                    lblItens.Text = Convert.ToString(gridItens.Rows.Count);
                    //Seleciona a linha do grid
                    gridItens.CurrentCell = gridItens.Rows[indice].Cells[0];
                    //Foca no grid
                    gridItens.Focus();
                }
                else
                {
                    MessageBox.Show("Não existe itens de flow rack para esse pedido!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
