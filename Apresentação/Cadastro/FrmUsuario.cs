using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Wms
{
    public partial class FrmUsuario : Form
    {
        //Controle para salvar e alterar
        bool opcao = false;
        //Instância a coleção
        UsuarioCollection usuarioCollection = new UsuarioCollection();
        //Instância a coleção
        PerfilCollection perfilCollection = new PerfilCollection();
        //Instância a coleção
        AcessoCollection acessoCollection = new AcessoCollection();
        //Controla a foto do usuário
        private long tamanhoArquivoImagem = 0;
        //Vertor de imagens
        private byte[] vetorImagens;

        //Controle de acesso
        public string perfilUsuario; //Perfil do usuário
        public List<Acesso> acesso; //Acesso Liberados
        public List<Empresa> empresaCollection;


        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
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
        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo nome
                txtLogin.Focus();
            }
        }

        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (opcao == true)
                {
                    //Foca no campo senha
                    txtSenha.Focus();
                }
                else
                {
                    //Foca no campo perfíl
                    cmbPerfil.Focus();
                }
            }
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo perfíl
                cmbPerfil.Focus();
            }
        }

        private void cmbPerfil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo email
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (chkSenha.Checked == true)
                {
                    //Foca no campo dias para expirar
                    txtDiasExpirar.Focus();
                }
                else
                {
                    //Foca no botão salvar
                    btnSalvar.Focus();
                }
            }
        }

        private void txtExpirarSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no campo data para expirar
                txtDataExpiracao.Focus();
            }
        }

        private void txtDtExpiracao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Foca no botão salvar
                btnSalvar.Focus();
            }
        }

        //KeyUp
        private void gridUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            //Exibe os dados
            DadosCampos();
        }

        private void gridFuncao_KeyUp(object sender, KeyEventArgs e)
        {
            //seta os dados da funções
            DadosPermisaoFuncao();
            //Habilita o acesso
            HabilitarAcesso();
        }

        //Changed
        private void txtExpirarSenha_TextChanged(object sender, EventArgs e)
        {
            //Calcula data para expira
            CalculaData();
        }

        //KeyDown
        private void gridUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            //Exibe os dados
            DadosCampos();
        }

        private void gridFuncao_KeyDown(object sender, KeyEventArgs e)
        {
            //seta os dados da funções
            DadosPermisaoFuncao();
            //Habilita o acesso
            HabilitarAcesso();
        }

        //Mouse Click
        private void chkSenha_MouseClick(object sender, MouseEventArgs e)
        {
            //Controla senha
            ControlaSenha();
        }

        private void gridUsuario_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Exibe os dados
            DadosCampos();
        }

        private void gridFuncao_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //seta os dados da funções
            DadosPermisaoFuncao();
            //Habilita o acesso
            HabilitarAcesso();
        }

        //Double click
        private void gridUsuario_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (gridUsuario.SelectedRows.Count > 0)
                {
                    //Controle para alterar  
                    opcao = false;
                    //Habilita campos
                    Habilita();
                }
            }
        }

        //Click
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //Pesquisa usuários
            PesqUsuario();
        }

        private void adicionarFoto_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                if (!(txtCodigo.Text.Equals("")))
                {
                    CarregaImagem();
                }
                else
                {
                    MessageBox.Show("Clique no botão 'Novo' para cadastrar um novo usuário ou\n pesquise um usuário para alterar a foto!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (acesso[0].escreverFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (!(txtCodigo.Text.Equals("")))
                {
                    CarregaImagem();
                }
                else
                {
                    MessageBox.Show("Clique no botão 'Novo' para cadastrar um novo usuário ou\n pesquise um usuário para alterar a foto!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void excluirFoto_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Limpa a imagem
                picFoto.Image = null;
                //Limpa o vetor
                vetorImagens = null;
            }
            else if (acesso[0].excluirFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Limpa a imagem
                picFoto.Image = null;
                //Limpa o vetor
                vetorImagens = null;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Pesquisa os perfís
                PesqPerfil();
                //Pesquisa usuários
                //PesqUsuario();
                //pesquisa um novo id
                PesqId();
            }
            else if (acesso[0].escreverFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Pesquisa os perfís
                PesqPerfil();
                //Pesquisa usuários
                //PesqUsuario();
                //pesquisa um novo id
                PesqId();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Verifica o login para salvar ou alterar
                VerificarLogin();
            }
            else
            {
                if (acesso[0].escreverFuncao == false)
                {
                    MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    //Verifica o login para salvar ou alterar
                    VerificarLogin();
                }
            }

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Atualiza o acesso
                AtualizarPermissao();
            }
            else if (acesso[0].editarFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Atualiza o acesso
                AtualizarPermissao();
            }
        }

        private void btnResetar_Click(object sender, EventArgs e)
        {
            if (perfilUsuario == "ADMINISTRADOR")
            {
                //Reseta a senha
                ResetaSenha();
            }
            else if (acesso[0].editarFuncao == false)
            {
                MessageBox.Show("Operação restrita, por favor procure o administrador! ", "WMS - Acesso negado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //Reseta a senha
                ResetaSenha();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Fecha o formulário
            Dispose();
        }

        //Pesquisa um novo id
        private void PesqId()
        {
            try
            {
                //Limpa todos os campos
                LimpaCampos();
                //Instância o objeto
                UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                //Instância o objeto
                Usuario usuario = new Usuario();
                //Recebe o id
                usuario = usuarioNegocios.PesqId();
                //seta o código
                txtCodigo.Text = Convert.ToString(usuario.codUsuario);
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

        private void VerificarLogin()
        {

            if (opcao == true)
            {
                //Pesquisa se já existe um login cadastrado
                List<Usuario> usuario = this.usuarioCollection.FindAll(delegate (Usuario n) { return n.login == txtLogin.Text; });
                //Recebe o novo login
                string novoLogin = null;
                //Adiciona o código do perfil
                usuario.ForEach(n => novoLogin = n.login);

                if (novoLogin == txtLogin.Text)
                {
                    MessageBox.Show("Login já cadastrado!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLogin.Focus();
                    txtLogin.SelectAll();
                }
                else
                {
                    //Salva o cadastro
                    Salvar();
                }
            }
            else
            {
                //Alterar o cadastro
                Alterar();
            }

        }

        private void Salvar()
        {
            try
            {
                if (txtCodigo.Text.Length == 0 || txtNome.Text.Length == 0 || txtLogin.Text.Length == 0 || txtSenha.Text.Length == 0 || cmbTurno.SelectedIndex == 0 || cmbPerfil.SelectedIndex == 0 || txtDiasExpirar.Text.Length == 0)
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (txtLogin.Text.Length < 5)
                {
                    MessageBox.Show("Digite um login com no mínimo 5 caracteres!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (chkSenha.Checked == true && txtDiasExpirar.Text.Equals(string.Empty) ||
                       chkSenha.Checked == true && txtDiasExpirar.Text.Equals("0"))
                    {
                        MessageBox.Show("Informa a quantidade de dias para controle de expiração de senha!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Recebe o código do perfíl
                        int codPerfil = 0;
                        //Localizando o perfil
                        List<Perfil> perfil = this.perfilCollection.FindAll(delegate (Perfil n) { return n.descPerfil == cmbPerfil.SelectedItem.ToString(); });
                        //Adiciona o código do perfil
                        perfil.ForEach(n => codPerfil = n.codPerfil);
                        //Instância o objeto
                        UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                        //Instância o objeto
                        Usuario usuario = new Usuario();
                        //Instância o objeto
                        SenhaMd5Negocios senhaMd5Negocios = new SenhaMd5Negocios();
                        //Seta o id 
                        usuario.codUsuario = Convert.ToInt32(txtCodigo.Text);
                        //Seta o nome
                        usuario.nome = txtNome.Text;
                        //Seta o login
                        usuario.login = txtLogin.Text;
                        //Seta a senha
                        usuario.senha = senhaMd5Negocios.md5(txtSenha.Text);
                        //seta o turno
                        usuario.turno = cmbTurno.SelectedItem.ToString();
                        //seta senha no campo
                        txtSenha.Text = usuario.senha;
                        //Seta o id do perfíl
                        usuario.codPerfil = codPerfil;
                        //Seta o email
                        usuario.email = txtEmail.Text;
                        //Seta o dias
                        usuario.diasExpirar = Convert.ToInt32(txtDiasExpirar.Text);

                        if (chkSenha.Checked == true)
                        {
                            //Seta a data
                            usuario.dataExpiracao = Convert.ToDateTime(txtDataExpiracao.Text);
                        }
                        else
                        {
                            //Seta a data
                            usuario.dataExpiracao = null;                            
                        }

                        //Seta o satus
                        usuario.status = chkStatus.Checked;
                        //Seta o controle de senha
                        usuario.controlaSenha = chkSenha.Checked;
                        //foto do usuário
                        usuario.foto = vetorImagens;

                        //Passa o perfil para a camada de negocios
                        usuarioNegocios.Salvar(usuario, cmbEmpresa.Text);







                        //Insere o cadastro na tabela
                        gridUsuario.Rows.Add(txtCodigo.Text, txtNome.Text, txtLogin.Text, txtSenha.Text, cmbTurno.SelectedItem.ToString(), usuario.status, usuario.controlaSenha, cmbPerfil.SelectedItem, txtEmail.Text, txtDiasExpirar.Text, txtDataExpiracao.Text, vetorImagens);
                        //Recebe a qtd de linha na tabela 
                        int linha = gridUsuario.RowCount;
                        //Seleciona a linha      
                        gridUsuario.CurrentCell = gridUsuario.Rows[linha - 1].Cells[0];
                        //Soma a qtd de usuário
                        lblQtd.Text = Convert.ToString(Convert.ToInt32(lblQtd.Text) + 1);
                        //Desabilita todos os campos
                        Desabilita();
                        //controle para alterar
                        opcao = false;
                        //Pesquisa o acesso
                        PesqAcessoUsuario();
                        MessageBox.Show("Cadastro realizado com sucesso!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Alterar()
        {
            try
            {
                if (txtCodigo.Text.Length == 0 || txtNome.Text.Length == 0 || txtLogin.Text.Length == 0 || txtSenha.Text.Length == 0 || cmbTurno.SelectedIndex == 0 || cmbPerfil.SelectedIndex == 0 || txtDiasExpirar.Text.Length == 0)
                {
                    //Mensagem
                    MessageBox.Show("Preencha todos os campos!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (txtLogin.Text.Length < 5)
                {
                    MessageBox.Show("Digite um login com no mínimo 5 caracteres!", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    //Recebe o código do perfíl
                    int codPerfil = 0;
                    //Localizando o código de perfíl
                    List<Perfil> perfil = this.perfilCollection.FindAll(delegate (Perfil n) { return n.descPerfil == cmbPerfil.SelectedItem.ToString(); });
                    //Adiciona o código do perfil
                    perfil.ForEach(n => codPerfil = n.codPerfil);
                    //Instância o objeto
                    UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                    //Instância o objeto
                    Usuario usuario = new Usuario();
                    //Seta o id 
                    usuario.codUsuario = Convert.ToInt32(txtCodigo.Text);
                    //Seta o nome
                    usuario.nome = txtNome.Text;
                    //Seta o login
                    usuario.login = txtLogin.Text;
                    //Seta o turno
                    usuario.turno = cmbTurno.SelectedItem.ToString();
                    //Seta o id do perfíl
                    usuario.codPerfil = codPerfil;
                    //Seta o email
                    usuario.email = txtEmail.Text;
                    //Seta o dias
                    usuario.diasExpirar = Convert.ToInt32(txtDiasExpirar.Text);
                    //Seta a data
                    usuario.dataExpiracao = Convert.ToDateTime(txtDataExpiracao.Text);
                    //Seta o satus
                    usuario.status = chkStatus.Checked;
                    //Seta o controle de senha
                    usuario.controlaSenha = chkSenha.Checked;
                    //seta a foto
                    usuario.foto = vetorImagens;

                    //Passa para a camada de negocios
                    usuarioNegocios.Alterar(usuario, cmbEmpresa.Text);

                    //Instância as linha da tabela
                    DataGridViewRow linha = gridUsuario.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;
                    //Insere o nome no grid                      
                    gridUsuario.Rows[indice].Cells[1].Value = txtNome.Text;
                    //Insere o login no grid                      
                    gridUsuario.Rows[indice].Cells[2].Value = txtLogin.Text;
                    //Insere o senha no grid                      
                    gridUsuario.Rows[indice].Cells[3].Value = txtSenha.Text;
                    //Insere o senha no grid                      
                    gridUsuario.Rows[indice].Cells[4].Value = cmbTurno.SelectedItem.ToString();
                    //Insere o status no grid                     
                    gridUsuario.Rows[indice].Cells[5].Value = chkStatus.Checked;
                    //Insere o controla senha no grid                     
                    gridUsuario.Rows[indice].Cells[6].Value = chkSenha.Checked;
                    //Insere o perfíl no grid                     
                    gridUsuario.Rows[indice].Cells[7].Value = cmbPerfil.SelectedItem;
                    //Insere o email no grid                     
                    gridUsuario.Rows[indice].Cells[8].Value = txtEmail.Text;
                    //Insere o dias no grid                     
                    gridUsuario.Rows[indice].Cells[9].Value = txtDiasExpirar.Text;
                    //Insere o data de expirar no grid                     
                    gridUsuario.Rows[indice].Cells[10].Value = txtDataExpiracao.Text;
                    //Insere a foto no grid                    
                    gridUsuario.Rows[indice].Cells[11].Value = vetorImagens;
                    //Foca no grid
                    gridUsuario.Focus();
                    //Desabilita todos os campos
                    Desabilita();

                    MessageBox.Show("Cadastro alterado com sucesso! ", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Reseta a senha
        private void ResetaSenha()
        {
            try
            {
                if (gridUsuario.Rows.Count > 0)
                {
                    //Instância o objeto
                    UsuarioNegocios usuarioNegocios = new UsuarioNegocios();
                    //Instância o objeto
                    SenhaMd5Negocios senhaMd5Negocios = new SenhaMd5Negocios();
                    //Seta a senha
                    string senha = txtLogin.Text.Substring(txtLogin.Text.Length - txtLogin.Text.Length, 3) + "@123.";
                    //Hash   
                    senha = senhaMd5Negocios.md5(senha);
                    //Passa para a camada de negocios
                    usuarioNegocios.ResetaSenha(Convert.ToInt32(txtCodigo.Text), senha);

                    MessageBox.Show("Senha resetada com sucesso! ", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Pesquisa o usuário cadastrados        
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

                if (usuarioCollection.Count > 0)
                {
                    //Pesquisa os perfís
                    PesqPerfil();

                    //Limpa todos os campos
                    LimpaCampos();
                    //Limpa o grid
                    gridUsuario.Rows.Clear();
                    //Grid Recebe o resultado da coleção
                    usuarioCollection.ForEach(n => gridUsuario.Rows.Add(n.codUsuario, n.nome, n.login, n.senha, n.turno, n.status, n.controlaSenha, n.perfil, n.email, n.diasExpirar, n.dataExpiracao, n.foto));

                    //Controla o novo login (Não deixa cadastrar um login repetido)
                    this.usuarioCollection = usuarioCollection;
                    //Qtd de usuário encontrado
                    lblQtd.Text = gridUsuario.RowCount.ToString();
                    //Seleciona a primeira linha do grid
                    gridUsuario.CurrentCell = gridUsuario.Rows[0].Cells[0];
                    //Foca no grid
                    gridUsuario.Focus();
                    //Seta os dados nos campos
                    DadosCampos();
                    //Pesquisa o acesso
                    PesqAcessoUsuario();
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
                //Limpa o campo perfíl
                cmbPerfil.Items.Clear();
                cmbPerfil.Items.Add("SELECIONE");
                //Grid Recebe o resultado da coleção
                perfilCollection.ForEach(n => cmbPerfil.Items.Add(n.descPerfil));
                //Seta a coleção local na coleção global
                this.perfilCollection = perfilCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pesquisa o acesso dos usuários
        private void PesqAcessoUsuario()
        {
            try
            {
                //Instância o objeto
                FuncaoNegocios funcaoNegocios = new FuncaoNegocios();
                //Instância a coleção
                AcessoCollection acessoCollection = new AcessoCollection();
                //A coleção recebe o resultado da consulta
                acessoCollection = funcaoNegocios.PesqAcessoUsuario();

                this.acessoCollection = acessoCollection;

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
                if (gridUsuario.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridUsuario.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    //Seta o valor do código
                    txtCodigo.Text = gridUsuario.Rows[indice].Cells[0].Value.ToString();
                    //Seta o nome
                    txtNome.Text = Convert.ToString(gridUsuario.Rows[indice].Cells[1].Value);
                    //Seta o login
                    txtLogin.Text = Convert.ToString(gridUsuario.Rows[indice].Cells[2].Value);
                    //Seta a senha
                    txtSenha.Text = Convert.ToString(gridUsuario.Rows[indice].Cells[3].Value);
                    //seta o turno
                    cmbTurno.SelectedItem = Convert.ToString(gridUsuario.Rows[indice].Cells[4].Value);
                    //Seta o status
                    chkStatus.Checked = Convert.ToBoolean(gridUsuario.Rows[indice].Cells[5].Value.ToString());
                    //seta a controla senha
                    chkSenha.Checked = Convert.ToBoolean(gridUsuario.Rows[indice].Cells[6].Value.ToString());
                    //Seta o perfil
                    cmbPerfil.SelectedItem = Convert.ToString(gridUsuario.Rows[indice].Cells[7].Value);
                    //Seta o email
                    txtEmail.Text = Convert.ToString(gridUsuario.Rows[indice].Cells[8].Value);
                    //Seta o dias
                    txtDiasExpirar.Text = Convert.ToString(gridUsuario.Rows[indice].Cells[9].Value);
                    //Seta o data de expiração
                    txtDataExpiracao.Text = Convert.ToDateTime(gridUsuario.Rows[indice].Cells[10].Value).ToShortDateString();
                    //seta a foto
                    vetorImagens = (byte[])gridUsuario.Rows[indice].Cells[11].Value;

                    if (vetorImagens == null)
                    {
                        //Limpa a imagem
                        picFoto.Image = null;
                    }
                    else
                    {
                        //exibe a imagem
                        string strNomeArquivo = Convert.ToString(DateTime.Now.ToFileTime());
                        FileStream fs = new FileStream(strNomeArquivo, FileMode.CreateNew, FileAccess.Write);
                        fs.Write(vetorImagens, 0, vetorImagens.Length);
                        fs.Flush();
                        fs.Close();
                        picFoto.Image = Image.FromFile(strNomeArquivo);
                    }

                    //Desabilita todos os campos
                    Desabilita();
                    //Controle para alterar
                    opcao = false;
                    //Exibe os dados de acesso
                    DadosFuncaoAcesso();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao exibir os dados nos campos! ", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //Exibe os dados de acesso
        private void DadosFuncaoAcesso()
        {
            try
            {
                //Limpa o grid acesso
                gridFuncao.Rows.Clear();
                //Localizando o perfíl
                List<Acesso> funcao = this.acessoCollection.FindAll(delegate (Acesso n) { return n.codUsuario == Convert.ToInt32(txtCodigo.Text); });
                //Adiciona no grid perfíl
                funcao.ForEach(n => gridFuncao.Rows.Add(n.codAcesso, n.codFuncao, n.descFuncao, n.lerFuncao, n.escreverFuncao, n.editarFuncao, n.excluirFuncao, n.paiFuncao, n.filhoFuncao, n.itemFilhoFuncao));

                if (gridFuncao.Rows.Count > 0)
                {
                    //Qtd de funções
                    lblQtdFuncao.Text = (gridFuncao.RowCount - 8).ToString();

                    for (int indice = 0; indice < gridFuncao.RowCount; indice++)
                    {

                        if (gridFuncao.Rows[indice].Cells[7].Value.ToString() == "S" && gridFuncao.Rows[indice].Cells[8].Value.ToString() == "N")
                        {
                            gridFuncao.Rows[indice].DefaultCellStyle.BackColor = Color.MediumSeaGreen;
                        }
                        else if (gridFuncao.Rows[indice].Cells[7].Value.ToString() == "S" && gridFuncao.Rows[indice].Cells[8].Value.ToString() == "S")
                        {
                            gridFuncao.Rows[indice].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                        else if (gridFuncao.Rows[indice].Cells[7].Value.ToString() == "N" && gridFuncao.Rows[indice].Cells[8].Value.ToString() == "N" && gridFuncao.Rows[indice].Cells[9].Value.ToString() == "S")
                        {
                            gridFuncao.Rows[indice].DefaultCellStyle.BackColor = Color.WhiteSmoke;
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
                MessageBox.Show("Ocorreu um erro ao exibir os dados de acesso! \nDetalhes" + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                //Ler
                chkLer.Checked = Convert.ToBoolean(gridFuncao.Rows[indice].Cells[3].Value.ToString());
                //Escrever
                chkEscrever.Checked = Convert.ToBoolean(gridFuncao.Rows[indice].Cells[4].Value.ToString());
                //Editar
                chkEditar.Checked = Convert.ToBoolean(gridFuncao.Rows[indice].Cells[5].Value.ToString());
                //Excluir
                chkExcluir.Checked = Convert.ToBoolean(gridFuncao.Rows[indice].Cells[6].Value.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Permissão de acesso não encontrada! \nDetalhes" + ex, "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                funcaoNegocios.AtualizaPermissaoUsuario(Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(gridFuncao.Rows[indice].Cells[1].Value), chkLer.Checked, chkEscrever.Checked, chkEditar.Checked, chkExcluir.Checked);
                //Alterar as permisões no grid                   
                gridFuncao.Rows[indice].Cells[3].Value = chkLer.Checked;
                gridFuncao.Rows[indice].Cells[4].Value = chkEscrever.Checked;
                gridFuncao.Rows[indice].Cells[5].Value = chkEditar.Checked;
                gridFuncao.Rows[indice].Cells[6].Value = chkExcluir.Checked;

                //Atualiza a coleção
                var colecao = acessoCollection.Where(n => n.codAcesso == Convert.ToInt32(gridFuncao.Rows[indice].Cells[0].Value));

                foreach (var customer in colecao)
                {
                    customer.lerFuncao = chkLer.Checked;
                    customer.escreverFuncao = chkEscrever.Checked;
                    customer.editarFuncao = chkEditar.Checked;
                    customer.excluirFuncao = chkExcluir.Checked;
                }

                MessageBox.Show("Acesso alterado com sucesso! ", "WMS - Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            //Limpa o campo nome
            txtNome.Clear();
            //Limpa o campo login
            txtLogin.Clear();
            //Limpa o campo senha
            txtSenha.Clear();
            //Limpa o campo turno
            cmbTurno.SelectedItem = "SELECIONE";
            //Limpa o campo perfíl
            cmbPerfil.SelectedItem = "SELECIONE";
            //Limpa o campo email
            txtEmail.Clear();
            //Limpa o campo dias
            txtDiasExpirar.Clear();
            //Limpa o campo data de expirar
            txtDataExpiracao.Clear();
            //Marca o checkbox status
            chkStatus.Checked = true;
            //Marca o checkbox controla senha
            chkSenha.Checked = true;
            //desmarca o checkbox ler
            chkLer.Checked = false;
            //desmarca o checkbox ler
            chkEscrever.Checked = false;
            //desmarca o checkbox ler
            chkEditar.Checked = false;
            //desmarca o checkbox ler
            chkExcluir.Checked = false;
        }

        private void HabilitarAcesso()
        {
            if (gridFuncao.Rows.Count > 0)
            {
                //Instância as linha da tabela
                DataGridViewRow linha = gridFuncao.CurrentRow;
                //Recebe o indice   
                int indice = linha.Index;

                //Instância as linha da tabela
                DataGridViewRow linhaUsuario = gridUsuario.CurrentRow;
                //Recebe o indice   
                int indiceUsuario = linhaUsuario.Index;

                if (gridUsuario.Rows[indiceUsuario].Cells[7].Value.ToString() != "ADMINISTRADOR")
                {
                    //Habilita o botão atualizar
                    btnAtualizar.Enabled = true;

                    //Desabilita o escrever, editar e excluir dos menus principais
                    if (gridFuncao.Rows[indice].Cells[7].Value.ToString() == "S")
                    {
                        //habilita o checkbox ler
                        chkLer.Enabled = true;
                        //desabilita o checkbox escrever
                        chkEscrever.Enabled = false;
                        //desabilita o checkbox editar
                        chkEditar.Enabled = false;
                        //desabilita o checkbox excluir
                        chkExcluir.Enabled = false;
                    }
                    else
                    {
                        //habilita o checkbox ler
                        chkLer.Enabled = true;
                        //habilita o checkbox escrever
                        chkEscrever.Enabled = true;
                        //habilita o checkbox editar
                        chkEditar.Enabled = true;
                        //habilita o checkbox excluir
                        chkExcluir.Enabled = true;
                    }
                }
            }
        }

        private void Habilita()
        {
            //Habilita o campo nome
            txtNome.Enabled = true;
            //Habilita o campo login
            txtLogin.Enabled = true;
            //Habilita o campo turno
            cmbTurno.Enabled = true;
            //Habilita o campo perfíl
            cmbPerfil.Enabled = true;
            //Habilita o campo email
            txtEmail.Enabled = true;
            //Habilita o checkbox controla senha
            chkSenha.Enabled = true;
            //Habilita o checkbox status
            chkStatus.Enabled = true;
            //Habilita foto
            picFoto.Enabled = true;
            //Desabilita o botão novo
            btnNovo.Enabled = false;
            //Habilita o botão salvar
            btnSalvar.Enabled = true;


            if (chkSenha.Checked == true)
            {
                //Habilita o campo expirar senha
                txtDiasExpirar.Enabled = true;
            }
            else
            {
                //Habilita o campo expirar senha
                txtDiasExpirar.Enabled = false;
            }


            if (opcao == true)
            {
                //Habilita o campo senha
                txtSenha.Enabled = true;
                //Seleciona o checkbox status
                chkStatus.Checked = true;
            }

            if (opcao == false)
            {
                if (gridUsuario.Rows.Count > 0)
                {
                    //Instância as linha da tabela
                    DataGridViewRow linha = gridUsuario.CurrentRow;
                    //Recebe o indice   
                    int indice = linha.Index;

                    if (gridUsuario.Rows[indice].Cells[7].Value.ToString() != "ADMINISTRADOR")
                    {
                        //Habilita o acesso
                        HabilitarAcesso();
                    }

                    //Habilita o grid função
                    gridFuncao.Enabled = true;
                }
            }
            //Foca no campo nome
            txtNome.Focus();
        }

        private void Desabilita()
        {
            //desabilita o campo nome
            txtNome.Enabled = false;
            //desabilita o campo login
            txtLogin.Enabled = false;
            //desabilita o campo senha
            txtSenha.Enabled = false;
            //desabilita o campo turno
            cmbTurno.Enabled = false;
            //desabilita o campo perfíl
            cmbPerfil.Enabled = false;
            //desabilita o campo email
            txtEmail.Enabled = false;
            //desabilita o campo expirar senha
            txtDiasExpirar.Enabled = false;
            //desabilita o checkbox controla senha
            chkSenha.Enabled = false;
            //desabilita o checkbox status
            chkStatus.Enabled = false;
            //Desabilita foto
            picFoto.Enabled = false;
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

        private void ControlaSenha()
        {
            if (chkSenha.Checked == true)
            {
                //Habilita o campo expirar senha
                txtDiasExpirar.Enabled = true;
                //limpa o campo Dias para expirar
                txtDiasExpirar.Clear();
                //limpa o campo expirar
                txtDataExpiracao.Clear();
            }
            else
            {
                //Desabilita o campo expirar senha
                txtDiasExpirar.Enabled = false;
                //Calcula senha
                CalculaData();
            }
        }

        private void CalculaData()
        {
            try
            {
                if (chkSenha.Checked == false)
                {
                    if (txtDiasExpirar.Text == "")
                    {
                        //Dias para expirar
                        txtDiasExpirar.Text = "0";
                        //Soma a data de expiração
                        txtDataExpiracao.Text = DateTime.Today.ToShortDateString();
                    }
                }
                else
                {
                    //Soma a data de expiração
                    txtDataExpiracao.Text = DateTime.Today.AddDays(Convert.ToInt32(txtDiasExpirar.Text)).ToShortDateString();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Ocorreu um erro ao calcular a data!", "WMS - Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void CarregaImagem()
        {
            try
            {
                this.openFileDialog1.ShowDialog(this);
                string strFn = this.openFileDialog1.FileName;

                if (string.IsNullOrEmpty(strFn))
                    return;

                this.picFoto.Image = Image.FromFile(strFn);
                FileInfo arqImagem = new FileInfo(strFn);
                tamanhoArquivoImagem = arqImagem.Length;
                FileStream fs = new FileStream(strFn, FileMode.Open, FileAccess.Read, FileShare.Read);
                vetorImagens = new byte[Convert.ToInt32(this.tamanhoArquivoImagem)];
                int iBytesRead = fs.Read(vetorImagens, 0, Convert.ToInt32(this.tamanhoArquivoImagem));
                fs.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void FrmUsuario_Load_1(object sender, EventArgs e)
        {

        }

        private void cmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
