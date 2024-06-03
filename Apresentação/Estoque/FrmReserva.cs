using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wms
{
    public partial class FrmReserva : Form
    {
        public int codUsuario;
        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        //Array com id
        private int[] codEstacao;

        public FrmReserva()
        {
            InitializeComponent();
        }

        private void FrmReseva_Load(object sender, EventArgs e)
        {
            if (empresaCollection != null)
            {
                //Preenche o combobox região
                empresaCollection.ForEach(n => cmbEmpresa.Items.Add(n.siglaEmpresa));
                //Seleciona a primeira empresa
                cmbEmpresa.SelectedIndex = 0;

                //Verifica se existe mais de uma empresa
                if (empresaCollection[0].multiEmpresa == false)
                {
                    cmbEmpresa.Enabled = false;

                }
                else
                {
                    cmbEmpresa.Enabled = true;
                }
            }
        }

        //KeyPress
        private void txtOrdemAbastecimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtCodigoAbastecimento.Text.Equals(""))
                {
                    cmbTipoReserva.Focus(); //Foca no campo tipo de reserva
                }
                else
                {
                    //Pesquisa as reservas
                    PesqItens();
                }
            }
        }

        private void cmbTipoReserva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmInicial.Focus(); //Foca no data inicial
            }
        }

        private void dtmInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmFinal.Focus(); //Foca no data final
            }
        }

        private void dtmFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisar.Focus(); //Foca no botão pesquisar
            }
        }

        //Changed
        private void txtCodigoAbastecimento_TextChanged(object sender, EventArgs e)
        {
            if (!txtCodigoAbastecimento.Text.Equals(""))
            {
                cmbEstacao.Enabled = true;
            }
            else
            {
                cmbEstacao.Items.Clear();
                cmbEstacao.Text = "SELECIONE";
                cmbEstacao.Enabled = false;
            }
        }
        
        private void cmbTipoReserva_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoReserva.Text.Equals("ABASTECIMENTO"))
            {
                colOA.Visible = true; //Exibe a coluna do código abastecimento               
                colPedido.Visible = false; //Esconde a coluna do pedido

                cmbStatus.Enabled = true; //Habilita o campo status

                cmbEstacao.Enabled = true;

                btnLiberar.Enabled = false; //Desabilita o botão de liberar reserva
                btnTransferir.Enabled = true; //Habilita o botão de transferir abastecimento
                btnCancelar.Enabled = true; //Habilita o botão de cancelar abastecimento

            }

            else if (cmbTipoReserva.Text.Equals("RESERVADOS"))
            {
                colOA.Visible = false; //Esconde a coluna do código abastecimento
                colPedido.Visible = true; //Exibe a coluna do pedido

                cmbStatus.Enabled = false; //Desabilita o campo status

                cmbEstacao.Items.Clear();
                cmbEstacao.Text = "SELECIONE";
                cmbEstacao.Enabled = false;

                btnLiberar.Enabled = true; //Habilita o botão de liberar reserva
                btnTransferir.Enabled = false; //Desabilita o botão de transferir abastecimento
                btnCancelar.Enabled = false; //Desabilita o botão de cancelar abastecimento
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa
            PesqItens();
        }

        //Click
        private void cmbEstacao_Click(object sender, EventArgs e)
        {
            if (cmbEstacao.Items.Count == 0)
            {
                PesqEstacao();
            }
        }

        private void btnLiberar_Click(object sender, EventArgs e)
        {
            //Cancela os itens do abastecimento
            Thread thread = new Thread(LiberarReservas);
            thread.Start();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Cancela os itens do abastecimento
            Thread thread = new Thread(CancelarAbastecimento);
            thread.Start();
        }

        private void btnTransferir_Click(object sender, EventArgs e)
        {
            //Transfere os itens do abastecimento
            Thread thread = new Thread(TransferirAbastecimento);
            thread.Start();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o frame
            Close();
        }

        //Pesquisa as estações
        private void PesqEstacao()
        {
            try
            {
                //Instância a coleção
                EstacaoCollection estacaoCollection = new EstacaoCollection();
                //Instância o negocios
                ReservaNegocios reservaNegocios = new ReservaNegocios();
                //Limpa o combobox regiao
                cmbEstacao.Items.Clear();
                //Preenche a coleção com apesquisa
                estacaoCollection = reservaNegocios.PesqEstacao();
                //Preenche o combobox região
                estacaoCollection.ForEach(n => cmbEstacao.Items.Add(n.descEstacao));
                
                //Define o tamanho do array para o combobox
                codEstacao = new int[estacaoCollection.Count];

                for (int i = 0; i < estacaoCollection.Count; i++)
                {
                    //Preenche o array combobox
                    codEstacao[i] = estacaoCollection[i].codEstacao;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }


        //Pesquisa as Reservas
        private void PesqItens()
        {
            try
            {
                //Instância a categoria negocios
                ReservaNegocios reservaNegocios = new ReservaNegocios();
                //Instância a coleção de categoria
                ReservaCollection reservaCollection = new ReservaCollection();

                if ((!txtCodigoAbastecimento.Text.Equals("")) || cmbTipoReserva.Text.Equals("ABASTECIMENTO"))
                {
                    int estacao = 0;

                    if (!cmbEstacao.Text.Equals("SELECIONE"))
                    {
                        estacao = codEstacao[cmbEstacao.SelectedIndex];
                    }
                    

                    //A coleção recebe o resultado da consulta dos produtos reservados para separação
                    reservaCollection = reservaNegocios.PesqAbastecimento(txtCodigoAbastecimento.Text, cmbStatus.Text, dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), estacao, cmbEmpresa.Text);
                }
                else if (cmbTipoReserva.Text.Equals("RESERVADOS"))
                {
                    //A coleção recebe o resultado da consulta dos produtos reservados para separação
                    reservaCollection = reservaNegocios.PesqReserva(txtPedido.Text, dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), cmbEmpresa.Text);
                }

                //Limpa o grid
                gridItens.Rows.Clear();
                //Grid Recebe o resultado da coleção
                reservaCollection.ForEach(n => gridItens.Rows.Add(gridItens.Rows.Count + 1, n.dataReserva, n.codAbastecimento, n.codPedido, n.status,
                    n.tipoReserva, n.codReserva, n.codPicking, n.enderecoPicking, n.idProduto, n.codProduto + " - " + n.descProduto, n.fatorPulmao,
                    n.codPulmao, n.enderecoPulmao, n.qtdReservada, n.unidadePulmao, string.Format("{0:d}", n.dataVencimento), string.Format(@"{0:N}", n.pesoProduto), n.loteProduto,
                    n.tipoAnalise, n.loginUsuario));

                if (reservaCollection.Count > 0)
                {

                    //Qtd de categoria encontrada
                    lblQtd.Text = gridItens.RowCount.ToString();

                    if ((!txtCodigoAbastecimento.Text.Equals("")) || cmbTipoReserva.Text.Equals("ABASTECIMENTO"))
                    {
                        //Exibe as cores e as do dashboard
                        PreencherInformacao();
                    }

                    //Seleciona a primeira linha do grid
                    gridItens.CurrentCell = gridItens.Rows[0].Cells[1];
                    //Foca no grid
                    gridItens.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhuma produto encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Libera as Reservas
        private void LiberarReservas()
        {
            try
            {
                //Instância a categoria negocios
                ReservaNegocios reservaNegocios = new ReservaNegocios();
                //Passa a quantidade de itens selecionados
                Progressbar(gridItens.SelectedRows.Count);

                //Passa o código da reserva e o código do pulmão
                foreach (DataGridViewRow row in gridItens.SelectedRows)
                {
                    //Exibe o endereços, quantidade e o tipo de estoque 
                    MsgLabel("LIBERANDO: " + gridItens.Rows[row.Index].Cells[13].Value + "  -  " + gridItens.Rows[row.Index].Cells[10].Value);

                    //Garante que seja executado pela thread
                    Invoke((MethodInvoker)delegate ()
                    {
                       
                        if (gridItens.Rows[row.Index].Cells[5].Value.Equals("ABASTECIMENTO"))
                        {
                            MessageBox.Show("Esta operação não é permetida para produtos em abastecimento!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {

                            //Passa o código da reserva e o código do pulmão e o id do produto
                            reservaNegocios.LiberarReserva(Convert.ToInt32(gridItens.Rows[row.Index].Cells[6].Value), //Codigo da reserva 
                                Convert.ToInt32(gridItens.Rows[row.Index].Cells[12].Value), //Código do pulmão 
                                Convert.ToInt32(gridItens.Rows[row.Index].Cells[9].Value), //Id do produto
                                Convert.ToString(gridItens.Rows[row.Index].Cells[10].Value));

                    //Remove a linha do grid
                    gridItens.Rows.RemoveAt(row.Index);                           

                        }
                    });

                    //Incrementa o progressbar
                    IncrementarProgressBar();
                }

                MessageBox.Show("Reserva(s) liberada(s) com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Transferir produto
        private void TransferirAbastecimento()
        {
            try
            {
                //Instância a categoria negocios
                ReservaNegocios reservaNegocios = new ReservaNegocios();
                //Passa a quantidade de itens selecionados
                Progressbar(gridItens.SelectedRows.Count);

                //Passa o código da reserva e o código do pulmão
                foreach (DataGridViewRow row in gridItens.SelectedRows)
                {
                    //Exibe o endereços, quantidade e o tipo de estoque 
                    MsgLabel("TRANSFERINDO: " + gridItens.Rows[row.Index].Cells[12].Value + "    " + gridItens.Rows[row.Index].Cells[10].Value);


                    if (gridItens.Rows[row.Index].Cells[5].Value.Equals("SEPARAÇÃO"))
                    {
                        MessageBox.Show("Esta operação não é permetida para produtos reservados!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (!(gridItens.Rows[row.Index].Cells[4].Value.Equals("CANCELADO") || gridItens.Rows[row.Index].Cells[4].Value.Equals("FINALIZADO")))
                    {
                        string codOrdem = null;

                        //Garante que seja executado pela thread
                        Invoke((MethodInvoker)delegate ()
                        {
                            if (!gridItens.Rows[row.Index].Cells[2].Value.Equals("SISTEMA"))
                            {
                                codOrdem = Convert.ToString(gridItens.Rows[row.Index].Cells[2].Value);
                            }

                            //Passa o código da reserva e o código do pulmão e o id do produto
                            reservaNegocios.TransferirAbastecimento(codOrdem, Convert.ToInt32(gridItens.Rows[row.Index].Cells[6].Value), //Codigo da reserva do abastecimento
                            Convert.ToInt32(gridItens.Rows[row.Index].Cells[7].Value), //Código do picking
                            Convert.ToInt32(gridItens.Rows[row.Index].Cells[12].Value), //Código do pulmão                            
                            Convert.ToInt32(gridItens.Rows[row.Index].Cells[9].Value), ///Id do produto
                            Convert.ToInt32(gridItens.Rows[row.Index].Cells[14].Value) * Convert.ToInt32(gridItens.Rows[row.Index].Cells[11].Value),
                            Convert.ToString(gridItens.Rows[row.Index].Cells[16].Value), //Vencimento do produto
                            Convert.ToDouble(gridItens.Rows[row.Index].Cells[17].Value), //Peso do Produto
                            Convert.ToString(gridItens.Rows[row.Index].Cells[18].Value), //Lote do Produto 
                            Convert.ToString(gridItens.Rows[row.Index].Cells[19].Value), //Tipo de análise
                            Convert.ToString(gridItens.Rows[row.Index].Cells[20].Value)); 

                        //Resgistra a operação no rastreamento
                        reservaNegocios.InserirRastreamento(codUsuario, codOrdem, Convert.ToInt32(gridItens.Rows[row.Index].Cells[9].Value), //ID do produto
                            Convert.ToInt32(gridItens.Rows[row.Index].Cells[12].Value), //Endereço de Origem                           
                            Convert.ToInt32(gridItens.Rows[row.Index].Cells[7].Value), //Endereço de destino                  
                            Convert.ToInt32(gridItens.Rows[row.Index].Cells[14].Value) * Convert.ToInt32(gridItens.Rows[row.Index].Cells[11].Value), //Quantidade de destino
                            Convert.ToString(gridItens.Rows[row.Index].Cells[16].Value), //Vencimento de destino
                            Convert.ToDouble(gridItens.Rows[row.Index].Cells[17].Value), //Peso de destino               
                            Convert.ToString(gridItens.Rows[row.Index].Cells[18].Value),  //Lote de destino
                            Convert.ToString(gridItens.Rows[row.Index].Cells[19].Value));

                        if (!gridItens.Rows[row.Index].Cells[2].Value.Equals("SISTEMA"))
                            {
                                reservaNegocios.AtualizarStatusAbastecimento(codOrdem, codUsuario, cmbEmpresa.Text);
                            }

                            gridItens.Rows[row.Index].Cells[4].Value = "FINALIZADO";
                        });
                    }

                    //Incrementa o progressbar
                    IncrementarProgressBar();
                }

                MessageBox.Show("Abastecimento do(s) produto(s) realizado(s) com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Libera as Reservas
        private void CancelarAbastecimento()
        {
            try
            {
                //Instância a categoria negocios
                ReservaNegocios reservaNegocios = new ReservaNegocios();

                //Passa a quantidade de itens selecionados
                Progressbar(gridItens.SelectedRows.Count);

                //Passa o código da reserva e o código do pulmão
                foreach (DataGridViewRow row in gridItens.SelectedRows)
                {
                    //Exibe o endereços, quantidade e o tipo de estoque 
                    MsgLabel("CANCELANDO: " + gridItens.Rows[row.Index].Cells[12].Value + "    " + gridItens.Rows[row.Index].Cells[10].Value);

                    //Garante que seja executado pela thread
                    Invoke((MethodInvoker)delegate ()
                    {
                        if (gridItens.Rows[row.Index].Cells[5].Value.Equals("SEPARAÇÃO"))
                        {
                            MessageBox.Show("Esta operação não é permetida para produtos reservados!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (!(gridItens.Rows[row.Index].Cells[4].Value.Equals("CANCELADO") || gridItens.Rows[row.Index].Cells[4].Value.Equals("FINALIZADO")))
                        {
                            //Passa o código da reserva e o código do pulmão e o id do produto
                            reservaNegocios.CancelarAbastecimento(Convert.ToInt32(gridItens.Rows[row.Index].Cells[6].Value), //Codigo da reserva do abastecimento
                                Convert.ToInt32(gridItens.Rows[row.Index].Cells[7].Value), //Código do picking
                                Convert.ToInt32(gridItens.Rows[row.Index].Cells[12].Value), //Código do pulmão 
                                Convert.ToInt32(gridItens.Rows[row.Index].Cells[9].Value),
                                cmbEmpresa.Text);

                            //Adiciona a linha no final do grid
                            gridItens.Rows.Add(gridItens.Rows[row.Index].Cells[0].Value, gridItens.Rows[row.Index].Cells[1].Value, gridItens.Rows[row.Index].Cells[2].Value, gridItens.Rows[row.Index].Cells[3].Value, "CANCELADO",
                            gridItens.Rows[row.Index].Cells[5].Value, gridItens.Rows[row.Index].Cells[6].Value, gridItens.Rows[row.Index].Cells[7].Value, gridItens.Rows[row.Index].Cells[8].Value,
                            gridItens.Rows[row.Index].Cells[9].Value, gridItens.Rows[row.Index].Cells[10].Value, gridItens.Rows[row.Index].Cells[11].Value, gridItens.Rows[row.Index].Cells[12].Value,
                            gridItens.Rows[row.Index].Cells[13].Value, gridItens.Rows[row.Index].Cells[14].Value, gridItens.Rows[row.Index].Cells[15].Value, gridItens.Rows[row.Index].Cells[16].Value,
                            gridItens.Rows[row.Index].Cells[17].Value, gridItens.Rows[row.Index].Cells[18].Value, gridItens.Rows[row.Index].Cells[19].Value);

                            //Remove a linha do grid
                            gridItens.Rows.RemoveAt(row.Index);
                        }
                    });

                    //Incrementa o progressbar
                    IncrementarProgressBar();
                }


                MessageBox.Show("Abastecimento do(s) produto(s) cancelado(s) com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Preenche o Dashboard e algumas informações adicionais
        private void PreencherInformacao()
        {
            //Limpa o título
            chartAbastecimento.Titles.Clear();
            //Limpa os dados
            chartAbastecimento.Series["Abastecimento"].Points.Clear();

            //Títuo do Chart
            chartAbastecimento.Titles.Add("Dashboard Abastecimento").Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter, ((byte)(1)), true);

            chartAbastecimento.ChartAreas[0].Area3DStyle.Enable3D = true;  // set the chartarea to 3D!
            chartAbastecimento.Series["Abastecimento"].IsValueShownAsLabel = true;
            //Total de pedidos
            chartAbastecimento.Series["Abastecimento"].Points.AddXY("Abastecimento", gridItens.Rows.Count);

            if (gridItens.Rows.Count > 0)
            {
                string status;

                int cancelados = 0, pendente = 0, finalizados = 0;

                for (int i = 0; gridItens.Rows.Count > i; i++)
                {
                    status = Convert.ToString(gridItens.Rows[i].Cells[4].Value);

                    if (status.Equals("CANCELADO"))
                    {
                        lblQtdCancelados.Text = Convert.ToString(++cancelados);
                        gridItens.Rows[i].Cells[4].Style.ForeColor = Color.OrangeRed;
                    }

                    if (status.Equals("PENDENTE"))
                    {
                        lblQtdPendentes.Text = Convert.ToString(++pendente);
                        gridItens.Rows[i].Cells[4].Style.ForeColor = Color.Green;
                    }

                    if (status.Equals("FINALIZADO"))
                    {
                        lblQtdFinalizados.Text = Convert.ToString(++finalizados);
                        gridItens.Rows[i].Cells[4].Style.ForeColor = Color.Blue;
                    }
                }

                //Dados do gráfico
                chartAbastecimento.Series["Abastecimento"].Points.AddXY("Pendentes", pendente);
                chartAbastecimento.Series["Abastecimento"].Points.AddXY("Finalizados", finalizados);
                chartAbastecimento.Series["Abastecimento"].Points.AddXY("Cancelados", cancelados);
            }
        }

        //Starta o progressbar
        private void Progressbar(int valor)
        {
            //Garante que o progressbar seja executado da thread que foi iniciado
            Invoke((MethodInvoker)delegate ()
            {
                //Zera o progressbar
                progressBar1.Value = 0;
                //Define um valor para o progressbar
                progressBar1.Maximum = (valor);

            });
        }

        //Incrementa o progressbar
        private void IncrementarProgressBar()
        {
            Invoke((MethodInvoker)delegate ()
            {
                //Incrementar o valor da ProgressBar um valor de uma de cada vez.
                progressBar1.Increment(1);

                if (progressBar1.Value == progressBar1.Maximum)
                {
                    lblProcesso.Visible = false;
                }
            });
        }

        //Exibe o texto do processo do progressbar
        private void MsgLabel(string texto)
        {
            Invoke((MethodInvoker)delegate ()
            {
                lblProcesso.Text = texto;

                lblProcesso.Visible = true;
            });
        }

        
    }
}
