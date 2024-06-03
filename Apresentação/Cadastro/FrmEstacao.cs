using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;
using Utilitarios;
using Wms.Relatorio.DataSet;

namespace Wms
{
    public partial class FrmEstacao : Form
    {
        //Array com id
        private int[] regiao;
        private int[] rua;
        private int[] bloco;

        public int codUsuario;

        //Instância a coleção de blocos da estação para controle do endereço associado
        List<Estacao> blocoEstacao;

        //Instância a coleção de produtos endereçado na estação
        EnderecoCollection enderecoCollection = new EnderecoCollection();

        //Controla a opção de salvar e alterar
        private bool opcao = false;

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;

        //Código do produto
        int idProduto;

        public FrmEstacao()
        {
            InitializeComponent();
        }

        //KeyPress
        private void txtDescricao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo código
                txtCodUsuario.Focus();
            }
        }

        private void txtCodUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtCodUsuario.Text.Equals(""))
                {
                    MessageBox.Show("Digite o código do usuário da estação!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (txtLogin.Text.Equals(""))
                    {
                        //Pesquisa o usuário
                        pesqUsuario();
                    }
                    else
                    {
                        //Foca no botão Salvar
                        btnSalvar.Focus();
                    }
                }
            }
        }

        private void txtPesqProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!txtPesqProduto.Text.Equals(""))
                {
                    //Pesquisa o produto, ele estando endereçado não
                    PesqProduto();
                }
            }
        }

        private void txtCapacidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo abastecimento
                txtAbastecimento.Focus();
            }
        }

        private void txtAbastecimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo novo endereço
                txtNovoEndereco.Focus();
            }
        }

        private void txtNovoEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão endereçar produto
                btnEnderecarProduto.Focus();
            }
        }

        //TextChanged
        private void txtCodUsuario_TextChanged(object sender, EventArgs e)
        {
            if (txtCodUsuario.Text.Length == 0)
            {
                //Limpa o campo login
                txtLogin.Clear();
            }
        }

        private void rbtFixo_CheckedChanged(object sender, EventArgs e)
        {
            if (opcao == true)
            {
                txtDescricao.Text = "ESTAÇÃO FIXA ";
            }
        }

        private void rbtMovel_CheckedChanged(object sender, EventArgs e)
        {
            if (opcao == true)
            {
                txtDescricao.Text = "ESTAÇÃO MÓVEL ";
            }
        }

        private void cmbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa a rua
            PesqRua();
        }

        private void cmbRua_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa o bloco
            PesqBloco();
        }

        private void txtPesqProduto_TextChanged(object sender, EventArgs e)
        {
            if (txtPesqProduto.Text.Length == 0)
            {
                lblDescProduto.Text = "DIGITE  O CÓDIGO DO SKU ACIMA";
            }

        }

        //KeyDown
        private void txtCodUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqUsuario frame = new FrmPesqUsuario();
                //Passa a função de conferente de Flowrack para a pesquisa
                frame.perfilUsuario = "CONFERENTE";
                //Nome da empresa
                frame.empresa = cmbEmpresa.Text;

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe os valores novos
                    txtCodUsuario.Text = Convert.ToString(frame.codUsuario);
                    txtLogin.Text = frame.nmUsuario;
                }
            }
        }

        //Pesquisa o produto em uma subtela
        private void txtPesqProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqProduto frame = new FrmPesqProduto();
                //Adiciona o nome da empresa
                frame.empresa = cmbEmpresa.Text;

                //Recebe as informações
                if (frame.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (frame.separacaoFlowrack.Equals("False"))
                    {
                        if (MessageBox.Show("O produto " + frame.codProduto + " - " + frame.descProduto + " não está selecionado para o flowrack, deseja selecioná-lo?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //Atualiza a seleção separação do flowrack do produto
                            AlterarSepProdutoFlowrack(frame.codProduto);

                            //Recebe os valores novos
                            txtPesqProduto.Text = frame.codProduto;
                            lblDescProduto.Text = frame.descProduto;
                            //Foca no campo
                            txtCapacidade.Focus();
                        }
                    }
                    else
                    {
                        //Recebe os valores novos
                        txtPesqProduto.Text = Convert.ToString(frame.codProduto);
                        lblDescProduto.Text = frame.descProduto;
                        //Foca no campo
                        txtCapacidade.Focus();
                    }
                }
            }
        }

        private void gridEstacao_KeyDown(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            DadosEstacaoCampos();
        }

        private void gridEndereco_KeyDown(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            DadosProdutoCampos();
        }

        //KeyUp
        private void gridEstacao_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            DadosEstacaoCampos();
        }

        private void gridEndereco_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            DadosProdutoCampos();
        }



        //MouseClick e MouseDoubleClick
        private void gridEstacao_MouseClick(object sender, MouseEventArgs e)
        {
            //Exibe os dados nos campos
            DadosEstacaoCampos();
        }

        private void gridEndereco_MouseClick(object sender, MouseEventArgs e)
        {
            //Exibe os dados nos campos
            DadosProdutoCampos();
        }

        private void gridEstacao_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (perfilUsuario == "ADMINISTRADOR" || acesso[0].editarFuncao == true)
                {
                    if (gridEstacao.SelectedRows.Count > 0)
                        //Habilita os campos
                        Habilita();
                }
            }
        }

        //Click
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa a estação cadastrada
            PesqEstacao();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Pesquisa um novo id
                PesqId();
            }
            else
            {
                if (acesso[0].escreverFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Pesquisa um novo id
                    PesqId();
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (opcao == true)
            {
                if (perfilUsuario == "ADMINISTRADOR")
                {
                    //Salva o cadastro
                    SalvarEstacao();
                }
                else
                {
                    if (acesso[0].escreverFuncao == false)
                    {
                        MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        //Salva o cadastro
                        SalvarEstacao();
                    }
                }
            }
            else
            {
                if (perfilUsuario == "ADMINISTRADOR")
                {
                    //Alterar o cadastro
                    AlterarEstacao();

                    //Altera o volume independete do volume
                    AlterarVolumeIndependete();
                }
                else
                {
                    if (acesso[0].editarFuncao == false)
                    {
                        MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        //Alterar o cadastro
                        AlterarEstacao();
                        //Altera o volume independete do volume
                        AlterarVolumeIndependete();
                    }
                }
            }
        }

        private void btnAssociarBloco_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Associa o bloco ao endereço
                AssociaBlocoEstacao();
            }
            else
            {
                if (acesso[0].escreverFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Associa o bloco ao endereço
                    AssociaBlocoEstacao();
                }
            }
        }

        private void mniDesassociarBloco_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Desassocia o bloco da estação
                DesassociarBlocoEstacao();
            }
            else
            {
                if (acesso[0].editarFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Desassocia o bloco da estação
                    DesassociarBlocoEstacao();
                }
            }
        }

        private void btnEnderecarProduto_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Pesquisa o endereço 
                EnderecarProduto();
            }
            else
            {
                if (acesso[0].escreverFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Pesquisa o endereço 
                    EnderecarProduto();
                }
            }
        }

        private void mnuDeletar_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Delata o produto dos endereços selecionados
                DelatarProdutoEndereco();
            }
            else
            {
                if (acesso[0].excluirFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Delata o produto dos endereços selecionados
                    DelatarProdutoEndereco();
                }
            }
        }

        private void mniImpressaoEtiqueta_Click(object sender, EventArgs e)
        {
            //Imprime a etiqueta selecionada
            ImpressaoEtiqueta();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o formulário
            Close();
        }

        //Pesquisa o id da estação
        private void PesqId()
        {
            try
            {
                //Limpa os campos da estação
                LimpaCamposEstacao();
                //Limpa os campos do produto
                LimparCamposProduto();
                //Instância o objêto
                EstacaoNegocios estacaoNegocios = new EstacaoNegocios();
                //Instância o objêto
                Estacao estacao = new Estacao();
                //Recebe o id
                estacao = estacaoNegocios.PesqId();
                //Seta o novo id
                txtCodigo.Text = estacao.codEstacao.ToString();
                //Controle para salvar 
                opcao = true;
                //Habilita componentes
                Habilita();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Pesquisa as estações
        private void PesqEstacao()
        {
            try
            {
                //Instância o objêto
                EstacaoNegocios estacaoNegocios = new EstacaoNegocios();
                //Instância a coleção
                EstacaoCollection estacaoCollection = new EstacaoCollection();
                //A coleção recebe o resultado da consulta
                estacaoCollection = estacaoNegocios.PesqEstacao(chkPesqAtivo.Checked, cmbEmpresa.Text);
                //Limpa o grid
                gridEstacao.Rows.Clear();
                //Grid Recebe o resultado da coleção
                estacaoCollection.ForEach(n => gridEstacao.Rows.Add(n.codEstacao, n.descEstacao, n.nivel, n.codUsuario, n.loginUsuario, n.tipo, n.volumeIndependente, n.status));

                if (gridEstacao.Rows.Count > 0)
                {
                    //Qtd de estacao encontrada
                    lblQtd.Text = gridEstacao.RowCount.ToString();
                    //Seleciona a primeira linha do grid
                    gridEstacao.CurrentCell = gridEstacao.Rows[0].Cells[1];
                    //Foca no grid
                    gridEstacao.Focus();
                    //Seta os dados nos campos
                    DadosEstacaoCampos();

                    if (cmbRegiao.Items.Count == 0)
                    {
                        //Pesquisa as regiões
                        PesqRegiao();
                    }
                }
                else
                {
                    MessageBox.Show("Nenhuma estação encontrada! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Seta os dados nos campos
        private void DadosEstacaoCampos()
        {
            try
            {
                if ((gridEstacao.SelectedRows.Count > 0))
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridEstacao.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[0].Value);
                    //Seta o valor da descrição
                    txtDescricao.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[1].Value);
                    //Seta o nível
                    cmbNivelEstacao.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[2].Value);
                    //Seta o código do usuário
                    txtCodUsuario.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[3].Value);
                    //Seta o login
                    txtLogin.Text = Convert.ToString(gridEstacao.Rows[indice].Cells[4].Value);
                    //Seta o tipo
                    if (gridEstacao.Rows[indice].Cells[5].Value.ToString() == "Fixa")
                    {
                        rbtFixo.Checked = true;
                    }
                    else
                    {
                        rbtMovel.Checked = true;
                    }

                    //Seta o se é volume Independene
                    //chkVolumeIndependente.Checked = Convert.ToBoolean(gridEstacao.Rows[indice].Cells[11].Value);
                    //Seta o status 
                    chkAtivo.Checked = Convert.ToBoolean(gridEstacao.Rows[indice].Cells[7].Value);

                    //Pesquisa os blocos da estação
                    PesqBlocoEstacao();
                    //Pesquisa os produtos vinculados a estação
                    PesqProdutoEstacao();

                    //Desabilita todos os campos
                    Desabilita();
                    //Controle para alterar
                    opcao = false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        //Ppesquisa os blocos associado a estação
        private void PesqBlocoEstacao()
        {
            try
            {
                //Instância as linha do grid
                DataGridViewRow linha = gridEstacao.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Instância o negocios
                EstacaoNegocios estacaoNegocios = new EstacaoNegocios();
                //Instância a coleção de blocos da estação
                EstacaoCollection blocoCollection = new EstacaoCollection();
                //A coleção recebe o resultado da consulta
                blocoCollection = estacaoNegocios.PesqBlocoEstacao(Convert.ToInt32(gridEstacao.Rows[indice].Cells[0].Value));

                //Limpa o grid
                gridBloco.Rows.Clear();
                //Grid Recebe o resultado da coleção
                blocoCollection.ForEach(n => gridBloco.Rows.Add(n.regiao, n.rua, n.codBloco, n.bloco, n.codEstacao));

                //Preenche a lista com os blocos da estação para controle de novo endereço do produto (verifica de o endereço pertence o bloco da estação)
                blocoEstacao = blocoCollection.FindAll(delegate (Estacao n) { return n.codEstacao == Convert.ToInt32(gridEstacao.Rows[indice].Cells[0].Value); });

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o produto endereçado ou para ser endereçado
        private void PesqProduto()
        {
            try
            {
                //Instância o objêto
                EstacaoNegocios estacaoNegocios = new EstacaoNegocios();
                //Instância a coleção
                EnderecoPicking enderecoProduto = new EnderecoPicking();
                //O objeto recebe o resultado da consulta
                enderecoProduto = estacaoNegocios.PesqProdutoFlowrack(txtPesqProduto.Text, "FLOWRACK");

                if (enderecoProduto.idProduto > 0)
                {
                    //Variável recebe o id do produto para inserir no rastreamento
                    idProduto = enderecoProduto.idProduto;

                    //Verifica se o produto não está selecionado para o flowrack
                    if (enderecoProduto.separacaoFlowrack == false)
                    {

                        if (MessageBox.Show("O SKU " + enderecoProduto.codProduto + " - " + enderecoProduto.descProduto + " não está selecionado para o flowrack, deseja selecioná-lo?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //Atualiza a seleção separação do flowrack do produto
                            AlterarSepProdutoFlowrack(enderecoProduto.codProduto);

                            lblDescProduto.Text = enderecoProduto.descProduto;
                            lblQuantidade.Text = Convert.ToString(enderecoProduto.estoque);
                            lblValidade.Text = string.Format("{0:dd/MM/yyyy}", enderecoProduto.vencimento);
                            lblEndereco.Text = enderecoProduto.endereco;
                            lblLote.Text = enderecoProduto.lote;
                            txtCapacidade.Text = Convert.ToString(enderecoProduto.capacidade);
                            txtAbastecimento.Text = Convert.ToString(enderecoProduto.abastecimento);
                            //Foca no campo 
                            txtCapacidade.Focus();
                        }
                    }
                    else
                    {
                        lblDescProduto.Text = enderecoProduto.descProduto;
                        lblQuantidade.Text = Convert.ToString(enderecoProduto.estoque);
                        lblValidade.Text = string.Format("{0:dd/MM/yyyy}", enderecoProduto.vencimento);
                        lblEndereco.Text = enderecoProduto.endereco;
                        lblLote.Text = enderecoProduto.lote;
                        txtCapacidade.Text = Convert.ToString(enderecoProduto.capacidade);
                        txtAbastecimento.Text = Convert.ToString(enderecoProduto.abastecimento);
                        //Foca no campo 
                        txtCapacidade.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum SKU encontrado! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Seta os dados nos campos
        private void DadosProdutoCampos()
        {
            try
            {
                if ((gridEndereco.SelectedRows.Count > 0))
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridEndereco.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    chkVolumeIndependente.Checked = Convert.ToBoolean(gridEndereco.Rows[indice].Cells[11].Value);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados do produto no campo!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        //Pesquisa o produto já endereçado na estação
        private void PesqProdutoEstacao()
        {
            try
            {
                //Instância as linha do grid
                DataGridViewRow linha = gridEstacao.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                if (enderecoCollection.Count == 0)
                {
                    //Instância o objêto
                    EstacaoNegocios estacaoNegocios = new EstacaoNegocios();
                    //A coleção recebe o resultado da consulta
                    enderecoCollection = estacaoNegocios.PesqProdutoEstacao(cmbEmpresa.Text);
                }

                //Localizando os endereços da estação selecionada
                List<EnderecoPicking> produtoEstacao = enderecoCollection.FindAll(delegate (EnderecoPicking n) { return n.codEstacao == Convert.ToInt32(gridEstacao.Rows[indice].Cells[0].Value); }).OrderBy(c => c.ordemEndereco).ToList();

                //Limpa o grid
                gridEndereco.Rows.Clear();
                //Grid Recebe o resultado da coleção
                produtoEstacao.ForEach(n => gridEndereco.Rows.Add(n.codSeparacao, n.codApartamento, n.endereco, n.idProduto, n.codProduto + "-" + n.descProduto, 
                    n.numeroRegiao, n.numeroRua, n.numeroBloco, n.numeroNivel, n.numeroApartamento, n.nomeEmpresa, n.volumeIdependente,
                    n.estoque, n.vencimento, n.peso, n.lote));

                if(gridEndereco.SelectedRows.Count > 0)
                {
                    DadosProdutoCampos();
                }
                else
                {
                    chkVolumeIndependente.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o usuário da estação
        private void pesqUsuario()
        {
            try
            {
                //Instância o negocios
                UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                //Instância a coleçãO
                UsuarioCollection usuarioCollection = new UsuarioCollection();
                //A coleção recebe o resultado da consulta
                usuarioCollection = usuarioNegocios.PesqUsuario(txtCodUsuario.Text, "", "CONFERENTE", null);

                if (usuarioCollection.Count > 0)
                {
                    //Seta o login do usuário no campo
                    txtLogin.Text = usuarioCollection[0].login;
                    //Foca no código do usuário
                    txtCodUsuario.Focus();
                }
                else
                {
                    MessageBox.Show("Nenhum usuário encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRegiao("");
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
                cmbBlocoIncial.Text = "Selecione";
                cmbBlocoFinal.Text = "Selecione";

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

        //Pesquisa o Bloco
        private void PesqBloco()
        {
            try
            {
                //Limpa o combobox rua
                cmbBlocoIncial.Items.Clear();
                cmbBlocoFinal.Items.Clear();

                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa os bloco da rua selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqBloco(rua[cmbRua.SelectedIndex]);
                //Preenche o combobox rua
                gerarEnderecoCollection.ForEach(n => cmbBlocoIncial.Items.Add(n.numeroBloco));
                //Preenche o combobox rua
                gerarEnderecoCollection.ForEach(n => cmbBlocoFinal.Items.Add(n.numeroBloco));

                bloco = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    bloco[i] = gerarEnderecoCollection[i].codBloco;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Salva os dados da estação
        private void SalvarEstacao()
        {
            try
            {
                if (txtCodigo.Text.Equals("") || txtDescricao.Text.Equals("") || cmbNivelEstacao.Text.Equals(""))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txtCodUsuario.Text.Equals(""))
                {
                    //Mensagem
                    MessageBox.Show("Preencha o campo usuário da estação!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objêto
                    EstacaoNegocios estacaoNegocios = new EstacaoNegocios();

                    if (rbtFixo.Checked == true)
                    {
                        //executa o método salvar
                        estacaoNegocios.SalvarEstacao(Convert.ToInt32(txtCodigo.Text), txtDescricao.Text, cmbNivelEstacao.Text, false, chkAtivo.Checked, "Fixa", txtCodUsuario.Text, cmbEmpresa.Text);
                        //Insere o cadastro no grid
                        gridEstacao.Rows.Add(txtCodigo.Text, txtDescricao.Text, cmbNivelEstacao.Text, txtCodUsuario.Text, txtLogin.Text, "Fixa", false, chkAtivo.Checked, cmbEmpresa.Text);

                    }
                    else
                    {
                        //executa o método salvar
                        estacaoNegocios.SalvarEstacao(Convert.ToInt32(txtCodigo.Text), txtDescricao.Text, cmbNivelEstacao.Text, false, chkAtivo.Checked, "Movel", txtCodUsuario.Text, cmbEmpresa.Text);
                        //Insere o cadastro no grid
                        gridEstacao.Rows.Add(txtCodigo.Text, txtDescricao.Text, cmbNivelEstacao.Text, txtCodUsuario.Text, txtLogin.Text, "Movel", false, chkAtivo.Checked, cmbEmpresa.Text);

                    }

                    //Qtd de estacao encontrada
                    lblQtd.Text = gridEstacao.RowCount.ToString();

                    //Recebe a qtd de linha no grid 
                    int linha = Convert.ToInt32(gridEstacao.RowCount.ToString());
                    //Seleciona a linha      
                    gridEstacao.CurrentCell = gridEstacao.Rows[linha - 1].Cells[1];
                    //Desabilita todos os campos
                    Desabilita();
                    //controle para alterar
                    opcao = false;

                    MessageBox.Show("Cadastro realizado com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro realizar o cadastro! \nDetalhes: " + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Altera os dados da estação
        private void AlterarEstacao()
        {
            try
            {
                if (txtCodigo.Text.Equals("") || txtDescricao.Text.Equals(""))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txtCodUsuario.Text.Equals(""))
                {
                    //Mensagem
                    MessageBox.Show("Preencha o campo usuário da estação!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridEstacao.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Instância o objêto
                    EstacaoNegocios estacaoNegocios = new EstacaoNegocios();

                    if (rbtFixo.Checked == true)
                    {
                        //executa o método alterar
                        estacaoNegocios.AlterarEstacao(Convert.ToInt32(txtCodigo.Text), txtDescricao.Text, cmbNivelEstacao.Text, false, chkAtivo.Checked, "Fixa", txtCodUsuario.Text);
                        //Seta no grid o tipo de estação
                        gridEstacao.Rows[indice].Cells[5].Value = "Fixa";
                    }
                    else
                    {
                        //executa o método alterar
                        estacaoNegocios.AlterarEstacao(Convert.ToInt32(txtCodigo.Text), txtDescricao.Text, cmbNivelEstacao.Text, false, chkAtivo.Checked, "Movel", txtCodUsuario.Text);
                        //Seta no grid o tipo de estação
                        gridEstacao.Rows[indice].Cells[5].Value = "Movel";
                    }

                    //Insere a alteração no grid                      
                    gridEstacao.Rows[indice].Cells[1].Value = txtDescricao.Text;
                    gridEstacao.Rows[indice].Cells[2].Value = cmbNivelEstacao.Text;
                    gridEstacao.Rows[indice].Cells[4].Value = txtLogin.Text;
                    gridEstacao.Rows[indice].Cells[6].Value = false;
                    gridEstacao.Rows[indice].Cells[7].Value = chkAtivo.Checked;

                    //Foca no grid
                    gridEstacao.Focus();
                    //Desabilita todos os campos
                    Desabilita();

                    MessageBox.Show("Cadastro alterado com sucesso! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Associa o bloco ao endereco
        private void AssociaBlocoEstacao()
        {
            try
            {
                if (cmbRegiao.Text.Equals("Selecione") || cmbRua.Text.Equals("Selecione") || cmbBlocoIncial.Text.Equals("Selecione") || cmbBlocoFinal.Text.Equals("Selecione"))
                {
                    MessageBox.Show("Por favor, selecione o(s) bloco(s) desejado(s)!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridEstacao.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Instância o objêto
                    EstacaoNegocios estacaoNegocios = new EstacaoNegocios();

                    estacaoNegocios.AssociaBlocoEstacao(Convert.ToInt32(gridEstacao.Rows[indice].Cells[0].Value), Convert.ToInt32(regiao[cmbRegiao.SelectedIndex]),
                        Convert.ToInt32(rua[cmbRua.SelectedIndex]), Convert.ToInt32(cmbBlocoIncial.SelectedItem), Convert.ToInt32(cmbBlocoFinal.SelectedItem));

                    //Pesquisa os blocos da estação
                    PesqBlocoEstacao();
                    //Limpa os campos dos blocos
                    LimparCamposBlocos();

                    MessageBox.Show("Bloco(s) associado(s) a " + gridEstacao.Rows[indice].Cells[1].Value + " com sucesso! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Desassocia os blocos selecionados da estação
        private void DesassociarBlocoEstacao()
        {
            try
            {
                if (MessageBox.Show("Tem certeza que deseja desassociar o(s) bloco(s) selecionado(s) da estação?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //Instância o objêto Negocios
                    EstacaoNegocios estacaoNegocios = new EstacaoNegocios();

                    foreach (DataGridViewRow row in gridBloco.SelectedRows)
                    {
                        //Deleta o bloco da coleção
                        blocoEstacao.RemoveAll((x) => x.codBloco == Convert.ToInt32(row.Cells[2].Value));
                        //Deleta o endereço com o produto ocupado
                        estacaoNegocios.DesassociaBlocoEstacao(Convert.ToInt32(row.Cells[2].Value));
                    }

                    //Atualiza o grid com a coleção
                    PesqBlocoEstacao();

                    MessageBox.Show("Bloco(s) desassociado(s) com sucesso da estação! ", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Endereça o produto na estação
        private void EnderecarProduto()
        {
            try
            {
                if (txtPesqProduto.Text.Equals("") || txtCapacidade.Text.Equals("") || txtAbastecimento.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, preencha às informações do SKU! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo código do produto
                    txtPesqProduto.Focus();
                }
                else if (txtNovoEndereco.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, digite um endereço! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo novo endereço
                    txtNovoEndereco.Focus();
                }
                else
                {
                    //Instância a camada de negocios
                    EnderecamentoNegocios enderecoNegocios = new EnderecamentoNegocios();
                    //Instância o objêto 
                    EnderecoPicking endereco = new EnderecoPicking();
                    //O objeto recebe o resultado da consulta
                    endereco = enderecoNegocios.PesqEndereco(txtNovoEndereco.Text);

                    //Instância as linha da tabela
                    DataGridViewRow linha = gridEstacao.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    if (endereco.codApartamento > 0)
                    {
                        var verificaBlobo = blocoEstacao.FindAll(delegate (Estacao n) { return n.codBloco == endereco.codBloco; });

                        //Verifica se o endereço digitado pertence ao bloco da estação
                        if (verificaBlobo.Count == 0)
                        {
                            MessageBox.Show("O endereço digitado não pertence a nenhum bloco associado a estação", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            //Verifica a disponibildade do endereço
                            if (!endereco.apDisponibilidade.Equals("Sim"))
                            {
                                if (MessageBox.Show("O endereço digitado encontra-se indisponível, deseja disponibilizá-lo?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    enderecoNegocios.AtualizarDisponibilidadeEndereco(endereco.codApartamento, "Sim", cmbEmpresa.Text);
                                    MessageBox.Show("O endereço foi disponibilizado com sucesso! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //Chama novamente o método
                                    EnderecarProduto();
                                }
                            }//Verifica se o endereço é da área de separação
                            else if (!endereco.tipoEndereco.Equals("Separacao"))
                            {
                                MessageBox.Show("Por favor, digite um endereço de separação! ", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            //Gera um novo endereço de flowrack se o endereço é está vago
                            else if (endereco.apStatus.Equals("Vago"))
                            {
                                //Se o produto já estiver endereçado
                                if (lblEndereco.Text != string.Empty)
                                {
                                    //Pesquisa o endereço tual do produto
                                    var antigoEndereco = enderecoCollection.FindAll(delegate (EnderecoPicking n) { return n.endereco == lblEndereco.Text; });
                                   
                                    //Verifica se o endeço existe na coleção
                                    if (antigoEndereco.Count > 0)
                                    {
                                        //Deleta o endereço da coleção
                                        enderecoCollection.RemoveAll((x) => x.codApartamento == antigoEndereco[0].codApartamento);
                                    }                 

                                    //Deleta o endereço com o produto ocupado
                                    enderecoNegocios.DeletaEnderecoSeparacao(antigoEndereco[0].codApartamento, cmbEmpresa.Text);
                                    
                                }

                                //Se a validade estiver vazia
                                if (lblValidade.Text == string.Empty)
                                {
                                    //Data do dia
                                    DateTime dataDia = DateTime.Today;
                                    lblValidade.Text = Convert.ToString(dataDia.ToString("d"));
                                }

                                //Endereça o produto para separação de flowrack
                                enderecoNegocios.EnderecarProdutoSeparacao(Convert.ToInt32(gridEstacao.Rows[indice].Cells[0].Value), endereco.codApartamento, txtPesqProduto.Text, 0, Convert.ToDateTime(lblValidade.Text), Convert.ToInt32(txtCapacidade.Text), Convert.ToInt32(txtAbastecimento.Text), lblLote.Text, "FLOWRACK", codUsuario, cmbEmpresa.Text);

                                //Registra a exclusão no rastreamento
                                enderecoNegocios.InserirRastreamento(cmbEmpresa.Text, "INCLUSÃO", codUsuario, idProduto, //Código do produto
                                                                                 null, null, null, null, null,
                                                                                 Convert.ToInt32(endereco.codApartamento), //Código do endereço
                                                                                 Convert.ToInt32(0), //Quantidade do produto
                                                                                 Convert.ToString(lblValidade.Text), //Vencimento do produto
                                                                                 Convert.ToDouble(0), null, "FLOWRACK"); //peso do produto
                                
                                
                                endereco.codEstacao = Convert.ToInt32(gridEstacao.Rows[indice].Cells[0].Value);
                                endereco.endereco = txtNovoEndereco.Text;
                                endereco.codProduto = txtPesqProduto.Text;
                                endereco.descProduto = lblDescProduto.Text;
                                endereco.apStatus = "Ocupado";
                                enderecoCollection.Add(endereco);

                                //Atualiza o grid com a coleção
                                PesqProdutoEstacao();

                                //Limpa os campos do produto
                                LimparCamposProduto();

                                MessageBox.Show("Produto endereçado com sucesso na " + gridEstacao.Rows[indice].Cells[1].Value + "!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }//Transfere de um endereço de flowrack para outro                   
                            else if (endereco.apStatus.Equals("Ocupado"))
                            {
                                //Pesquisa o 
                                var novoEndereco = enderecoCollection.FindAll(delegate (EnderecoPicking n) { return n.endereco == txtNovoEndereco.Text; });                             

                                if (novoEndereco.Count != 0)
                                {

                                    if (MessageBox.Show("O endereço digitado encontra-se ocupado com o produto " + novoEndereco[0].codProduto + "-" + novoEndereco[0].descProduto + ", deseja transferir assim mesmo? Este produto ficará desendereçado!", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        //Deleta o endereço com o produto ocupado
                                        enderecoNegocios.DeletaEnderecoSeparacao(Convert.ToInt32(novoEndereco[0].codApartamento), cmbEmpresa.Text);

                                        //Registra a exclusão no rastreamento
                                        enderecoNegocios.InserirRastreamento(cmbEmpresa.Text, "EXCLUSÃO", codUsuario,
                                                                                        novoEndereco[0].idProduto, //Código do produto                                                                                     
                                                                                         Convert.ToInt32(novoEndereco[0].codApartamento), //Código do endereço
                                                                                         Convert.ToInt32(novoEndereco[0].estoque), //Quantidade do produto
                                                                                         Convert.ToString(novoEndereco[0].vencimento), //Vencimento do produto
                                                                                         Convert.ToDouble(novoEndereco[0].peso),
                                                                                         novoEndereco[0].lote,
                                                                                         null, null, null, null, null, "FLOWRACK"); //peso do produto


                                        //Deleta o endereço existe da coleção
                                        enderecoCollection.RemoveAll((x) => x.codApartamento == novoEndereco[0].codApartamento);
                                        //Cria o novo endereço, chamando novamente o método
                                        EnderecarProduto();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("O status de endereço se encontra ocupado sem existir produto no endereço, por favor vá no cadastro -> Estrutura selecione o endereço e o coloque como vago!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Endereço não encontrado! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Altera a separacão do produto no flowrack
        private void AlterarSepProdutoFlowrack(string codProduto)
        {
            try
            {
                //Instância o objêto
                EstacaoNegocios estacaoNegocios = new EstacaoNegocios();
                //O objeto recebe o resultado da consulta
                estacaoNegocios.AlterarSepProdutoFlowrack(codProduto);

                MessageBox.Show("Produto selecionado para o flowrack com sucesso! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Altera a separacão do produto no flowrack
        private void AlterarVolumeIndependete()
        {
            try
            {
                if(gridEndereco.SelectedRows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridEndereco.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Instância o objêto
                    EstacaoNegocios estacaoNegocios = new EstacaoNegocios();
                    //O objeto recebe o resultado da consulta
                    estacaoNegocios.AlterarVolumeIndependente(Convert.ToString(gridEndereco.Rows[indice].Cells[3].Value), chkVolumeIndependente.Checked);
                    //Altera o valor no grupo
                    gridEndereco.Rows[indice].Cells[11].Value = chkVolumeIndependente.Checked;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Delata o produto dos endereços selecionados
        private void DelatarProdutoEndereco()
        {
            try
            {
                if (MessageBox.Show("Tem certeza que deseja excluir o(s) SKU(s) selecionado(s) do(s) endereço(s)?", "Wms - Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //Instância a camada de negocios
                    EnderecamentoNegocios enderecoNegocios = new EnderecamentoNegocios();

                    foreach (DataGridViewRow row in gridEndereco.SelectedRows)
                    {
                        //Deleta o endereço da coleção
                        enderecoCollection.RemoveAll((x) => x.codApartamento == Convert.ToInt32(row.Cells[1].Value));
                        //Deleta o endereço com o produto ocupado
                        enderecoNegocios.DeletaEnderecoSeparacao(Convert.ToInt32(row.Cells[1].Value), cmbEmpresa.Text);

                        //Registra a exclusão no rastreamento
                        enderecoNegocios.InserirRastreamento(cmbEmpresa.Text, "EXCLUSÃO", codUsuario,
                                                                        Convert.ToInt32(row.Cells[3].Value), //Código do produto                                                                                     
                                                                         Convert.ToInt32(row.Cells[1].Value), //Código do endereço
                                                                         Convert.ToInt32(row.Cells[12].Value), //Quantidade do produto
                                                                         Convert.ToString(row.Cells[13].Value), //Vencimento do produto
                                                                         Convert.ToDouble(row.Cells[14].Value),
                                                                         Convert.ToString(row.Cells[15].Value),
                                                                         null, null, null, null, null, "FLOWRACK"); //peso do produto


                    }

                    //Atualiza o grid com a coleção
                    PesqProdutoEstacao();

                    MessageBox.Show("SKU(s) deletado(s) com sucesso do(s) endereço(s)! ", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Imprime a etiqueta
        private void ImpressaoEtiqueta()
        {
            try
            {
                //Pega o caminho da etiqueta prn
                string etiqueta = AppDomain.CurrentDomain.BaseDirectory + "Picking_Flow_40x100.prn";

                //Caminho do novo arquivo atualizado
                string NovaEtiqueta = AppDomain.CurrentDomain.BaseDirectory + "Picking_Flow_40x100.txt";

                foreach (DataGridViewRow row in gridEndereco.SelectedRows)
                {
                    // Abre o arquivo para escrita
                    StreamReader streamReader;
                    streamReader = File.OpenText(etiqueta);

                    string contents = streamReader.ReadToEnd();

                    streamReader.Close();

                    string conteudo = contents;

                    StreamWriter streamWriter = File.CreateText(NovaEtiqueta);

                    // Atualiza as variaveis do arquivo
                    //streamWriter.WriteLine("<STX>L");
                    conteudo = conteudo.Replace("P5", "" + row.Cells[1].Value);
                    conteudo = conteudo.Replace("P1", "" + row.Cells[5].Value);
                    conteudo = conteudo.Replace("P2", "" + row.Cells[6].Value);
                    conteudo = conteudo.Replace("P3", "" + row.Cells[7].Value);// BLOCO
                    conteudo = conteudo.Replace("P4", "" + row.Cells[9].Value);
                    conteudo = conteudo.Replace("EMPRESA", "" + row.Cells[10].Value);
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


        //Limpa os campos da estação
        private void LimpaCamposEstacao()
        {
            //Limpa o campo código
            txtCodigo.Clear();
            //Limpa o campo descrição
            txtDescricao.Clear();
            //Limpa o campo nivel
            cmbNivelEstacao.Text = "";
            //Limpa o campo código do usuário
            txtCodUsuario.Clear();
            //Limpa o login do usuário
            txtLogin.Clear();
            //Marca o radiobutton fixo
            rbtFixo.Checked = true;
            //Marca o status 
            chkAtivo.Checked = true;
            //Seta o texto como padrão
            txtDescricao.Text = "ESTAÇÃO FIXA ";
        }

        //Limpa os campos do produto
        private void LimparCamposProduto()
        {
            //Limpa o campo do produto
            txtPesqProduto.Clear();
            lblDescProduto.Text = "DIGITE  O CÓDIGO DO SKU ACIMA";
            lblQuantidade.Text = "-";
            lblValidade.Text = "-";
            lblEndereco.Text = "-";
            txtCapacidade.Clear();
            txtAbastecimento.Clear();
            txtNovoEndereco.Clear();
            //Foca no campo código do produto
            txtPesqProduto.Focus();
        }

        //Limpa os campos dos blocos da estação
        private void LimparCamposBlocos()
        {
            cmbRegiao.Text = "Selecione";
            cmbRua.Text = "Selecione";
            cmbBlocoIncial.Text = "Selecione";
            cmbBlocoFinal.Text = "Selecione";
        }

        //Habilita os campos
        private void Habilita()
        {
            //Habilita a descrição
            txtDescricao.Enabled = true;
            //Habilita o nível
            cmbNivelEstacao.Enabled = true;
            //Habilita o campo código usuario
            txtCodUsuario.Enabled = true;
            //Habilita o radiobutton fixa
            rbtFixo.Enabled = true;
            //Habilita o radiobutton móvel
            rbtMovel.Enabled = true;
            //Habilita Volume Independene
            chkVolumeIndependente.Enabled = true;
            //Habilita o status
            chkAtivo.Enabled = true;
            //Habilita o botão salvar
            btnSalvar.Enabled = true;
            //Desabilita o botão novo
            btnNovo.Enabled = false;

            //Foca no campo descrição
            txtDescricao.Focus();

            //Habilita o grid endereço
            gridEndereco.Enabled = true;
            //Habilita o grid bloco
            gridBloco.Enabled = true;

            //Habilta o campo do produto
            txtPesqProduto.Enabled = true;
            txtCapacidade.Enabled = true;
            txtAbastecimento.Enabled = true;
            txtNovoEndereco.Enabled = true;
            btnEnderecarProduto.Enabled = true;

            //Habilita os campos dos endereços
            cmbRegiao.Enabled = true;
            cmbRua.Enabled = true;
            cmbBlocoIncial.Enabled = true;
            cmbBlocoFinal.Enabled = true;
            btnAssociarBloco.Enabled = true;

            //Limpa os campos
            LimparCamposBlocos();

        }
        //Desabilita os campos
        private void Desabilita()
        {
            //desabilita a descrição
            txtDescricao.Enabled = false;
            //desabilita o nível
            cmbNivelEstacao.Enabled = false;
            //desabilita o campo código usuario
            txtCodUsuario.Enabled = false;
            //desabilita o radiobutton fixa
            rbtFixo.Enabled = false;
            //desabilita o radiobutton móvel
            rbtMovel.Enabled = false;
            //Desabilita o volume Independene
            chkVolumeIndependente.Enabled = false;
            //Desabilita o status 
            chkAtivo.Enabled = false;
            //desabilita o botão
            btnSalvar.Enabled = false;
            //desabilita o botão novo
            btnNovo.Enabled = true;

            //desabilita o grid endereço
            gridEndereco.Enabled = false;
            //desabilita o grid bloco
            gridBloco.Enabled = false;

            //desabilta o campo do produto
            txtPesqProduto.Enabled = false;
            txtCapacidade.Enabled = false;
            txtAbastecimento.Enabled = false;
            txtNovoEndereco.Enabled = false;
            btnEnderecarProduto.Enabled = false;

            //Desabilita os campos dos endereços
            cmbRegiao.Enabled = false;
            cmbRua.Enabled = false;
            cmbBlocoIncial.Enabled = false;
            cmbBlocoFinal.Enabled = false;
            btnAssociarBloco.Enabled = false;

        }

        private void FrmEstacao_Load_1(object sender, EventArgs e)
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

        private void txtDescricao_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }
    }
}
