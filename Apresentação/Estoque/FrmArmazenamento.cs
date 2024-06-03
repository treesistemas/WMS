using Negocios;
using ObjetoTransferencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Utilitarios;

namespace Wms
{
    public partial class FrmArmazenamento : Form
    {
        //Array com id
        private int[] regiao;
        private int[] rua;
        //Código do usuário
        public int codUsuario;
        public string empresa;
        public string impressora;
        //Caminho das imagens do produto
        string caminho = null;
        //Objêto que controla os endereços armazenados
        EnderecoPulmaoCollection enderecoPulmaoCollection = new EnderecoPulmaoCollection();

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;


        public FrmArmazenamento()
        {
            InitializeComponent();
        }
        private void FrmArmazenamento_Load(object sender, EventArgs e)
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
        //Changed
        private void cmbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa a rua
            PesqRua();
        }
        private void cmbPalete_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridItens.Rows.Count > 0)
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridItens.CurrentRow;
                //Recebe o indice   
                int i = linha.Index;

                //Seta o tamanho do palete
                gridItens.Rows[i].Cells[12].Value = cmbPalete.SelectedItem;

                //paletização
                if (cmbPalete.Text.Equals("PP"))
                {
                    gridItens.Rows[i].Cells[13].Value = gridItens.Rows[i].Cells[17].Value + " x " + gridItens.Rows[i].Cells[18].Value + " = " + Convert.ToInt32(gridItens.Rows[i].Cells[17].Value) * Convert.ToInt32(gridItens.Rows[i].Cells[18].Value);

                }

                if (cmbPalete.Text.Equals("PM"))
                {
                    //paletização
                    gridItens.Rows[i].Cells[13].Value = gridItens.Rows[i].Cells[19].Value + " x " + gridItens.Rows[i].Cells[20].Value + " = " + Convert.ToInt32(gridItens.Rows[i].Cells[19].Value) * Convert.ToInt32(gridItens.Rows[i].Cells[20].Value);
                }

                if (cmbPalete.Text.Equals("PG"))
                {
                    //paletização
                    gridItens.Rows[i].Cells[13].Value = gridItens.Rows[i].Cells[21].Value + " x " + gridItens.Rows[i].Cells[22].Value + " = " + Convert.ToInt32(gridItens.Rows[i].Cells[21].Value) * Convert.ToInt32(gridItens.Rows[i].Cells[22].Value);
                }

                if (cmbPalete.Text.Equals("PB"))
                {
                    //paletização
                    gridItens.Rows[i].Cells[13].Value = gridItens.Rows[i].Cells[23].Value + " x " + gridItens.Rows[i].Cells[24].Value + " = " + Convert.ToInt32(gridItens.Rows[i].Cells[23].Value) * Convert.ToInt32(gridItens.Rows[i].Cells[24].Value);
                }
            }
        }
        //KeyPress
        private void txtPesqNotaCega_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //foca no botão pesquisar
                btnPesquisar.Focus();
            }
        }
        private void dtmVencimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gridItens.SelectedRows.Count > 0)
                {
                    AdicionaVencimento();
                }

                txtLote.Focus();
            }
        }
        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gridItens.SelectedRows.Count > 0)
                {
                    AdicionaLote();
                }
                txtAssociarPalete.Focus();
            }
        }
        private void txtAssociarPalete_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (gridItens.SelectedRows.Count > 0)
                {
                    AdicionaPalete();
                }
                txtEndereco.Focus();
            }
        }
        private void txtEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Oculta a lista de endereço
                lstEndereco.Visible = false;
                //Foca no campo
                txtQuantidade.Focus();
            }
        }
        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnConfirmar.Focus();
            }
        }
        private void lstEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtEndereco.Text = Convert.ToString(lstEndereco.SelectedItem);
                lstEndereco.Visible = false;
                lstEndereco.Items.Clear();
                txtQuantidade.Focus();
            }
        }

        //Key UP
        private void gridItens_KeyUp(object sender, KeyEventArgs e)
        {
            if (gridItens.Rows.Count > 0)
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridItens.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;
                //Verifica os dados logísticos 
                VerificaDadosLogisticos(indice);
                //Pesquisa os endereços armazenados do produto
                PesqEnderecoProduto();
            }
        }

        //KeyDow
        private void txtEndereco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstEndereco.Items.Count > 0)
                {
                    lstEndereco.Focus();
                    lstEndereco.SelectedIndex = 0;

                }
            }
        }

        //DoubleClick
        private void gridItens_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (gridItens.Rows.Count > 0)
            {
                //Instância o frame
                FrmProduto frame = new FrmProduto();
                frame.Show();

                //Instância as linha da tabela
                DataGridViewRow linha = gridItens.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;
                frame.txtPesqCodigo.Text = Convert.ToString(gridItens.Rows[indice].Cells[4].Value);
                frame.PesqProdutos();
            }
        }

        //Click
        private void cmbRegiao_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbRegiao.Items.Count == 0)
            {
                PesqRegiao();
            }
        }

        private void txtEndereco_TextChanged(object sender, EventArgs e)
        {
            if (txtEndereco.Text != string.Empty)
            {
                ArmazenagemNegocios armazenagemNegocios = new ArmazenagemNegocios();
                string[] endereco = armazenagemNegocios.PesqEnderecoVago(txtEndereco.Text);

                lstEndereco.Items.Clear();
                lstEndereco.Visible = true;

                for (int i = 0; i < endereco.Length; i++)
                {
                    //Preenche o array combobox
                    lstEndereco.Items.Add(endereco[i]);
                }
            }
            else
            {
                lstEndereco.Items.Clear();
                lstEndereco.Visible = false;
            }
        }

        private void mniRemover_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Remove o endereço selecionado
                gridEndereco.Rows.RemoveAt(gridEndereco.CurrentRow.Index);
            }
            else if (acesso[0].excluirFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Remove o endereço selecionado
                gridEndereco.Rows.RemoveAt(gridEndereco.CurrentRow.Index);
            }

        }

        private void mniImprimirEtiqueta_Click(object sender, EventArgs e)
        {
            ImprimirEtiqueta();
        }

        private void mniReimprimirEtiqueta_Click(object sender, EventArgs e)
        {

        }

        private void gridItens_MouseClick(object sender, MouseEventArgs e)
        {
            if (gridItens.Rows.Count > 0)
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridItens.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;
                //Verifica os dados logísticos 
                VerificaDadosLogisticos(indice);
                //Pesquisa os endereços armazenados do produto
                PesqEnderecoProduto();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa a nota cega
            PesqNotaCega();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Pesquisa o endereço ocupado
                PesqEndereco();
            }
            else if (acesso[0].escreverFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Pesquisa o endereço ocupado
                PesqEndereco();
            }
        }

        private void btnAdicionarEndereco_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Adiciona endereço para armazenagem automática
                AdicionarEndereco();
            }
            else if (acesso[0].escreverFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Adiciona endereço para armazenagem automática
                AdicionarEndereco();
            }

        }

        private void btnArmazenarAutomatico_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Amazenagem automática
                ArmazenarProdutoAutomatico();
            }
            else if (acesso[0].escreverFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Amazenagem automática
                ArmazenarProdutoAutomatico();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            //Limpa todos os campos
            LimparCampos();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha a tela
            Dispose();
        }

        //Pesquisa região
        private void PesqRegiao()
        {
            try
            {
                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Limpa o combobox regiao
                cmbRegiao.Items.Clear();
                //Preenche a coleção com apesquisa
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRegiao(cmbEmpresa.Text);
                //Preenche o combobox região
                gerarEnderecoCollection.ForEach(n => cmbRegiao.Items.Add(n.numeroRegiao));
                //Define o tamanho do array para o combobox
                regiao = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array combobox
                    regiao[i] = gerarEnderecoCollection[i].codRegiao;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa rua combobox
        private void PesqRua()
        {
            try
            {
                //Limpa o combobox rua inícial
                cmbRua.Items.Clear();
                //Adiciona o texto
                cmbRua.Text = "Selecione";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa as ruas da região selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRua(regiao[cmbRegiao.SelectedIndex]);
                //Preenche o combobox rua
                gerarEnderecoCollection.ForEach(n => cmbRua.Items.Add(n.numeroRua));
                //Define o tamanho do array
                rua = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    rua[i] = gerarEnderecoCollection[i].codRua;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa a nota cega
        private void PesqNotaCega()
        {
            try
            {
                if (txtPesqNotaCega.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, digite o número da nota cega!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o negocios
                    ArmazenagemNegocios armazenagemNegocios = new ArmazenagemNegocios();
                    //Instância a coleção
                    NotaEntrada notaCega = new NotaEntrada();
                    //Objeto recebe o resultado da consulta
                    notaCega = armazenagemNegocios.PesqNotaCega(txtPesqNotaCega.Text, cmbEmpresa.Text);

                    if (notaCega.codNotaCega > 0)
                    {
                        //passa os valores para os campos Recebe o resultado da coleção    
                        empresa = cmbEmpresa.Text;
                        lblFornecedor.Text = notaCega.codFornecedor + " - " + notaCega.nmFornecedor;
                        lblUsuario.Text = notaCega.usuarioNotaCega;
                        lblConferente.Text = notaCega.conferente;
                        lblDataInicial.Text = string.Format("{0:G}", notaCega.inicioConferencia);
                        lblDataFinal.Text = string.Format("{0:G}", notaCega.fimConferencia);
                        lblQtdNota.Text = Convert.ToString(notaCega.quantidadeNota);
                        lblQtdItens.Text = Convert.ToString(notaCega.quantidadeItens);
                        lblSomaPeso.Text = string.Format(@"{0:N}", notaCega.pesoNota);

                        if (notaCega.exigirValidade == true)
                        {
                            lblExigeVencimento.Text = "SIM";
                        }
                        else
                        {
                            lblExigeVencimento.Text = "NÃO";
                        }

                        if (notaCega.crossDocking == true)
                        {
                            lblCrossDocking.Text = "SIM";
                        }
                        else
                        {
                            lblCrossDocking.Text = "NÃO";
                        }

                        if (!Convert.ToString(notaCega.fimConferencia).Equals(""))
                        {
                            lblStatusConferencia.Text = "CONFERÊNCIA FINALIZADA";
                        }
                        else if (!Convert.ToString(notaCega.inicioConferencia).Equals("") && Convert.ToString(notaCega.fimConferencia).Equals(""))
                        {
                            lblStatusConferencia.Text = "CONFERÊNCIA INÍCIADA";
                        }
                        else if (Convert.ToString(notaCega.inicioConferencia).Equals(""))
                        {
                            lblStatusConferencia.Text = "CONFERÊNCIA PENDENTE";
                        }

                        //Pesquisa os itens da nota cega
                        PesqItens();
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma nota cega encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os itens
        private void PesqItens()
        {
            try
            {
                //Instância o negocios
                ArmazenagemNegocios armazenagemNegocios = new ArmazenagemNegocios();
                //instância a camada de objêto
                ItensNotaEntradaCollection itenPendentes = new ItensNotaEntradaCollection();
                //Limpa o grid de itens
                gridItens.Rows.Clear();
                //Objeto recebe o resultado da consulta
                itenPendentes = armazenagemNegocios.PesqProdutoNotaCega(txtPesqNotaCega.Text,cmbEmpresa.Text);

                if (itenPendentes.Count > 0)
                {
                    itenPendentes.ForEach(n => gridItens.Rows.Add(gridItens.Rows.Count + 1, n.codApartamento, n.descApartamento, n.idProduto, n.codProduto, n.codProduto + " - " + n.descProduto,
                    string.Format("{0:g}", n.quantidadeConferida), n.undPulmao, string.Format("{0:g}", n.quantidadeArmazenada / n.fatorPulmao), string.Format("{0:g}", (n.quantidadeConferida - (n.quantidadeArmazenada / n.fatorPulmao))), string.Format("{0:d}", n.validadeProduto), n.loteProduto, n.tipoPalete, "", n.paleteAssociado,
                    n.NivelMaximo, n.aceitaBlocado, n.lastroPequeno, n.alturaPequeno, n.lastroMedio, n.alturaMedio, n.lastroGrande, n.alturaGrande, n.lastroBlocado, n.alturaBlocado,
                    n.fatorPulmao, n.vidaProduto, n.toleranciaProduto, n.descCategoria, n.controlaVencimentoProduto, n.controlaLoteCategoria, n.pesoProduto, n.tipoArmazenamento, n.undPulmao, "", n.numeroRegiao, n.numeroRua, "", n.pesoVariavel));


                    int fatorPulmao; //Fator pulmão (caixa do produto)
                    double qtdEntrada; //QTD Entrada
                    int qtdArmazenada; //QTD Armazenado
                    bool controlaVencimento;//Controle de vencimento
                    bool pesoVariavel;//Controle de vencimento


                    for (int i = 0; gridItens.Rows.Count > i; i++)
                    {

                        fatorPulmao = Convert.ToInt32(gridItens.Rows[i].Cells[25].Value); //Recebe o fator item do grid
                        qtdEntrada = Convert.ToDouble(gridItens.Rows[i].Cells[6].Value); //Recebe a quantidade do item do grid
                        qtdArmazenada = Convert.ToInt32(gridItens.Rows[i].Cells[8].Value); //Recebe a quantidade armazenada do item do grid
                        controlaVencimento = Convert.ToBoolean(gridItens.Rows[i].Cells[29].Value); //Recebe a opção de controlw
                        pesoVariavel = Convert.ToBoolean(gridItens.Rows[i].Cells[38].Value);



                        //Verifica o controle de validade do produto
                        if (controlaVencimento == false)
                        {
                            //Adiciona a data do dia mais 2 anos
                            gridItens.Rows[i].Cells[10].Value = String.Format("{0:d}", DateTime.Now.AddYears(2).Date);
                        }

                        //Verifica o FATOR PULMÃO
                        if (fatorPulmao == 0)
                        {
                            //quantidade
                            gridItens.Rows[i].Cells[6].Value = 0;
                            //quantidade pendente
                            gridItens.Rows[i].Cells[9].Value = 0;
                        }


                        //Controle do tamanho do palete
                        if (Convert.ToString(gridItens.Rows[i].Cells[12].Value).Equals("PP"))
                        {
                            //paletização
                            gridItens.Rows[i].Cells[13].Value = gridItens.Rows[i].Cells[17].Value + " x " + gridItens.Rows[i].Cells[18].Value + " = " + Convert.ToInt32(gridItens.Rows[i].Cells[17].Value) * Convert.ToInt32(gridItens.Rows[i].Cells[18].Value);
                        }

                        if (Convert.ToString(gridItens.Rows[i].Cells[12].Value).Equals("PM"))
                        {
                            //paletização
                            gridItens.Rows[i].Cells[13].Value = gridItens.Rows[i].Cells[19].Value + " x " + gridItens.Rows[i].Cells[20].Value + " = " + Convert.ToInt32(gridItens.Rows[i].Cells[19].Value) * Convert.ToInt32(gridItens.Rows[i].Cells[20].Value);
                        }

                        if (Convert.ToString(gridItens.Rows[i].Cells[12].Value).Equals("PG"))
                        {
                            //paletização
                            gridItens.Rows[i].Cells[13].Value = gridItens.Rows[i].Cells[21].Value + " x " + gridItens.Rows[i].Cells[22].Value + " = " + Convert.ToInt32(gridItens.Rows[i].Cells[21].Value) * Convert.ToInt32(gridItens.Rows[i].Cells[22].Value);
                        }

                        if (Convert.ToString(gridItens.Rows[i].Cells[12].Value).Equals("PB"))
                        {
                            //paletização
                            gridItens.Rows[i].Cells[13].Value = gridItens.Rows[i].Cells[23].Value + " x " + gridItens.Rows[i].Cells[24].Value + " = " + Convert.ToInt32(gridItens.Rows[i].Cells[23].Value) * Convert.ToInt32(gridItens.Rows[i].Cells[24].Value);
                        }

                        //Verifica os dados logísticos e exibe as cores
                        VerificaDadosLogisticos(i);
                    }

                    //Pesquisa os endereços armazenados do produto
                    PesqEnderecoProduto();

                    //Foca no campo de endereço
                    txtEndereco.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Verifica se há dados logísticos pendentes 
        private void VerificaDadosLogisticos(int i)
        {
            try
            {
                //Dados logísticos
                if (Convert.ToString(gridItens.Rows[i].Cells[12].Value).Equals("") || Convert.ToString(gridItens.Rows[i].Cells[13].Value).Equals("") ||
                     Convert.ToInt32(gridItens.Rows[i].Cells[25].Value).Equals(0) || Convert.ToInt32(gridItens.Rows[i].Cells[25].Value).Equals(0) ||
                     Convert.ToInt32(gridItens.Rows[i].Cells[26].Value).Equals(0) || Convert.ToInt32(gridItens.Rows[i].Cells[27].Value).Equals(0))
                {
                    gridItens.Rows[i].Cells[0].Style.BackColor = Color.Red;
                    gridItens.Rows[i].Cells[0].Style.SelectionBackColor = Color.Red;
                }
                else
                {
                    //quantidade pendente
                    if (Convert.ToDouble(gridItens.Rows[i].Cells[9].Value) > 0)
                    {
                        gridItens.Rows[i].Cells[0].Style.BackColor = Color.LimeGreen;
                        gridItens.Rows[i].Cells[0].Style.SelectionBackColor = Color.LimeGreen;

                    }
                    //quantidade pendente for zero
                    else if (Convert.ToInt32(gridItens.Rows[i].Cells[6].Value) > 0 && Convert.ToDouble(gridItens.Rows[i].Cells[9].Value) == 0)
                    {
                        gridItens.Rows[i].Cells[0].Style.BackColor = Color.RoyalBlue;
                        gridItens.Rows[i].Cells[0].Style.SelectionBackColor = Color.RoyalBlue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao verificar os dados logísticos" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o endereco
        private void PesqEndereco()
        {
            try
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridItens.CurrentRow;
                int indice = 0;
                //Recebe o indice   
                if (linha != null)
                   indice = linha.Index;
    
                if (gridItens.Rows[indice].Cells[0].Style.SelectionBackColor == Color.Red)
                {
                    MessageBox.Show("Por favor, verifique os dados logísticos do SKU!", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEndereco.Focus();
                }
                else if (Convert.ToInt32(gridItens.Rows[indice].Cells[3].Value).Equals(0) || txtEndereco.Text.Equals("") || txtQuantidade.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Convert.ToString(gridItens.Rows[indice].Cells[10].Value).Equals(""))
                {
                    MessageBox.Show("Por favor, digite o vencimento!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o negocios
                    ArmazenagemNegocios armazenagemNegocios = new ArmazenagemNegocios();
                    //Instância a camada de objêtos
                    EstruturaCollection enderecoCollection = new EstruturaCollection();
                    //Objeto recebe o resultado da consulta
                    //enderecoCollection = armazenagemNegocios.PesqEndereco(txtEndereco.Text, cmbEmpresa.Text);
                    enderecoCollection = armazenagemNegocios.PesqEndereco(txtEndereco.Text);


                    //Verifica se o produto existe
                    if (enderecoCollection.Count > 0)
                    {
                        int idProduto = Convert.ToInt32(gridItens.Rows[indice].Cells[3].Value);//quantidade que passou na conferência                       
                        double quantidadeDigitada = Convert.ToDouble(txtQuantidade.Text); //Quantidade digitada                        
                        double quantidadeEntrada = Convert.ToDouble(gridItens.Rows[indice].Cells[6].Value); //quantidade que passou na conferência                        
                        double quantidadeArmazenada = Convert.ToDouble(gridItens.Rows[indice].Cells[8].Value); //quantidade armazenada                        
                        double quantidadePendente = Convert.ToDouble(gridItens.Rows[indice].Cells[9].Value);//quantidade armazenada                        
                        DateTime vencimento = Convert.ToDateTime(gridItens.Rows[indice].Cells[10].Value);//vencimento                         
                        string lote = Convert.ToString(gridItens.Rows[indice].Cells[11].Value);//lote 
                        string tamanhoPaleteProduto = Convert.ToString(gridItens.Rows[indice].Cells[12].Value);// tamanho do palete
                        int nivelMaximo = Convert.ToInt32(gridItens.Rows[indice].Cells[15].Value);//nível do produto                                      
                        bool aceitaBlocado = Convert.ToBoolean(gridItens.Rows[indice].Cells[16].Value);// aceita palete blocado
                        int lastroP = Convert.ToInt32(gridItens.Rows[indice].Cells[17].Value);
                        int alturaP = Convert.ToInt32(gridItens.Rows[indice].Cells[18].Value);
                        int lastroM = Convert.ToInt32(gridItens.Rows[indice].Cells[19].Value);
                        int alturaM = Convert.ToInt32(gridItens.Rows[indice].Cells[20].Value);
                        int lastroG = Convert.ToInt32(gridItens.Rows[indice].Cells[21].Value);
                        int alturaG = Convert.ToInt32(gridItens.Rows[indice].Cells[22].Value);
                        int lastroB = Convert.ToInt32(gridItens.Rows[indice].Cells[23].Value);
                        int alturaB = Convert.ToInt32(gridItens.Rows[indice].Cells[24].Value);
                        int fatorPulmao = Convert.ToInt32(gridItens.Rows[indice].Cells[25].Value); //Quantidade de caixa
                        int qtdEndereco = 0;
                        double qtdProduto = 0;

                        double pesoProduto = Convert.ToDouble(gridItens.Rows[indice].Cells[31].Value) / Convert.ToInt32(gridItens.Rows[indice].Cells[25].Value);//peso do produto/ pelo fator pulmão
                        string tipoArmazenamento = Convert.ToString(gridItens.Rows[indice].Cells[32].Value);
                        string unidadeArmazenamento = Convert.ToString(gridItens.Rows[indice].Cells[33].Value);
                        string autorizaArmazenamento = Convert.ToString(gridItens.Rows[indice].Cells[34].Value);
                        bool pesoVariavel = Convert.ToBoolean(gridItens.Rows[indice].Cells[38].Value);

                        //Verifica o tipo de região do endereço e do produto
                        if (enderecoCollection[0].tipoRegiao != tipoArmazenamento && autorizaArmazenamento.Equals("Não") || enderecoCollection[0].tipoRegiao != tipoArmazenamento && autorizaArmazenamento.Equals(""))
                        {
                            if (MessageBox.Show("O tipo de armzenamento do endereço é diferente do tipo de armzenamento do SKU, deseja continuar?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                            {
                                gridItens.Rows[indice].Cells[34].Value = "Sim";
                            }
                            else
                            {
                                gridItens.Rows[indice].Cells[34].Value = "Não";
                                return;
                            }
                        }

                        //Verifica a disponibilidade do endereço
                        if (enderecoCollection[0].disposicaoApartamento == "NÃO")
                        {
                            MessageBox.Show("O endereço digitado encontra-se indisponível!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        //Verifica o nível do endereço e compara com o nível do produto
                        if (enderecoCollection[0].numeroNivel > nivelMaximo)
                        {
                            MessageBox.Show("O nível do produto é menor que o nível do endereço digitado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        //Verifica a quantidade máxima do produto no palete
                        if (tamanhoPaleteProduto == "PP")
                        {
                            qtdProduto = lastroP * alturaP;
                        }
                        else if (tamanhoPaleteProduto == "PM")
                        {
                            qtdProduto = lastroM * alturaM;
                        }
                        else if (tamanhoPaleteProduto == "PG")
                        {
                            qtdProduto = lastroG * alturaG;

                        }
                        else if (tamanhoPaleteProduto == "PB")
                        {
                            qtdProduto = lastroB * alturaB;
                        }

                        //Verifica a quantidade máxima do endereço por palete
                        if (enderecoCollection[0].tamanhoApartamento == "PP")
                        {
                            qtdEndereco = lastroP * alturaP;
                        }
                        else if (enderecoCollection[0].tamanhoApartamento == "PM")
                        {
                            qtdEndereco = lastroM * alturaM;
                        }
                        else if (enderecoCollection[0].tamanhoApartamento == "PG")
                        {
                            qtdEndereco = lastroG * alturaG;
                        }
                        else if (enderecoCollection[0].tamanhoApartamento == "PB")
                        {
                            qtdEndereco = lastroB * alturaB;
                        }

                        double taxaOcupacao = 0;
                        string mensagem = null;

                        //Soma a taxa de ocupação do endereço
                        for (int i = 0; i < enderecoCollection.Count; i++)
                        {
                            if (enderecoCollection[i].qtdPulmaoOcupado > 0)
                            {

                                taxaOcupacao += Convert.ToDouble(enderecoCollection[i].estoqueProduto / enderecoCollection[i].paleteProduto) * 100;

                                if (i == 0)
                                {
                                    mensagem = "O endereço digitado já se encontra com a taxa de ocupação no limite:\n" + enderecoCollection[0].descProduto.Substring(0, 20) + " /Taxa de ocupação: " + (enderecoCollection[i].estoqueProduto / enderecoCollection[i].paleteProduto) * 100 + "%\n";
                                }
                                else
                                {
                                    mensagem += "" + enderecoCollection[i].descProduto.Substring(0, 20) + " /Taxa de ocupação: " + (enderecoCollection[i].estoqueProduto / enderecoCollection[i].paleteProduto) * 100 + "%\n";
                                }
                            }
                        }                        

                        if(taxaOcupacao > 100)
                        {
                            MessageBox.Show(mensagem);
                            return;
                        }
                        //Verifica se o produto aceita o palete blocado
                        else if (tamanhoPaleteProduto.Equals("PB") && qtdProduto == 0 || tamanhoPaleteProduto.Equals("PB") && aceitaBlocado == false)
                        {
                            MessageBox.Show("SKU não aceita palete blocado, por favor verifique o cadastro!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbPalete.Focus();
                            return;
                        }//Verifica se a quantidade digitada é maior que a quantidade do palete cadastrado
                        else if (quantidadeDigitada > qtdProduto)
                        {
                            MessageBox.Show("A quantidade digitada ultrapassa o palete " + tamanhoPaleteProduto + " do SKU! \nQuantidade máxima: " + qtdProduto + " " + unidadeArmazenamento + ".", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            txtQuantidade.Focus();
                            return;
                        }//Verifica se a quantidade digitada é maior que a do endereço
                        else if (quantidadeDigitada > qtdEndereco)
                        {
                            MessageBox.Show("O endereço é " + enderecoCollection[0].tamanhoApartamento + ", não suporta a quantidade digitada! \nQuantidade máxima: " + qtdEndereco + " " + unidadeArmazenamento + ".", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtQuantidade.Focus();
                            return;
                        }//Verifica se a quantidade digitada é maior que a da entrada
                        else if (quantidadeEntrada < (quantidadeArmazenada + quantidadeDigitada))
                        {
                            MessageBox.Show("A quantidade digitada ultrapassa a quantidade de entrada!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtQuantidade.Focus();
                            txtQuantidade.SelectAll();
                            return;
                        }
                        else if (quantidadeDigitada <= 0)
                        {
                            MessageBox.Show("A quantidade digitada não pode ser menor ou igual a zero!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtQuantidade.Focus();
                            txtQuantidade.SelectAll();
                            return;
                        }//Verifica se existe quantidade para armazenar
                        else if (quantidadePendente == 0)
                        {
                            MessageBox.Show("Não é possível armazenar o SKU selecionado!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else if (enderecoCollection[0].qtdPulmaoOcupado >= enderecoCollection[0].qtdPulmaoMaximo)
                        {
                            MessageBox.Show("O endereço atingiu a quantide máxima de produto no palete!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;
                        }
                        else
                        {
                            bool controlaEndereco = false;

                            int numeroLinha = 0;

                            for (int i = 0; gridArmazenamento.Rows.Count > i; i++)
                            {
                                if (Convert.ToString(gridArmazenamento.Rows[i].Cells[1].Value).Equals(txtEndereco.Text) && quantidadeDigitada + Convert.ToInt32(gridArmazenamento.Rows[i].Cells[2].Value) > qtdProduto)
                                {
                                    MessageBox.Show("Quantidade digitada ultrapassa a quantidade do palete!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                if (Convert.ToString(gridArmazenamento.Rows[i].Cells[1].Value).Equals(txtEndereco.Text))
                                {
                                    numeroLinha = i;
                                    controlaEndereco = true;

                                }
                            }
                            //Armazena o produto
                            ArmazenaProduto(controlaEndereco, numeroLinha, idProduto, enderecoCollection[0].codApartamento, indice, quantidadeEntrada, quantidadePendente, quantidadeArmazenada, quantidadeDigitada, fatorPulmao, pesoProduto, vencimento, lote, pesoVariavel);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Endereço não encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtEndereco.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os endereços do produto armazenado
        private void PesqEnderecoProduto()
        {
            try
            {
                if (enderecoPulmaoCollection.Count == 0)
                {
                    //Instância o negocios
                    ArmazenagemNegocios armazenagemNegocios = new ArmazenagemNegocios();

                    //Limpa o grid de itens
                    gridArmazenamento.Rows.Clear();
                    //Objeto recebe o resultado da consulta
                    enderecoPulmaoCollection = armazenagemNegocios.PesqProdutoArmazenado(txtPesqNotaCega.Text, cmbEmpresa.Text);
                }

                if (enderecoPulmaoCollection.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridItens.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    int idProduto = Convert.ToInt32(gridItens.Rows[indice].Cells[3].Value);
                    int fatorPulmao = Convert.ToInt32(gridItens.Rows[indice].Cells[25].Value);

                    //Localizando os empilhadores e ordena por turno
                    List<EnderecoPulmao> enderecoPulmao = enderecoPulmaoCollection.FindAll(delegate (EnderecoPulmao n) { return n.idProduto == idProduto; }).OrderBy(c => c.descEndereco1).ToList();

                    gridArmazenamento.Rows.Clear();
                    enderecoPulmao.ForEach(n => gridArmazenamento.Rows.Add(gridArmazenamento.Rows.Count + 1, n.descEndereco1, string.Format("{0:g}", n.qtdCaixaOrigem / fatorPulmao)));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Adiciona o vencimento
        private void AdicionaVencimento()
        {
            //Instância a data atual
            DateTime dataAtual = DateTime.Today;

            //Instância as linha da tabela
            DataGridViewRow linha = gridItens.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;

            //Instância a camada de negocios
            ConferenciaCegaNegocios notaCega = new ConferenciaCegaNegocios();

            //Recebe a data do campo vencimento
            DateTime data = dtmVencimento.Value;
            //Recebe a data inserida
            string dataInserida = Convert.ToString(gridItens.Rows[indice].Cells[10].Value);
            int vidaUtil = Convert.ToInt32(gridItens.Rows[indice].Cells[26].Value);
            double tolerancia = Convert.ToDouble(gridItens.Rows[indice].Cells[27].Value);
            bool produtoControlaVencimento = Convert.ToBoolean(gridItens.Rows[indice].Cells[29].Value);



            //Se não controla a validade
            if (produtoControlaVencimento == false)
            {
                gridItens.Rows[indice].Cells[10].Value = String.Format("{0:d}", dataAtual);
                //Adiciona o vencimento do item
                notaCega.AdicionarVencimento(txtPesqNotaCega.Text, Convert.ToInt32(gridItens.Rows[indice].Cells[3].Value), data, cmbEmpresa.Text);
            }
            else if (produtoControlaVencimento == true)
            {
                //Controla a validade ao inserir o produto
                if (String.Format("{0:d}", data) != dataAtual.ToString("d"))
                {
                    int ValidadeMaxima = Convert.ToInt32(vidaUtil - ((tolerancia / 100) * vidaUtil));

                    //Verifica se a data é menor que a data atual
                    if (data < dataAtual)
                    {
                        MessageBox.Show("A data digitada não pode ser menor que a data atual!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //Verifica se a data é maior que a vida útil do produto
                    else if (data > dataAtual.AddDays(vidaUtil))
                    {
                        MessageBox.Show("Data digitada ultrapassa o shelf life do produto!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //Verifica se a data digitada é menor que a tolerância do produto
                    else if (data < dataAtual.AddDays(ValidadeMaxima))
                    {
                        if (MessageBox.Show("A data digitada é menor que a tolerância (" + dataAtual.AddDays(ValidadeMaxima).ToString("d") + ") para o recebimento do produto, deseja continuar?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            gridItens.Rows[indice].Cells[10].Value = String.Format("{0:d}", data);
                            //Adiciona o vencimento do item
                            notaCega.AdicionarVencimento(txtPesqNotaCega.Text, Convert.ToInt32(gridItens.Rows[indice].Cells[3].Value), data, cmbEmpresa.Text);
                            //Adiciona a data atual
                            dtmVencimento.Value = dataAtual;
                        }
                    }
                    //verifica se já existe data inserida
                    else if (!dataInserida.Equals(""))
                    {
                        if (MessageBox.Show("Deseja substituir a data já inserida?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            gridItens.Rows[indice].Cells[10].Value = String.Format("{0:d}", data);
                            //Adiciona o vencimento do item
                            notaCega.AdicionarVencimento(txtPesqNotaCega.Text, Convert.ToInt32(gridItens.Rows[indice].Cells[3].Value), data, cmbEmpresa.Text);

                            return;
                        }
                    }
                    //Se tudo estiver ok, registra a data
                    else
                    {
                        gridItens.Rows[indice].Cells[10].Value = String.Format("{0:d}", data);
                        //Adiciona o vencimento do item
                        notaCega.AdicionarVencimento(txtPesqNotaCega.Text, Convert.ToInt32(gridItens.Rows[indice].Cells[3].Value), data, cmbEmpresa.Text);
                        //Adiciona a data atual
                        dtmVencimento.Value = dataAtual;
                    }
                }

            }
        }

        //Adiciona o lote
        private void AdicionaLote()
        {
            //Instância as linha da tabela
            DataGridViewRow linha = gridItens.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;

            //Instância a camada de negocios
            ConferenciaCegaNegocios notaCega = new ConferenciaCegaNegocios();

            //Controla o lote ao inserir o produto
            if (!txtLote.Text.Equals(""))
            {
                gridItens.Rows[indice].Cells[11].Value = txtLote.Text;
                //Adiciona o lote do item
                notaCega.AdicionarLote(txtPesqNotaCega.Text, Convert.ToInt32(gridItens.Rows[indice].Cells[3].Value), txtLote.Text, cmbEmpresa.Text);

                //Limpa o campo lote
                txtLote.Clear();
            }
        }

        //Adiciona palete
        private void AdicionaPalete()
        {
            try
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridItens.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Instância a camada de negocios
                ConferenciaCegaNegocios notaCega = new ConferenciaCegaNegocios();

                if (!txtAssociarPalete.Text.Equals(""))
                {
                    gridItens.Rows[indice].Cells[14].Value = txtAssociarPalete.Text;
                    //Adiciona a falta do item
                    notaCega.AdicionarProdutoPalete(txtPesqNotaCega.Text, Convert.ToInt32(gridItens.Rows[indice].Cells[3].Value), Convert.ToInt32(txtAssociarPalete.Text), cmbEmpresa.Text);

                    //Limpa o campo de associação
                    txtAssociarPalete.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Adiciona o endereço no grid
        private void AdicionarEndereco()
        {
            try
            {
                //Controla o adicionamento no grid
                bool controle = true;
                //Verifica se o endereço já foi adicionado no grid
                for (int i = 0; gridEndereco.Rows.Count > i; i++)
                {
                    if (Convert.ToString(gridEndereco.Rows[i].Cells[0].Value).Equals(cmbRegiao.Text) && Convert.ToString(gridEndereco.Rows[i].Cells[1].Value).Equals(cmbRua.Text))
                    {
                        controle = false;
                        MessageBox.Show("Endereço já adicionado!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

                if (controle == true)
                {
                    //Adiciona o endereço no grid
                    gridEndereco.Rows.Add(cmbRegiao.Text, cmbRua.Text, cmbLado.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao adicionar o endereço! \nDetalhes: " + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Endereça o produto
        private void ArmazenaProduto(bool controlaQtdEndereco, int numeroLinha, int idProduto, int codEndereco, int indice, double quantidadeEntrada, double quantidadePendente, double quantidadeArmazenada, double quantidadeDigitada, int fatorPulmao, double pesoProduto, DateTime vencimento, string lote, bool pesoVariavel)
        {
            try
            {
                //Instância a camada de negocios
                ArmazenagemNegocios armzenarProduto = new ArmazenagemNegocios();

                //False = Insere o produto no endereço
                if (controlaQtdEndereco == false)
                {
                    //Controla por quantidade
                    if (pesoVariavel == false)
                    {                        
                        //Endereça o produto
                        armzenarProduto.ArmazenarProduto(controlaQtdEndereco, codUsuario, txtPesqNotaCega.Text, cmbEmpresa.Text, codEndereco, idProduto, (quantidadeDigitada * fatorPulmao), pesoProduto, vencimento, lote, (quantidadeArmazenada + quantidadeDigitada) * fatorPulmao);

                        //Registra no rastreamento
                        armzenarProduto.InserirRastreamento(codUsuario, txtPesqNotaCega.Text, cmbEmpresa.Text, codEndereco, idProduto, (quantidadePendente * fatorPulmao), (quantidadeDigitada * fatorPulmao), (pesoProduto * quantidadeDigitada), vencimento, lote);
                    }
                    else
                    {
                        //Endereça o produto
                        armzenarProduto.ArmazenarProduto(controlaQtdEndereco, codUsuario, txtPesqNotaCega.Text, cmbEmpresa.Text, codEndereco, idProduto, 0, quantidadeDigitada, vencimento, lote, (quantidadeArmazenada + quantidadeDigitada) * fatorPulmao);

                        //Registra no rastreamento
                        armzenarProduto.InserirRastreamento(codUsuario, txtPesqNotaCega.Text, cmbEmpresa.Text, codEndereco, idProduto, (quantidadePendente * fatorPulmao), 0, quantidadeDigitada, vencimento, lote);

                    }

                }
                //True = Atualiza a quantidade do produto no endereço
                else if (controlaQtdEndereco == true)
                {
                    int quantidadeEndereco = Convert.ToInt32(gridArmazenamento.Rows[numeroLinha].Cells[2].Value);

                    //Controla por quantidade
                    if (pesoVariavel == false)
                    {
                        //Atualiza o endereço já existente
                        armzenarProduto.ArmazenarProduto(controlaQtdEndereco, codUsuario, txtPesqNotaCega.Text, cmbLado.Text, codEndereco, idProduto, (quantidadeEndereco + quantidadeDigitada) * fatorPulmao, (pesoProduto * quantidadeEndereco + quantidadeDigitada), vencimento, lote, (quantidadeArmazenada + quantidadeDigitada) * fatorPulmao);
                        //Soma a quantidade digitada mais o valor já existente
                        quantidadeDigitada = quantidadeEndereco + quantidadeDigitada;

                        //Registra no rastreamento
                        armzenarProduto.InserirRastreamento(codUsuario, txtPesqNotaCega.Text, cmbEmpresa.Text, codEndereco, idProduto, (quantidadePendente * fatorPulmao), (quantidadeDigitada * fatorPulmao), (pesoProduto * quantidadeDigitada), vencimento, lote);
                    }
                    else
                    {
                        //Endereça o produto
                        armzenarProduto.ArmazenarProduto(controlaQtdEndereco, codUsuario, txtPesqNotaCega.Text, cmbEmpresa.Text, codEndereco, idProduto, 0, quantidadeDigitada, vencimento, lote, (quantidadeArmazenada + quantidadeDigitada) * fatorPulmao);

                        //Registra no rastreamento
                        armzenarProduto.InserirRastreamento(codUsuario, txtPesqNotaCega.Text, cmbEmpresa.Text, codEndereco, idProduto, (quantidadePendente * fatorPulmao), 0, quantidadeDigitada, vencimento, lote);

                    }

                    //Deleta o valor existente da lista
                    enderecoPulmaoCollection.RemoveAt(numeroLinha + 1);
                    //Deleta o endereço existente do grid
                    gridArmazenamento.Rows.RemoveAt(numeroLinha);
                }


                int linha = gridArmazenamento.Rows.Count + 1;
                //Adiciona o endereço 
                gridArmazenamento.Rows.Add(linha, txtEndereco.Text, quantidadeDigitada);

                //Adiciona o endereço a lista
                EnderecoPulmao enderecoPulmao = new EnderecoPulmao();
                enderecoPulmao.idProduto = idProduto;
                enderecoPulmao.descEndereco1 = txtEndereco.Text;
                enderecoPulmao.qtdCaixaOrigem = quantidadeDigitada * fatorPulmao;
                enderecoPulmaoCollection.Add(enderecoPulmao);

                //atualiza o grid de itens para o armazenamento
                gridItens.Rows[indice].Cells[8].Value = quantidadeArmazenada + quantidadeDigitada;
                gridItens.Rows[indice].Cells[9].Value = quantidadeEntrada - (quantidadeArmazenada + quantidadeDigitada);

                //Foca no grid Armazenagem
                gridArmazenamento.Focus();
                //Exibe a última linha
                gridArmazenamento.CurrentCell = gridArmazenamento.Rows[gridArmazenamento.Rows.Count - 1].Cells[0];
                //Limpa o campo
                txtEndereco.Clear();
                txtQuantidade.Clear();
                //Foca no campo endereço
                txtEndereco.Focus();


                //Muda o status - Verifica se existe quantidade para armazenar
                VerificaDadosLogisticos(indice);


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Endereça o produto automático
        private void ArmazenarProdutoAutomatico()
        {
            try
            {
                //Instância a camada de negocios
                ArmazenagemNegocios armazenagemNegocios = new ArmazenagemNegocios();
                //Instância a camada de objetos
                EstruturaCollection enderecoCollection = new EstruturaCollection();

                //Percorre todos os itens
                for (int i = 0; gridItens.Rows.Count > i; i++)
                {
                    //Foca no grid
                    gridItens.Focus();
                    //Foca na linha do item
                    gridItens.CurrentCell = gridItens.Rows[i].Cells[0];

                    //dados logísticos do produto
                    int idProduto = Convert.ToInt32(gridItens.Rows[i].Cells[3].Value);//quantidade que passou na conferência                       
                    int quantidadeDigitada = 0;
                    int quantidadeEntrada = Convert.ToInt32(gridItens.Rows[i].Cells[6].Value); //quantidade que passou na conferência                        
                    int quantidadeArmazenada = Convert.ToInt32(gridItens.Rows[i].Cells[8].Value); //quantidade armazenada                        
                    int quantidadePendente = Convert.ToInt32(gridItens.Rows[i].Cells[9].Value);//quantidade armazenada                        
                    DateTime vencimento = Convert.ToDateTime(gridItens.Rows[i].Cells[10].Value);//vencimento                         
                    string lote = Convert.ToString(gridItens.Rows[i].Cells[11].Value);//lote 
                    string tamanhoPaleteProduto = Convert.ToString(gridItens.Rows[i].Cells[12].Value);// tamanho do palete
                    int nivelMaximo = Convert.ToInt32(gridItens.Rows[i].Cells[15].Value);//nível do produto                                      
                    bool aceitaBlocado = Convert.ToBoolean(gridItens.Rows[i].Cells[16].Value);// aceita palete blocado
                    int lastroP = Convert.ToInt32(gridItens.Rows[i].Cells[17].Value);
                    int alturaP = Convert.ToInt32(gridItens.Rows[i].Cells[18].Value);
                    int lastroM = Convert.ToInt32(gridItens.Rows[i].Cells[19].Value);
                    int alturaM = Convert.ToInt32(gridItens.Rows[i].Cells[20].Value);
                    int lastroG = Convert.ToInt32(gridItens.Rows[i].Cells[21].Value);
                    int alturaG = Convert.ToInt32(gridItens.Rows[i].Cells[22].Value);
                    int lastroB = Convert.ToInt32(gridItens.Rows[i].Cells[23].Value);
                    int alturaB = Convert.ToInt32(gridItens.Rows[i].Cells[24].Value);


                    double pesoProduto = Convert.ToDouble(gridItens.Rows[i].Cells[31].Value);//peso do produto
                    string tipoArmazenamento = Convert.ToString(gridItens.Rows[i].Cells[32].Value);
                    string unidadeArmazenamento = Convert.ToString(gridItens.Rows[i].Cells[33].Value);
                    string autorizaArmazenamento = Convert.ToString(gridItens.Rows[i].Cells[34].Value);

                    //Passa o numero da regiao, rua, nivel
                    int regiao = Convert.ToInt32(gridItens.Rows[i].Cells[35].Value);
                    int rua = Convert.ToInt32(gridItens.Rows[i].Cells[36].Value);
                    int nivel = Convert.ToInt32(gridItens.Rows[i].Cells[15].Value);

                    //Pesquisa os endereços - verifica o picking, nível, categoria, tipo da região
                    enderecoCollection = armazenagemNegocios.PesqEnderecoAutomatico(regiao, rua, nivel);


                    // MessageBox.Show("SKU:" + gridItens.Rows[i].Cells[3].Value); 
                    foreach (Estrutura e in enderecoCollection)
                    {
                        if (quantidadePendente > 0)
                        {
                            //Verifica a paletização
                            if (tamanhoPaleteProduto.Equals("PP"))
                            {
                                if ((lastroP * alturaP) < quantidadePendente)
                                {
                                    quantidadeDigitada = lastroP * alturaP;
                                }
                                else
                                {
                                    quantidadeDigitada = quantidadePendente;
                                }
                            }

                            if (tamanhoPaleteProduto.Equals("PM"))
                            {
                                if ((lastroM * alturaM) < quantidadePendente)
                                {
                                    quantidadeDigitada = lastroM * alturaM;
                                }
                                else
                                {
                                    quantidadeDigitada = quantidadePendente;
                                }
                            }

                            if (tamanhoPaleteProduto.Equals("PG"))
                            {
                                if ((lastroG * alturaG) < quantidadePendente)
                                {
                                    quantidadeDigitada = lastroG * alturaG;
                                }
                                else
                                {
                                    quantidadeDigitada = quantidadePendente;
                                }
                            }

                            if (tamanhoPaleteProduto.Equals("PB"))
                            {
                                if ((lastroB * alturaB) < quantidadePendente)
                                {
                                    quantidadeDigitada = lastroB * alturaB;
                                }
                                else
                                {
                                    quantidadeDigitada = quantidadePendente;
                                }
                            }

                            //Endereça o produto
                            armazenagemNegocios.ArmazenarProduto(false, codUsuario, txtPesqNotaCega.Text, cmbLado.Text, e.codApartamento, idProduto, quantidadeDigitada, (pesoProduto * quantidadeDigitada), vencimento, lote, (quantidadeArmazenada + quantidadeDigitada));

                            //Remove o endereço ultilizado
                            //enderecoCollection.RemoveAll((x) => x.idApartamento == Convert.ToInt32(e.idApartamento));

                            //Controla a numeração dos paletes armazenados
                            int linha = gridArmazenamento.Rows.Count + 1;
                            //Adiciona o endereço 
                            gridArmazenamento.Rows.Add(linha, e.descApartamento, quantidadeDigitada);

                            //Adiciona o endereço a lista
                            EnderecoPulmao enderecoPulmao = new EnderecoPulmao();
                            enderecoPulmao.idProduto = idProduto;
                            enderecoPulmao.descEndereco1 = e.descApartamento;
                            enderecoPulmao.qtdCaixaOrigem = quantidadeDigitada;
                            enderecoPulmaoCollection.Add(enderecoPulmao);

                            //atualiza o grid de itens para o armazenamento
                            gridItens.Rows[i].Cells[8].Value = quantidadeArmazenada + quantidadeDigitada;
                            gridItens.Rows[i].Cells[9].Value = quantidadeEntrada - (quantidadeArmazenada + quantidadeDigitada);

                            if (quantidadeArmazenada == quantidadeEntrada)
                            {
                                //Muda o status - Verifica se existe quantidade para armazenar
                                VerificaDadosLogisticos(i);
                            }

                            //Atualiza a quantidades 
                            quantidadeArmazenada = Convert.ToInt32(gridItens.Rows[i].Cells[8].Value); //quantidade armazenada                        
                            quantidadePendente = Convert.ToInt32(gridItens.Rows[i].Cells[9].Value);//quantidade armazenada 

                        }
                    }


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Imprimi a etiqueta
        private void ImprimirEtiqueta()
        {
            try
            {
                //Pega o caminho da etiqueta prn
                string etiqueta = null;
                etiqueta = AppDomain.CurrentDomain.BaseDirectory + "ZEBRA PULMAO 40X100.prn";
                
                if (impressora.Equals("ARGOX 214"))
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "PulmaoArgox214_40x100.prn";

                }

                else if (impressora.Equals("ARGOX 214 PLUS"))
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "PulmaoArgox214Plus_40x100.prn";
                }

                else if (impressora.Equals("ZEBRA"))
                {
                    etiqueta = AppDomain.CurrentDomain.BaseDirectory + "ZEBRA PULMAO 40X100.prn";
                }
                
                //Instância as linha da tabela
                DataGridViewRow linha = gridItens.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                for (int i = 0; gridArmazenamento.Rows.Count > i; i++)
                {
                    //Instância a camada de negocios
                    ArmazenagemNegocios armazenagemNegocios = new ArmazenagemNegocios();

                    //Caminho do novo arquivo atualizado
                    string NovaEtiqueta = AppDomain.CurrentDomain.BaseDirectory + "Pulmao_40x100.txt";

                    // Abre o arquivo para escrita
                    StreamReader streamReader;
                    streamReader = File.OpenText(etiqueta);

                    string contents = streamReader.ReadToEnd();

                    streamReader.Close();

                    string conteudo = contents;

                    StreamWriter streamWriter = File.CreateText(NovaEtiqueta);

                    // Atualizo as variaveis do arquivo
                    conteudo = conteudo.Replace("EMPRESA....................", cmbEmpresa.Text);
                    conteudo = conteudo.Replace("SKU", Convert.ToString(gridItens.Rows[indice].Cells[5].Value));
                    conteudo = conteudo.Replace("L1", Convert.ToString(gridItens.Rows[indice].Cells[19].Value));
                    conteudo = conteudo.Replace("L2", Convert.ToString(gridItens.Rows[indice].Cells[20].Value));
                    conteudo = conteudo.Replace("Q1", Convert.ToString(gridArmazenamento.Rows[i].Cells[2].Value) + " " + Convert.ToString(gridItens.Rows[indice].Cells[7].Value));
                    conteudo = conteudo.Replace("DT", string.Format("{0:d}", gridItens.Rows[indice].Cells[10].Value));
                    conteudo = conteudo.Replace("PS", string.Format("{0:n}", Convert.ToDouble(gridItens.Rows[indice].Cells[31].Value) * Convert.ToInt32(gridArmazenamento.Rows[i].Cells[2].Value)));
                    conteudo = conteudo.Replace("LT", Convert.ToString(gridItens.Rows[indice].Cells[11].Value));
                    conteudo = conteudo.Replace("D2", string.Format("{0:d}", DateTime.Now));
                    conteudo = conteudo.Replace("NRNOTA", Convert.ToString(txtPesqNotaCega.Text));
                    conteudo = conteudo.Replace("ENDERECO", Convert.ToString(gridArmazenamento.Rows[i].Cells[1].Value));
                    //string.Format("{0:d}",string.Format("{0:n}",

                    streamWriter.Write(conteudo);

                    streamWriter.WriteLine("E"); //Fim do modo de formatação e imprime

                    streamWriter.WriteLine("/222"); //Avanço para corte da etiqueta
                    streamWriter.Close();

                    PrintDialog pd = new PrintDialog();

                    pd.PrinterSettings = new PrinterSettings();

                    if (pd.PrinterSettings.IsValid)
                    {
                        ArgoxPPLA.RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, NovaEtiqueta);

                        //File.Delete(AppDomain.CurrentDomain.BaseDirectory + NovaEtiqueta);

                        armazenagemNegocios.AtualizaImpressaoEtiqueta(cmbEmpresa.Text, Convert.ToInt32(gridItens.Rows[indice].Cells[3].Value), txtPesqNotaCega.Text);
                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrada nenhuma impressora instalada no seu computador!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro na geração da etiqueta: " + ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Limpa todos os campos
        private void LimparCampos()
        {
            txtPesqNotaCega.Clear(); //Limpa o campo nota cega
            dtmVencimento.Value = DateTime.Today; //Recebe a data atual
            txtLote.Clear(); //Limpa o campo lote
            txtAssociarPalete.Clear(); //Limpa o campo associação
            cmbPalete.SelectedItem = "PM"; //Seleciona o PM como padrão
            txtEndereco.Clear(); //Limpa o campo endereço
            txtQuantidade.Clear(); //Limpa o campo quantidade

            cmbRegiao.Text = ""; //Limpa o campo regiao
            cmbRua.Text = ""; //Limpa o campo regiao
            cmbLado.Text = ""; //Limpa o lado

            gridEndereco.Rows.Clear(); //Limpa o grid de endereço (Armazenagem automática)
            gridItens.Rows.Clear(); //Limpa o grid de itens da nota cega
            gridArmazenamento.Rows.Clear(); //Limpa o grid de armazenamento

            lblUsuario.Text = "-"; //Limpa o label Login do usuário
            lblQtdNota.Text = "-"; //Limpa o label Quanidade de notas
            lblExigeVencimento.Text = "-"; //Limpa o label Exige vencimento
            lblExigeLote.Text = "-"; //Limpa o label Exige lote
            lblCrossDocking.Text = "-"; //Limpa o label Crossdocking

            lblConferente.Text = "-"; //Limpa o label conferente
            lblDataInicial.Text = "-";//Limpa o label data inicial de conferencia
            lblDataFinal.Text = "-";//Limpa o label data final da conferencia
            lblStatusConferencia.Text = "-";//Limpa o label status de conferencia

            lblTipoArmazenagem.Text = "-";//Limpa o label tipo de armazenagem
            lblQtdItens.Text = "-";//Limpa o label quantidade de itens
            lblSomaPeso.Text = "-";//Limpa o label peso total
            lblStatus.Text = "-";//Limpa o label status do armazenamento

            txtPesqNotaCega.Focus(); //Foca no campo nota cega


        }

        private void lstEndereco_MouseClick(object sender, MouseEventArgs e)
        {
            txtEndereco.Text = Convert.ToString(lstEndereco.SelectedItem);
            lstEndereco.Visible = false;
            lstEndereco.Items.Clear();
            txtQuantidade.Focus();
        }

        
    }
}
