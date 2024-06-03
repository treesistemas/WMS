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

namespace Wms.Movimentacao
{
    public partial class FrmCorteManifesto : Form
    {
        public int idProduto;
        public string produto;
        public int manifesto;
        public int quantidade;
        public int qtdConferida;
        public int corteConferido;
        public int codUsuario;

        public FrmCorteManifesto()
        {
            InitializeComponent();
        }

        private void FrmCorteManifesto_Load(object sender, EventArgs e)
        {
            //Pesquisa os itens do pedido
            PesqPedido();
        }

        private void gridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalcularCorte();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            //Confirma o corte no banco
            ConfirmarCorte();
        }

        //Pesquisa os itens do pedido
        private void PesqPedido()
        {
            try
            {
                //Instância a camada de negocios
                ConferenciaManifestoNegocios manifestoNegocios = new ConferenciaManifestoNegocios();
                //Instância a camada de objêto
                ItensPedidoCollection pedidoCollection = new ItensPedidoCollection();
                //pesquisa informações sobre o produto no picking
                pedidoCollection = manifestoNegocios.PesqPedidoItemCorte(manifesto, idProduto);

                if (pedidoCollection.Count > 0)
                {
                    ///Grid Recebe o resultado da coleção
                    pedidoCollection.ForEach(n => gridItens.Rows.Add(gridItens.Rows.Count + 1, n.codPedido, n.idProduto, string.Format("{0:N3}", n.qtdProduto),
                      n.volume, string.Format("{0:N3}", n.qtdConferida), string.Format("{0:N3}", n.qtdCorte)));

                    //Exibe a descrição do produto
                    lblProduto.Text = produto;
                    //Quantidade de pedido
                    lblPedido.Text = Convert.ToString(gridItens.Rows.Count);
                    //Quantidade de item
                    lblQuantidade.Text = Convert.ToString(quantidade);
                    //Quantidade de item
                    lblqtdConferida.Text = Convert.ToString(qtdConferida);
                }
                else
                {
                    MessageBox.Show("Produto não endereçado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularCorte()
        {
            try
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridItens.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                double quantidade = Convert.ToDouble(gridItens.Rows[indice].Cells[3].Value);
                double corte = Convert.ToDouble(gridItens.Rows[indice].Cells[4].Value);
                double saldo = Convert.ToDouble(gridItens.Rows[indice].Cells[5].Value);
                

                
                    if (corte > quantidade)
                    {
                        //Zera a quantidade
                        gridItens.Rows[indice].Cells[4].Value = "0";
                        MessageBox.Show("Corte ultrapassa a quantidade do pedido", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        gridItens.Rows[indice].Cells[5].Value = Convert.ToInt32(quantidade - corte);
                    }

                    //Zera os valores
                corte = 0;

                //Calcula os valores
                for (int i = 0; gridItens.Rows.Count > i; i++)
                {
                    corte += Convert.ToDouble(gridItens.Rows[i].Cells[4].Value);                    
                }

                //Seta o valor
                lblCorte.Text = Convert.ToString(corte);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao efetuar o calculo \n" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        //Trânsfere os dados
        private void ConfirmarCorte()
        {
            //Instância a camada de negocios
            ConferenciaManifestoNegocios manifestoNegocios = new ConferenciaManifestoNegocios();

            for (int i = 0; gridItens.Rows.Count > i; i++)
            {
                if (Convert.ToInt32(gridItens.Rows[i].Cells[4].Value) > 0)
                {
                    /*manifestoNegocios.RegistrarCorteItem(
                        Convert.ToInt32(gridItens.Rows[i].Cells[1].Value), //Código do pedido
                        Convert.ToInt32(gridItens.Rows[i].Cells[2].Value), //Id do produto
                        Convert.ToInt32(gridItens.Rows[i].Cells[5].Value), //Corte
                        codUsuario); //Conferente*/
                }
            }

            //Seta os valores
            corteConferido = Convert.ToInt32(lblQuantidade.Text);
            //controla  a ação do frame
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            //Fecha a tela
            Close();

        }

       
    }
}
