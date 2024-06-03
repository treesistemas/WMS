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
    public partial class FrmPerfil : Form
    {
        //Controle para salvar e alterar
        bool opcao = false;

        //Instância a coleção
        AcessoCollection funcaoCollection = new AcessoCollection();

        //Perfíl do usuário
        public string perfilUsuario;
        //Controle de acesso
        public List<Acesso> acesso;

        public FrmPerfil()
        {
            InitializeComponent();

        }

        private void txtPerfil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo salvar
                btnSalvar.Focus();
            }
        }

        private void gridPerfil_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Exibe os dados
            DadosCampos();
        }

        private void gridPerfil_KeyDown(object sender, KeyEventArgs e)
        {
            //Exibe os dados
            DadosCampos();
        }

        private void gridPerfil_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe os dados
            DadosCampos();
        }

        private void gridPerfil_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                //Controle para alterar  
                opcao = false;
                //Verifica o perfíl
                VerificaPerfilHabilita();
            }
        }
       
        private void gridFuncao_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Exibe a permissão de acesso
            DadosPermisaoFuncao();
        }

        private void gridFuncao_KeyDown(object sender, KeyEventArgs e)
        {
            //Exibe a permissão de acesso
            DadosPermisaoFuncao();
        }

        private void gridFuncao_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe a permissão de acesso
            DadosPermisaoFuncao();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa o perfíl
            PesqPerfil();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (acesso[0].escreverFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Pesquisa o id
                PesqId();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (opcao == true)
            {
                if (acesso[0].escreverFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Salva o cadastro
                    Salvar(Convert.ToInt32(txtCodigo.Text), txtPerfil.Text);
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
                    //Alterar o cadastro
                    Alterar(Convert.ToInt32(txtCodigo.Text), txtPerfil.Text);
                }
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (acesso[0].editarFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Atualiza as permissões
                AtualizarPermissao();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha a tela
            Dispose();
        }

        private void PesqId()
        {
            try
            {
                //Limpa todos os campos
                LimpaCampos();
                //Instância o objeto
                PerfilNegocios perfilNegocios = new PerfilNegocios();
                //Instância o objeto
                Perfil perfil = new Perfil();
                //Recebe o id
                perfil = perfilNegocios.PesqId();
                //seta o código
                txtCodigo.Text = Convert.ToString(perfil.codPerfil);
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

        //Salva o perfíl
        private void Salvar(int id, string descricao)
        {
            try
            {
                if ((id == 0) || (descricao == ""))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo
                    txtPerfil.Focus();
                }
                else
                {
                    //Instância o objeto
                    PerfilNegocios perfilNegocios = new PerfilNegocios();
                    //Instância o objeto
                    Perfil perfil = new Perfil();
                    //Seta o id 
                    perfil.codPerfil = id;
                    //Seta o perfil
                    perfil.descPerfil = descricao;
                    //Passa o perfil para a camada de negocios
                    perfilNegocios.Salvar(perfil);

                    //Insere o cadastro na tabela
                    gridPerfil.Rows.Add(txtCodigo.Text, txtPerfil.Text);
                    //Recebe a qtd de linha na tabela 
                    int linha = Convert.ToInt32(gridPerfil.RowCount.ToString());
                    //Seleciona a linha      
                    gridPerfil.CurrentCell = gridPerfil.Rows[linha - 1].Cells[1];
                    //Soma a qtd de perfil 
                    lblQtd.Text = Convert.ToString(Convert.ToInt32(lblQtd.Text) + 1);
                    //Desabilita todos os campos
                    Desabilita();
                    //controle para alterar
                    opcao = false;
                    //Pesquisa o acesso
                    PesqFuncao();
                    MessageBox.Show("Cadastro realizado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Altera a descrição do perfil
        private void Alterar(int id, string descricao)
        {
            try
            {
                if ((id == 0) || (descricao == ""))
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Foca no campo
                    txtPerfil.Focus();
                }
                else
                {
                    //Instância o objeto
                    PerfilNegocios perfilNegocios = new PerfilNegocios();
                    //Instância o objeto
                    Perfil perfil = new Perfil();
                    //Seta o id
                    perfil.codPerfil = id;
                    //Seta o perfil
                    perfil.descPerfil = descricao;
                    //Passa para a camada de negocios
                    perfilNegocios.Alterar(perfil);

                    //Instância as linha da tabela
                    DataGridViewRow linha = gridPerfil.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;
                    //Insere a descrição na tabela                      
                    gridPerfil.Rows[indice].Cells[1].Value = txtPerfil.Text;
                    //Foca na tabela
                    gridPerfil.Focus();
                    //Desabilita todos os campos
                    Desabilita();

                    MessageBox.Show("Cadastro alterado com sucesso! ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o perfíl cadastrados        
        private void PesqPerfil()
        {
            try
            {
                //Instância o objeto
                PerfilNegocios perfilNegocios = new PerfilNegocios();
                //Instância a coleção
                PerfilCollection perfilCollection = new PerfilCollection();
                //A coleção recebe o resultado da consulta
                perfilCollection = perfilNegocios.PesqPerfil();
                //Limpa o grid
                gridPerfil.Rows.Clear();
                //Grid Recebe o resultado da coleção
                perfilCollection.ForEach(n => gridPerfil.Rows.Add(n.codPerfil, n.descPerfil));

                if (gridPerfil.RowCount > 0)
                {
                    //Qtd de perfil encontrado
                    lblQtd.Text = gridPerfil.RowCount.ToString();
                    //Seleciona a primeira linha do grid
                    gridPerfil.CurrentCell = gridPerfil.Rows[0].Cells[1];
                    //Foca no grid
                    gridPerfil.Focus();
                    //Seta os dados nos campos
                    DadosCampos();
                    //Pesquisa o acesso
                    PesqFuncao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //pesquisa as funções cadastrados
        private void PesqFuncao()
        {
            try
            {
                //Instância o objeto
                FuncaoNegocios funcaoNegocios = new FuncaoNegocios();
                //Instância a coleção
                AcessoCollection funcaoCollection = new AcessoCollection();
                //A coleção recebe o resultado da consulta
                funcaoCollection = funcaoNegocios.PesqFuncao();
                //Limpa o grid
                gridFuncao.Rows.Clear();
                //Grid Recebe o resultado da coleção
                funcaoCollection.ForEach(n => gridFuncao.Rows.Add("", n.codFuncao, n.descFuncao));

                if (gridPerfil.Rows.Count > 0)
                {
                    //Pesquisa o acesso das funções
                    PesqAcessoFuncao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o acesso dos perfís
        private void PesqAcessoFuncao()
        {
            try
            {
                //Instância o objeto
                FuncaoNegocios funcaoNegocios = new FuncaoNegocios();
                //Instância a coleção
                AcessoCollection funcaoCollection = new AcessoCollection();
                //A coleção recebe o resultado da consulta
                funcaoCollection = funcaoNegocios.PesqAcessoFuncao();

                this.funcaoCollection = funcaoCollection;

                //Exibe os dados de acesso do perfíl
                DadosFuncaoAcesso();
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
                //Instância as linha da tabela
                DataGridViewRow linha = gridPerfil.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Seta o valor do código
                txtCodigo.Text = gridPerfil.Rows[indice].Cells[0].Value.ToString();
                //Seta o valor do perfil
                txtPerfil.Text = gridPerfil.Rows[indice].Cells[1].Value.ToString();
                //Desabilita todos os campos
                Desabilita();
                //Controle para alterar
                opcao = false;
                //Exibe os dados de acesso
                DadosFuncaoAcesso();

            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Exibe os dados de acesso
        private void DadosFuncaoAcesso()
        {
            try
            {
                //Limpa o grid acesso
                gridFuncao.Rows.Clear();
                //Localizando o código de barra
                List<Acesso> funcao = this.funcaoCollection.FindAll(delegate (Acesso n) { return n.codPerfil == Convert.ToInt32(txtCodigo.Text); });
                //Adiciona no grid a função
                funcao.ForEach(n => gridFuncao.Rows.Add(n.codAcesso, n.codFuncao, n.descFuncao, n.lerFuncao, n.escreverFuncao, n.editarFuncao, n.excluirFuncao, n.paiFuncao, n.filhoFuncao, n.itemFilhoFuncao));

                if (gridFuncao.Rows.Count > 0)
                {
                    //Qtd de funções
                    lblQtdFuncao.Text = (gridFuncao.RowCount).ToString();

                    for (int i = 0; gridFuncao.Rows.Count > i; i++)
                    {
                        //Função menu
                        if (gridFuncao.Rows[i].Cells[7].Value.ToString().Equals("S") && gridFuncao.Rows[i].Cells[8].Value.ToString().Equals("N"))
                        {
                            gridFuncao.Rows[i].DefaultCellStyle.BackColor = Color.MediumSeaGreen;
                        }
                        //Função sub-menu
                        else if (gridFuncao.Rows[i].Cells[7].Value.ToString().Equals("S") && gridFuncao.Rows[i].Cells[8].Value.ToString().Equals("S"))
                        {
                            gridFuncao.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                        //Função sub-menu-menu
                        else if (gridFuncao.Rows[i].Cells[7].Value.ToString() == "S" && gridFuncao.Rows[i].Cells[8].Value.ToString() == "S" && gridFuncao.Rows[i].Cells[9].Value.ToString() == "S")
                        {
                            gridFuncao.Rows[i].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                        }
                    }

                    //Seleciona a primeira linha do grid
                    gridFuncao.CurrentCell = gridFuncao.Rows[0].Cells[2];

                    //Pesquisa as permissões de acesso
                    DadosPermisaoFuncao();


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados de acesso! \nDetalhes" + ex, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DadosPermisaoFuncao()
        {
            try
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridFuncao.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //ler
                chkLer.Checked = Convert.ToBoolean(gridFuncao.Rows[indice].Cells[3].Value.ToString());
                //escrever
                chkEscrever.Checked = Convert.ToBoolean(gridFuncao.Rows[indice].Cells[4].Value.ToString());
                //editar
                chkEditar.Checked = Convert.ToBoolean(gridFuncao.Rows[indice].Cells[5].Value.ToString());
                //editar
                chkExcluir.Checked = Convert.ToBoolean(gridFuncao.Rows[indice].Cells[6].Value.ToString());

                //Desabilita o escrever, editar e excluir dos menus principais
                if (gridFuncao.Rows[indice].Cells[7].Value.ToString() == "S")
                {
                    //desabilita o checkbox escrever
                    chkEscrever.Enabled = false;
                    //desabilita o checkbox editar
                    chkEditar.Enabled = false;
                    //desabilita o checkbox excluir
                    chkExcluir.Enabled = false;
                }
                else
                {
                    //habilita o checkbox escrever
                    chkEscrever.Enabled = true;
                    //habilita o checkbox editar
                    chkEditar.Enabled = true;
                    //habilita o checkbox excluir
                    chkExcluir.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Permissão de acesso não encontrada! \nDetalhes" + ex, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AtualizarPermissao()
        {
            try
            {
                //Instância o objeto
                FuncaoNegocios funcaoNegocios = new FuncaoNegocios();
                //Instância o objeto
                Acesso funcao = new Acesso();
                //Instância as linha da tabela
                DataGridViewRow linha = gridFuncao.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;
                //Passa para a camada de negocios
                funcaoNegocios.AtualizaPermissao(Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(gridFuncao.Rows[indice].Cells[1].Value), chkLer.Checked, chkEscrever.Checked, chkEditar.Checked, chkExcluir.Checked);
                //Alterar as permisões no grid                   
                gridFuncao.Rows[indice].Cells[3].Value = chkLer.Checked;
                gridFuncao.Rows[indice].Cells[4].Value = chkEscrever.Checked;
                gridFuncao.Rows[indice].Cells[5].Value = chkEditar.Checked;
                gridFuncao.Rows[indice].Cells[6].Value = chkExcluir.Checked;

                //Atualiza a coleção
                var colecao = funcaoCollection.Where(n => n.codAcesso == Convert.ToInt32(gridFuncao.Rows[indice].Cells[0].Value));

                foreach (var customer in colecao)
                {
                    customer.lerFuncao = chkLer.Checked;
                    customer.escreverFuncao = chkEscrever.Checked;
                    customer.editarFuncao = chkEditar.Checked;
                    customer.excluirFuncao = chkExcluir.Checked;
                }

                MessageBox.Show("Acesso alterado com sucesso! ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpaCampos()
        {
            //Limpa o campo código
            txtCodigo.Clear();
            //Limpa o campo perfil
            txtPerfil.Clear();
            //desmarca o checkbox ler
            chkLer.Checked = false;
            //desmarca o checkbox ler
            chkEscrever.Checked = false;
            //desmarca o checkbox ler
            chkEditar.Checked = false;
            //desmarca o checkbox ler
            chkExcluir.Checked = false;
        }
        //Verifica o perfíl
        private void VerificaPerfilHabilita()
        {
            if (gridPerfil.Rows.Count > 0)
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridPerfil.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                if (opcao == false && gridPerfil.Rows[indice].Cells[1].Value.ToString() == "ADMINISTRADOR")
                {
                    MessageBox.Show("Perfíl padrão do sistema!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else if (opcao == false && gridPerfil.Rows[indice].Cells[1].Value.ToString() == "CONFERENTE")
                {
                    //Habilta
                    Habilita();
                    //Desabilita o perfil
                    txtPerfil.Enabled = false;
                }
                else if (opcao == false && gridPerfil.Rows[indice].Cells[1].Value.ToString() == "SEPARADOR")
                {
                    //Habilta
                    Habilita();
                    //Desabilita o perfil
                    txtPerfil.Enabled = false;
                }
                else if (opcao == false && gridPerfil.Rows[indice].Cells[1].Value.ToString() == "EMPILHADOR")
                {
                    //Habilta
                    Habilita();
                    //Desabilita o perfil
                    txtPerfil.Enabled = false;
                }
                else if (opcao == false && gridPerfil.Rows[indice].Cells[1].Value.ToString() == "PERSONALIZADO")
                {
                    MessageBox.Show("Perfíl padrão do sistema!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    //Habilta
                    Habilita();
                }

            }
        }

        private void Habilita()
        {
            //Habilita o perfil
            txtPerfil.Enabled = true;
            //Habilita o botão salvar
            btnSalvar.Enabled = true;
            //Desabilita o botão novo
            btnNovo.Enabled = false;


            if (opcao == true)
            {
                //desabilita o grid função
                gridFuncao.Enabled = false;
                //desabilita o checkbox ler
                chkLer.Enabled = false;
                //desabilita o checkbox escrever
                chkEscrever.Enabled = false;
                //desabilita o checkbox editar
                chkEditar.Enabled = false;
                //desabilita o checkbox excluir
                chkExcluir.Enabled = false;
                //desabilita o botão atulizar
                btnAtualizar.Enabled = false;
            }
            else
            {
                //habilita o grid função
                gridFuncao.Enabled = true;
                //habilita o checkbox ler
                chkLer.Enabled = true;
                //habilita o checkbox escrever
                chkEscrever.Enabled = true;
                //habilita o checkbox editar
                chkEditar.Enabled = true;
                //habilita o checkbox excluir
                chkExcluir.Enabled = true;
                //hailita o botão atualizar              
                btnAtualizar.Enabled = true;

                //Desabilita o escrever, editar e excluir do menu principal cadastro
                if (gridFuncao.Rows[0].Cells[7].Value.ToString() == "True")
                {
                    chkEscrever.Enabled = false;
                    chkEditar.Enabled = false;
                    chkExcluir.Enabled = false;
                }
            }

            //Foca no campo perfíl
            txtPerfil.Focus();
        }

        private void Desabilita()
        {
            //desbilita a perfil
            txtPerfil.Enabled = false;
            //desabilita o botão
            btnSalvar.Enabled = false;
            //desabilita o grid função
            gridFuncao.Enabled = false;
            //desabilita o checkbox ler
            chkLer.Enabled = false;
            //desabilita o checkbox escrever
            chkEscrever.Enabled = false;
            //desabilita o checkbox editar
            chkEditar.Enabled = false;
            //desabilita o checkbox excluir
            chkExcluir.Enabled = false;
            //desabilita o botão atulizar
            btnAtualizar.Enabled = false;

            //Habilita o botão novo
            btnNovo.Enabled = true;
        }


    }
}