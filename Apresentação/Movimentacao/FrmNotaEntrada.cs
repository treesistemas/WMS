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
    public partial class FrmNotaEntrada : Form
    {
        public int codUsuario;
        public string loginUsuario;

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;


        //Instância a camada de objetos - Controla a exibição dos itens da nota
        ItensNotaEntradaCollection itensNotaEntradaCollection = new ItensNotaEntradaCollection();

        //Instância a camada de objetos - Controla a exibição dos itens da nota cega
        ItensNotaEntradaCollection itensNotaCegaCollection = new ItensNotaEntradaCollection();

        
        public FrmNotaEntrada()
        {
            InitializeComponent();
        }

        private void FrmNotaEntrada_Load(object sender, EventArgs e)
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
        private void txtNota_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtPesqNota.Text == "")
                {
                    txtPesqNotaCega.Focus();
                }
                else if (txtPesqNota.Text != "" && txtPesqNotaCega.Text == "")
                {
                    PesqNotaEntrada();
                }
            }
        }

        private void txtEspelho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtPesqNotaCega.Text == "")
                {
                    txtPesqCodFornecedor.Focus();
                }
                else if (txtPesqNota.Text == "" && txtPesqNotaCega.Text != "")
                {
                    PesqNotaEntrada();
                }
            }
        }

        private void dtmInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo data final
                dtmFinal.Focus();
            }
        }

        private void dtmFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo data final
                btnPesquisar.Focus();
            }
        }

        //Changed
        private void chkComNotaCega_CheckedChanged(object sender, EventArgs e)
        {
            if (chkComNotaCega.Checked == true)
            {
                chkSemNotaCega.Checked = false;
            }
        }

        private void chkSemNotaCega_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSemNotaCega.Checked == true)
            {
                chkComNotaCega.Checked = false;
            }
        }

        private void chkConferido_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConferido.Checked == true)
            {
                chkNaoConferido.Checked = false;
            }
        }

        private void chkNaoConferido_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNaoConferido.Checked == true)
            {
                chkConferido.Checked = false;
            }
        }

        //KeyDown
        private void txtFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    //Instância o formulário
                    FrmPesqFornecedor frame = new FrmPesqFornecedor();
                    frame.ShowDialog();

                    if (Convert.ToInt32(frame.codFornecedor) > 0)
                    {
                        //Adiciona o valor encontrado
                        txtPesqCodFornecedor.Text = Convert.ToString(frame.codFornecedor);
                        txtPesqFornecedor.Text = Convert.ToString(frame.nmFornecedor);
                        //Foca no campo data inicial
                        dtmInicial.Focus();

                        return;
                    }
                    //Foca no campo fornecedor
                    txtPesqCodFornecedor.Focus();

                    break;

                case Keys.Delete:
                    //Limpa o campo nome do fornecedor
                    txtPesqFornecedor.Clear();

                    break;

                case Keys.Back:
                    //Limpa o campo nome do fornecedor
                    txtPesqFornecedor.Clear();

                    break;

                case Keys.Enter:
                    //Pesquisa o fornecedor
                    if (txtPesqCodFornecedor.Text == "")
                    {
                        dtmInicial.Focus();
                    }
                    else
                    {
                        //Pesquisa o fornecedor pelo código
                        PesqFornecedor();
                    }

                    break;
            }
        }

        //KeyUp
        private void gridNota_KeyUp(object sender, KeyEventArgs e)
        {
            //Pesquisa os dados da nota cega
            PesqNotaCega();
            //Pesquisa os itens da nota
            PesqItensNota();
        }

        private void gridNotaCega_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Habilitas os campos de controle
            HabilitarCampos();
        }

        //Mouse click
        private void gridNota_MouseClick(object sender, MouseEventArgs e)
        {
            //Pesquisa os dados da nota cega
            PesqNotaCega();
            //Pesquisa os itens da nota
            PesqItensNota();
        }
        //Click
        private void mniGerarNotaCega_Click(object sender, EventArgs e)
        {
            //Gera nota Cega
            GerarNotaCega();
        }

        private void mniAddNotaNotaCega_Click(object sender, EventArgs e)
        {
            //Adiciona a nota de entrada a uma nota fiscal já existente
            AdicionarNota();
        }

        private void mniRemoverNota_Click(object sender, EventArgs e)
        {
            //Remove a nota ou espelho
            RemoverNotaNotaCega();
        }

        private void mniPaletizacao_Click(object sender, EventArgs e)
        {
            //Instância as linha da tabela
            DataGridViewRow linha = gridNota.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;

            //Recebe o número da nota cega
            int notaCega = Convert.ToInt32(gridNota.Rows[indice].Cells[6].Value);

            FrmPaletizacaoNotaCega frame = new FrmPaletizacaoNotaCega();
            frame.GerarRelatorio(notaCega, cmbEmpresa.Text);

            //Instância a camda de negocios
            NotaEntradaNegocios notaEntradaNegocios = new NotaEntradaNegocios();
            //Pesquisa os itens sem dados logísticos
            ItensNotaEntradaCollection itensCollection = notaEntradaNegocios.PesqItensSemDadosLogisticos(Convert.ToString(gridNota.Rows[indice].Cells[6].Value), cmbEmpresa.Text);
        
            if(itensCollection.Count > 0)
            {
                string mensagem = null;
                
                for( int i = 0; i < itensCollection.Count; i++)
                {
                    mensagem = itensCollection[i].codProduto + " - " + itensCollection[i].descProduto +"\n";
                }

                MessageBox.Show("Existe itens com dados logísticos pendêntes: \n" + mensagem);
            }
        
        }

        private void mniDivergencias_Click(object sender, EventArgs e)
        {
            //Instância as linha da tabela
            DataGridViewRow linha = gridNota.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;

            //Recebe o número da nota cega
            int notaCega = Convert.ToInt32(gridNota.Rows[indice].Cells[6].Value);

            //Instância a camda de negocios
            NotaEntradaNegocios notaEntradaNegocios = new NotaEntradaNegocios();
            //Pesquisa os itens sem dados logísticos
            ItensNotaEntradaCollection itensCollection = notaEntradaNegocios.PesqItensSemDadosLogisticos(Convert.ToString(gridNota.Rows[indice].Cells[6].Value), cmbEmpresa.Text);


            if (itensCollection.Count > 0)
            {
                string mensagem = null;

                for (int i = 0; i < itensCollection.Count; i++)
                {
                    mensagem = itensCollection[i].codProduto + " - " + itensCollection[i].descProduto + "\n";
                }

                MessageBox.Show("Existe itens com dados logísticos pendêntes: \n" + mensagem);
            }
            else
            {

                FrmDivergenciaNotaCega frame = new FrmDivergenciaNotaCega();
                frame.GerarRelatorio(notaCega, "Divergência", cmbEmpresa.Text);
            }
        }

        private void mniConferencia_Click(object sender, EventArgs e)
        {
            //Instância as linha da tabela
            DataGridViewRow linha = gridNota.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;

            //Recebe o número da nota cega
            int notaCega = Convert.ToInt32(gridNota.Rows[indice].Cells[6].Value);

            //Instância a camda de negocios
            NotaEntradaNegocios notaEntradaNegocios = new NotaEntradaNegocios();
            //Pesquisa os itens sem dados logísticos
            ItensNotaEntradaCollection itensCollection = notaEntradaNegocios.PesqItensSemDadosLogisticos(Convert.ToString(gridNota.Rows[indice].Cells[6].Value), cmbEmpresa.Text);


            if (itensCollection.Count > 0)
            {
                string mensagem = null;

                for (int i = 0; i < itensCollection.Count; i++)
                {
                    mensagem = itensCollection[i].codProduto + " - " + itensCollection[i].descProduto + "\n";
                }

                MessageBox.Show("Existe itens com dados logísticos pendêntes: \n" + mensagem);
            }
            else
            {
                FrmDivergenciaNotaCega frame = new FrmDivergenciaNotaCega();
                frame.GerarRelatorio(notaCega, "Conferência", cmbEmpresa.Text);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtPesqNota.Text != "" && txtPesqNotaCega.Text != "")
            {
                MessageBox.Show("Selecione apenas um campo para pesquisa!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                PesqNotaEntrada();
            }

        }

        private void btnAtualizarControles_Click(object sender, EventArgs e)
        {
            //Atualiza os controles de entrada da nota cega
            AtualizarControlesEntrada();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha a tela
            Close();
        }

        //Pesquisa o fornecedor pelo código
        private void PesqFornecedor()
        {
            try
            {
                if (!txtPesqCodFornecedor.Text.Equals(""))
                {
                    //Instância o negocios
                    FornecedorNegocios pesqFornecedorNegocios = new FornecedorNegocios();
                    //Instância a coleçãO
                    FornecedorCollection fornecedorCollection = new FornecedorCollection();
                    //A coleção recebe o resultado da consulta
                    fornecedorCollection = pesqFornecedorNegocios.pesqFornecedor("", Convert.ToInt32(txtPesqCodFornecedor.Text));

                    if (fornecedorCollection.Count > 0)
                    {
                        //Limpa o nome do fornecedor
                        txtPesqFornecedor.Clear();
                        //Seta o nome do fornecedor
                        txtPesqFornecedor.Text = fornecedorCollection[0].nomeFornecedor;

                        //Foca no campo data inicial
                        dtmInicial.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum fornecedor encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa as notas de entrada
        private void PesqNotaEntrada()
        {
            try
            {
                //Instância o negocios
                NotaEntradaNegocios notaEntradaNegocios = new NotaEntradaNegocios();
                //Instância a coleção
                NotaEntradaCollection notaEntradaCollection = new NotaEntradaCollection();
                //A coleção recebe o resultado da consulta
                notaEntradaCollection = notaEntradaNegocios.PesqNotaEntrada(txtPesqNota.Text, txtPesqNotaCega.Text, txtPesqCodFornecedor.Text, dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), chkComNotaCega.Checked, chkSemNotaCega.Checked, chkConferido.Checked, chkNaoConferido.Checked, cmbEmpresa.Text);
                //Limpa o grid
                gridNota.Rows.Clear();
                gridNotaCega.Rows.Clear();
                //Grid Recebe o resultado da coleção
                notaEntradaCollection.ForEach(n => gridNota.Rows.Add(n.dataNota, n.nota, n.codFornecedor, n.codFornecedor + " - " + n.nmFornecedor, string.Format(@"{0:N}", n.pesoNota), Convert.ToString(n.dataNotaCega), n.codNotaCega, n.usuarioNotaCega, n.conferente, n.inicioConferencia, n.fimConferencia, n.tempoConferencia, n.liberarEstoque, n.liberarArmazenagem, n.armzenagemPicking, n.exigirValidade, n.tipoRelatorio, n.crossDocking));

                //Preenche o grupo de informação
                PesqInformacao();

                if (gridNota.RowCount > 0)
                {
                    //Seleciona a primeira linha do grid
                    gridNota.CurrentCell = gridNota.Rows[0].Cells[1];
                    //Foca no grid
                    gridNota.Focus();
                    //Pesquisa os dados da nota cega
                    PesqNotaCega();
                    //Limpa a coleção global de itens para que seja atualizada
                    itensNotaEntradaCollection.Clear();
                    //Pesquisa os itens da nota
                    PesqItensNota();
                }
                else
                {
                    //Limpa o itens da nota cega
                    gridItensNotaCega.Rows.Clear();

                    MessageBox.Show("Nenhuma nota de entrada encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa oss itens da nota Fiscal
        private void PesqItensNota()
        {
            try
            {
                if (gridNota.Rows.Count > 0)
                {
                    //Instância as linha do grid
                    DataGridViewRow linha = gridNota.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Localizando os itens da nota
                    List<ItensNotaEntrada> itensNota = itensNotaEntradaCollection.FindAll(delegate (ItensNotaEntrada n) { return n.codNota == Convert.ToInt32(gridNota.Rows[indice].Cells[1].Value); });

                    if (itensNota.Count == 0)
                    {
                        //Instância o negocios
                        NotaEntradaNegocios notaEntradaNegocios = new NotaEntradaNegocios();
                        //A coleção recebe o resultado da consulta
                        itensNotaEntradaCollection = notaEntradaNegocios.PesqItensNota(txtPesqNota.Text, txtPesqNotaCega.Text, txtPesqCodFornecedor.Text, dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), chkComNotaCega.Checked, chkSemNotaCega.Checked, chkConferido.Checked, chkNaoConferido.Checked, cmbEmpresa.Text);

                        //Recebe o itens da nota
                        itensNota = itensNotaEntradaCollection.FindAll(delegate (ItensNotaEntrada n) { return n.codNota == Convert.ToInt32(gridNota.Rows[indice].Cells[1].Value); });
                    }

                    //Limpa o grid
                    gridItensNota.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    itensNota.ForEach(n => gridItensNota.Rows.Add(n.idProduto, n.codProduto, n.descProduto, n.quantidadeNota));
                    //Pesquisa as informações da nota
                    PesqInformaçãoNota();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa no grid de notas as informações da nota cega
        private void PesqNotaCega()
        {
            try
            {
                //Limpa o grid da nota cega
                gridNotaCega.Rows.Clear();
                //Limpa o grid da itens nota cega
                gridItensNotaCega.Rows.Clear();
                //Limpar os controles
                LimparCampos();

                if (gridNota.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridNota.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Se a nota possui nota cega
                    if (Convert.ToString(gridNota.Rows[indice].Cells[6].Value) != string.Empty)
                    {
                        //Seta o valor do código
                        gridNotaCega.Rows.Add(gridNota.Rows[indice].Cells[5].Value, gridNota.Rows[indice].Cells[6].Value, gridNota.Rows[indice].Cells[8].Value, gridNota.Rows[indice].Cells[9].Value, gridNota.Rows[indice].Cells[10].Value, gridNota.Rows[indice].Cells[11].Value);

                        if (Convert.ToString(gridNota.Rows[indice].Cells[11].Value) != string.Empty)
                        {
                            gridNotaCega.Rows[0].Cells[6].Value = "Conferência Finalizada";
                            gridNotaCega.Rows[0].Cells[6].Style.ForeColor = Color.MediumSeaGreen;
                        }
                        else if (Convert.ToString(gridNota.Rows[indice].Cells[9].Value) != string.Empty && Convert.ToString(gridNota.Rows[indice].Cells[10].Value) == string.Empty)
                        {
                            gridNotaCega.Rows[0].Cells[6].Value = "Conferência Iníciada";
                            gridNotaCega.Rows[0].Cells[6].Style.ForeColor = Color.SteelBlue;
                        }
                        else if (Convert.ToString(gridNota.Rows[indice].Cells[9].Value) == string.Empty)
                        {
                            gridNotaCega.Rows[0].Cells[6].Value = "Não Iniciada";
                            gridNotaCega.Rows[0].Cells[6].Style.ForeColor = Color.Red;
                        }

                        chkEstoqueErpUpdate.Checked = Convert.ToBoolean(gridNota.Rows[indice].Cells[12].Value);
                        chkArmazenagemFimUpdate.Checked = Convert.ToBoolean(gridNota.Rows[indice].Cells[13].Value);
                        chkSkuPickingUpdate.Checked = Convert.ToBoolean(gridNota.Rows[indice].Cells[14].Value);
                        chkExigirValidadeUpdate.Checked = Convert.ToBoolean(gridNota.Rows[indice].Cells[15].Value);
                        chkTipoRelatorioUpdate.Checked = Convert.ToBoolean(gridNota.Rows[indice].Cells[16].Value);
                        chkCrossDockingUpdate.Checked = Convert.ToBoolean(gridNota.Rows[indice].Cells[17].Value);

                        //Pesquisa os itens da nota cega
                        PesqItensNotaCega();
                        //Limpa a seleção do grid nota
                        gridNotaCega.ClearSelection();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu ao exibir os dados da nota cega! \nDetalhes " + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa oss itens da nota Fiscal
        private void PesqItensNotaCega()
        {
            try
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridNota.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Limpa o grid
                gridItensNotaCega.Rows.Clear();

                //Se existir uma nota cega
                if (!gridNota.Rows[indice].Cells[6].Value.Equals(""))
                {
                    //Localizando os itens da nota
                    List<ItensNotaEntrada> itensNotaCega = itensNotaCegaCollection.FindAll(delegate (ItensNotaEntrada n) { return n.codNotaCega == Convert.ToInt32(gridNota.Rows[indice].Cells[6].Value); });

                    //Controla a quantide de itens no grid
                    int i = 1;

                    //Grid Recebe o resultado da coleção
                    itensNotaCega.ForEach(n => gridItensNotaCega.Rows.Add(i++, n.codProduto + " - " + n.descProduto, n.quantidadeNota, n.quantidadeConferida, n.quantidadeAvariada, n.quantidadeFalta, n.loteProduto, string.Format("{0:dd/MM/yyyy}", n.validadeProduto), string.Format(@"{0:N}", n.pesoProduto)));

                    if (itensNotaCega.Count == 0)
                    {
                        //Instância o negocios
                        NotaEntradaNegocios notaEntradaNegocios = new NotaEntradaNegocios();
                        //A coleção recebe o resultado da consulta
                        itensNotaCegaCollection = notaEntradaNegocios.PesqItensNotaCega(txtPesqNota.Text, txtPesqNotaCega.Text, txtPesqCodFornecedor.Text, dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), chkComNotaCega.Checked, chkSemNotaCega.Checked, chkConferido.Checked, chkNaoConferido.Checked, cmbEmpresa.Text);

                        itensNotaCega = itensNotaCegaCollection.FindAll(delegate (ItensNotaEntrada n) { return n.codNotaCega == Convert.ToInt32(gridNota.Rows[indice].Cells[6].Value); });

                        //Grid Recebe o resultado da coleção
                        itensNotaCega.ForEach(n => gridItensNotaCega.Rows.Add(i++, n.codProduto + " - " + n.descProduto, n.quantidadeNota, n.quantidadeConferida, n.quantidadeAvariada, n.loteProduto, string.Format("{0:dd/MM/yyyy}", n.validadeProduto), string.Format(@"{0:N}", n.pesoProduto)));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Informação da pesquisa
        private void PesqInformacao()
        {
            //Qtd de nota encontrada
            lblTotalNota.Text = gridNota.RowCount.ToString();

            lblTotalComNotaCega.Text = "0";
            lblTotalSemNotaCega.Text = "0";
            lblTotalConferido.Text = "0";
            lblTotalNaoConferido.Text = "0";

            int i = 0;
            //Qtd com nota cega
            int comNotaCega = 1;
            //Qtd sem nota cega
            int semNotaCega = 1;
            //Qtd conferido
            int conferido = 1;
            //Qtd nao conferido
            int naoConferido = 1;

            while (gridNota.RowCount > i)
            {
                //Com espelho
                if (!Convert.ToString(gridNota.Rows[i].Cells[6].Value).Equals(""))
                {
                    lblTotalComNotaCega.Text = Convert.ToString(comNotaCega++);
                }

                //Sem espelho
                if (Convert.ToString(gridNota.Rows[i].Cells[6].Value).Equals(""))
                {
                    lblTotalSemNotaCega.Text = Convert.ToString(semNotaCega++);
                }

                //Conferido
                if (!Convert.ToString(gridNota.Rows[i].Cells[10].Value).Equals(""))
                {
                    lblTotalConferido.Text = Convert.ToString(conferido++);
                }

                //Não Conferido
                if (Convert.ToString(gridNota.Rows[i].Cells[10].Value).Equals(""))
                {
                    lblTotalNaoConferido.Text = Convert.ToString(naoConferido++);
                }

                i++;
            }

        }

        //Informação da nota
        private void PesqInformaçãoNota()
        {
            //Instância as linha da tabela
            DataGridViewRow linha = gridNota.CurrentRow;
            //Recebe o indice   
            int indice = linha.Index;

            lblNotaFiscal.Text = Convert.ToString(gridNota.Rows[indice].Cells[1].Value);
            lblTotalItensNota.Text = Convert.ToString(gridItensNota.Rows.Count);

            int volume = 0;

            for (int i = 0; gridItensNota.Rows.Count > i; i++)
            {
                volume = volume + Convert.ToInt32(gridItensNota.Rows[i].Cells[3].Value);
            }

            lblTotalVolumesNota.Text = Convert.ToString(volume);

            volume = 0;

            if (!Convert.ToString(gridNota.Rows[indice].Cells[6].Value).Equals(""))
            {

                lblNotaCega.Text = Convert.ToString(gridNota.Rows[indice].Cells[6].Value);
                lblTotalItensNotaCega.Text = Convert.ToString(gridItensNotaCega.Rows.Count);

                for (int i = 0; gridItensNotaCega.Rows.Count > i; i++)
                {
                    volume = volume + Convert.ToInt32(gridItensNotaCega.Rows[i].Cells[2].Value);
                }

                lblTotalVolumesNotaCega.Text = Convert.ToString(volume);
            }
        }

        //Gera a Nota Cega
        private void GerarNotaCega()
        {
            try
            {
                if (gridNota.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecione uma nota de entrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (gridNota.SelectedRows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridNota.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Controla a geração de nota cega com o mesmo fornecedor
                    string nomefornecedor = gridNota.Rows[indice].Cells[3].Value.ToString();
                    bool gerarNota = true;

                    //Caso for mais de um registro que o usuário vai poder selecionar
                    foreach (DataGridViewRow verificar in gridNota.SelectedRows)
                    {
                        if (!Convert.ToString(verificar.Cells[6].Value).Equals(""))
                        {
                            MessageBox.Show("Nota já vinculada ao espelho: " + verificar.Cells[6].Value, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else if (gridNota.SelectedRows.Count > 1 && !nomefornecedor.Equals(verificar.Cells[3].Value))
                        {
                            gerarNota = false;
                            MessageBox.Show("Notas selecionadas não pertecem ao mesmo fornecedor!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    if (gerarNota == true)
                    {
                        //Instância a camada de negocios
                        NotaEntradaNegocios notaEntradaNegocios = new NotaEntradaNegocios();
                        //Instância a camada de objeto
                        NotaEntrada notaCega = new NotaEntrada();
                        NotaEntradaCollection notaCegaCollection = new NotaEntradaCollection();
                        //Pesquisa o id do espelho a ser gerado
                        int codNotaCega = notaEntradaNegocios.PesqIdNotaCega();

                        //Percorre o grid excluindo as notas geradas e acrescentando ao de notas
                        foreach (DataGridViewRow row in gridNota.SelectedRows)
                        {
                            int indiceSelecionado = row.Index;

                            //Passa as informações da nota
                            notaCega.dataNota = Convert.ToDateTime(row.Cells[0].Value);
                            notaCega.nota = Convert.ToInt32(row.Cells[1].Value);
                            notaCega.codFornecedor = Convert.ToInt32(row.Cells[2].Value);
                            notaCega.codNotaCega = Convert.ToInt32(codNotaCega);
                            notaCega.liberarEstoque = chkLiberarEstoqueFim.Checked;
                            notaCega.liberarArmazenagem = chkArmazenarFim.Checked;
                            notaCega.armzenagemPicking = chkArmazenarPicking.Checked;
                            notaCega.exigirValidade = chkExigirValidade.Checked;
                            notaCega.tipoRelatorio = chkTipoRelatorio.Checked;
                            notaCega.crossDocking = chkCrossDocking.Checked;
                            notaCega.codUsuarioNotaCega = codUsuario;

                            notaEntradaNegocios.GerarNotaCega(notaCega, cmbEmpresa.Text);

                            gridNota.Rows[indiceSelecionado].Cells[6].Value = codNotaCega;

                        }

                        //Atualiza as informações
                        PesqInformacao();
                        MessageBox.Show("Nota cega de N° " + codNotaCega + " gerada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Adiciona nota a um nota cega existente
        private void AdicionarNota()
        {
            try
            {
                if (gridNota.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Por favor selecione apenas uma nota!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridNota.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    string notaCega = ShowDialog("Digite a Nota Cega", "Inclusão de Nota");

                    //verifica se a nota cega existe
                    bool notaCegaEncontrada = false;
                    int linhaNotaCegaEncontrada = 0;

                    for (int i = 0; gridNota.Rows.Count > i; i++)
                    {
                        if (Convert.ToString(gridNota.Rows[i].Cells[6].Value).Equals(notaCega))
                        {
                            notaCegaEncontrada = true;
                            linhaNotaCegaEncontrada = i;

                            break;
                        }
                    }

                    if (notaCegaEncontrada == false)
                    {
                        MessageBox.Show("Nenhuma nota cega encontrada! Por favor, verifique se a mesma encontra-se listada no grid! ", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (!Convert.ToString(gridNota.Rows[linhaNotaCegaEncontrada].Cells[3].Value).Equals(gridNota.Rows[indice].Cells[3].Value))
                    {
                        MessageBox.Show("A nota selecionada não pertecem ao mesmo fornecedor da nota cega digitada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (!Convert.ToString(gridNota.Rows[linhaNotaCegaEncontrada].Cells[10].Value).Equals(""))
                    {
                        MessageBox.Show("A nota cega já se encontra finalizada, essa operação não pode ser realizada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        //Instância a camada de negocios
                        NotaEntradaNegocios notaEntradaNegocios = new NotaEntradaNegocios();
                        //Executa 
                        notaEntradaNegocios.AdicionarNotaNotaCega(Convert.ToInt32(gridNota.Rows[indice].Cells[1].Value), Convert.ToInt32(notaCega), codUsuario, cmbEmpresa.Text);
                        //Exibe o númro da nota cega na nota no grid
                        gridNota.Rows[indice].Cells[6].Value = notaCega;
                        //Exibe o login do usuário
                        gridNota.Rows[indice].Cells[7].Value = loginUsuario;
                        //Limpa os itns da nota cega
                        itensNotaCegaCollection.Clear();
                        //Pesquisa os itens da nota cega novamente
                        PesqItensNotaCega();
                        //Atualiza as informações
                        PesqInformacao();
                        MessageBox.Show("Nota inserida na nota cega com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Remove a nota da nota cega
        private void RemoverNotaNotaCega()
        {
            try
            {
                if (gridNota.Focus() == true)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridNota.CurrentRow;
                    //Index selecionado
                    int indice = linha.Index;

                    //Instância o objeto
                    NotaEntradaNegocios gerarNotaCegaNegocios = new NotaEntradaNegocios();

                    if (Convert.ToString(gridNota.Rows[indice].Cells[6].Value) != "")
                    {
                        DialogResult resultado = MessageBox.Show("Nota(s) vínculada a nota cega: " + gridNota.Rows[indice].Cells[6].Value + ", Deseja realmente removê-la(s)?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            foreach (DataGridViewRow row in gridNota.SelectedRows)
                            {
                                //Recebe o indice
                                indice = row.Index;

                                if (Convert.ToString(gridNota.Rows[indice].Cells[10].Value) != "")
                                {
                                    MessageBox.Show("Essa remoção não pode ser realizada, nota cega já conferida!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                else
                                {
                                    //Remove nota
                                    gerarNotaCegaNegocios.RemoverNota(Convert.ToInt32(gridNota.Rows[indice].Cells[1].Value), cmbEmpresa.Text);
                                    //Remove o valor da nota cega inserida no grid nota
                                    gridNota.Rows[indice].Cells[6].Value = "";
                                    //Remove o login do usuário
                                    gridNota.Rows[indice].Cells[7].Value = "";
                                    //Limpa o grid da nota cega
                                    gridNotaCega.Rows.Clear();
                                    //Limpa o grid da nota cega
                                    gridItensNotaCega.Rows.Clear();
                                    //limpa a coleção de itens
                                    itensNotaCegaCollection.Clear();
                                    //Pesquisa os itens da nota cega novamente
                                    PesqItensNotaCega();
                                    //Atualiza as informações
                                    PesqInformacao();
                                }
                            }

                            MessageBox.Show("Remoção efetuada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao remover as notas selecionadas \nDetalhes " + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Atualiza os controles de entrada da nota cega
        private void AtualizarControlesEntrada()
        {
            try
            {

                //Instância a camada de negocios
                NotaEntradaNegocios notaEntradaNegocios = new NotaEntradaNegocios();
                //Passa os valores para camada de negocios
                notaEntradaNegocios.AtualizarControle(Convert.ToInt32(gridNotaCega.Rows[0].Cells[1].Value), codUsuario, chkEstoqueErpUpdate.Checked, chkArmazenagemFimUpdate.Checked,
                    chkSkuPickingUpdate.Checked, chkExigirValidadeUpdate.Checked, chkTipoRelatorioUpdate.Checked, chkCrossDockingUpdate.Checked, cmbEmpresa.Text);

                //Atualiza o gridNota
                for(int i = 0; gridNota.Rows.Count > i; i++)
                {
                    if(Convert.ToInt32(gridNota.Rows[i].Cells[6].Value) == Convert.ToInt32(gridNotaCega.Rows[0].Cells[1].Value))
                    {
                        gridNota.Rows[i].Cells[12].Value = chkEstoqueErpUpdate.Checked;
                        gridNota.Rows[i].Cells[13].Value = chkArmazenagemFimUpdate.Checked;
                        gridNota.Rows[i].Cells[14].Value = chkSkuPickingUpdate.Checked;
                        gridNota.Rows[i].Cells[15].Value = chkExigirValidadeUpdate.Checked;
                        gridNota.Rows[i].Cells[16].Value = chkTipoRelatorioUpdate.Checked;
                        gridNota.Rows[i].Cells[17].Value = chkCrossDockingUpdate.Checked;
                    }
                }

                //Desabilita os campos controles de entrada
                DesabilitarCampos();
                MessageBox.Show("Controles de entrada atualizados com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Limpar os controles
        private void LimparCampos()
        {
            chkEstoqueErpUpdate.Checked = false;
            chkArmazenagemFimUpdate.Checked = false;
            chkSkuPickingUpdate.Checked = false;
            chkExigirValidadeUpdate.Checked = false;
            chkTipoRelatorioUpdate.Checked = false;
            chkCrossDockingUpdate.Checked = false;

            lblNotaCega.Text = "-";
            lblTotalItensNotaCega.Text = "-";
            lblTotalVolumesNotaCega.Text = "-";
        }

        //Habilita os campos de controle
        private void HabilitarCampos()
        {
            if (gridNotaCega.Rows.Count > 0)
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridNotaCega.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                if (Convert.ToString(gridNotaCega.Rows[indice].Cells[4].Value) != "")
                {
                    MessageBox.Show("Essa operação não pode ser realizada, nota cega já conferida!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    chkEstoqueErpUpdate.Enabled = true;
                    chkArmazenagemFimUpdate.Enabled = true;
                    chkSkuPickingUpdate.Enabled = true;
                    chkExigirValidadeUpdate.Enabled = true;
                    chkTipoRelatorioUpdate.Enabled = true;
                    chkCrossDockingUpdate.Enabled = true;

                    btnAtualizarControles.Enabled = true;

                    //Seleiona a itens da nota cega
                    tabNotaCega.SelectedTab = pagItensNotaCega;
                }
            }

        }

        //Desabilita os campos de controle
        private void DesabilitarCampos()
        {
            chkEstoqueErpUpdate.Enabled = false;
            chkArmazenagemFimUpdate.Enabled = false;
            chkSkuPickingUpdate.Enabled = false;
            chkExigirValidadeUpdate.Enabled = false;
            chkTipoRelatorioUpdate.Enabled = false;
            chkCrossDockingUpdate.Enabled = false;

            btnAtualizarControles.Enabled = false;
        }

        //Cria um form para receber o número da nota cega
        public static string ShowDialog(string text, string caption)
        {
            Form Prompt = new Form()
            {
                Width = 150,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 10, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 12, Top = 40, Width = 93 };
            Button confirmation = new Button() { Text = "Ok", Left = 35, Width = 60, Top = 75, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { Prompt.Close(); };
            Prompt.Controls.Add(textBox);
            Prompt.Controls.Add(confirmation);
            Prompt.Controls.Add(textLabel);
            Prompt.AcceptButton = confirmation;

            return Prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
