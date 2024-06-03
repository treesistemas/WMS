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
    public partial class FrmPesoVariavel : Form
    {
        public int idProduto;
        public string produto;
        public string tipo;
        public int manifesto;
        public double quantidade;
        public int volume;
        public double corteConferido;
        public double pesoConferido;

        public FrmPesoVariavel()
        {
            InitializeComponent();
        }

        private void FrmPesoVariavel_Load(object sender, EventArgs e)
        {
            //Pesquisa o pedido
            PesqPedido();
        }

        private void gridItens_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Calculo
            CalcularDiferenca();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            //Trasnfere os dados
            TransferirDados();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Fechar Frame
            Close();
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
                pedidoCollection = manifestoNegocios.PesqPedidoPesoVariavel(manifesto, idProduto);

                if (pedidoCollection.Count > 0)
                {
                    ///Grid Recebe o resultado da coleção
                    pedidoCollection.ForEach(n => gridItens.Rows.Add(gridItens.Rows.Count + 1, n.codPedido, n.idProduto, string.Format("{0:N3}", n.qtdProduto), tipo,
                      n.volume, string.Format("{0:N3}", n.qtdConferida), string.Format("{0:N3}", n.qtdCorte)));

                    //Exibe a descrição do produto
                    lblProduto.Text = produto;
                    //Quantidade de pedido
                    lblPedido.Text = Convert.ToString(gridItens.Rows.Count);
                    //Quantidade de item
                    lblQuantidade.Text = Convert.ToString(quantidade);
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

        private void CalcularDiferenca()
        {
            try
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridItens.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                double quantidade = Convert.ToDouble(gridItens.Rows[indice].Cells[3].Value);
                int volume = Convert.ToInt32(gridItens.Rows[indice].Cells[5].Value);
                double quantidadeConferida = Convert.ToDouble(gridItens.Rows[indice].Cells[6].Value);
                double corte = Convert.ToDouble(gridItens.Rows[indice].Cells[7].Value);

                if (quantidadeConferida > 0)
                {
                    if (quantidadeConferida > (quantidade + corte))
                    {
                        //Zera a quantidade
                        gridItens.Rows[indice].Cells[6].Value = "0,00";
                        MessageBox.Show("Quantidade conferida ultrapassa a quantidade do pedido", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        gridItens.Rows[indice].Cells[6].Value = string.Format("{0:N3}", quantidadeConferida);
                        gridItens.Rows[indice].Cells[7].Value = string.Format("{0:N3}", quantidade - quantidadeConferida);
                    }
                }

                //Zera a quantidade conferida
                quantidadeConferida = 0;
                corte = 0;
                volume = 0;
                

                //Calcula o peso conferido e a diferença
                for (int i = 0; gridItens.Rows.Count > i; i++)
                {
                    quantidadeConferida += Convert.ToDouble(gridItens.Rows[i].Cells[6].Value);
                    corte += Convert.ToDouble(gridItens.Rows[i].Cells[7].Value);
                    volume += Convert.ToInt32(gridItens.Rows[i].Cells[5].Value);
                }

                //Seta o valor
                lblVolume.Text = Convert.ToString(volume);
                lblConferido.Text = string.Format("{0:N3}", quantidadeConferida);
                lblDiferenca.Text = string.Format("{0:N3}", corte);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao efetuar o calculo \n" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Trânsfere os dados
        private void TransferirDados()
        {
            //Seta os valores
            volume = Convert.ToInt32(lblVolume.Text);
            pesoConferido = Convert.ToDouble(lblConferido.Text);
            corteConferido = Convert.ToDouble(lblDiferenca.Text);
            //controla  a ação do frame
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            //Fecha a tela
            Close();

        }

        
    }
}
