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
    public partial class FrmAuditoria : Form
    {
        public int codUsuario;// Código do usuário
        private int idProduto;

        private int[] regiao; //Array com id da regiao
        private int[] auditor; //Array com id do auditor

        //Coleção de itens global - Controla somente a pesquisa de itens
        ItensAuditoriaCollection itensCollection = new ItensAuditoriaCollection();

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        public FrmAuditoria()
        {
            InitializeComponent();
        }

        private void FrmAuditoria_Load(object sender, EventArgs e)
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
        private void txtPesqCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo tipo 
                cmbPesqTipo.Focus();
            }
        }

        private void cmbPesqTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo status 
                cmbPesqStatus.Focus();
            }
        }

        private void cmbPesqStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo data inicial 
                dtmInicial.Focus();
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
                //Foca no botão pesquisar 
                btnPesquisar.Focus();
            }
        }

        private void cmbTipoAuditoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (cmbTipoAuditoria.Text.Equals("Endereço"))
                {
                    //Foca no campo região 
                    cmbRegiao.Focus();
                }
                else if (cmbTipoAuditoria.Text.Equals("Produto"))
                {
                    //Foca no campo produto 
                    txtCodProduto.Focus();
                }
            }
        }

        private void cmbRegiao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo rua inicial 
                cmbRuaInicial.Focus();
            }
        }

        private void cmbRuaInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo rua final 
                cmbRuaFinal.Focus();
            }
        }

        private void cmbRuaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
               
            }
        }

        private void cmbTipoArmazenamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo lado 
                cmbLado.Focus();
            }
        }
        private void cmbAuditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão gerar 
                btnGerar.Focus();
            }
        }

        private void txtCodProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Pesquisa o produto
                PesqProduto();
            }
        }

        //SelectedChanged
        private void cmbTipoAuditoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoAuditoria.Text.Equals("ENDERECO") && cmbTipoAuditoria.Enabled == true)
            {
                txtCodProduto.Enabled = false; //Desabilita o campo do produto
                gridProduto.Enabled = false; //Desabilita o grid do produto
                btnProduto.Enabled = false; //Desabilita o botão produto

                cmbRegiao.Enabled = true; //Habilita o campo região
                cmbRuaInicial.Enabled = true; //Habilita o campo rua inicial
                cmbRuaFinal.Enabled = true; //Habilita o campo rua final
                cmbLado.Enabled = true; //Habilita o campo lado

            }
            else if (cmbTipoAuditoria.Text.Equals("PRODUTO") && cmbTipoAuditoria.Enabled == true)
            {
                txtCodProduto.Enabled = true; //Habilita o campo do produto
                gridProduto.Enabled = true; //Habilita

                btnProduto.Enabled = true; //Habita o botão produto

                cmbRegiao.Enabled = false; //Desabilita o campo região
                cmbRuaInicial.Enabled = false; //Desabilita o campo rua inicial
                cmbRuaFinal.Enabled = false; //Desabilita o campo rua final
                cmbLado.Enabled = false; //Desabilita o campo lado


                txtCodProduto.Focus();//Foca no campo código do produto
            }
            else if (cmbTipoAuditoria.Text.Equals("SELECIONE") || cmbTipoAuditoria.Text.Equals(""))
            {
                txtCodProduto.Enabled = false; //Habilita o campo do produto
                gridProduto.Enabled = false; //Habilita
                btnProduto.Enabled = false; //Desabilita o botão produto

                cmbRegiao.Enabled = false; //Desabilita o campo região
                cmbRuaInicial.Enabled = false; //Desabilita o campo rua inicial
                cmbRuaFinal.Enabled = false; //Desabilita o campo rua final
                cmbLado.Enabled = false; //Desabilita o campo lado


            }

        }

        private void cmbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            PesqRua(); //Pesquisa a rua
        }

        //CheckedChanged
        private void chkTipoRelatorio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTipoRelatorio.Checked == true)
            {
                chkExigeContagem.Enabled = true; //Habilta o campo contagem
                chkExigeVencimento.Enabled = true; //Habilta o campo vencimento
                chkExigeLote.Enabled = true; //Habilta o campo lote   
                chkExigeBarra.Enabled = true; //Habilta o campo código de barra
            }
            else
            {
                chkExigeContagem.Enabled = false; //Desabilita o campo contagem
                chkExigeVencimento.Enabled = false; //Desabilita o campo vencimento
                chkExigeLote.Enabled = false; //Desabilita o campo lote
                chkExigeBarra.Enabled = false; //Desabilita o campo código de barra
            }

        }

        //KeyUP
        private void gridAuditoria_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        private void cmbRegiao_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbRegiao.Items.Count == 0) //Verifica se já houve pesquisa
            {
                PesqRegiao(); //Pesquisa a região caso não exista itens
            }
        }

        private void cmbAuditor_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbAuditor.Items.Count == 0) //Verifica se já houve pesquisa
            {
                PesqAuditor();
            }
        }

        //Click
        private void gridAuditoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        private void cmbAuditor_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbAuditor.Items.Count == 0)
            {
                PesqAuditor();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa as auditorias
            PesqAuditoria();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            //Pesquisa um novo código para auditoria
            PesqId();
        }

        private void btnProduto_Click(object sender, EventArgs e)
        {

            //Adiciona o produto no grid
            AdicionaProduto();

        }



        private void btnAnalisar_Click(object sender, EventArgs e)
        {

            if (cmbTipoAuditoria.Text.Equals("PRODUTO"))
            {
                //Analisa os endereço do produto
                Thread thread = new Thread(AnalisarAuditoriaProduto);
                thread.Start();
            }
            else if (cmbTipoAuditoria.Text.Equals("ENDERECO"))
            {
                if (cmbRegiao.Text.Equals("Selec...") || cmbRegiao.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, selecione região!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbRuaInicial.Text.Equals("Selec...") || cmbRuaInicial.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, selecione uma rua inicial!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbRuaFinal.Text.Equals("Selec...") || cmbRuaFinal.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, selecione uma rua final!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Analisa os endereço do produto
                    Thread thread = new Thread(AnalisarAuditoriaEndereco);
                    thread.Start();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um tipo de auditoria!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void mniCancelarAuditoria_Click(object sender, EventArgs e)
        {
            CancelarAuditoria();
        }

        //Menu
        private void mniAtualizarAuditoria_Click(object sender, EventArgs e)
        {
            //Transfere os itens do abastecimento
            Thread thread = new Thread(AtualizarAuditoria);
            thread.Start();
        }
        private void mniRecusarAuditoria_Click(object sender, EventArgs e)
        {
            //Transfere os itens do abastecimento
            Thread thread = new Thread(RecusarAuditoria);
            thread.Start();
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            //Gera a auditoria
            Thread thread = new Thread(GeraAuditoria);
            thread.Start();
        }

        //Pesquisa um novo código para auditoria
        private void PesqId()
        {
            try
            {
                //Limpa os campos
                LimparCampos();
                //Habilita componentes
                HabilitarCampos();

                //Instância a categoria negocios
                AuditoriaNegocios auditoriaNegocios = new AuditoriaNegocios();
                //Seta o novo id
                txtCodigo.Text = Convert.ToString(auditoriaNegocios.PesqId());

                //Pesquisa a região
                PesqRegiao();

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void PesqRegiao() //Pesquisa região
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

        private void PesqRua() //Pesquisa rua
        {
            try
            {
                //Limpa o combobox rua
                cmbRuaInicial.Items.Clear();
                cmbRuaFinal.Items.Clear();
                //Adiciona o texto
                cmbRuaInicial.Text = "";
                cmbRuaFinal.Text = "";

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa as ruas da região selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRua(regiao[cmbRegiao.SelectedIndex]);
                //Preenche o combobox rua
                gerarEnderecoCollection.ForEach(n => cmbRuaInicial.Items.Add(n.numeroRua));
                gerarEnderecoCollection.ForEach(n => cmbRuaFinal.Items.Add(n.numeroRua));

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        private void PesqAuditor() //Pesquisa o usuário que  tem o perfíl auditor
        {
            try
            {
                //Instância o objeto
                UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                //Instância a coleção
                UsuarioCollection usuarioCollection = new UsuarioCollection();
                //A coleção recebe o resultado da consulta
                usuarioCollection = usuarioNegocios.PesqUsuario(null);
                //Localizando os auditores e ordena por login
                List<Usuario> usuarioRepositor = usuarioCollection.FindAll(delegate (Usuario n) { return n.perfil == "AUDITOR"; }).OrderBy(c => c.login).ToList();

                //Preenche o combobox região
                usuarioRepositor.ForEach(n => cmbAuditor.Items.Add(n.login));
                //Define o tamanho do array para o combobox
                auditor = new int[usuarioRepositor.Count];

                for (int i = 0; i < usuarioRepositor.Count; i++)
                {
                    //Preenche o array combobox
                    auditor[i] = usuarioRepositor[i].codUsuario;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa as auditorias
        private void PesqAuditoria()
        {
            try
            {
                //Instância a camada de negocios
                AuditoriaNegocios auditoriaNegocios = new AuditoriaNegocios();
                //Instância a coleção de objêto
                AuditoriaCollection auditoriaCollection = new AuditoriaCollection();
                //A coleção recebe o resultado da consulta
                auditoriaCollection = auditoriaNegocios.PesqAuditoria(txtPesqCodigo.Text, cmbPesqTipo.Text, cmbPesqStatus.Text.ToUpper(), dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), cmbEmpresa.Text);
                //Limpa o grid
                gridAuditoria.Rows.Clear();
                //Grid Recebe o resultado da coleção
                auditoriaCollection.ForEach(n => gridAuditoria.Rows.Add(gridAuditoria.Rows.Count + 1, n.dataAuditoria, n.codAuditoria, n.responsavel, n.tipoAuditoria, n.qtdEndereco, (100 - (100 - n.problemas)) + "%",
                    n.regiao, n.ruaIncial, n.ruaFinal, n.tipoArmazenamento, n.ladoRua, n.tipoRelatorio, n.exigeContagem, n.exigemVencimento, n.exigeLote, n.exigeBarra, n.status));


                if (auditoriaCollection.Count > 0)
                {
                    //Qtd de auditoria
                    lblQtd.Text = Convert.ToString(gridAuditoria.Rows.Count);

                    //Seleciona a primeira linha do grid
                    gridAuditoria.CurrentCell = gridAuditoria.Rows[0].Cells[1];
                    //Foca no grid
                    gridAuditoria.Focus();

                    //Pesquisa os itens do abastecimento
                    PesqItensAuditoria();

                    //Seta os dados nos campos
                    DadosCampos();

                }
                else
                {
                    //Limpa os campos
                    LimparCampos();
                    MessageBox.Show("Nenhum auditoria encontrada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os itens da auditoria
        private void PesqItensAuditoria()
        {
            try
            {
                //Instância a camada de negocios
                AuditoriaNegocios auditoriaNegocios = new AuditoriaNegocios();
                //Limpa o coleção
                itensCollection.Clear();
                //A coleção recebe o resultado da consulta
                itensCollection = auditoriaNegocios.PesqItensAuditoria(cmbEmpresa.Text,txtPesqCodigo.Text, cmbPesqTipo.Text, cmbPesqStatus.Text.ToUpper(), dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Seta os dados nos campos
        private void DadosCampos()
        {
            try
            {
                if (gridAuditoria.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridAuditoria.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Desabilita todos os campos
                    DesabilitarCampos();

                    //Seta o valor do código
                    txtCodigo.Text = Convert.ToString(gridAuditoria.Rows[indice].Cells[2].Value);
                    //Seta o tipo
                    cmbTipoAuditoria.Text = Convert.ToString(gridAuditoria.Rows[indice].Cells[4].Value);
                    //Seta o região
                    cmbRegiao.Text = Convert.ToString(gridAuditoria.Rows[indice].Cells[7].Value);
                    //Seta o rua
                    cmbRuaInicial.Text = Convert.ToString(gridAuditoria.Rows[indice].Cells[8].Value);
                    //Seta o rua final
                    cmbRuaFinal.Text = Convert.ToString(gridAuditoria.Rows[indice].Cells[9].Value);
                    //Seta o lado
                    cmbLado.Text = Convert.ToString(gridAuditoria.Rows[indice].Cells[11].Value);
                    //Seta o relatório
                    chkTipoRelatorio.Checked = Convert.ToBoolean(gridAuditoria.Rows[indice].Cells[12].Value);
                    //Seta a contagem
                    chkExigeContagem.Checked = Convert.ToBoolean(gridAuditoria.Rows[indice].Cells[13].Value);
                    //Seta o vencimento
                    chkExigeVencimento.Checked = Convert.ToBoolean(gridAuditoria.Rows[indice].Cells[14].Value);
                    //Seta o lote
                    chkExigeLote.Checked = Convert.ToBoolean(gridAuditoria.Rows[indice].Cells[15].Value);
                    //Seta o código de barra
                    chkExigeBarra.Checked = Convert.ToBoolean(gridAuditoria.Rows[indice].Cells[16].Value);

                    if (cmbTipoAuditoria.Text.Equals("PRODUTO"))
                    {
                        //Seta o região
                        cmbRegiao.Text = "Selec...";
                        //Seta o rua
                        cmbRuaInicial.Text = "Selec...";
                        //Seta o rua final
                        cmbRuaFinal.Text = "Selec...";
                        //Seta o lado
                        cmbLado.Text = "TODOS";
                    }

                    //Exibe os itens
                    if (itensCollection.Count > 0)
                    {
                        //Limpa os grids
                        gridItensPulmao.Rows.Clear();
                        gridItensPicking.Rows.Clear();
                        gridItensFlow.Rows.Clear();
                        gridProduto.Rows.Clear();

                        //Localizando os itens da auditoria
                        List<ItensAuditoria> itensAuditoriaCollection = itensCollection.FindAll(delegate (ItensAuditoria n) { return n.codAuditoria == Convert.ToInt32(gridAuditoria.Rows[indice].Cells[2].Value); });

                        //Exibe os dados nos grid itens e pulmão
                        for (int i = 0; itensAuditoriaCollection.Count > i; i++)
                        {
                            //Adiciona os itens análisados no grid
                            if (cmbTipoAuditoria.Text.Equals("PRODUTO"))
                            {
                                //Adiciona o primeiro item
                                if (gridProduto.Rows.Count == 0)
                                {
                                    //Adiciona o produto no grid
                                    gridProduto.Rows.Add(gridProduto.Rows.Count + 1, itensAuditoriaCollection[i].idProduto, itensAuditoriaCollection[i].codProduto + " - " + itensAuditoriaCollection[i].descProduto);
                                }
                                else
                                {
                                    //Verifica se o produto já foi digitado
                                    for (int ii = 0; gridProduto.Rows.Count > ii; ii++)
                                    {
                                        if (gridProduto.Rows[ii].Cells[1].Value.Equals(itensAuditoriaCollection[i].idProduto))
                                        {
                                            break;
                                        }
                                        else if (gridProduto.Rows.Count - 1 == ii)
                                        {
                                            //Adiciona o produto no grid
                                            gridProduto.Rows.Add(gridProduto.Rows.Count + 1, itensAuditoriaCollection[i].idProduto, itensAuditoriaCollection[i].codProduto + " - " + itensAuditoriaCollection[i].descProduto);
                                            break;
                                        }

                                    }
                                }

                            }

                            if (itensAuditoriaCollection[i].tipoEndereco.Equals("PULMAO"))
                            {
                                //grid Recebe o resultado da coleção
                                gridItensPulmao.Rows.Add(gridItensPulmao.Rows.Count + 1,
                                        itensAuditoriaCollection[i].dataAuditoria,
                                        itensAuditoriaCollection[i].codItemAuditoria,
                                        itensAuditoriaCollection[i].codApartamento,
                                        itensAuditoriaCollection[i].endereco,
                                        itensAuditoriaCollection[i].idProduto,
                                        itensAuditoriaCollection[i].codProduto + " - " + itensAuditoriaCollection[i].descProduto,
                                        itensAuditoriaCollection[i].fatorPulmao,
                                        itensAuditoriaCollection[i].estoque + " " + itensAuditoriaCollection[i].unidadeEstoque,
                                        itensAuditoriaCollection[i].estoqueAuditado,
                                        itensAuditoriaCollection[i].estoqueFalta,
                                        itensAuditoriaCollection[i].estoqueSobra,
                                        itensAuditoriaCollection[i].estoqueProblema,
                                         string.Format("{0:d}", itensAuditoriaCollection[i].vencimento),
                                        itensAuditoriaCollection[i].lote,
                                        itensAuditoriaCollection[i].auditor,
                                        itensAuditoriaCollection[i].statusAuditoria);
                            }
                            else if (itensAuditoriaCollection[i].tipoEndereco.Equals("PICKING") && itensAuditoriaCollection[i].tipoSeparacao.Equals("CAIXA"))
                            {
                                //grid Recebe o resultado da coleção de produto para o abastecimento
                                gridItensPicking.Rows.Add(gridItensPicking.Rows.Count + 1,
                                        itensAuditoriaCollection[i].dataAuditoria,
                                        itensAuditoriaCollection[i].codItemAuditoria,
                                        itensAuditoriaCollection[i].codApartamento,
                                        itensAuditoriaCollection[i].endereco,
                                        itensAuditoriaCollection[i].idProduto,
                                        itensAuditoriaCollection[i].codProduto + " - " + itensAuditoriaCollection[i].descProduto,
                                        itensAuditoriaCollection[i].fatorPulmao,
                                        itensAuditoriaCollection[i].estoque + " " + itensAuditoriaCollection[i].unidadeEstoque,
                                        itensAuditoriaCollection[i].estoqueAuditado,
                                        itensAuditoriaCollection[i].estoqueFalta,
                                        itensAuditoriaCollection[i].estoqueSobra,
                                        itensAuditoriaCollection[i].estoqueProblema,
                                         string.Format("{0:d}", itensAuditoriaCollection[i].vencimento),
                                        itensAuditoriaCollection[i].lote,
                                        itensAuditoriaCollection[i].auditor,
                                        itensAuditoriaCollection[i].statusAuditoria);
                            }
                            else if (itensAuditoriaCollection[i].tipoEndereco.Equals("PICKING") && itensAuditoriaCollection[i].tipoSeparacao.Equals("FLOWRACK"))
                            {
                                //grid Recebe o resultado da coleção de produto para o abastecimento
                                gridItensFlow.Rows.Add(gridItensFlow.Rows.Count + 1,
                                        itensAuditoriaCollection[i].dataAuditoria,
                                        itensAuditoriaCollection[i].codItemAuditoria,
                                        itensAuditoriaCollection[i].codApartamento,
                                        itensAuditoriaCollection[i].endereco,
                                        itensAuditoriaCollection[i].idProduto,
                                        itensAuditoriaCollection[i].codProduto + " - " + itensAuditoriaCollection[i].descProduto,
                                        itensAuditoriaCollection[i].fatorPulmao,
                                        itensAuditoriaCollection[i].estoque + " " + itensAuditoriaCollection[i].unidadeEstoque,
                                        itensAuditoriaCollection[i].estoqueAuditado,
                                        itensAuditoriaCollection[i].estoqueFalta,
                                        itensAuditoriaCollection[i].estoqueSobra,
                                        itensAuditoriaCollection[i].estoqueProblema,
                                        string.Format("{0:d}", itensAuditoriaCollection[i].vencimento),
                                        itensAuditoriaCollection[i].lote,
                                        itensAuditoriaCollection[i].auditor,
                                        itensAuditoriaCollection[i].statusAuditoria);
                            }

                        }

                        //Pesquisa o resultado da auditoria
                        AnalisarResultado(Convert.ToInt32(txtCodigo.Text));

                        //Exibe a quantidade
                        lblPulmao.Text = gridItensPulmao.Rows.Count.ToString();
                        lblPicking.Text = gridItensPicking.Rows.Count.ToString();
                        lblTotalFlow.Text = gridItensFlow.Rows.Count.ToString();
                        lblItensAuditoria.Text = (gridItensPulmao.Rows.Count + gridItensPicking.Rows.Count + gridItensFlow.Rows.Count).ToString();
                    }
                    else
                    {
                        //MessageBox.Show("Nenhum item encontrado para a auditoria selecionada!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        //Pesquisa o produto
        private void PesqProduto()
        {
            try
            {
                if (!txtCodProduto.Text.Equals(""))
                {
                    //Instância o negocios
                    ProdutoNegocios produtoNegocios = new ProdutoNegocios();
                    //Instância a coleção
                    ProdutoCollection produtoCollection = new ProdutoCollection();
                    //A coleção recebe o resultado da consulta
                    produtoCollection = produtoNegocios.PesqProduto(cmbEmpresa.Text,"", txtCodProduto.Text);

                    if (produtoCollection.Count > 0)
                    {
                        //Seta o id do Produto
                        idProduto = produtoCollection[0].idProduto;
                        //Seta a descrição do produto
                        lblDescProduto.Text = produtoCollection[0].codProduto + "-" + produtoCollection[0].descProduto;

                        //Foca no botão do produto
                        btnProduto.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum produto encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //Foca no botão do produto
                    btnProduto.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Adiciona o produto no grid
        private void AdicionaProduto()
        {
            if (gridProduto.Rows.Count == 0)
            {
                //Adiciona o produto no grid
                gridProduto.Rows.Add(gridProduto.Rows.Count + 1, idProduto, lblDescProduto.Text);

                //Limpa o campo
                idProduto = 0;
                txtCodProduto.Clear();
                lblDescProduto.Text = "-";
                txtCodProduto.Focus();
            }
            else
            {
                //Verifica se o produto já foi digitado
                for (int i = 0; gridProduto.Rows.Count > i; i++)
                {
                    if (gridProduto.Rows[i].Cells[1].Value.Equals(lblDescProduto.Text))
                    {
                        txtCodProduto.Focus();
                        txtCodProduto.SelectAll();
                        MessageBox.Show("Produto já digitado!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    else if (gridProduto.Rows.Count - 1 == i)
                    {
                        //Adiciona o manifesto no grid
                        gridProduto.Rows.Add(gridProduto.Rows.Count + 1, idProduto, lblDescProduto.Text);

                        //Limpa o campo
                        idProduto = 0;
                        lblDescProduto.Text = "-";
                        txtCodProduto.Clear();
                        txtCodProduto.Focus();
                        break;
                    }

                }
            }
        }

        //Analisa auditoria por endereço
        private void AnalisarAuditoriaEndereco()
        {
            try
            {
                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    //Limpa o grid de itens
                    gridItensPulmao.Rows.Clear();
                    gridItensPicking.Rows.Clear();
                    gridItensFlow.Rows.Clear();
                    //Seleciona a primeira página
                    tabControl1.SelectedTab = tabPage1;
                });

                //Instância a camada de negócios
                AuditoriaNegocios auditoriaNegocios = new AuditoriaNegocios();
                //Instância a camada de objêto - coleção 
                ItensAuditoriaCollection itensPulmaoCollection = new ItensAuditoriaCollection();
                //Instância a camada de objêto - coleção 
                ItensAuditoriaCollection itensPickingCollection = new ItensAuditoriaCollection();
                //Instância a camada de objêto - coleção 
                ItensAuditoriaCollection itensPickingFlowCollection = new ItensAuditoriaCollection();

                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    //Pesquisa o id dos Produtos no pulmão
                    itensPulmaoCollection = auditoriaNegocios.PesqItensPulmao(cmbEmpresa.Text, cmbTipoAuditoria.Text, cmbRegiao.Text, cmbRuaInicial.Text, cmbRuaFinal.Text, null, cmbLado.Text, null); //A coleção recebe o resultado da consulta 

                    //Pesquisa o id dos Produtos no picking
                    itensPickingCollection = auditoriaNegocios.PesqItensPicking(cmbEmpresa.Text, cmbTipoAuditoria.Text, cmbRegiao.Text, cmbRuaInicial.Text, cmbRuaFinal.Text, null, cmbLado.Text, null); //A coleção recebe o resultado da consulta 


                    //Pesquisa o id dos Produtos no picking
                    itensPickingFlowCollection = auditoriaNegocios.PesqItensPickingFlow(cmbEmpresa.Text, cmbTipoAuditoria.Text, cmbRegiao.Text, cmbRuaInicial.Text, cmbRuaFinal.Text, null, cmbLado.Text, null); //A coleção recebe o resultado da consulta 

                });


                //Se não existiritem para o abastecimento
                if (itensPulmaoCollection.Count == 0 && itensPickingCollection.Count == 0)
                {
                    MessageBox.Show("Nenhum produto encontrado para análise! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    //Garante que seja executado pela thread
                    Invoke((MethodInvoker)delegate ()
                    {
                        //grid Recebe o resultado da coleção
                        itensPulmaoCollection.ForEach(n => gridItensPulmao.Rows.Add(gridItensPulmao.Rows.Count + 1, "", n.codApartamento, n.endereco, n.idProduto, n.codProduto + " - " + n.descProduto,
                    n.fatorPulmao, n.estoque + " " + n.unidadeEstoque, "", "", "", "", string.Format("{0:d}", n.vencimento), n.lote, ""));

                        foreach (ItensAuditoria n in itensPickingCollection)
                        {
                            if (n.tipoEndereco.Equals("CAIXA"))
                            {
                                gridItensPicking.Rows.Add(gridItensPicking.Rows.Count + 1, "", n.codApartamento, n.endereco, n.idProduto, n.codProduto + " - " + n.descProduto,
                                n.fatorPulmao, n.estoque + " " + n.unidadeEstoque, "", "", "", string.Format("{0:d}", n.vencimento), n.lote, "");
                            }
                            else if (n.tipoEndereco.Equals("FLOWRACK"))
                            {
                                gridItensFlow.Rows.Add(gridItensFlow.Rows.Count + 1, "", n.codApartamento, n.endereco, n.idProduto, n.codProduto + " - " + n.descProduto,
                                n.fatorPulmao, n.estoque + " " + n.unidadeEstoque, "", "", "", string.Format("{0:d}", n.vencimento), n.lote, "");
                            }
                        }

                        foreach (ItensAuditoria n in itensPickingFlowCollection)
                        {
                            if (n.tipoEndereco.Equals("FLOWRACK"))
                            {
                                gridItensFlow.Rows.Add(gridItensFlow.Rows.Count + 1, "", n.codApartamento, n.endereco, n.idProduto, n.codProduto + " - " + n.descProduto,
                                n.fatorPulmao, n.estoque + " " + n.unidadeEstoque, "", "", "", string.Format("{0:d}", n.vencimento), n.lote, "");
                            }
                        }

                        //Exibe a quantidade
                        lblPulmao.Text = gridItensPulmao.Rows.Count.ToString();
                        lblPicking.Text = gridItensPicking.Rows.Count.ToString();
                        lblTotalFlow.Text = gridItensFlow.Rows.Count.ToString();
                        lblItensAuditoria.Text = (gridItensPulmao.Rows.Count + gridItensPicking.Rows.Count + gridItensFlow.Rows.Count).ToString();

                        //Foca na página de análise de picking
                        tabControl1.SelectedTab = tabPage3;

                    });

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //Analisa auditoria por produto
        private void AnalisarAuditoriaProduto()
        {
            try
            {
                //Array responsável pelos id dos produtos
                int[] idProdutoGrid = new int[gridProduto.Rows.Count]; //Define o tamanho do array

                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    //Verifica as rotas selecionadas
                    if (gridProduto.Rows.Count > 0)
                    {
                        //Preenche o array
                        for (int i = 0; gridProduto.Rows.Count > i; i++)
                        {
                            idProdutoGrid[i] = Convert.ToInt32(gridProduto.Rows[i].Cells[1].Value); //Passa o o id do produto
                        }
                    }
                });

                //Verifica se existe id do produto no grid
                if (idProdutoGrid.Length == 0)
                {
                    MessageBox.Show("Por favor, digite pelo menos um produto! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    //Garante que seja executado pela thread
                    Invoke((MethodInvoker)delegate ()
                    {
                        //Limpa o grid de itens
                        gridItensPulmao.Rows.Clear();
                        gridItensPicking.Rows.Clear();
                        gridItensFlow.Rows.Clear();
                        //Seleciona a primeira página
                        tabControl1.SelectedTab = tabPage1;
                    });

                    //Instância a camada de negócios
                    AuditoriaNegocios auditoriaNegocios = new AuditoriaNegocios();
                    //Instância a camada de objêto - coleção 
                    ItensAuditoriaCollection itensPulmaoCollection = new ItensAuditoriaCollection();
                    //Instância a camada de objêto - coleção 
                    ItensAuditoriaCollection itensPickingCollection = new ItensAuditoriaCollection();

                    //Garante que seja executado pela thread
                    Invoke((MethodInvoker)delegate ()
                    {
                        //Pesquisa o id dos Produtos no pulmão
                        itensPulmaoCollection = auditoriaNegocios.PesqItensPulmao(cmbEmpresa.Text, cmbTipoAuditoria.Text, cmbRegiao.Text, cmbRuaInicial.Text, cmbRuaFinal.Text, null, cmbLado.Text, idProdutoGrid); //A coleção recebe o resultado da consulta 

                        //Pesquisa o id dos Produtos no picking
                        itensPickingCollection = auditoriaNegocios.PesqItensPicking(cmbEmpresa.Text, cmbTipoAuditoria.Text, cmbRegiao.Text, cmbRuaInicial.Text, cmbRuaFinal.Text, null, cmbLado.Text, idProdutoGrid); //A coleção recebe o resultado da consulta 

                    });


                    //Garante que seja executado pela thread
                    Invoke((MethodInvoker)delegate ()
                    {
                        //grid Recebe o resultado da coleção de produto para o abastecimento
                        itensPulmaoCollection.ForEach(n => gridItensPulmao.Rows.Add(gridItensPulmao.Rows.Count + 1, "", "", n.codApartamento, n.endereco, n.idProduto, n.codProduto + " - " + n.descProduto,
                        n.fatorPulmao, n.estoque + " " + n.unidadeEstoque, "", "", "", "", string.Format("{0:d}", n.vencimento), n.lote, ""));

                        foreach (ItensAuditoria n in itensPickingCollection)
                        {
                            if (n.tipoEndereco.Equals("CAIXA"))
                            {
                                gridItensPicking.Rows.Add(gridItensPicking.Rows.Count + 1, "", "", n.codApartamento, n.endereco, n.idProduto, n.codProduto + " - " + n.descProduto,
                                n.fatorPulmao, n.estoque + " " + n.unidadeEstoque, "", "", "", "", string.Format("{0:d}", n.vencimento), n.lote, "");
                            }
                            else if (n.tipoEndereco.Equals("FLOWRACK"))
                            {
                                gridItensFlow.Rows.Add(gridItensFlow.Rows.Count + 1, "", "", n.codApartamento, n.endereco, n.idProduto, n.codProduto + " - " + n.descProduto,
                                n.fatorPulmao, n.estoque + " " + n.unidadeEstoque, "", "", "", "", string.Format("{0:d}", n.vencimento), n.lote, "");
                            }
                        }

                        //Exibe a quantidade
                        lblPulmao.Text = gridItensPulmao.Rows.Count.ToString();
                        lblPicking.Text = gridItensPicking.Rows.Count.ToString();
                        lblTotalFlow.Text = gridItensFlow.Rows.Count.ToString();
                        lblItensAuditoria.Text = (gridItensPulmao.Rows.Count + gridItensPicking.Rows.Count + gridItensFlow.Rows.Count).ToString();

                        //Foca na página de análise de picking
                        tabControl1.SelectedTab = tabPage2;

                        //Foca na página de análise de picking
                        tabControl1.SelectedTab = tabPage2;

                        //Exibe cores de alerta
                        //CoresGridObservacao();

                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //Gera a auditoria
        private void GeraAuditoria()
        {
            try
            {
                //Instância a camada de negócios
                AuditoriaNegocios auditoriaNegocios = new AuditoriaNegocios();

                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    if (gridItensPulmao.Rows.Count == 0 && gridItensPicking.Rows.Count == 0 && gridItensFlow.Rows.Count == 0)
                    {
                        MessageBox.Show("Não existem endereços analizados para gerar uma auditoria!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        if (cmbTipoAuditoria.Text.Equals("ENDERECO"))
                        {

                            //Pesquisa o os Produtos no pulmão por endereço
                            auditoriaNegocios.GeraAuditoria(Convert.ToInt32(txtCodigo.Text), cmbTipoAuditoria.Text, cmbRegiao.Text, cmbRuaInicial.Text, cmbRuaFinal.Text, null, cmbLado.Text,
                                chkTipoRelatorio.Checked, chkExigeContagem.Checked, chkExigeVencimento.Checked, chkExigeLote.Checked, chkExigeBarra.Checked, codUsuario, cmbEmpresa.Text);

                        }
                        else if (cmbTipoAuditoria.Text.Equals("PRODUTO"))
                        {
                            //Pesquisa o os Produtos no pulmão por endereço
                            auditoriaNegocios.GeraAuditoria(Convert.ToInt32(txtCodigo.Text), cmbTipoAuditoria.Text, null, null, null, null, null,
                                chkTipoRelatorio.Checked, chkExigeContagem.Checked, chkExigeVencimento.Checked, chkExigeLote.Checked, chkExigeBarra.Checked, codUsuario, cmbEmpresa.Text);
                        }

                        //Gera os itens
                        GerarItensAuditoria();
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //Gera os itens da auditoria
        private void GerarItensAuditoria()
        {
            try
            {
                //Instância a camada de negócios
                AuditoriaNegocios auditoriaNegocios = new AuditoriaNegocios();


                //Array responsável pelos id dos produtos
                int[] idProdutoGrid = new int[gridProduto.Rows.Count]; //Define o tamanho do array


                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    //Verifica as rotas selecionadas
                    if (gridProduto.Rows.Count > 0)
                    {
                        //Preenche o array
                        for (int i = 0; gridProduto.Rows.Count > i; i++)
                        {
                            idProdutoGrid[i] = Convert.ToInt32(gridProduto.Rows[i].Cells[1].Value); //Passa o o id do produto
                        }
                    }
                });

                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    int? codAuditor = null;

                    if (!(cmbAuditor.Text.Equals("SELECIONE") || cmbAuditor.Text.Equals("")))
                    {
                        codAuditor = auditor[cmbAuditor.SelectedIndex];
                    }

                    if (cmbTipoAuditoria.Text.Equals("ENDERECO"))
                    {
                        //Pesquisa o os Produtos no pulmão por endereço
                        auditoriaNegocios.GerarItensAuditoriaPulmao(cmbEmpresa.Text, Convert.ToInt32(txtCodigo.Text), cmbTipoAuditoria.Text, cmbRegiao.Text, cmbRuaInicial.Text, cmbRuaFinal.Text, null, cmbLado.Text, null, codAuditor, chkAuditoriaPulmao.Checked); //A coleção recebe o resultado da consulta 
                    }
                    else if (cmbTipoAuditoria.Text.Equals("PRODUTO"))
                    {
                        //Pesquisa o id dos Produtos no pulmão
                        auditoriaNegocios.GerarItensAuditoriaPulmao(cmbEmpresa.Text, Convert.ToInt32(txtCodigo.Text), cmbTipoAuditoria.Text, null, null, null, null, null, idProdutoGrid, codAuditor, chkAuditoriaPulmao.Checked); //A coleção recebe o resultado da consulta 
                    }

                    //Picking de Caixa
                    if (cmbTipoAuditoria.Text.Equals("ENDERECO"))
                    {
                        //Gerar a auditoria do picking de Caixa
                        auditoriaNegocios.GerarItensAuditoriaPicking(cmbEmpresa.Text ,Convert.ToInt32(txtCodigo.Text), cmbTipoAuditoria.Text, cmbRegiao.Text, cmbRuaInicial.Text, cmbRuaFinal.Text, null, cmbLado.Text, null, codAuditor, chkPickingCaixa.Checked); //A coleção recebe o resultado da consulta 

                    }
                    else if (cmbTipoAuditoria.Text.Equals("PRODUTO"))
                    {
                        //Pesquisa o id dos Produtos no picking
                        auditoriaNegocios.GerarItensAuditoriaPicking(cmbEmpresa.Text ,Convert.ToInt32(txtCodigo.Text), cmbTipoAuditoria.Text, null, null, null, null, null, idProdutoGrid, codAuditor, chkPickingCaixa.Checked); //A coleção recebe o resultado da consulta                  
                    }

                    //Picking de Flow Rsck
                    if (cmbTipoAuditoria.Text.Equals("ENDERECO"))
                    {
                        //Gerar a auditoria do picking de FlowRack
                        auditoriaNegocios.GerarItensAuditoriaPickingFlow(cmbEmpresa.Text ,Convert.ToInt32(txtCodigo.Text), cmbTipoAuditoria.Text, cmbRegiao.Text, cmbRuaInicial.Text, cmbRuaFinal.Text, null, cmbLado.Text, null, codAuditor, chkPickingFlow.Checked); //A coleção recebe o resultado da consulta 
                    }

                    //Pesquisa o resultado da auditoria
                    AnalisarResultado(Convert.ToInt32(txtCodigo.Text));

                });

                //Desabilita todos os campos
                DesabilitarCampos();
                MessageBox.Show("Auditoria gerada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //Cancelar auditoria
        private void CancelarAuditoria()
        {
            try
            {
                if (MessageBox.Show("Tem certeza que deseja cancelar a auditoria "+ txtCodigo.Text + "?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //Instância a categoria negocios
                    AuditoriaNegocios auditoriaNegocios = new AuditoriaNegocios();
                    //Passa o código da reserva e o código do pulmão e o id do produto
                    auditoriaNegocios.CancelarAuditoria(cmbEmpresa.Text, Convert.ToInt32(txtCodigo.Text));
                }

            MessageBox.Show("Auditoria cancelada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //Analisa auditoria por endereço
        private void AnalisarResultado(int codAuditoria)
        {
            try
            {


                //Instância a camada de negócios
                AuditoriaNegocios auditoriaNegocios = new AuditoriaNegocios();
                //Instância a camada de objêto - coleção 
                ItensAuditoriaCollection itemResultadoCollection = new ItensAuditoriaCollection();
                //Instância a camada de objêto - coleção 
                ItensAuditoriaCollection itemCaixaCollection = new ItensAuditoriaCollection();
                //Instância a camada de objêto - coleção 
                ItensAuditoriaCollection itemFlowRackCollection = new ItensAuditoriaCollection();
                //Instância a camada de objêto - coleção 
                ItensAuditoriaCollection itemPulmaoCollection = new ItensAuditoriaCollection();

                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    //Limpa o grid de itens
                    gridItensPicking.Rows.Clear();
                    gridItensFlow.Rows.Clear();
                    gridItensPulmao.Rows.Clear();
                    gridResultado.Rows.Clear();

                    //Pesquisa o picking de caixa
                    itemCaixaCollection = auditoriaNegocios.PesqPickingCaixaAuditoria(cmbEmpresa.Text, codAuditoria);

                    //grid Recebe o resultado da auditoria de caixa
                    itemCaixaCollection.ForEach(n => gridItensPicking.Rows.Add(gridItensPicking.Rows.Count + 1,
                    n.codItemAuditoria, n.codApartamento, n.endereco, n.idProduto, n.codProduto + " - " + n.descProduto, n.fatorPulmao,
                    n.qtdPulmao, n.qtdPicking, (n.qtdPicking - n.qtdPulmao), "", n.vencimento, n.lote, n.dataAuditoria, n.auditor, n.statusAuditoria));

                    //Pesquisa o picking de flowrack
                    itemFlowRackCollection = auditoriaNegocios.PesqPickingFlowRackAuditoria(cmbEmpresa.Text, codAuditoria);

                    //grid Recebe o resultado da auditoria de caixa
                    itemFlowRackCollection.ForEach(n => gridItensFlow.Rows.Add(gridItensFlow.Rows.Count + 1,
                    n.codItemAuditoria, n.codApartamento, n.endereco, n.idProduto, n.codProduto + " - " + n.descProduto, n.fatorPulmao,
                    n.qtdPulmao, n.qtdPicking, (n.qtdPicking - n.qtdPulmao), "", n.vencimento, n.lote, n.dataAuditoria, n.auditor, n.statusAuditoria));

                    //Pesquisa o picking de flowrack
                    itemPulmaoCollection = auditoriaNegocios.PesqPulmaoAuditoria(cmbEmpresa.Text, codAuditoria);

                    //grid Recebe o resultado da auditoria de caixa
                    itemPulmaoCollection.ForEach(n => gridItensPulmao.Rows.Add(gridItensPulmao.Rows.Count + 1,
                    n.codItemAuditoria, n.codApartamento, n.endereco, n.idProduto, n.codProduto + " - " + n.descProduto, n.fatorPulmao,
                    n.qtdPulmao, n.qtdPicking, (n.qtdPicking - n.qtdPulmao), "", n.vencimento, n.lote, n.dataAuditoria, n.auditor, n.statusAuditoria));

                    //Pesquisa o resultado
                    itemResultadoCollection = auditoriaNegocios.PesqResultadoAuditoria(cmbEmpresa.Text, codAuditoria);

                    //(n.qtdPulmao/ (n.qtdPicking - n.qtdPulmao)) * 100

                    //grid Recebe o resultado da auditoria
                    itemResultadoCollection.ForEach(n => gridResultado.Rows.Add(gridResultado.Rows.Count + 1, n.codAuditoria, n.idProduto, n.codProduto + " - " + n.descProduto,
                    n.qtdEntrada, n.qtdPulmao, n.qtdPicking, n.qtdVolume, n.qtdSeparacao, (n.qtdEntrada + n.qtdPulmao + n.qtdPicking + n.qtdSeparacao + n.qtdVolume), n.estoque,
                    (n.qtdEntrada + n.qtdPulmao + n.qtdPicking + n.qtdSeparacao + n.qtdVolume) - n.estoque, n.statusAuditoria));

                    //Exibe a quantidade
                    lblPulmao.Text = gridItensPulmao.Rows.Count.ToString();
                    lblPicking.Text = gridItensPicking.Rows.Count.ToString();
                    lblTotalFlow.Text = gridItensFlow.Rows.Count.ToString();
                    lblItensAuditoria.Text = (gridItensPulmao.Rows.Count + gridItensPicking.Rows.Count + gridItensFlow.Rows.Count).ToString();


                });



            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //Atualiza os endereços com o resultado da auditoria
        private void AtualizarAuditoria()
        {
            try
            {
                //Instância a categoria negocios
                AuditoriaNegocios auditoriaNegocios = new AuditoriaNegocios();


                //Passa o código da reserva e o código do pulmão
                foreach (DataGridViewRow row in gridResultado.SelectedRows)
                {
                    if (row.Cells[12].Value == null)
                    {
                        //Garante que seja executado pela thread
                        Invoke((MethodInvoker)delegate ()
                        {
                            //Percorre todo os endereços de picking de caixa
                            for (int i = 0; gridItensPicking.Rows.Count > i; i++)
                            {
                                //Verifica se o produto foi selecionado
                                if (Convert.ToInt32(row.Cells[2].Value) == Convert.ToInt32(gridItensPicking.Rows[i].Cells[4].Value))
                                {
                                    //Passa o código da reserva e o código do pulmão e o id do produto
                                    auditoriaNegocios.AtualizarPicking(cmbEmpresa.Text, Convert.ToInt32(txtCodigo.Text),
                                        Convert.ToInt32(gridItensPicking.Rows[i].Cells[2].Value), //Código do picking                           
                                    Convert.ToInt32(gridItensPicking.Rows[i].Cells[4].Value), ///Id do produto
                                    Convert.ToInt32(gridItensPicking.Rows[i].Cells[8].Value), ///Quantidade
                                    Convert.ToDateTime(gridItensPicking.Rows[i].Cells[11].Value)); //Vencimento do produto

                                    //Resgistra a operação no rastreamento
                                    auditoriaNegocios.InserirRastreamento(cmbEmpresa.Text, codUsuario,
                                    Convert.ToInt32(gridItensPicking.Rows[i].Cells[4].Value), //ID do produto
                                    Convert.ToInt32(gridItensPicking.Rows[i].Cells[2].Value), //Código do endereço
                                    Convert.ToInt32(gridItensPicking.Rows[i].Cells[8].Value), //Quantidade auditada
                                    Convert.ToDateTime(gridItensPicking.Rows[i].Cells[11].Value)); //Vencimento

                                    //Exibe o status Finalizado
                                    gridResultado.Rows[row.Index].Cells[12].Value = "FINALIZADO";
                                    gridItensPicking.Rows[i].Cells[15].Value = "FINALIZADO";

                                    break;
                                }
                            }

                            //Percorre todo os endereços de picking de flowrack
                            for (int i = 0; gridItensFlow.Rows.Count > i; i++)
                            {
                                //Verifica se o produto foi selecionado
                                if (Convert.ToInt32(row.Cells[2].Value) == Convert.ToInt32(gridItensFlow.Rows[i].Cells[4].Value))
                                {
                                    //Passa o código da reserva e o código do pulmão e o id do produto
                                    auditoriaNegocios.AtualizarPicking(cmbEmpresa.Text, Convert.ToInt32(txtCodigo.Text),
                                        Convert.ToInt32(gridItensFlow.Rows[i].Cells[2].Value), //Código do picking                           
                                    Convert.ToInt32(gridItensFlow.Rows[i].Cells[4].Value), ///Id do produto
                                    Convert.ToInt32(gridItensFlow.Rows[i].Cells[8].Value), ///Quantidade
                                    Convert.ToDateTime(gridItensFlow.Rows[i].Cells[11].Value)); //Vencimento do produto

                                    //Resgistra a operação no rastreamento
                                    auditoriaNegocios.InserirRastreamento(cmbEmpresa.Text, codUsuario,
                                    Convert.ToInt32(gridItensFlow.Rows[i].Cells[4].Value), //ID do produto
                                    Convert.ToInt32(gridItensFlow.Rows[i].Cells[2].Value), //Código do endereço
                                    Convert.ToInt32(gridItensFlow.Rows[i].Cells[8].Value), //Quantidade auditada
                                    Convert.ToDateTime(gridItensFlow.Rows[i].Cells[11].Value)); //Vencimento

                                    //Exibe o status Finalizado
                                    gridResultado.Rows[row.Index].Cells[12].Value = "FINALIZADO";
                                    gridItensFlow.Rows[i].Cells[15].Value = "FINALIZADO";

                                    break;
                                }
                            }

                            //Percorre todo os endereços de pulmão
                            for (int i = 0; gridItensPulmao.Rows.Count > i; i++)
                            {
                                //Verifica se o produto foi selecionado
                                if (Convert.ToInt32(row.Cells[2].Value) == Convert.ToInt32(gridItensPulmao.Rows[i].Cells[4].Value))
                                {
                                    //Passa o código da reserva e o código do pulmão e o id do produto
                                    auditoriaNegocios.AtualizarPulmao(cmbEmpresa.Text, Convert.ToInt32(txtCodigo.Text),
                                        Convert.ToInt32(gridItensPulmao.Rows[i].Cells[2].Value), //Código do picking                           
                                    Convert.ToInt32(gridItensPulmao.Rows[i].Cells[4].Value), ///Id do produto
                                    Convert.ToInt32(gridItensPulmao.Rows[i].Cells[8].Value), ///Quantidade
                                    Convert.ToDateTime(gridItensPulmao.Rows[i].Cells[11].Value)); //Vencimento do produto

                                    //Resgistra a operação no rastreamento
                                    auditoriaNegocios.InserirRastreamento(cmbEmpresa.Text, codUsuario,
                                    Convert.ToInt32(gridItensPulmao.Rows[i].Cells[4].Value), //ID do produto
                                    Convert.ToInt32(gridItensPulmao.Rows[i].Cells[2].Value), //Código do endereço
                                    Convert.ToInt32(gridItensPulmao.Rows[i].Cells[8].Value), //Quantidade auditada
                                    Convert.ToDateTime(gridItensPulmao.Rows[i].Cells[11].Value)); //Vencimento

                                    //Exibe o status Finalizado
                                    gridResultado.Rows[row.Index].Cells[12].Value = "FINALIZADO";
                                    gridItensPulmao.Rows[i].Cells[15].Value = "FINALIZADO";

                                    break;
                                }
                            }


                        });
                    }
                }

                MessageBox.Show("Atualização registrada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Atualiza os endereços com o resultado da auditoria
        private void RecusarAuditoria()
        {
            try
            {
                //Instância a categoria negocios
                AuditoriaNegocios auditoriaNegocios = new AuditoriaNegocios();


                //Passa o código da reserva e o código do pulmão
                foreach (DataGridViewRow row in gridResultado.SelectedRows)
                {
                    if (row.Cells[12].Value == null)
                    {
                        //Garante que seja executado pela thread
                        Invoke((MethodInvoker)delegate ()
                        {
                            //Passa o código da reserva e o código do pulmão e o id do produto
                            auditoriaNegocios.RecusarAuditoria(cmbEmpresa.Text, Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(row.Cells[2].Value));
                        });
                    }
                }

                MessageBox.Show("Recusa realizada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //Limpa os campos
        private void LimparCampos()
        {
            txtCodigo.Clear(); //Limpa o campo código de auditoria
            cmbTipoAuditoria.Text = "SELECIONE"; //Limpa o campo tipo de auditoria
            txtCodProduto.Clear(); //Limpa o campo do produto            

            cmbRegiao.Items.Clear(); //Limpa o campo região           
            cmbRuaInicial.Items.Clear(); //Limpa o campo rua inicial
            cmbRuaFinal.Items.Clear(); //Limpa o campo rua final
            cmbAuditor.Items.Clear(); //Limpa o campo auditor

            cmbRegiao.Text = "Selec..."; //Limpa o campo região           
            cmbRuaInicial.Text = "Selec..."; //Limpa o campo rua inicial
            cmbRuaFinal.Text = "Selec..."; //Limpa o campo rua final
            cmbLado.Text = "TODOS"; //Limpa o campo lado
            cmbAuditor.Text = "SELECIONE"; //Limpa o campo lado

            chkTipoRelatorio.Checked = false; //Desmarca o campo tipo relatório
            chkExigeContagem.Checked = false; //Desmarca o campo contagem
            chkExigeVencimento.Checked = false; //Desmarca o campo vencimento
            chkExigeLote.Checked = false; //Desmarca o campo lote
            chkExigeBarra.Checked = false; //Desmarca o campo código de barra         

            gridProduto.Rows.Clear(); //Limpa o grid do produto
            gridItensPulmao.Rows.Clear(); //Limpa o grid de itens
            gridItensPicking.Rows.Clear(); //Limpa o grid de itens
            gridItensFlow.Rows.Clear(); //Limpa o grid de itens

            //Exibe a quantidade
            lblPulmao.Text = "0";
            lblPicking.Text = "0";
            lblTotalFlow.Text = "0";
            lblItensAuditoria.Text = "0";

        }

        //Habiita os campos
        private void HabilitarCampos()
        {
            cmbTipoAuditoria.Enabled = true; //Habilta o campo tipo de auditoria

            chkTipoRelatorio.Enabled = true; //Habilta o campo tipo relatório             

            cmbAuditor.Enabled = true; //Habilita o campo auditor

            btnAnalisar.Enabled = true; //Habilita o botão analisar

            btnGerar.Enabled = true; //Habilita o botão gerar
        }

        //Desabilita os campos
        private void DesabilitarCampos()
        {
            cmbTipoAuditoria.Enabled = false; //Desabilita o campo tipo de auditoria
            txtCodProduto.Enabled = false; //Desabilita o campo do produto
            gridProduto.Enabled = false; //Desabilita o grid do produto

            cmbRegiao.Enabled = false; //Desabilita o campo região
            cmbRuaInicial.Enabled = false; //Desabilita o campo rua inicial
            cmbRuaFinal.Enabled = false; //Desabilita o campo rua final
            cmbLado.Enabled = false; //Desabilita o campo lado

            chkTipoRelatorio.Enabled = false; //Desabilita o campo tipo relatório
            chkExigeContagem.Enabled = false; //Desabilita o campo contagem
            chkExigeVencimento.Enabled = false; //Desabilita o campo vencimento
            chkExigeLote.Enabled = false; //Desabilita o campo lote
            chkExigeBarra.Enabled = false; //Desabilita o campo código de barra

            cmbAuditor.Enabled = false; //Desabilita o campo auditor
        }

        private void cmbLado_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


    }


}
