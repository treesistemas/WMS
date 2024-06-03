using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmEmpilhador : Form
    {
        //Array com id
        private int[] regiao;
        private int[] ruaInicial;
        private int[] ruaFinal;
        private int indexRegiao;
        private int indexRuaInicial;
        private int indexRuaFinal;

        //controla a opção de salvar e alterar (false = alterar)
        bool opcao = false;

        //Instância a coleção
        EmpilhadorCollection empilhadorCollection = new EmpilhadorCollection();

        //Perfíl do usuário
        public string perfilUsuario;
        public List<Empresa> empresaCollection;

        //Controle de acesso
        public List<Acesso> acesso;

        public FrmEmpilhador()
        {
            InitializeComponent();
        }

        private void FrmEmpilhador_Load(object sender, EventArgs e)
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

        private void lnkInformacao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("No cadastro de empilhador você define a responsabilidades de cada empilhador", "WMS - Informação");
        }

        private void cmbRegiao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pesquisa a rua
            PesqRua();
            //recebe o index da região selecionada
            indexRegiao = regiao[cmbRegiao.SelectedIndex];

        }

        private void cmbRuaInicial_SelectedIndexChanged(object sender, EventArgs e)
        {
            //recebe o index da rua selecionada
            indexRuaInicial = ruaInicial[cmbRuaInicial.SelectedIndex];
        }

        private void cmbRuaFinal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbRuaInicial.SelectedItem) > Convert.ToInt32(cmbRuaFinal.SelectedItem))
            {
                MessageBox.Show("Rua inícial não pode ser maior que a rua final.", "WMS - Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRuaFinal.SelectedText = "Selecione";
            }
            else
            {
                //recebe o index da rua selecionada
                indexRuaFinal = ruaFinal[cmbRuaFinal.SelectedIndex];
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa região
            PesqRegiao();

            //Pesquisa o empilhador
            PesqUsuario();
        }

        private void gridEmpilhador_MouseClick(object sender, MouseEventArgs e)
        {
            //Exibe os dados nos campos
            exibirEmpilhadorCampos();
        }

        private void gridEmpilhador_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (perfilUsuario == "ADMINISTRADOR" || acesso[0].editarFuncao == true)
                {
                    if (gridEmpilhador.Rows.Count > 0)
                    {
                        //Habilita todos os campos
                        habilita();

                        if (gridEndereco.RowCount > 0)
                        {
                            opcao = false;
                        }
                        else
                        {
                            opcao = true;
                        }
                    }
                }

            }
        }

        private void gridEmpilhador_KeyDown(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            exibirEmpilhadorCampos();
        }

        private void gridEmpilhador_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe os dados nos campos
            exibirEmpilhadorCampos();
        }

        private void gridEndereco_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Exibe os endereços no combobox
            exibirEnderecoCampos();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (gridEmpilhador.RowCount > 0)
            {
                //Limpa os campos enderecos
                limpaEnderecosCampos();
                //habilita os campos
                habilita();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um empilhador!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (opcao.Equals(true))
            {
                if (acesso[0].escreverFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Salva o cadastro
                    salvarResponsabilidades();
                }
            }
            else
            {
                if (acesso[0].editarFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Alterar Cadastro
                    alterarResponsabilidades();
                }
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (acesso[0].excluirFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Remove o endereço
                Remover();
            }
        }

        private void btnRendimento_Click(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o form
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
                cmbRuaInicial.Items.Clear();
                //Limpa o combobox rua final
                cmbRuaFinal.Items.Clear();
                //Adiciona o texto
                cmbRuaInicial.Text = "Selecione";
                cmbRuaFinal.Text = "Selecione";
                //Instância a coleção
                EstruturaCollection gerarEnderecoCollection = new EstruturaCollection();
                //Instância o negocios
                EstruturaNegocios gerarEnderecoNegocios = new EstruturaNegocios();
                //Pesquisa as ruas da região selecionado
                gerarEnderecoCollection = gerarEnderecoNegocios.PesqRua(regiao[cmbRegiao.SelectedIndex]);
                //Preenche o combobox rua
                gerarEnderecoCollection.ForEach(n => cmbRuaInicial.Items.Add(n.numeroRua));
                gerarEnderecoCollection.ForEach(n => cmbRuaFinal.Items.Add(n.numeroRua));
                //Define o tamanho do array
                ruaInicial = new int[gerarEnderecoCollection.Count];
                ruaFinal = new int[gerarEnderecoCollection.Count];

                for (int i = 0; i < gerarEnderecoCollection.Count; i++)
                {
                    //Preenche o array
                    ruaInicial[i] = gerarEnderecoCollection[i].codRua;
                    ruaFinal[i] = gerarEnderecoCollection[i].codRua;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        //Pesquisa o usuário
        private void PesqUsuario()
        {
            try
            {
                //Instância o objeto
                UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                //Instância a coleção
                UsuarioCollection usuarioCollection = new UsuarioCollection();
                //A coleção recebe o resultado da consulta
                usuarioCollection = usuarioNegocios.PesqUsuario(cmbEmpresa.Text);
                //Limpa o grid
                gridEmpilhador.Rows.Clear();
                //Localizando os empilhadores e ordena por turno
                List<Usuario> usuarioEmpilhador = usuarioCollection.FindAll(delegate (Usuario n) { return n.perfil == "EMPILHADOR"; }).OrderBy(c => c.turno).ToList();

                //grid recebe o resultado da coleção
                usuarioEmpilhador.ForEach(n => gridEmpilhador.Rows.Add(n.codUsuario, n.login, n.turno));

                if (gridEmpilhador.Rows.Count > 0)
                {
                    //seta a quantidade de empilhadores
                    lblQtd.Text = gridEmpilhador.Rows.Count.ToString();
                    //Exibe os dados nos campos
                    exibirEmpilhadorCampos();
                    //Seleciona a primeira linha do grid
                    gridEmpilhador.CurrentCell = gridEmpilhador.Rows[0].Cells[0];
                    //Foca no grid
                    gridEmpilhador.Focus();
                    //Pesquisa as responsabilidades
                    PesqResponsabilidadeEmpilhador();


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa empilhador
        private void PesqResponsabilidadeEmpilhador()
        {
            try
            {
                //Instância o objêto
                EmpilhadorNegocios empilhadorNegocios = new EmpilhadorNegocios();
                //Instância a coleção
                EmpilhadorCollection empilhadorCollection = new EmpilhadorCollection();
                //A coleção recebe o resultado da consulta
                empilhadorCollection = empilhadorNegocios.PesqResponsabilidadeEmpilhador(cmbEmpresa.Text);
                //Coleção global recebe a local
                this.empilhadorCollection = empilhadorCollection;

                //Instância as linha da tabela
                DataGridViewRow linha = gridEmpilhador.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Limpa o grid
                gridEndereco.Rows.Clear();
                //Localizando as responsabilidades do empilhador
                List<Empilhador> empilhador = empilhadorCollection.FindAll(delegate (Empilhador n) { return n.codUsuario == Convert.ToInt32(gridEmpilhador.Rows[indice].Cells[0].Value); });
                //grid recebe o resultado da coleção
                empilhador.ForEach(n => gridEndereco.Rows.Add(n.codEmpilhador, n.regiao, n.ruaInicial, n.ruafinal));

                //Exibe os endereço
                exibirEnderecoCampos();
                //Desabilita todos os campos
                Desabilita();

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Seta os dados nos campos
        private void exibirEmpilhadorCampos()
        {
            try
            {
                if ((gridEmpilhador.SelectedRows.Count > 0))
                {
                    //Instância as linha do grid
                    DataGridViewRow linha = gridEmpilhador.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o código
                    txtCodigo.Text = gridEmpilhador.Rows[indice].Cells[0].Value.ToString();
                    //Seta o empilhador
                    txtEmpilhador.Text = gridEmpilhador.Rows[indice].Cells[1].Value.ToString();
                    //Seta o turno
                    cmbTurno.SelectedItem = gridEmpilhador.Rows[indice].Cells[2].Value;

                    //Exibe as responsabilidades do empilhador
                    PesqResponsabilidadeEmpilhador();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos.", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void exibirEnderecoCampos()
        {
            try
            {

                if (gridEndereco.Rows.Count > 0)
                {
                    //Instância as linha do grid
                    DataGridViewRow linha = gridEndereco.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seleciona a região, rua inicial e a rua ninal
                    cmbRegiao.Text = gridEndereco.Rows[indice].Cells[1].Value.ToString();
                    cmbRuaInicial.Text = gridEndereco.Rows[indice].Cells[2].Value.ToString();
                    cmbRuaFinal.Text = gridEndereco.Rows[indice].Cells[3].Value.ToString();
                }
                else
                {
                    cmbRegiao.Text = "Selecione";
                    cmbRuaInicial.Text = "Selecione";
                    cmbRuaFinal.Text = "Selecione";
                }



            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os enderecos nos campos.", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void salvarResponsabilidades()
        {
            try
            {
                if ((cmbRegiao.Text.Equals("Selecione")) || (cmbRuaInicial.Text.Equals("Selecione")) || (cmbRuaFinal.Text.Equals("Selecione")))
                {
                    MessageBox.Show("Preencha todos os campos!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objêto
                    EmpilhadorNegocios empilhadorNegocios = new EmpilhadorNegocios();
                    //Salva o empilhador
                    empilhadorNegocios.Salvar(Convert.ToInt32(txtCodigo.Text), indexRegiao, indexRuaInicial, indexRuaFinal, cmbEmpresa.Text);

                    //Instância as linha da tabela
                    DataGridViewRow linha = gridEmpilhador.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Adiciona a linha no grid
                    gridEndereco.Rows.Add("", cmbRegiao.SelectedItem, cmbRuaInicial.SelectedItem, cmbRuaFinal.SelectedItem);
                    //Insere o turno no grid                      
                    gridEmpilhador.Rows[indice].Cells[2].Value = cmbTurno.SelectedItem;
                    //Limpa campos
                    limpaEnderecosCampos();
                    //Pesquisa os endereços associados (Preenche o novo id do endereço associado caso o usuário queira excluir)
                    PesqResponsabilidadeEmpilhador();
                    //Exibe os dados do empilhador nos campos
                    exibirEmpilhadorCampos();
                    //Desabilita todos os campos
                    Desabilita();

                    MessageBox.Show("Cadastro realizado com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Altera as responsabilidades do empilhador
        private void alterarResponsabilidades()
        {
            try
            {
                if ((cmbRegiao.Text.Equals("Selecione")) || (cmbRuaInicial.Text.Equals("Selecione")) || (cmbRuaFinal.Text.Equals("Selecione")))
                {
                    MessageBox.Show("Preencha todos os campos!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Instância o objêto
                    EmpilhadorNegocios empilhadorNegocios = new EmpilhadorNegocios();

                    // Instância as linha da tabela
                    DataGridViewRow linha = gridEndereco.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Altera as responsabilidades do empilhador
                    empilhadorNegocios.Alterar(Convert.ToInt32(gridEndereco.Rows[indice].Cells[0].Value), indexRegiao, indexRuaInicial, indexRuaFinal, cmbEmpresa.Text);

                    //Altera a região no grid                      
                    gridEndereco.Rows[indice].Cells[1].Value = cmbRegiao.SelectedItem;
                    //Altera a rua inicial no grid                      
                    gridEndereco.Rows[indice].Cells[2].Value = cmbRuaInicial.SelectedItem;
                    //Altera a região no grid                      
                    gridEndereco.Rows[indice].Cells[3].Value = cmbRuaFinal.SelectedItem;

                    //Limpa campos
                    limpaEnderecosCampos();
                    //Exibe os dados do empilhador nos campos
                    exibirEnderecoCampos();
                    //Desabilita todos os campos
                    Desabilita();


                    MessageBox.Show("Cadastro alterado com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Romove o endereço associado
        private void Remover()
        {
            try
            {
                if (!(gridEndereco.SelectedRows.Count > 0))
                {
                    MessageBox.Show("Selecione um endereço para remoção.", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    DialogResult resultado = MessageBox.Show(" Você deseja realmente remover o endereço associado?", "WMS - Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        //Instância as linha da tabela
                        DataGridViewRow linha = gridEndereco.CurrentRow;
                        //Recebe o indice   
                        int indice = linha.Index;

                        //Instância o objêto
                        EmpilhadorNegocios empilhadorNegocios = new EmpilhadorNegocios();
                        empilhadorNegocios.Remover(cmbEmpresa.Text, Convert.ToInt32(gridEndereco.Rows[indice].Cells[0].Value));

                        //Remove o endereço associado da coleção
                        //Remove pelo index
                        empilhadorCollection.RemoveAt(indice);
                        //Remove a linha do grid endereço
                        gridEndereco.Rows.RemoveAt(indice);
                        //exibe os dados do endereco e existir
                        exibirEnderecoCampos();
                        //Desabilita todos os campos
                        Desabilita();


                        MessageBox.Show("Remoção realizada com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limpaEnderecosCampos()
        {
            //Seta o texto 
            cmbRegiao.Text = "Selecione";
            //Seta o texto
            cmbRuaInicial.Text = "Selecione";
            //Seta o texto
            cmbRuaFinal.Text = "Selecione";

            opcao = true;
        }

        private void habilita()
        {
            //Habilita o campo região
            cmbRegiao.Enabled = true;
            //Habilita o campo rua inícial
            cmbRuaInicial.Enabled = true;
            //Habilita o campo final
            cmbRuaFinal.Enabled = true;
            //Habilita o grid endereço
            gridEndereco.Enabled = true;
            //Habilita o botão salvar
            btnSalvar.Enabled = true;
            //Habilita o botão remover
            btnRemover.Enabled = true;

            if (cmbTurno.Text.Equals("Selecione"))
            {
                //Habilita o campo turno
                cmbTurno.Enabled = true;
            }

        }

        private void Desabilita()
        {
            //Desabilita o campo região
            cmbRegiao.Enabled = false;
            //Desabilita o campo rua inícial
            cmbRuaInicial.Enabled = false;
            //Desabilita o campo final
            cmbRuaFinal.Enabled = false;
            //Desabilita o campo turno
            cmbTurno.Enabled = false;
            //Desabilita o grid endereço
            gridEndereco.Enabled = false;
            //Desabilita o botão salvar
            btnSalvar.Enabled = false;
            //Desabilita o botão remover
            btnRemover.Enabled = false;
        }

    }
}
