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
using System.Windows.Forms;
using Wms.Relatorio;

namespace Wms
{
    public partial class FrmAbastecimento : Form
    {
        private int[] regiao; //Array com id da regiao
        private int[] rua; //Array com id da rua
        private int[] categoria; //Array com id da rua
        private int[] empilhador; //Array com id do empilhador
        private int[] repositor; //Array com id do repositor

        public int codUsuario; //Código do usuário

        //Instância a coleção de objêto responsável por receber os itens da ordem gerada na pesquisa
        ItensAbastecimentoCollection itensCollection = new ItensAbastecimentoCollection();

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;
        public List<Empresa> empresaCollection;
        private static string empresa = string.Empty;

        public FrmAbastecimento()
        {
            InitializeComponent();
        }

        private void FrmAbastecimento_Load(object sender, EventArgs e)
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
                    empresa = cmbEmpresa.Text;

                }
                else
                {
                    cmbEmpresa.Enabled = true;
                }
            }
        }

        //Changed
        private void cmbModo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbModo.Text.Equals("Corretivo"))
            {
                grpEstacao.Visible = true; //Exibe o grupo de estações
                grpManifesto.Visible = false; //Esconde o grupo de manifesto
                cmbTipo.Enabled = true; //Habilita o campo tipo

                ColPulmaoEstacao.Visible = true; //Exibe a coluna da estação no grid observação

                ColItensEstacao.Visible = true; //Exibe a coluna da estação no grid itens
                colitensEstoque.Visible = true; //Exibe a coluna do estoque no grid itens
                colitensTipoEstoque.Visible = true; //Exibe a coluna tipo de estoque no grid itens

                ColObsEstacao.Visible = true; //Exibe a coluna da estação no grid observação
                ColObsEstoque.Visible = true; //Exibe a coluna estoque no grid observação
                ColObsTipoEstoque.Visible = true; //Exibe a coluna tipo de estoque no grid observação


            }
            else if (cmbModo.Text.Equals("Preventivo"))
            {
                grpManifesto.Visible = true; //Exibe o grupo de manifesto
                grpEstacao.Visible = false; //Esconde o grupo de estações
                cmbTipo.Enabled = false; //Desabilita o campo tipo

                ColPulmaoEstacao.Visible = false; //Esconde a coluna da estação no grid observação

                ColItensEstacao.Visible = false; //Esconde a coluna da estação no grid itens
                colitensEstoque.Visible = false; //Esconde a coluna do estoque no grid itens
                colitensTipoEstoque.Visible = false; //Esconde a coluna tipo de estoque no grid itens

                ColObsEstacao.Visible = false; //Esconde a coluna da estação no grid observação
                ColObsEstoque.Visible = false; //Esconde a coluna estoque no grid observação
                ColObsTipoEstoque.Visible = false; //Esconde a coluna tipo de estoque no grid observação

                cmbRegiao.Enabled = false; //Desabilitar o campo região
                cmbRua.Enabled = false; //Desabilitar o campo rua
                cmbLado.Enabled = false; //Desabilitar o campo lado
                cmbCategoria.Enabled = false; //Desabilitar o campo categoria
                txtCodFornecedor.Enabled = false; //Desabilitar o campo código do fornecedor

                //Adiciona o texto
                cmbRegiao.Text = "Selec...";
                cmbRua.Text = "Selec...";
                cmbLado.Text = "Todos";
                cmbCategoria.Text = "";
                txtCodFornecedor.Clear();
                txtNomeFornecedor.Clear();

            }

            cmbTipo.Text = "Selecione"; //Exibe o texto

        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbTipo.Text.Equals("FlowRack")) //Verifica se é flow rack
            {
                chkSelecionar.Enabled = true; //Habilita o checkbox  da estação   
                gridEstacao.Enabled = true; //Habilita o grid estação                    

                ColItensEstacao.Visible = true; //Exibe a coluna da estação no grid itens
                colitensEstoque.Visible = true; //Exibe a coluna do estoque no grid itens
                colitensTipoEstoque.Visible = true; //Exibe a coluna tipo de estoque no grid itens

                ColPulmaoEstacao.Visible = true; //Exibe a coluna da estação no grid observação

                ColObsEstacao.Visible = true; //Exibe a coluna da estação no grid observação
                ColObsEstoque.Visible = true; //Exibe a coluna estoque no grid observação
                ColObsTipoEstoque.Visible = true; //Exibe a coluna tipo de estoque no grid observação

                cmbRegiao.Enabled = false; //Desabilitar o campo região
                cmbRua.Enabled = false; //Desabilitar o campo rua
                cmbLado.Enabled = false; //Desabilitar o campo lado
                cmbCategoria.Enabled = false; //Desabilitar o campo categoria
                txtCodFornecedor.Enabled = false; //Desabilitar o campo código do fornecedor

                //Adiciona o texto
                cmbRegiao.Text = "Selec...";
                cmbRua.Text = "Selec...";
                cmbLado.Text = "Todos";
                cmbCategoria.Text = "";
                txtCodFornecedor.Clear();
                txtNomeFornecedor.Clear();

                if (gridEstacao.Rows.Count == 0) //Verifica se existe pesquisa realizada
                {
                    PesqEstacao(); //Pesquisa as estações
                }
            }
            else if (cmbTipo.Text.Equals("Caixa")) //Verifica se é flow rack
            {
                chkSelecionar.Enabled = false; //Desabilita o checkbox  da estação                  
                gridEstacao.Enabled = false; //Desabilita o grid estação                   

                ColItensEstacao.Visible = false; //Esconde a coluna da estação no grid itens
                colitensEstoque.Visible = true; //Esconde a coluna do estoque no grid itens
                colitensTipoEstoque.Visible = true; //Esconde a coluna tipo de estoque no grid itens
                colItensPulmao.HeaderText = "Pulmão"; //Exibe o nome da coluna

                ColPulmaoEstacao.Visible = false; //Esconde a coluna da estação no grid observação

                ColObsEstacao.Visible = false; //Esconde a coluna da estação no grid observação
                ColObsEstoque.Visible = true; //Exibe a coluna estoque no grid observação
                ColObsTipoEstoque.Visible = true; //Exibe a coluna tipo de estoque no grid observação

                cmbRegiao.Enabled = true; //Habilitar o campo região
                cmbRua.Enabled = true; //Habilitar o campo rua
                cmbLado.Enabled = true; //Habilitar o campo lado
                cmbCategoria.Enabled = true; //Habilitar o campo categoria
                txtCodFornecedor.Enabled = true; //Habilitar o campo código do fornecedor
            }

        }

        private void cmbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {

            PesqRua(); //Pesquisa a rua
        }

        private void chkSelecionar_CheckedChanged(object sender, EventArgs e)
        {
            if (gridEstacao.Rows.Count > 0)//Verifica se existe dados no grid
            {
                if (chkSelecionar.Checked == true) //Verifica se o checkbox está selecionado
                {
                    foreach (DataGridViewRow row in gridEstacao.Rows) //Percorre todas as linha do grid
                    {
                        row.Cells[0].Value = true; //Seleciona todas as estações
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in gridEstacao.Rows) //Percorre todas as linha do grid
                    {
                        row.Cells[0].Value = false; //Seleciona todas as estações
                    }
                }
            }
        }

        //KeyPress
        private void txtPesqCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbPesqModo.Focus();
            }
        }

        private void cmbPesqModo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbPesqTipo.Focus();
            }
        }

        private void cmbPesqTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbPesqStatus.Focus();
            }
        }

        private void cmbPesqStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmInicial.Focus();
            }
        }

        private void dtmInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dtmFinal.Focus();
            }
        }

        private void dtmFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnPesquisar.Focus();
            }
        }

        //KeyPress - Cadastro

        private void cmbModo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbTipo.Focus();
            }
        }

        private void cmbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbRegiao.Focus();
            }
        }

        private void cmbRegiao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbRua.Focus();
            }
        }

        private void cmbRua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbLado.Focus();
            }
        }

        private void cmbLado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbCategoria.Focus();
            }
        }

        private void cmbCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCodFornecedor.Focus();
            }
        }

        private void txtCodFornecedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Pesquisa o fonecedor
                PesqFornecedor();
            }
        }

        private void txtManifesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnManifesto.Focus(); //Foca no campo botão
            }
        }

        private void cmbEmpilhador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbRepositor.Focus();
            }
        }

        private void cmbRepositor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAnalisar.Focus();
            }
        }

        //KeyDown
        private void txtFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmPesqFornecedor frameForn = new FrmPesqFornecedor();

                //Recebe as informações
                if (frameForn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Recebe os valores novos
                    txtCodFornecedor.Text = frameForn.codFornecedor;
                    txtNomeFornecedor.Text = frameForn.nmFornecedor;
                }
            }
        }

        //KeyUp
        private void gridAbastecimento_KeyUp(object sender, KeyEventArgs e)
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

        private void cmbCategoria_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbCategoria.Items.Count == 0)
            {
                //Preenche o combobox categoria
                PesqCategorias();
            }
        }

        private void cmbEmpilhador_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbEmpilhador.Items.Count == 0)
            {
                PesqEmpilhador(); //Pesquisa os empilhadores
            }
        }

        private void cmbRepositor_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbRepositor.Items.Count == 0)
            {
                PesqRepositor(); //Pesquisa os repositores
            }
        }

        private void gridAbastecimento_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Exibe os dados nos campos
            DadosCampos();
        }

        //CellContent - Controla o chekbox no gridview
        private void gridEstacao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Verifica se e somente se a celula checkbox (Todos) foi clicada
            if (e.ColumnIndex == gridEstacao.Columns[0].Index)
            {
                if (e.RowIndex >= 0 && Convert.ToBoolean(gridEstacao.Rows[e.RowIndex].Cells[0].Value) == false)
                {
                    gridEstacao.Rows[e.RowIndex].Cells[0].Value = "true";
                }
                else if (e.RowIndex >= 0 && Convert.ToBoolean(gridEstacao.Rows[e.RowIndex].Cells[0].Value) == true)
                {
                    gridEstacao.Rows[e.RowIndex].Cells[0].Value = "false";
                }
            }
        }

        //MouseDobleClick
        private void gridAbastecimento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                //Habilita
                cmbEmpilhador.Enabled = true;
                cmbRepositor.Enabled = true;
                gridItens.Enabled = true;
                gridPulmao.Enabled = true;
                //Exibe o botão de alterar
                btnAlterarOrdem.Visible = true;
            }

        }

        //MouseClick
        private void cmbRegiao_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbRegiao.Items.Count == 0) //Verifica se já houve pesquisa
            {
                PesqRegiao(); //Pesquisa a região caso não exista itens
            }
        }

        private void cmbCategoria_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbCategoria.Items.Count == 0)
            {
                //Preenche o combobox categoria
                PesqCategorias();
            }
        }

        private void cmbEmpilhador_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbEmpilhador.Items.Count == 0)
            {
                PesqEmpilhador(); //Pesquisa os empilhadores
            }
        }

        private void cmbRepositor_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmbRepositor.Items.Count == 0)
            {
                PesqRepositor(); //Pesquisa os repositores
            }
        }

        //Click
        private void btnNovo_Click(object sender, EventArgs e)
        {
            GerarCodigo();//Gera um novo código para a ordem de abastecimento
        }

        //Adiciona o manifesto no grid
        private void btnManifesto_Click(object sender, EventArgs e)
        {
            if (gridManifesto.Rows.Count == 0)
            {
                //Adiciona o manifesto no grid
                gridManifesto.Rows.Add(gridManifesto.Rows.Count + 1, txtManifesto.Text);

                //Limpa o campo texto do manifesto
                txtManifesto.Clear();
                txtManifesto.Focus();
            }
            else
            {
                //Verifica se o manifesto já foi digitado
                for (int i = 0; gridManifesto.Rows.Count > i; i++)
                {
                    if (gridManifesto.Rows[i].Cells[1].Value.Equals(txtManifesto.Text))
                    {
                        txtManifesto.Focus();
                        txtManifesto.SelectAll();
                        MessageBox.Show("Manifesto já digitado!", "Wms - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    else if (gridManifesto.Rows.Count - 1 == i)
                    {
                        //Adiciona o manifesto no grid
                        gridManifesto.Rows.Add(gridManifesto.Rows.Count + 1, txtManifesto.Text);

                        //Limpa o campo texto do manifesto
                        txtManifesto.Clear();
                        txtManifesto.Focus();
                        break;
                    }

                }
            }
        }

        private void btnAnalisar_Click(object sender, EventArgs e)
        {
            //Pesquisa os itens para o abastecimento
            PesqItens();
        }

        //Pesquisa as ordem de abastecimento
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa os abastecimentos
            PesqAbastecimento();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            //Abre a ordem de abastecimento
            GerarAbastecimento();
        }

        private void btnAlterarOrdem_Click(object sender, EventArgs e)
        {
            //ALtera os responsáveis da ordem
            AlterarAbastecimento();
        }

        private void mniImprimirOrdem_Click(object sender, EventArgs e)
        {
            //Gera o relatório
            Thread thread = new Thread(GerarRelatorio);
            thread.Start(); //inicializa
        }

        private void mniFlowRack_Click(object sender, EventArgs e)
        {
            //Gera o relatório
            Thread thread = new Thread(GerarRelatorioFlowRack);
            thread.Start(); //inicializa
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o frame
            Close();
        }

        //Métodos
        private void PesqEstacao() //Pesquisa as estações
        {
            try
            {
                gridEstacao.Rows.Clear();  //Limpa o grid
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios(); //Instância o objêto               
                EstacaoCollection estacaoCollection = new EstacaoCollection();  //Instância a coleção                
                estacaoCollection = abastecimentoNegocios.PesqEstacao(); //A coleção recebe o resultado da consulta   

                //Grid Recebe o resultado da coleção
                estacaoCollection.ForEach(n => gridEstacao.Rows.Add(false, n.codEstacao, n.descEstacao, n.tipo.ToUpper()));

                if (estacaoCollection.Count == 0)
                {
                    MessageBox.Show("Nenhuma estação encontrada! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                //Limpa o combobox rua inícial
                cmbRua.Items.Clear();
                //Adiciona o texto
                cmbRua.Text = "Selec...";

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

        private void PesqCategorias() //Pesquisa as categorias cadastradas
        {
            try
            {
                //Instância a coleção
                CategoriaCollection categoriaCollection = new CategoriaCollection();
                //Instância o negocios
                CategoriaNegocios categoriaNegocios = new CategoriaNegocios();
                //Limpa o combobox de cadastro
                cmbCategoria.Items.Clear();
                //Limpa o combobox de pesquisa
                cmbCategoria.Items.Clear();

                //Preenche a coleção com a pesquisa
                categoriaCollection = categoriaNegocios.PesqCategoria();
                //Preenche o combobox categoria de cadastro
                categoriaCollection.ForEach(n => cmbCategoria.Items.Add(n.descCategoria));
                //Define o tamanho do array para o combobox
                categoria = new int[categoriaCollection.Count];

                for (int i = 0; i < categoriaCollection.Count; i++)
                {
                    //Preenche o array combobox
                    categoria[i] = categoriaCollection[i].codCategoria;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o usuário que  tem o perfíl de empilhador e repositor
        private void PesqEmpilhador()
        {
            try
            {
                //Instância o objeto
                UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                //Instância a coleção
                UsuarioCollection usuarioCollection = new UsuarioCollection();
                //A coleção recebe o resultado da consulta
                usuarioCollection = usuarioNegocios.PesqUsuario(null);
                //Localizando os empilhadores e ordena por turno
                List<Usuario> usuarioEmpilhador = usuarioCollection.FindAll(delegate (Usuario n) { return n.perfil == "EMPILHADOR"; }).OrderBy(c => c.turno).ToList();

                //Preenche o combobox região
                usuarioEmpilhador.ForEach(n => cmbEmpilhador.Items.Add(n.login));
                //Define o tamanho do array para o combobox
                empilhador = new int[usuarioEmpilhador.Count];

                for (int i = 0; i < usuarioEmpilhador.Count; i++)
                {
                    //Preenche o array combobox
                    empilhador[i] = usuarioEmpilhador[i].codUsuario;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o usuário que  tem o perfíl repositor
        private void PesqRepositor()
        {
            try
            {
                //Instância o objeto
                UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                //Instância a coleção
                UsuarioCollection usuarioCollection = new UsuarioCollection();
                //A coleção recebe o resultado da consulta
                usuarioCollection = usuarioNegocios.PesqUsuario(null);
                //Localizando os repositores e ordena por login
                List<Usuario> usuarioRepositor = usuarioCollection.FindAll(delegate (Usuario n) { return n.perfil == "REPOSITOR"; }).OrderBy(c => c.login).ToList();

                //Preenche o combobox região
                usuarioRepositor.ForEach(n => cmbRepositor.Items.Add(n.login));
                //Define o tamanho do array para o combobox
                repositor = new int[usuarioRepositor.Count];

                for (int i = 0; i < usuarioRepositor.Count; i++)
                {
                    //Preenche o array combobox
                    repositor[i] = usuarioRepositor[i].codUsuario;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o fornecedor
        private void PesqFornecedor()
        {
            try
            {
                if (!txtCodFornecedor.Text.Equals(""))
                {
                    //Instância o negocios
                    FornecedorNegocios pesqFornecedorNegocios = new FornecedorNegocios();
                    //Instância a coleçãO
                    FornecedorCollection fornecedorCollection = new FornecedorCollection();
                    //A coleção recebe o resultado da consulta
                    fornecedorCollection = pesqFornecedorNegocios.pesqFornecedor(cmbEmpresa.Text, "", Convert.ToInt32(txtCodFornecedor.Text));

                    if (fornecedorCollection.Count > 0)
                    {
                        //Limpa o nome do fornecedor
                        txtNomeFornecedor.Clear();
                        //Seta o nome do fornecedor
                        txtNomeFornecedor.Text = fornecedorCollection[0].nomeFornecedor;

                        //Foca no campo empihador
                        cmbEmpilhador.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum fornecedor encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //Foca no campo empihador
                    cmbEmpilhador.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Pesquisa os abastecimentos
        private void PesqAbastecimento()
        {
            try
            {
                //Instância a camada de negocios
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();
                //Instância a coleção de objêto
                AbastecimentoCollection abastecimentoCollection = new AbastecimentoCollection();
                //A coleção recebe o resultado da consulta
                abastecimentoCollection = abastecimentoNegocios.PesqAbastecimento(txtPesqCodigo.Text, cmbPesqModo.Text, cmbPesqTipo.Text, cmbPesqStatus.Text.ToUpper(), dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), cmbEmpresa.Text);
                //Limpa o grid
                gridAbastecimento.Rows.Clear();
                //Grid Recebe o resultado da coleção
                abastecimentoCollection.ForEach(n => gridAbastecimento.Rows.Add(n.dataAbertura, n.codAbastecimento, n.modoAbastecimento, n.tipoAbastecimento, n.statusAbastecimento, n.loginResponsavel,
                    n.numeroRegiao, n.numeroRua, n.ladoRua, n.descCategoria, n.codFornecedor, n.nomeFornecedor, n.loginEmpilhador, n.loginRepositor, n.codManifesto));


                if (abastecimentoCollection.Count > 0)
                {
                    //Qtd de ordens encontrada
                    lblQtdOrdem.Text = Convert.ToString(gridAbastecimento.Rows.Count);

                    //Seleciona a primeira linha do grid
                    gridAbastecimento.CurrentCell = gridAbastecimento.Rows[0].Cells[1];
                    //Foca no grid
                    gridAbastecimento.Focus();

                    //Pesquisa os itens do abastecimento
                    PesqItensAbastecimento();

                    //Seta os dados nos campos
                    DadosCampos();

                }
                else
                {
                    //Limpa os campos
                    LimparCampos();
                    MessageBox.Show("Nenhum abastecimento encontrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa os itens abastecimentos
        private void PesqItensAbastecimento()
        {
            try
            {
                //Instância a camada de negocios
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();
                //Limpa o coleção
                itensCollection.Clear();
                //A coleção recebe o resultado da consulta
                itensCollection = abastecimentoNegocios.PesqItensAbastecimento(txtPesqCodigo.Text, cmbPesqModo.Text, cmbPesqTipo.Text, cmbPesqStatus.Text.ToUpper(), dtmInicial.Value.ToShortDateString(), dtmFinal.Value.ToShortDateString(), cmbEmpresa.Text);

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
                if (gridAbastecimento.Rows.Count > 0)
                {
                    //Limpa os grid
                    gridManifesto.Rows.Clear();

                    //Instância as linha da tabela
                    DataGridViewRow linha = gridAbastecimento.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = Convert.ToString(gridAbastecimento.Rows[indice].Cells[1].Value);
                    //Seta o modo
                    cmbModo.Text = Convert.ToString(gridAbastecimento.Rows[indice].Cells[2].Value);
                    //Seta o tipo
                    cmbTipo.Text = Convert.ToString(gridAbastecimento.Rows[indice].Cells[3].Value);
                    //Seta o região
                    cmbRegiao.Text = Convert.ToString(gridAbastecimento.Rows[indice].Cells[6].Value);
                    //Seta o rua
                    cmbRua.Text = Convert.ToString(gridAbastecimento.Rows[indice].Cells[7].Value);
                    //Seta o lado
                    cmbLado.Text = Convert.ToString(gridAbastecimento.Rows[indice].Cells[8].Value);
                    //Seta o lado
                    cmbCategoria.Text = Convert.ToString(gridAbastecimento.Rows[indice].Cells[9].Value);
                    //Seta o código fornecedor
                    txtCodFornecedor.Text = Convert.ToString(gridAbastecimento.Rows[indice].Cells[10].Value);
                    //Seta o nome do fornecedor
                    txtNomeFornecedor.Text = Convert.ToString(gridAbastecimento.Rows[indice].Cells[11].Value);
                    //Seta o login do empilhador
                    cmbEmpilhador.Text = Convert.ToString(gridAbastecimento.Rows[indice].Cells[12].Value);
                    //Seta o login do repositor
                    cmbRepositor.Text = Convert.ToString(gridAbastecimento.Rows[indice].Cells[13].Value);
                    //Seta os códigos do manifestos
                    string codManifesto = Convert.ToString(gridAbastecimento.Rows[indice].Cells[14].Value);

                    //Separa os códigos dos manifestos por espaço
                    var linhas = codManifesto.Split(' ');
                    //Exibe no grid o códigos do manifesto
                    for (int i = 0; linhas.Count() > i; i++)
                    {
                        if (!linhas[i].Equals(""))
                        {
                            gridManifesto.Rows.Add(gridManifesto.Rows.Count + 1, linhas[i]);
                        }
                    }

                    //Padroniza as informações
                    if (Convert.ToString(gridAbastecimento.Rows[indice].Cells[3].Value).Equals(""))
                    {
                        cmbTipo.Text = "Selecione";
                    }

                    // Padroniza as informações
                    if (Convert.ToString(gridAbastecimento.Rows[indice].Cells[6].Value).Equals("0"))
                    {
                        cmbRegiao.Text = "Selec...";
                    }

                    if (Convert.ToString(gridAbastecimento.Rows[indice].Cells[7].Value).Equals("0"))
                    {
                        cmbRua.Text = "Selec...";
                    }

                    if (Convert.ToString(gridAbastecimento.Rows[indice].Cells[8].Value).Equals(""))
                    {
                        cmbLado.Text = "Todos";
                    }

                    if (Convert.ToString(gridAbastecimento.Rows[indice].Cells[12].Value).Equals(""))
                    {
                        cmbEmpilhador.Text = "Selecione";
                    }

                    if (Convert.ToString(gridAbastecimento.Rows[indice].Cells[13].Value).Equals(""))
                    {
                        cmbRepositor.Text = "Selecione";
                    }

                    //Exibe os itens
                    if (itensCollection.Count > 0)
                    {
                        //Limpa os grids
                        gridItens.Rows.Clear();
                        gridPulmao.Rows.Clear();
                        gridObservacao.Rows.Clear();

                        //Localizando os empilhadores e ordena por turno
                        List<ItensAbastecimento> itensAbastecimentoCollection = itensCollection.FindAll(delegate (ItensAbastecimento n) { return n.codAbastecimento == Convert.ToInt32(gridAbastecimento.Rows[indice].Cells[1].Value); });

                        //Exibe os dados nos grid itens e pulmão
                        for (int i = 0; itensAbastecimentoCollection.Count > i; i++)
                        {
                            if (itensAbastecimentoCollection[i].tipoAnalise.Equals("ANÁLISE DE PICKING"))
                            {
                                //grid Recebe o resultado da coleção de produto para o abastecimento
                                gridItens.Rows.Add(gridItens.Rows.Count + 1,
                                        itensAbastecimentoCollection[i].codEstacao,
                                        itensAbastecimentoCollection[i].descEstacao,
                                        itensAbastecimentoCollection[i].codApartamentoPicking,
                                        itensAbastecimentoCollection[i].enderecoPicking,
                                        itensAbastecimentoCollection[i].idProduto,
                                        itensAbastecimentoCollection[i].codProduto + " - " + itensAbastecimentoCollection[i].descProduto,
                                        itensAbastecimentoCollection[i].qtdCaixaProduto,
                                        itensAbastecimentoCollection[i].capacidadePicking,
                                        itensAbastecimentoCollection[i].abastecimentoPicking,
                                        itensAbastecimentoCollection[i].qtdAbastecer,
                                        itensAbastecimentoCollection[i].unidadePulmao,
                                        itensAbastecimentoCollection[i].qtdPicking,
                                        itensAbastecimentoCollection[i].unidadePicking,
                                        itensAbastecimentoCollection[i].observacao,
                                        itensAbastecimentoCollection[i].codApartamentoPulmao,
                                        itensAbastecimentoCollection[i].enderecoPulmao,
                                        itensAbastecimentoCollection[i].qtdPulmao,
                                        itensAbastecimentoCollection[i].unidadePulmao,
                                        itensAbastecimentoCollection[i].vencimentoPulmao,
                                        itensAbastecimentoCollection[i].lotePulmao);
                            }
                            else if (itensAbastecimentoCollection[i].tipoAnalise.Equals("ANÁLISE DO PULMÃO"))
                            {
                                //grid Recebe o resultado da coleção de produto para o abastecimento
                                gridPulmao.Rows.Add(gridPulmao.Rows.Count + 1,
                                        itensAbastecimentoCollection[i].codEstacao,
                                        itensAbastecimentoCollection[i].descEstacao,
                                        itensAbastecimentoCollection[i].codApartamentoPicking,
                                        itensAbastecimentoCollection[i].enderecoPicking,
                                        itensAbastecimentoCollection[i].idProduto,
                                        itensAbastecimentoCollection[i].codProduto + " - " + itensAbastecimentoCollection[i].descProduto,
                                        itensAbastecimentoCollection[i].qtdCaixaProduto,
                                        itensAbastecimentoCollection[i].capacidadePicking,
                                        itensAbastecimentoCollection[i].abastecimentoPicking,
                                        itensAbastecimentoCollection[i].qtdAbastecer,
                                        itensAbastecimentoCollection[i].unidadePulmao,
                                        itensAbastecimentoCollection[i].qtdPicking,
                                        itensAbastecimentoCollection[i].unidadePicking,
                                        itensAbastecimentoCollection[i].observacao,
                                        itensAbastecimentoCollection[i].codApartamentoPulmao,
                                        itensAbastecimentoCollection[i].enderecoPulmao,
                                        itensAbastecimentoCollection[i].qtdPulmao,
                                        itensAbastecimentoCollection[i].unidadePulmao,
                                        itensAbastecimentoCollection[i].vencimentoPulmao,
                                        itensAbastecimentoCollection[i].lotePulmao);
                            }
                        }

                        //Desmarca todas as estações
                        for (int ii = 0; gridEstacao.Rows.Count > ii; ii++)
                        {
                            gridEstacao.Rows[ii].Cells[0].Value = false;
                        }

                        //Recebe a descrição da estação
                        string estacao = "descricao";

                        //Informa as estações da ordem
                        for (int i = 0; gridItens.Rows.Count > i; i++)
                        {
                            if (!estacao.Equals(Convert.ToString(gridItens.Rows[i].Cells[2].Value)))
                            {
                                estacao = Convert.ToString(gridItens.Rows[i].Cells[2].Value);

                                for (int ii = 0; gridEstacao.Rows.Count > ii; ii++)
                                {
                                    if (Convert.ToString(gridEstacao.Rows[ii].Cells[2].Value).Equals(estacao))
                                    {
                                        gridEstacao.Rows[ii].Cells[0].Value = true;
                                    }
                                }
                            }
                        }

                        //Qtd de produto analisados
                        lblQtdAnalise.Text = "0";
                        //Exibe a quantidade de observações
                        lblQtdObs.Text = gridObservacao.Rows.Count.ToString();
                        //Exibe a quantidade de endereços do picking 
                        lblQtdPicking.Text = gridItens.Rows.Count.ToString();
                        //Exibe a quantidade de endereços do pulmão 
                        lblQtdPulmao.Text = gridPulmao.Rows.Count.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Nenhum item encontrado para o abastecimento selecionado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //Desabilita todos os campos
                    DesabilitarCampos();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! \n" + ex, "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        //Gera um novo código para o abastecimento
        private void GerarCodigo()
        {
            try
            {
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();  //Instância a camada de negócios
                LimparCampos(); //Limpa os campos
                txtCodigo.Text = abastecimentoNegocios.PesqCodigo().ToString(); //Seta o novo id                
                HabilitarCampos(); //Habilita componentes
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //Analisa os itens para o abastecimento
        private void PesqItens()
        {
            try
            {

                if (cmbModo.Text.Equals("Selecione") || cmbTipo.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, selecione o modo de abastecimento!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbModo.Text.Equals("Corretivo") && cmbTipo.Text.Equals("Selecione") || cmbTipo.Text.Equals(""))
                {
                    MessageBox.Show("Por favor, selecione o tipo de abastecimento!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbTipo.Text.Equals("Caixa") && cmbRegiao.Text.Equals(string.Empty))
                {
                    MessageBox.Show("Selecione uma região para análise!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbTipo.Text.Equals("Caixa") && cmbRua.Text.Equals("Selec...") && cmbCategoria.Text.Equals(string.Empty) && txtCodFornecedor.Text.Equals(string.Empty) ||
                         cmbTipo.Text.Equals("Caixa") && cmbRua.Text.Equals(string.Empty) && cmbCategoria.Text.Equals(string.Empty) && txtCodFornecedor.Text.Equals(string.Empty))
                {
                    MessageBox.Show("Selecione uma opção: Rua, Categoria ou Fornecedor para análise!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (cmbModo.Text.Equals("Preventivo"))
                    {
                        //Analisa o abastecimento de picking de grandeza por manifesto
                        Thread thread = new Thread(AnalisaAbastecimentoManifesto);
                        thread.Start();
                    }
                    if (cmbTipo.Text.Equals("Caixa")) //Tipo Caixa
                    {
                        //Analisa o abastecimento de picking de grandeza
                        Thread thread = new Thread(AnalisaAbastecimentoCaixa);
                        thread.Start();
                    }
                    else if (cmbTipo.Text.Equals("FlowRack")) //Tipo Flowrack
                    {
                        //Analisa o abastecimento de picking do flowrack
                        Thread thread = new Thread(AnalisarAbastecimentoFlowRack);
                        thread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Analisar abastecimento no picking de caixa
        private void AnalisaAbastecimentoManifesto()
        {
            try
            {
                if (gridManifesto.Rows.Count == 0)
                {
                    MessageBox.Show("Por favor, digite o manifesto! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    Invoke((MethodInvoker)delegate ()
                    {
                        //Limpa o grid de itens
                        gridItens.Rows.Clear();
                        //Limpa o grid de itens
                        gridPulmao.Rows.Clear();
                        //Limpa o grid observação
                        gridObservacao.Rows.Clear();
                        //Seleciona a primeira página
                        tabControl1.SelectedTab = tabPage1;
                    });

                    //Instância a camada de negócios
                    AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();
                    //Instância a camada de objêto - coleção 
                    ItensAbastecimentoCollection itensCollection = new ItensAbastecimentoCollection();
                    //Instância a camada de objêto - coleção 
                    ItensAbastecimentoCollection itensAbastecimentoCollection = new ItensAbastecimentoCollection();
                    //Instância a camada de objêto - coleção 
                    ItensAbastecimentoCollection itensObservacaoCollection = new ItensAbastecimentoCollection();

                    Invoke((MethodInvoker)delegate ()
                    {

                        //Array responsável pelos códigos das estações
                        int[] codigoManifesto = new int[gridManifesto.Rows.Count]; //Define o tamanho do array


                        //Verifica as rotas selecionadas
                        if (gridManifesto.Rows.Count > 0)
                        {
                            //Verifica as rotas selecionadas
                            for (int i = 0; gridManifesto.Rows.Count > i; i++)
                            {
                                codigoManifesto[i] = Convert.ToInt32(gridManifesto.Rows[i].Cells[1].Value); //Passa o código da Manifesto
                            }
                        }

                        //Pesquisa o id dos Produtos que precisam de abastecimento por manifesto
                        itensCollection = abastecimentoNegocios.PesqItensManifesto(codigoManifesto, cmbEmpresa.Text); //A coleção recebe o resultado da consulta 
                    });

                    //Passa a quantidade de itens
                    Progressbar(itensCollection.Count);

                    //Se não existiritem para o abastecimento
                    if (itensCollection.Count == 0)
                    {
                        MessageBox.Show("Nenhum produto encontrado para análise! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        for (int i = 0; itensCollection.Count > i; i++)
                        {
                            MsgLabel("Analisando: " + itensCollection[i].codProduto + " - " + itensCollection[i].descProduto);

                            //Separa os itens com problemas dos que necessitam para abastecer
                            if (itensCollection[i].observacao != null)
                            {
                                Invoke((MethodInvoker)delegate ()
                                {
                                    //Grid Recebe o resultado da coleção
                                    gridObservacao.Rows.Add(gridObservacao.Rows.Count + 1, itensCollection[i].descEstacao, itensCollection[i].enderecoPicking,
                                        itensCollection[i].codProduto + " - " + itensCollection[i].descProduto, itensCollection[i].qtdAbastecer, itensCollection[i].unidadePulmao,
                                        itensCollection[i].qtdPicking, itensCollection[i].unidadePicking, itensCollection[i].observacao);

                                    //Exibe a quantidade de observações
                                    lblQtdObs.Text = gridObservacao.Rows.Count.ToString();

                                });
                            }
                            else
                            {
                                //Controla a quantidade de abastecimento
                                int? qtdEncontrada = 0;

                                for (int ii = 0; itensCollection[i].qtdAbastecer > qtdEncontrada; ii++)
                                {
                                    //Instância a camada de objêto - coleção
                                    ItensAbastecimento itensPulmao = new ItensAbastecimento();

             

                                    //Pesquisa o abastecimento no pulmão, passando o id do produto
                                    itensPulmao = abastecimentoNegocios.PesqAbastecimentoPulmao(itensCollection[i].idProduto, ii, cmbEmpresa.Text); //ii - Quantidade de linha a pular no select


                                    if (itensPulmao.idProduto == 0)
                                    {
                                        //Altera a menssagem 
                                        itensCollection[i].observacao = "NÃO EXISTE ESTOQUE NO PULMÃO";
                                        //Adiciona o produto a coleção com problemas ou observações
                                        itensObservacaoCollection.Add(itensCollection[i]);
                                        //Pula para o próximo produto
                                        break;
                                    }
                                    else
                                    {

                                        //Complementa as informações do pulmão para adicionar no grid
                                        //itensPulmao.codManifesto = itensCollection[i].codManifesto;
                                        itensPulmao.codEstacao = itensCollection[i].codEstacao;
                                        itensPulmao.descEstacao = itensCollection[i].descEstacao;
                                        itensPulmao.codApartamentoPicking = itensCollection[i].codApartamentoPicking;
                                        itensPulmao.enderecoPicking = itensCollection[i].enderecoPicking;
                                        itensPulmao.idProduto = itensCollection[i].idProduto;
                                        itensPulmao.codProduto = itensCollection[i].codProduto;
                                        itensPulmao.descProduto = itensCollection[i].descProduto;
                                        itensPulmao.qtdCaixaProduto = itensCollection[i].qtdCaixaProduto;
                                        itensPulmao.capacidadePicking = itensCollection[i].capacidadePicking;
                                        itensPulmao.abastecimentoPicking = itensCollection[i].abastecimentoPicking;
                                        itensPulmao.qtdAbastecer = itensCollection[i].qtdAbastecer;
                                        itensPulmao.unidadePulmao = itensCollection[i].unidadePulmao;
                                        itensPulmao.qtdPicking = itensCollection[i].qtdPicking;

                                        if (Convert.ToInt32(itensCollection[i].qtdAbastecer) <= itensPulmao.qtdPulmao)
                                        {
                                            //Se sim recebe a Quantidade necessária
                                            itensPulmao.qtdPulmao = Convert.ToInt32(itensCollection[i].qtdAbastecer);
                                        }

                                        //Adiciona o objêto a coleção
                                        itensAbastecimentoCollection.Add(itensPulmao);

                                        //Verifica se há necessidade de continuar a pesquisa
                                        if (itensCollection[i].qtdAbastecer <= itensPulmao.qtdPulmao)
                                        {
                                            //Pula para o próximo produto
                                            break;
                                        }

                                        //Diminui a quantidade de abastecimento
                                        itensCollection[i].qtdAbastecer = Convert.ToInt32(itensCollection[i].qtdAbastecer - itensPulmao.qtdPulmao);
                                    }
                                }
                            }

                            //Incrementa o progressbar
                            IncrementarProgressBar();
                        }

                        Invoke((MethodInvoker)delegate ()
                        {
                            //grid Recebe o resultado da coleção de produto para o abastecimento
                            itensAbastecimentoCollection.ForEach(n => gridPulmao.Rows.Add(gridPulmao.Rows.Count + 1, n.codEstacao, n.descEstacao, n.codApartamentoPicking, n.enderecoPicking, n.idProduto,
                                    n.codProduto + " - " + n.descProduto, n.qtdCaixaProduto, n.capacidadePicking, n.abastecimentoPicking, n.qtdAbastecer, n.unidadePulmao, n.qtdPicking, n.unidadePicking,
                                    n.observacao, n.codApartamentoPulmao, n.enderecoPulmao, n.qtdPulmao, n.unidadePulmao, n.vencimentoPulmao, n.lotePulmao));

                            //grid Recebe o resultado da coleção de produtos com observação
                            itensObservacaoCollection.ForEach(n => gridObservacao.Rows.Add(gridObservacao.Rows.Count + 1, n.descEstacao, n.enderecoPicking,
                                    n.codProduto + " - " + n.descProduto, n.qtdAbastecer, n.unidadePulmao, n.qtdPicking, n.unidadePicking, n.observacao));

                            //Exibe a quantidade de observações
                            lblQtdObs.Text = gridObservacao.Rows.Count.ToString();

                            //Exibe a quantidade de endereços do pulmão 
                            lblQtdPulmao.Text = gridPulmao.Rows.Count.ToString();

                            //Foca na página de análise de pulmão
                            tabControl1.SelectedTab = tabPage3;

                            //Exibe cores de alerta
                            CoresGridObservacao();

                        });

                        if (itensAbastecimentoCollection.Count == 0)
                        {
                            MessageBox.Show("Não existe produto para gerar o abastecimento! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //Analisar abastecimento no picking de caixa
        private void AnalisaAbastecimentoCaixa()
        {
            try
            {
                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    //Limpa o grid de itens
                    gridItens.Rows.Clear();
                    //Limpa o grid de itens
                    gridPulmao.Rows.Clear();
                    //Limpa o grid observação
                    gridObservacao.Rows.Clear();
                    //Seleciona a primeira página
                    tabControl1.SelectedTab = tabPage1;
                });


                //Instância a camada de negócios
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();
                //Instância a camada de objêto - coleção 
                ItensAbastecimentoCollection itensCollection = new ItensAbastecimentoCollection();
                //Instância a camada de objêto - coleção 
                ItensAbastecimentoCollection itensAbastecimentoCollection = new ItensAbastecimentoCollection();
                //Instância a camada de objêto - coleção 
                ItensAbastecimentoCollection itensObservacaoCollection = new ItensAbastecimentoCollection();

                Invoke((MethodInvoker)delegate ()
                {
                    //Pesquisa o id dos Produtos que precisam de abastecimento
                    itensCollection = abastecimentoNegocios.PesqItens(cmbEmpresa.Text, cmbTipo.Text.ToUpper(), cmbRegiao.Text, cmbRua.Text, cmbLado.Text, cmbCategoria.Text, txtCodFornecedor.Text, null); //A coleção recebe o resultado da consulta 
                });

                //Passa a quantidade de itens
                Progressbar(itensCollection.Count);

                //Se não existiritem para o abastecimento
                if (itensCollection.Count == 0)
                {
                    MessageBox.Show("Nenhum produto encontrado para análise! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    for (int i = 0; itensCollection.Count > i; i++)
                    {
                        //Exibe os produtos em análise
                        MsgLabel("Analisando: " + itensCollection[i].codProduto + " - " + itensCollection[i].descProduto);

                        //Separa os itens com problemas dos que necessitam para abastecer
                        if (itensCollection[i].observacao != null)
                        {
                            //Garante que seja executado pela thread
                            Invoke((MethodInvoker)delegate ()
                            {
                                //Grid Recebe o resultado da coleção
                                gridObservacao.Rows.Add(gridObservacao.Rows.Count + 1, itensCollection[i].descEstacao, itensCollection[i].enderecoPicking,
                                itensCollection[i].codProduto + " - " + itensCollection[i].descProduto, itensCollection[i].qtdAbastecer, itensCollection[i].unidadePulmao,
                                itensCollection[i].qtdPicking, itensCollection[i].unidadePicking, itensCollection[i].observacao);

                                //Exibe a quantidade de observações
                                lblQtdObs.Text = gridObservacao.Rows.Count.ToString();

                            });
                        }
                        else
                        {
                            //Controla a quantidade de abastecimento
                            int? qtdEncontrada = 0;

                            for (int ii = 0; itensCollection[i].qtdAbastecer > qtdEncontrada; ii++)
                            {
                                //Instância a camada de objêto - coleção
                                ItensAbastecimento itensPulmao = new ItensAbastecimento();

                                Invoke((MethodInvoker)delegate ()
                                {
                                    //Pesquisa o abastecimento no pulmão, passando o id do produto
                                    itensPulmao = abastecimentoNegocios.PesqAbastecimentoPulmao(itensCollection[i].idProduto, ii, cmbEmpresa.Text); //ii - Quantidade de linha a pular no select
                                });

                                if (itensPulmao.idProduto == 0)
                                {
                                    //Altera a menssagem 
                                    itensCollection[i].observacao = "NÃO EXISTE ESTOQUE NO PULMÃO";
                                    //Adiciona o produto a coleção com problemas ou observações
                                    itensObservacaoCollection.Add(itensCollection[i]);
                                    //Pula para o próximo produto
                                    break;
                                }
                                else
                                {

                                    //Complementa as informações do pulmão para adicionar no grid
                                    itensPulmao.codEstacao = itensCollection[i].codEstacao;
                                    itensPulmao.descEstacao = itensCollection[i].descEstacao;
                                    itensPulmao.codApartamentoPicking = itensCollection[i].codApartamentoPicking;
                                    itensPulmao.enderecoPicking = itensCollection[i].enderecoPicking;
                                    itensPulmao.idProduto = itensCollection[i].idProduto;
                                    itensPulmao.codProduto = itensCollection[i].codProduto;
                                    itensPulmao.descProduto = itensCollection[i].descProduto;
                                    itensPulmao.qtdCaixaProduto = itensCollection[i].qtdCaixaProduto;
                                    itensPulmao.capacidadePicking = itensCollection[i].capacidadePicking;
                                    itensPulmao.abastecimentoPicking = itensCollection[i].abastecimentoPicking;
                                    itensPulmao.qtdAbastecer = itensCollection[i].qtdAbastecer;
                                    itensPulmao.unidadePulmao = itensCollection[i].unidadePulmao;
                                    itensPulmao.qtdPicking = itensCollection[i].qtdPicking;
                                    itensPulmao.unidadePicking = itensCollection[i].unidadePicking;

                                    if (Convert.ToInt32(itensCollection[i].qtdAbastecer) <= itensPulmao.qtdPulmao)
                                    {
                                        //Se sim recebe a Quantidade necessária
                                        itensPulmao.qtdPulmao = Convert.ToInt32(itensCollection[i].qtdAbastecer);
                                    }

                                    //Adiciona o objêto a coleção
                                    itensAbastecimentoCollection.Add(itensPulmao);

                                    //Verifica se há necessidade de continuar a pesquisa
                                    if (itensCollection[i].qtdAbastecer <= itensPulmao.qtdPulmao)
                                    {
                                        //Pula para o próximo produto
                                        break;
                                    }

                                    //Diminui a quantidade de abastecimento
                                    itensCollection[i].qtdAbastecer = Convert.ToInt32(itensCollection[i].qtdAbastecer - itensPulmao.qtdPulmao);
                                }
                            }


                        }

                        //Incrementa o progressbar
                        IncrementarProgressBar();
                    }

                    //Garante que seja executado pela thread
                    Invoke((MethodInvoker)delegate ()
                    {
                        //grid Recebe o resultado da coleção de produto para o abastecimento
                        itensAbastecimentoCollection.ForEach(n => gridPulmao.Rows.Add(gridPulmao.Rows.Count + 1, n.codEstacao, n.descEstacao, n.codApartamentoPicking, n.enderecoPicking, n.idProduto,
                            n.codProduto + " - " + n.descProduto, n.qtdCaixaProduto, n.capacidadePicking, n.abastecimentoPicking, n.qtdAbastecer, n.unidadePulmao, n.qtdPicking, n.unidadePicking,
                            n.observacao, n.codApartamentoPulmao, n.enderecoPulmao, n.qtdPulmao, n.unidadePulmao, n.vencimentoPulmao, n.lotePulmao));

                        //grid Recebe o resultado da coleção de produtos com observação
                        itensObservacaoCollection.ForEach(n => gridObservacao.Rows.Add(gridObservacao.Rows.Count + 1, n.descEstacao, n.enderecoPicking,
                            n.codProduto + " - " + n.descProduto, n.qtdAbastecer, n.unidadePulmao, n.qtdPicking, n.unidadePicking, n.observacao));

                        //Exibe a quantidade de observações
                        lblQtdObs.Text = gridObservacao.Rows.Count.ToString();

                        //Exibe a quantidade de endereços do pulmão 
                        lblQtdPulmao.Text = gridPulmao.Rows.Count.ToString();

                        //Foca na página de análise de picking
                        tabControl1.SelectedTab = tabPage3;

                        //Exibe cores de alerta
                        CoresGridObservacao();

                    });

                    //Se não existiritem para o abastecimento
                    if (itensAbastecimentoCollection.Count == 0)
                    {
                        MessageBox.Show("Não existe produto para gerar o abastecimento! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //Analisar abastecimento
        private void AnalisarAbastecimentoFlowRack()
         {
            try
            {
                //Array responsável pelos códigos das estações
                int[] codigoEstacao = new int[gridEstacao.Rows.Count]; //Define o tamanho do array

                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    //Verifica se existe estação
                    if (gridEstacao.Rows.Count > 0)
                    {
                        //Percorre todas as estações existente no grid
                        for (int i = 0; gridEstacao.Rows.Count > i; i++)
                        {
                            //Verifica as estações selecionadas
                            if (Convert.ToBoolean(gridEstacao.Rows[i].Cells[0].Value) == true)
                            {
                                //Passa o código da estação para o array
                                codigoEstacao[i] = Convert.ToInt32(gridEstacao.Rows[i].Cells[1].Value); 
                            }
                        }
                    }
                });

                //Verifica se existe estação selecionada
                if (codigoEstacao.Length == 0)
                {
                    MessageBox.Show("Por favor, selecione uma estação! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    //Garante que seja executado pela thread
                    Invoke((MethodInvoker)delegate ()
                    {
                        //Limpa o grid de itens
                        gridItens.Rows.Clear();
                        //Limpa o grid de itens
                        gridPulmao.Rows.Clear();
                        //Limpa o grid observação
                        gridObservacao.Rows.Clear();
                        //Seleciona a primeira página
                        tabControl1.SelectedTab = tabPage1;
                    });

                    //Instância a camada de negócios
                    AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();
                    //Instância a camada de objêto - coleção 
                    ItensAbastecimentoCollection itensCollection = new ItensAbastecimentoCollection();
                    //Instância a camada de objêto - coleção 
                    ItensAbastecimentoCollection itensAbastecimentoCollection = new ItensAbastecimentoCollection();
                    //Instância a camada de objêto - coleção 
                    ItensAbastecimentoCollection itensAbastecimentoPulmaoCollection = new ItensAbastecimentoCollection();
                    //Instância a camada de objêto - coleção 
                    ItensAbastecimentoCollection itensObservacaoCollection = new ItensAbastecimentoCollection();

                    //Garante que seja executado pela thread
                    Invoke((MethodInvoker)delegate ()
                    {
                        //Pesquisa o id dos Produtos que precisam de abastecimento
                        itensCollection = abastecimentoNegocios.PesqItens(cmbEmpresa.Text, cmbTipo.Text.ToUpper(), cmbRegiao.Text, cmbRua.Text, cmbLado.Text, cmbCategoria.Text, txtCodFornecedor.Text, codigoEstacao); //A coleção recebe o resultado da consulta 
                    });

                    //Passa a quantidade de itens
                    Progressbar(itensCollection.Count);

                    //Verifica se existe item para o abastecimento
                    if (itensCollection.Count == 0)
                    {
                        MessageBox.Show("Nenhum produto encontrado para análise! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Percorrer todos os itens
                        for (int i = 0; itensCollection.Count > i; i++)
                        {
                            //Exibe os produtos em análise
                            MsgLabel("Analisando: " + itensCollection[i].codProduto + " - " + itensCollection[i].descProduto);

                            //Separa os itens com problemas dos que necessitam para abastecer
                            if (itensCollection[i].observacao != null)
                            {
                                //Garante que seja executado pela thread
                                Invoke((MethodInvoker)delegate ()
                                {
                                    //Grid Recebe o resultado da coleção
                                    gridObservacao.Rows.Add(gridObservacao.Rows.Count + 1, itensCollection[i].descEstacao, itensCollection[i].enderecoPicking,
                                    itensCollection[i].codProduto + " - " + itensCollection[i].descProduto, itensCollection[i].qtdAbastecer, itensCollection[i].unidadePulmao,
                                    itensCollection[i].qtdPicking, itensCollection[i].unidadePicking, itensCollection[i].observacao);

                                    //Exibe a quantidade de observações
                                    lblQtdObs.Text = gridObservacao.Rows.Count.ToString();
                                });
                            }
                            else
                            {
                                //Controla a quantidade de abastecimento
                                int? qtdEncontrada = 0;
                                //Garante que seja executado pela thread
                                Invoke((MethodInvoker)delegate ()
                                {
                                    //Pesquisa o abastecimento no picking de grandeza
                                    for (int ii = 0; itensCollection[i].qtdAbastecer > qtdEncontrada; ii++)
                                    {
                                        //Instância a camada de objêto - coleção
                                        ItensAbastecimento itensPicking = new ItensAbastecimento();

                                        //Pesquisa o abastecimento no picking de caixa, passando o id do produto
                                        itensPicking = abastecimentoNegocios.PesquisaAbstecimentoPicking(itensCollection[i].idProduto, cmbEmpresa.Text);

                                        //Verifica se existe estoque para abastecimento no picking
                                        if (itensPicking.qtdPulmao <= 0)
                                        {
                                            //Altera a menssagem 
                                            itensCollection[i].observacao = "PICKING COM ESTOQUE INSUFICIENTE PARA ABASTECER";                                            
                                            //Adiciona o produto a coleção com problemas ou observações
                                            itensObservacaoCollection.Add(itensCollection[i]);
                                            //Pula para o próximo produto
                                            break;
                                        }
                                        else
                                        {
                                            if (itensPicking.idProduto == 0)
                                            {
                                                //Altera a menssagem 
                                                itensCollection[i].observacao = "NÃO POSSUI PICKING DE CAIXA";
                                                qtdEncontrada = 0;
                                                //Adiciona o produto a coleção com problemas ou observações
                                                itensObservacaoCollection.Add(itensCollection[i]);
                                                //Pula para o próximo produto
                                                break;
                                            }
                                            else
                                            {
                                                //Complementa as informações do pulmão para adicionar no grid
                                                itensPicking.codEstacao = itensCollection[i].codEstacao;
                                                itensPicking.descEstacao = itensCollection[i].descEstacao;
                                                itensPicking.codApartamentoPicking = itensCollection[i].codApartamentoPicking;
                                                itensPicking.enderecoPicking = itensCollection[i].enderecoPicking;
                                                itensPicking.idProduto = itensCollection[i].idProduto;
                                                itensPicking.codProduto = itensCollection[i].codProduto;
                                                itensPicking.descProduto = itensCollection[i].descProduto;
                                                itensPicking.qtdCaixaProduto = itensCollection[i].qtdCaixaProduto;
                                                itensPicking.capacidadePicking = itensCollection[i].capacidadePicking;
                                                itensPicking.abastecimentoPicking = itensCollection[i].abastecimentoPicking;
                                                itensPicking.qtdAbastecer = itensCollection[i].qtdAbastecer;
                                                itensPicking.unidadePulmao = itensCollection[i].unidadePulmao;
                                                itensPicking.qtdPicking = itensCollection[i].qtdPicking;
                                                itensPicking.unidadePicking = itensCollection[i].unidadePicking;

                                                qtdEncontrada = itensPicking.qtdPulmao; //Recebe a quantidade encontrada

                                                if (Convert.ToInt32(itensCollection[i].qtdAbastecer) <= itensPicking.qtdPulmao)
                                                {
                                                    //Se sim recebe a Quantidade necessária
                                                    itensPicking.qtdPulmao = Convert.ToInt32(itensCollection[i].qtdAbastecer);

                                                    //Passa a quantidade necessária
                                                    qtdEncontrada = Convert.ToInt32(itensCollection[i].qtdAbastecer);
                                                }

                                                //Adiciona o objêto a coleção
                                                itensAbastecimentoCollection.Add(itensPicking);

                                                //Diminui a quantidade de abastecimento
                                                itensCollection[i].qtdAbastecer = Convert.ToInt32(itensCollection[i].qtdAbastecer - itensPicking.qtdPulmao);

                                                //Pula para o próximo produto
                                                break;
                                            }
                                        }
                                    }

                               

                                //Verifica o abastecimento no pulmão
                                for (int iii = 0; itensCollection[i].qtdAbastecer >= qtdEncontrada; iii++)
                                {
                                    //Instância a camada de objêto - coleção
                                    ItensAbastecimento itensPulmao = new ItensAbastecimento();

                                    //Pesquisa o abastecimento no pulmão, passando o id do produto
                                    itensPulmao = abastecimentoNegocios.PesqAbastecimentoPulmao(itensCollection[i].idProduto, iii, cmbEmpresa.Text); //iii - Quantidade de linha a pular no select

                                    //Complementa as informações do pulmão para adicionar no grid - Precisa ficar aqui, se não sobrescre a observação
                                    itensPulmao.codEstacao = itensCollection[i].codEstacao;
                                    itensPulmao.descEstacao = itensCollection[i].descEstacao;
                                    itensPulmao.codApartamentoPicking = itensCollection[i].codApartamentoPicking;
                                    //itensPulmao.enderecoPicking = itensCollection[i].enderecoPicking;
                                    //itensPulmao.idProduto = itensCollection[i].idProduto;
                                    itensPulmao.codProduto = itensCollection[i].codProduto;
                                    itensPulmao.descProduto = itensCollection[i].descProduto;
                                    itensPulmao.qtdCaixaProduto = itensCollection[i].qtdCaixaProduto;
                                    itensPulmao.capacidadePicking = itensCollection[i].capacidadePicking;
                                    itensPulmao.abastecimentoPicking = itensCollection[i].abastecimentoPicking;
                                    itensPulmao.qtdAbastecer = itensCollection[i].qtdAbastecer;
                                    itensPulmao.unidadePulmao = itensCollection[i].unidadePulmao;
                                    itensPulmao.qtdPicking = itensCollection[i].qtdPicking;
                                    itensPulmao.unidadePicking = itensCollection[i].unidadePicking;

                                    if (itensPulmao.idProduto == 0)
                                    {
                                        //Altera a menssagem 
                                        itensPulmao.observacao = "NÃO EXISTE ESTOQUE NO PULMÃO";
                                        //Adiciona o produto a coleção com problemas ou observações
                                        itensObservacaoCollection.Add(itensPulmao);
                                        //Pula para o próximo produto
                                        break;
                                    }
                                    else
                                    {
                                        //Verifica se o pulmão tem mais do que precisa abastecer
                                        if (Convert.ToInt32(itensCollection[i].qtdAbastecer) <= itensPulmao.qtdPulmao)
                                        {
                                            //Se sim recebe a Quantidade necessária
                                            itensPulmao.qtdPulmao = Convert.ToInt32(itensCollection[i].qtdAbastecer);

                                            //  qtdEncontrada = Convert.ToInt32(itensCollection[i].qtdAbastecer);
                                        }

                                        //Adiciona o objêto a coleção
                                        itensAbastecimentoPulmaoCollection.Add(itensPulmao);

                                        //Verifica se há necessidade de continuar a pesquisa
                                        if (itensCollection[i].qtdAbastecer <= itensPulmao.qtdPulmao)
                                        {
                                            //Pula para o próximo produto
                                            break;
                                        }

                                        //Diminui a quantidade de abastecimento
                                        itensCollection[i].qtdAbastecer = Convert.ToInt32(itensCollection[i].qtdAbastecer - itensPulmao.qtdPulmao);

                                    }
                                }

                                });
                            }

                            //Incrementa o progressbar
                            IncrementarProgressBar();
                        }



                        //Garante que seja executado pela thread
                        Invoke((MethodInvoker)delegate ()
                        {
                            //grid Recebe o resultado da coleção de produto para o abastecimento
                            itensAbastecimentoCollection.ForEach(n => gridItens.Rows.Add(gridItens.Rows.Count + 1, n.codEstacao, n.descEstacao, n.codApartamentoPicking, n.enderecoPicking, n.idProduto,
                            n.codProduto + " - " + n.descProduto, n.qtdCaixaProduto, n.capacidadePicking, n.abastecimentoPicking, n.qtdAbastecer, n.unidadePulmao, n.qtdPicking, n.unidadePicking,
                            n.observacao, n.codApartamentoPulmao, n.enderecoPulmao, n.qtdPulmao, n.unidadePulmao, n.vencimentoPulmao, n.lotePulmao));

                            //grid Recebe o resultado da coleção de produto para o abastecimento
                            itensAbastecimentoPulmaoCollection.ForEach(n => gridPulmao.Rows.Add(gridPulmao.Rows.Count + 1, n.codEstacao, n.descEstacao, n.codApartamentoPicking, n.enderecoPicking, n.idProduto,
                                n.codProduto + " - " + n.descProduto, n.qtdCaixaProduto, n.capacidadePicking, n.abastecimentoPicking, n.qtdAbastecer, n.unidadePulmao, n.qtdPicking, n.unidadePicking,
                                n.observacao, n.codApartamentoPulmao, n.enderecoPulmao, n.qtdPulmao, n.unidadePulmao, n.vencimentoPulmao, n.lotePulmao));


                            //grid Recebe o resultado da coleção de produtos com observação
                            itensObservacaoCollection.ForEach(n => gridObservacao.Rows.Add(gridObservacao.Rows.Count + 1, n.descEstacao, n.enderecoPicking,
                                n.codProduto + " - " + n.descProduto, n.qtdAbastecer, n.unidadePulmao, n.qtdPicking, n.unidadePicking, n.observacao));

                            //Exibe a quantidade de observações
                            lblQtdObs.Text = gridObservacao.Rows.Count.ToString();

                            //Exibe a quantidade de endereços do picking 
                            lblQtdPicking.Text = gridItens.Rows.Count.ToString();

                            //Exibe a quantidade de endereços do pulmão 
                            lblQtdPulmao.Text = gridPulmao.Rows.Count.ToString();

                            //Foca na página de análise de picking
                            tabControl1.SelectedTab = tabPage2;

                            //Exibe cores de alerta
                            CoresGridObservacao();

                        });

                        //Se não existiritem para o abastecimento
                        if (itensAbastecimentoCollection.Count == 0 && itensAbastecimentoPulmaoCollection.Count == 0)
                        {
                            MessageBox.Show("Não existe produto para gerar o abastecimento! ", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //Exibe cores de alerta no grid observação
        private void CoresGridObservacao()
        {
            try
            {
                if (gridObservacao.Rows.Count > 0)
                {
                    string observacao;

                    for (int i = 0; gridObservacao.Rows.Count > i; i++)
                    {
                        observacao = Convert.ToString(gridObservacao.Rows[i].Cells[8].Value);

                        if (observacao.Equals("ERRO: ESTOQUE NEGATIVO") ||
                            observacao.Equals("ESTOQUE ACIMA DA CAPACIDADE") ||
                            observacao.Equals("FALTA DADOS LOGÍSTICOS"))
                        {
                            gridObservacao.Rows[i].Cells[8].Style.ForeColor = Color.Red;
                        }

                        if (observacao.Equals("PICKING COM ESTOQUE INSUFICIENTE PARA ABASTECER"))
                        {
                            gridObservacao.Rows[i].Cells[8].Style.ForeColor = Color.OrangeRed;
                        }

                        if (observacao.Equals("PRODUTO NÃO PRECISA DE ABASTECIMENTO"))
                        {
                            gridObservacao.Rows[i].Cells[8].Style.ForeColor = Color.Green;
                        }

                        if (observacao.Equals("NÃO EXISTE ESTOQUE NO PULMÃO"))
                        {
                            gridObservacao.Rows[i].Cells[8].Style.ForeColor = Color.Blue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //Gera o abastecimento
        private void GerarAbastecimento()
        {
            try
            {
                //Instância a camada de negocios
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();

                int? codRegiao = null, codRua = null, codCategoria = null, codFornecedor = null,
                     codEmpilhador = null, codRepositor = null;

                string ladoRua = null, tipo = null, codManifesto = null;

                if (cmbModo.Text.Equals("Corretivo") && cmbTipo.Text.Equals("Caixa"))
                {
                    if (!(cmbTipo.Text.Equals("Selecione") || cmbTipo.Text.Equals("")))
                    {
                        //Recebe o tipo
                        tipo = cmbTipo.Text;
                    }

                    if (!(cmbRegiao.Text.Equals("Selec...") || cmbRegiao.Text.Equals("")))
                    {
                        if (cmbRegiao.SelectedItem == null)
                        {
                            MessageBox.Show("Por favor, ao digitar uma região pressione para cima ou para baixo.", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //Recebe o código da região
                            codRegiao = regiao[cmbRegiao.SelectedIndex];
                        }
                    }


                    if (!(cmbRua.Text.Equals("Selec...") || cmbRua.Text.Equals("")))
                    {
                        if (cmbRua.SelectedItem == null)
                        {
                            MessageBox.Show("Por favor, ao digitar uma rua pressione para cima ou para baixo.", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //Recebe o código da rua
                            codRua = rua[cmbRua.SelectedIndex];
                        }
                    }


                    if (!(cmbLado.Text.Equals("Selec...") || cmbLado.Text.Equals("")))
                    {
                        //Recebe o lado da rua
                        ladoRua = cmbLado.Text;
                    }

                    if (!(cmbCategoria.Text.Equals("Selecione") || cmbCategoria.Text.Equals("")))
                    {
                        if (cmbCategoria.SelectedItem == null)
                        {
                            MessageBox.Show("Por favor, ao digitar uma categoria pressione para cima ou para baixo.", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            return;
                        }
                        else
                        {
                            codCategoria = categoria[cmbCategoria.SelectedIndex];
                        }
                    }

                    if (!txtCodFornecedor.Text.Equals(""))
                    {
                        codFornecedor = Convert.ToInt32(txtCodFornecedor.Text);
                    }
                }
                else if (cmbModo.Text.Equals("Corretivo") && cmbTipo.Text.Equals("FlowRack"))
                {
                    if (!(cmbTipo.Text.Equals("Selecione") || cmbTipo.Text.Equals("")))
                    {
                        //Recebe o tipo
                        tipo = cmbTipo.Text;
                    }
                }
                //Seta os códigos do manifesto
                else if (cmbModo.Text.Equals("Preventivo"))
                {
                    for (int i = 0; gridManifesto.Rows.Count > i; i++)
                    {
                        codManifesto += Convert.ToString(gridManifesto.Rows[i].Cells[1].Value) + "  ";
                    }

                    if (cmbTipo.Text.Equals("Selecione"))
                    {
                        tipo = "Manifesto";
                    }
                }

                if (!(cmbEmpilhador.Text.Equals("Selecione") || cmbEmpilhador.Text.Equals("")))
                {
                    if (cmbEmpilhador.SelectedItem == null)
                    {
                        MessageBox.Show("Por favor, ao digitar um empilhador pressione para cima ou para baixo.", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        codEmpilhador = empilhador[cmbEmpilhador.SelectedIndex];
                    }
                }

                if (!(cmbRepositor.Text.Equals("Selecione") || cmbRepositor.Text.Equals("")))
                {
                    if (cmbRepositor.SelectedItem == null)
                    {
                        MessageBox.Show("Por favor, ao digitar um repositor pressione para cima ou para baixo.", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }
                    else
                    {
                        codRepositor = repositor[cmbRepositor.SelectedIndex];
                    }
                }


                //Passa as informações para a camada de négocios
                abastecimentoNegocios.GerarAbastecimento(Convert.ToInt32(txtCodigo.Text), codUsuario, codCategoria, codFornecedor,
                    codRegiao, codRua, ladoRua,
                    tipo, cmbModo.Text, codEmpilhador, codRepositor, codManifesto, cmbEmpresa.Text);


                //Gerar itens do abastecimento
                Thread thread = new Thread(GerarItensAbastecimento);
                thread.Start();




            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Gerar os itens do abastecimento
        private void GerarItensAbastecimento()
        {
            try
            {
                //Instância a camada de negocios
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();

                if (gridItens.Rows.Count > 0)
                {
                    //Passa a quantidade de itens
                    Progressbar(gridItens.Rows.Count);

                    for (int i = 0; gridItens.Rows.Count > i; i++)
                    {
                        abastecimentoNegocios.GerarItens(Convert.ToInt32(txtCodigo.Text), //Código do abastecimento
                            Convert.ToInt32(gridItens.Rows[i].Cells[1].Value), //Código do estação
                            Convert.ToInt32(gridItens.Rows[i].Cells[3].Value), //Código do picking
                            Convert.ToInt32(gridItens.Rows[i].Cells[5].Value), //ID do produto
                            Convert.ToInt32(gridItens.Rows[i].Cells[10].Value) * Convert.ToInt32(gridItens.Rows[i].Cells[7].Value), //Quantidade a abastecer x qtd Caixa
                            Convert.ToInt32(gridItens.Rows[i].Cells[12].Value), //Estoque do produto
                            Convert.ToInt32(gridItens.Rows[i].Cells[15].Value), //Código do pulmão
                            Convert.ToInt32(gridItens.Rows[i].Cells[17].Value) * Convert.ToInt32(gridItens.Rows[i].Cells[7].Value), //Estoque do pulmão x qtd caixa
                            Convert.ToString(gridItens.Rows[i].Cells[19].Value), //Vencimento do pulmão
                            Convert.ToString(gridItens.Rows[i].Cells[20].Value), //Lote do pulmão
                            "ANÁLISE DE PICKING");

                        //Exibe o endereços, quantidade e o tipo de estoque 
                        MsgLabel("GERANDO: " + gridItens.Rows[i].Cells[16].Value + " - " + gridItens.Rows[i].Cells[17].Value + " " + gridItens.Rows[i].Cells[18].Value + "   " + gridItens.Rows[i].Cells[6].Value);

                        //Incrementa o progressbar
                        IncrementarProgressBar();
                    }
                }

                if (gridPulmao.Rows.Count > 0)
                {
                    //Passa a quantidade de itens
                    Progressbar(gridPulmao.Rows.Count);

                    for (int i = 0; gridPulmao.Rows.Count > i; i++)
                    {
                        abastecimentoNegocios.GerarItens(Convert.ToInt32(txtCodigo.Text), //Código do abastecimento
                            Convert.ToInt32(gridPulmao.Rows[i].Cells[1].Value), //Código do estação
                            Convert.ToInt32(gridPulmao.Rows[i].Cells[3].Value), //Código do picking
                            Convert.ToInt32(gridPulmao.Rows[i].Cells[5].Value), //ID do produto
                            Convert.ToInt32(gridPulmao.Rows[i].Cells[10].Value) * Convert.ToInt32(gridPulmao.Rows[i].Cells[7].Value), //Quantidade a abastecer x qtd Caixa
                            Convert.ToInt32(gridPulmao.Rows[i].Cells[12].Value), //Estoque do produto
                            Convert.ToInt32(gridPulmao.Rows[i].Cells[15].Value), //Código do pulmão
                            Convert.ToInt32(gridPulmao.Rows[i].Cells[17].Value) * Convert.ToInt32(gridPulmao.Rows[i].Cells[7].Value), //Estoque do pulmão x qtd caixa
                            Convert.ToString(gridPulmao.Rows[i].Cells[19].Value), //Vencimento do pulmão
                            Convert.ToString(gridPulmao.Rows[i].Cells[20].Value), //Lote do pulmão
                                "ANÁLISE DO PULMÃO");
                        //Exibe o endereços, quantidade e o tipo de estoque 
                        MsgLabel("GERANDO: " + gridPulmao.Rows[i].Cells[16].Value + " - " + gridPulmao.Rows[i].Cells[17].Value + " " + gridPulmao.Rows[i].Cells[18].Value + "   " + gridPulmao.Rows[i].Cells[6].Value);

                        //Incrementa o progressbar
                        IncrementarProgressBar();
                    }
                }

                //Garante que seja executado pela thread
                Invoke((MethodInvoker)delegate ()
                {
                    //Desabilita os campos
                    DesabilitarCampos();
                });

                MessageBox.Show("Ordem de abastecimento gerada com sucesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Altera os responsáveis da ordem
        private void AlterarAbastecimento()
        {
            try
            {
                //Instância a camada de negocios
                AbastecimentoNegocios abastecimentoNegocios = new AbastecimentoNegocios();

                int? codEmpilhador = null, codRepositor = null;

                if (!(cmbEmpilhador.Text.Equals("Selecione") || cmbEmpilhador.Text.Equals("")))
                {
                    codEmpilhador = empilhador[cmbEmpilhador.SelectedIndex];
                }

                if (!(cmbRepositor.Text.Equals("Selecione") || cmbRepositor.Text.Equals("")))
                {
                    codRepositor = repositor[cmbRepositor.SelectedIndex];
                }

                //Passa as informações para a camada de négocios
                abastecimentoNegocios.AlterarOrdem(Convert.ToInt32(txtCodigo.Text), codEmpilhador, codRepositor);

                //Esconde o botão 
                btnAlterarOrdem.Visible = false;
                //Desabilita
                DesabilitarCampos();
                MessageBox.Show("Ordem de abastecimento atualizada com sucesso!", "Wms - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

                lblAnalise.Text = "Analisando: ";
                //Exibe a quantidade de itens analizados
                lblQtdAnalise.Text = Convert.ToString(valor);

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
                    lblAnalise.Text = "Analisados: ";
                    lblProcesso.Visible = false;
                }
            });
        }

        //Exibe o texto do processo 
        private void MsgLabel(string texto)
        {
            Invoke((MethodInvoker)delegate ()
            {
                lblProcesso.Text = texto;

                lblProcesso.Visible = true;
            });
        }

        private void LimparCampos() //Limpar componentes
        {

            cmbRegiao.Items.Clear(); //Limpar o campo região
            cmbRua.Items.Clear(); //Limpar o campo rua            
            cmbCategoria.Items.Clear(); //Limpar o campo categoria
            txtCodFornecedor.Clear();  //Limpar o campo código do fornecedor
            txtNomeFornecedor.Clear();  //Limpar o campo nome do fornecedor
            txtManifesto.Clear();  //Limpar o campo manifesto

            cmbModo.Text = "Selecione"; //Seta o texto no campo modo
            cmbTipo.Text = "Selecione"; //Seta o texto no campo tipo
            cmbRegiao.Text = "Selec...";// Seta o texto no campo região
            cmbRua.Text = "Selec...";// Seta o texto no campo rua
            cmbLado.Text = "Todos"; //Seta o texto no campo lado
            cmbEmpilhador.Text = "Selecione";
            cmbRepositor.Text = "Selecione";

            gridEstacao.Rows.Clear(); //Limpar o grid estação
            gridManifesto.Rows.Clear(); //Limpar o grid manifesto
            gridItens.Rows.Clear(); //Limpar o grid itens
            gridPulmao.Rows.Clear(); //Limpar o grid pulmao
            gridObservacao.Rows.Clear(); //Limpar o grid observação

            lblQtdOrdem.Text = "0";
            lblQtdAnalise.Text = "0";
            lblQtdObs.Text = "0";
            lblQtdPicking.Text = "0";
            lblQtdPulmao.Text = "0";

            //Seleciona a primeira página
            tabControl1.SelectedTab = tabPage1;
        }

        private void HabilitarCampos() //Habilita componentes
        {
            cmbModo.Enabled = true; //Habilitar o campo modo
            cmbTipo.Enabled = true; //Habilitar o campo tipo
            cmbRegiao.Enabled = true; //Habilitar o campo região
            cmbRua.Enabled = true; //Habilitar o campo rua
            cmbLado.Enabled = true; //Habilitar o campo lado
            cmbCategoria.Enabled = true; //Habilitar o campo categoria
            txtCodFornecedor.Enabled = true; //Habilitar o campo código do fornecedor
            cmbEmpilhador.Enabled = true; //Habilitar o campo categoria
            cmbRepositor.Enabled = true; //Habilitar o campo categoria
            txtManifesto.Enabled = true; //Habilitar o campo manifesto

            chkSelecionar.Enabled = true; //Habilitar a opção de seleciona todas as estações 
            gridItens.Enabled = true; //Habilitar o grid itens
            gridPulmao.Enabled = true; //Habilitar o grid pulmão
            gridObservacao.Enabled = true; //Habilitar o grid observação

            btnAnalisar.Enabled = true;//Habilitar botão analisar
            btnAbrir.Enabled = true;//Habilitar botão abrir ordem
        }

        private void DesabilitarCampos() //Desabilita componentes
        {
            cmbModo.Enabled = false; //Desabilitar o campo modo
            cmbTipo.Enabled = false; //Desabilitar o campo tipo
            cmbRegiao.Enabled = false; //Desabilitar o campo região
            cmbRua.Enabled = false; //Desabilitar o campo rua
            cmbLado.Enabled = false; //Desabilitar o campo lado
            cmbCategoria.Enabled = false; //Desabilitar o campo categoria
            txtCodFornecedor.Enabled = false; //Desabilitar o campo código do fornecedor
            cmbEmpilhador.Enabled = false; //Desabilitar o campo empilhador
            cmbRepositor.Enabled = false; //Desabilitar o campo repositor
            txtManifesto.Enabled = true; //Desabilitar o campo manifesto


            chkSelecionar.Enabled = false; //Desabilita o checkbox 
            gridEstacao.Enabled = false; //Desabilitar o grid estação
            gridManifesto.Enabled = false; //Desabilitar o grid manifesto
            gridItens.Enabled = false; //Desabilitar o grid itens
            gridPulmao.Enabled = false; //Desabilitar o grid pulmão
            gridObservacao.Enabled = false; //Desabilitar o grid observação

            btnAnalisar.Enabled = false;//Desabilitar botão analisar
            btnAbrir.Enabled = false;//Desabilitar botão abrir ordem

            btnAlterarOrdem.Visible = false;//Esconde o botão de alterar

        }

        //Gera o relatório
        private void GerarRelatorio()
        {
            try
            {
                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância o relatório
                    FrmMapaAbastecimentoPicking frame = new FrmMapaAbastecimentoPicking();
                    //Passa o número do manifesto
                    frame.GerarRelatorioPicking(Convert.ToInt32(txtCodigo.Text), "CAIXA");

                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Gera o relatório
        private void GerarRelatorioFlowRack()
        {
            try
            {
                //Garante que o processo seja executado da thread que foi iniciado
                Invoke((MethodInvoker)delegate ()
                {
                    //Instância o relatório
                    FrmMapaAbastecimentoFlowRack frame = new FrmMapaAbastecimentoFlowRack();
                    //Passa o número do manifesto
                    frame.GerarRelatorio(Convert.ToInt32(txtCodigo.Text), "FLOWRACK");

                    //Instância o relatório
                    FrmMapaAbastecimentoFlowRack framePulmao = new FrmMapaAbastecimentoFlowRack();
                    //Passa o número do manifesto
                    framePulmao.GerarRelatorioPulmao(Convert.ToInt32(txtCodigo.Text));

                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }

}
