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

namespace Wms
{
    public partial class FrmConsultaPedido : Form
    {
        public int codPedido;
        public string empresa;

        public FrmConsultaPedido()
        {
            InitializeComponent();


        }

        private void FrmConsultaPedido_Load(object sender, EventArgs e)
        {
            //Pesquisa o pedido
            PesqPedido();
        }

        //Pesquisa o pedido
        private void PesqPedido()
        {

            try
            {
                //Instância a camada de negocios
                ConsultaPedidoNegocios pedidoNegocios = new ConsultaPedidoNegocios();
                //Instância a camada de objêto
                Pedido pedido = new Pedido();
                //O objêto recebe o resultado da consulta
                pedido = pedidoNegocios.PesqPedido(codPedido, empresa);

                lblDataPedido.Text = Convert.ToString(pedido.dataPedido.Date);
                lblCodPedido.Text = Convert.ToString(codPedido);
                lblCFOP.Text = Convert.ToString(pedido.cfop);
                lblTipoPedido.Text = Convert.ToString(pedido.tipoPedido);
                lblFormaPagameno.Text = Convert.ToString(pedido.formaPagamento);
                lblPrazo.Text = Convert.ToString(pedido.prazo);
                lblTotal.Text = string.Format(@"{0:C}", pedido.totalPedido);
                lblPeso.Text = string.Format(@"{0:N}", pedido.pesoPedido);
                txtObservacao.Text = Convert.ToString(pedido.observacaoPedido);
                lblCliente.Text = Convert.ToString(pedido.nomeCliente);
                lblFantasia.Text = Convert.ToString(pedido.fantasiaCliente);
                lblEndereco.Text = Convert.ToString(pedido.enderecoCliente);
                lblBairro.Text = Convert.ToString(pedido.bairroCliente);
                lblCidade.Text = Convert.ToString(pedido.cidadeCliente);
                lblUF.Text = Convert.ToString(pedido.ufCliente);
                lblRota.Text = Convert.ToString(pedido.rotaCliente);
                lblRepresentante.Text = Convert.ToString(pedido.representante);


                if (codPedido > 0)
                {
                    //Pesquisa os itens do pedido
                    PesqItens();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Pesquisa os itens do pedido
        private void PesqItens()
        {
            try
            {
                //Instância a camada de negocios
                ConsultaPedidoNegocios pedidoNegocios = new ConsultaPedidoNegocios();
                //Instância a camada de objêto
                ItensPedidoCollection itemPedidoCollection = new ItensPedidoCollection();
                //O objêto recebe o resultado da consulta
                itemPedidoCollection = pedidoNegocios.PesqItem(codPedido, empresa);
                //Limpa o grid
                gridItens.Rows.Clear();

                if (itemPedidoCollection.Count > 0)
                {
                    //Grid Recebe o resultado da coleção
                    itemPedidoCollection.ForEach(n => gridItens.Rows.Add(gridItens.Rows.Count + 1, n.enderecoProduto, n.codProduto +" - "+ n.descProduto, n.qtdProduto +" "+ n.uniUnidade, string.Format(@"{0:C}", n.valorUnitario), n.qtdCaixaProduto + " "+ n.uniCaixa, n.qtdUnidadeProduto + " "+n.uniUnidade,  string.Format(@"{0:C}", n.valorTotal, string.Format(@"{0:N}", n.pesoTotal))));

                    
               
                    //Qtd de categoria encontrada
                    //lblQtd.Text = gridItens.RowCount.ToString();

                    //Seleciona a primeira linha do grid
                    gridItens.CurrentCell = gridItens.Rows[0].Cells[1];
                    //Foca no grid
                    gridItens.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum item encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
